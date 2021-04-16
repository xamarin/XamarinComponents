using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Bluetooth;
using Android.Gms.Common.Apis;
using Android.Gms.Nearby.ExposureNotification;
using Android.Runtime;
using AndroidX.Work;
using Java.Nio.FileNio;

using AndroidCalibrationConfidence = Android.Gms.Nearby.ExposureNotification.CalibrationConfidence;
using AndroidDailySummary = Android.Gms.Nearby.ExposureNotification.DailySummary;
using AndroidInfectiousness = Android.Gms.Nearby.ExposureNotification.Infectiousness;
using AndroidReportType = Android.Gms.Nearby.ExposureNotification.ReportType;
using AndroidRiskLevel = Android.Gms.Nearby.ExposureNotification.RiskLevel;
using AndroidScanInstance = Android.Gms.Nearby.ExposureNotification.ScanInstance;

using Nearby = Android.Gms.Nearby.NearbyClass;

[assembly: UsesFeature("android.hardware.bluetooth_le", Required = true)]
[assembly: UsesFeature("android.hardware.bluetooth")]
[assembly: UsesPermission(Android.Manifest.Permission.Bluetooth)]


namespace Xamarin.ExposureNotifications
{
	public static partial class ExposureNotification
	{
		static readonly Lazy<IExposureNotificationClient?> instance = new Lazy<IExposureNotificationClient?>(() =>
		{
			return Nearby.GetExposureNotificationClient(Application.Context);
		});

		// get an instance that may not be ready
		static IExposureNotificationClient? Instance => instance.Value;

		// get the instance that is ready
		static IExposureNotificationClient GetClient()
		{
			EnsureSupported();

			var client = Instance!;

			return client;
		}

		// Not really "obsolete" as this is just Google's recommendation
		// and v1 might still be the only thing on the device.
#pragma warning disable CS0618

		static async Task<ExposureConfiguration> GetConfigurationAsync()
		{
			var c = await Handler.GetConfigurationAsync();

			return new ExposureConfiguration.ExposureConfigurationBuilder()
				.SetAttenuationScores(c.AttenuationScores)
				.SetDurationScores(c.DurationScores)
				.SetDaysSinceLastExposureScores(c.DaysSinceLastExposureScores)
				.SetTransmissionRiskScores(c.TransmissionRiskScores)
				.SetAttenuationWeight(c.AttenuationWeight)
				.SetDaysSinceLastExposureWeight(c.DaysSinceLastExposureWeight)
				.SetDurationWeight(c.DurationWeight)
				.SetTransmissionRiskWeight(c.TransmissionWeight)
				.SetMinimumRiskScore(c.MinimumRiskScore)
				.SetDurationAtAttenuationThresholds(c.DurationAtAttenuationThresholds)
				.Build();
		}

#pragma warning restore CS0618

		const int requestCodeStartExposureNotification = 1111;
		const int requestCodeGetTempExposureKeyHistory = 2222;

		static TaskCompletionSource<bool>? tcsResolveConnection;

		public static void OnActivityResult(int requestCode, Result resultCode, global::Android.Content.Intent data)
		{
			if (requestCode == requestCodeStartExposureNotification || requestCode == requestCodeGetTempExposureKeyHistory)
			{
				if (resultCode == Result.Ok)
					tcsResolveConnection?.TrySetResult(true);
				else
					tcsResolveConnection?.TrySetException(new AccessDeniedException("Failed to resolve Exposure Notifications API"));
			}
		}

		static async Task<T> ResolveApi<T>(int requestCode, Func<Task<T>> apiCall)
		{
			try
			{
				return await apiCall();
			}
			catch (ApiException apiEx) when (apiEx.StatusCode == CommonStatusCodes.ResolutionRequired)
			{
				// Resolution required
				tcsResolveConnection = new TaskCompletionSource<bool>();

				// Start the resolution
				apiEx.Status.StartResolutionForResult(Essentials.Platform.CurrentActivity, requestCode);

				// Wait for the activity result to be called
				await tcsResolveConnection.Task;

				// Try the original api call again
				return await apiCall();
			}
		}

		static async void PlatformInit()
		{
			await ScheduleFetchAsync();
		}

		static Task PlatformStart()
			=> ResolveApi<bool>(requestCodeStartExposureNotification, async () =>
				{
					await GetClient().StartAsync();
					return default;
				});

		static Task PlatformStop()
			=> ResolveApi<bool>(requestCodeStartExposureNotification, async () =>
				{
					await GetClient().StopAsync();
					return default;
				});

		static Task<bool> PlatformIsEnabled()
			=> ResolveApi(requestCodeStartExposureNotification, () =>
				GetClient().IsEnabledAsync());

		public static void ConfigureBackgroundWorkRequest(TimeSpan repeatInterval, Action<PeriodicWorkRequest.Builder> requestBuilder)
		{
			if (requestBuilder == null)
				throw new ArgumentNullException(nameof(requestBuilder));
			if (repeatInterval == null)
				throw new ArgumentNullException(nameof(repeatInterval));

			bgRequestBuilder = requestBuilder;
			bgRepeatInterval = repeatInterval;
		}

		static Action<PeriodicWorkRequest.Builder> bgRequestBuilder = b =>
			b.SetConstraints(new Constraints.Builder()
				.SetRequiresBatteryNotLow(true)
				.SetRequiresDeviceIdle(true)
				.SetRequiredNetworkType(NetworkType.Connected)
				.Build());

		static TimeSpan bgRepeatInterval = TimeSpan.FromHours(6);

		static Task PlatformScheduleFetch()
		{
			if (!IsSupported)
				return Task.CompletedTask;

			var workManager = WorkManager.GetInstance(Essentials.Platform.AppContext);

			var workRequestBuilder = new PeriodicWorkRequest.Builder(
				typeof(BackgroundFetchWorker),
				bgRepeatInterval);

			bgRequestBuilder.Invoke(workRequestBuilder);

			var workRequest = workRequestBuilder.Build();

			workManager.EnqueueUniquePeriodicWork("exposurenotification",
				ExistingPeriodicWorkPolicy.Replace,
				workRequest);

			return Task.CompletedTask;
		}

		// Tells the local API when new diagnosis keys have been obtained from the server
		static async Task PlatformDetectExposuresAsync(IEnumerable<string> keyFiles, CancellationToken cancellationToken)
		{
			// Not really "obsolete" as this is just Google's recommendation
			// and v1 might still be the only thing on the device.
#pragma warning disable CS0618

			var config = await GetConfigurationAsync();

			// If the app supports v2, then use the special token.
			// If the device is still v1, then this special token has no meaning,
			// but, if it is v2 capable, then the device will use the v2 path (window mode)
			var token = DailySummaryHandler != null
				? ExposureNotificationClient.TokenA
				: Guid.NewGuid().ToString();

			// When going v2, the configuration is not actually used, but if
			// the device is still v1, then we need it

			await GetClient().ProvideDiagnosisKeysAsync(
				keyFiles.Select(f => new Java.IO.File(f)).ToList(),
				config,
				token);

#pragma warning restore CS0618
		}

		static Task<IEnumerable<TemporaryExposureKey>> PlatformGetTemporaryExposureKeys()
			=> ResolveApi(requestCodeGetTempExposureKeyHistory, async () =>
				{
					var exposureKeyHistory = await GetClient().GetTemporaryExposureKeyHistoryAsync();

					return exposureKeyHistory.Select(k =>
						new TemporaryExposureKey(
							k.GetKeyData(),
							k.RollingStartIntervalNumber,
							TimeSpan.FromMinutes(k.RollingPeriod * 10),
							k.TransmissionRiskLevel.FromNativeRiskLevel()));
				});

		internal static async Task<IEnumerable<ExposureInfo>> PlatformGetExposureInformationAsync(string token)
		{
			var exposures = await GetClient().GetExposureInformationAsync(token);
			var info = exposures.Select(d => new ExposureInfo(
				DateTimeOffset.UnixEpoch.AddMilliseconds(d.DateMillisSinceEpoch).UtcDateTime,
				TimeSpan.FromMinutes(d.DurationMinutes),
				d.AttenuationValue,
				d.TotalRiskScore,
				d.TransmissionRiskLevel.FromNativeRiskLevel()));
			return info;
		}

		internal static async Task<ExposureDetectionSummary> PlatformGetExposureSummaryAsync(string token)
		{
			var summary = await GetClient().GetExposureSummaryAsync(token);

			// TODO: Reevaluate byte usage here
			return new ExposureDetectionSummary(
				summary.DaysSinceLastExposure,
				(ulong)summary.MatchedKeyCount,
				summary.MaximumRiskScore,
				summary.GetAttenuationDurationsInMinutes()
					.Select(a => TimeSpan.FromMinutes(a)).ToArray(),
				summary.SummationRiskScore);
		}

		internal static async Task<IEnumerable<DailySummary>> PlatformGetDailySummariesAsync()
		{
			if (DailySummaryHandler == null)
				throw new InvalidOperationException("The handler does not support Exposure Window Mode.");

			var config = await DailySummaryHandler.GetDailySummaryConfigurationAsync();
			if (config == null)
				throw new InvalidOperationException("The daily summary configuration was not provided on the handler.");

			var builder = new DailySummariesConfig.DailySummariesConfigBuilder();

			var attenuationThresholds = config.AttenuationThresholds.Select(t => (Java.Lang.Integer)t).ToArray();
			var attenuationWeights = new Java.Lang.Double[4];
			var aW = config.AttenuationWeights;
			attenuationWeights[0] = new Java.Lang.Double(aW[DistanceEstimate.Immediate]);
			attenuationWeights[1] = new Java.Lang.Double(aW[DistanceEstimate.Near]);
			attenuationWeights[2] = new Java.Lang.Double(aW[DistanceEstimate.Medium]);
			attenuationWeights[3] = new Java.Lang.Double(aW[DistanceEstimate.Other]);

			builder.SetAttenuationBuckets(attenuationThresholds, attenuationWeights);

			builder.SetDaysSinceExposureThreshold(config.DaysSinceLastExposureThreshold);

			foreach (var pair in config.InfectiousnessWeights)
			{
				builder.SetInfectiousnessWeight(pair.Key.ToNative(), pair.Value);
			}

			foreach (var pair in config.ReportTypeWeights)
			{
				builder.SetReportTypeWeight(pair.Key.ToNative(), pair.Value);
			}

			var summaries = await GetClient().GetDailySummariesAsync(builder.Build());
			if (summaries == null || summaries.Count == 0)
				return Array.Empty<DailySummary>();

			return summaries.Select(s => new DailySummary(
				DateTime.UnixEpoch + TimeSpan.FromDays(s.DaysSinceEpoch),
				s.SummaryData.FromNative(),
				new Dictionary<ReportType, DailySummaryReport?>
				{
					[ReportType.Unknown] = s.GetReport(ReportType.Unknown),
					[ReportType.ConfirmedTest] = s.GetReport(ReportType.ConfirmedTest),
					[ReportType.ConfirmedClinicalDiagnosis] = s.GetReport(ReportType.ConfirmedClinicalDiagnosis),
					[ReportType.SelfReported] = s.GetReport(ReportType.SelfReported),
					[ReportType.Recursive] = s.GetReport(ReportType.Recursive),
					[ReportType.Revoked] = s.GetReport(ReportType.Revoked),
				}));
		}

		internal static async Task<IEnumerable<ExposureWindow>> PlatformGetExposureWindowsAsync()
		{
			if (DailySummaryHandler == null)
				throw new InvalidOperationException("The handler does not support Exposure Window Mode.");

			var windows = await GetClient().GetExposureWindowsAsync();
			if (windows == null || windows.Count == 0)
				return Array.Empty<ExposureWindow>();

			return windows.Select(w => new ExposureWindow(
				w.CalibrationConfidence.FromNativeCalibrationConfidence(),
				DateTime.UnixEpoch + TimeSpan.FromMilliseconds(w.DateMillisSinceEpoch),
				w.Infectiousness.FromNativeInfectiousness(),
				w.ReportType.FromNativeReportType(),
				w.ScanInstances.Select(s => s.FromNative())));
		}

		internal static async Task PlatformUpdateDiagnosisKeysDataMappingAsync()
		{
			if (DailySummaryHandler == null)
				throw new InvalidOperationException("The handler does not support Exposure Window Mode.");

			var nativeMapping = await GetClient().GetDiagnosisKeysDataMappingAsync();
			var config = await DailySummaryHandler.GetDailySummaryConfigurationAsync();

			// because this API has a quota, only change when we have to
			if (AreEqual(nativeMapping, config))
				return;

			var builder = new DiagnosisKeysDataMapping.DiagnosisKeysDataMappingBuilder();
			var newMap = new JavaDictionary<Java.Lang.Integer, Java.Lang.Integer>();
			foreach (var pair in config.DaysSinceOnsetInfectiousness)
			{
				newMap[new Java.Lang.Integer(pair.Key)] = new Java.Lang.Integer(pair.Value.ToNative());
			}
			builder.SetDaysSinceOnsetToInfectiousness(newMap);
			builder.SetInfectiousnessWhenDaysSinceOnsetMissing(config.DefaultInfectiousness.ToNative());
			builder.SetReportTypeWhenMissing(config.DefaultReportType.ToNative());

			await GetClient().SetDiagnosisKeysDataMappingAsync(builder.Build());

			static bool AreEqual(DiagnosisKeysDataMapping mapping, DailySummaryConfiguration config)
			{
				if (mapping.InfectiousnessWhenDaysSinceOnsetMissing != config.DefaultInfectiousness.ToNative())
					return false;
				if (mapping.ReportTypeWhenMissing != config.DefaultReportType.ToNative())
					return false;
				if (mapping.DaysSinceOnsetToInfectiousness.Count != config.DaysSinceOnsetInfectiousness?.Count)
					return false;

				// check each value in the map [-14, 14]
				for (var day = -14; day <= 14; day++)
				{
					var native = mapping.DaysSinceOnsetToInfectiousness.TryGetValue(new Java.Lang.Integer(day), out var nativeInfect)
						? ((int)nativeInfect).FromNativeInfectiousness()
						: Infectiousness.Standard;
					var managed = config.DaysSinceOnsetInfectiousness.TryGetValue(day, out var infect)
						? infect
						: Infectiousness.Standard;

					if (native != managed)
						return false;
				}

				return true;
			}
		}

		static async Task<Status> PlatformGetStatusAsync()
		{
			var bt = BluetoothAdapter.DefaultAdapter;

			if (bt == null || !bt.IsEnabled)
				return Status.BluetoothOff;

			var status = await GetClient().IsEnabledAsync();

			return status ? Status.Active : Status.Disabled;
		}
	}

	public class BackgroundFetchWorker : Worker
	{
		public BackgroundFetchWorker(global::Android.Content.Context context, WorkerParameters workerParameters)
			: base(context, workerParameters)
		{
		}

		public override Result DoWork()
		{
			try
			{
				Task.Run(() => DoAsyncWork()).GetAwaiter().GetResult();
				return Result.InvokeSuccess();
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex);
				return Result.InvokeRetry();
			}
		}

		async Task DoAsyncWork()
		{
			if (await ExposureNotification.IsEnabledAsync())
				await ExposureNotification.UpdateKeysFromServer();
		}
	}

	static partial class Utils
	{
		public static RiskLevel FromNativeRiskLevel(this int riskLevel) =>
			riskLevel switch
			{
				AndroidRiskLevel.RiskLevelLowest => RiskLevel.Lowest,
				AndroidRiskLevel.RiskLevelLow => RiskLevel.Low,
				AndroidRiskLevel.RiskLevelLowMedium => RiskLevel.MediumLow,
				AndroidRiskLevel.RiskLevelMedium => RiskLevel.Medium,
				AndroidRiskLevel.RiskLevelMediumHigh => RiskLevel.MediumHigh,
				AndroidRiskLevel.RiskLevelHigh => RiskLevel.High,
				AndroidRiskLevel.RiskLevelVeryHigh => RiskLevel.VeryHigh,
				AndroidRiskLevel.RiskLevelHighest => RiskLevel.Highest,
				_ => AndroidRiskLevel.RiskLevelInvalid,
			};

		public static int ToNative(this RiskLevel riskLevel) =>
			riskLevel switch
			{
				RiskLevel.Lowest => AndroidRiskLevel.RiskLevelLowest,
				RiskLevel.Low => AndroidRiskLevel.RiskLevelLow,
				RiskLevel.MediumLow => AndroidRiskLevel.RiskLevelLowMedium,
				RiskLevel.Medium => AndroidRiskLevel.RiskLevelMedium,
				RiskLevel.MediumHigh => AndroidRiskLevel.RiskLevelMediumHigh,
				RiskLevel.High => AndroidRiskLevel.RiskLevelHigh,
				RiskLevel.VeryHigh => AndroidRiskLevel.RiskLevelVeryHigh,
				RiskLevel.Highest => AndroidRiskLevel.RiskLevelHighest,
				_ => AndroidRiskLevel.RiskLevelInvalid,
			};

		public static int ToNative(this Infectiousness infectiousness) =>
			infectiousness switch
			{
				Infectiousness.None => AndroidInfectiousness.None,
				Infectiousness.Standard => AndroidInfectiousness.Standard,
				Infectiousness.High => AndroidInfectiousness.High,
				_ => AndroidInfectiousness.Standard,
			};

		public static Infectiousness FromNativeInfectiousness(this int infectiousness) =>
			infectiousness switch
			{
				AndroidInfectiousness.None => Infectiousness.None,
				AndroidInfectiousness.Standard => Infectiousness.Standard,
				AndroidInfectiousness.High => Infectiousness.High,
				_ => Infectiousness.Standard,
			};

		public static int ToNative(this ReportType reportType) =>
			reportType switch
			{
				ReportType.Unknown => AndroidReportType.Unknown,
				ReportType.ConfirmedTest => AndroidReportType.ConfirmedTest,
				ReportType.ConfirmedClinicalDiagnosis => AndroidReportType.ConfirmedClinicalDiagnosis,
				ReportType.SelfReported => AndroidReportType.SelfReport,
				ReportType.Recursive => AndroidReportType.Recursive,
				ReportType.Revoked => AndroidReportType.Revoked,
				_ => AndroidReportType.Unknown,
			};

		public static ReportType FromNativeReportType(this int reportType) =>
			reportType switch
			{
				AndroidReportType.Unknown => ReportType.Unknown,
				AndroidReportType.ConfirmedTest => ReportType.ConfirmedTest,
				AndroidReportType.ConfirmedClinicalDiagnosis => ReportType.ConfirmedClinicalDiagnosis,
				AndroidReportType.SelfReport => ReportType.SelfReported,
				AndroidReportType.Recursive => ReportType.Recursive,
				AndroidReportType.Revoked => ReportType.Revoked,
				_ => ReportType.Unknown,
			};

		public static DailySummaryReport FromNative(this AndroidDailySummary.ExposureSummaryData data) =>
			new DailySummaryReport(
				data.MaximumScore,
				data.ScoreSum,
				data.WeightedDurationSum);

		public static ScanInstance FromNative(this AndroidScanInstance instance) =>
			new ScanInstance(
				instance.MinAttenuationDb,
				instance.TypicalAttenuationDb,
				TimeSpan.FromSeconds(instance.SecondsSinceLastScan));

		public static DailySummaryReport? GetReport(this AndroidDailySummary summary, ReportType reportType) =>
			summary.GetSummaryDataForReportType(reportType.ToNative())?.FromNative();

		public static CalibrationConfidence FromNativeCalibrationConfidence(this int confidence) =>
			confidence switch
			{
				AndroidCalibrationConfidence.Lowest => CalibrationConfidence.Lowest,
				AndroidCalibrationConfidence.Low => CalibrationConfidence.Low,
				AndroidCalibrationConfidence.Medium => CalibrationConfidence.Medium,
				AndroidCalibrationConfidence.High => CalibrationConfidence.High,
				_ => AndroidCalibrationConfidence.Lowest,
			};
	}
}

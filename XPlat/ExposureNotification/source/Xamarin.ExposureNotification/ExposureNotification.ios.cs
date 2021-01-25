using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BackgroundTasks;
using ExposureNotifications;
using Foundation;
using UIKit;
using Xamarin.Essentials;

namespace Xamarin.ExposureNotifications
{
	public static partial class ExposureNotification
	{
		// This is a special ID suffix which iOS treats a certain way
		// we can basically request infinite background tasks
		// and iOS will throttle it sensibly for us.
		static readonly string backgroundTaskId = AppInfo.PackageName + ".exposure-notification";

		static readonly Lazy<bool> isDailySummariesSupported = new Lazy<bool>(() =>
		{
			if (UIDevice.CurrentDevice.CheckSystemVersion(13, 7))
				return true;
			if (UIDevice.CurrentDevice.CheckSystemVersion(13, 5))
				return false;
			if (ObjCRuntime.Class.GetHandle("ENManager") != null)
				return true;
			return false;
		});

		static readonly Lazy<bool> isDailySummaries = new Lazy<bool>(() =>
		{
			if (!isDailySummariesSupported.Value)
				return false;

			if (!NSBundle.MainBundle.InfoDictionary.TryGetValue(new NSString("ENAPIVersion"), out var version))
				return false;

			if (!(version is NSNumber number) || (number.Int32Value != 1 && number.Int32Value != 2))
				throw new InvalidOperationException("Value of ENAPIVersion was not a valid version number (1 or 2).");

			if (number.Int32Value == 2)
			{
				// Unlike Android where this is optional, iOS performs entirely differently depending on the version
				if (DailySummaryHandler == null)
					throw new NotImplementedException($"Missing an implementation for {nameof(IExposureNotificationDailySummaryHandler)}");

				return true;
			}

			return false;
		});

		static readonly Lazy<ENManager?> instance = new Lazy<ENManager?>(() =>
		{
			if (ObjCRuntime.Class.GetHandle("ENManager") != null)
				return new ENManager();
			return null;
		});

		static Task? activateTask;

		// get a valid instance that may not be ready
		static ENManager? Instance => instance.Value;

		// get the activated instance
		static async Task<ENManager> GetManagerAsync()
		{
			EnsureSupported();

			var manager = Instance!;

			activateTask ??= manager.ActivateAsync();
			await activateTask;

			return manager;
		}

		public static bool IsDailySummaries => isDailySummaries.Value;

		static Task<ENExposureConfiguration> GetConfigurationAsync()
		{
			return IsDailySummaries
				? GetDailyConfigAsync(DailySummaryHandler!)
				: GetConfigAsync(Handler);

			static async Task<ENExposureConfiguration> GetConfigAsync(IExposureNotificationHandler handler)
			{
				var c = await handler.GetConfigurationAsync();
				if (c == null)
					throw new InvalidOperationException("The configuration was not provided on the handler.");

				var nc = new ENExposureConfiguration
				{
					AttenuationLevelValues = c.AttenuationScores,
					DurationLevelValues = c.DurationScores,
					DaysSinceLastExposureLevelValues = c.DaysSinceLastExposureScores,
					TransmissionRiskLevelValues = c.TransmissionRiskScores,
					AttenuationWeight = c.AttenuationWeight,
					DaysSinceLastExposureWeight = c.DaysSinceLastExposureWeight,
					DurationWeight = c.DurationWeight,
					TransmissionRiskWeight = c.TransmissionWeight,
					MinimumRiskScore = (byte)c.MinimumRiskScore,
				};

				var metadata = new NSMutableDictionary();
				metadata.SetValueForKey(new NSNumber(c.MinimumRiskScore), new NSString("minimumRiskScoreFullRange"));

				if (c.DurationAtAttenuationThresholds != null)
				{
					if (c.DurationAtAttenuationThresholds.Length < 2)
						throw new ArgumentOutOfRangeException(nameof(c.DurationAtAttenuationThresholds), "Must be an array of length 2");

					var attKey = new NSString("attenuationDurationThresholds");
					var attValue = NSArray.FromObjects(2, c.DurationAtAttenuationThresholds[0], c.DurationAtAttenuationThresholds[1]);
					metadata.SetValueForKey(attValue, attKey);
				}

				nc.Metadata = metadata;

				return nc;
			}

			static async Task<ENExposureConfiguration> GetDailyConfigAsync(IExposureNotificationDailySummaryHandler handler)
			{
				var c = await handler.GetDailySummaryConfigurationAsync();
				if (c == null)
					throw new InvalidOperationException("The daily summary configuration was not provided on the handler.");

				var infectiousnessMap = new NSMutableDictionary<NSNumber, NSNumber>();
				foreach (var pair in c.DaysSinceOnsetInfectiousness)
				{
					infectiousnessMap[pair.Key] = (int)pair.Value.ToNative();
				}

				var nc = new ENExposureConfiguration
				{
					ImmediateDurationWeight = c.AttenuationWeights[DistanceEstimate.Immediate],
					NearDurationWeight = c.AttenuationWeights[DistanceEstimate.Near],
					MediumDurationWeight = c.AttenuationWeights[DistanceEstimate.Medium],
					OtherDurationWeight = c.AttenuationWeights[DistanceEstimate.Other],

					InfectiousnessForDaysSinceOnsetOfSymptoms = infectiousnessMap.ToNSDictionary(),

					InfectiousnessStandardWeight = c.InfectiousnessWeights[Infectiousness.Standard],
					InfectiousnessHighWeight = c.InfectiousnessWeights[Infectiousness.High],

					ReportTypeConfirmedTestWeight = c.ReportTypeWeights[ReportType.ConfirmedTest],
					ReportTypeConfirmedClinicalDiagnosisWeight = c.ReportTypeWeights[ReportType.ConfirmedClinicalDiagnosis],
					ReportTypeSelfReportedWeight = c.ReportTypeWeights[ReportType.SelfReported],
					ReportTypeRecursiveWeight = c.ReportTypeWeights[ReportType.Recursive],

					ReportTypeNoneMap = c.DefaultReportType.ToNative(),

					AttenuationDurationThresholds = c.AttenuationThresholds,

					DaysSinceLastExposureThreshold = c.DaysSinceLastExposureThreshold,
				};

				return nc;
			}
		}

		static async void PlatformInit()
		{
			await ScheduleFetchAsync();
		}

		static async Task PlatformStart()
		{
			var m = await GetManagerAsync();
			await m.SetExposureNotificationEnabledAsync(true);
		}

		static async Task PlatformStop()
		{
			var m = await GetManagerAsync();
			await m.SetExposureNotificationEnabledAsync(false);
		}

		static async Task<bool> PlatformIsEnabled()
		{
			var m = await GetManagerAsync();
			return m.ExposureNotificationEnabled;
		}

		static Task PlatformScheduleFetch()
		{
			if (!IsSupported)
				return Task.CompletedTask;

			// BGTaskScheduler is only available from iOS 13.0
			if (DeviceInfo.Version < new Version(13, 0))
				CreateLaunchActivityHandler();
			else
				CreateBackgroundTask();

			return Task.CompletedTask;
		}

		static void CreateLaunchActivityHandler()
		{
			var isUpdating = false;
			Instance?.SetLaunchActivityHandler(activityFlags =>
			{
				if (!activityFlags.HasFlag(ENActivityFlags.PeriodicRun))
					return;

				// Disallow concurrent exposure detection.
				if (isUpdating)
					return;
				isUpdating = true;

				// Run the actual task on a background thread
				Task.Run(async () =>
				{
					try
					{
						await UpdateKeysFromServer();
					}
					catch (OperationCanceledException)
					{
						Console.WriteLine($"[Xamarin.ExposureNotifications] Launch activity handler took too long to complete.");
					}
					catch (Exception ex)
					{
						Console.WriteLine($"[Xamarin.ExposureNotifications] There was an error running the launch activity handler: {ex}");
					}

					isUpdating = false;
				});
			});
		}

		static void CreateBackgroundTask()
		{
			var isUpdating = false;
			BGTaskScheduler.Shared.Register(backgroundTaskId, null, task =>
			{
				// Disallow concurrent exposure detection, because if allowed we might try to detect the same diagnosis keys more than once
				if (isUpdating)
				{
					task.SetTaskCompleted(false);
					return;
				}
				isUpdating = true;

				var cancelSrc = new CancellationTokenSource();
				task.ExpirationHandler = cancelSrc.Cancel;

				// Run the actual task on a background thread
				Task.Run(async () =>
				{
					try
					{
						await UpdateKeysFromServer(cancelSrc.Token);
						task.SetTaskCompleted(true);
					}
					catch (OperationCanceledException)
					{
						Console.WriteLine($"[Xamarin.ExposureNotifications] Background task took too long to complete.");
					}
					catch (Exception ex)
					{
						Console.WriteLine($"[Xamarin.ExposureNotifications] There was an error running the background task: {ex}");
						task.SetTaskCompleted(false);
					}

					isUpdating = false;
				});

				scheduleBgTask();
			});

			scheduleBgTask();

			static void scheduleBgTask()
			{
				if (ENManager.AuthorizationStatus != ENAuthorizationStatus.Authorized)
					return;

				var newBgTask = new BGProcessingTaskRequest(backgroundTaskId);
				newBgTask.RequiresNetworkConnectivity = true;
				try
				{
					BGTaskScheduler.Shared.Submit(newBgTask, out var error);

					if (error != null)
						throw new NSErrorException(error);
				}
				catch (Exception ex)
				{
					Console.WriteLine($"[Xamarin.ExposureNotifications] There was an error submitting the background task: {ex}");
				}
			}
		}

		// Tells the local API when new diagnosis keys have been obtained from the server
		static async Task PlatformDetectExposuresAsync(IEnumerable<string> keyFiles, CancellationToken cancellationToken)
		{
			// Submit to the API
			var c = await GetConfigurationAsync();
			var m = await GetManagerAsync();

			// Extract all the files from the zips
			var allFiles = new List<string>();
			foreach (var file in keyFiles)
			{
				using var stream = File.OpenRead(file);
				using var archive = new ZipArchive(stream, ZipArchiveMode.Read);

				// .bin
				var binTmp = Path.Combine(FileSystem.CacheDirectory, Guid.NewGuid().ToString() + ".bin");
				using (var binWrite = File.Create(binTmp))
				{
					var bin = archive.GetEntry("export.bin");
					using var binRead = bin.Open();
					await binRead.CopyToAsync(binWrite);
				}
				allFiles.Add(binTmp);

				// .sig
				var sigTmp = Path.ChangeExtension(binTmp, ".sig");
				using (var sigWrite = File.Create(sigTmp))
				{
					var sig = archive.GetEntry("export.sig");
					using var sigRead = sig.Open();
					await sigRead.CopyToAsync(sigWrite);
				}
				allFiles.Add(sigTmp);
			}

			// Start the detection
			var detectionSummaryTask = m.DetectExposuresAsync(
				c,
				allFiles.Select(k => new NSUrl(k, false)).ToArray(),
				out var detectProgress);
			cancellationToken.Register(detectProgress.Cancel);
			var detectionSummary = await detectionSummaryTask;

			// Delete all the extracted files
			foreach (var file in allFiles)
			{
				try
				{
					File.Delete(file);
				}
				catch
				{
					// no-op
				}
			}

			// Branch the logic to either return the new data, or fall back to the old info
			if (IsDailySummaries)
			{
				// Check that the summary has actual data before notifying the callback
				if (detectionSummary.DaySummaries.Length > 0)
				{
					var (windows, dailySummary) = await PlatformProcessSummaryV2Async(m, detectionSummary, cancellationToken);

					await DailySummaryHandler!.ExposureStateUpdatedAsync(windows, dailySummary);
				}
			}
			else
			{
				var (summary, info) = PlatformProcessSummaryV1(m, detectionSummary, cancellationToken);

				// Check that the summary has any matches before notifying the callback
				if (summary?.MatchedKeyCount > 0)
					await Handler.ExposureDetectedAsync(summary, info);
			}
		}

		static async Task<(IEnumerable<ExposureWindow>, IEnumerable<DailySummary>)> PlatformProcessSummaryV2Async(ENManager m, ENExposureDetectionSummary detectionSummary, CancellationToken cancellationToken)
		{
			var exposureWindows = await m.GetExposureWindowsAsync(detectionSummary);
			var windows = exposureWindows.Select(w => new ExposureWindow(
				w.CalibrationConfidence.FromNative(),
				(DateTime)w.Date,
				w.Infectiousness.FromNative(),
				w.DiagnosisReportType.FromNative(),
				w.ScanInstances.Select(
					s => new ScanInstance(
						s.MinimumAttenuation,
						s.TypicalAttenuation,
						TimeSpan.FromSeconds(s.SecondsSinceLastScan))).ToArray()));

			var summaries = detectionSummary.DaySummaries.Select(s => new DailySummary(
				(DateTime)s.Date,
				s.DaySummary.FromNative()!,
				new Dictionary<ReportType, DailySummaryReport?>
				{
					[ReportType.Unknown] = null,
					[ReportType.ConfirmedTest] = s.ConfirmedTestSummary?.FromNative(),
					[ReportType.ConfirmedClinicalDiagnosis] = s.ConfirmedClinicalDiagnosisSummary?.FromNative(),
					[ReportType.SelfReported] = s.SelfReportedSummary?.FromNative(),
					[ReportType.Recursive] = s.RecursiveSummary?.FromNative(),
					[ReportType.Revoked] = null,
				}));

			return (windows, summaries);
		}

		static (ExposureDetectionSummary, Func<Task<IEnumerable<ExposureInfo>>>) PlatformProcessSummaryV1(ENManager m, ENExposureDetectionSummary detectionSummary, CancellationToken cancellationToken)
		{
			var attDurTs = new List<TimeSpan>();
			var dictKey = new NSString("attenuationDurations");
			if (detectionSummary.Metadata?.ContainsKey(dictKey) == true)
			{
				if (detectionSummary.Metadata.ObjectForKey(dictKey) is NSArray attDur)
				{
					for (nuint i = 0; i < attDur.Count; i++)
						attDurTs.Add(TimeSpan.FromSeconds(attDur.GetItem<NSNumber>(i).Int32Value));
				}
			}

			var sumRisk = 0;
			dictKey = new NSString("riskScoreSumFullRange");
			if (detectionSummary.Metadata?.ContainsKey(dictKey) == true)
			{
				var sro = detectionSummary.Metadata.ObjectForKey(dictKey);
				if (sro is NSNumber sron)
					sumRisk = sron.Int32Value;
			}

			var maxRisk = 0;
			dictKey = new NSString("maximumRiskScoreFullRange");
			if (detectionSummary.Metadata?.ContainsKey(dictKey) == true)
			{
				var sro = detectionSummary.Metadata.ObjectForKey(dictKey);
				if (sro is NSNumber sron)
					maxRisk = sron.Int32Value;
			}
			else
			{
				maxRisk = detectionSummary.MaximumRiskScore;
			}

			var summary = new ExposureDetectionSummary(
				(int)detectionSummary.DaysSinceLastExposure,
				detectionSummary.MatchedKeyCount,
				maxRisk,
				attDurTs.ToArray(),
				sumRisk);

			async Task<IEnumerable<ExposureInfo>> GetInfo()
			{
				// Get the info
				IEnumerable<ExposureInfo> info = Array.Empty<ExposureInfo>();
				if (summary?.MatchedKeyCount > 0)
				{
					var exposures = await m.GetExposureInfoAsync(detectionSummary, Handler.UserExplanation, out var exposuresProgress);
					cancellationToken.Register(exposuresProgress.Cancel);
					info = exposures.Select(i =>
					{
						var totalRisk = 0;
						var dictKey = new NSString("totalRiskScoreFullRange");
						if (i.Metadata?.ContainsKey(dictKey) == true)
						{
							var sro = i.Metadata.ObjectForKey(dictKey);
							if (sro is NSNumber sron)
								totalRisk = sron.Int32Value;
						}
						else
						{
							totalRisk = i.TotalRiskScore;
						}

						return new ExposureInfo(
							(DateTime)i.Date,
							TimeSpan.FromSeconds(i.Duration),
							i.AttenuationValue,
							totalRisk,
							i.TransmissionRiskLevel.FromNative());
					});
				}
				return info;
			}

			// Return everything
			return (summary, GetInfo);
		}

		static async Task<IEnumerable<TemporaryExposureKey>> PlatformGetTemporaryExposureKeys()
		{
			var m = await GetManagerAsync();
			var selfKeys = await m.GetDiagnosisKeysAsync();

			return selfKeys.Select(k => new TemporaryExposureKey(
				k.KeyData.ToArray(),
				k.RollingStartNumber,
				TimeSpan.FromMinutes(k.RollingPeriod * 10),
				k.TransmissionRiskLevel.FromNative()));
		}

		static async Task<Status> PlatformGetStatusAsync()
		{
			var m = await GetManagerAsync();

			return m.ExposureNotificationStatus switch
			{
				ENStatus.Active => Status.Active,
				ENStatus.BluetoothOff => Status.BluetoothOff,
				ENStatus.Disabled => Status.Disabled,
				ENStatus.Restricted => Status.Restricted,
				_ => Status.Unknown,
			};
		}
	}

	static partial class Utils
	{
		public static RiskLevel FromNative(this byte riskLevel) =>
			riskLevel switch
			{
				1 => RiskLevel.Lowest,
				2 => RiskLevel.Low,
				3 => RiskLevel.MediumLow,
				4 => RiskLevel.Medium,
				5 => RiskLevel.MediumHigh,
				6 => RiskLevel.High,
				7 => RiskLevel.VeryHigh,
				8 => RiskLevel.Highest,
				_ => RiskLevel.Invalid,
			};

		public static byte ToNative(this RiskLevel riskLevel) =>
			riskLevel switch
			{
				RiskLevel.Lowest => 1,
				RiskLevel.Low => 2,
				RiskLevel.MediumLow => 3,
				RiskLevel.Medium => 4,
				RiskLevel.MediumHigh => 5,
				RiskLevel.High => 6,
				RiskLevel.VeryHigh => 7,
				RiskLevel.Highest => 8,
				_ => 0,
			};

		public static Infectiousness FromNative(this ENInfectiousness infectiousness) =>
			infectiousness switch
			{
				ENInfectiousness.None => Infectiousness.None,
				ENInfectiousness.Standard => Infectiousness.Standard,
				ENInfectiousness.High => Infectiousness.High,
				_ => Infectiousness.Standard,
			};

		public static ENInfectiousness ToNative(this Infectiousness infectiousness) =>
			infectiousness switch
			{
				Infectiousness.None => ENInfectiousness.None,
				Infectiousness.Standard => ENInfectiousness.Standard,
				Infectiousness.High => ENInfectiousness.High,
				_ => ENInfectiousness.Standard,
			};

		public static ReportType FromNative(this ENDiagnosisReportType reportType) =>
			reportType switch
			{
				ENDiagnosisReportType.Unknown => ReportType.Unknown,
				ENDiagnosisReportType.ConfirmedTest => ReportType.ConfirmedTest,
				ENDiagnosisReportType.ConfirmedClinicalDiagnosis => ReportType.ConfirmedClinicalDiagnosis,
				ENDiagnosisReportType.SelfReported => ReportType.SelfReported,
				ENDiagnosisReportType.Recursive => ReportType.Recursive,
				ENDiagnosisReportType.Revoked => ReportType.Revoked,
				_ => ReportType.Unknown,
			};

		public static ENDiagnosisReportType ToNative(this ReportType reportType) =>
			reportType switch
			{
				ReportType.Unknown => ENDiagnosisReportType.Unknown,
				ReportType.ConfirmedTest => ENDiagnosisReportType.ConfirmedTest,
				ReportType.ConfirmedClinicalDiagnosis => ENDiagnosisReportType.ConfirmedClinicalDiagnosis,
				ReportType.SelfReported => ENDiagnosisReportType.SelfReported,
				ReportType.Recursive => ENDiagnosisReportType.Recursive,
				ReportType.Revoked => ENDiagnosisReportType.Revoked,
				_ => ENDiagnosisReportType.Unknown,
			};

		public static CalibrationConfidence FromNative(this ENCalibrationConfidence confidence) =>
			confidence switch
			{
				ENCalibrationConfidence.Lowest => CalibrationConfidence.Lowest,
				ENCalibrationConfidence.Low => CalibrationConfidence.Low,
				ENCalibrationConfidence.Medium => CalibrationConfidence.Medium,
				ENCalibrationConfidence.High => CalibrationConfidence.High,
				_ => CalibrationConfidence.Lowest,
			};

		public static DailySummaryReport? FromNative(this ENExposureSummaryItem summary) =>
			summary == null
				? null
				: new DailySummaryReport(summary.MaximumScore, summary.ScoreSum, summary.WeightedDurationSum);

		public static NSDictionary<NSNumber, NSNumber> ToNSDictionary(this NSMutableDictionary<NSNumber, NSNumber> mutable) =>
			NSDictionary<NSNumber, NSNumber>.FromObjectsAndKeys(mutable.Values, mutable.Keys, (nint)mutable.Count);
	}
}

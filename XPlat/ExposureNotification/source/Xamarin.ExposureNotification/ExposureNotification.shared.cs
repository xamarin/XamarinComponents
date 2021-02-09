using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Xamarin.ExposureNotifications
{
	public static partial class ExposureNotification
	{
		static INativeImplementation nativeImplementation;

		public static void OverrideNativeImplementation(INativeImplementation nativeImplementation)
			=> ExposureNotification.nativeImplementation = nativeImplementation;

		static IExposureNotificationHandler handler;

		public static bool OverridesNativeImplementation
			=> ExposureNotification.nativeImplementation != null;

		public static bool IsSupported => Instance != null;

		internal static void EnsureSupported()
		{
			if (Instance == null)
				throw new PlatformNotSupportedException("Exposure notifications are not supported on this device.");
		}

		internal static IExposureNotificationHandler Handler
		{
			get
			{
				if (handler != default)
					return handler;

				// Look up implementations of IExposureNotificationHandler
				var allAssemblies = AppDomain.CurrentDomain.GetAssemblies();
				foreach (var asm in allAssemblies)
				{
					if (asm.IsDynamic)
						continue;

					var asmName = asm.GetName().Name;

					if (asmName.StartsWith("mscorlib")
						|| asmName.StartsWith("System.", StringComparison.OrdinalIgnoreCase)
						|| asmName.StartsWith("Xamarin.", StringComparison.OrdinalIgnoreCase))
						continue;

					var allTypes = asm.GetExportedTypes();

					foreach (var t in allTypes)
					{
						if (t.IsClass && typeof(IExposureNotificationHandler).IsAssignableFrom(t))
						{
							handler = (IExposureNotificationHandler)Activator.CreateInstance(t);
						}
					}
				}

				if (handler == default)
					throw new NotImplementedException($"Missing an implementation for {nameof(IExposureNotificationHandler)}");

				return handler;
			}
		}

		public static void Init()
			=> PlatformInit();

		public static Task ScheduleFetchAsync()
			=> PlatformScheduleFetch();

		public static Task StartAsync()
			=> nativeImplementation != null ? nativeImplementation.StartAsync() : PlatformStart();

		public static Task StopAsync()
			=> nativeImplementation != null ? nativeImplementation.StopAsync() : PlatformStop();

		public static Task<bool> IsEnabledAsync()
			=> nativeImplementation != null ? nativeImplementation.IsEnabledAsync() : PlatformIsEnabled();

		// Call this when the user has confirmed diagnosis
		public static async Task SubmitSelfDiagnosisAsync()
		{
			var selfKeys = await GetSelfTemporaryExposureKeysAsync();

			await Handler.UploadSelfExposureKeysToServerAsync(selfKeys);
		}

		// Call this when the app needs to update the local keys
		public static async Task<bool> UpdateKeysFromServer(CancellationToken cancellationToken = default)
		{
			var processedAnyFiles = false;

			await Handler?.FetchExposureKeyBatchFilesFromServerAsync(async downloadedFiles =>
			{
				cancellationToken.ThrowIfCancellationRequested();

				if (!downloadedFiles.Any())
					return;

				if (nativeImplementation != null)
				{
					var r = await nativeImplementation.DetectExposuresAsync(downloadedFiles);

					var hasMatches = (r.summary?.MatchedKeyCount ?? 0) > 0;

					if (hasMatches)
						await Handler.ExposureDetectedAsync(r.summary, r.getInfo);
				}
				else
				{
#if __IOS__
					// On iOS we need to check this ourselves and invoke the handler
					var (summary, info) = await PlatformDetectExposuresAsync(downloadedFiles, cancellationToken);

					// Check that the summary has any matches before notifying the callback
					if (summary?.MatchedKeyCount > 0)
						await Handler.ExposureDetectedAsync(summary, info);
#elif __ANDROID__
					// on Android this will happen in the broadcast receiver
					await PlatformDetectExposuresAsync(downloadedFiles, cancellationToken);
#endif
				}

				processedAnyFiles = true;
			}, cancellationToken);

			return processedAnyFiles;
		}

		internal static Task<IEnumerable<TemporaryExposureKey>> GetSelfTemporaryExposureKeysAsync()
			=> nativeImplementation != null ? nativeImplementation.GetSelfTemporaryExposureKeysAsync() : PlatformGetTemporaryExposureKeys();

		public static Task<Status> GetStatusAsync()
			=> nativeImplementation != null ? nativeImplementation.GetStatusAsync() : PlatformGetStatusAsync();
	}

	public enum Status
	{
		Unknown,
		Disabled,
		Active,
		BluetoothOff,
		Restricted,
		NotAuthorized
	}

	public class Configuration
	{
		// Minimum risk score required to record
		public int MinimumRiskScore { get; set; } = 4;

		public int AttenuationWeight { get; set; } = 50;

		public int TransmissionWeight { get; set; } = 50;

		public int DurationWeight { get; set; } = 50;

		public int DaysSinceLastExposureWeight { get; set; } = 50;

		public int[] TransmissionRiskScores { get; set; } = new int[] { 4, 4, 4, 4, 4, 4, 4, 4 };

		// Scores assigned to the attenuation of the BTLE signal of exposures
		// A > 73dBm, 73 <= A > 63, 63 <= A > 51, 51 <= A > 33, 33 <= A > 27, 27 <= A > 15, 15 <= A > 10, A <= 10
		public int[] AttenuationScores { get; set; } = new[] { 4, 4, 4, 4, 4, 4, 4, 4 };

		// Scores assigned to each length of exposure
		// < 5min, 5min, 10min, 15min, 20min, 25min, 30min, > 30min
		public int[] DurationScores { get; set; } = new[] { 4, 4, 4, 4, 4, 4, 4, 4 };

		// Scores assigned to each range of days of exposure
		// >= 14days, 13-12, 11-10, 9-8, 7-6, 5-4, 3-2, 1-0
		public int[] DaysSinceLastExposureScores { get; set; } = new[] { 4, 4, 4, 4, 4, 4, 4, 4 };

		public int[] DurationAtAttenuationThresholds { get; set; } = new[] { 50, 70 };
	}
}

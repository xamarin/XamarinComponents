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
using Xamarin.Essentials;

namespace Xamarin.ExposureNotifications
{
	public static partial class ExposureNotification
	{
		// This is a special ID suffix which iOS treats a certain way
		// we can basically request infinite background tasks
		// and iOS will throttle it sensibly for us.
		static readonly string backgroundTaskId = AppInfo.PackageName + ".exposure-notification";

		static ENManager instance;
		static Task activateTask;

		// get a valid instance that may not be ready
		static ENManager Instance
		{
			get
			{
				if (instance != null)
					return instance;

				if (ObjCRuntime.Class.GetHandle("ENManager") != null)
					return instance ??= new ENManager();

				return null;
			}
		}

		// get the activated instance
		static async Task<ENManager> GetManagerAsync()
		{
			EnsureSupported();

			var manager = Instance;

            activateTask ??= manager.ActivateAsync();
            await activateTask;

			return manager;
		}

		static async Task<ENExposureConfiguration> GetConfigurationAsync()
		{
			var c = await Handler.GetConfigurationAsync();

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
			Instance.SetLaunchActivityHandler(activityFlags =>
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
		static async Task<(ExposureDetectionSummary, Func<Task<IEnumerable<ExposureInfo>>>)> PlatformDetectExposuresAsync(IEnumerable<string> keyFiles, CancellationToken cancellationToken)
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

			var attDurTs = new List<TimeSpan>();
			var dictKey = new NSString("attenuationDurations");
			if (detectionSummary.Metadata.ContainsKey(dictKey))
			{
				var attDur = detectionSummary.Metadata.ObjectForKey(dictKey) as NSArray;

				for (nuint i = 0; i < attDur.Count; i++)
					attDurTs.Add(TimeSpan.FromSeconds(attDur.GetItem<NSNumber>(i).Int32Value));
			}

			var sumRisk = 0;
			dictKey = new NSString("riskScoreSumFullRange");
			if (detectionSummary.Metadata.ContainsKey(dictKey))
			{
				var sro = detectionSummary.Metadata.ObjectForKey(dictKey);
				if (sro is NSNumber sron)
					sumRisk = sron.Int32Value;
			}

			var maxRisk = 0;
			dictKey = new NSString("maximumRiskScoreFullRange");
			if (detectionSummary.Metadata.ContainsKey(dictKey))
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
						if (i.Metadata.ContainsKey(dictKey))
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
	}
}

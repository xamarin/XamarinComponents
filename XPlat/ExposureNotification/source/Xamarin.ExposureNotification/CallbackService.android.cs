using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Gms.Common.Apis;
using Android.Gms.Nearby.ExposureNotification;
using Android.Runtime;
using AndroidX.Core.App;

namespace Xamarin.ExposureNotifications
{
	[BroadcastReceiver(Permission = permissionExposureCallback, Exported = true)]
	[IntentFilter(new[] { actionExposureStateUpdated, actionExposureNotFound })]
	[Preserve]
	class ExposureNotificationCallbackBroadcastReceiver : BroadcastReceiver
	{
		const string actionExposureStateUpdated = ExposureNotificationClient.ActionExposureStateUpdated;
		const string actionExposureNotFound = ExposureNotificationClient.ActionExposureNotFound;
		const string permissionExposureCallback = "com.google.android.gms.nearby.exposurenotification.EXPOSURE_CALLBACK";

		public override void OnReceive(Context? context, Intent? intent)
		{
			// https://developers.google.com/android/exposure-notifications/exposure-notifications-api#broadcast-receivers
			var action = intent?.Action;
			if (action == actionExposureStateUpdated)
			{
				global::Android.Util.Log.Debug("Xamarin.ExposureNotifications", "Exposure state updated.");

				ExposureNotificationCallbackService.EnqueueWork(context, intent);
			}
			else if (action == actionExposureNotFound)
			{
				global::Android.Util.Log.Debug("Xamarin.ExposureNotifications", "Exposure not found.");
			}
		}
	}

	[Service(Permission = "android.permission.BIND_JOB_SERVICE")]
	[Preserve]
	class ExposureNotificationCallbackService : JobIntentService
	{
		const int jobId = 0x02;

		public static void EnqueueWork(Context? context, Intent? work)
			=> EnqueueWork(context, Java.Lang.Class.FromType(typeof(ExposureNotificationCallbackService)), jobId, work);

		protected override async void OnHandleWork(Intent workIntent)
		{
			// Not really "obsolete" as this is just Google's recommendation
			// and v1 might still be the only thing on the device.
#pragma warning disable CS0618

			var token = workIntent.GetStringExtra(ExposureNotificationClient.ExtraToken);
			if (token == null)
				return;

			// we used windows mode and implemented the windows mode handler
			if (token == ExposureNotificationClient.TokenA && ExposureNotification.DailySummaryHandler != null)
			{
				// try see if we have v1.5 APIs
				IEnumerable<ExposureWindow>? windows = null;
				try
				{
					windows = await ExposureNotification.PlatformGetExposureWindowsAsync();
				}
				catch (ApiException ex) when (ex.StatusCode == CommonStatusCodes.ApiNotConnected)
				{
					// To determine if a device received the v1.5 update, you can catch the API_NOT_CONNECTED
					// exception when accessing fields or functions that apply to v1.5 and higher.
				}

				// we support v1.5+
				if (windows != null)
				{
					// try and see if we have v1.6 APIs
					IEnumerable<DailySummary>? dailySummary = null;
					try
					{
						dailySummary = await ExposureNotification.PlatformGetDailySummariesAsync();
					}
					catch (ApiException ex) when (ex.StatusCode == CommonStatusCodes.ApiNotConnected)
					{
						// To determine if a device received the v1.6 update, you can catch the API_NOT_CONNECTED
						// exception when accessing fields or functions that apply to v1.5 and higher.
					}

					await ExposureNotification.DailySummaryHandler.ExposureStateUpdatedAsync(windows, dailySummary);
					return;
				}
			}

			// fall back to v1
			var summary = await ExposureNotification.PlatformGetExposureSummaryAsync(token);

			// Invoke the custom implementation handler code with the summary info
			if (summary?.MatchedKeyCount > 0)
			{
				await ExposureNotification.Handler.ExposureDetectedAsync(
					summary,
					() => ExposureNotification.PlatformGetExposureInformationAsync(token));
			}

#pragma warning restore CS0618
		}
	}
}

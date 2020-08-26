using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
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

		public override void OnReceive(Context context, Intent intent)
		{
			// https://developers.google.com/android/exposure-notifications/exposure-notifications-api#broadcast-receivers
			var action = intent.Action;
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

		public static void EnqueueWork(Context context, Intent work)
			=> EnqueueWork(context, Java.Lang.Class.FromType(typeof(ExposureNotificationCallbackService)), jobId, work);

		protected override async void OnHandleWork(Intent workIntent)
		{
			var token = workIntent.GetStringExtra(ExposureNotificationClient.ExtraToken);

			var summary = await ExposureNotification.PlatformGetExposureSummaryAsync(token);

			Task<IEnumerable<ExposureInfo>> GetInfo()
			{
				return ExposureNotification.PlatformGetExposureInformationAsync(token);
			}

			// Invoke the custom implementation handler code with the summary info
			if (summary?.MatchedKeyCount > 0)
			{
				await ExposureNotification.Handler.ExposureDetectedAsync(summary, GetInfo);
			}
		}
	}
}

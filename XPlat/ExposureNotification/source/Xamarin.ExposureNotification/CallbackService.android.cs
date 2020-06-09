using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Gms.Nearby.ExposureNotification;
using Android.Runtime;
using AndroidX.Core.App;

namespace Xamarin.ExposureNotifications
{
	[BroadcastReceiver(
		Permission = "com.google.android.gms.nearby.exposurenotification.EXPOSURE_CALLBACK",
		Exported = true)]
	[IntentFilter(new[] { ExposureNotificationClient.ActionExposureStateUpdated })]
	[Preserve]
	class ExposureNotificationCallbackBroadcastReceiver : BroadcastReceiver
	{
		public override void OnReceive(Context context, Intent intent)
			=> ExposureNotificationCallbackService.EnqueueWork(context, intent);
	}

	[Service(
		Permission = "android.permission.BIND_JOB_SERVICE")]
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

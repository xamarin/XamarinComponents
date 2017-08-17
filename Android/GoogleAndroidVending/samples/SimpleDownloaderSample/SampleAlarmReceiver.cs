using Android.Content;

using Google.Android.Vending.Expansion.Downloader;

namespace SimpleDownloaderSample
{
	[BroadcastReceiver(Exported = false)]
	public class SampleAlarmReceiver : BroadcastReceiver
	{
		public override void OnReceive(Context context, Intent intent)
		{
			DownloaderClientMarshaller.StartDownloadServiceIfRequired(context, intent, typeof(SampleDownloaderService));
		}
	}
}

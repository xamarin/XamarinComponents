using Android.Content;
using Android.Content.PM;

using Google.Android.Vending.Expansion.Downloader;

namespace DownloaderSample
{
	/// <summary>
	/// The alarm receiver for this sample.
	/// </summary>
	[BroadcastReceiver(Exported = false)]
	public class SampleAlarmReceiver : BroadcastReceiver
	{
		/// <summary>
		/// This method is called when the BroadcastReceiver is receiving an Intent
		/// broadcast.
		/// </summary>
		/// <param name="context">
		/// The Context in which the receiver is running.
		/// </param>
		/// <param name="intent">
		/// The Intent being received.
		/// </param>
		public override void OnReceive(Context context, Intent intent)
		{
			try
			{
				DownloaderClientMarshaller.StartDownloadServiceIfRequired(
					context, intent, typeof(SampleDownloaderService));
			}
			catch (PackageManager.NameNotFoundException e)
			{
				e.PrintStackTrace();
			}
		}
	}
}

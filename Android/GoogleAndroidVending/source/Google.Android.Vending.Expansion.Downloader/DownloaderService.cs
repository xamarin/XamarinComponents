using System;
using Android.App;
using Android.Content;

namespace Google.Android.Vending.Expansion.Downloader
{
	partial class DownloaderService
	{
		public static DownloaderServiceRequirement StartDownloadServiceIfRequired(Context context, PendingIntent pendingIntent, Type serviceType)
		{
			return StartDownloadServiceIfRequired(context, pendingIntent, Java.Lang.Class.FromType(serviceType));
		}

		public static DownloaderServiceRequirement StartDownloadServiceIfRequired(Context context, Intent intent, Type serviceType)
		{
			return StartDownloadServiceIfRequired(context, intent, Java.Lang.Class.FromType(serviceType));
		}
	}
}

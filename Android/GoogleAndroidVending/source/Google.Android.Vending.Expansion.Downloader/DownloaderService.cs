using System;
using Android.App;
using Android.Content;

namespace Google.Android.Vending.Expansion.Downloader
{
	partial class DownloaderService
	{
		public static DownloadServiceRequirement StartDownloadServiceIfRequired(Context context, PendingIntent pendingIntent, Type serviceType)
		{
			return StartDownloadServiceIfRequired(context, pendingIntent, Java.Lang.Class.FromType(serviceType));
		}
		public static DownloadServiceRequirement StartDownloadServiceIfRequired(Context context, Intent intent, Type serviceType)
		{
			return StartDownloadServiceIfRequired(context, intent, Java.Lang.Class.FromType(serviceType));
		}
	}
}

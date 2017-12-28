using System;
using Android.App;
using Android.Content;

namespace Google.Android.Vending.Expansion.Downloader
{
	partial class DownloaderClientMarshaller
	{
		public static IDownloaderServiceConnection CreateStub(IDownloaderClient itf, Type downloaderService)
		{
			return CreateStub(itf, Java.Lang.Class.FromType(downloaderService));
		}

		public static DownloaderServiceRequirement StartDownloadServiceIfRequired(Context context, PendingIntent notificationClient, Type serviceType)
		{
			return StartDownloadServiceIfRequired(context, notificationClient, Java.Lang.Class.FromType(serviceType));
		}

		public static DownloaderServiceRequirement StartDownloadServiceIfRequired(Context context, Intent notificationClient, Type serviceType)
		{
			return StartDownloadServiceIfRequired(context, notificationClient, Java.Lang.Class.FromType(serviceType));
		}
	}
}

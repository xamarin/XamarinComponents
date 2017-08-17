using System;
using Android.App;
using Android.Content;

namespace Google.Android.Vending.Expansion.Downloader
{
	partial class DownloaderClientMarshaller
	{
		public static IStub CreateStub(IDownloaderClient itf, Type downloaderService)
		{
			return CreateStub(itf, Java.Lang.Class.FromType(downloaderService));
		}

		public static DownloadServiceRequirement StartDownloadServiceIfRequired(Context context, PendingIntent notificationClient, Type serviceType)
		{
			return StartDownloadServiceIfRequired(context, notificationClient, Java.Lang.Class.FromType(serviceType));
		}
		public static DownloadServiceRequirement StartDownloadServiceIfRequired(Context context, Intent notificationClient, Type serviceType)
		{
			return StartDownloadServiceIfRequired(context, notificationClient, Java.Lang.Class.FromType(serviceType));
		}
	}
}

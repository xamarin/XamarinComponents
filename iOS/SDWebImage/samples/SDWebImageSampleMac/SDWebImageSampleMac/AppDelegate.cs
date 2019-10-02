using AppKit;
using Foundation;

using SDWebImage;

namespace SDWebImageSampleMac
{
	[Register("AppDelegate")]
	public partial class AppDelegate : NSApplicationDelegate
	{
		public override void DidFinishLaunching(NSNotification notification)
		{
			SDWebImageManager.SharedManager.ImageDownloader.SetHttpHeaderValue("SDWebImage Demo", "SDWebImageSampleMac");
			SDWebImageManager.SharedManager.ImageDownloader.ExecutionOrder = SDWebImageDownloaderExecutionOrder.LastInFirstOut;
		}

		public override bool ApplicationShouldTerminateAfterLastWindowClosed(NSApplication sender)
		{
			return true;
		}

		partial void OnClearCaches(NSObject sender)
		{
			SDWebImageManager.SharedManager.ImageCache.ClearMemory();
			SDWebImageManager.SharedManager.ImageCache.ClearDisk();
		}
	}
}

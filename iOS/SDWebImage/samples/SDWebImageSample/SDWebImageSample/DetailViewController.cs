using System;
using Foundation;
using UIKit;

using SDWebImage;

namespace SDWebImageSample
{
	public partial class DetailViewController : UIViewController
	{
		public DetailViewController(IntPtr handle)
			: base(handle)
		{
		}

		public NSUrl ImageUrl { get; set; }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			if (ImageUrl != null)
			{
				activity.StartAnimating();
				progress.Hidden = false;
				progress.Progress = 0;

				imageView.SetImage(
					ImageUrl,
					UIImage.FromBundle("placeholder"),
					SDWebImageOptions.ProgressiveDownload,
					ProgressHandler,
					CompletedHandler);
			}
		}

		private void ProgressHandler(nint receivedSize, nint expectedSize, NSUrl url)
		{
			InvokeOnMainThread(() =>
			{
				progress.Progress = (float)receivedSize / (float)expectedSize;
			});
		}

		private void CompletedHandler(UIImage image, NSError error, SDImageCacheType cacheType, NSUrl url)
		{
			InvokeOnMainThread(() =>
			{
				activity.StopAnimating();
				progress.Hidden = true;
			});
		}
	}
}

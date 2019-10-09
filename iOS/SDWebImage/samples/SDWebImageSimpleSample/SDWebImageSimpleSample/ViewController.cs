using System;
using Foundation;
using UIKit;

using SDWebImage;

namespace SDWebImageSimpleSample
{
	public partial class ViewController : UIViewController
	{
		protected ViewController(IntPtr handle)
			: base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			btnDownload.TouchUpInside += delegate
			{
				// start the download
				imageView.SetImage(
					new NSUrl("https://blog.xamarin.com/wp-content/uploads/2013/11/MicrosoftXamarin2.png"),
					null,
					SDWebImageOptions.ProgressiveDownload,
					ProgressHandler,
					CompletedHandler);
			};
		}

		private void ProgressHandler(nint receivedSize, nint expectedSize, NSUrl url)
		{
			if (expectedSize > 0)
			{
				// update the UI with progress
				InvokeOnMainThread(() =>
				{
					float progress = (float)receivedSize / (float)expectedSize;
					progressBar.SetProgress(progress, true);
					lblPercent.Text = $"downloading: {(progress):0.0%}";
				});
			}
		}

		private void CompletedHandler(UIImage image, NSError error, SDImageCacheType cacheType, NSUrl url)
		{
			InvokeOnMainThread(() =>
			{
				// update the UI with complete
				if (error != null)
					lblPercent.Text = "there was a problem";
				else
					lblPercent.Text = "download completed";
			});
		}
	}
}

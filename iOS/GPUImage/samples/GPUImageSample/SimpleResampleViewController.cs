using System;
using CoreGraphics;
using UIKit;

using GPUImage.Filters.ColorProcessing;
using GPUImage.Filters.ImageProcessing;
using GPUImage.Sources;

namespace GPUImageSample
{
	partial class SimpleResampleViewController : UIViewController
	{
		private UIImage	inputImage;

		public SimpleResampleViewController (IntPtr handle)
			: base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			inputImage = UIImage.FromBundle ("Lambeau.jpg");

			OnResampleSelectorChanged (resampleSelector);
		}

		async partial void OnResampleSelectorChanged (UISegmentedControl sender)
		{
			switch (sender.SelectedSegment) {
			case 0:
				// Linear downsampling
				var passthroughFilter = new GPUImageBrightnessFilter ();
				passthroughFilter.ForceProcessingAtSize (new CGSize (640.0, 480.0));

				var linearImageSource = new GPUImagePicture (inputImage);
				linearImageSource.AddTarget (passthroughFilter);

				passthroughFilter.UseNextFrameForImageCapture ();
				await linearImageSource.ProcessImageAsync ();

				imageView.Image = passthroughFilter.ToImage (UIImageOrientation.Up);
				break;

			case 1:
				// Lanczos downsampling
				var lanczosResamplingFilter = new GPUImageLanczosResamplingFilter ();
				lanczosResamplingFilter.ForceProcessingAtSize (new CGSize (640.0, 480.0));

				var lanczosImageSource = new GPUImagePicture (inputImage);
				lanczosImageSource.AddTarget (lanczosResamplingFilter);

				lanczosResamplingFilter.UseNextFrameForImageCapture ();
				await lanczosImageSource.ProcessImageAsync ();

				imageView.Image = lanczosResamplingFilter.ToImage (UIImageOrientation.Up);
				break;

			case 2:
				// Trilinear downsampling
				var passthroughFilter2 = new GPUImageBrightnessFilter ();
				passthroughFilter2.ForceProcessingAtSize (new CGSize (640.0, 480.0));

				var trilinearImageSource = new GPUImagePicture (inputImage, true);
				trilinearImageSource.AddTarget (passthroughFilter2);

				passthroughFilter2.UseNextFrameForImageCapture ();
				await trilinearImageSource.ProcessImageAsync ();

				imageView.Image = passthroughFilter2.ToImage (UIImageOrientation.Up);
				break;
			}
		}
	}
}

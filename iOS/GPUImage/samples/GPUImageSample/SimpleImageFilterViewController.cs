using System;
using UIKit;

using GPUImage.Filters.ImageProcessing;
using GPUImage.Sources;

namespace GPUImageSample
{
	partial class SimpleImageFilterViewController : UIViewController
	{
		private GPUImagePicture picture;
		private GPUImageTiltShiftFilter filter;

		public SimpleImageFilterViewController (IntPtr handle)
			: base (handle)
		{
		}

		public async override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			filter = new GPUImageTiltShiftFilter ();
			// This is now needed to make the filter run at the smaller output size
			filter.ForceProcessingAtSize (imageView.SizeInPixels);
			filter.AddTarget (imageView);

			var inputImage = UIImage.FromBundle ("WID-small.jpg");
			picture = new GPUImagePicture (inputImage, true);
			picture.AddTarget (filter);

			await picture.ProcessImageAsync ();
		}

		async partial void OnImageSliderChanged (UISlider sender)
		{
			var midpoint = sender.Value;

			filter.TopFocusLevel = midpoint - 0.1f;
			filter.BottomFocusLevel = midpoint + 0.1f;

			await picture.ProcessImageAsync ();
		}
	}
}

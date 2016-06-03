using System;
using AssetsLibrary;
using Foundation;
using UIKit;

using GPUImage.Filters.ColorProcessing;
using GPUImage.Filters.Effects;
using GPUImage.Sources;

namespace GPUImageSample
{
	partial class CameraPhotoViewController : UIViewController
	{
		private GPUImageStillCamera stillCamera;
		private GPUImagePixellateFilter pixellateFilter;
		private GPUImageSepiaFilter sepiaFilter;

		public CameraPhotoViewController (IntPtr handle)
			: base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			stillCamera = new GPUImageStillCamera ();
			stillCamera.OutputImageOrientation = InterfaceOrientation;

			pixellateFilter = new GPUImagePixellateFilter ();
			sepiaFilter = new GPUImageSepiaFilter ();

			stillCamera.AddTarget (pixellateFilter);
			pixellateFilter.AddTarget (sepiaFilter);
			sepiaFilter.AddTarget (imageView);

			stillCamera.StartCameraCapture ();
		}

		public override void DidRotate (UIInterfaceOrientation fromInterfaceOrientation)
		{
			base.DidRotate (fromInterfaceOrientation);

			// update the camera orientation
			stillCamera.OutputImageOrientation = InterfaceOrientation;
		}

		partial void OnImageSliderChanged (UISlider sender)
		{
			pixellateFilter.FractionalWidthOfAPixel = sender.Value;
		}

		async partial void OnTakePhoto (UIBarButtonItem sender)
		{
			sender.Enabled = false;

			try {
				// capture from camera
				var processedJPEG = await stillCamera.CapturePhotoAsJPEGAsync (sepiaFilter);

				// Save to assets library
				var library = new ALAssetsLibrary ();
				try {
					var assetURL = await library.WriteImageToSavedPhotosAlbumAsync (processedJPEG, stillCamera.CurrentCaptureMetadata);
					Console.WriteLine ("Photo saved: " + assetURL);
				} catch (Exception ex) {
					Console.WriteLine ("Photo save error: " + ex);
				}
			} catch (Exception ex) {
				Console.WriteLine ("Photo capture error: " + ex);
			}

			sender.Enabled = true;
		}
	}
}

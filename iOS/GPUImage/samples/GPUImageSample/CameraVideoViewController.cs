using System;
using System.IO;
using AssetsLibrary;
using AVFoundation;
using CoreGraphics;
using Foundation;
using UIKit;

using GPUImage.Filters.ColorProcessing;
using GPUImage.Filters.Effects;
using GPUImage.Outputs;
using GPUImage.Sources;

namespace GPUImageSample
{
	partial class CameraVideoViewController : UIViewController
	{
		private GPUImageVideoCamera videoCamera;
		private GPUImagePixellateFilter pixellateFilter;
		private GPUImageSepiaFilter sepiaFilter;
		private GPUImageMovieWriter movieWriter;

		private NSUrl movieUrl;

		public CameraVideoViewController (IntPtr handle)
			: base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			videoCamera = new GPUImageVideoCamera (AVCaptureSession.Preset640x480, AVCaptureDevicePosition.Back);
			videoCamera.OutputImageOrientation = InterfaceOrientation;
			videoCamera.HorizontallyMirrorFrontFacingCamera = false;
			videoCamera.HorizontallyMirrorRearFacingCamera = false;

			pixellateFilter = new GPUImagePixellateFilter ();
			sepiaFilter = new GPUImageSepiaFilter ();

			videoCamera.AddTarget (pixellateFilter);
			pixellateFilter.AddTarget (sepiaFilter);
			sepiaFilter.AddTarget (imageView);

			videoCamera.StartCameraCapture ();
		}

		public override void DidRotate (UIInterfaceOrientation fromInterfaceOrientation)
		{
			base.DidRotate (fromInterfaceOrientation);

			// update the camera orientation
			videoCamera.OutputImageOrientation = InterfaceOrientation;
		}

		partial void OnImageSliderChanged (UISlider sender)
		{
			pixellateFilter.FractionalWidthOfAPixel = sender.Value;
		}

		async partial void OnTakePhoto (UIBarButtonItem sender)
		{
			sender.Enabled = false;

			if (movieWriter == null) {
				// get new file path
				var documents = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
				var pathToMovie = Path.Combine (documents, "Movie.m4v");
				if (File.Exists (pathToMovie)) {
					File.Delete (pathToMovie);
				}
				movieUrl = new NSUrl (pathToMovie, false);

				// start recording video
				movieWriter = new GPUImageMovieWriter (movieUrl, new CGSize (480, 640));
				movieWriter.EncodingLiveVideo = true;
				sepiaFilter.AddTarget (movieWriter);
				videoCamera.AudioEncodingTarget = movieWriter;
				movieWriter.StartRecording ();

				Console.WriteLine ("Video recording started.");
			} else {
				// stop recording video
				sepiaFilter.RemoveTarget (movieWriter);
				videoCamera.AudioEncodingTarget = null;
				await movieWriter.FinishRecordingAsync ();

				// save to library
				var library = new ALAssetsLibrary ();
				try {
					var assetURL = await library.WriteVideoToSavedPhotosAlbumAsync (movieUrl);
					Console.WriteLine ("Video saved: " + assetURL);
				} catch (Exception ex) {
					Console.WriteLine ("Video save error: " + ex);
				}

				movieWriter = null;
				movieUrl = null;

				Console.WriteLine ("Video recording completed.");
			}

			sender.Enabled = true;
		}
	}
}

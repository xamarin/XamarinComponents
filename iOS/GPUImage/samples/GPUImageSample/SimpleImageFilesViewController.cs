using System;
using System.IO;
using Foundation;
using UIKit;

using GPUImage.Filters.ColorProcessing;
using GPUImage.Filters.Effects;
using GPUImage.Sources;

namespace GPUImageSample
{
	partial class SimpleImageFilesViewController : UIViewController
	{
		private UIImage	inputImage;
		private NSUrl inputImageUrl;

		private string path1;
		private string path2;

		public SimpleImageFilesViewController (IntPtr handle)
			: base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			inputImage = UIImage.FromBundle ("Lambeau.jpg");
			inputImageUrl = NSBundle.MainBundle.GetUrlForResource ("Lambeau", "jpg");

			var documents = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
			path1 = Path.Combine (documents, "Lambeau-filtered1.png");
			path2 = Path.Combine (documents, "Lambeau-filtered2.png");

			OnFilterSelectorChanged (filteringSelector);
		}

		async partial void OnFilterSelectorChanged (UISegmentedControl sender)
		{
			switch (sender.SelectedSegment) {
			case 0:
				// Set up a manual image filtering chain
				var vignetteFilter = new GPUImageVignetteFilter ();
				vignetteFilter.VignetteEnd = 0.6f;
				vignetteFilter.VignetteStart = 0.4f;

				var sepiaFilter = new GPUImageSepiaFilter ();
				sepiaFilter.AddTarget (vignetteFilter);

				var imageSource = new GPUImagePicture (inputImageUrl);
				imageSource.AddTarget (sepiaFilter);

				vignetteFilter.UseNextFrameForImageCapture ();
				await imageSource.ProcessImageAsync ();

				using (var currentImage = vignetteFilter.ToImage ())
				using (var pngData = currentImage.AsPNG ()) {
					pngData.Save (path1, true);
				}

				imageView.Image = UIImage.FromFile (path1);
				break;

			case 1:
				// Do a simpler image filtering
				var imageFilter = new GPUImageSketchFilter ();

				using (var quickFilteredImage = imageFilter.CreateFilteredImage (inputImage))
				using (var pngData = quickFilteredImage.AsPNG ()) {
					pngData.Save (path2, true);
				}

				imageView.Image = UIImage.FromFile (path2);
				break;
			}
		}
	}
}

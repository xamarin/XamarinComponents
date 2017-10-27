
using System;

using Foundation;
using UIKit;
using CoreGraphics;

using Dropbox.CoreApi.iOS;
using System.IO;

namespace DropboxCoreApiSample
{
	public class ImageViewController : UIViewController, IRestClientDelegate
	{
		UIImageView imageView;
		RestClient restClient;
		string photoPath;

		public ImageViewController (string path, string name)
		{
			View.BackgroundColor = UIColor.White;

			photoPath = path;

			imageView = new UIImageView (CGRect.Empty);
			imageView.Image = UIImage.FromFile ("dropbox_not_found.png");
			imageView.TranslatesAutoresizingMaskIntoConstraints = false;
			View.Add (imageView);

			AddConstraints ();

			// Create a rest client that handle the download of the image
			restClient = new RestClient (Session.SharedSession);
			// Handle the download of the image
			restClient.ThumbnailLoaded += (sender, e) => imageView.Image = UIImage.FromFile (e.DestPath);
			// Handle if something went wrong with the download of the image
			restClient.LoadThumbnailFailed += (sender, e) => {
				imageView.Image = UIImage.FromFile ("dropbox_not_found.png");

				// Try to download the image again if the user asks for it
				var alertView = new UIAlertView ("Hmm...", "Something went wrong when trying to show the image...", null, "Not now", new [] { "Try Again" });
				alertView.Clicked += (avSender, avE) => {
					if (avE.ButtonIndex == 1)
						restClient.LoadThumbnail (path, "iphone_bestfit", PhotoPath ());
				};
				alertView.Show ();
			};

			// Show the image if you have downloaded previously, otherwise,
			// download the image and save it in the tmp folder
			if (File.Exists (PhotoPath (name)))
				imageView.Image = UIImage.FromFile (PhotoPath (name));
			else
				restClient.LoadThumbnail (photoPath, "iphone_bestfit", PhotoPath (name));
		}

		// Add constraints to imageView
		void AddConstraints ()
		{
			var views = new NSDictionary ("image", imageView);
			var metrics = new NSDictionary ("width", View.Frame.Width);

			// Set the width and height to imageView
			imageView.AddConstraints (NSLayoutConstraint.FromVisualFormat ("H:[image(width)]", 0, metrics, views));
			imageView.AddConstraints (NSLayoutConstraint.FromVisualFormat ("V:[image(width)]", 0, metrics, views));

			// Set the imageView in the center of the view
			View.AddConstraint (NSLayoutConstraint.Create (
				imageView, 
				NSLayoutAttribute.CenterX, 
				NSLayoutRelation.Equal, 
				View, 
				NSLayoutAttribute.CenterX, 
				1, 
				0));

			View.AddConstraint (NSLayoutConstraint.Create (
				imageView, 
				NSLayoutAttribute.CenterY, 
				NSLayoutRelation.Equal, 
				View, 
				NSLayoutAttribute.CenterY, 
				1, 
				0));
		}

		// Return the path of the tmp folder plus a photo name
		string PhotoPath (string imageName = "photo.jpg")
		{
			return Path.GetTempPath () + imageName;
		}

	}
}


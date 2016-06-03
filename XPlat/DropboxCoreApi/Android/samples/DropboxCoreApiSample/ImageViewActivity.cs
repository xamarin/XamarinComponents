
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Dropbox.CoreApi.Android;
using Android.Graphics.Drawables;
using System.Threading.Tasks;
using System.IO;

namespace DropboxCoreApiSample
{
	[Activity (Label = "ImageViewActivity")]			
	public class ImageViewActivity : Activity
	{

		string imagePath;
		string imageName;
		ImageView imgDropbox;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.ImageView);

			imgDropbox = FindViewById<ImageView> (Resource.Id.imgDropbox);

			// Retrieves the name and the path of the image that will be downloaded
			imagePath = Intent.Extras.GetString (MainActivity.ImagePathKey);
			imageName = Intent.Extras.GetString (MainActivity.ImageNameKey);
		}

		protected async override void OnStart ()
		{
			base.OnStart ();

			// Start the download of the image
			// Must be called in another thread, because it makes a network call
			await Task.Factory.StartNew (() => GetImageFromDropbox ());
		}

		// Gets the specified image from the Dropbox folder that you specified in the DropboxCredentials class
		// This method must be invoked on a background thread because it makes a network call
		void GetImageFromDropbox ()
		{
			try {
				// Image to be shown
				Drawable thumbnail;
				var cachePath = CacheDir.AbsolutePath + "/" + imageName;

				// Verifies if the image has not been downloaded before
				if (!File.Exists (cachePath))
					using (var output = File.OpenWrite (cachePath)) {
						// Get the info of the image to be downloaded
						var metadata = MainActivity.DBApi.Metadata (imagePath, 0, null, false, null);
						if (metadata == null)
							throw new Exception ("The file doesn't exist or the name of the file is incorrect");

						// Gets the image from Dropbox and saves it to the cache folder
						MainActivity.DBApi.GetThumbnail (metadata.Path, output, DropboxApi.ThumbSize.BestFit1024x768, DropboxApi.ThumbFormat.Jpeg, null);
					}
				
				// Get the image from the cache
				thumbnail = Drawable.CreateFromPath (cachePath);
				// Show the image
				RunOnUiThread (() => imgDropbox.SetImageDrawable (thumbnail));
			} catch (System.Exception ex) {
				RunOnUiThread (() => Toast.MakeText (this, "There was a problem when trying to get the images: " + ex.Message, ToastLength.Long).Show ());
			}
		}
	}
}


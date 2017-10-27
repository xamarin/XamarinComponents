
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

using MonoTouch.Dialog;
using Dropbox.CoreApi.iOS;

using Foundation;
using UIKit;


namespace DropboxCoreApiSample
{
	public partial class MainViewController : DialogViewController
	{
		public const string UpdateViewNotification = "UpdateView";

		UIBarButtonItem btnLink;
		RestClient restClient;

		public MainViewController () : base (UITableViewStyle.Grouped, null)
		{
			Root = new RootElement ("Dropbox Core Api iOS Sample");

			// Button that will ask the link of the app or will unlink the app
			btnLink = new UIBarButtonItem ("Link", UIBarButtonItemStyle.Plain, (sender, e) => {
				if (!Session.SharedSession.IsLinked)
					// Ask for linking the app
					Session.SharedSession.LinkFromController (this);
				else {
					// Unlink the app when you're done
					Session.SharedSession.UnlinkAll ();
					new UIAlertView ("App Unlinked!", "Your App has been unlinked", null, "OK", null).Show ();
					UpdateView ();
				}
			});
			// Verifies that the App Key is in the plist file
			btnLink.Enabled = VerifyInfoPlist ();

			// Deletes all the images that were downloaded from Dropbox
			// and all the files that were written into the tmp folder
			var btnClear = new UIBarButtonItem ("Clear", UIBarButtonItemStyle.Plain, (sender, e) => {
				var files = Directory.GetFiles (Path.GetTempPath ());

				foreach (var file in files)
					File.Delete (file);

				new UIAlertView ("Files deleted", "All images downloaded from Dropbox\rand all the files written have been deleted!", null, "OK", null).Show ();
			});

			NavigationItem.RightBarButtonItem = btnLink;
			NavigationItem.LeftBarButtonItem = btnClear;

			// Add a notification that alerts when the link was successful, updates the view
			NSNotificationCenter.DefaultCenter.AddObserver (new NSString (MainViewController.UpdateViewNotification), (n) => UpdateView ());
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			UpdateView ();
		}

		// Verifies that the App Key is in the plist file
		bool VerifyInfoPlist ()
		{
			var plistDict = NSDictionary.FromFile (NSBundle.MainBundle.PathForResource ("Info", "plist"));
			var urlTypes = plistDict ["CFBundleURLTypes"] as NSArray;
			var urlSchemes = urlTypes.GetItem<NSDictionary> (0) ["CFBundleURLSchemes"] as NSArray;

			if (urlSchemes.GetItem<NSString> (0) == "db-APP_KEY") {
				new UIAlertView ("DB App Key missing in Info.plist", "Set your DB App Key in your Url Scheme in Info.plist", null, "OK", null).Show ();
				return false;
			}

			return true;
		}

		// If the app is linked, add every image that it finds, otherwise, remove the image list from the view.
		void UpdateView ()
		{	
			// If the app is linked, get every image and add it to the view with Monotouch.Dialog
			if (Session.SharedSession.IsLinked) {
				btnLink.Title = "Unlink";

				if (Root.Count != 0)
					return;
				
				// Create the rest client that handle the download of all the files
				if (restClient == null) {
					restClient = new RestClient (Session.SharedSession);
					// Method to get the images
					restClient.MetadataLoaded += GetImages;
					// Handle if something went wrong when tried to get the images
					restClient.LoadMetadataFailed += (sender, e) => new UIAlertView ("Error getting images", e.Error.Description, null, "OK", null).Show ();
				}
				// Get all the files from your folder
				restClient.LoadMetadata (DropboxCredentials.FolderPath);
			} else {
				// If the app was unliked successfully, remove the list of image from view
				btnLink.Title = "Link";
				Root.Clear ();
			}
		}

		#region RestClient Events

		// Get all the images from the Dropbox folder
		void GetImages (object sender, RestClientMetadataLoadedEventArgs e)
		{
			// If the path is a file, there is nothing to search
			if (!e.Metadata.IsDirectory) {
				new UIAlertView ("Not a directory", "The specified path is a file, not a directory", null, "OK", null).Show ();
				return;
			}

			Root.Add (new Section ("Upload a file to Dropbox") {
				new StringElement ("Write a new file", () => 
					NavigationController.PushViewController (new TextViewController (), true))
			});

			// Section that will contain the name of the images
			var imagesName = new Section ("Images from Dropbox");

			// Will get only the images with .jpg or .jpeg extension
			foreach (var item in e.Metadata.Contents) {
				if (!item.IsDirectory &&
				    (item.Path.EndsWith (".jpeg", StringComparison.InvariantCultureIgnoreCase) ||
				    item.Path.EndsWith (".jpg", StringComparison.InvariantCultureIgnoreCase)))
					imagesName.Add (new StringElement (item.Filename, () => 
						NavigationController.PushViewController (new ImageViewController (item.Path, item.Filename), true)));
			}

			if (imagesName.Count == 0)
				new UIAlertView ("No images found", "Please add some images to the Dropbox Folder", null, "OK", null).Show ();
			else
				Root.Add (imagesName);
		}

		#endregion
	}
}

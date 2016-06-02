using System;
using System.IO;

using Foundation;
using UIKit;
using CoreGraphics;


using Dropbox.CoreApi.iOS;

namespace DropboxCoreApiSample
{
	public partial class TextViewController : UIViewController
	{
		// A TextField with Placeholder
		CustomUITextView textView;
		RestClient restClient;
		string filename;

		public TextViewController ()
		{
			View.BackgroundColor = UIColor.White;

			// Will handle the save to Dropbox process
			var btnSave = new UIBarButtonItem ("Save", UIBarButtonItemStyle.Plain, WriteFile);
			btnSave.Enabled = false;

			// Create the TextField with a Placeholder
			textView = new CustomUITextView (CGRect.Empty, "Type something nice!");
			textView.TranslatesAutoresizingMaskIntoConstraints = false;
			// If the user has written something, you can save the file
			textView.Changed += (sender, e) => btnSave.Enabled = textView.Text.Length != 0;

			// Rest client that will handle the file upload
			restClient = new RestClient (Session.SharedSession);
			// Once the file is on Dropbox, notify the user
			restClient.FileUploaded += (sender, e) => {
				new UIAlertView ("Saved on Dropbox", "The file was uploaded to Dropbox correctly", null, "OK", null).Show ();
				#if __UNIFIED__
				NavigationController.PopViewController (true);
				#else
				NavigationController.PopViewControllerAnimated (true);
				#endif
			};
			// Handle if something went wrong with the upload of the file
			restClient.LoadFileFailed += (sender, e) => {
				// Try to upload the file again
				var alertView = new UIAlertView ("Hmm...", "Something went wrong when trying to save the file on Dropbox...", null, "Not now", new [] { "Try Again" });
				alertView.Clicked += (avSender, avE) => {
					if (avE.ButtonIndex == 1)
						restClient.UploadFile (filename, DropboxCredentials.FolderPath, null, Path.GetTempPath () + filename);
				};
				alertView.Show ();
			};

			// Add the view with its constraints
			View.Add (textView);
			NavigationItem.RightBarButtonItem = btnSave;

			AddConstraints ();
		}

		void AddConstraints ()
		{
			var views = new NSDictionary ("textView", textView);

			View.AddConstraints (NSLayoutConstraint.FromVisualFormat ("H:|-0-[textView]-0-|", 0, null, views));
			View.AddConstraints (NSLayoutConstraint.FromVisualFormat ("V:|-0-[textView]-0-|", 0, null, views));
		}

		// Process to save the file on Dropbox
		void WriteFile (object sender, EventArgs e)
		{
			// Notify that the user has ended typing
			textView.EndEditing (true);

			// Ask for a name to the file
			var alertView = new UIAlertView ("Save to Dropbox", "Enter a name for the file", null, "Cancel", new [] { "Save" });
			alertView.AlertViewStyle = UIAlertViewStyle.PlainTextInput;
			alertView.Clicked += (avSender, avE) => {
				// Once we have the name, we need to save the file locally first and then upload it to Dropbox
				if (avE.ButtonIndex == 1) {
					filename = alertView.GetTextField (0).Text + ".txt";
					var fullPath = Path.GetTempPath () + filename;
					// Write the file locally
					File.WriteAllText (fullPath, textView.Text);
					// Now upload it to Dropbox
					restClient.UploadFile (filename, DropboxCredentials.FolderPath, null, fullPath);
				}
			};
			alertView.Show ();
		}
	}
}


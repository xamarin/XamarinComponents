using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Dropbox.CoreApi.Android;
using Dropbox.CoreApi.Android.Session;
using Java.Lang;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;

namespace DropboxCoreApiSample
{
	public static class DropboxCredentials
	{
		// To get your credentials, create your own Drobox App.
		// Visit the following link: https://www.dropbox.com/developers/apps
		public static string AppKey = "YOUR_APP_KEY";
		public static string AppSecret = "YOUR_APP_SECRET";
		public static string FolderPath = "/Photos/";
		public static string PreferencesName = "Prefs";
	}

	[Activity (Label = "DropboxCoreApiSample", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		public static readonly string ImagePathKey = "ImagePath";
		public static readonly string ImageNameKey = "ImageName";

		public static DropboxApi DBApi { get; set; }

		Button btnLink;
		Button btnClear;
		ListView lstImagesName;
		ImageNameAdapter adapter;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Main);

			// Creates a new session
			DBApi = new DropboxApi (CreateSession ());

			btnLink = FindViewById<Button> (Resource.Id.btnLink);
			// If you have not specified the keys correctly, disable the link button
			btnLink.Enabled = VerifyDropboxKeys ();

			btnClear = FindViewById<Button> (Resource.Id.btnClear);

			lstImagesName = FindViewById<ListView> (Resource.Id.lstImagesName);
			// Passes the name and the path of the file that will be downloaded
			lstImagesName.ItemClick += (sender, e) => {
				var intent = new Intent (this, typeof(ImageViewActivity));
				intent.PutExtra (MainActivity.ImagePathKey, adapter.ImagesPath [e.Position]);
				intent.PutExtra (MainActivity.ImageNameKey, adapter.ImagesName [e.Position]);
				StartActivity (intent);
			};

			// Initializes the process of linking to Dropbox or the process to revoke the permission
			btnLink.Click += delegate {
				try {
					if (!DBApi.Session.IsLinked)
						(DBApi.Session as AndroidAuthSession).StartOAuth2Authentication (this);
					else
						Logout ();
				} catch (ActivityNotFoundException ex) {
					Toast.MakeText (this, ex.Message, ToastLength.Long).Show ();
				}
			};

			// Removes all the downloaded images from Dropbox
			btnClear.Click += (sender, e) => {
				try {
					var files = Directory.GetFiles (CacheDir.AbsolutePath);

					foreach (var file in files)
						File.Delete (file);

					Toast.MakeText (this, "All images downloaded from Dropbox have been deleted!", ToastLength.Long).Show ();
				} catch (System.Exception ex) {
					Toast.MakeText (this, "There was a problem trying to erase the cache folder: " + ex.Message, ToastLength.Long).Show ();
				}
			};
		}

		protected async override void OnStart ()
		{
			base.OnStart ();

			// Verifies if you have linked the app with Dropbox before and retrieves the images on Dropbox
			if (DBApi.Session.IsLinked) {
				btnLink.Text = "Unlink account";

				// Must be called in another thread, because it makes a network call
				await Task.Factory.StartNew (() => GetImagesFromDropbox ());
			}
		}

		protected async override void OnResume ()
		{
			base.OnResume ();

			// After you allowed to link the app with Dropbox,
			// you need to finish the Authentication process
			var session = DBApi.Session as AndroidAuthSession;
			if (!session.AuthenticationSuccessful ())
				return;
			
			try {
				// Call this method to finish the authentcation process
				// Will bind the user's access token to the session.
				session.FinishAuthentication ();
				// Save the Dropbox authetication token
				StoreAuth (session);
				btnLink.Text = "Unlink account";

				// Get all the images that live in the specified Dropbox folder
				// Must be called in another thread, because it makes a network call
				await Task.Factory.StartNew (() => GetImagesFromDropbox ());
			} catch (IllegalStateException ex) {
				Toast.MakeText (this, ex.LocalizedMessage, ToastLength.Short).Show ();
			}
		}

		// Gets the images from the Dropbox folder that you specified in the DropboxCredentials class
		// This method must be invoked on a background thread because it makes a network call
		void GetImagesFromDropbox ()
		{
			try {
				// This line must be invoked on a background thread because makes a network call
				var metadata = DBApi.Metadata (DropboxCredentials.FolderPath, 0, null, true, null);

				if (!metadata.IsDir) {
					RunOnUiThread (() => Toast.MakeText (this, "The specified path is a file, not a folder. Please verify.", ToastLength.Long).Show ());
					return;
				}

				var fileNames = new List<string> ();
				var filePaths = new List<string> ();

				// Gets all images in the specified folder
				foreach (DropboxApi.Entry entry in metadata.Contents) {
					if (entry.IsDir ||
					    (!entry.FileName ().EndsWith (".jpg", StringComparison.InvariantCultureIgnoreCase) &&
					    !entry.FileName ().EndsWith (".jpeg", StringComparison.InvariantCultureIgnoreCase)))
						continue;

					fileNames.Add (entry.FileName ());
					filePaths.Add (entry.Path);
				}

				// Creates and assign the retrieved info in the list
				adapter = new ImageNameAdapter (this, fileNames.ToArray (), filePaths.ToArray ());
				RunOnUiThread (() => lstImagesName.Adapter = adapter);
			} catch (System.Exception ex) {
				RunOnUiThread (() => Toast.MakeText (this, "There was a problem when trying to get the images: " + ex.Message, ToastLength.Long).Show ());
			}
		}

		// Creates a new Dropbox Session or retrives the existing session
		AndroidAuthSession CreateSession ()
		{
			var session = new AndroidAuthSession (new AppKeyPair (DropboxCredentials.AppKey, DropboxCredentials.AppSecret));
			GetAuth (session);
			return session;
		}

		// Retrieves the Dropbox Auth Token saved on preferences
		void GetAuth (AndroidAuthSession session)
		{
			var prefs = GetSharedPreferences (DropboxCredentials.PreferencesName, 0);
			var key = prefs.GetString (DropboxCredentials.AppKey, null);
			var secret = prefs.GetString (DropboxCredentials.AppSecret, null);

			if (string.IsNullOrWhiteSpace (key) || string.IsNullOrWhiteSpace (secret))
				return;
			
			session.OAuth2AccessToken = secret;
		}

		// Unlink the app from Dropbox
		void Logout ()
		{
			DBApi.Session.Unlink ();

			RemoveAuth ();

			lstImagesName.Adapter = null;
			btnLink.Text = "Link to Dropbox!";
		}

		// Saves the Dropbox Auth token to proferences
		void StoreAuth (AndroidAuthSession session)
		{
			var oauth2AccessToken = session.OAuth2AccessToken;

			if (!string.IsNullOrWhiteSpace (oauth2AccessToken)) {
				var edit = GetSharedPreferences (DropboxCredentials.PreferencesName, 0).Edit ();
				edit.PutString (DropboxCredentials.AppKey, "oauth2:");
				edit.PutString (DropboxCredentials.AppSecret, oauth2AccessToken);
				edit.Commit ();
			}
		}

		// Removes the Dropbox Auth token from preferences
		void RemoveAuth ()
		{
			var edit = GetSharedPreferences (DropboxCredentials.PreferencesName, 0).Edit ();
			edit.Clear ();
			edit.Commit ();
		}

		// Verifies that the Dropbox Keys are set correctly
		bool VerifyDropboxKeys ()
		{
			// Verifies the keys in the code
			if (DropboxCredentials.AppKey.Equals ("YOUR_APP_KEY") || DropboxCredentials.AppSecret.Equals ("YOUR_APP_SECRET")) {
				Toast.MakeText (this, "Please, set your Dropbox Keys in DropboxCredentials class.", ToastLength.Short).Show ();
				return false;
			}

			// Verifies the App key in the AndroidManifest file
			var testIntent = new Intent (Intent.ActionView);
			var manifestKey = "db-" + DropboxCredentials.AppKey;
			var uri = manifestKey + "://" + AuthActivity.AuthVersion + "/test";
			testIntent.SetData (Android.Net.Uri.Parse (uri));

			if (PackageManager.QueryIntentActivities (testIntent, 0).Count == 0) {
				Toast.MakeText (this, "The App key in the manifest file doesn't match with your App key in code." +
				" Please verify.", ToastLength.Short).Show ();
				return false;
			}

			return true;
		}
	}

}



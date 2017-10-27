using System;
using System.IO;

using Foundation;
using UIKit;

using Dropbox.CoreApi.iOS;

namespace DropboxCoreApiSample
{
	public static class DropboxCredentials
	{
		public static string AppKey = "DB_APP_KEY";
		public static string AppSecret = "DB_APP_SECRET";
		public static string FolderPath = "/Photos/";
	}

	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
	[Register ("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations

		public override UIWindow Window {
			get;
			set;
		}

		// To get your credentials, create your own Drobox App.
		// Visit the following link: https://www.dropbox.com/developers/apps

		UINavigationController navigationController;
		MainViewController mainVC;

		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			mainVC = new MainViewController ();
			navigationController = new UINavigationController (mainVC);

			// Create a new Dropbox Session, choose the type of access that your app has to your folders.
			// Session.RootAppFolder = The app will only have access to its own folder located in /Applications/AppName/
			// Session.RootDropbox = The app will have access to all the files that you have granted permission
			var session = new Session (DropboxCredentials.AppKey, DropboxCredentials.AppSecret, Session.RootDropbox);

			// Handle if something went wrong with the link of the app
			session.AuthorizationFailureReceived += (sender, e) => {
				// You can try again with the link if something went wrong
				var alertView = new UIAlertView ("Dropbox Session Ended", "Do you want to relink?", null, "No, thanks", new [] { "Yes, please" });
				alertView.Clicked += (avSender, avE) => {
					if (avE.ButtonIndex == 0)
						return;

					// Try to link the app
					Session.SharedSession.LinkUserId (e.UserId, mainVC);
				};
				alertView.Show ();
			};

			// The session that you have just created, will live through all the app
			Session.SharedSession = session;

			Window = new UIWindow (UIScreen.MainScreen.Bounds);
			Window.RootViewController = navigationController;
			Window.MakeKeyAndVisible ();

			return true;
		}

		public override bool OpenUrl (UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
		{
			// Once you have been linked, notify the MainViewController to update its view
			if (Session.SharedSession.HandleOpenUrl (url) && Session.SharedSession.IsLinked) {
				new UIAlertView ("Successful", "Your App has been linked successfully!", null, "OK", null).Show ();
				NSNotificationCenter.DefaultCenter.PostNotificationName (MainViewController.UpdateViewNotification, null);
			}
			
			return true;	
		}
	}
}



using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using WindowsAzure.Messaging;

namespace AzureMessagingSampleiOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		const string HUB_NAME = "YOUR-HUB-NAME";
		const string HUB_LISTEN_SECRET = "YOUR-HUB-LISTEN-SECRET";

		// class-level declarations
		UIWindow window;
		HomeViewController homeViewController;

		//
		// This method is invoked when the application has loaded and is ready to run. In this
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			// create a new window instance based on the screen size
			window = new UIWindow (UIScreen.MainScreen.Bounds);

			homeViewController = new HomeViewController (HUB_NAME == "YOUR-HUB-NAME");

			// If you have defined a root view controller, set it here:
			window.RootViewController = homeViewController;

			// Process any potential notification data from launch
			ProcessNotification (options);

			// Register for Notifications
			var notificationTypes = UIUserNotificationType.Badge |
                                    UIUserNotificationType.Sound | 
                                    UIUserNotificationType.Alert;

            var notificationSettings = UIUserNotificationSettings.GetSettingsForTypes (notificationTypes, null);

            UIApplication.SharedApplication.RegisterUserNotificationSettings (notificationSettings);

			// make the window visible
			window.MakeKeyAndVisible ();
			
			return true;
		}

        public override void DidRegisterUserNotificationSettings (UIApplication application, UIUserNotificationSettings notificationSettings)
        {
            UIApplication.SharedApplication.RegisterForRemoteNotifications ();
        }

		public override void ReceivedRemoteNotification (UIApplication application, NSDictionary userInfo)
		{
			// Process a notification received while the app was already open
			ProcessNotification (userInfo);
		}

		public override void RegisteredForRemoteNotifications (UIApplication application, NSData deviceToken)
		{
			// Connection string from your azure dashboard
			var cs = SBConnectionString.CreateListenAccess(
				new NSUrl("sb://" + HUB_NAME + "-ns.servicebus.windows.net/"),
				HUB_LISTEN_SECRET);

			// Register our info with Azure
			var hub = new SBNotificationHub (cs, HUB_NAME);
			hub.RegisterNativeAsync (deviceToken, null, err => {

				if (err != null) {
					Console.WriteLine("Error: " + err.Description);
					homeViewController.RegisteredForNotifications ("Error: " + err.Description);
				} else  {
					Console.WriteLine("Success");
					homeViewController.RegisteredForNotifications ("Successfully registered for notifications");
				}
			});
		}

		void ProcessNotification(NSDictionary userInfo)
		{
			if (userInfo == null)
				return;

			Console.WriteLine ("Received Notification");

			var apsKey = new NSString ("aps");
				
			if (userInfo.ContainsKey (apsKey)) {

				var alertKey = new NSString ("alert");

				var aps = (NSDictionary)userInfo.ObjectForKey (apsKey);

				if (aps.ContainsKey (alertKey)) {
					var alert = (NSString)aps.ObjectForKey (alertKey);

					homeViewController.ProcessNotification (alert);

					Console.WriteLine ("Notification: " + alert);
				}
			}
		}
	}
}


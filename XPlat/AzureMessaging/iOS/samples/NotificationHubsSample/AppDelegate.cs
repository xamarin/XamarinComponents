using System;

using Foundation;
using UIKit;
using WindowsAzure.Messaging.NotificationHubs;

namespace AzureMessagingSampleiOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate, IMSNotificationHubDelegate
	{
		const string HUB_NAME = "YOUR-HUB-NAME";
		const string CONNECTION_STRING = "YOUR-HUB-CONNECTION-STRING";

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

			var needsConfig = HUB_NAME == "YOUR-HUB-NAME" || CONNECTION_STRING == "YOUR-HUB-CONNECTION-STRING";

			homeViewController = new HomeViewController(needsConfig);

			// If you have defined a root view controller, set it here:
			window.RootViewController = homeViewController;

			MSNotificationHub.Init(CONNECTION_STRING, HUB_NAME);

			Console.WriteLine("Device Token: " + MSNotificationHub.GetPushChannel());

			MSNotificationHub.SetDelegate(this);

			// make the window visible
			window.MakeKeyAndVisible ();
			
			return true;
		}

        public void DidReceivePushNotification(MSNotificationHub notificationHub, MSNotificationHubMessage message)
        {
			this.homeViewController.ProcessNotification(message.Title, message.Body);

			Console.WriteLine("Notification Title: " + message.Title);
			Console.WriteLine("Notification Body: " + message.Body);
		}
	}
}

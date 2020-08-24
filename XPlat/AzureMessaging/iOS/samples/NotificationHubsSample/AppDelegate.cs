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
	public partial class AppDelegate : UIApplicationDelegate
	{
		// TODO: Customize these values to your own
		const string HUB_NAME = "YOUR-HUB-NAME";
		const string CONNECTION_STRING = "YOUR-HUB-CONNECTION-STRING";

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
			var window = new UIWindow (UIScreen.MainScreen.Bounds);

			var needsConfig = HUB_NAME == "YOUR-HUB-NAME" || CONNECTION_STRING == "YOUR-HUB-CONNECTION-STRING";

			var homeViewController = new HomeViewController(needsConfig);

			// If you have defined a root view controller, set it here:
			window.RootViewController = homeViewController;

			MSNotificationHub.Start(CONNECTION_STRING, HUB_NAME);

			Console.WriteLine("Device Token: " + MSNotificationHub.GetPushChannel());

			MSNotificationHub.SetDelegate(new NotificationListener(homeViewController));

			// make the window visible
			window.MakeKeyAndVisible ();
			
			return true;
		}       
	}

	public partial class NotificationListener : MSNotificationHubDelegate
	{
		HomeViewController homeViewController;

		public NotificationListener(HomeViewController homeViewController)
        {
			this.homeViewController = homeViewController;

		}

        public override void DidReceivePushNotification(MSNotificationHub notificationHub, MSNotificationHubMessage message)
        {
			homeViewController.ProcessNotification(message.Title, message.Body);

			Console.WriteLine("Notification Title: " + message.Title);
			Console.WriteLine("Notification Body: " + message.Body);
		}
	}
}

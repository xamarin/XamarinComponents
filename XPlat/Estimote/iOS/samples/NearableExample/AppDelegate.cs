using System;
using System.Linq;
using System.Collections.Generic;

using Foundation;
using UIKit;
using Estimote;

namespace NearableMonitoringExample
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		
		public override UIWindow Window {
			get;
			set;
		}

		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			CloudManager.SetupAppID ("app_09m6zk9o3m", "54db97c20dfff85fd3cba3db889d56f1");
			UINavigationBar.Appearance.BarTintColor = UIColor.FromRGBA (0.490f, 0.631f, 0.548f, 1.000f);
			UINavigationBar.Appearance.TintColor = UIColor.White;
			UINavigationBar.Appearance.SetTitleTextAttributes (new UITextAttributes { TextColor = UIColor.White });
			UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.LightContent;

			if (UIApplication.SharedApplication.RespondsToSelector (new ObjCRuntime.Selector ("registerUserNotificationSettings:"))) {
				UIApplication.SharedApplication.RegisterUserNotificationSettings (UIUserNotificationSettings.GetSettingsForTypes (
					UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, null));
			}

			return true;
		}
		
		// This method is invoked when the application is about to move from active to inactive state.
		// OpenGL applications should use this method to pause.
		public override void OnResignActivation (UIApplication application)
		{
		}
		
		// This method should be used to release shared resources and it should store the application state.
		// If your application supports background exection this method is called instead of WillTerminate
		// when the user quits.
		public override void DidEnterBackground (UIApplication application)
		{
		}
		
		// This method is called as part of the transiton from background to active state.
		public override void WillEnterForeground (UIApplication application)
		{
		}
		
		// This method is called when the application is about to terminate. Save data, if needed.
		public override void WillTerminate (UIApplication application)
		{
		}
	}
}


using System;
using System.Collections.Generic;
using System.Linq;

#if __UNIFIED__
using Foundation;
using UIKit;
#else
using MonoTouch.Foundation;
using MonoTouch.UIKit;
#endif

using MTiRate;

namespace iRateDemo
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		iRateDemoViewController viewController;

		// Must override the Window property for iRate to work properly
		public override UIWindow Window { get; set; }

		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			Window = new UIWindow (UIScreen.MainScreen.Bounds);

			var rateAlert = iRate.SharedInstance;

			//set the bundle ID. normally you wouldn't need to do this
			//as it is picked up automatically from your Info.plist file
			//but we want to test with an app that's actually on the store
			rateAlert.ApplicationBundleID = "com.charcoaldesign.rainbowblocks-free";
			rateAlert.OnlyPromptIfLatestVersion = false;

			// Subscribe to events
			rateAlert.UserDidAttemptToRateApp += (sender, e) => {
				Console.WriteLine ("User is rating app now!");	
			};

			rateAlert.UserDidDeclineToRateApp += (sender, e) => {
				Console.WriteLine ("User does not want to rate app");
			};

			rateAlert.UserDidRequestReminderToRateApp += (sender, e) => {
				Console.WriteLine ("User will rate app later");
			};

			// Enable preview mode so everytime Application is launched you get the promt
			rateAlert.PreviewMode = true;

			viewController = new iRateDemoViewController ();
			Window.RootViewController = viewController;
			Window.MakeKeyAndVisible ();
			
			return true;
		}
	}
}


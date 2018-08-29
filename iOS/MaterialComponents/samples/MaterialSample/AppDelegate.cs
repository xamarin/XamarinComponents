using System;
using System.Runtime.CompilerServices;

using Foundation;
using UIKit;

using MaterialComponents;

namespace MaterialSample {
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
	[Register ("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate, IAppBarNavigationControllerDelegate, IUINavigationControllerDelegate {
		// class-level declarations

		public override UIWindow Window { get; set; }

		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.LightContent;

			(Window.RootViewController as UINavigationController).Delegate = this;

			return true;
		}

		public static void LogMessage (string message, string className, [CallerMemberName] string methodName = "", [CallerLineNumber] int lineNumber = 0)
		=> Console.WriteLine ($"{className}.{methodName} line {lineNumber}: {message}");

		#region UINavigationController Delegate

		[Export ("navigationController:willShowViewController:animated:")]
		public void WillShowViewController (UINavigationController navigationController, UIViewController viewController, bool animated)
		{
			var btnBack = new UIBarButtonItem (" ", UIBarButtonItemStyle.Plain, null);
			viewController.NavigationItem.BackBarButtonItem = btnBack;
		}

		#endregion

	}
}


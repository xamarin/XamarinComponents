using Foundation;
using UIKit;

using GigyaSDK;

namespace GigyaSDKSampleiOS
{
	[Register("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		public override UIWindow Window { get; set; }

		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{
			// This method initializes the SDK and should always appear in FinishedLaunching
			Gigya.Init("3_maetISj1vIK2f6uWrM1gHaYHOxT-kKw-Y0g6D531hKF_8t74nBgbAHsJI4YUairG", application, launchOptions);

			// This demo project is preconfigured for Facebook, Google+ and Twitter native login.
			// To disable Facebook native login - remove the FacebookAppID and FacebookDisplayName parameters from the app's .plist file.
			// To disable Google+ native login - remove the GooglePlusClientID parameter from the app's .plist file.
			// To disable Twitter native login - add a BOOL parameter named DisableTwitterNativeLogin to the app's .plist file, and set it to YES.

			return true;
		}

		public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
		{
			return Gigya.HandleOpenUrl(url, app, options);
		}

		public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
		{
			return Gigya.HandleOpenUrl(url, application, sourceApplication, annotation);
		}

		public override void OnActivated(UIApplication application)
		{
			Gigya.HandleOnActivated();
		}
	}
}


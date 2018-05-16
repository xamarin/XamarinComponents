using Foundation;
using UIKit;

using VKontakte;

namespace VKontakteSampleiOS
{
	[Register ("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		public const string AppId = "5167570";
		public const string SecureKey = "Tmyw8D2vGdvVcs4Y1Lcz";

		public override UIWindow Window { get; set; }

		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			return true;
		}

		public override bool OpenUrl (UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
		{
			if (VKSdk.ProcessOpenUrl (url, sourceApplication)) {
				return true;
			}

			return base.OpenUrl (application, url, sourceApplication, annotation);
		}
	}
}

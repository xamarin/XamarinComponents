using Foundation;
using UIKit;

namespace AMScrollingNavbarSample
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		public override UIWindow Window { get; set; }

		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			UINavigationBar.Appearance.TintColor = UIColor.White;
			UINavigationBar.Appearance.TitleTextAttributes = new UIStringAttributes {
				ForegroundColor = UIColor.White
			};
			UINavigationBar.Appearance.LargeTitleTextAttributes = new UIStringAttributes {
				ForegroundColor = UIColor.White
			};

			return true;
		}
	}
}

using Foundation;
using UIKit;

namespace SimplePingSample.iOS
{
	[Register(nameof(AppDelegate))]
	public class AppDelegate : UIApplicationDelegate
	{
		public override UIWindow Window { get; set; }

		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{
			return true;
		}
	}
}

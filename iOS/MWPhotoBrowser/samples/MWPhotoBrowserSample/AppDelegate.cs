using Foundation;
using UIKit;

using MWPhotoBrowser;

namespace MWPhotoBrowserSample
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		public override UIWindow Window { get; set; }

		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			return true;
		}
	}
}

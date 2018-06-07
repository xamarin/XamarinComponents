using Foundation;
using UIKit;

namespace ZipArchiveSample
{
	[Register("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		public override UIWindow Window { get; set; }

		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			return true;
		}
	}
}

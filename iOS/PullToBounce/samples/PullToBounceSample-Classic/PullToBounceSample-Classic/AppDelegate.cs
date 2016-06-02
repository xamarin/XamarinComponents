using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace PullToBounceSampleClassic
{
    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        public override UIWindow Window { get; set; }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            return true;
        }
    }
}

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using SidePanels;

namespace SidePanelsClassicSample
{
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        public override UIWindow Window { get; set; }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            return true;
        }
    }
}

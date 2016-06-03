using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace LiquidFloatingActionButtonSampleClassic
{
    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        public override UIWindow Window { get; set; }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            Window = new UIWindow(UIScreen.MainScreen.Bounds);

            Window.RootViewController = new ViewController();
            Window.MakeKeyAndVisible();

            return true;
        }
    }
}

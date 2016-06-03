using Foundation;
using UIKit;

using SidePanels;

namespace SidePanelsCodeSample
{
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        private MainViewController viewController;

        public override UIWindow Window { get; set; }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            viewController = new MainViewController();

            Window = new UIWindow(UIScreen.MainScreen.Bounds);
            Window.RootViewController = viewController;
            Window.MakeKeyAndVisible();

            return true;
        }

    }
}


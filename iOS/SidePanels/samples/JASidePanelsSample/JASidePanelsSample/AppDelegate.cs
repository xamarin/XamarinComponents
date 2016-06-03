using Foundation;
using UIKit;

using JASidePanels;

namespace JASidePanelsSample
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;
		JASidePanelController viewController;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);

			viewController = new JASidePanelController ();
			viewController.ShouldDelegateAutorotateToVisiblePanel = false;

			viewController.LeftPanel = new JALeftViewController ();
			viewController.CenterPanel = new UINavigationController (new JACenterViewController ());
			viewController.RightPanel = new JARightViewController ();

			window.RootViewController = viewController;
			window.MakeKeyAndVisible ();

			return true;
		}
	}
}


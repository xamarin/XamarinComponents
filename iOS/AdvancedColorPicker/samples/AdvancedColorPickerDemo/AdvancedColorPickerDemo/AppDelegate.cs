using Foundation;
using UIKit;

namespace AdvancedColorPickerDemo
{
	[Register(nameof(AppDelegate))]
	public partial class AppDelegate : UIApplicationDelegate
	{
		private ContainerController container;
		private UINavigationController nav;

		public override UIWindow Window { get; set; }

		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			Window = new UIWindow(UIScreen.MainScreen.Bounds);

			container = new ContainerController();
			nav = new UINavigationController(container);

			Window.RootViewController = nav;
			Window.MakeKeyAndVisible();

			return true;
		}
	}
}

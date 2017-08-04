#if __UNIFIED__
using UIKit;
#else
using MonoTouch.UIKit;
#endif

namespace LoginScreen.Utils
{
	static class AutorotateHelper
	{
		public static UIInterfaceOrientationMask GetSupportedInterfaceOrientations ()
		{
			return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad ?
				UIInterfaceOrientationMask.All : UIInterfaceOrientationMask.Portrait;
		}

		public static bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad ||
				toInterfaceOrientation == UIInterfaceOrientation.Portrait;
		}
	}
}


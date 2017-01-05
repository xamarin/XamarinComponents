#if __UNIFIED__
using UIKit;
#else
using MonoTouch.UIKit;
#endif

using LoginScreen.Utils;

namespace LoginScreen.ViewControllers
{
	class CustomNavigationController : UINavigationController
	{
		public CustomNavigationController (UIViewController rootViewController)
			: base(rootViewController)
		{
			NavigationBarHidden = true;
		}

		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations ()
		{
			return AutorotateHelper.GetSupportedInterfaceOrientations ();
		}

#pragma warning disable 0672
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
#pragma warning restore 0672
		{
			return AutorotateHelper.ShouldAutorotateToInterfaceOrientation (toInterfaceOrientation);
		}
	}
}


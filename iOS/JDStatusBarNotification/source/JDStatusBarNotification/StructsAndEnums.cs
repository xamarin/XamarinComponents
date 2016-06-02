#if __UNIFIED__
using ObjCRuntime;
using UIKit;
#else
using MonoTouch.ObjCRuntime;
using MonoTouch.UIKit;
#endif

namespace JDStatusBarNotification
{
	[Native]
	public enum StatusBarAnimationType : long
	{
		None,
		Move,
		Bounce,
		Fade
	}

	[Native]
	public enum StatusBarProgressBarPosition : long
	{
		Bottom,
		Center,
		Top,
		Below,
		NavBar
	}
}

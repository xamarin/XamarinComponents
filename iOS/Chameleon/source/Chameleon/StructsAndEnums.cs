#if __UNIFIED__
using ObjCRuntime;
using UIKit;
#else
using MonoTouch.ObjCRuntime;
using MonoTouch.UIKit;
#endif

namespace ChameleonFramework
{
	[Native]
	public enum ContentStyle : ulong
	{
		Contrast,
		Light,
		Dark
	}

	[Native]
	public enum GradientStyle : ulong
	{
		LeftToRight,
		Radial,
		TopToBottom
	}

	[Native]
	public enum ShadeStyle : long
	{
		Light,
		Dark
	}

	[Native]
	public enum ColorScheme : long
	{
		Analogous,
		Triadic,
		Complementary
	}

	public enum StatusBarStyle : long
	{
		BlackOpaque = UIStatusBarStyle.BlackOpaque,
		BlackTranslucent = UIStatusBarStyle.BlackTranslucent,
		Default = UIStatusBarStyle.Default,
		LightContent = UIStatusBarStyle.LightContent,

		Contrast = 100,
	}
}

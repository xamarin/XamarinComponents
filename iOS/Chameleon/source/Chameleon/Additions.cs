using System;
using System.Runtime.CompilerServices;

#if __UNIFIED__
using ObjCRuntime;
using UIKit;
#else
using MonoTouch.ObjCRuntime;
using MonoTouch.UIKit;
#endif

namespace ChameleonFramework
{
	partial class ChameleonColor
	{
		[CompilerGenerated]
		static readonly IntPtr class_ptr = Class.GetHandle ("UIColor");
	}

	partial class ChameleonColorArray
	{
		[CompilerGenerated]
		static readonly IntPtr class_ptr = Class.GetHandle ("NSArray");
	}

	static partial class UIViewControllerExtensions
	{
		public static void SetStatusBarStyle (this UIViewController This, UIStatusBarStyle statusBarStyle)
		{
			This.SetStatusBarStyle ((StatusBarStyle)statusBarStyle);
		}
	}

	static partial class UINavigationControllerExtensions
	{
		public static void SetStatusBarStyle (this UINavigationController This, UIStatusBarStyle statusBarStyle)
		{
			This.SetStatusBarStyle ((StatusBarStyle)statusBarStyle);
		}
	}
}

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
	partial class ChameleonColorArray
	{
		[CompilerGenerated]
		static readonly IntPtr class_ptr = Class.GetHandle ("NSArray");
	}
}

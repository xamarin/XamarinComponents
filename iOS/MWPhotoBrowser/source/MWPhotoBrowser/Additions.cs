using System;
using System.Runtime.CompilerServices;

#if __UNIFIED__
using Foundation;
using ObjCRuntime;
using UIKit;
#else
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
using MonoTouch.UIKit;
#endif

namespace MWPhotoBrowser
{
//	partial class PhotoBrowserImage
//	{
//		[CompilerGenerated]
//		static readonly IntPtr class_ptr = Class.GetHandle ("UIImage");
//	}

	partial class PhotoBrowserPhoto
	{
		public static PhotoBrowserPhoto FromFilePath (string path)
		{
			return PhotoBrowserPhoto.FromUrl (new NSUrl (path, false));
		}
	}
}

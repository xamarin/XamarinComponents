using System;
using System.Runtime.CompilerServices;
using Foundation;
using ObjCRuntime;
using UIKit;

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

// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace GPUImageSample
{
	[Register ("RawDataViewController")]
	partial class RawDataViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		GPUImage.Outputs.GPUImageView imageView { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (imageView != null) {
				imageView.Dispose ();
				imageView = null;
			}
		}
	}
}

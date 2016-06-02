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
	[Register ("MultipleViewController")]
	partial class MultipleViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		GPUImage.Outputs.GPUImageView bottomLeftImageView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		GPUImage.Outputs.GPUImageView bottomRightImageView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		GPUImage.Outputs.GPUImageView topLeftImageView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		GPUImage.Outputs.GPUImageView topRightImageView { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (bottomLeftImageView != null) {
				bottomLeftImageView.Dispose ();
				bottomLeftImageView = null;
			}
			if (bottomRightImageView != null) {
				bottomRightImageView.Dispose ();
				bottomRightImageView = null;
			}
			if (topLeftImageView != null) {
				topLeftImageView.Dispose ();
				topLeftImageView = null;
			}
			if (topRightImageView != null) {
				topRightImageView.Dispose ();
				topRightImageView = null;
			}
		}
	}
}

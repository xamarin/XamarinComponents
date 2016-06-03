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
	[Register ("CameraPhotoViewController")]
	partial class CameraPhotoViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UISlider imageSlider { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		GPUImage.Outputs.GPUImageView imageView { get; set; }

		[Action ("OnImageSliderChanged:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void OnImageSliderChanged (UISlider sender);

		[Action ("OnTakePhoto:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void OnTakePhoto (UIBarButtonItem sender);

		void ReleaseDesignerOutlets ()
		{
			if (imageSlider != null) {
				imageSlider.Dispose ();
				imageSlider = null;
			}
			if (imageView != null) {
				imageView.Dispose ();
				imageView = null;
			}
		}
	}
}

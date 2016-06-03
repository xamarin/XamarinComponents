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
	[Register ("SimpVideoFilterViewController")]
	partial class SimpVideoFilterViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		GPUImage.Outputs.GPUImageView imageView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UISlider pixelWidthSlider { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel progressLabel { get; set; }

		[Action ("OnPixelWidthChanged:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void OnPixelWidthChanged (UISlider sender);

		void ReleaseDesignerOutlets ()
		{
			if (imageView != null) {
				imageView.Dispose ();
				imageView = null;
			}
			if (pixelWidthSlider != null) {
				pixelWidthSlider.Dispose ();
				pixelWidthSlider = null;
			}
			if (progressLabel != null) {
				progressLabel.Dispose ();
				progressLabel = null;
			}
		}
	}
}

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
	[Register ("SimpleResampleViewController")]
	partial class SimpleResampleViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView imageView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UISegmentedControl resampleSelector { get; set; }

		[Action ("OnResampleSelectorChanged:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void OnResampleSelectorChanged (UISegmentedControl sender);

		void ReleaseDesignerOutlets ()
		{
			if (imageView != null) {
				imageView.Dispose ();
				imageView = null;
			}
			if (resampleSelector != null) {
				resampleSelector.Dispose ();
				resampleSelector = null;
			}
		}
	}
}

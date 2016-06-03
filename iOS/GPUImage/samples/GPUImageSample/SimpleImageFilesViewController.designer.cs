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
	[Register ("SimpleImageFilesViewController")]
	partial class SimpleImageFilesViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UISegmentedControl filteringSelector { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView imageView { get; set; }

		[Action ("OnFilterSelectorChanged:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void OnFilterSelectorChanged (UISegmentedControl sender);

		void ReleaseDesignerOutlets ()
		{
			if (filteringSelector != null) {
				filteringSelector.Dispose ();
				filteringSelector = null;
			}
			if (imageView != null) {
				imageView.Dispose ();
				imageView = null;
			}
		}
	}
}

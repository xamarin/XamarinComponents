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

namespace FXBlurViewSample
{
	[Register ("AnimatedBlurViewController")]
	partial class AnimatedBlurViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		FXBlur.FXBlurView blurView { get; set; }

		[Action ("OnToggleBlur:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void OnToggleBlur (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (blurView != null) {
				blurView.Dispose ();
				blurView = null;
			}
		}
	}
}

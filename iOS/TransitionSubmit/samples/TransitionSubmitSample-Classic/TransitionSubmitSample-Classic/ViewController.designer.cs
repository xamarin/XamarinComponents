// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System;
using System.CodeDom.Compiler;

namespace TransitionSubmitSampleClassic
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView loginBackground { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		AnimatedButtons.TransitionSubmitButton storyboardButton { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (loginBackground != null) {
				loginBackground.Dispose ();
				loginBackground = null;
			}
			if (storyboardButton != null) {
				storyboardButton.Dispose ();
				storyboardButton = null;
			}
		}
	}
}

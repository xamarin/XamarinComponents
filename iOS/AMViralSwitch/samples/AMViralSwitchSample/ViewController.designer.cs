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

namespace AMViralSwitchSample
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel blueLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		AMViralSwitch.ViralSwitch blueSwitch { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		AMViralSwitch.ViralSwitch greenSwitch { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView greenView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton infoButton { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (blueLabel != null) {
				blueLabel.Dispose ();
				blueLabel = null;
			}
			if (blueSwitch != null) {
				blueSwitch.Dispose ();
				blueSwitch = null;
			}
			if (greenSwitch != null) {
				greenSwitch.Dispose ();
				greenSwitch = null;
			}
			if (greenView != null) {
				greenView.Dispose ();
				greenView = null;
			}
			if (infoButton != null) {
				infoButton.Dispose ();
				infoButton = null;
			}
		}
	}
}

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

namespace VideoWidgetDemo
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextView attributionTextView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		Google.VR.GVRVideoView videoView { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (attributionTextView != null) {
				attributionTextView.Dispose ();
				attributionTextView = null;
			}
			if (videoView != null) {
				videoView.Dispose ();
				videoView = null;
			}
		}
	}
}

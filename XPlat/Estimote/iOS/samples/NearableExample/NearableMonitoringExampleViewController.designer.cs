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

namespace NearableMonitoringExample
{
	[Register ("NearableMonitoringExampleViewController")]
	partial class NearableMonitoringExampleViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView BackgroundView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UISwitch enterSwitch { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UISwitch exitSwitch { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel LabelNotification { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextView TextViewInfo { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (BackgroundView != null) {
				BackgroundView.Dispose ();
				BackgroundView = null;
			}
			if (enterSwitch != null) {
				enterSwitch.Dispose ();
				enterSwitch = null;
			}
			if (exitSwitch != null) {
				exitSwitch.Dispose ();
				exitSwitch = null;
			}
			if (LabelNotification != null) {
				LabelNotification.Dispose ();
				LabelNotification = null;
			}
			if (TextViewInfo != null) {
				TextViewInfo.Dispose ();
				TextViewInfo = null;
			}
		}
	}
}

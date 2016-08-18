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

namespace BeaconExample
{
	[Register ("NotificationViewController")]
	partial class NotificationViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UISwitch enterSwitch { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UISwitch exitSwitch { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (enterSwitch != null) {
				enterSwitch.Dispose ();
				enterSwitch = null;
			}
			if (exitSwitch != null) {
				exitSwitch.Dispose ();
				exitSwitch = null;
			}
		}
	}
}

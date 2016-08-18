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

namespace NearableExample
{
	[Register ("TriggersViewController")]
	partial class TriggersViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIPickerView FirstHourSwitch { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UISwitch RemindSwitch { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIPickerView SecondHourSwitch { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (FirstHourSwitch != null) {
				FirstHourSwitch.Dispose ();
				FirstHourSwitch = null;
			}
			if (RemindSwitch != null) {
				RemindSwitch.Dispose ();
				RemindSwitch = null;
			}
			if (SecondHourSwitch != null) {
				SecondHourSwitch.Dispose ();
				SecondHourSwitch = null;
			}
		}
	}
}

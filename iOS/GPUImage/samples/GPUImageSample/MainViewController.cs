using System;
using Foundation;
using UIKit;

namespace GPUImageSample
{
	partial class MainViewController : UITableViewController
	{
		public MainViewController (IntPtr handle)
			: base (handle)
		{
		}

		public override bool ShouldPerformSegue (string segueIdentifier, NSObject sender)
		{
			if (ObjCRuntime.Runtime.Arch == ObjCRuntime.Arch.SIMULATOR)
			if (segueIdentifier == "multipleViews" ||
			    segueIdentifier == "videoCamera" ||
			    segueIdentifier == "stillCamera") {

				new UIAlertView ("Simulator", "The camera is not available on the iOS simulator.", null, "OK").Show ();

				return false;
			}

			return true;
		}
	}
}

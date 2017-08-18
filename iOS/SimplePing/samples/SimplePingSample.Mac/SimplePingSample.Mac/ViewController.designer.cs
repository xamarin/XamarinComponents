// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace SimplePingSample.Mac
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSButton forceIPv4Check { get; set; }

		[Outlet]
		AppKit.NSButton forceIPv6Check { get; set; }

		[Outlet]
		AppKit.NSButton startStopButton { get; set; }

		[Action ("startStopClicked:")]
		partial void startStopClicked (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (forceIPv4Check != null) {
				forceIPv4Check.Dispose ();
				forceIPv4Check = null;
			}

			if (forceIPv6Check != null) {
				forceIPv6Check.Dispose ();
				forceIPv6Check = null;
			}

			if (startStopButton != null) {
				startStopButton.Dispose ();
				startStopButton = null;
			}
		}
	}
}

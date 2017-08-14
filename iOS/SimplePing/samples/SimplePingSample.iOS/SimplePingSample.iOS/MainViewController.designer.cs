// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace SimplePingSample.iOS
{
    [Register ("MainViewController")]
    partial class MainViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableViewCell forceIPv4Cell { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableViewCell forceIPv6Cell { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableViewCell startStopCell { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (forceIPv4Cell != null) {
                forceIPv4Cell.Dispose ();
                forceIPv4Cell = null;
            }

            if (forceIPv6Cell != null) {
                forceIPv6Cell.Dispose ();
                forceIPv6Cell = null;
            }

            if (startStopCell != null) {
                startStopCell.Dispose ();
                startStopCell = null;
            }
        }
    }
}
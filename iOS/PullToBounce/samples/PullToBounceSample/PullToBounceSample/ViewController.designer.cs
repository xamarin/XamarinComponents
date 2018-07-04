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

namespace PullToBounceSample
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        PullToBounceSample.TableView tableView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        PullToBounce.PullToBounceWrapper tableViewWrapper { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView title { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (tableView != null) {
                tableView.Dispose ();
                tableView = null;
            }

            if (tableViewWrapper != null) {
                tableViewWrapper.Dispose ();
                tableViewWrapper = null;
            }

            if (title != null) {
                title.Dispose ();
                title = null;
            }
        }
    }
}
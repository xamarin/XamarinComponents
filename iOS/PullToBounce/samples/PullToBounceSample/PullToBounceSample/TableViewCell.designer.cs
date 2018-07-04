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
    [Register ("TableViewCell")]
    partial class TableViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView line1 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView line2 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView line3 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView picture { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (line1 != null) {
                line1.Dispose ();
                line1 = null;
            }

            if (line2 != null) {
                line2.Dispose ();
                line2 = null;
            }

            if (line3 != null) {
                line3.Dispose ();
                line3 = null;
            }

            if (picture != null) {
                picture.Dispose ();
                picture = null;
            }
        }
    }
}
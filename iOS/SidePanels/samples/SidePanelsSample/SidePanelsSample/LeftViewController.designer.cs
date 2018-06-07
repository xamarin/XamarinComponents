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

namespace SidePanelsSample
{
    [Register ("LeftViewController")]
    partial class LeftViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton switchCenter { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton toggleCenter { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton toggleRight { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (switchCenter != null) {
                switchCenter.Dispose ();
                switchCenter = null;
            }

            if (toggleCenter != null) {
                toggleCenter.Dispose ();
                toggleCenter = null;
            }

            if (toggleRight != null) {
                toggleRight.Dispose ();
                toggleRight = null;
            }
        }
    }
}
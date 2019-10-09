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

namespace VideoSplashSample
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton leftButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton rightButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel text { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (leftButton != null) {
                leftButton.Dispose ();
                leftButton = null;
            }

            if (rightButton != null) {
                rightButton.Dispose ();
                rightButton = null;
            }

            if (text != null) {
                text.Dispose ();
                text = null;
            }
        }
    }
}
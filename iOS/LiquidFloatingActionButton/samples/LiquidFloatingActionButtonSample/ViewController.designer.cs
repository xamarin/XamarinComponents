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

namespace LiquidFloatingActionButtonSample
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        AnimatedButtons.LiquidFloatingActionButton bottomRightButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        AnimatedButtons.LiquidFloatingActionButton topLeftButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (bottomRightButton != null) {
                bottomRightButton.Dispose ();
                bottomRightButton = null;
            }

            if (topLeftButton != null) {
                topLeftButton.Dispose ();
                topLeftButton = null;
            }
        }
    }
}
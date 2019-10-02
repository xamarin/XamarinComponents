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

namespace TransitionSubmitSample
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView loginBackground { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        AnimatedButtons.TransitionSubmitButton storyboardButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (loginBackground != null) {
                loginBackground.Dispose ();
                loginBackground = null;
            }

            if (storyboardButton != null) {
                storyboardButton.Dispose ();
                storyboardButton = null;
            }
        }
    }
}
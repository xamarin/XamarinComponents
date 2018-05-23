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

namespace AnimatedShapeButtonSample
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        AnimatedButtons.AnimatedShapeButton heartButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        AnimatedButtons.AnimatedShapeButton likeButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        AnimatedButtons.AnimatedShapeButton smileButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        AnimatedButtons.AnimatedShapeButton starButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (heartButton != null) {
                heartButton.Dispose ();
                heartButton = null;
            }

            if (likeButton != null) {
                likeButton.Dispose ();
                likeButton = null;
            }

            if (smileButton != null) {
                smileButton.Dispose ();
                smileButton = null;
            }

            if (starButton != null) {
                starButton.Dispose ();
                starButton = null;
            }
        }
    }
}
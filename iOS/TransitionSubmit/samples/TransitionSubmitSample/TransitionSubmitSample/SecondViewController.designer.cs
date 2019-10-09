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
    [Register ("SecondViewController")]
    partial class SecondViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView homeBackground { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (homeBackground != null) {
                homeBackground.Dispose ();
                homeBackground = null;
            }
        }
    }
}
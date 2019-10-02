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

namespace TextSwitchSample
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        AnimatedButtons.TextSwitch runkeeperSwitch4 { get; set; }

        [Action ("OnCompanyChanged:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void OnCompanyChanged (AnimatedButtons.TextSwitch sender);

        void ReleaseDesignerOutlets ()
        {
            if (runkeeperSwitch4 != null) {
                runkeeperSwitch4.Dispose ();
                runkeeperSwitch4 = null;
            }
        }
    }
}
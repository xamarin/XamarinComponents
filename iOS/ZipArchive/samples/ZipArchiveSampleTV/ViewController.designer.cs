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

namespace ZipArchiveSampleTV
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel statusLabel { get; set; }

        [Action ("OnUnzip:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void OnUnzip (UIKit.UIButton sender);

        [Action ("OnZip:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void OnZip (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (statusLabel != null) {
                statusLabel.Dispose ();
                statusLabel = null;
            }
        }
    }
}
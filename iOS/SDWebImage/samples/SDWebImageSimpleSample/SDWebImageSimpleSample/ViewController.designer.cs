// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace SDWebImageSimpleSample
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnDownload { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imageView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblPercent { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIProgressView progressBar { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnDownload != null) {
                btnDownload.Dispose ();
                btnDownload = null;
            }

            if (imageView != null) {
                imageView.Dispose ();
                imageView = null;
            }

            if (lblPercent != null) {
                lblPercent.Dispose ();
                lblPercent = null;
            }

            if (progressBar != null) {
                progressBar.Dispose ();
                progressBar = null;
            }
        }
    }
}
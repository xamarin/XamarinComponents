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

namespace SDWebImageSample
{
    [Register ("DetailViewController")]
    partial class DetailViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIActivityIndicatorView activity { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imageView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIProgressView progress { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (activity != null) {
                activity.Dispose ();
                activity = null;
            }

            if (imageView != null) {
                imageView.Dispose ();
                imageView = null;
            }

            if (progress != null) {
                progress.Dispose ();
                progress = null;
            }
        }
    }
}
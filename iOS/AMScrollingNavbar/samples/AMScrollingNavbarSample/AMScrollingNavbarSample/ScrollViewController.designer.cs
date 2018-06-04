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

namespace AMScrollingNavbarSample
{
    [Register ("ScrollViewController")]
    partial class ScrollViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIScrollView scrollView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (scrollView != null) {
                scrollView.Dispose ();
                scrollView = null;
            }
        }
    }
}
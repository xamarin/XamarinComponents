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
    [Register ("CollectionViewCell")]
    partial class CollectionViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel label { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (label != null) {
                label.Dispose ();
                label = null;
            }
        }
    }
}
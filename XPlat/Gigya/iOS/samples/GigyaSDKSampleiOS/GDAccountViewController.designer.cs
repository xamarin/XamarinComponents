// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace GigyaSDKSampleiOS
{
    [Register ("GDAccountViewController")]
    partial class GDAccountViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel nameLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView profileImageView { get; set; }

        [Action ("AddConnectionPressed:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void AddConnectionPressed (UIKit.UIButton sender);

        [Action ("LogoutPressed:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void LogoutPressed (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (nameLabel != null) {
                nameLabel.Dispose ();
                nameLabel = null;
            }

            if (profileImageView != null) {
                profileImageView.Dispose ();
                profileImageView = null;
            }
        }
    }
}
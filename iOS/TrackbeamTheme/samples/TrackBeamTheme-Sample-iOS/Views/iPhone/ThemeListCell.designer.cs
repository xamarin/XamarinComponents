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

namespace TrackBeamTheme_Sample_iOS.Views.iPhone
{
    [Register ("ThemeListCell")]
    partial class ThemeListCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView albumImageView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel artistLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lengthLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel trackLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (albumImageView != null) {
                albumImageView.Dispose ();
                albumImageView = null;
            }

            if (artistLabel != null) {
                artistLabel.Dispose ();
                artistLabel = null;
            }

            if (lengthLabel != null) {
                lengthLabel.Dispose ();
                lengthLabel = null;
            }

            if (trackLabel != null) {
                trackLabel.Dispose ();
                trackLabel = null;
            }
        }
    }
}
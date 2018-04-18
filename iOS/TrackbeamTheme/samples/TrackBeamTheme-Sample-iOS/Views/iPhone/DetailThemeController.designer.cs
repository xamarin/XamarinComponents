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
    [Register ("DetailThemeController")]
    partial class DetailThemeController
    {
        [Outlet]
        UILabel trackLabel { get; set; }


        [Outlet]
        UIScrollView scrollView { get; set; }


        [Outlet]
        UILabel genreLabel { get; set; }


        [Outlet]
        UIImageView bgImageView { get; set; }


        [Outlet]
        UILabel artistLabel { get; set; }


        [Outlet]
        UIImageView albumImageView { get; set; }

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

            if (trackLabel != null) {
                trackLabel.Dispose ();
                trackLabel = null;
            }
        }
    }
}
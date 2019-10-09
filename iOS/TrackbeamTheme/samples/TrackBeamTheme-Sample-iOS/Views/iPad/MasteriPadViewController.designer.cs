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

namespace TrackBeamTheme_Sample_iOS.Views.iPad
{
    [Register ("MasteriPadViewController")]
    partial class MasteriPadViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView trackImage { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView tracksTable { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (trackImage != null) {
                trackImage.Dispose ();
                trackImage = null;
            }

            if (tracksTable != null) {
                tracksTable.Dispose ();
                tracksTable = null;
            }
        }
    }
}
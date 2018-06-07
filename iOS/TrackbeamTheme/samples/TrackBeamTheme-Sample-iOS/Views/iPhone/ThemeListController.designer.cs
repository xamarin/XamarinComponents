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
    [Register ("ThemeListController")]
    partial class ThemeListController
    {
        [Outlet]
        UITableView tableListView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (tableListView != null) {
                tableListView.Dispose ();
                tableListView = null;
            }
        }
    }
}
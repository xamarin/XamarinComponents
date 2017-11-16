// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using System.CodeDom.Compiler;
using Foundation;
using UIKit;

namespace TrackBeamTheme_Sample_iOS.Views.iPhone
{
	[Register ("ThemeListCell")]
	partial class ThemeListCell
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView albumImageView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel artistLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lengthLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel trackLabel { get; set; }

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

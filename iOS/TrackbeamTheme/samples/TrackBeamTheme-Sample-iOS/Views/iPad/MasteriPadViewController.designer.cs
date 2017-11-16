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

namespace TrackBeamTheme_Sample_iOS.Views.iPad
{
	[Register ("MasteriPadViewController")]
	partial class MasteriPadViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView trackImage { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView tracksTable { get; set; }

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

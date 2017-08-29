// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace YouTubePlayerSample
{
	[Register ("PlaylistViewController")]
	partial class PlaylistViewController
	{
		[Outlet]
		UIKit.UIButton BtnNext { get; set; }

		[Outlet]
		UIKit.UIButton BtnPlayPause { get; set; }

		[Outlet]
		UIKit.UIButton BtnPrevious { get; set; }

		[Outlet]
		UIKit.UIButton BtnStop { get; set; }

		[Outlet]
		YouTube.Player.PlayerView PlayerView { get; set; }

		[Outlet]
		UIKit.UITableView TblVideos { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (PlayerView != null) {
				PlayerView.Dispose ();
				PlayerView = null;
			}

			if (BtnPrevious != null) {
				BtnPrevious.Dispose ();
				BtnPrevious = null;
			}

			if (BtnPlayPause != null) {
				BtnPlayPause.Dispose ();
				BtnPlayPause = null;
			}

			if (BtnStop != null) {
				BtnStop.Dispose ();
				BtnStop = null;
			}

			if (BtnNext != null) {
				BtnNext.Dispose ();
				BtnNext = null;
			}

			if (TblVideos != null) {
				TblVideos.Dispose ();
				TblVideos = null;
			}
		}
	}
}

using System;
using Foundation;
using UIKit;

namespace YouTubePlayerSample
{
	public partial class PlaylistViewController : UIViewController
	{
		public PlaylistViewController(IntPtr handle)
			: base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var playlistId = @"PLhBgTdAWkxeCMHYCQ0uuLyhydRJGDRNo5";

			// For a full list of player parameters, see the documentation for the HTML5 player
			// at: https://developers.google.com/youtube/player_parameters?playerVersion=HTML5
			var playerVars = new NSMutableDictionary();
			playerVars["controls"] = (NSNumber)0;
			playerVars["playsinline"] = (NSNumber)1;
			playerVars["autohide"] = (NSNumber)1;
			playerVars["showinfo"] = (NSNumber)0;
			playerVars["modestbranding"] = (NSNumber)1;

			playerView.LoadWithPlaylistId(playlistId, playerVars);
		}

		partial void buttonPressed(UIButton sender)
		{
			if (sender == playButton)
			{
				playerView.PlayVideo();
			}
			else if (sender == pauseButton)
			{
				playerView.PauseVideo();
			}
			else if (sender == stopButton)
			{
				playerView.StopVideo();
			}
			else if (sender == nextVideoButton)
			{
				AppendStatusText("Loading next video in playlist\n");
				playerView.NextVideo();
			}
			else if (sender == previousVideoButton)
			{
				AppendStatusText("Loading previous video in playlist\n");
				playerView.PreviousVideo();
			}
		}

		private void AppendStatusText(string status)
		{
			statusTextView.Text += status;
			var range = new NSRange(statusTextView.Text.Length - 1, 1);

			// To avoid dizzying scrolling on appending latest status.
			statusTextView.ScrollEnabled = false;
			statusTextView.ScrollRangeToVisible(range);
			statusTextView.ScrollEnabled = true;
		}
	}
}

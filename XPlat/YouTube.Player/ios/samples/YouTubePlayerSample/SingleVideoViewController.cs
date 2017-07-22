using System;
using Foundation;
using UIKit;

namespace YouTubePlayerSample
{
	public partial class SingleVideoViewController : UIViewController
	{
		public SingleVideoViewController(IntPtr handle)
			: base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			playerView.DidPlayTime += (sender, e)
				=> slider.Value = (float)(e.PlayTime / playerView.Duration);

			playerView.DidChangeState += (sender, e)
				=> AppendStatusText($"Player state changed: {e.State}\n");

			var videoId = "M7lc1UVf-VE";

			// For a full list of player parameters, see the documentation for the HTML5 player
			// at: https://developers.google.com/youtube/player_parameters?playerVersion=HTML5
			var playerVars = new NSMutableDictionary();
			playerVars["controls"] = (NSNumber)0;
			playerVars["playsinline"] = (NSNumber)1;
			playerVars["autohide"] = (NSNumber)1;
			playerVars["showinfo"] = (NSNumber)0;
			playerVars["modestbranding"] = (NSNumber)1;

			playerView.LoadWithVideoId(videoId, playerVars);
		}

		partial void onSliderChange(UISlider sender)
		{
			var seekToTime = playerView.Duration * slider.Value;
			playerView.SeekTo((float)seekToTime, true);
			AppendStatusText($"Seeking to time: {seekToTime:0.0} seconds\n");
		}

		partial void buttonPressed(UIButton sender)
		{
			if (sender == playButton)
			{
				playerView.PlayVideo();
			}
			else if (sender == stopButton)
			{
				playerView.StopVideo();
			}
			else if (sender == pauseButton)
			{
				playerView.PauseVideo();
			}
			//else if (sender == reverseButton)
			//{
			//	var seekToTime = playerView.CurrentTime - 30.0;
			//	playerView.SeekTo(seekToTime, true);
			//	AppendStatusText($"Seeking to time: {seekToTime:0.0} seconds\n");
			//}
			//else if (sender == forwardButton)
			//{
			//	var seekToTime = playerView.CurrentTime + 30.0;
			//	playerView.SeekTo(seekToTime, true);
			//	AppendStatusText($"Seeking to time: {seekToTime:0,0} seconds\n");
			//}
			//else if (sender == startButton)
			//{
			//	playerView.SeekTo(0, true);
			//	AppendStatusText("Seeking to beginning\n");
			//}
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

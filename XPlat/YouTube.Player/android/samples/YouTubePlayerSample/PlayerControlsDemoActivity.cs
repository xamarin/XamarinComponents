using System;
using System.Text;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;

using YouTube.Player;

namespace YouTubePlayerSample
{
	// A simple YouTube Android API demo application demonstrating the use of IYouTubePlayer
	// programmatic controls.
	[Activity(
		Label = "@string/player_controls_demo_name",
		WindowSoftInputMode = SoftInput.StateHidden)]
	[MetaData("@string/minVersion", Value = "8")]
	[MetaData("@string/isLaunchableActivity", Value = "true")]
	public class PlayerControlsDemoActivity : YouTubeFailureRecoveryActivity,
		IYouTubePlayerPlayerStateChangeListener,
		IYouTubePlayerPlaybackEventListener,
		IYouTubePlayerPlaylistEventListener
	{
		private static readonly ListEntry[] Entries = {
			new ListEntry("Androidify App", "irH3OSOskcE", false),
			new ListEntry("Chrome Speed Tests", "nCgQDjiotG0", false),
			new ListEntry("Playlist: Google I/O 2012", "PL56D792A831D0C362", true)
		};

		private const string KeyCurrentlySelectedId = "currentlySelectedId";

		private YouTubePlayerView youTubePlayerView;
		private IYouTubePlayer player;
		private TextView stateText;
		private ArrayAdapter<ListEntry> videoAdapter;
		private Spinner videoChooser;
		private Button playButton;
		private Button pauseButton;
		private EditText skipTo;
		private TextView eventLog;
		private StringBuilder logString;
		private RadioGroup styleRadioGroup;

		private int currentlySelectedPosition;
		private string currentlySelectedId;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.player_controls_demo);

			youTubePlayerView = FindViewById<YouTubePlayerView>(Resource.Id.youtube_view);
			stateText = FindViewById<TextView>(Resource.Id.state_text);
			videoChooser = FindViewById<Spinner>(Resource.Id.video_chooser);
			playButton = FindViewById<Button>(Resource.Id.play_button);
			pauseButton = FindViewById<Button>(Resource.Id.pause_button);
			skipTo = FindViewById<EditText>(Resource.Id.skip_to_text);
			eventLog = FindViewById<TextView>(Resource.Id.event_log);

			styleRadioGroup = (RadioGroup)FindViewById(Resource.Id.style_radio_group);
			FindViewById<RadioButton>(Resource.Id.style_default).CheckedChange += (sender, e) => player.SetPlayerStyle(YouTubePlayerPlayerStyle.Default);
			FindViewById<RadioButton>(Resource.Id.style_minimal).CheckedChange += (sender, e) => player.SetPlayerStyle(YouTubePlayerPlayerStyle.Minimal);
			FindViewById<RadioButton>(Resource.Id.style_chromeless).CheckedChange += (sender, e) => player.SetPlayerStyle(YouTubePlayerPlayerStyle.Chromeless);
			logString = new StringBuilder();

			videoAdapter = new ArrayAdapter<ListEntry>(this, Android.Resource.Layout.SimpleSpinnerItem, Entries);
			videoAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
			videoChooser.ItemSelected += (sender, e) =>
			{
				currentlySelectedPosition = e.Position;
				PlayVideoAtSelection();
			};
			videoChooser.Adapter = videoAdapter;

			playButton.Click += (sender, e) => player.Play();
			pauseButton.Click += (sender, e) => player.Pause();
			skipTo.EditorAction += (sender, e) =>
			{
				int skipToSecs = ParseInt(skipTo.Text, 0);
				player.SeekToMillis(skipToSecs * 1000);
				var imm = InputMethodManager.FromContext(this);
				imm.HideSoftInputFromWindow(skipTo.WindowToken, 0);
				e.Handled = true;
			};

			youTubePlayerView.Initialize(DeveloperKey.Key, this);

			SetControlsEnabled(false);
		}

		public override void OnInitializationSuccess(IYouTubePlayerProvider provider, IYouTubePlayer player, bool wasRestored)
		{
			this.player = player;

			player.SetPlaylistEventListener(this);
			player.SetPlayerStateChangeListener(this);
			player.SetPlaybackEventListener(this);

			if (!wasRestored)
			{
				PlayVideoAtSelection();
			}
			SetControlsEnabled(true);
		}

		protected override IYouTubePlayerProvider YouTubePlayerProvider => youTubePlayerView;

		private void PlayVideoAtSelection()
		{
			var selectedEntry = videoAdapter.GetItem(currentlySelectedPosition);
			if (selectedEntry.Id != currentlySelectedId && player != null)
			{
				currentlySelectedId = selectedEntry.Id;
				if (selectedEntry.IsPlaylist)
				{
					player.CuePlaylist(selectedEntry.Id);
				}
				else
				{
					player.CueVideo(selectedEntry.Id);
				}
			}
		}

		protected override void OnSaveInstanceState(Bundle outState)
		{
			base.OnSaveInstanceState(outState);
			outState.PutString(KeyCurrentlySelectedId, currentlySelectedId);
		}

		protected override void OnRestoreInstanceState(Bundle savedInstanceState)
		{
			base.OnRestoreInstanceState(savedInstanceState);
			currentlySelectedId = savedInstanceState.GetString(KeyCurrentlySelectedId);
		}

		private void UpdateText()
		{
			stateText.Text = $"Current state: {playerState} {playbackState} {bufferingState}";
		}

		private void Log(String message)
		{
			logString.Append(message + "\n");
			eventLog.Text = logString.ToString();
		}

		private void SetControlsEnabled(bool enabled)
		{
			playButton.Enabled = enabled;
			pauseButton.Enabled = enabled;
			skipTo.Enabled = enabled;
			videoChooser.Enabled = enabled;
			for (int i = 0; i < styleRadioGroup.ChildCount; i++)
			{
				styleRadioGroup.GetChildAt(i).Enabled = enabled;
			}
		}

		private int ParseInt(String text, int defaultValue)
		{
			if (int.TryParse(text, out int result))
			{
				return result;
			}
			return defaultValue;
		}

		private string FormatTime(int millis)
		{
			int seconds = millis / 1000;
			int minutes = seconds / 60;
			int hours = minutes / 60;

			return (hours == 0 ? "" : hours + ":") + $"{(minutes % 60):00}:{(seconds % 60):00}";
		}

		private string TimesText
			=> $"({FormatTime(player.CurrentTimeMillis)}/{FormatTime(player.DurationMillis)})";

		void IYouTubePlayerPlaylistEventListener.OnNext()
		{
			Log("NEXT VIDEO");
		}

		void IYouTubePlayerPlaylistEventListener.OnPlaylistEnded()
		{
			Log("PLAYLIST ENDED");
		}

		void IYouTubePlayerPlaylistEventListener.OnPrevious()
		{
			Log("PREVIOUS VIDEO");
		}

		private string playbackState = "NOT_PLAYING";
		private string bufferingState = "";

		void IYouTubePlayerPlaybackEventListener.OnBuffering(bool isBuffering)
		{
			bufferingState = isBuffering ? "(BUFFERING)" : "";

			UpdateText();
			Log("\t\t" + (isBuffering ? "BUFFERING " : "NOT BUFFERING ") + TimesText);
		}

		void IYouTubePlayerPlaybackEventListener.OnPaused()
		{
			playbackState = "PAUSED";

			UpdateText();
			Log("\tPAUSED " + TimesText);
		}

		void IYouTubePlayerPlaybackEventListener.OnPlaying()
		{
			playbackState = "PLAYING";

			UpdateText();
			Log("\tPLAYING " + TimesText);
		}

		void IYouTubePlayerPlaybackEventListener.OnSeekTo(int endPositionMillis)
		{
			Log($"\tSEEKTO: ({FormatTime(endPositionMillis)}/{FormatTime(player.DurationMillis)})");
		}

		void IYouTubePlayerPlaybackEventListener.OnStopped()
		{
			playbackState = "STOPPED";

			UpdateText();
			Log("\tSTOPPED");
		}

		private string playerState = "UNINITIALIZED";

		void IYouTubePlayerPlayerStateChangeListener.OnAdStarted()
		{
			playerState = "AD_STARTED";

			UpdateText();
			Log(playerState);
		}

		void IYouTubePlayerPlayerStateChangeListener.OnError(YouTubePlayerErrorReason reason)
		{
			playerState = $"ERROR ({reason})";

			if (reason == YouTubePlayerErrorReason.UnexpectedServiceDisconnection)
			{
				// When this error occurs the player is released and can no longer be used.
				player = null;
				SetControlsEnabled(false);
			}

			UpdateText();
			Log(playerState);
		}

		void IYouTubePlayerPlayerStateChangeListener.OnLoaded(string videoId)
		{
			playerState = "LOADED " + videoId;

			UpdateText();
			Log(playerState);
		}

		void IYouTubePlayerPlayerStateChangeListener.OnLoading()
		{
			playerState = "LOADING";

			UpdateText();
			Log(playerState);
		}

		void IYouTubePlayerPlayerStateChangeListener.OnVideoEnded()
		{
			playerState = "VIDEO_ENDED";

			UpdateText();
			Log(playerState);
		}

		void IYouTubePlayerPlayerStateChangeListener.OnVideoStarted()
		{
			playerState = "VIDEO_STARTED";

			UpdateText();
			Log(playerState);
		}

		private class ListEntry
		{
			public ListEntry(string title, string videoId, bool isPlaylist)
			{
				Title = title;
				Id = videoId;
				IsPlaylist = isPlaylist;
			}

			public string Title { get; private set; }
			public string Id { get; private set; }
			public bool IsPlaylist { get; private set; }

			public override string ToString() => Title;
		}
	}
}

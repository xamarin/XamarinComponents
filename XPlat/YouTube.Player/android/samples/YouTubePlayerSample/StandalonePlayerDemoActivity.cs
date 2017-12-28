using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

using YouTube.Player;

namespace YouTubePlayerSample
{
	// A simple YouTube Android API demo application which shows how to use a
	// YouTubeStandalonePlayer intent to start a YouTube video playback.
	[Activity(Label = "@string/standalone_player_demo_name")]
	[MetaData("@string/minVersion", Value = "8")]
	[MetaData("@string/isLaunchableActivity", Value = "true")]
	public class StandalonePlayerDemoActivity : Activity
	{
		private const int StartStandalonePlayerRequest = 1;
		private const int ResolveServiceMissingRequest = 2;

		private const string VideoId = "cdgQpa1pUUE";
		private const string PlaylistId = "7E952A67F31C58A3";
		private static readonly string[] VideoIds = { "cdgQpa1pUUE", "8aCYZ3gXfy8", "zMabEyrtPRg" };

		private Button playVideoButton;
		private Button playPlaylistButton;
		private Button playVideoListButton;

		private EditText startIndexEditText;
		private EditText startTimeEditText;
		private CheckBox autoplayCheckBox;
		private CheckBox lightboxModeCheckBox;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.standalone_player_demo);

			playVideoButton = FindViewById<Button>(Resource.Id.start_video_button);
			playPlaylistButton = FindViewById<Button>(Resource.Id.start_playlist_button);
			playVideoListButton = FindViewById<Button>(Resource.Id.start_video_list_button);
			startIndexEditText = FindViewById<EditText>(Resource.Id.start_index_text);
			startTimeEditText = FindViewById<EditText>(Resource.Id.start_time_text);
			autoplayCheckBox = FindViewById<CheckBox>(Resource.Id.autoplay_checkbox);
			lightboxModeCheckBox = FindViewById<CheckBox>(Resource.Id.lightbox_checkbox);

			playVideoButton.Click += OnClick;
			playPlaylistButton.Click += OnClick;
			playVideoListButton.Click += OnClick;
		}

		private void OnClick(object sender, EventArgs e)
		{
			int startIndex = ParseInt(startIndexEditText.Text, 0);
			int startTimeMillis = ParseInt(startTimeEditText.Text, 0) * 1000;
			bool autoplay = autoplayCheckBox.Checked;
			bool lightboxMode = lightboxModeCheckBox.Checked;

			Intent intent = null;
			if (sender == playVideoButton)
			{
				intent = YouTubeStandalonePlayer.CreateVideoIntent(this, DeveloperKey.Key, VideoId, startTimeMillis, autoplay, lightboxMode);
			}
			else if (sender == playPlaylistButton)
			{
				intent = YouTubeStandalonePlayer.CreatePlaylistIntent(this, DeveloperKey.Key, PlaylistId, startIndex, startTimeMillis, autoplay, lightboxMode);
			}
			else if (sender == playVideoListButton)
			{
				intent = YouTubeStandalonePlayer.CreateVideosIntent(this, DeveloperKey.Key, VideoIds, startIndex, startTimeMillis, autoplay, lightboxMode);
			}

			if (intent != null)
			{
				if (CanResolveIntent(intent))
				{
					StartActivityForResult(intent, StartStandalonePlayerRequest);
				}
				else
				{
					// Could not resolve the intent - must need to install or update the YouTube API service.
					YouTubeInitializationResult.ServiceMissing.GetErrorDialog(this, ResolveServiceMissingRequest).Show();
				}
			}
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);

			if (requestCode == StartStandalonePlayerRequest && resultCode != Result.Ok)
			{
				var errorReason = YouTubeStandalonePlayer.GetReturnedInitializationResult(data);
				if (errorReason.IsUserRecoverableError)
				{
					errorReason.GetErrorDialog(this, 0).Show();
				}
				else
				{
					var errorMessage = string.Format(GetString(Resource.String.error_player), errorReason);
					Toast.MakeText(this, errorMessage, ToastLength.Long).Show();
				}
			}
		}

		private bool CanResolveIntent(Intent intent)
		{
			var resolveInfo = PackageManager.QueryIntentActivities(intent, 0);
			return resolveInfo != null && resolveInfo.Count > 0;
		}

		private int ParseInt(String text, int defaultValue)
		{
			if (int.TryParse(text, out int result))
			{
				return result;
			}
			return defaultValue;
		}
	}
}

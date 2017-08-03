using System;
using Android.App;
using Android.Content.PM;
using Android.OS;

using YouTube.Player;

namespace YouTubePlayerSample
{
	// A simple YouTube Android API demo application which shows how to create a simple application that
	// shows a YouTube Video in a YouTubePlayerFragment.
	// 
	// Note, this sample app extends from YouTubeFailureRecoveryActivity to handle errors, which
	// itself extends YouTubeBaseActivity. However, you are not required to extend
	// YouTubeBaseActivity if using YouTubePlayerFragments.
	[Activity(
		Label = "@string/fragment_demo_name",
		ScreenOrientation = ScreenOrientation.Nosensor,
		ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.KeyboardHidden)]
	[MetaData("@string/minVersion", Value = "11")]
	[MetaData("@string/isLaunchableActivity", Value = "true")]
	public class FragmentDemoActivity : YouTubeFailureRecoveryActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.fragments_demo);

			var fragment = FragmentManager.FindFragmentById<YouTubePlayerFragment>(Resource.Id.youtube_fragment);
			fragment.Initialize(DeveloperKey.Key, this);
		}

		public override void OnInitializationSuccess(IYouTubePlayerProvider provider, IYouTubePlayer player, bool wasRestored)
		{
			if (!wasRestored)
			{
				player.CueVideo("nCgQDjiotG0");
			}
		}

		protected override IYouTubePlayerProvider YouTubePlayerProvider
			=> FragmentManager.FindFragmentById<YouTubePlayerFragment>(Resource.Id.youtube_fragment);
	}
}

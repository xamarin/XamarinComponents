using Android.App;
using Android.Content.PM;
using Android.OS;

using YouTube.Player;

namespace YouTubePlayerSample
{
	// A simple YouTube Android API demo application which shows how to create a simple application that
	// displays a YouTube Video in a YouTubePlayerView.
	// 
	// Note, to use a YouTubePlayerView, your activity must extend YouTubeBaseActivity.
	[Activity(
		Label = "@string/playerview_demo_name",
		ScreenOrientation = ScreenOrientation.Nosensor,
		ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.KeyboardHidden)]
	[MetaData("@string/minVersion", Value = "8")]
	[MetaData("@string/isLaunchableActivity", Value = "true")]
	public class PlayerViewDemoActivity : YouTubeFailureRecoveryActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.playerview_demo);

			var youTubeView = FindViewById<YouTubePlayerView>(Resource.Id.youtube_view);
			youTubeView.Initialize(DeveloperKey.Key, this);
		}

		public override void OnInitializationSuccess(IYouTubePlayerProvider provider, IYouTubePlayer player, bool wasRestored)
		{
			if (!wasRestored)
			{
				player.CueVideo("wKJ9KzGQq0w");
			}
		}

		protected override IYouTubePlayerProvider YouTubePlayerProvider
			=> FindViewById<YouTubePlayerView>(Resource.Id.youtube_view);
	}
}

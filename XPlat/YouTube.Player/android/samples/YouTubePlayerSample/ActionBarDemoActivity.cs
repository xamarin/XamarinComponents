using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

using YouTube.Player;

namespace YouTubePlayerSample
{
	// A sample showing how to use the ActionBar as an overlay when the video is playing in fullscreen.
	//
	// The ActionBar is the only view allowed to overlay the player, so it is a useful place to put
	// custom application controls when the video is in fullscreen. The ActionBar can not change back
	// and forth between normal mode and overlay mode, so to make sure our application's content
	// is not covered by the ActionBar we want to pad our root view when we are not in fullscreen.
	[Activity(
		Label = "@string/action_bar_demo_name",
		Theme = "@style/OverlayActionBarTheme",
		ScreenOrientation = ScreenOrientation.SensorLandscape,
		ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.KeyboardHidden)]
	[MetaData("@string/minVersion", Value = "11")]
	[MetaData("@string/isLaunchableActivity", Value = "true")]
	[Register("com.examples.youtubeapidemo.ActionBarDemoActivity")]
	public class ActionBarDemoActivity : YouTubeFailureRecoveryActivity, IYouTubePlayerOnFullscreenListener
	{
		private ActionBarPaddedFrameLayout viewContainer;
		private YouTubePlayerFragment playerFragment;
		private View tutorialTextView;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.action_bar_demo);

			viewContainer = FindViewById<ActionBarPaddedFrameLayout>(Resource.Id.view_container);
			playerFragment = FragmentManager.FindFragmentById<YouTubePlayerFragment>(Resource.Id.player_fragment);
			tutorialTextView = FindViewById(Resource.Id.tutorial_text);
			playerFragment.Initialize(DeveloperKey.Key, this);
			viewContainer.SetActionBar(ActionBar);

			// Action bar background is transparent by default.
			var trans = Color.Black;
			trans.A = 0xAA;
			ActionBar.SetBackgroundDrawable(new ColorDrawable(trans));
		}

		public override void OnInitializationSuccess(IYouTubePlayerProvider provider, IYouTubePlayer player, bool wasRestored)
		{
			player.AddFullscreenControlFlag(YouTubePlayer.FullscreenFlagCustomLayout);
			player.SetOnFullscreenListener(this);

			if (!wasRestored)
			{
				player.CueVideo("9c6W4CCU9M4");
			}
		}

		void IYouTubePlayerOnFullscreenListener.OnFullscreen(bool fullscreen)
		{
			viewContainer.SetEnablePadding(!fullscreen);

			var playerParams = playerFragment.View.LayoutParameters;
			if (fullscreen)
			{
				tutorialTextView.Visibility = ViewStates.Gone;
				playerParams.Width = ViewGroup.LayoutParams.MatchParent;
				playerParams.Height = ViewGroup.LayoutParams.MatchParent;
			}
			else
			{
				tutorialTextView.Visibility = ViewStates.Visible;
				playerParams.Width = 0;
				playerParams.Height = ViewGroup.LayoutParams.WrapContent;
			}
		}

		protected override IYouTubePlayerProvider YouTubePlayerProvider
			=> FragmentManager.FindFragmentById<YouTubePlayerFragment>(Resource.Id.player_fragment);

		// This is a FrameLayout which adds top-padding equal to the height of the ActionBar unless
		// disabled by SetEnablePadding
		public class ActionBarPaddedFrameLayout : FrameLayout
		{
			private ActionBar actionBar;
			private bool paddingEnabled;

			public ActionBarPaddedFrameLayout(Context context)
				: base(context)
			{
			}

			public ActionBarPaddedFrameLayout(Context context, IAttributeSet attrs)
				: base(context, attrs, 0)
			{
			}

			public ActionBarPaddedFrameLayout(Context context, IAttributeSet attrs, int defStyle)
				: base(context, attrs, defStyle)
			{
				paddingEnabled = true;
			}

			public void SetActionBar(ActionBar actionBar)
			{
				this.actionBar = actionBar;
				RequestLayout();
			}

			public void SetEnablePadding(bool enable)
			{
				paddingEnabled = enable;
				RequestLayout();
			}

			protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
			{
				int topPadding = paddingEnabled && actionBar != null && actionBar.IsShowing ? actionBar.Height : 0;
				SetPadding(0, topPadding, 0, 0);

				base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
			}
		}
	}
}

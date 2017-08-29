using System;
using System.Collections.Generic;
using Android.Animation;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using YouTube.Player;

namespace YouTubePlayerSample
{
	// A sample Activity showing how to manage multiple YouTubeThumbnailViews in an adapter for display
	// in a List. When the list items are clicked, the video is played by using a YouTubePlayerFragment.
	// 
	// The demo supports custom fullscreen and transitioning between portrait and landscape without
	// rebuffering.
	[Activity(
		Label = "@string/videolist_demo_name",
		ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.KeyboardHidden)]
	[MetaData("@string/minVersion", Value = "13")]
	[MetaData("@string/isLaunchableActivity", Value = "true")]
	[Register("com.examples.youtubeapidemo.VideoListDemoActivity")]
	public class VideoListDemoActivity : Activity, IYouTubePlayerOnFullscreenListener
	{
		// The duration of the animation sliding up the video in portrait.
		private const int AnimationDuration = 300;
		// The padding between the video list and the video in landscape orientation.
		private const int LandscapeVideoPadding = 5;

		// The request code when calling startActivityForResult to recover from an API service error.
		private const int RecoveryDialogRequest = 1;

		private VideoListFragment listFragment;
		private VideoFragment videoFragment;

		private View videoBox;
		private View closeButton;

		private bool isFullscreen;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.video_list_demo);

			listFragment = FragmentManager.FindFragmentById<VideoListFragment>(Resource.Id.list_fragment);
			videoFragment = FragmentManager.FindFragmentById<VideoFragment>(Resource.Id.video_fragment_container);

			videoBox = FindViewById(Resource.Id.video_box);
			closeButton = FindViewById(Resource.Id.close_button);
			closeButton.Click += OnClickClose;

			videoBox.Visibility = ViewStates.Invisible;

			Layout();

			CheckYouTubeApi();
		}

		private void CheckYouTubeApi()
		{
			var errorReason = YouTubeApiServiceUtil.IsYouTubeApiServiceAvailable(this);
			if (errorReason.IsUserRecoverableError)
			{
				errorReason.GetErrorDialog(this, RecoveryDialogRequest).Show();
			}
			else if (errorReason != YouTubeInitializationResult.Success)
			{
				var errorMessage = string.Format(GetString(Resource.String.error_player), errorReason);
				Toast.MakeText(this, errorMessage, ToastLength.Long).Show();
			}
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			if (requestCode == RecoveryDialogRequest)
			{
				// Recreate the activity if user performed a recovery action
				Recreate();
			}
		}

		public override void OnConfigurationChanged(Android.Content.Res.Configuration newConfig)
		{
			base.OnConfigurationChanged(newConfig);
			Layout();
		}


		void IYouTubePlayerOnFullscreenListener.OnFullscreen(bool isFullscreen)
		{
			this.isFullscreen = isFullscreen;
			Layout();
		}

		// Sets up the layout programatically for the three different states. Portrait, landscape or
		// fullscreen+landscape. This has to be done programmatically because we handle the orientation
		// changes ourselves in order to get fluent fullscreen transitions, so the xml layout resources
		// do not get reloaded.
		private void Layout()
		{
			var isPortrait = Resources.Configuration.Orientation == Android.Content.Res.Orientation.Portrait;

			listFragment.View.Visibility = isFullscreen ? ViewStates.Gone : ViewStates.Visible;
			listFragment.SetLabelVisibility(isPortrait);
			closeButton.Visibility = isPortrait ? ViewStates.Visible : ViewStates.Gone;

			if (isFullscreen)
			{
				videoBox.TranslationY = 0; // Reset any translation that was applied in portrait.
				SetLayoutSize(videoFragment.View, ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
				SetLayoutSizeAndGravity(videoBox, ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent, GravityFlags.Top | GravityFlags.Left);
			}
			else if (isPortrait)
			{
				SetLayoutSize(listFragment.View, ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
				SetLayoutSize(videoFragment.View, ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
				SetLayoutSizeAndGravity(videoBox, ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent, GravityFlags.Bottom);
			}
			else
			{
				videoBox.TranslationY = 0; // Reset any translation that was applied in portrait.
				int screenWidth = DpToPx(Resources.Configuration.ScreenWidthDp);
				SetLayoutSize(listFragment.View, screenWidth / 4, ViewGroup.LayoutParams.MatchParent);
				int videoWidth = screenWidth - screenWidth / 4 - DpToPx(LandscapeVideoPadding);
				SetLayoutSize(videoFragment.View, videoWidth, ViewGroup.LayoutParams.WrapContent);
				SetLayoutSizeAndGravity(videoBox, videoWidth, ViewGroup.LayoutParams.WrapContent, GravityFlags.Right | GravityFlags.CenterVertical);
			}
		}

		private void OnClickClose(object sender, EventArgs e)
		{
			listFragment.ListView.ClearChoices();
			listFragment.ListView.RequestLayout();
			videoFragment.Pause();

			var animator = videoBox
				.Animate()
				.TranslationYBy(videoBox.Height)
				.SetDuration(AnimationDuration);
			RunOnAnimationEnd(animator, () => videoBox.Visibility = ViewStates.Invisible);
		}

		private void RunOnAnimationEnd(ViewPropertyAnimator animator, Action runnable)
		{
			if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBean)
			{
				animator.WithEndAction(new Java.Lang.Runnable(runnable));
			}
			else
			{
				animator.SetListener(new Listener(runnable));
			}
		}

		private class Listener : Java.Lang.Object, Animator.IAnimatorListener
		{
			private Action runnable;

			public Listener(Action runnable)
			{
				this.runnable = runnable;
			}

			public void OnAnimationCancel(Animator animation) { }

			public void OnAnimationEnd(Animator animation)
			{
				runnable?.Invoke();
			}

			public void OnAnimationRepeat(Animator animation) { }

			public void OnAnimationStart(Animator animation) { }
		}

		// A fragment that shows a static list of videos.
		public class VideoListFragment : ListFragment
		{
			private static readonly List<VideoEntry> VideoList = new List<VideoEntry> {
				new VideoEntry("YouTube Collection", "Y_UmWdcTrrc"),
				new VideoEntry("GMail Tap", "1KhZKNZO8mQ"),
				new VideoEntry("Chrome Multitask", "UiLSiqyDf4Y"),
				new VideoEntry("Google Fiber", "re0VRK6ouwI"),
				new VideoEntry("Autocompleter", "blB_X38YSxQ"),
				new VideoEntry("GMail Motion", "Bu927_ul_X0"),
				new VideoEntry("Translate for Animals", "3I24bSteJpw"),
			};

			private PageAdapter adapter;
			private View videoBox;

			public override void OnCreate(Bundle savedInstanceState)
			{
				base.OnCreate(savedInstanceState);
				adapter = new PageAdapter(Activity, VideoList);
			}

			public override void OnActivityCreated(Bundle savedInstanceState)
			{
				base.OnActivityCreated(savedInstanceState);

				videoBox = Activity.FindViewById(Resource.Id.video_box);
				ListView.ChoiceMode = ChoiceMode.Single;
				ListAdapter = adapter;
			}

			public override void OnListItemClick(ListView l, View v, int position, long id)
			{
				var videoId = VideoList[position].VideoId;

				var videoFragment = FragmentManager.FindFragmentById<VideoFragment>(Resource.Id.video_fragment_container);
				videoFragment.SetVideoId(videoId);

				// The videoBox is INVISIBLE if no video was previously selected, so we need to show it now.
				if (videoBox.Visibility != ViewStates.Visible)
				{
					if (Resources.Configuration.Orientation == Android.Content.Res.Orientation.Portrait)
					{
						// Initially translate off the screen so that it can be animated in from below.
						videoBox.TranslationY = videoBox.Height;
					}
					videoBox.Visibility = ViewStates.Visible;
				}

				// If the fragment is off the screen, we animate it in.
				if (videoBox.TranslationY > 0)
				{
					videoBox.Animate().TranslationY(0).SetDuration(AnimationDuration);
				}
			}

			public override void OnDestroyView()
			{
				base.OnDestroyView();

				adapter.ReleaseLoaders();
			}

			public void SetLabelVisibility(bool visible)
			{
				adapter.SetLabelVisibility(visible);
			}
		}

		// Adapter for the video list. Manages a set of YouTubeThumbnailViews, including initializing each
		// of them only once and keeping track of the loader of each one. When the ListFragment gets
		// destroyed it releases all the loaders.
		private class PageAdapter : BaseAdapter,
			YouTubeThumbnailView.IOnInitializedListener,
			IYouTubeThumbnailLoaderOnThumbnailLoadedListener
		{
			private readonly List<VideoEntry> entries;
			private readonly List<View> entryViews;
			private readonly Dictionary<YouTubeThumbnailView, IYouTubeThumbnailLoader> thumbnailViewToLoaderMap;
			private readonly LayoutInflater inflater;

			private bool labelsVisible;

			public PageAdapter(Context context, List<VideoEntry> entries)
			{
				this.entries = entries;

				entryViews = new List<View>();
				thumbnailViewToLoaderMap = new Dictionary<YouTubeThumbnailView, IYouTubeThumbnailLoader>();
				inflater = LayoutInflater.From(context);

				labelsVisible = true;
			}

			public void ReleaseLoaders()
			{
				foreach (IYouTubeThumbnailLoader loader in thumbnailViewToLoaderMap.Values)
				{
					loader.Release();
				}
			}

			public void SetLabelVisibility(bool visible)
			{
				labelsVisible = visible;
				foreach (View view in entryViews)
				{
					view.FindViewById(Resource.Id.text).Visibility = visible ? ViewStates.Visible : ViewStates.Gone;
				}
			}

			public override int Count => entries.Count;

			public override Java.Lang.Object GetItem(int position) => entries[position];

			public override long GetItemId(int position) => 0;

			public override View GetView(int position, View convertView, ViewGroup parent)
			{
				var view = convertView;
				var entry = entries[position];

				// There are three cases here
				if (view == null)
				{
					// 1) The view has not yet been created - we need to initialize the YouTubeThumbnailView.
					view = inflater.Inflate(Resource.Layout.video_list_item, parent, false);
					var thumbnail = view.FindViewById<YouTubeThumbnailView>(Resource.Id.thumbnail);
					thumbnail.Tag = entry.VideoId;
					thumbnail.Initialize(DeveloperKey.Key, this);
				}
				else
				{
					var thumbnail = view.FindViewById<YouTubeThumbnailView>(Resource.Id.thumbnail);
					var loader = thumbnailViewToLoaderMap[thumbnail];
					if (loader == null)
					{
						// 2) The view is already created, and is currently being initialized. We store the
						//    current videoId in the tag.
						thumbnail.Tag = entry.VideoId;
					}
					else
					{
						// 3) The view is already created and already initialized. Simply set the right videoId
						//    on the loader.
						thumbnail.SetImageResource(Resource.Drawable.loading_thumbnail);
						loader.SetVideo(entry.VideoId);
					}
				}

				var label = view.FindViewById<TextView>(Resource.Id.text);
				label.Text = entry.Text;
				label.Visibility = labelsVisible ? ViewStates.Visible : ViewStates.Gone;

				return view;
			}

			void YouTubeThumbnailView.IOnInitializedListener.OnInitializationFailure(YouTubeThumbnailView view, YouTubeInitializationResult result)
			{
				view.SetImageResource(Resource.Drawable.no_thumbnail);
			}

			void YouTubeThumbnailView.IOnInitializedListener.OnInitializationSuccess(YouTubeThumbnailView view, IYouTubeThumbnailLoader loader)
			{
				loader.SetOnThumbnailLoadedListener(this);
				thumbnailViewToLoaderMap.Add(view, loader);
				view.SetImageResource(Resource.Drawable.loading_thumbnail);
				var videoId = (string)view.Tag;
				loader.SetVideo(videoId);
			}

			void IYouTubeThumbnailLoaderOnThumbnailLoadedListener.OnThumbnailError(YouTubeThumbnailView view, YouTubeThumbnailLoaderErrorReason errorReason)
			{
				view.SetImageResource(Resource.Drawable.no_thumbnail);
			}

			void IYouTubeThumbnailLoaderOnThumbnailLoadedListener.OnThumbnailLoaded(YouTubeThumbnailView view, string videoId)
			{
			}
		}

		public class VideoFragment : YouTubePlayerFragment, IYouTubePlayerOnInitializedListener
		{
			private IYouTubePlayer player;
			private string videoId;

			public override void OnCreate(Bundle savedInstanceState)
			{
				base.OnCreate(savedInstanceState);

				Initialize(DeveloperKey.Key, this);
			}

			public override void OnDestroy()
			{
				if (player != null)
				{
					player.Release();
				}

				base.OnDestroy();
			}

			public void SetVideoId(string videoId)
			{
				if (videoId != null && videoId != this.videoId)
				{
					this.videoId = videoId;
					if (player != null)
					{
						player.CueVideo(videoId);
					}
				}
			}

			public void Pause()
			{
				if (player != null)
				{
					player.Pause();
				}
			}

			void IYouTubePlayerOnInitializedListener.OnInitializationFailure(IYouTubePlayerProvider provider, YouTubeInitializationResult result)
			{
				player = null;
			}

			void IYouTubePlayerOnInitializedListener.OnInitializationSuccess(IYouTubePlayerProvider provider, IYouTubePlayer player, bool restored)
			{
				this.player = player;
				player.AddFullscreenControlFlag(YouTubePlayer.FullscreenFlagCustomLayout);
				player.SetOnFullscreenListener((VideoListDemoActivity)Activity);
				if (!restored && videoId != null)
				{
					player.CueVideo(videoId);
				}
			}

			public static VideoFragment Create() => new VideoFragment();
		}

		private class VideoEntry : Java.Lang.Object
		{
			public VideoEntry(string text, string videoId)
			{
				Text = text;
				VideoId = videoId;
			}

			public string Text { get; private set; }
			public string VideoId { get; private set; }
		}

		// Utility methods for layouting.

		private int DpToPx(int dp)
		{
			return (int)(dp * Resources.DisplayMetrics.Density + 0.5f);
		}

		private static void SetLayoutSize(View view, int width, int height)
		{
			var param = view.LayoutParameters;
			param.Width = width;
			param.Height = height;
			view.LayoutParameters = param;
		}

		private static void SetLayoutSizeAndGravity(View view, int width, int height, GravityFlags gravity)
		{
			var param = (FrameLayout.LayoutParams)view.LayoutParameters;
			param.Width = width;
			param.Height = height;
			param.Gravity = gravity;
			view.LayoutParameters = param;
		}
	}
}

using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using YouTube.Player;

namespace YouTubePlayerSample
{
	// A demo application aimed at showing the capabilities of the YouTube Player API.  It shows a video
	// wall of flipping YouTube thumbnails.  Every 5 flips, one of the thumbnails will be replaced with
	// a playing YouTube video.
	[Activity(
		Label = "@string/videowall_demo_name",
		ScreenOrientation = ScreenOrientation.Landscape,
		Theme = "@style/BlackNoBarsTheme",
		ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.KeyboardHidden)]
	[MetaData("@string/minVersion", Value = "11")]
	[MetaData("@string/isLaunchableActivity", Value = "true")]
	public class VideoWallDemoActivity : Activity,
		IYouTubePlayerOnInitializedListener,
		YouTubeThumbnailView.IOnInitializedListener,
		IYouTubeThumbnailLoaderOnThumbnailLoadedListener,
		IYouTubePlayerPlayerStateChangeListener
	{
		private const int RECOVERY_DIALOG_REQUEST = 1;

		// The player view cannot be smaller than 110 pixels high.
		private const float PLAYER_VIEW_MINIMUM_HEIGHT_DP = 110;
		private const int MAX_NUMBER_OF_ROWS_WANTED = 4;

		// Example playlist from which videos are displayed on the video wall
		private const String PLAYLIST_ID = "ECAE6B03CA849AD332";

		private const int INTER_IMAGE_PADDING_DP = 5;

		// YouTube thumbnails have a 16 / 9 aspect ratio
		private const double THUMBNAIL_ASPECT_RATIO = 16 / 9d;

		private const int INITIAL_FLIP_DURATION_MILLIS = 100;
		private const int FLIP_DURATION_MILLIS = 500;
		private const int FLIP_PERIOD_MILLIS = 2000;

		private ImageWallView imageWallView;
		private Handler flipDelayHandler;

		private FlippingView flippingView;
		private YouTubeThumbnailView thumbnailView;
		private IYouTubeThumbnailLoader thumbnailLoader;

		private YouTubePlayerFragment playerFragment;
		private View playerView;
		private IYouTubePlayer player;

		private Dialog errorDialog;

		private int flippingCol;
		private int flippingRow;
		private int videoCol;
		private int videoRow;

		private bool nextThumbnailLoaded;
		private bool activityResumed;
		private State state;

		private enum State
		{
			UNINITIALIZED,
			LOADING_THUMBNAILS,
			VIDEO_FLIPPED_OUT,
			VIDEO_LOADING,
			VIDEO_CUED,
			VIDEO_PLAYING,
			VIDEO_ENDED,
			VIDEO_BEING_FLIPPED_OUT,
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			state = State.UNINITIALIZED;

			var viewFrame = new FrameLayout(this);

			var displayMetrics = Resources.DisplayMetrics;
			int maxAllowedNumberOfRows = (int)Math.Floor((displayMetrics.HeightPixels / displayMetrics.Density) / PLAYER_VIEW_MINIMUM_HEIGHT_DP);
			int numberOfRows = Math.Min(maxAllowedNumberOfRows, MAX_NUMBER_OF_ROWS_WANTED);
			int interImagePaddingPx = (int)displayMetrics.Density * INTER_IMAGE_PADDING_DP;
			int imageHeight = (displayMetrics.HeightPixels / numberOfRows) - interImagePaddingPx;
			int imageWidth = (int)(imageHeight * THUMBNAIL_ASPECT_RATIO);

			imageWallView = new ImageWallView(this, imageWidth, imageHeight, interImagePaddingPx);
			viewFrame.AddView(imageWallView, ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);

			thumbnailView = new YouTubeThumbnailView(this);
			thumbnailView.Initialize(DeveloperKey.Key, this);

			flippingView = new FlippingView(this, imageWidth, imageHeight);
			flippingView.Flipped += OnFlipped;
			flippingView.SetFlipDuration(INITIAL_FLIP_DURATION_MILLIS);
			viewFrame.AddView(flippingView, imageWidth, imageHeight);

			playerView = new FrameLayout(this);
			playerView.Id = Resource.Id.player_view;
			playerView.Visibility = ViewStates.Invisible;
			viewFrame.AddView(playerView, imageWidth, imageHeight);

			playerFragment = YouTubePlayerFragment.NewInstance();
			playerFragment.Initialize(DeveloperKey.Key, this);
			FragmentManager.BeginTransaction().Add(Resource.Id.player_view, playerFragment).Commit();

			flipDelayHandler = new Handler(msg =>
			{
				FlipNext();
				flipDelayHandler.SendEmptyMessageDelayed(0, FLIP_PERIOD_MILLIS);
			});

			SetContentView(viewFrame);
		}

		void IYouTubePlayerOnInitializedListener.OnInitializationFailure(IYouTubePlayerProvider provider, YouTubeInitializationResult errorReason)
		{
			if (errorReason.IsUserRecoverableError)
			{
				if (errorDialog == null || !errorDialog.IsShowing)
				{
					errorDialog = errorReason.GetErrorDialog(this, RECOVERY_DIALOG_REQUEST);
					errorDialog.Show();
				}
			}
			else
			{
				var errorMessage = string.Format(GetString(Resource.String.error_player), errorReason);
				Toast.MakeText(this, errorMessage, ToastLength.Long).Show();
			}
		}

		void IYouTubePlayerOnInitializedListener.OnInitializationSuccess(IYouTubePlayerProvider provider, IYouTubePlayer player, bool wasResumed)
		{
			this.player = player;
			player.SetPlayerStyle(YouTubePlayerPlayerStyle.Chromeless);
			player.SetPlayerStateChangeListener(this);
			MaybeStartDemo();
		}

		void YouTubeThumbnailView.IOnInitializedListener.OnInitializationFailure(YouTubeThumbnailView thumbnailView, YouTubeInitializationResult errorReason)
		{
			if (errorReason.IsUserRecoverableError)
			{
				if (errorDialog == null || !errorDialog.IsShowing)
				{
					errorDialog = errorReason.GetErrorDialog(this, RECOVERY_DIALOG_REQUEST);
					errorDialog.Show();
				}
			}
			else
			{
				var errorMessage = string.Format(GetString(Resource.String.error_thumbnail_view), errorReason);
				Toast.MakeText(this, errorMessage, ToastLength.Long).Show();
			}
		}

		void YouTubeThumbnailView.IOnInitializedListener.OnInitializationSuccess(YouTubeThumbnailView thumbnailView, IYouTubeThumbnailLoader thumbnailLoader)
		{
			this.thumbnailLoader = thumbnailLoader;
			thumbnailLoader.SetOnThumbnailLoadedListener(this);
			MaybeStartDemo();
		}

		private void MaybeStartDemo()
		{
			if (activityResumed && player != null && thumbnailLoader != null && state == State.UNINITIALIZED)
			{
				thumbnailLoader.SetPlaylist(PLAYLIST_ID); // loading the first thumbnail will kick off demo
				state = State.LOADING_THUMBNAILS;
			}
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			if (requestCode == RECOVERY_DIALOG_REQUEST)
			{
				// Retry initialization if user performed a recovery action
				if (errorDialog != null && errorDialog.IsShowing)
				{
					errorDialog.Dismiss();
				}
				errorDialog = null;
				playerFragment.Initialize(DeveloperKey.Key, this);
				thumbnailView.Initialize(DeveloperKey.Key, this);
			}
		}

		protected override void OnResume()
		{
			base.OnResume();

			activityResumed = true;
			if (thumbnailLoader != null && player != null)
			{
				if (state == State.UNINITIALIZED)
				{
					MaybeStartDemo();
				}
				else if (state == State.LOADING_THUMBNAILS)
				{
					LoadNextThumbnail();
				}
				else
				{
					if (state == State.VIDEO_PLAYING)
					{
						player.Play();
					}
					flipDelayHandler.SendEmptyMessageDelayed(0, FLIP_DURATION_MILLIS);
				}
			}
		}

		protected override void OnPause()
		{
			flipDelayHandler.RemoveCallbacksAndMessages(null);
			activityResumed = false;

			base.OnPause();
		}

		protected override void OnDestroy()
		{
			if (thumbnailLoader != null)
			{
				thumbnailLoader.Release();
			}

			base.OnDestroy();
		}

		private void FlipNext()
		{
			if (!nextThumbnailLoaded || state == State.VIDEO_LOADING)
			{
				return;
			}

			if (state == State.VIDEO_ENDED)
			{
				flippingCol = videoCol;
				flippingRow = videoRow;
				state = State.VIDEO_BEING_FLIPPED_OUT;
			}
			else
			{
				var nextTarget = imageWallView.GetNextLoadTarget();
				flippingCol = nextTarget.col;
				flippingRow = nextTarget.row;
			}

			flippingView.SetX(imageWallView.GetXPosition(flippingCol, flippingRow));
			flippingView.SetY(imageWallView.GetYPosition(flippingCol, flippingRow));
			flippingView.SetFlipInDrawable(thumbnailView.Drawable);
			flippingView.SetFlipOutDrawable(imageWallView.GetImageDrawable(flippingCol, flippingRow));
			imageWallView.SetImageDrawable(flippingCol, flippingRow, thumbnailView.Drawable);
			imageWallView.HideImage(flippingCol, flippingRow);
			flippingView.Visibility = ViewStates.Visible;
			flippingView.Flip();
		}

		private void OnFlipped(object sender, EventArgs e)
		{
			imageWallView.ShowImage(flippingCol, flippingRow);
			flippingView.Visibility = ViewStates.Invisible;

			if (activityResumed)
			{
				LoadNextThumbnail();

				if (state == State.VIDEO_BEING_FLIPPED_OUT)
				{
					state = State.VIDEO_FLIPPED_OUT;
				}
				else if (state == State.VIDEO_CUED)
				{
					videoCol = flippingCol;
					videoRow = flippingRow;
					playerView.SetX(imageWallView.GetXPosition(flippingCol, flippingRow));
					playerView.SetY(imageWallView.GetYPosition(flippingCol, flippingRow));
					imageWallView.HideImage(flippingCol, flippingRow);
					playerView.Visibility = ViewStates.Visible;
					player.Play();
					state = State.VIDEO_PLAYING;
				}
				else if (state == State.LOADING_THUMBNAILS && imageWallView.AllImagesLoaded)
				{
					state = State.VIDEO_FLIPPED_OUT; // trigger flip in of an initial video
					flippingView.SetFlipDuration(FLIP_DURATION_MILLIS);
					flipDelayHandler.SendEmptyMessage(0);
				}
			}
		}

		private void LoadNextThumbnail()
		{
			nextThumbnailLoaded = false;
			if (thumbnailLoader.HasNext)
			{
				thumbnailLoader.Next();
			}
			else
			{
				thumbnailLoader.First();
			}
		}

		void IYouTubeThumbnailLoaderOnThumbnailLoadedListener.OnThumbnailError(YouTubeThumbnailView thumbnail, YouTubeThumbnailLoaderErrorReason reason)
		{
			LoadNextThumbnail();
		}

		void IYouTubeThumbnailLoaderOnThumbnailLoadedListener.OnThumbnailLoaded(YouTubeThumbnailView thumbnail, string videoId)
		{
			nextThumbnailLoaded = true;

			if (activityResumed)
			{
				if (state == State.LOADING_THUMBNAILS)
				{
					FlipNext();
				}
				else if (state == State.VIDEO_FLIPPED_OUT)
				{
					// load player with the video of the next thumbnail being flipped in
					state = State.VIDEO_LOADING;
					player.CueVideo(videoId);
				}
			}
		}

		void IYouTubePlayerPlayerStateChangeListener.OnAdStarted()
		{
		}

		void IYouTubePlayerPlayerStateChangeListener.OnError(YouTubePlayerErrorReason errorReason)
		{
			if (errorReason == YouTubePlayerErrorReason.UnexpectedServiceDisconnection)
			{
				// player has encountered an unrecoverable error - stop the demo
				flipDelayHandler.RemoveCallbacksAndMessages(null);
				state = State.UNINITIALIZED;
				thumbnailLoader.Release();
				thumbnailLoader = null;
				player = null;
			}
			else
			{
				state = State.VIDEO_ENDED;
			}
		}

		void IYouTubePlayerPlayerStateChangeListener.OnLoaded(string videoId)
		{
			state = State.VIDEO_CUED;
		}

		void IYouTubePlayerPlayerStateChangeListener.OnLoading()
		{
		}

		void IYouTubePlayerPlayerStateChangeListener.OnVideoEnded()
		{
			state = State.VIDEO_ENDED;
			imageWallView.ShowImage(videoCol, videoRow);
			playerView.Visibility = ViewStates.Invisible;
		}

		void IYouTubePlayerPlayerStateChangeListener.OnVideoStarted()
		{
		}
	}
}

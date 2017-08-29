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
		private const int RecoveryDialogRequest = 1;

		// The player view cannot be smaller than 110 pixels high.
		private const float PlayerViewMinimumHeight = 110;
		private const int MaxRowCount = 4;

		// Example playlist from which videos are displayed on the video wall
		private const String PlaylistId = "ECAE6B03CA849AD332";

		private const int ImagePadding = 5;

		// YouTube thumbnails have a 16 / 9 aspect ratio
		private const double ThumbnailAspectRatio = 16 / 9d;

		private const int InitialFlipDuration = 100;
		private const int FlipDuration = 500;
		private const int FlipPeriod = 2000;

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
			Uninitialized,
			LoadingThumbnails,
			VideoFlippedOut,
			VideoLoading,
			VideoCued,
			VideoPlaying,
			VideoEnded,
			VideoBeingFlippedOut,
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			state = State.Uninitialized;

			var viewFrame = new FrameLayout(this);

			var displayMetrics = Resources.DisplayMetrics;
			int maxAllowedNumberOfRows = (int)Math.Floor((displayMetrics.HeightPixels / displayMetrics.Density) / PlayerViewMinimumHeight);
			int numberOfRows = Math.Min(maxAllowedNumberOfRows, MaxRowCount);
			int interImagePaddingPx = (int)displayMetrics.Density * ImagePadding;
			int imageHeight = (displayMetrics.HeightPixels / numberOfRows) - interImagePaddingPx;
			int imageWidth = (int)(imageHeight * ThumbnailAspectRatio);

			imageWallView = new ImageWallView(this, imageWidth, imageHeight, interImagePaddingPx);
			viewFrame.AddView(imageWallView, ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);

			thumbnailView = new YouTubeThumbnailView(this);
			thumbnailView.Initialize(DeveloperKey.Key, this);

			flippingView = new FlippingView(this, imageWidth, imageHeight);
			flippingView.Flipped += OnFlipped;
			flippingView.SetFlipDuration(InitialFlipDuration);
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
				flipDelayHandler.SendEmptyMessageDelayed(0, FlipPeriod);
			});

			SetContentView(viewFrame);
		}

		void IYouTubePlayerOnInitializedListener.OnInitializationFailure(IYouTubePlayerProvider provider, YouTubeInitializationResult errorReason)
		{
			if (errorReason.IsUserRecoverableError)
			{
				if (errorDialog == null || !errorDialog.IsShowing)
				{
					errorDialog = errorReason.GetErrorDialog(this, RecoveryDialogRequest);
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
					errorDialog = errorReason.GetErrorDialog(this, RecoveryDialogRequest);
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
			if (activityResumed && player != null && thumbnailLoader != null && state == State.Uninitialized)
			{
				thumbnailLoader.SetPlaylist(PlaylistId); // loading the first thumbnail will kick off demo
				state = State.LoadingThumbnails;
			}
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			if (requestCode == RecoveryDialogRequest)
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
				if (state == State.Uninitialized)
				{
					MaybeStartDemo();
				}
				else if (state == State.LoadingThumbnails)
				{
					LoadNextThumbnail();
				}
				else
				{
					if (state == State.VideoPlaying)
					{
						player.Play();
					}
					flipDelayHandler.SendEmptyMessageDelayed(0, FlipDuration);
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
			if (!nextThumbnailLoaded || state == State.VideoLoading)
			{
				return;
			}

			if (state == State.VideoEnded)
			{
				flippingCol = videoCol;
				flippingRow = videoRow;
				state = State.VideoBeingFlippedOut;
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

				if (state == State.VideoBeingFlippedOut)
				{
					state = State.VideoFlippedOut;
				}
				else if (state == State.VideoCued)
				{
					videoCol = flippingCol;
					videoRow = flippingRow;
					playerView.SetX(imageWallView.GetXPosition(flippingCol, flippingRow));
					playerView.SetY(imageWallView.GetYPosition(flippingCol, flippingRow));
					imageWallView.HideImage(flippingCol, flippingRow);
					playerView.Visibility = ViewStates.Visible;
					player.Play();
					state = State.VideoPlaying;
				}
				else if (state == State.LoadingThumbnails && imageWallView.AllImagesLoaded)
				{
					state = State.VideoFlippedOut; // trigger flip in of an initial video
					flippingView.SetFlipDuration(FlipDuration);
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
				if (state == State.LoadingThumbnails)
				{
					FlipNext();
				}
				else if (state == State.VideoFlippedOut)
				{
					// load player with the video of the next thumbnail being flipped in
					state = State.VideoLoading;
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
				state = State.Uninitialized;
				thumbnailLoader.Release();
				thumbnailLoader = null;
				player = null;
			}
			else
			{
				state = State.VideoEnded;
			}
		}

		void IYouTubePlayerPlayerStateChangeListener.OnLoaded(string videoId)
		{
			state = State.VideoCued;
		}

		void IYouTubePlayerPlayerStateChangeListener.OnLoading()
		{
		}

		void IYouTubePlayerPlayerStateChangeListener.OnVideoEnded()
		{
			state = State.VideoEnded;
			imageWallView.ShowImage(videoCol, videoRow);
			playerView.Visibility = ViewStates.Invisible;
		}

		void IYouTubePlayerPlayerStateChangeListener.OnVideoStarted()
		{
		}
	}
}

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoreGraphics;
using Foundation;
using UIKit;

using YouTube.Player;

namespace YouTubePlayerSample
{
	public partial class PlaylistViewController : UIViewController, IUITableViewDataSource, IUITableViewDelegate
	{
		UIActivityIndicatorView indicatorView;
		List<Video> videos;
		bool firstLoad = true;
		bool isPlaylistRequested;
		Playlist playlist;

		public PlaylistViewController(IntPtr handle)
			: base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			InitializeComponents();

			// Where videos are saved
			videos = new List<Video>();

			// If you want to load your own playlist,
			// change this ID with yours.
			playlist = new Playlist
			{
				Id = "PLM75ZaNQS_Fb7I6E9MDnMgwW1GGZIijf_"
			};
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			YouTubeManager.SharedInstance.MaxResults = 10;
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			GetPlaylist();
		}

		public override void ViewWillDisappear(bool animated)
		{
			if (PlayerView.PlayerState == PlayerState.Playing)
				PlayerView.StopVideo();

			base.ViewWillDisappear(animated);
		}

		// Reproduce the previous video in the playlist.
		void BtnPrevious_TouchUpInside(object sender, EventArgs e)
		{
			int currentIndex = PlayerView.PlaylistIndex;

			// There is no previous video if we reached to the beggining of the list.
			if (currentIndex == 0)
				return;

			// Play previous video in the playlist.
			PlayerView.PreviousVideo();

			// Highlight the current video in Table View.
			var newIndex = currentIndex - 1;
			var indexPath = NSIndexPath.FromRowSection(newIndex, 0);
			TblVideos.SelectRow(indexPath, true, UITableViewScrollPosition.Top);

			// Enable the next button because maybe we are not longer in the last video of the playlist.
			EnableButton(BtnNext);

			// Disable the Previous button if we reach to the beggining of the playlist.
			if (newIndex == 0)
				DisableButton(BtnPrevious);
		}

		// Play/Pause the current video in the YouTube Player.
		void BtnPlayPause_TouchUpInside(object sender, EventArgs e)
		{
			if (PlayerView.PlayerState == PlayerState.Playing)
			{
				PlayerView.PauseVideo();
				BtnPlayPause.SetTitle("Play", UIControlState.Normal);
			}
			else
			{
				PlayerView.PlayVideo();
				BtnPlayPause.SetTitle("Pause", UIControlState.Normal);
			}
		}

		// Stop the current video in the YouTube Player.
		void BtnStop_TouchUpInside(object sender, EventArgs e)
		{
			PlayerView.StopVideo();
			BtnPlayPause.SetTitle("Play", UIControlState.Normal);
		}

		// Reproduce the next video in the playlist.
		void BtnNext_TouchUpInside(object sender, EventArgs e)
		{
			int currentIndex = PlayerView.PlaylistIndex;

			// There is no next video if we reached to the end of the list.
			if (currentIndex == videos.Count - 1)
				return;

			// Play next video in the playlist.
			PlayerView.NextVideo();

			// Highlight the current video in Table View.
			var newIndex = currentIndex + 1;
			var indexPath = NSIndexPath.FromRowSection(newIndex, 0);
			TblVideos.SelectRow(indexPath, true, UITableViewScrollPosition.Top);

			// Enable the Previous Button because maybe we are not longer in the first video of the playlist.
			EnableButton(BtnPrevious);

			// Disable the Next button if we reach to the end of the playlist.
			if (newIndex == videos.Count - 1)
				DisableButton(BtnNext);
		}

		void InitializeComponents()
		{
			Title = "Playlist";

			// Showed when we are getting the playlist from YouTube.
			indicatorView = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.White)
			{
				Frame = new CGRect(0, 0, 30, 30),
				HidesWhenStopped = true
			};

			var btnProgress = new UIBarButtonItem(indicatorView);

			NavigationItem.RightBarButtonItems = new[] { btnProgress };

			PlayerView.BecameReady += PlayerView_BecameReady;
			PlayerView.StateChanged += PlayerView_StateChanged;
			PlayerView.PreferredWebViewBackgroundColor += PlayerView_PreferredWebViewBackgroundColor;

			BtnPrevious.TouchUpInside += BtnPrevious_TouchUpInside;
			BtnPlayPause.TouchUpInside += BtnPlayPause_TouchUpInside;
			BtnStop.TouchUpInside += BtnStop_TouchUpInside;
			BtnNext.TouchUpInside += BtnNext_TouchUpInside;
		}

		// Retrives the playlist from YouTube.
		void GetPlaylist()
		{
			// We validate if we are already getting the playlist
			// to avoid a duplicate call.
			if (isPlaylistRequested)
				return;

			isPlaylistRequested = true;

			indicatorView.StartAnimating();

			Task.Run(async () =>
			{
				// Retrives the playlist from YouTube.
				var result = await YouTubeManager.SharedInstance.GetVideos(playlist);
				videos.AddRange(result);
				InvokeOnMainThread(() =>
				{
					// Show new videos in TableView
					TblVideos.ReloadData();
					indicatorView.StopAnimating();
					isPlaylistRequested = false;

					// Enable the Player View to start playing videos from Playlist.
					if (firstLoad)
					{
						// Try using this method once, due it reloads the whole view.
						// If you want to play another video from Playlist,
						// use PlayVideoAt method.
						PlayerView.LoadPlaylistById(playlist.Id);
						firstLoad = false;
					}
				});
			});
		}

		// Enables the button and add YouTube red color
		void EnableButton(UIButton button)
		{
			button.SetTitleColor(YouTubeManager.YouTubeColor, UIControlState.Normal);
			button.Enabled = true;
		}

		// Disables the button and add a gray color
		void DisableButton(UIButton button)
		{
			button.SetTitleColor(YouTubeManager.DisabledColor, UIControlState.Normal);
			button.Enabled = false;
		}

		// Player View Delegate

		// To know when the Player View is ready to reproduce videos.
		void PlayerView_BecameReady(object sender, EventArgs e)
		{
			Console.WriteLine($"Player is ready to reproduce videos");

			EnableButton(BtnPlayPause);

			if (PlayerView.Playlist?.Length > 1)
				EnableButton(BtnNext);
		}

		// To know when Player View changes the video state.
		void PlayerView_StateChanged(object sender, PlayerViewStateChangedEventArgs e)
		{
			Console.WriteLine($"Player changed state to {e.State}");

			if (e.State == PlayerState.Queued)
			{
				BtnPlayPause.SetTitle("Play", UIControlState.Normal);
				EnableButton(BtnPlayPause);
			}

			if (e.State == PlayerState.Playing)
			{
				BtnPlayPause.SetTitle("Pause", UIControlState.Normal);
				EnableButton(BtnPlayPause);
			}
		}

		UIColor PlayerView_PreferredWebViewBackgroundColor(PlayerView playerView)
		{
			return UIColor.Black;
		}

		// UITableView Data Source

		[Export("numberOfSectionsInTableView:")]
		public nint NumberOfSections(UITableView tableView)
		{
			return 1;
		}

		public nint RowsInSection(UITableView tableView, nint section)
		{
			return videos.Count;
		}

		public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			// Set the video title.
			var video = videos[indexPath.Row];
			var cell = tableView.DequeueReusableCell(VideoCell.Key, indexPath) as VideoCell;
			cell.LblTitleText = video.Title;

			// Set the default thumbnail.
			var imagePath = NSBundle.MainBundle.PathForResource("thumbnail", "png");
			cell.ImgThumbnailImage = new UIImage(imagePath);

			// Cancel any previous download thumbnail process
			// to avoid showing an incorrect thumbnail on cell.
			if (cell.CancellationTokenSource != null &&
			   !cell.CancellationTokenSource.IsCancellationRequested)
				cell.CancellationTokenSource.Cancel();

			// Lazy download of videos from playlist.
			// When we reach to the bottom of TableView, 
			// keep downloading videos from playlist if any.
			if (videos.Count - 1 == indexPath.Row && playlist.NextPageToken != null)
				GetPlaylist();

			// If video is not private, download the thumbnail of video.
			if (video.ThumbnailUrl == null)
				return cell;

			var cancellationTokenSource = new CancellationTokenSource();
			cell.CancellationTokenSource = cancellationTokenSource;

			var cancellationToken = cancellationTokenSource.Token;

			Task.Run(async () =>
			{
				try
				{
					// Download thumbnail and show it.
					var thumbnailPath = await YouTubeManager.SharedInstance.DownloadThumbnail(video, cancellationToken);
					InvokeOnMainThread(() =>
					{
						cell.ImgThumbnailImage = UIImage.FromFile(thumbnailPath);
					});
				}
				catch (OperationCanceledException ex)
				{
					Console.WriteLine($"Get Thumbnail cancelled: {ex.Message}");
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}, cancellationToken);

			return cell;
		}

		[Export("tableView:titleForHeaderInSection:")]
		public string TitleForHeader(UITableView tableView, nint section)
		{
			return "Playlist content";
		}

		// UITableView Delegate

		[Export("tableView:didSelectRowAtIndexPath:")]
		public void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			// Play selected video from Playlist
			var video = videos[indexPath.Row];
			PlayerView.PlayVideoAt(indexPath.Row);

			BtnPlayPause.SetTitle("Play", UIControlState.Normal);
			DisableButton(BtnPlayPause);

			if (indexPath.Row == 0)
				DisableButton(BtnPrevious);
			else
				EnableButton(BtnPrevious);

			if (indexPath.Row == videos.Count - 1)
				DisableButton(BtnNext);
			else
				EnableButton(BtnNext);
		}

		[Export("tableView:heightForRowAtIndexPath:")]
		public nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			return 68;
		}
	}
}

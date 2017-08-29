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
	public partial class SearchVideosViewController : UIViewController, IUITextFieldDelegate, IUITableViewDataSource, IUITableViewDelegate
	{
		UITextField txtSearch;
		UIActivityIndicatorView indicatorView;
		List<Video> videos;
		Search search;
		bool firstLoad = true;

		public SearchVideosViewController(IntPtr handle)
			: base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			InitializeComponents();

			// Where videos are saved.
			videos = new List<Video>();
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			YouTubeManager.SharedInstance.MaxResults = 10;
		}

		public override void ViewWillDisappear(bool animated)
		{
			if (PlayerView.PlayerState == PlayerState.Playing)
				PlayerView.StopVideo();

			base.ViewWillDisappear(animated);
		}

		// Starts the videos search with what user typed
		void SearchButtonTapped(object sender, EventArgs e)
		{
			// Set focus on TetxField
			if (!txtSearch.IsFirstResponder)
			{
				txtSearch.BecomeFirstResponder();
				return;
			}

			// Clear previous search.
			videos.Clear();

			// Start the search
			search = new Search { Tags = txtSearch.Text };
			txtSearch.ResignFirstResponder();
			SearchForVideos();
		}

		// Play/Pause the current video in the YouTube Player.
		void BtnPlayPause_TouchUpInside(object sender, EventArgs e)
		{
			if (PlayerView.PlayerState == PlayerState.Playing)
			{
				PlayerView.PauseVideo();
				BtnPlayPause.SetTitle("Play Video", UIControlState.Normal);
			}
			else
			{
				PlayerView.PlayVideo();
				BtnPlayPause.SetTitle("Pause Video", UIControlState.Normal);
			}
		}

		// Stop the current video in the YouTube Player.
		void BtnStop_TouchUpInside(object sender, EventArgs e)
		{
			PlayerView.StopVideo();
			BtnPlayPause.SetTitle("Play Video", UIControlState.Normal);
		}

		void InitializeComponents()
		{
			// TextField we user types what he/she wants to search.
			var placeholderAttributes = new UIStringAttributes { ForegroundColor = UIColor.LightGray };
			txtSearch = new UITextField(new CGRect(0, 0, NavigationController.NavigationBar.Bounds.Width, 30))
			{
				Delegate = this,
				AttributedPlaceholder = new NSAttributedString("Search for a video…", placeholderAttributes),
				TextColor = UIColor.White,
				BackgroundColor = YouTubeManager.BackgroundYouTubeColor,
				TextAlignment = UITextAlignment.Center,
				BorderStyle = UITextBorderStyle.RoundedRect
			};

			NavigationItem.TitleView = txtSearch;

			// Showed when we do a search to YouTube.
			indicatorView = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.White)
			{
				Frame = new CGRect(0, 0, 30, 30),
				HidesWhenStopped = true
			};

			var btnSearch = new UIBarButtonItem(UIBarButtonSystemItem.Search, SearchButtonTapped);
			var btnProgress = new UIBarButtonItem(indicatorView);

			NavigationItem.RightBarButtonItems = new[] { btnSearch, btnProgress };

			PlayerView.BecameReady += PlayerView_BecameReady;
			PlayerView.StateChanged += PlayerView_StateChanged;
			PlayerView.PreferredWebViewBackgroundColor += PlayerView_PreferredWebViewBackgroundColor;

			BtnPlayPause.TouchUpInside += BtnPlayPause_TouchUpInside;
			BtnStop.TouchUpInside += BtnStop_TouchUpInside;
		}

		// Search for videos with user suggestion.
		void SearchForVideos()
		{
			indicatorView.StartAnimating();

			Task.Run(async () =>
			{
				// Get videos that result from user suggestion
				var result = await YouTubeManager.SharedInstance.GetVideos(search);
				videos.AddRange(result);
				InvokeOnMainThread(() =>
				{
					// Show videos on TableView
					TblVideos.ReloadData();
					indicatorView.StopAnimating();
				});
			});
		}

		// Player View Delegate

		// To know when the Player View is ready to reproduce videos.
		void PlayerView_BecameReady(object sender, EventArgs e)
		{
			Console.WriteLine($"Player is ready to reproduce videos");

			BtnPlayPause.Enabled = true;
			BtnPlayPause.SetTitleColor(YouTubeManager.YouTubeColor, UIControlState.Normal);
		}

		// To know when Player View changes the video state.
		void PlayerView_StateChanged(object sender, PlayerViewStateChangedEventArgs e)
		{
			Console.WriteLine($"Player changed state to {e.State}");

			if (e.State == PlayerState.Queued)
			{
				BtnPlayPause.SetTitle("Play Video", UIControlState.Normal);
				BtnPlayPause.Enabled = true;
				BtnPlayPause.SetTitleColor(YouTubeManager.YouTubeColor, UIControlState.Normal);
			}
		}

		UIColor PlayerView_PreferredWebViewBackgroundColor(PlayerView playerView)
		{
			return UIColor.Black;
		}

		// UITextField Delegate

		[Export("textFieldShouldReturn:")]
		public bool ShouldReturn(UITextField textField)
		{
			// When user taps Return button
			// it starts the search

			// Clear previous search.
			videos.Clear();

			// Start the search
			search = new Search { Tags = textField.Text };
			txtSearch.ResignFirstResponder();
			SearchForVideos();

			return true;
		}

		// UITableView Data Source

		[Export("numberOfSectionsInTableView:")]
		public nint NumberOfSections(UITableView tableView)
		{
			return 1;
		}

		public nint RowsInSection(UITableView tableView, nint section)
		{
			return videos.Count == 0 ? 1 : videos.Count;
		}

		public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			// If you have not search for something,
			// show a nice message.
			if (videos.Count == 0)
			{
				var defaultCell = tableView.DequeueReusableCell("DefaultCellId", indexPath);
				defaultCell.TextLabel.Text = "It feels empty…\nSearch for some cool videos!";
				return defaultCell;
			}

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
			if (videos.Count - 1 == indexPath.Row && search.NextPageToken != null)
				SearchForVideos();

			// Download the thumbnail of video.
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
			return "Search results";
		}

		// UITableView Delegate

		[Export("tableView:didSelectRowAtIndexPath:")]
		public void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			if (videos.Count == 0)
				return;

			BtnPlayPause.SetTitle("Play Video", UIControlState.Normal);
			BtnPlayPause.SetTitleColor(YouTubeManager.DisabledColor, UIControlState.Normal);
			BtnPlayPause.Enabled = false;

			var video = videos[indexPath.Row];

			if (firstLoad)
			{
				PlayerView.LoadVideoById(video.Id);
				firstLoad = false;
			}
			else
			{
				PlayerView.CueVideoById(video.Id, 0, PlaybackQuality.Auto);
			}
		}

		[Export("tableView:heightForRowAtIndexPath:")]
		public nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			return 68;
		}
	}
}

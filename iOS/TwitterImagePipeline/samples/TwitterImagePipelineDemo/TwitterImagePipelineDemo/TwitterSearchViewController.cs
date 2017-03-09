using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;

namespace TwitterImagePipelineDemo
{
	public class TwitterSearchViewController : UIViewController, IUISearchResultsUpdating, IUISearchControllerDelegate, IUISearchBarDelegate, IUITableViewDelegate, IUITableViewDataSource
	{
		private UISearchController searchController;
		private UITableView tableView;

		private readonly List<TweetInfo> tweets = new List<TweetInfo>();
		private string term;

		public TwitterSearchViewController()
		{
			NavigationItem.Title = "Twitter Search";
		}

		public override async void ViewDidLoad()
		{
			base.ViewDidLoad();

			searchController = new UISearchController((UIViewController)null);
			searchController.SearchResultsUpdater = this;
			searchController.DefinesPresentationContext = true;
			searchController.SearchBar.Delegate = this;
			searchController.SearchBar.SizeToFit();

			tableView = new UITableView(View.Bounds, UITableViewStyle.Plain);
			tableView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
			tableView.Delegate = this;
			tableView.DataSource = this;
			tableView.TableHeaderView = searchController.SearchBar;

			View.AddSubview(tableView);
		}

		// IUITableViewDataSource

		public nint RowsInSection(UITableView tableView, nint section)
		{
			return tweets.Count;
		}

		public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var tweet = tweets[indexPath.Row];
			var hasImages = tweet.Images.Count > 0;
			var cell = tableView.DequeueReusableCell(hasImages ? TweetWithMediaTableViewCell.ReuseId : "TweetNoMedia");
			if (cell == null)
			{
				if (hasImages)
				{
					cell = new TweetWithMediaTableViewCell();
				}
				else
				{
					cell = new UITableViewCell(UITableViewCellStyle.Subtitle, "TweetNoMedia");
				}
			}

			cell.TextLabel.Text = tweet.Handle;
			cell.DetailTextLabel.Text = tweet.Text;
			if (hasImages)
			{
				((TweetWithMediaTableViewCell)cell).Tweet = tweet;
			}

			return cell;
		}

		// IUITableViewDelegate

		[Export("tableView:didSelectRowAtIndexPath:")]
		public void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			tableView.DeselectRow(indexPath, true);
			var imageInfo = tweets[indexPath.Row].Images.FirstOrDefault();
			if (imageInfo != null)
			{
				var vc = new ZoomingTweetImageViewController(imageInfo);
				NavigationController.PushViewController(vc, true);
			}
		}

		[Export("tableView:heightForRowAtIndexPath:")]
		public nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			return tweets[indexPath.Row].Images.Count > 0 ? 180 : 44;
		}

		// IUISearchResultsUpdating

		public void UpdateSearchResultsForSearchController(UISearchController searchController)
		{
		}

		// IUISearchControllerDelegate

		[Export("willPresentSearchController:")]
		public void WillPresentSearchController(UISearchController searchController)
		{
			searchController.SearchBar.Text = term;
		}

		// IUISearchBarDelegate

		[Export("searchBarSearchButtonClicked:")]
		public async void SearchButtonClicked(UISearchBar searchBar)
		{
			var search = searchBar.Text;
			term = search;
			searchController.Active = false;
			searchController.SearchBar.UserInteractionEnabled = false;
			searchBar.Text = term;

			var newTweets = await TwitterApi.SharedInstance.SearchAsync(term, AppDelegate.Current.SearchCount);
			tweets.Clear();
			tweets.AddRange(newTweets);

			searchController.SearchBar.UserInteractionEnabled = true;
			tableView.ReloadData();
		}
	}
}

using System;
using System.Linq;
using System.Threading.Tasks;
using Foundation;
using UIKit;
using Newtonsoft.Json.Linq;
using SDWebImage;

using GigyaSDK;

namespace GigyaSDKSampleiOS
{
	public partial class GDFriendsViewController : UITableViewController
	{
		private UIActivityIndicatorView activityIndicator;
		private JArray friends;

		public GDFriendsViewController(IntPtr handle)
			: base(handle)
		{
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			NavigationController?.SetNavigationBarHidden(false, true);

			// Create and show an activity indicator
			activityIndicator = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);
			activityIndicator.Center = View.Center;
			activityIndicator.HidesWhenStopped = true;
			View.AddSubview(activityIndicator);
			activityIndicator.StartAnimating();

			FetchFriendsList();
		}

		private async void FetchFriendsList()
		{
			// Create the request
			var friendsRequest = GSRequest.Create("socialize.getFriendsInfo");
			friendsRequest.Parameters["detailLevel"] = (NSString)"extended";

			try
			{
				// Send it and handle the response
				var response = await friendsRequest.SendAsync();
				friends = (JArray)JObject.Parse(response.JsonString)["friends"];

				TableView.ReloadData();
				activityIndicator.StopAnimating();
			}
			catch (NSErrorException ex)
			{
				// If the operation is still pending, wait for 5 seconds and try again
				// See: http://developers.gigya.com/display/GD/socialize.getFriendsInfo+REST
				if (ex.Error.Code == 100001)
				{
					await Task.Delay(5000);
					FetchFriendsList();
				}
				else
				{
					Console.WriteLine("Error loading friends: {0}", ex.Error);
					activityIndicator.StopAnimating();

					if (ex.Error.Code == 403005 || ex.Error.Code == 403007)
					{
						await TabBarController.PresentingViewController.DismissViewControllerAsync(true);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error loading friends: {0}", ex.Message);
				activityIndicator.StopAnimating();
			}
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			return 1;
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return (nint)(friends?.Count ?? 0);
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			const string CellIdentifier = "FriendCell";

			var cell = tableView.DequeueReusableCell(CellIdentifier, indexPath);

			// Get the current friend's information
			var currentFriend = (JObject)friends[indexPath.Row];
			var thumbnailUrl = new NSUrl(currentFriend.Value<string>("thumbnailURL"));

			// Add the full name and provider to the cell display
			var fullName = $"{currentFriend["firstName"]} {currentFriend["lastName"]}";
			var providers = currentFriend["identities"].Select(i => i["provider"]);

			cell.TextLabel.Text = fullName;
			cell.DetailTextLabel.Text = string.Join(", ", providers);
			cell.ImageView.SetImage(thumbnailUrl, UIImage.FromBundle("placeholder.png"));

			return cell;
		}
	}
}

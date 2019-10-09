using System;
using Estimote;
using UIKit;
using Foundation;
using CoreLocation;

namespace BeaconExample
{
	public class CloudBeaconsViewController : UITableViewController, IUITableViewDataSource, IUITableViewDelegate
	{
		BeaconVO[] beacons;
		CloudManager cloudAPI;

		public CloudBeaconsViewController ()
		{
			base.Init ();
		}

		public override async void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.TableView = new UITableView (this.View.Bounds, UITableViewStyle.Grouped);
			this.TableView.WeakDataSource = this;
			this.TableView.WeakDelegate = this;

			this.TableView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;

			this.Title = "Cloud Beacons";
			cloudAPI = new CloudManager ();
		
			try {
				beacons = await cloudAPI.FetchEstimoteBeaconsAsync ();
				TableView.ReloadData ();
			} catch {
				new UIAlertView ("Error", "Unable to fetch cloud beacons, ensure you have set Config in AppDelegate", null, "OK").Show ();
			}
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			return 1;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			if (beacons == null)
				return 0;
			else
				return beacons.Length;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell ("cellID");
			if (cell == null) {
				cell = new UITableViewCell (UITableViewCellStyle.Subtitle, "cellId");
			}

			var beacon = beacons [indexPath.Row];
			cell.TextLabel.Text = "Proximity UUID: " + beacon.ProximityUuid;
			cell.DetailTextLabel.Text = string.Format ("Major: {0}, Minor: {1}, Name: {2}, Color: {3} ", beacon.Major, beacon.Minor, beacon.Name, beacon.Color.ToString ());
			return cell;
		}
	}
}

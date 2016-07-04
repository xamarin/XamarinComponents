using System;
using Estimote;
using UIKit;
using Foundation;

namespace NearableExample
{
	public class CloudBeaconsViewController : UITableViewController
	{
		NearableManager nearableManager;
		Nearable[] nearables;
		public CloudBeaconsViewController ()
		{
		}

		public async override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.Title = "Cloud Nearables";
			nearableManager = new NearableManager ();
		
			try
			{
				var cloudApi = new CloudManager ();
				nearables = await cloudApi.FetchEstimoteNearablesAsync ();
				TableView.ReloadData ();
			}
			catch(Exception ex) {
				new UIAlertView ("Error", "Unable to fetch cloud nearable, ensure you have set Config in AppDelegate", null, "OK").Show ();
			}
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			return 1;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			if (nearables == null)
				return 0;
			else
				return nearables.Length;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell ("cellID");
			if (cell == null) {
				cell = new UITableViewCell (UITableViewCellStyle.Subtitle, "cellId");
			}

			var nearable = nearables [indexPath.Row];

			cell.TextLabel.Text = string.Format ("Id: {0}", nearable.Type, nearable.Identifier);
			cell.DetailTextLabel.Text = "Color: " + nearable.Color.ToString () + " Type: " + nearable.Type;

			return cell;
		}
	}
}


using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.Collections.Generic;
using System.Linq;

namespace NearableExample
{
	partial class DemosViewController  : UITableViewController, IUITableViewDataSource, IUITableViewDelegate
	{
		Dictionary<string, List<string>> beaconDemoList;
		public DemosViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			TableView.SectionHeaderHeight = 20;
			beaconDemoList = new Dictionary<string, List<string>> {
				{
					"sticker demos", 
					new List<string>{
						"Monitoring Demo",
						"Triggers Demo",
						"Orientation Trigger Demo",
						/*"Cloud Beacons"*/}
				},
				/*{
					"Sensor demos", 
					new List<string>{
						"Temperature Demo",
						"Accelerometer Demo",
						"Motion UUID Demo"}
				},
				{
					"Utilities demos", 
					new List<string>{
						"Beacon Settings Demo",
						"Update Firmware Demo",
						"My beacons in Cloud Demo",
						"Bulk update of beacons"}
				}*/
			};



		}

		public override nint NumberOfSections (UITableView tableView)
		{
			return beaconDemoList.Count;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return beaconDemoList [beaconDemoList.Keys.ElementAt ((int)section)].Count;
		}

		public override string TitleForHeader (UITableView tableView, nint section)
		{
			return beaconDemoList.Keys.ElementAt ((int)section);
		}

		public override nfloat GetHeightForHeader (UITableView tableView, nint section)
		{
			return 40;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell ("CellId", indexPath);

			if (cell == null) {
				cell = new UITableViewCell (UITableViewCellStyle.Default, "CellId");
			}

			cell.TextLabel.Text = beaconDemoList [beaconDemoList.Keys.ElementAt(indexPath.Section)] [indexPath.Row];

			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			if (indexPath.Section == 0) {
				if (indexPath.Row == 0) {
					var storyboard = UIStoryboard.FromName ("MainStoryboard", null);
					var vc = storyboard.InstantiateViewController ("NearablesViewController");
					NavigationController.PushViewController (vc, true);
				} else if (indexPath.Row == 1) {
					var storyboard = UIStoryboard.FromName ("MainStoryboard", null);
					var vc = storyboard.InstantiateViewController ("TriggersViewController");
					NavigationController.PushViewController (vc, true);
				}  else if (indexPath.Row == 2) {
					var storyboard = UIStoryboard.FromName ("MainStoryboard", null);
					var vc = storyboard.InstantiateViewController ("TriggerOrientationViewController");
					NavigationController.PushViewController (vc, true);
				}  else if (indexPath.Row == 3) {
					NavigationController.PushViewController(new CloudBeaconsViewController(), true);
				}
			}
		}
	}
}

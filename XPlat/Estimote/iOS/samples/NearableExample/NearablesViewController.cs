using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using Estimote;
using System.Linq;
using CoreGraphics;

namespace NearableMonitoringExample
{
	partial class NearablesViewController : UITableViewController, IUITableViewDataSource, IUITableViewDelegate, INearableManagerDelegate
	{
		Nearable[] nearables;
		private NearableManager manager;

		public NearablesViewController (IntPtr handle) : base (handle)
		{
		}

		async public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			TableView.WeakDataSource = this;
			TableView.WeakDelegate = this;

			manager = new NearableManager ();

            manager.RangedNearables += (sender, e) => 
            {
                this.nearables = e.Nearables;
                TableView.ReloadData ();
            };

		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			manager.StartRangingForType (NearableType.All);
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);

			manager.StopRangingForType (NearableType.All);
		}


		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell ("nearable", indexPath);

			if (cell == null) {
				cell = new UITableViewCell (UITableViewCellStyle.Subtitle, "nearable");
			}

			var nearable = nearables [indexPath.Row];

			if (nearable == null)
				cell.TextLabel.Text = "Nearable is null";

			cell.TextLabel.Text = "Id: " + nearable.Identifier;
			cell.DetailTextLabel.Text = "Type: " + nearable.Type + " RSSI: " + nearable.Rssi + " " + nearable.Zone.ToString ();
		
			var imageView = new UIImageView (new CGRect (View.Frame.Size.Width - 60, 30, 30, 30));
			imageView.ContentMode = UIViewContentMode.ScaleAspectFill;
			imageView.Image = ImageForNearableType (nearable.Type);
			cell.ContentView.AddSubview (imageView);
			return cell;
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



		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return 80;
		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue (segue, sender);

			var vc = (NearableMonitoringExampleViewController)segue.DestinationViewController;
			vc.Nearable = this.nearables [TableView.IndexPathForSelectedRow.Row];

		}


		UIImage ImageForNearableType (NearableType type)
		{
			switch (type) {
			case NearableType.Bag:
				return UIImage.FromFile ("sticker_bag");
			case NearableType.Bike:
				return UIImage.FromFile ("sticker_bike");
			case NearableType.Car:
				return UIImage.FromFile ("sticker_car");
			case NearableType.Fridge:
				return UIImage.FromFile ("sticker_fridge");
			case NearableType.Bed:
				return UIImage.FromFile ("sticker_bed");
			case NearableType.Chair:
				return UIImage.FromFile ("sticker_chair");
			case NearableType.Shoe:
				return UIImage.FromFile ("sticker_shoe");
			case NearableType.Door:
				return UIImage.FromFile ("sticker_door");
			case NearableType.Dog:
				return UIImage.FromFile ("sticker_dog");
			default:
				return UIImage.FromFile ("sticker_grey");
			}
		}
	}
}

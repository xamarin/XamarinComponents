using System;
using Foundation;
using UIKit;

using AMScrollingNavbar;

namespace AMScrollingNavbarSample
{
	partial class TableViewController : ScrollingNavigationTableViewController
	{
		public TableViewController (IntPtr handle)
			: base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			NavigationItem.RightBarButtonItem = new UIBarButtonItem (UIBarButtonSystemItem.Add);
			NavigationController.NavigationBar.BarTintColor = new UIColor (0.91f, 0.3f, 0.24f, 1.0f);
		}

		public override void ViewDidAppear (bool animated)
		{
			if (ScrollingNavigationController != null) {
				// Enable the navbar scrolling
				ScrollingNavigationController.FollowScrollView (TableView, 50.0);
			}
		}

		public override nint RowsInSection (UITableView tableView, nint section)
		{
			return 100;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = TableView.DequeueReusableCell ("Cell");
			cell.TextLabel.Text = "Cell " + indexPath.Row;
			return cell;
		}
	}
}

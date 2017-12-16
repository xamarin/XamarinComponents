using System;
using Foundation;
using UIKit;

using AMScrollingNavbar;

namespace AMScrollingNavbarSample
{
	partial class TableViewController : ScrollingNavigationViewController, IUITableViewDelegate, IUITableViewDataSource
	{
		public TableViewController (IntPtr handle)
			: base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Title = "Table View";

			tableView.ContentInset = new UIEdgeInsets (44, 0, 100, 0);
			toolbar.BarTintColor = UIColor.FromRGBA (0.91f, 0.3f, 0.24f, 1f);
			toolbar.TintColor = UIColor.White;
			NavigationItem.RightBarButtonItem = new UIBarButtonItem (UIBarButtonSystemItem.Add, null, null);
			NavigationController.NavigationBar.BarTintColor = UIColor.FromRGBA (0.91f, 0.3f, 0.24f, 1f);

			NavigationItem.RightBarButtonItem = new UIBarButtonItem (UIBarButtonSystemItem.Add);
			NavigationController.NavigationBar.BarTintColor = new UIColor (0.91f, 0.3f, 0.24f, 1.0f);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			// Enable the navbar scrolling
			ScrollingNavigationController?.FollowScrollView (tableView, 50.0, new[] { toolbar });

			ScrollingNavigationController.StateChanging += OnScrollingStageChanging;
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);

			ScrollingNavigationController.StateChanging -= OnScrollingStageChanging;
		}

		private void OnScrollingStageChanging (object sender, EventArgs e)
		{
			View.NeedsUpdateConstraints ();
		}

		public nint RowsInSection (UITableView tableView, nint section)
		{
			return 100;
		}

		public UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell ("Cell");
			cell.TextLabel.Text = "Cell " + indexPath.Row;
			return cell;
		}
	}
}

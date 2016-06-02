using System;
using System.Collections.Generic;
using System.Linq;

#if __UNIFIED__
using Foundation;
using UIKit;
#else
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using CGRect = global::System.Drawing.RectangleF;
using CGSize = global::System.Drawing.SizeF;
using CGPoint = global::System.Drawing.PointF;
using nfloat = global::System.Single;
using nint = global::System.Int32;
using nuint = global::System.UInt32;
#endif

using SWTableViewCells;

namespace SWTableViewCellSample
{
	partial class ViewController : UITableViewController
	{
		private string[] sections;
		private List<string>[] testArray;
		private CellDelegate cellDelegate;
		private bool useCustomCells;

		public ViewController (IntPtr handle)
			: base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// set up the data
			Random rnd = new Random ();
			sections = UILocalizedIndexedCollation.CurrentCollation ().SectionIndexTitles;
			testArray = (
			    from section in sections
			    group section by section into g
			    select Enumerable.Range (1, rnd.Next (5) + 1).Select (i => i.ToString ()).ToList ()
			).ToArray ();

			cellDelegate = new CellDelegate (testArray, TableView);

			NavigationItem.LeftBarButtonItem = EditButtonItem;
			TableView.RowHeight = 90;
			NavigationItem.Title = "Pull to Toggle Cell Type";

			// Setup refresh control for example app
			UIRefreshControl refreshControl = new UIRefreshControl ();
			refreshControl.ValueChanged += (sender, args) => {
				refreshControl.BeginRefreshing ();
				useCustomCells = !useCustomCells;
				if (useCustomCells) {
					RefreshControl.TintColor = UIColor.Yellow;
				} else {
					RefreshControl.TintColor = UIColor.Blue;
				}

				TableView.ReloadData ();
				refreshControl.EndRefreshing ();
			};
			refreshControl.TintColor = UIColor.Blue;
			TableView.AddSubview (refreshControl);
			RefreshControl = refreshControl;

			useCustomCells = false;
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			return testArray.Length;
		}

		public override nint RowsInSection (UITableView tableView, nint section)
		{
			return testArray [section].Count ();
		}

		public override string TitleForHeader (UITableView tableView, nint section)
		{
			return sections [section];
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			if (useCustomCells) {
				UMTableViewCell cell = (UMTableViewCell)tableView.DequeueReusableCell ("UMCell");
				// we assume that only new cells have no delegate
				if (cell.Delegate == null) {
					cell.Delegate = cellDelegate;
					// optionally specify a width that each set of utility buttons will share
					cell.SetLeftUtilityButtons (LeftButtons (), 32.0f);
					cell.SetRightUtilityButtons (RightButtons (), 58.0f);
				}

				cell.Label.Text = string.Format ("Section: {0}, Seat: {1}", indexPath.Section, indexPath.Row);
				return cell;
			} else {
				SWTableViewCell cell = (SWTableViewCell)tableView.DequeueReusableCell ("Cell");
				// we assume that only new cells have no delegate
				if (cell.Delegate == null) {
					cell.Delegate = cellDelegate;
					cell.RightUtilityButtons = RightButtons ();
					cell.LeftUtilityButtons = LeftButtons ();
				}

				cell.TextLabel.Text = string.Format ("Seat: {0}", testArray [indexPath.Section] [indexPath.Row]);
				cell.DetailTextLabel.Text = string.Format ("Details for seat {1} in section: {0}.", indexPath.Section, indexPath.Row);
				return cell;
			}
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			Console.WriteLine ("cell selected at index path {0}:{1}", indexPath.Section, indexPath.Row);
			Console.WriteLine ("selected cell index path is {0}", TableView.IndexPathForSelectedRow);

			if (!tableView.Editing) {
				tableView.DeselectRow (indexPath, true);
			}
		}

		static UIButton[] RightButtons ()
		{
			NSMutableArray rightUtilityButtons = new NSMutableArray ();
			rightUtilityButtons.AddUtilityButton (UIColor.FromRGBA (0.78f, 0.78f, 0.8f, 1.0f), "More");
			rightUtilityButtons.AddUtilityButton (UIColor.FromRGBA (1.0f, 0.231f, 0.188f, 1.0f), "Delete");
			return NSArray.FromArray<UIButton> (rightUtilityButtons);
		}

		static UIButton[] LeftButtons ()
		{
			NSMutableArray leftUtilityButtons = new NSMutableArray ();
			leftUtilityButtons.AddUtilityButton (UIColor.FromRGBA (0.07f, 0.75f, 0.16f, 1.0f), UIImage.FromBundle ("check.png"));
			leftUtilityButtons.AddUtilityButton (UIColor.FromRGBA (1.0f, 1.0f, 0.35f, 1.0f), UIImage.FromBundle ("clock.png"));
			leftUtilityButtons.AddUtilityButton (UIColor.FromRGBA (1.0f, 0.231f, 0.188f, 1.0f), UIImage.FromBundle ("cross.png"));
			leftUtilityButtons.AddUtilityButton (UIColor.FromRGBA (0.55f, 0.27f, 0.07f, 1.0f), UIImage.FromBundle ("list.png"));
			return NSArray.FromArray<UIButton> (leftUtilityButtons);
		}

		class CellDelegate : SWTableViewCellDelegate
		{
			private readonly List<string>[] testArray;
			private readonly UITableView tableView;

			public CellDelegate (List<string>[] testArray, UITableView tableView)
			{
				this.testArray = testArray;
				this.tableView = tableView;
			}

			public override void ScrollingToState (SWTableViewCell cell, SWCellState state)
			{
				switch (state) {
				case SWCellState.Center:
					Console.WriteLine ("utility buttons closed");
					break;
				case SWCellState.Left:
					Console.WriteLine ("left utility buttons open");
					break;
				case SWCellState.Right:
					Console.WriteLine ("right utility buttons open");
					break;
				}
			}

			public override void DidTriggerLeftUtilityButton (SWTableViewCell cell, nint index)
			{
				Console.WriteLine ("Left button {0} was pressed.", index);

				new UIAlertView ("Left Utility Buttons", string.Format ("Left button {0} was pressed.", index), null, "OK", null).Show ();
			}

			public override void DidTriggerRightUtilityButton (SWTableViewCell cell, nint index)
			{
				Console.WriteLine ("Right button {0} was pressed.", index);

				switch (index) {
				case 0:
					// More button was pressed
					Console.WriteLine ("More button was pressed");
					new UIAlertView ("Hello", "More more more", null, "cancel", null).Show ();
					cell.HideUtilityButtons (true);
					break;
				case 1:
					// Delete button was pressed
					NSIndexPath cellIndexPath = tableView.IndexPathForCell (cell);
					testArray [cellIndexPath.Section].RemoveAt (cellIndexPath.Row);
					tableView.DeleteRows (new[] { cellIndexPath }, UITableViewRowAnimation.Left);
					break;
				}
			}

			public override bool ShouldHideUtilityButtonsOnSwipe (SWTableViewCell cell)
			{
				// allow just one cell's utility button to be open at once
				return true;
			}

			public override bool CanSwipeToState (SWTableViewCell cell, SWCellState state)
			{
				switch (state) {
				case SWCellState.Left:
						// set to false to disable all left utility buttons appearing
					return true;
				case SWCellState.Right:
						// set to false to disable all right utility buttons appearing
					return true;
				}
				return true;
			}
		}
	}
}

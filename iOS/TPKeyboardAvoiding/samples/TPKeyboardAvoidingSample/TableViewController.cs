using System;
using Foundation;
using CoreGraphics;
using UIKit;

namespace TPKeyboardAvoidingSample
{
	partial class TableViewController : UITableViewController
	{
		private const string CellIdentifier = "Cell";

		public TableViewController (IntPtr handle)
			: base (handle)
		{
		}

		public override nint RowsInSection (UITableView tableView, nint section)
		{
			return 30;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = TableView.DequeueReusableCell (CellIdentifier);
			if (cell == null) {
				cell = new UITableViewCell (UITableViewCellStyle.Default, CellIdentifier);

				var textField = new UITextField (new CGRect (0, 0, 200, 30));
				textField.BorderStyle = UITextBorderStyle.RoundedRect;
				cell.AccessoryView = textField;
				cell.SelectionStyle = UITableViewCellSelectionStyle.None;
			}

			cell.TextLabel.Text = "Label " + indexPath.Row;
			((UITextField)cell.AccessoryView).Placeholder = "Field " + indexPath.Row;

			return cell;
		}
	}
}


using System;

using Foundation;
using UIKit;

namespace DatePickerCellSample
{
	public class DateTableViewController : UITableViewController
	{
		public DateTableViewController() : base(UITableViewStyle.Grouped)
		{
		}

		public override void DidReceiveMemoryWarning()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			
			// Register the TableView's data source
			TableView.Source = new DateTableViewSource();
		}
	}
}


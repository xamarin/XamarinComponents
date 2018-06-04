using System;
using UIKit;

namespace AMScrollingNavbarSample
{
	partial class ViewController : UITableViewController
	{
		public ViewController (IntPtr handle)
			: base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Title = "Samples";

			TableView.TableFooterView = new UIView ();
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			NavigationController.NavigationBar.BarTintColor = new UIColor (0.1f, 0.1f, 0.1f, 1.0f);
		}
	}
}

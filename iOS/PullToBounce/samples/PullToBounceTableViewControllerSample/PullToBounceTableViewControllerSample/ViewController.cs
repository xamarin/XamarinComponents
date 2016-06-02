using System;
using System.Threading.Tasks;
using CoreGraphics;
using Foundation;
using UIKit;

using PullToBounce;

namespace PullToBounceTableViewControllerSample
{
    partial class ViewController : UITableViewController
    {
        private UITableView realTableView;

        public ViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override UITableView TableView
        {
            get { return realTableView; }
            set { realTableView = value; }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // keep a handle on the real table view
            realTableView = base.TableView;

            // make our view the root
            var tableViewWrapper = new PullToBounceWrapper(View.Frame);
            tableViewWrapper.BackgroundColor = UIColor.Orange;
            View = tableViewWrapper;

            // add the table view to our view
            tableViewWrapper.AddSubview(realTableView);

            // continue as normal
            tableViewWrapper.RefreshStarted += async delegate
            {
                await Task.Delay(2000);
                tableViewWrapper.StopLoadingAnimation();
            };
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return 30;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            return tableView.DequeueReusableCell("fakeCell", indexPath);
        }
    }
}

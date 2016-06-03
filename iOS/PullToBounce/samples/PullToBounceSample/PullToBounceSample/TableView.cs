using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace PullToBounceSample
{
    partial class TableView : UITableView, IUITableViewDelegate, IUITableViewDataSource
    {
        public TableView(IntPtr handle)
            : base(handle)
        {
            Delegate = this;
            DataSource = this;
        }

        [Export("tableView:numberOfRowsInSection:")]
        public nint RowsInSection(UITableView tableView, nint section)
        {
            return 30;
        }

        [Export("tableView:cellForRowAtIndexPath:")]
        public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            return tableView.DequeueReusableCell("fakeCell", indexPath);
        }
    }
}

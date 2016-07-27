//
//  Created by Shopify.
//  Copyright (c) 2016 Shopify Inc. All rights reserved.
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in
//  all copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//  THE SOFTWARE.
//

using System;
using Foundation;
using UIKit;

using Shopify;

namespace ShopifyiOSSample
{
	partial class CollectionListViewController : UITableViewController
	{
		private BUYClient client;
		private BUYCollection[] collections;

		public CollectionListViewController (IntPtr handle)
			: base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Title = "Collections";

			TableView.RegisterClassForCellReuse (typeof(UITableViewCell), "Cell");

			client = new BUYClient (AppDelegate.SHOP_DOMAIN, AppDelegate.API_KEY, AppDelegate.CHANNEL_ID);

			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
			client.GetCollections ((collections, error) => {
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;

				if (error == null && collections != null) {
					this.collections = collections;
					TableView.ReloadData ();
				} else {
					Console.WriteLine ("Error fetching products: {0}", error);
				}
			});
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			return 2;
		}

		public override nint RowsInSection (UITableView tableView, nint section)
		{
			switch (section) {
			case 1:
				return collections == null ? 0 : collections.Length;
			default:
				return 1;
			}
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell ("Cell", indexPath);
			cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
			switch (indexPath.Section) {
			case 1:
				var collection = collections [indexPath.Row];
				cell.TextLabel.Text = collection.Title;
				break;
			default:
				cell.TextLabel.Text = "All products (no collection)";
				break;
			}
			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			BUYCollection collection = null;
			if (indexPath.Section == 1) {
				collection = collections [indexPath.Row];
			}

			var productListViewController = new ProductListViewController (client, collection);
			NavigationController.PushViewController (productListViewController, true);
		}
	}
}

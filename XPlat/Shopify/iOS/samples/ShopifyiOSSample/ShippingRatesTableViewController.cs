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
	public class ShippingRatesTableViewController : UITableViewController
	{
		private readonly BUYClient client;
		private readonly BUYCheckout checkout;

		private NSNumberFormatter currencyFormatter;
		private BUYShippingRate[] shippingRates;
		private NSOperation[] allOperations;

		public ShippingRatesTableViewController (BUYClient client, BUYCheckout checkout)
		{
			this.client = client;
			this.checkout = checkout;
		}

		protected override void Dispose (bool disposing)
		{
			base.Dispose (disposing);

			foreach (var operation in allOperations) {
				operation.Cancel ();
			}
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Title = "Shipping Rates";

			// Setup both operations to run
			var shopOperation = new GetShopOperation (client);
			shopOperation.DidReceiveShop += (op, shop) => {
				currencyFormatter = new NSNumberFormatter ();
				currencyFormatter.NumberStyle = NSNumberFormatterStyle.Currency;
				currencyFormatter.CurrencyCode = shop.Currency;
			};
			shopOperation.FailedToReceiveShop += (op, error) => {
				Console.WriteLine ("Failed to retrieve shop: {0}", error);
			};
			NSOperationQueue.MainQueue.AddOperation (shopOperation);

			var shippingOperation = new GetShippingRatesOperation (client, checkout);
			shippingOperation.DidReceiveShippingRates += (op, rates) => {
				shippingRates = rates;
			};
			shippingOperation.FailedToReceiveShippingRates += (op, error) => {
				Console.WriteLine ("Failed to retrieve shipping rates: {0}", error);
			};
			NSOperationQueue.MainQueue.AddOperation (shippingOperation);

			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;

			// Ensure both operations are completed before we reload the table view
			var blockOperation = NSBlockOperation.Create (() => {
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
				TableView.ReloadData ();
			});
			blockOperation.AddDependency (shopOperation);
			blockOperation.AddDependency (shippingOperation);
			NSOperationQueue.MainQueue.AddOperation (blockOperation);

			allOperations = new NSOperation[] {
				blockOperation,
				shopOperation,
				shippingOperation
			};
		}

		public override nint RowsInSection (UITableView tableView, nint section)
		{
			return shippingRates == null ? 0 : shippingRates.Length;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell ("Cell") ?? new ShippingRateTableViewCell ("Cell");

			var shippingRate = shippingRates [indexPath.Row];
			cell.TextLabel.Text = shippingRate.Title;
			cell.DetailTextLabel.Text = currencyFormatter.StringFromNumber (shippingRate.Price);

			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			var shippingRate = shippingRates [indexPath.Row];

			checkout.ShippingRate = shippingRate;

			client.UpdateCheckout (checkout, (checkout, error) => {
				if (error == null && checkout != null) {
					var preCheckoutController = new PreCheckoutViewController (client, checkout);
					preCheckoutController.CurrencyFormatter = currencyFormatter;
					NavigationController.PushViewController (preCheckoutController, true);
				} else {
					Console.WriteLine ("Error applying checkout: {0}", error);
				}
			});
		}
	}
}

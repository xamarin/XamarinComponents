//
//  Created by Shopify.
//  Copyright (c) 2016 Shopify Inc. All rights reserved.
//  Copyright (c) 2016 Xamarin Inc. All rights reserved.
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
using System.Threading.Tasks;
using Foundation;
using UIKit;

using Shopify.Buy;

namespace ShopifyiOSSample
{
	public class ShippingRatesTableViewController : UITableViewController
	{
		private readonly BuyClient client;
		private Checkout checkout;

		private NSNumberFormatter currencyFormatter;
		private ShippingRate[] shippingRates;

		public ShippingRatesTableViewController(BuyClient client, Checkout checkout)
		{
			this.client = client;
			this.checkout = checkout;
		}

		public async override void ViewDidLoad()
		{
			base.ViewDidLoad();

			Title = "Shipping Rates";

			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;

			// Ensure both operations are completed before we reload the table view
			await Task.WhenAll(
				GetShopAsync(),
				GetRatesAsync());

			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;

			TableView.ReloadData();
		}

		private async Task GetShopAsync()
		{
			try
			{
				var shop = await client.GetShopAsync();
				currencyFormatter = new NSNumberFormatter();
				currencyFormatter.NumberStyle = NSNumberFormatterStyle.Currency;
				currencyFormatter.CurrencyCode = shop.Currency;
			}
			catch (NSErrorException ex)
			{
				Console.WriteLine("Failed to retrieve shop: {0}", ex.Error);
			}
		}

		private async Task GetRatesAsync()
		{
			// We're now fetching the rates from Shopify. This will will calculate shipping rates very similarly to how our web checkout.
			// We then turn our BUYShippingRate objects into PKShippingMethods for Apple to present to the user.

			if (checkout.RequiresShipping)
			{
				try
				{
					BuyClient.ShippingRatesForCheckoutResult result;
					while ((result = await client.GetShippingRatesForCheckoutAsync(checkout)).Status == Status.Processing)
					{
						await Task.Delay(500);
					}
					shippingRates = result.ShippingRates;
				}
				catch (NSErrorException ex)
				{
					Console.WriteLine("Failed to retrieve shipping rates: {0}", ex.Error);
				}
			}
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return shippingRates == null ? 0 : shippingRates.Length;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell("Cell") ?? new ShippingRateTableViewCell("Cell");

			var shippingRate = shippingRates[indexPath.Row];
			cell.TextLabel.Text = shippingRate.Title;
			cell.DetailTextLabel.Text = currencyFormatter.StringFromNumber(shippingRate.Price);

			return cell;
		}

		public async override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			checkout.ShippingRate = shippingRates[indexPath.Row];

			try
			{
				checkout = await client.UpdateCheckoutAsync(checkout);
				var preCheckoutController = new PreCheckoutViewController(client, checkout);
				preCheckoutController.CurrencyFormatter = currencyFormatter;
				NavigationController.PushViewController(preCheckoutController, true);
			}
			catch (NSErrorException ex)
			{
				Console.WriteLine("Error applying checkout: {0}", ex.Error);
			}
		}
	}
}

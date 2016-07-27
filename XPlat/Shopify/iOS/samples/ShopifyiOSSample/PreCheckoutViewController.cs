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
using CoreGraphics;
using Foundation;
using PassKit;
using SafariServices;
using UIKit;

using Shopify;

namespace ShopifyiOSSample
{
	public class PreCheckoutViewController : UITableViewController
	{
		private readonly BUYClient client;
		private BUYCheckout _checkout;

		private PKPaymentSummaryItem[] summaryItems;

		private enum UITableViewSections
		{
			SummaryItems,
			DiscountGiftCard,
			Continue,

			Count
		}

		private enum UITableViewDiscountGiftCardSection
		{
			Discount,
			GiftCard,

			Count
		}

		public PreCheckoutViewController (BUYClient client, BUYCheckout checkout)
			: base (UITableViewStyle.Grouped)
		{
			this.client = client;
			this.checkout = checkout;
		}

		public NSNumberFormatter CurrencyFormatter { get; set; }

		private BUYCheckout checkout {
			get { return _checkout; }
			set {
				_checkout = value;

				// We can take advantage of the PKPaymentSummaryItems used for 
				// Apple Pay to display summary items natively in our own 
				// checkout
				summaryItems = _checkout.ApplePaySummaryItems ();
			}
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Title = "Add Discount or Gift Card(s)";

			TableView.RegisterClassForCellReuse (typeof(UITableViewCell), "Cell");
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			return (int)UITableViewSections.Count;
		}

		public override nint RowsInSection (UITableView tableView, nint section)
		{
			switch ((UITableViewSections)(int)section) {
			case UITableViewSections.SummaryItems:
				return summaryItems == null ? 0 : summaryItems.Length;
			case UITableViewSections.DiscountGiftCard:
				return (int)UITableViewDiscountGiftCardSection.Count;
			default:
				return 1;
			}
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = null;

			switch ((UITableViewSections)indexPath.Section) {
			case UITableViewSections.SummaryItems:
				cell = tableView.DequeueReusableCell ("SummaryCell") ?? new SummaryItemsTableViewCell ("SummaryCell");
				var summaryItem = summaryItems [indexPath.Row];
				cell.TextLabel.Text = summaryItem.Label;
				cell.DetailTextLabel.Text = CurrencyFormatter.StringFromNumber (summaryItem.Amount);
				cell.SelectionStyle = UITableViewCellSelectionStyle.None;
					// Only show a line above the last cell
				if (indexPath.Row != summaryItems.Length - 2) {
					cell.SeparatorInset = new UIEdgeInsets (0.0f, 0.0f, 0.0f, cell.Bounds.Size.Width);
				}
				break;
			case UITableViewSections.DiscountGiftCard:
				cell = tableView.DequeueReusableCell ("Cell", indexPath);
				cell.SeparatorInset = UIEdgeInsets.Zero;
				switch ((UITableViewDiscountGiftCardSection)indexPath.Row) {
				case UITableViewDiscountGiftCardSection.Discount:
					cell.TextLabel.Text = "Add Discount";
					break;
				case UITableViewDiscountGiftCardSection.GiftCard:
					cell.TextLabel.Text = "Apply Gift Card";
					break;
				}
				cell.TextLabel.TextAlignment = UITextAlignment.Center;
				break;
			case UITableViewSections.Continue:
				cell = tableView.DequeueReusableCell ("Cell", indexPath);
				cell.TextLabel.Text = "Continue";
				cell.TextLabel.TextAlignment = UITextAlignment.Center;
				break;
			}

			cell.PreservesSuperviewLayoutMargins = false;
			cell.LayoutMargins = UIEdgeInsets.Zero;

			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			switch ((UITableViewSections)indexPath.Section) {
			case UITableViewSections.DiscountGiftCard:
				switch ((UITableViewDiscountGiftCardSection)indexPath.Row) {
				case UITableViewDiscountGiftCardSection.Discount:
					AddDiscount ();
					break;
				case UITableViewDiscountGiftCardSection.GiftCard:
					ApplyGiftCard ();
					break;
				}
				break;
			case UITableViewSections.Continue:
				ProceedToCheckout ();
				break;
			}
			tableView.DeselectRow (indexPath, true);
		}

		private void AddDiscount ()
		{
			var alertController = UIAlertController.Create ("Enter Discount Code", null, UIAlertControllerStyle.Alert);

			alertController.AddTextField (textField => {
				textField.Placeholder = "Discount Code";
			});

			alertController.AddAction (UIAlertAction.Create ("Cancel", UIAlertActionStyle.Cancel, action => {
				Console.WriteLine ("Cancel action");
			}));

			alertController.AddAction (UIAlertAction.Create ("OK", UIAlertActionStyle.Default, action => {
				var discount = new BUYDiscount (alertController.TextFields [0].Text);
				checkout.Discount = discount;

				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
				client.UpdateCheckout (checkout, (checkout, error) => {
					UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;

					if (error == null && checkout != null) {
						Console.WriteLine ("Successfully added discount");
						this.checkout = checkout;
						TableView.ReloadData ();
					} else {
						Console.WriteLine ("Error applying discount: {0}", error);
					}
				});
			}));

			PresentViewController (alertController, true, null);
		}

		private void ApplyGiftCard ()
		{
			var alertController = UIAlertController.Create ("Enter Gift Card Code", null, UIAlertControllerStyle.Alert);
		
			alertController.AddTextField (textField => {
				textField.Placeholder = "Gift Card Code";
			});
		
			alertController.AddAction (UIAlertAction.Create ("Cancel", UIAlertActionStyle.Cancel, action => {
				Console.WriteLine ("Cancel action");
			}));
		
			alertController.AddAction (UIAlertAction.Create ("OK", UIAlertActionStyle.Default, action => {

				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
				client.ApplyGiftCard (alertController.TextFields [0].Text, checkout, (checkout, error) => {
					UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;

					if (error == null && checkout != null) {
						Console.WriteLine ("Successfully added gift card");
						this.checkout = checkout;
						TableView.ReloadData ();
					} else {
						Console.WriteLine ("Error applying gift card: {0}", error);
					}
				});
			}));
		
			PresentViewController (alertController, true, null);
		}

		private void ProceedToCheckout ()
		{
			var checkoutController = new CheckoutViewController (client, checkout);
			checkoutController.CurrencyFormatter = CurrencyFormatter;
			NavigationController.PushViewController (checkoutController, true);
		}
	}
}

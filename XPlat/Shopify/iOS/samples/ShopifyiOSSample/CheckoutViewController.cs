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
	public class CheckoutViewController : UITableViewController, ISFSafariViewControllerDelegate
	{
		private readonly BUYClient client;
		private BUYCheckout _checkout;

		private BUYShop shop;
		private PKPaymentSummaryItem[] summaryItems;
		private BUYApplePayHelpers applePayHelper;

		private static BUYCreditCard CreditCard {
			get {
				var creditCard = new BUYCreditCard ();
				creditCard.Number = "4242424242424242";
				creditCard.ExpiryMonth = "12";
				creditCard.ExpiryYear = "2020";
				creditCard.Cvv = "123";
				creditCard.NameOnCard = "John Smith";

				return creditCard;
			}
		}

		public CheckoutViewController (BUYClient client, BUYCheckout checkout)
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

			Title = "Checkout";

			var footerView = new UIView (new CGRect (0, 0, View.Bounds.Width, 164));

			var creditCardButton = new UIButton (UIButtonType.RoundedRect);
			creditCardButton.SetTitle ("Checkout with Credit Card", UIControlState.Normal);
			creditCardButton.BackgroundColor = UIColor.FromRGBA (0.48f, 0.71f, 0.36f, 1.0f);
			creditCardButton.Layer.CornerRadius = 6;
			creditCardButton.SetTitleColor (UIColor.White, UIControlState.Normal);
			creditCardButton.TranslatesAutoresizingMaskIntoConstraints = false;
			creditCardButton.TouchUpInside += CheckoutWithCreditCard;
			footerView.AddSubview (creditCardButton);

			var webCheckoutButton = new UIButton (UIButtonType.RoundedRect);
			webCheckoutButton.SetTitle ("Web Checkout", UIControlState.Normal);
			webCheckoutButton.BackgroundColor = UIColor.FromRGBA (0.48f, 0.71f, 0.36f, 1.0f);
			webCheckoutButton.Layer.CornerRadius = 6;
			webCheckoutButton.SetTitleColor (UIColor.White, UIControlState.Normal);
			webCheckoutButton.TranslatesAutoresizingMaskIntoConstraints = false;
			webCheckoutButton.TouchUpInside += CheckoutOnWeb;
			footerView.AddSubview (webCheckoutButton);

			var applePayButton = BUYPaymentButton.Create (BUYPaymentButtonType.Buy, BUYPaymentButtonStyle.Black);
			applePayButton.TranslatesAutoresizingMaskIntoConstraints = false;
			applePayButton.TouchUpInside += CheckoutWithApplePay;
			footerView.AddSubview (applePayButton);

			var views = NSDictionary.FromObjectsAndKeys (new [] {
				creditCardButton,
				webCheckoutButton,
				applePayButton
			}, new [] {
				"creditCardButton",
				"webCheckoutButton",
				"applePayButton"
			});
			footerView.AddConstraints (NSLayoutConstraint.FromVisualFormat ("H:|-[creditCardButton]-|", 0, null, views));
			footerView.AddConstraints (NSLayoutConstraint.FromVisualFormat ("H:|-[webCheckoutButton]-|", 0, null, views));
			footerView.AddConstraints (NSLayoutConstraint.FromVisualFormat ("H:|-[applePayButton]-|", 0, null, views));
			footerView.AddConstraints (NSLayoutConstraint.FromVisualFormat ("V:|-[creditCardButton(44)]-[webCheckoutButton(==creditCardButton)]-[applePayButton(==creditCardButton)]-|", 0, null, views));

			TableView.TableFooterView = footerView;

			// Prefetch the shop object for Apple Pay
			client.GetShop ((shop, error) => {
				this.shop = shop;
			});
		}

		public override nint RowsInSection (UITableView tableView, nint section)
		{
			return summaryItems == null ? 0 : summaryItems.Length;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell ("SummaryCell") ?? new SummaryItemsTableViewCell ("SummaryCell");
			var summaryItem = summaryItems [indexPath.Row];
			cell.TextLabel.Text = summaryItem.Label;
			cell.DetailTextLabel.Text = CurrencyFormatter.StringFromNumber (summaryItem.Amount);
			// Only show a line above the last cell
			if (indexPath.Row != summaryItems.Length - 2) {
				cell.SeparatorInset = new UIEdgeInsets (0.0f, 0.0f, 0.0f, cell.Bounds.Size.Width);
			}

			return cell;
		}

		private void AddCreditCardToCheckout (Action<bool> callback)
		{
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
			client.StoreCreditCard (CreditCard, checkout, (checkout, paymentSessionId, error) => {
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;

				if (error == null && checkout != null) {
					Console.WriteLine ("Successfully added credit card to checkout");
					this.checkout = checkout;
					TableView.ReloadData ();
				} else {
					Console.WriteLine ("Error applying credit card: {0}", error);
				}

				callback (error == null && checkout != null);
			});
		}

		private void ShowCheckoutConfirmation ()
		{
			var alertController = UIAlertController.Create ("Checkout complete", null, UIAlertControllerStyle.Alert);

			alertController.AddAction (UIAlertAction.Create ("Start over", UIAlertActionStyle.Default, action => {
				NavigationController.PopToRootViewController (true);
			}));
			alertController.AddAction (UIAlertAction.Create ("Show order status page", UIAlertActionStyle.Default, action => {
				var safariViewController = new SFSafariViewController (checkout.Order.StatusURL);
				safariViewController.Delegate = this;
				PresentViewController (safariViewController, true, null);
			}));

			PresentViewController (alertController, true, null);
		}

		[Export ("safariViewControllerDidFinish:")]
		public virtual void SafariViewControllerDidFinish (SFSafariViewController controller)
		{
			GetCompletedCheckout (() => {
				if (checkout.Order != null) {
					InvokeOnMainThread (() => {
						ShowCheckoutConfirmation ();
					});
				}
			});
		}

		private void CheckoutWithCreditCard (object sender, EventArgs e)
		{
			// First, the credit card must be stored on the checkout
			AddCreditCardToCheckout (success => {
				if (success) {
					UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;

					// Upon successfully adding the credit card to the checkout, complete checkout must be called immediately
					client.CompleteCheckout (checkout, (checkout, error) => {
						if (error == null && checkout != null) {
							Console.WriteLine ("Successfully completed checkout");
							this.checkout = checkout;

							var completionOperation = new GetCompletionStatusOperation (client, checkout);
							completionOperation.DidReceiveCompletionStatus += (op, status) => {
								UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;

								Console.WriteLine ("Successfully got completion status: {0}", status);
								GetCompletedCheckout (null);
							};
							completionOperation.FailedToReceiveCompletionStatus += (op, err) => {
								UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;

								Console.WriteLine ("Error getting completion status: {0}", err);
							};

							UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
							NSOperationQueue.MainQueue.AddOperation (completionOperation);
						} else {
							UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
							Console.WriteLine ("Error completing checkout: {0}", error);
						}
					});
				}
			});
		}

		private void CheckoutWithApplePay (object sender, EventArgs e)
		{
			var request = GetPaymentRequest ();

			applePayHelper = new BUYApplePayHelpers (client, checkout, shop);

			var paymentController = new PKPaymentAuthorizationViewController (request);

			// Add additional methods if needed and forward the callback to BUYApplePayHelpers
			paymentController.DidAuthorizePayment += (_, args) => {
				applePayHelper.DidAuthorizePayment (paymentController, args.Payment, args.Completion);

				checkout = applePayHelper.Checkout;
				GetCompletedCheckout (null);
			};
			paymentController.PaymentAuthorizationViewControllerDidFinish += (_, args) => {
				applePayHelper.PaymentAuthorizationViewControllerDidFinish (paymentController);
			};
			paymentController.DidSelectShippingAddress += (_, args) => {
				applePayHelper.DidSelectShippingAddress (paymentController, args.Address, args.Completion);
			};
			paymentController.DidSelectShippingContact += (_, args) => {
				applePayHelper.DidSelectShippingContact (paymentController, args.Contact, args.Completion);
			};
			paymentController.DidSelectShippingMethod += (_, args) => {
				applePayHelper.DidSelectShippingMethod (paymentController, args.ShippingMethod, args.Completion);
			};

			/**
			 * 
			 *  Alternatively we can set the delegate to applePayHelper.
			 * 
			 *  If you do not care about any IPKPaymentAuthorizationViewControllerDelegate callbacks
			 *  uncomment the code below to let BUYApplePayHelpers take care of them automatically.
			 *  You can then also safely remove the IPKPaymentAuthorizationViewControllerDelegate
			 *  events above.
			 *
			 *  // paymentController.Delegate = applePayHelper;
			 *
			 *  If you keep the events as the delegate, you have a chance to intercept the
			 *  IPKPaymentAuthorizationViewControllerDelegate callbacks and add any additional logging
			 *  and method calls as you need. Ensure that you forward them to the BUYApplePayHelpers
			 *  class by calling the delegate methods on BUYApplePayHelpers which already implements
			 *  the IPKPaymentAuthorizationViewControllerDelegate interface.
			 *
			 */

			PresentViewController (paymentController, true, null);
		}

		private PKPaymentRequest GetPaymentRequest ()
		{
			var paymentRequest = new PKPaymentRequest ();
		
			paymentRequest.MerchantIdentifier = AppDelegate.MERCHANT_ID;
			paymentRequest.RequiredBillingAddressFields = PKAddressField.All;
			paymentRequest.RequiredShippingAddressFields = checkout.RequiresShipping ? PKAddressField.All : PKAddressField.Email | PKAddressField.Phone;
			paymentRequest.SupportedNetworks = new [] {
				PKPaymentNetwork.Visa,
				PKPaymentNetwork.MasterCard
			};
			paymentRequest.MerchantCapabilities = PKMerchantCapability.ThreeDS;
			paymentRequest.CountryCode = shop != null ? shop.Country : "US";
			paymentRequest.CurrencyCode = shop != null ? shop.Currency : "USD";
		
			paymentRequest.PaymentSummaryItems = checkout.ApplePaySummaryItems (shop.Name);
		
			return paymentRequest;
		}

		private void CheckoutOnWeb (object sender, EventArgs e)
		{
			IDisposable observer = null;
			observer = NSNotificationCenter.DefaultCenter.AddObserver (AppDelegate.CheckoutCallbackNotification, notification => {
				var url = (NSUrl)notification.UserInfo ["url"];
				if (PresentedViewController is SFSafariViewController) {
					DismissViewController (true, () => {
						GetCompletionStatusAndCompletedCheckoutWithURL (url);
					});
				} else {
					GetCompletionStatusAndCompletedCheckoutWithURL (url);
				}
				observer.Dispose ();
			});
		
			// On iOS 9+ we should use the SafariViewController to display the checkout in-app
			if (UIDevice.CurrentDevice.CheckSystemVersion (9, 0)) {
				var safariViewController = new SFSafariViewController (checkout.WebCheckoutURL);
				safariViewController.Delegate = this;
		
				PresentViewController (safariViewController, true, null);
			} else {
				UIApplication.SharedApplication.OpenUrl (checkout.WebCheckoutURL);
			}
		}

		private void GetCompletionStatusAndCompletedCheckoutWithURL (NSUrl url)
		{
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
		
			client.GetCompletionStatusOfCheckoutURL (url, (status, error) => {
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
		
				if (error == null && status == BUYStatus.Complete) {
					Console.WriteLine ("Successfully completed checkout");
					GetCompletedCheckout (() => {
						InvokeOnMainThread (() => {
							ShowCheckoutConfirmation ();
						});
					});
				} else {
					Console.WriteLine ("Error completing checkout: {0}", error);
				}
			});
		}

		private void GetCompletedCheckout (Action completionBlock)
		{
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;

			client.GetCheckout (checkout, (checkout, error) => {
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;

				if (error != null) {
					Console.WriteLine ("Unable to get completed checkout");
					Console.WriteLine (error);
				}
				if (checkout != null) {
					this.checkout = checkout;
					Console.WriteLine (checkout);
				}

				if (completionBlock != null) {
					completionBlock ();
				}
			});
		}
	}
}

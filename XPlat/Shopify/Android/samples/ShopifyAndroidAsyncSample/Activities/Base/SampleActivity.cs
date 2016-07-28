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
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Square.Retrofit;

using Shopify.Buy;
using Shopify.Buy.DataProvider;
using Shopify.Buy.Model;

namespace ShopifyAndroidSample.Activities.Base
{
	// Base class for all activities in the app. Manages the ProgressDialog that is displayed while network activity is occurring.
	public class SampleActivity : Activity
	{
		// The amount of time in milliseconds to delay between network calls when you are polling for Shipping Rates and Checkout Completion
		protected const int PollDelay = 500;

		private ProgressDialog progressDialog;
		private bool webCheckoutInProgress;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			InitializeProgressDialog();
		}

		protected async override void OnResume()
		{
			base.OnResume();

			// If we are being launched by a url scheme, check the scheme and retrieve the checkout token if provided
			var uri = Intent.Data;
			var scheme = GetString(Resource.String.web_return_to_scheme);

			if (uri != null && uri.Scheme == scheme)
			{
				webCheckoutInProgress = false;

				// If the app was launched using the scheme, we know we just successfully completed an order
				OnCheckoutComplete();
			}
			else
			{
				// If a Web checkout was previously launched, we should check its status
				if (webCheckoutInProgress && SampleApplication.Checkout != null)
				{
					await PollCheckoutCompletionStatusAsync(SampleApplication.Checkout);
				}
			}
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();

			progressDialog?.Dismiss();
		}

		protected SampleApplication SampleApplication
		{
			get { return (SampleApplication)Application; }
		}

		// Initializes a simple progress dialog that gets presented while the app is communicating with the server.
		private void InitializeProgressDialog()
		{
			progressDialog?.Dismiss();

			progressDialog = new ProgressDialog(this);
			progressDialog.Indeterminate = true;
			progressDialog.SetTitle(GetString(Resource.String.please_wait));
			progressDialog.SetCancelable(true);
			progressDialog.CancelEvent += delegate
			{
				Finish();
			};
		}

		// Present the progress dialog.
		protected void ShowLoadingDialog(int messageId)
		{
			progressDialog.SetMessage(GetString(messageId));
			progressDialog.Show();
		}

		protected void DismissLoadingDialog()
		{
			progressDialog.Dismiss();
		}

		protected void OnError(RetrofitError error)
		{
			OnError(BuyClient.GetErrorBody(error));
		}

		// When we encounter an error with one of our network calls, we abort and return to the previous activity.
		// In a production app, you'll want to handle these types of errors more gracefully.
		protected void OnError(string errorMessage)
		{
			progressDialog.Dismiss();
			Console.WriteLine("Error: " + errorMessage);
			Toast.MakeText(this, Resource.String.error, ToastLength.Long).Show();
			Finish();
		}

		// Use the latest Checkout objects details to populate the text views in the order summary section.
		protected void UpdateOrderSummary()
		{
			var checkout = SampleApplication.Checkout;

			if (checkout == null)
			{
				return;
			}

			FindViewById<TextView>(Resource.Id.line_item_price_value).Text = checkout.Currency + " " + checkout.LineItems[0].Price;

			var totalDiscount = 0.0;
			var discount = checkout.Discount;
			if (discount != null && !string.IsNullOrEmpty(discount.Amount))
			{
				totalDiscount += double.Parse(discount.Amount);
			}
			FindViewById<TextView>(Resource.Id.discount_value).Text = "-" + checkout.Currency + " " + totalDiscount;

			var totalGiftCards = 0.0;
			var giftCards = checkout.GiftCards;
			if (giftCards != null)
			{
				foreach (var giftCard in giftCards)
				{
					if (!string.IsNullOrEmpty(giftCard.AmountUsed))
					{
						totalGiftCards += double.Parse(giftCard.AmountUsed);
					}
				}
			}
			FindViewById<TextView>(Resource.Id.gift_card_value).Text = "-" + checkout.Currency + " " + totalGiftCards;
			FindViewById<TextView>(Resource.Id.taxes_value).Text = checkout.Currency + " " + checkout.TotalTax;
			FindViewById<TextView>(Resource.Id.total_value).Text = checkout.Currency + " " + checkout.PaymentDue;

			if (checkout.ShippingRate != null)
			{
				FindViewById<TextView>(Resource.Id.shipping_value).Text = checkout.Currency + " " + checkout.ShippingRate.Price;
			}
			else
			{
				FindViewById<TextView>(Resource.Id.shipping_value).Text = "N/A";
			}
		}

		// Polls until the web checkout has completed.
		protected async Task PollCheckoutCompletionStatusAsync(Checkout checkout)
		{
			ShowLoadingDialog(Resource.String.getting_checkout_status);

			try
			{
				bool complete;
				while (!(complete = await SampleApplication.GetCheckoutCompletionStatusAsync()))
				{
					await Task.Delay(PollDelay);
				}
				DismissLoadingDialog();
				OnCheckoutComplete();
			}
			catch (ShopifyException ex)
			{
				OnError(ex.Error);
			}
		}

		// When our polling determines that the checkout is completely processed, show a toast.
		private void OnCheckoutComplete()
		{
			DismissLoadingDialog();
			webCheckoutInProgress = false;

			Toast.MakeText(this, Resource.String.checkout_complete, ToastLength.Long).Show();
		}
	}
}

using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Square.Retrofit;

using Shopify.Buy.DataProvider;
using Shopify.Buy.Model;

namespace ShopifyAndroidSample.Activities.Base
{
	// Base class for all activities in the app. Manages the ProgressDialog that is displayed while network activity is occurring.
	public class SampleActivity : Activity
	{
		// The amount of time in milliseconds to delay between network calls when you are polling for Shipping Rates and Checkout Completion
		protected const long PollDelay = 500;

		protected Handler pollingHandler;

		private ProgressDialog progressDialog;
		private bool webCheckoutInProgress;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			pollingHandler = new Handler();

			InitializeProgressDialog();
		}

		protected override void OnResume()
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
					PollCheckoutCompletionStatus(SampleApplication.Checkout);
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
			RunOnUiThread(() =>
			{
				progressDialog.SetMessage(GetString(messageId));
				progressDialog.Show();
			});
		}

		protected void DismissLoadingDialog()
		{
			RunOnUiThread(() =>
			{
				progressDialog.Dismiss();
			});
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
			else {
				FindViewById<TextView>(Resource.Id.shipping_value).Text = "N/A";
			}
		}

		// Polls until the web checkout has completed.
		protected void PollCheckoutCompletionStatus(Checkout checkout)
		{
			ShowLoadingDialog(Resource.String.getting_checkout_status);

			SampleApplication.GetCheckoutCompletionStatus(
				(complete, response) =>
				{
					if (complete)
					{
						DismissLoadingDialog();
						OnCheckoutComplete();
					}
					else
					{
						pollingHandler.PostDelayed(() =>
						{
							PollCheckoutCompletionStatus(checkout);
						}, PollDelay);
					}
				},
				OnError);
		}

		// When our polling determines that the checkout is completely processed, show a toast.
		private void OnCheckoutComplete()
		{
			DismissLoadingDialog();
			webCheckoutInProgress = false;

			RunOnUiThread(() =>
			{
				Toast.MakeText(this, Resource.String.checkout_complete, ToastLength.Long).Show();
			});
		}
	}
}

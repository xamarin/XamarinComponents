using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Shopify.Buy.Model;

using ShopifyAndroidSample.Activities.Base;

namespace ShopifyAndroidSample.Activities
{
	// The final activity in the app flow. Allows the user to choose between:
	// 1. A native checkout where the payment info is hardcoded and the chekcout is completed within the app; or
	// 2. A web checkout where the user enters their payment info and completes the checkout in a web browser
	[Activity]
	public class CheckoutActivity : SampleActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetTitle(Resource.String.checkout);
			SetContentView(Resource.Layout.checkout_activity);

			var nativeCheckoutButton = FindViewById<Button>(Resource.Id.native_checkout_button);
			nativeCheckoutButton.Click += OnNativeCheckoutButtonClicked;

			var webCheckoutButton = FindViewById<Button>(Resource.Id.web_checkout_button);
			webCheckoutButton.Click += OnWebCheckoutButtonClicked;

			UpdateOrderSummary();
		}

		// For our sample native checkout, we use a hardcoded credit card.
		private void OnNativeCheckoutButtonClicked(object sender, EventArgs e)
		{
			// Create the card to send to Shopify.  This is hardcoded here for simplicity, but the user should be prompted for their credit card information.
			var creditCard = new CreditCard
			{
				FirstName = "Dinosaur",
				LastName = "Banana",
				Month = "2",
				Year = "20",
				VerificationValue = "123",
				Number = "4242424242424242"
			};

			ShowLoadingDialog(Resource.String.completing_checkout);
			SampleApplication.StoreCreditCard(creditCard, delegate
			{
				// When the credit card has successfully been added, complete the checkout and begin polling.
				SampleApplication.CompleteCheckout((checkout, response) =>
				{
					DismissLoadingDialog();
					PollCheckoutCompletionStatus(checkout);
				}, OnError);
			}, OnError);
		}

		// Launch the device browser so the user can complete the checkout.
		private void OnWebCheckoutButtonClicked(object sender, EventArgs e)
		{
			var intent = new Intent(Intent.ActionView);
			intent.AddFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
			intent.SetData(Android.Net.Uri.Parse(SampleApplication.Checkout.WebUrl));

			try
			{
				intent.SetPackage("com.android.chrome");
				StartActivity(intent);
			}
			catch (Exception)
			{
				try
				{
					// Chrome could not be opened, attempt to us other launcher
					intent.SetPackage(null);
					StartActivity(intent);
				}
				catch (Exception)
				{
					OnError(GetString(Resource.String.checkout_error));
				}
			}
		}
	}
}

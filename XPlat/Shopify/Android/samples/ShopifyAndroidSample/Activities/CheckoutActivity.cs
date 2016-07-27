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

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
using Android.Views;
using Android.Widget;

using Shopify.Buy;

using ShopifyAndroidSample.Activities.Base;

namespace ShopifyAndroidSample.Activities
{
	// After a shipping rate is selected, this activity allows the user to add discount codes or gift card codes to the order.
	// It also shows a summary of the order, including the line item price, any discounts or gift cards used, the shipping charge, the taxes, and the total price.
	[Activity]
	public class DiscountActivity : SampleActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetTitle(Resource.String.apply_discounts);
			SetContentView(Resource.Layout.discount_activity);

			var discountButton = FindViewById<Button>(Resource.Id.discount_button);
			discountButton.Click += delegate
			{
				ShowTextEntryDialog(Resource.String.discount_code, Resource.String.enter_discount_code, Resource.String.apply_discount, SetDiscountCodeAsync);
			};

			var giftCardButton = FindViewById<Button>(Resource.Id.gift_card_button);
			giftCardButton.Click += delegate
			{
				ShowTextEntryDialog(Resource.String.gift_card_code, Resource.String.enter_gift_card_code, Resource.String.apply_gift_card, AddGiftCardCodeAsync);
			};

			var checkoutButton = FindViewById<Button>(Resource.Id.checkout_button);
			checkoutButton.Click += delegate
			{
				// When the checkout button is clicked, proceed to the checkout activity(final activity in the app flow).
				StartActivity(new Intent(this, typeof(CheckoutActivity)));
			};

			UpdateOrderSummary();
		}

		// Displays a simple dialog with an EditText field and a single button, allowing the user to enter either a gift card code or a discount code.
		private void ShowTextEntryDialog(int hint, int title, int button, Func<string, Task> listener)
		{
			var dialogView = LayoutInflater.From(this).Inflate(Resource.Layout.dialog_code_entry, null);
			var editText = dialogView.FindViewById<EditText>(Resource.Id.edit_text);
			editText.SetHint(hint);

			new AlertDialog.Builder(this)
				.SetView(dialogView)
				.SetCancelable(true)
				.SetTitle(title)
				.SetPositiveButton(button, async delegate
				{
					await listener(editText.Text);
				})
				.Create()
				.Show();
		}

		// Add the discount code to the checkout and update the order summary when the request completes.
		private async Task SetDiscountCodeAsync(string discountCode)
		{
			ShowLoadingDialog(Resource.String.syncing_data);
			try
			{
				await SampleApplication.SetDiscountCodeAsync(discountCode);
				UpdateOrderSummary();
			}
			catch (ShopifyException ex)
			{
				Toast.MakeText(this, GetString(Resource.String.discount_error, discountCode), ToastLength.Long).Show();
			}
			DismissLoadingDialog();
		}


		// Add the gift card code to the checkout and update the order summary when the request completes.
		private async Task AddGiftCardCodeAsync(string giftCardCode)
		{
			ShowLoadingDialog(Resource.String.syncing_data);
			try
			{
				await SampleApplication.AddGiftCardAsync(giftCardCode);
				UpdateOrderSummary();
			}
			catch (ShopifyException ex)
			{
				Toast.MakeText(this, GetString(Resource.String.gift_card_error, giftCardCode), ToastLength.Long).Show();
			}
			DismissLoadingDialog();
		}
	}
}

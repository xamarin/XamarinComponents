using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

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
				ShowTextEntryDialog(Resource.String.discount_code, Resource.String.enter_discount_code, Resource.String.apply_discount, SetDiscountCode);
			};

			var giftCardButton = FindViewById<Button>(Resource.Id.gift_card_button);
			giftCardButton.Click += delegate
			{
				ShowTextEntryDialog(Resource.String.gift_card_code, Resource.String.enter_gift_card_code, Resource.String.apply_gift_card, AddGiftCardCode);
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
		private void ShowTextEntryDialog(int hint, int title, int button, Action<string> listener)
		{
			var dialogView = LayoutInflater.From(this).Inflate(Resource.Layout.dialog_code_entry, null);
			var editText = dialogView.FindViewById<EditText>(Resource.Id.edit_text);
			editText.SetHint(hint);

			new AlertDialog.Builder(this)
				.SetView(dialogView)
				.SetCancelable(true)
				.SetTitle(title)
				.SetPositiveButton(button, delegate
				{
					listener(editText.Text);
				})
				.Create()
				.Show();
		}

		// Add the discount code to the checkout and update the order summary when the request completes.
		private void SetDiscountCode(string discountCode)
		{
			ShowLoadingDialog(Resource.String.syncing_data);
			SampleApplication.SetDiscountCode(discountCode,
				delegate
				{
					DismissLoadingDialog();
					UpdateOrderSummary();
				},
				delegate
				{
					DismissLoadingDialog();
					Toast.MakeText(this, GetString(Resource.String.discount_error, discountCode), ToastLength.Long).Show();
				});
		}


		// Add the gift card code to the checkout and update the order summary when the request completes.
		private void AddGiftCardCode(string giftCardCode)
		{
			ShowLoadingDialog(Resource.String.syncing_data);
			SampleApplication.AddGiftCard(giftCardCode,
				delegate
				{
					DismissLoadingDialog();
					UpdateOrderSummary();
				},
				delegate
				{
					DismissLoadingDialog();
					Toast.MakeText(this, GetString(Resource.String.gift_card_error, giftCardCode), ToastLength.Long).Show();
				});
		}
	}
}

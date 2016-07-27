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

using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Java.Net;
using Shopify.Buy.Model;

using ShopifyAndroidSample;
using ShopifyAndroidSample.Activities.Base;

namespace ShopifyAndroidSample.Activities
{
	// If the selected product requires shipping, this activity allows the user to select a list of shipping rates.
	// For the sample app, the shipping address has been hardcoded and we will only see the shipping rates applicable to that address.
	[Activity]
	public class ShippingRateListActivity : SampleListActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetTitle(Resource.String.choose_shipping_rate);
		}

		protected override void OnResume()
		{
			base.OnResume();

			// If we haven't already loaded the products from the store, do it now
			if (listView.Adapter == null && !isFetching)
			{
				isFetching = true;
				ShowLoadingDialog(Resource.String.loading_data);
				FetchShippingRates();
			}
		}

		// Fetching shipping rates requires communicating with 3rd party shipping servers, so we need to poll until all the rates have been fetched.
		private void FetchShippingRates()
		{
			SampleApplication.GetShippingRates(
				(shippingRates, response) =>
				{
					if (response.Status == (int)HttpStatus.Accepted)
					{
						// Poll until the server either fails or returns HttpStatus.SC_ACCEPTED
						pollingHandler.PostDelayed(() =>
						{
							FetchShippingRates();
						}, PollDelay);

					}
					else if (response.Status == (int)HttpStatus.Ok)
					{
						isFetching = false;

						OnFetchedShippingRates(shippingRates.ToList());
					}
					else {
						isFetching = false;

						// Handle error
						OnError(response.Reason);
					}
				},
				error =>
				{
					isFetching = false;

					// Handle error
					OnError(error);
				});
		}

		private class ShippingRateAdaptor : ArrayAdapter<ShippingRate>
		{
			public ShippingRateAdaptor(Context context, int textViewResourceId, IList<ShippingRate> objects)
				: base(context, textViewResourceId, objects)
			{
			}

			public override View GetView(int position, View convertView, ViewGroup parent)
			{
				var view = convertView;
				if (view == null)
				{
					view = View.Inflate(Context, Resource.Layout.shipping_rate_list_item, null);
				}

				var rate = GetItem(position);
				view.FindViewById<TextView>(Resource.Id.list_item_left_text).Text = rate.Title;
				var checkout = ((SampleApplication)Context.ApplicationContext).Checkout;
				view.FindViewById<TextView>(Resource.Id.list_item_right_text).Text = checkout.Currency + " " + rate.Price;

				return view;
			}
		}

		// Once the shipping rates have been fetched, display the name and price of each rate in the list.
		private void OnFetchedShippingRates(List<ShippingRate> shippingRates)
		{
			// The application should surface to the user that their items cannot be shipped to that location
			if (shippingRates.Count == 0)
			{
				Toast.MakeText(this, Resource.String.no_shipping_rates, ToastLength.Long).Show();
				Finish();
			}
			else
			{
				DismissLoadingDialog();

				listView.Adapter = new ShippingRateAdaptor(this, Resource.Layout.shipping_rate_list_item, shippingRates);

				listView.ItemClick += (sender, e) =>
				{
					OnShippingRateSelected(shippingRates[e.Position]);
				};
			}
		}

		// When the user selects a shipping rate, set that rate on the checkout and proceed to the discount activity.
		private void OnShippingRateSelected(ShippingRate shippingRate)
		{
			ShowLoadingDialog(Resource.String.syncing_data);

			SampleApplication.SetShippingRate(shippingRate, delegate
			{
				DismissLoadingDialog();
				StartActivity(new Intent(this, typeof(DiscountActivity)));
			}, OnError);
		}
	}
}

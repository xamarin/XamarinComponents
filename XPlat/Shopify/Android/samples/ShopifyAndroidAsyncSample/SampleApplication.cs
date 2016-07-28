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
using System.Collections.Generic;
using Android.App;
using Android.Runtime;
using Android.Widget;
using Square.Retrofit;
using Square.Retrofit.Client;
using Shopify.Buy.DataProvider;
using Shopify.Buy.Model;
using Shopify.Buy.UI;
using System.Threading.Tasks;

namespace ShopifyAndroidSample
{
	// Application class that maintains instances of BuyClient and Checkout for the lifetime of the app.
	[Application]
	public class SampleApplication : Application
	{
		protected SampleApplication(IntPtr javaReference, JniHandleOwnership transfer)
			: base(javaReference, transfer)
		{
		}

		public BuyClient BuyClient { get; private set; }
		public Shop Shop { get; private set; }
		public Checkout Checkout { get; private set; }

		public async override void OnCreate()
		{
			base.OnCreate();

			await InitializeBuyClient();
		}

		private async Task InitializeBuyClient()
		{
			var shopUrl = GetString(Resource.String.shop_url);
			if (string.IsNullOrEmpty(shopUrl))
			{
				throw new ArgumentException("You must populate the 'shop_url' entry in strings.xml, in the form '<myshop>.myshopify.com'");
			}

			var shopifyApiKey = GetString(Resource.String.shopify_api_key);
			if (string.IsNullOrEmpty(shopifyApiKey))
			{
				throw new ArgumentException("You must populate the 'shopify_api_key' entry in strings.xml");
			}

			var channelId = GetString(Resource.String.channel_id);
			if (string.IsNullOrEmpty(channelId))
			{
				throw new ArgumentException("You must populate the 'channel_id' entry in the strings.xml");
			}

			var applicationName = PackageName;

			// Create the BuyClient
			BuyClient = BuyClientFactory.GetBuyClient(shopUrl, shopifyApiKey, channelId, applicationName);

			try
			{
				Shop = await BuyClient.GetShopAsync();
			}
			catch
			{
				Toast.MakeText(this, Resource.String.shop_error, ToastLength.Long).Show();
			}
		}

		public Task<IEnumerable<Collection>> GetCollectionsAsync()
		{
			return BuyClient.GetCollectionsAsync();
		}

		public Task<IEnumerable<Product>> GetAllProductsAsync()
		{
			// For this sample app, "all" products will just be the first page of products
			return BuyClient.GetProductPageAsync(1);
		}

		public Task<IEnumerable<Product>> GetProductsAsync(string collectionId)
		{
			// For this sample app, we'll just fetch the first page of products in the collection
			return BuyClient.GetProductsAsync(1, collectionId);
		}

		// Create a new checkout with the selected product. For convenience in the sample app we will hardcode the user's shipping address.
		// The shipping rates fetched in ShippingRateListActivity will be for this address.
		public async Task<Checkout> CreateCheckoutAsync(Product product)
		{
			var cart = new Cart();
			cart.AddVariant(product.Variants[0]);

			Checkout = new Checkout(cart);
			Checkout.ShippingAddress = new Address
			{
				FirstName = "Dinosaur",
				LastName = "Banana",
				Address1 = "421 8th Ave",
				City = "New York",
				Province = "NY",
				Zip = "10001",
				CountryCode = "US"
			};
			Checkout.Email = "something@somehost.com";
			Checkout.SetWebReturnToUrl(GetString(Resource.String.web_return_to_url));
			Checkout.SetWebReturnToLabel(GetString(Resource.String.web_return_to_label));

			Checkout = await BuyClient.CreateCheckoutAsync(Checkout);
			return Checkout;
		}

		public Task<IEnumerable<ShippingRate>> GetShippingRatesAsync()
		{
			return BuyClient.GetShippingRatesAsync(Checkout.Token);
		}

		public async Task<Checkout> SetShippingRateAsync(ShippingRate shippingRate)
		{
			Checkout.ShippingRate = shippingRate;
			Checkout = await BuyClient.UpdateCheckoutAsync(Checkout);
			return Checkout;
		}

		public async Task<Checkout> SetDiscountCodeAsync(string code)
		{
			Checkout.SetDiscountCode(code);
			Checkout = await BuyClient.UpdateCheckoutAsync(Checkout);
			return Checkout;
		}

		public async Task<Checkout> AddGiftCardAsync(string code)
		{
			Checkout = await BuyClient.ApplyGiftCardAsync(code, Checkout);
			return Checkout;
		}

		public async Task<Checkout> StoreCreditCardAsync(CreditCard card)
		{
			Checkout.BillingAddress = Checkout.ShippingAddress;
			Checkout = await BuyClient.StoreCreditCardAsync(card, Checkout);
			return Checkout;
		}

		public async Task<Checkout> CompleteCheckoutAsync()
		{
			Checkout = await BuyClient.CompleteCheckoutAsync(Checkout);
			return Checkout;
		}

		public Task<bool> GetCheckoutCompletionStatusAsync()
		{
			return BuyClient.GetCheckoutCompletionStatusAsync(Checkout);
		}

		public void LaunchProductDetailsActivity(Activity activity, Product product, ProductDetailsTheme theme)
		{
			var builder = new ProductDetailsBuilder(this, BuyClient);
			var intent = builder.SetShopDomain(BuyClient.ShopDomain)
					.SetProduct(product)
					.SetTheme(theme)
					.SetShop(Shop)
					.SetWebReturnToUrl(GetString(Resource.String.web_return_to_url))
					.SetWebReturnToLabel(GetString(Resource.String.web_return_to_label))
					.Build();
			activity.StartActivityForResult(intent, 1);
		}
	}
}

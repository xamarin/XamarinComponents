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

		public override void OnCreate()
		{
			base.OnCreate();

			InitializeBuyClient();
		}

		private void InitializeBuyClient()
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

			BuyClient.GetShop(
				(shop, response) =>
				{
					Shop = shop;
				},
				(error) =>
				{
					Toast.MakeText(this, Resource.String.shop_error, ToastLength.Long).Show();
				});
		}

		public void GetCollections(Action<IEnumerable<Collection>, Response> success, Action<RetrofitError> failure)
		{
			BuyClient.GetCollections(success, failure);
		}

		public void GetAllProducts(Action<IEnumerable<Product>, Response> success, Action<RetrofitError> failure)
		{
			// For this sample app, "all" products will just be the first page of products
			BuyClient.GetProductPage(1, success, failure);
		}

		public void GetProducts(string collectionId, Action<IEnumerable<Product>, Response> success, Action<RetrofitError> failure)
		{
			// For this sample app, we'll just fetch the first page of products in the collection
			BuyClient.GetProducts(1, collectionId, success, failure);
		}

		// Create a new checkout with the selected product. For convenience in the sample app we will hardcode the user's shipping address.
		// The shipping rates fetched in ShippingRateListActivity will be for this address.
		public void CreateCheckout(Product product, Action<Checkout, Response> success, Action<RetrofitError> failure)
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

			BuyClient.CreateCheckout(Checkout, (data, response) =>
			{
				Checkout = data;
				success(data, response);
			}, failure);
		}

		public void GetShippingRates(Action<IEnumerable<ShippingRate>, Response> success, Action<RetrofitError> failure)
		{
			BuyClient.GetShippingRates(Checkout.Token, success, failure);
		}

		public void SetShippingRate(ShippingRate shippingRate, Action<Checkout, Response> success, Action<RetrofitError> failure)
		{
			Checkout.ShippingRate = shippingRate;
			BuyClient.UpdateCheckout(Checkout, (data, response) =>
			{
				Checkout = data;
				success(data, response);
			}, failure);
		}

		public void SetDiscountCode(string code, Action<Checkout, Response> success, Action<RetrofitError> failure)
		{
			Checkout.SetDiscountCode(code);
			BuyClient.UpdateCheckout(Checkout, (data, response) =>
			{
				Checkout = data;
				success(data, response);
			}, failure);
		}

		public void AddGiftCard(string code, Action<Checkout, Response> success, Action<RetrofitError> failure)
		{
			BuyClient.ApplyGiftCard(code, Checkout, (data, response) =>
			{
				Checkout = data;
				success(data, response);
			}, failure);
		}

		public void StoreCreditCard(CreditCard card, Action<Checkout, Response> success, Action<RetrofitError> failure)
		{
			Checkout.BillingAddress = Checkout.ShippingAddress;
			BuyClient.StoreCreditCard(card, Checkout, (data, response) =>
			{
				Checkout = data;
				success(data, response);
			}, failure);
		}

		public void CompleteCheckout(Action<Checkout, Response> success, Action<RetrofitError> failure)
		{
			BuyClient.CompleteCheckout(Checkout, (data, response) =>
			{
				Checkout = data;
				success(data, response);
			}, failure);
		}

		public void GetCheckoutCompletionStatus(Action<bool, Response> success, Action<RetrofitError> failure)
		{
			BuyClient.GetCheckoutCompletionStatus(Checkout, success, failure);
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

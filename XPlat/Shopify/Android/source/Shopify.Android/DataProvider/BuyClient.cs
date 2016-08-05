using System;
using Square.Retrofit;
using Square.Retrofit.Client;
using Android.Runtime;

using Shopify.Buy.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopify.Buy.DataProvider
{
	partial class BuyClient
	{
		// delegate methods

		public void ApplyGiftCard(string giftCardCode, Checkout checkout, Action<Checkout, Response> success, Action<RetrofitError> failure)
		{
			ApplyGiftCard(giftCardCode, checkout, new RetrofitCallback<Checkout>(success, failure));
		}

		public void CompleteCheckout(Checkout checkout, Action<Checkout, Response> success, Action<RetrofitError> failure)
		{
			CompleteCheckout(checkout, new RetrofitCallback<Checkout>(success, failure));
		}

		public void CreateCheckout(Checkout checkout, Action<Checkout, Response> success, Action<RetrofitError> failure)
		{
			CreateCheckout(checkout, new RetrofitCallback<Checkout>(success, failure));
		}

		public void GetCheckout(string checkoutToken, Action<Checkout, Response> success, Action<RetrofitError> failure)
		{
			GetCheckout(checkoutToken, new RetrofitCallback<Checkout>(success, failure));
		}

		public void GetCheckoutCompletionStatus(Checkout checkout, Action<bool, Response> success, Action<RetrofitError> failure)
		{
			GetCheckoutCompletionStatus(checkout, new RetrofitCallback<Java.Lang.Boolean>(
				(data, response) => success(data.BooleanValue(), response),
				failure));
		}

		public void GetCollectionPage(int page, Action<IEnumerable<Collection>, Response> success, Action<RetrofitError> failure)
		{
			GetCollectionPage(page, new RetrofitCallback<JavaList<Collection>>(
				(data, response) => success(data, response),
				failure));
		}

		public void GetCollections(Action<IEnumerable<Collection>, Response> success, Action<RetrofitError> failure)
		{
			GetCollections(new RetrofitCallback<JavaList<Collection>>(
				(data, response) => success(data, response),
				failure));
		}

		public void GetProduct(string productId, Action<Product, Response> success, Action<RetrofitError> failure)
		{
			GetProduct(productId, new RetrofitCallback<Product>(success, failure));
		}

		public void GetProductPage(int page, Action<IEnumerable<Product>, Response> success, Action<RetrofitError> failure)
		{
			GetProductPage(page, new RetrofitCallback<JavaList<Product>>(
				(data, response) => success(data, response),
				failure));
		}

		public void GetProducts(IList<string> productIds, Action<IEnumerable<Product>, Response> success, Action<RetrofitError> failure)
		{
			GetProducts(productIds, new RetrofitCallback<JavaList<Product>>(
				(data, response) => success(data, response),
				failure));
		}

		public void GetProducts(int page, string collectionId, Action<IEnumerable<Product>, Response> success, Action<RetrofitError> failure)
		{
			GetProducts(page, collectionId, new RetrofitCallback<JavaList<Product>>(
				(data, response) => success(data, response),
				failure));
		}

		public void GetProducts(int page, string collectionId, Collection.SortOrder sortOrder, Action<IEnumerable<Product>, Response> success, Action<RetrofitError> failure)
		{
			GetProducts(page, collectionId, sortOrder, new RetrofitCallback<JavaList<Product>>(
				(data, response) => success(data, response),
				failure));
		}

		public void GetShippingRates(string checkoutToken, Action<IEnumerable<ShippingRate>, Response> success, Action<RetrofitError> failure)
		{
			GetShippingRates(checkoutToken, new RetrofitCallback<JavaList<ShippingRate>>(
				(data, response) => success(data, response),
				failure));
		}

		public void GetShop(Action<Shop, Response> success, Action<RetrofitError> failure)
		{
			GetShop(new RetrofitCallback<Shop>(success, failure));
		}

		public void RemoveGiftCard(GiftCard giftCard, Checkout checkout, Action<Checkout, Response> success, Action<RetrofitError> failure)
		{
			RemoveGiftCard(giftCard, checkout, new RetrofitCallback<Checkout>(success, failure));
		}

		public void RemoveProductReservationsFromCheckout(Checkout checkout, Action<Checkout, Response> success, Action<RetrofitError> failure)
		{
			RemoveProductReservationsFromCheckout(checkout, new RetrofitCallback<Checkout>(success, failure));
		}

		public void StoreCreditCard(CreditCard card, Checkout checkout, Action<Checkout, Response> success, Action<RetrofitError> failure)
		{
			StoreCreditCard(card, checkout, new RetrofitCallback<Checkout>(success, failure));
		}

		public void TestIntegration(Action<Response> success, Action<RetrofitError> failure)
		{
			TestIntegration(new RetrofitCallback<Java.Lang.Void>(
				(data, response) => success(response),
				failure));
		}

		public void UpdateCheckout(Checkout checkout, Action<Checkout, Response> success, Action<RetrofitError> failure)
		{
			UpdateCheckout(checkout, new RetrofitCallback<Checkout>(success, failure));
		}

		// async methods

		public Task<Checkout> ApplyGiftCardAsync(string giftCardCode, Checkout checkout)
		{
			var callback = new RetrofitCallbackAsync<Checkout>();
			ApplyGiftCard(giftCardCode, checkout, callback);
			return callback.Task;
		}

		public Task<Checkout> CompleteCheckoutAsync(Checkout checkout)
		{
			var callback = new RetrofitCallbackAsync<Checkout>();
			CompleteCheckout(checkout, callback);
			return callback.Task;
		}

		public Task<Checkout> CreateCheckoutAsync(Checkout checkout)
		{
			var callback = new RetrofitCallbackAsync<Checkout>();
			CreateCheckout(checkout, callback);
			return callback.Task;
		}

		public Task<Checkout> GetCheckoutAsync(string checkoutToken)
		{
			var callback = new RetrofitCallbackAsync<Checkout>();
			GetCheckout(checkoutToken, callback);
			return callback.Task;
		}

		public Task<bool> GetCheckoutCompletionStatusAsync(Checkout checkout)
		{
			var callback = new RetrofitCallbackAsync<Java.Lang.Boolean, bool>(data => data.BooleanValue());
			GetCheckoutCompletionStatus(checkout, callback);
			return callback.Task;
		}

		public Task<IEnumerable<Collection>> GetCollectionPageAsync(int page)
		{
			var callback = new RetrofitCallbackAsync<JavaList<Collection>, IEnumerable<Collection>>(data => data);
			GetCollectionPage(page, callback);
			return callback.Task;
		}

		public Task<IEnumerable<Collection>> GetCollectionsAsync()
		{
			var callback = new RetrofitCallbackAsync<JavaList<Collection>, IEnumerable<Collection>>(data => data);
			GetCollections(callback);
			return callback.Task;
		}

		public Task<Product> GetProductAsync(string productId)
		{
			var callback = new RetrofitCallbackAsync<Product>();
			GetProduct(productId, callback);
			return callback.Task;
		}

		public Task<IEnumerable<Product>> GetProductPageAsync(int page)
		{
			var callback = new RetrofitCallbackAsync<JavaList<Product>, IEnumerable<Product>>(data => data);
			GetProductPage(page, callback);
			return callback.Task;
		}

		public Task<IEnumerable<Product>> GetProductsAsync(IList<string> productIds)
		{
			var callback = new RetrofitCallbackAsync<JavaList<Product>, IEnumerable<Product>>(data => data);
			GetProducts(productIds, callback);
			return callback.Task;
		}

		public Task<IEnumerable<Product>> GetProductsAsync(int page, string collectionId)
		{
			var callback = new RetrofitCallbackAsync<JavaList<Product>, IEnumerable<Product>>(data => data);
			GetProducts(page, collectionId, callback);
			return callback.Task;
		}

		public Task<IEnumerable<Product>> GetProductsAsync(int page, string collectionId, Collection.SortOrder sortOrder)
		{
			var callback = new RetrofitCallbackAsync<JavaList<Product>, IEnumerable<Product>>(data => data);
			GetProducts(page, collectionId, sortOrder, callback);
			return callback.Task;
		}

		public Task<IEnumerable<ShippingRate>> GetShippingRatesAsync(string checkoutToken)
		{
			var callback = new RetrofitCallbackAsync<JavaList<ShippingRate>, IEnumerable<ShippingRate>>(data => data);
			GetShippingRates(checkoutToken, callback);
			return callback.Task;
		}

		public Task<Shop> GetShopAsync()
		{
			var callback = new RetrofitCallbackAsync<Shop>();
			GetShop(callback);
			return callback.Task;
		}

		public Task<Checkout> RemoveGiftCardAsync(GiftCard giftCard, Checkout checkout)
		{
			var callback = new RetrofitCallbackAsync<Checkout>();
			RemoveGiftCard(giftCard, checkout, callback);
			return callback.Task;
		}

		public Task<Checkout> RemoveProductReservationsFromCheckoutAsync(Checkout checkout)
		{
			var callback = new RetrofitCallbackAsync<Checkout>();
			RemoveProductReservationsFromCheckout(checkout, callback);
			return callback.Task;
		}

		public Task<Checkout> StoreCreditCardAsync(CreditCard card, Checkout checkout)
		{
			var callback = new RetrofitCallbackAsync<Checkout>();
			StoreCreditCard(card, checkout, callback);
			return callback.Task;
		}

		public Task TestIntegrationAsync()
		{
			var callback = new RetrofitCallbackAsync<Java.Lang.Void>();
			TestIntegration(callback);
			return callback.Task;
		}

		public Task<Checkout> UpdateCheckoutAsync(Checkout checkout)
		{
			var callback = new RetrofitCallbackAsync<Checkout>();
			UpdateCheckout(checkout, callback);
			return callback.Task;
		}
	}
}

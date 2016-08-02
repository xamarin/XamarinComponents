using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Foundation;
using ObjCRuntime;
using PassKit;

namespace Shopify.Buy
{
	//public static class BUYRuntime
	//{
	//	// extern NSSet * class_getBUYProperties (Class clazz) __attribute__((overloadable));
	//	[DllImport ("__Internal")]
	//	static extern NSSet class_getBUYProperties (Class clazz);
	//}

	partial class BuyClient
	{
		public Task<Shop> GetShopAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			var tcs = new TaskCompletionSource<Shop>();
			var urlTask = GetShop((data, error) => HandleCallback(tcs, cancellationToken, data, error));
			cancellationToken.Register(urlTask.Cancel);
			return tcs.Task;
		}

		public Task<ProductsPageResult> GetProductsPageAsync(nuint page, CancellationToken cancellationToken = default(CancellationToken))
		{
			var tcs = new TaskCompletionSource<ProductsPageResult>();
			var urlTask = GetProductsPage(page, (data, pageArg, reachedEndArg, error) => HandleCallback(tcs, cancellationToken, new ProductsPageResult { Products = data, Page = pageArg, ReachedEnd = reachedEndArg }, error));
			cancellationToken.Register(urlTask.Cancel);
			return tcs.Task;
		}

		public Task<Product> GetProductAsync(string productId, CancellationToken cancellationToken = default(CancellationToken))
		{
			var tcs = new TaskCompletionSource<Product>();
			var urlTask = GetProduct(productId, (data, error) => HandleCallback(tcs, cancellationToken, data, error));
			cancellationToken.Register(urlTask.Cancel);
			return tcs.Task;
		}

		public Task<Product[]> GetProductsAsync(string[] productIds, CancellationToken cancellationToken = default(CancellationToken))
		{
			var tcs = new TaskCompletionSource<Product[]>();
			var urlTask = GetProducts(productIds, (data, error) => HandleCallback(tcs, cancellationToken, data, error));
			cancellationToken.Register(urlTask.Cancel);
			return tcs.Task;
		}

		public Task<Collection[]> GetCollectionsAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			var tcs = new TaskCompletionSource<Collection[]>();
			var urlTask = GetCollections((data, error) => HandleCallback(tcs, cancellationToken, data, error));
			cancellationToken.Register(urlTask.Cancel);
			return tcs.Task;
		}

		public Task<CollectionsPageResult> GetCollectionsPageAsync(nuint page, CancellationToken cancellationToken = default(CancellationToken))
		{
			var tcs = new TaskCompletionSource<CollectionsPageResult>();
			var urlTask = GetCollectionsPage(page, (data, pageArg, reachedEndArg, error) => HandleCallback(tcs, cancellationToken, new CollectionsPageResult { Collections = data, Page = pageArg, RachedEnd = reachedEndArg }, error));
			cancellationToken.Register(urlTask.Cancel);
			return tcs.Task;
		}

		public Task<ProductsPageResult> GetProductsPageAsync(nuint page, NSNumber collectionId, CancellationToken cancellationToken = default(CancellationToken))
		{
			var tcs = new TaskCompletionSource<ProductsPageResult>();
			var urlTask = GetProductsPage(page, collectionId, (data, pageArg, reachedEndArg, error) => HandleCallback(tcs, cancellationToken, new ProductsPageResult { Products = data, Page = pageArg, ReachedEnd = reachedEndArg }, error));
			cancellationToken.Register(urlTask.Cancel);
			return tcs.Task;
		}

		public Task<ProductsPageResult> GetProductsPageAsync(nuint page, NSNumber collectionId, CollectionSort sortOrder, CancellationToken cancellationToken = default(CancellationToken))
		{
			var tcs = new TaskCompletionSource<ProductsPageResult>();
			var urlTask = GetProductsPage(page, collectionId, sortOrder, (data, pageArg, reachedEndArg, error) => HandleCallback(tcs, cancellationToken, new ProductsPageResult { Products = data, Page = pageArg, ReachedEnd = reachedEndArg }, error));
			cancellationToken.Register(urlTask.Cancel);
			return tcs.Task;
		}

		public Task<Checkout> CreateCheckoutAsync(Checkout checkout, CancellationToken cancellationToken = default(CancellationToken))
		{
			var tcs = new TaskCompletionSource<Checkout>();
			var urlTask = CreateCheckout(checkout, (data, error) => HandleCallback(tcs, cancellationToken, data, error));
			cancellationToken.Register(urlTask.Cancel);
			return tcs.Task;
		}

		public Task<Checkout> CreateCheckoutAsync(string cartToken, CancellationToken cancellationToken = default(CancellationToken))
		{
			var tcs = new TaskCompletionSource<Checkout>();
			var urlTask = CreateCheckout(cartToken, (data, error) => HandleCallback(tcs, cancellationToken, data, error));
			cancellationToken.Register(urlTask.Cancel);
			return tcs.Task;
		}

		public Task<Checkout> ApplyGiftCardAsync(string giftCardCode, Checkout checkout, CancellationToken cancellationToken = default(CancellationToken))
		{
			var tcs = new TaskCompletionSource<Checkout>();
			var urlTask = ApplyGiftCard(giftCardCode, checkout, (data, error) => HandleCallback(tcs, cancellationToken, data, error));
			cancellationToken.Register(urlTask.Cancel);
			return tcs.Task;
		}

		public Task<Checkout> RemoveGiftCardAsync(GiftCard giftCard, Checkout checkout, CancellationToken cancellationToken = default(CancellationToken))
		{
			var tcs = new TaskCompletionSource<Checkout>();
			var urlTask = RemoveGiftCard(giftCard, checkout, (data, error) => HandleCallback(tcs, cancellationToken, data, error));
			cancellationToken.Register(urlTask.Cancel);
			return tcs.Task;
		}

		public Task<Checkout> GetCheckoutAsync(Checkout checkout, CancellationToken cancellationToken = default(CancellationToken))
		{
			var tcs = new TaskCompletionSource<Checkout>();
			var urlTask = GetCheckout(checkout, (data, error) => HandleCallback(tcs, cancellationToken, data, error));
			cancellationToken.Register(urlTask.Cancel);
			return tcs.Task;
		}

		public Task<Checkout> UpdateCheckoutAsync(Checkout checkout, CancellationToken cancellationToken = default(CancellationToken))
		{
			var tcs = new TaskCompletionSource<Checkout>();
			var urlTask = UpdateCheckout(checkout, (data, error) => HandleCallback(tcs, cancellationToken, data, error));
			cancellationToken.Register(urlTask.Cancel);
			return tcs.Task;
		}

		public Task<Checkout> CompleteCheckoutAsync(Checkout checkout, CancellationToken cancellationToken = default(CancellationToken))
		{
			var tcs = new TaskCompletionSource<Checkout>();
			var urlTask = CompleteCheckout(checkout, (data, error) => HandleCallback(tcs, cancellationToken, data, error));
			cancellationToken.Register(urlTask.Cancel);
			return tcs.Task;
		}

		public Task<Checkout> CompleteCheckoutAsync(Checkout checkout, PKPaymentToken token, CancellationToken cancellationToken = default(CancellationToken))
		{
			var tcs = new TaskCompletionSource<Checkout>();
			var urlTask = CompleteCheckout(checkout, token, (data, error) => HandleCallback(tcs, cancellationToken, data, error));
			cancellationToken.Register(urlTask.Cancel);
			return tcs.Task;
		}

		public Task<Status> GetCompletionStatusOfCheckoutAsync(Checkout checkout, CancellationToken cancellationToken = default(CancellationToken))
		{
			var tcs = new TaskCompletionSource<Status>();
			var urlTask = GetCompletionStatusOfCheckout(checkout, (data, error) => HandleCallback(tcs, cancellationToken, data, error));
			cancellationToken.Register(urlTask.Cancel);
			return tcs.Task;
		}

		public Task<Status> GetCompletionStatusOfCheckoutUrlAsync(NSUrl url, CancellationToken cancellationToken = default(CancellationToken))
		{
			var tcs = new TaskCompletionSource<Status>();
			var urlTask = GetCompletionStatusOfCheckoutUrl(url, (data, error) => HandleCallback(tcs, cancellationToken, data, error));
			cancellationToken.Register(urlTask.Cancel);
			return tcs.Task;
		}

		public Task<ShippingRatesForCheckoutResult> GetShippingRatesForCheckoutAsync(Checkout checkout, CancellationToken cancellationToken = default(CancellationToken))
		{
			var tcs = new TaskCompletionSource<ShippingRatesForCheckoutResult>();
			var urlTask = GetShippingRatesForCheckout(checkout, (data, status, error) => HandleCallback(tcs, cancellationToken, new ShippingRatesForCheckoutResult { ShippingRates = data, Status = status }, error));
			cancellationToken.Register(urlTask.Cancel);
			return tcs.Task;
		}
		public Task<StoreCreditCardResult> StoreCreditCardAsync(ISerializable creditCard, Checkout checkout, CancellationToken cancellationToken = default(CancellationToken))
		{
			var tcs = new TaskCompletionSource<StoreCreditCardResult>();
			var urlTask = StoreCreditCard(creditCard, checkout, (data, paymentSessionId, error) => HandleCallback(tcs, cancellationToken, new StoreCreditCardResult { Checkout = data, PaymentSessionId = paymentSessionId }, error));
			cancellationToken.Register(urlTask.Cancel);
			return tcs.Task;
		}
		public Task<Checkout> RemoveProductReservationsAsync(Checkout checkout, CancellationToken cancellationToken = default(CancellationToken))
		{
			var tcs = new TaskCompletionSource<Checkout>();
			var urlTask = RemoveProductReservations(checkout, (data, error) => HandleCallback(tcs, cancellationToken, data, error));
			cancellationToken.Register(urlTask.Cancel);
			return tcs.Task;
		}

		public struct CollectionsPageResult
		{
			public Collection[] Collections { get; set; }
			public nuint Page { get; set; }
			public bool RachedEnd { get; set; }
		}

		public struct ProductsPageResult
		{
			public Product[] Products { get; set; }
			public nuint Page { get; set; }
			public bool ReachedEnd { get; set; }
		}

		public struct StoreCreditCardResult
		{
			public Checkout Checkout { get; set; }
			public string PaymentSessionId { get; set; }
		}

		public struct ShippingRatesForCheckoutResult
		{
			public ShippingRate[] ShippingRates { get; set; }
			public Status Status { get; set; }
		}

		private static void HandleCallback<T>(TaskCompletionSource<T> tcs, CancellationToken cancellationToken, T data, NSError error)
		{
			if (error != null)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					tcs.SetCanceled();
				}
				else
				{
					tcs.SetException(new NSErrorException(error));
				}
			}
			else
			{
				tcs.SetResult(data);
			}
		}
	}
}

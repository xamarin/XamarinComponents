using System.Threading.Tasks;
using System.Collections.Generic;
using Android.Content;
using Android.App;

namespace Xamarin.InAppBilling
{
	/// <summary>
	/// Defines the interfance that all InAppBillingHandlers used with Google Play In-App Billing need to support.
	/// </summary>
	public interface IInAppBillingHandler
	{
		/// <summary>
		/// Queries the inventory asynchronously.
		/// </summary>
		/// <returns>List of strings</returns>
		/// <param name="skuList">Sku list.</param>
		/// <param name="itemType">Item type.</param>
		Task<IList<Product>> QueryInventoryAsync (IList<string> skuList, string itemType);

		/// <summary>
		/// Buys an item.
		/// </summary>
		/// <param name="sku">Sku.</param>
		/// <param name="itemType">Item type.</param>
		/// <param name="payload">Payload.</param>
		void BuyProduct (string sku, string itemType, string payload);

		/// <summary>
		/// Buys an items
		/// </summary>
		/// <param name="product">Product.</param>
		/// <param name="payload">Payload.</param>
		void BuyProduct (Product product);

		/// <summary>
		/// Handles the activity result.
		/// </summary>
		/// <param name="requestCode">Request code.</param>
		/// <param name="resultCode">Result code.</param>
		/// <param name="data">Data.</param>
		void HandleActivityResult (int requestCode, Result resultCode, Intent data);

		/// <summary>
		/// Gets the purchases.
		/// </summary>
		/// <returns>The purchases.</returns>
		/// <param name="itemType">Item type (inapp or subs)</param>
		IList<Purchase> GetPurchases (string itemType);

		/// <summary>
		/// Consumes the purchased item
		/// </summary>
		/// <returns><c>true</c>, if purchased item was consumed, <c>false</c> otherwise.</returns>
		/// <param name="purchase">Purchased item</param>
		bool ConsumePurchase (Purchase purchase);

		/// <summary>
		/// Consumes the purchased item
		/// </summary>
		/// <returns><c>true</c>, if purchased item was consumed, <c>false</c> otherwise.</returns>
		/// <param name="token">Token.</param>
		bool ConsumePurchase (string token);
	}
}


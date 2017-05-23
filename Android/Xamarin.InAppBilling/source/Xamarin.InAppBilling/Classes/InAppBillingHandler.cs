using Android.Content;
using System.Collections.Generic;
using Android.OS;
using Com.Android.Vending.Billing;
using Android.App;
using System.Threading.Tasks;
using System;
using Xamarin.InAppBilling.Utilities;
//using Newtonsoft.Json;
using System.Linq;
using System.Security.Cryptography;

namespace Xamarin.InAppBilling
{
	/// <summary>
	/// The <see cref="Xamarin.InAppBilling.InAppBillingHandler"/> is a helper class that handles communication with the
	/// Google Play services to provide support for getting a list of available products, buying a product, consuming a
	/// product, and getting a list of already owned products.
	/// </summary>
	public class InAppBillingHandler : IInAppBillingHandler
	{
		#region Constants
		const int PurchaseRequestCode = 1001;
		#endregion 

		#region Private Variables
		private Activity _activity;
		private string _payload;
		private IInAppBillingService _billingService;
		private string _publicKey;
		#endregion

		#region Computed Properties
		/// <summary>
		/// Gets or sets the Google Play Service public key used for In-App Billing
		/// </summary>
		/// <value>The public key.</value>
		/// <remarks>NOTE: The key will be encrypted when it is stored in memory.</remarks>
		public string PublicKey {
			get { return Crypto.Decrypt (_publicKey, _activity.PackageName); }
			set { _publicKey = Crypto.Encrypt (value, _activity.PackageName); }
		}
		#endregion 

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Xamarin.InAppBilling.InAppBillingHandler"/> class.
		/// </summary>
		/// <param name="activity">Activity.</param>
		/// <param name="billingService">Billing service.</param>
		/// <param name="publicKey">Public key.</param>
		public InAppBillingHandler (Activity activity, IInAppBillingService billingService, string publicKey)
		{
			// Initialize
			_billingService = billingService;
			_activity = activity;
			PublicKey = publicKey;
		}
		#endregion 

		#region Public Methods
		/// <summary>
		/// Queries the inventory asynchronously and returns a list of <see cref="Xamarin.Android.InAppBilling.Product"/>s matching 
		/// the given list of SKU numbers.
		/// </summary>
		/// <returns>List of <see cref="Xamarin.Android.InAppBilling.Product"/>s matching the given list of SKUs.</returns>
		/// <param name="skuList">Sku list.</param>
		/// <param name="itemType">The <see cref="Xamarin.Android.InAppBilling.ItemType"/> of product being queried.</param>
		public Task<IList<Product>> QueryInventoryAsync (IList<string> skuList, string itemType)
		{
			// Create a task to load the requested list of products in the background
			var getSkuDetailsTask = Task.Factory.StartNew<IList<Product>> (() => {

				// Trap all errors
				try {
					// Create a bundle and attach the list of requested SKUs
					Bundle querySku = new Bundle ();
					querySku.PutStringArrayList (Billing.ItemIdList, skuList);

					// Query Google Play for the products
					Bundle skuDetails = _billingService.GetSkuDetails (Billing.APIVersion, _activity.PackageName, itemType, querySku);

					// Get the response code
					var responseCode = skuDetails.GetInt(Response.Code);

					// Take action based on the returned code
					switch(responseCode) {
					case BillingResult.OK:
						// Get the list of returned products	 
						var products = skuDetails.GetStringArrayList (Billing.SkuDetailsList);

						// Testing deserialization
						//					var plist = products.ToArray();
						//					Product prod;
						//					List<Product> AvailableProducts = new List<Product>();
						//					for(int n=0; n<plist.Count(); ++n) {
						//						prod = SimpleJson.DeserializeObject<Product>(plist[n]);
						//						AvailableProducts.Add(prod);
						//					}


						// Either return null if no matching products were found or use the json parser
						// to deserialize the list in the products.
						return (products == null) ? null
								: products.Select (SimpleJson.DeserializeObject<Product>).ToList ();
					default:
						// Inform caller of error
						RaiseQueryInventoryError(responseCode,skuDetails);
						return null;
					}

					return null;
				}
				catch (Exception e) {
					// Report error to user
					RaiseInAppBillingProcessingError(String.Format("Error Available Inventory: {0}", e.ToString()));

					// Return nothing 
					return null;
				}

			});

			// Return the results
			return getSkuDetailsTask;
		}

		/// <summary>
		/// Buys the given <see cref="Xamarin.Android.InAppBilling.Product"/> 
		/// </summary>
		/// <param name="product">The <see cref="Xamarin.Android.InAppBilling.Product"/> representing the item the users wants to
		/// purchase.</param>
		/// <remarks>This method automatically generates a unique GUID and attaches it as the developer payload for this purchase.
		/// </remarks>
		public void BuyProduct (Product product)
		{
			// Create a unique ID for this purchase
			_payload = Guid.NewGuid ().ToString ();

			// Request the product be purchased
			BuyProduct (product.ProductId, product.Type, _payload);
		}

		/// <summary>
		/// Buys the given <see cref="Xamarin.Android.InAppBilling.Product"/> and attaches the given developer payload to the 
		/// purchase.
		/// </summary>
		/// <param name="product">The <see cref="Xamarin.Android.InAppBilling.Product"/> representing the item the users wants to
		/// purchase.</param>
		/// <param name="payload">The developer payload to attach to the purchase.</param>
		public void BuyProduct (Product product, string payload)
		{
			// Request the product be purchased
			BuyProduct (product.ProductId, product.Type, payload);
		}

		/// <summary>
		/// Buys a product based on the given product SKU and Item Type attaching the given payload
		/// </summary>
		/// <param name="sku">The SKU of the item to purchase.</param>
		/// <param name="itemType">The type of the item to purchase.</param>
		/// <param name="payload">The developer payload to attach to the purchase.</param>
		public void BuyProduct (string sku, string itemType, string payload)
		{
			try {
				// Request to purchase the given product and get the response of the request
				Bundle buyIntentBundle = _billingService.GetBuyIntent (Billing.APIVersion, _activity.PackageName, sku, itemType, payload);

				// Get the response code
				var responseCode = buyIntentBundle.GetInt(Response.Code);

				// Take action based on the returned code
				switch(responseCode) {
				case BillingResult.OK:
					// Call Google Play services to purchase product
					var pendingIntent = buyIntentBundle.GetParcelable (Response.BuyIntent) as PendingIntent;
					if (pendingIntent != null) {
						_activity.StartIntentSenderForResult (pendingIntent.IntentSender, PurchaseRequestCode, new Intent (), 0, 0, 0);
					}
					break;
				default:
					// Inform caller
					RaiseBuyProductError(responseCode,sku);
					return;
				}

			}
			catch (Exception e) {
				// Report error to user
				RaiseInAppBillingProcessingError(String.Format("Error Buy Product: {0}", e.ToString()));
			}
		}

		/// <summary>
		/// Consumes the purchased item.
		/// </summary>
		/// <returns><c>true</c> if the purchase is successfully consumed else returns <c>false</c>.</returns>
		/// <param name="purchase">The purchase receipt of the item to consume.</param>
		public bool ConsumePurchase (Purchase purchase)
		{
			// Anything to consume
			if (purchase == null) {
				// No, throw error
				throw new ArgumentNullException ("Purchase receipt is null");
			}

			// Consume give purchase
			return ConsumePurchase (purchase.PurchaseToken);
		}

		/// <summary>
		/// Consumes the purchased item
		/// </summary>
		/// <returns><c>true</c> if the purchase is successfully consumed else returns <c>false</c>.</returns>
		/// <param name="token">The purchase token of the purchase to consume.</param>
		public bool ConsumePurchase (string token)
		{
			// Was a valid token passed?
			if (string.IsNullOrEmpty (token)) {
				// no, throw an error
				throw new ArgumentException ("Purchase token is null");
			}

			try {
				// Ask Google Play to consume the purchase
				int response = _billingService.ConsumePurchase (Billing.APIVersion, _activity.PackageName, token);
				Logger.Info ("Consuming purchase '{0}', response: {1}", token, response);

				// Take action based on the response
				switch(response) {
				case BillingResult.OK:
					// Inform caller that the product was consumed.
					RaiseOnPurchaseConsumed (token);
					return true;
				default:
					// Failure, report error to caller
					Logger.Error ("Unable to consume '{0}', response: {1}", token, response);
					RaiseOnPurchaseConsumedError (response, token);
					return false;
				}
		
			}
			catch (Exception e) {
				// Report error to user
				RaiseInAppBillingProcessingError(String.Format("Error Consume Purchase: {0}", e.ToString()));

				// Return issue
				return false;
			}
		}

		/// <summary>
		/// Gets a list of all products of a given item type purchased by the current user.
		/// </summary>
		/// <returns>A list of <see cref="Xamarin.InAppBilling.Product"/>s purchased by the current user.</returns>
		/// <param name="itemType">Item type (product or subs)</param>
		public IList<Purchase> GetPurchases (string itemType)
		{
			string continuationToken = string.Empty;
			var purchases = new List<Purchase> ();
			Purchase purchase;
			Bundle ownedItems;
			int response;
			IList<string> items; 
			IList<string> dataList; 
			IList<string> signatures;

			// Process all purchases until we hit an empty continuation token
			do {

				try {
					// Is there a continuation token?
					if (continuationToken == string.Empty) {
						// No, Query Google Play for a list of purchases matching the given item type
						ownedItems = _billingService.GetPurchases (Billing.APIVersion, _activity.PackageName, itemType, null);
					} else {
						// Yes, Query for the next set of items
						ownedItems = _billingService.GetPurchases (Billing.APIVersion, _activity.PackageName, itemType, continuationToken);
					}

					// Anything returned?
					if (ownedItems==null) {
						// Report error to user and abort
						RaiseInAppBillingProcessingError("No items returned from Google Play Services.");
						return null;
					}

					// Get the response code
					var responseCode = ownedItems.GetInt(Response.Code);

					// Take action based on the returned code
					switch(responseCode) {
					case BillingResult.OK:
						// Are the returned items valid purchases?
						if (!ValidOwnedItems (ownedItems)) {
							// No, stop processing and return any valid purchases that we have
							// already processed
							Logger.Debug ("Invalid purchases");
							RaiseOnInvalidOwnedItemsBundleReturned(ownedItems);
							return purchases;
						}

						// Get a list of items, data and signatures from the response
						items = ownedItems.GetStringArrayList (Response.InAppPurchaseItemList);
						dataList = ownedItems.GetStringArrayList (Response.InAppPurchaseDataList);
						signatures = ownedItems.GetStringArrayList (Response.InAppDataSignatureList);

						// Did we receive a valid bundle from Google Play Services?
						if (items == null || dataList==null || signatures ==null) {
							// Report error to user and abort
							RaiseInAppBillingProcessingError(String.Format("Invalid owned items bundle returned by Google Play Services: {0}", ownedItems.ToString()));
							return null;
						}
						break;
					default:
						// Inform caller of error
						RaiseOnGetProductsError(responseCode, ownedItems);
						return null;
					}

				}
				catch (Exception e) {
					// Report error to user and abort
					RaiseInAppBillingProcessingError(String.Format("Error retrieving previous purchases: {0}", e.ToString()));
					return null;
				}


				// Process each item in the list
				for (int i = 0; i < items.Count; i++) {
					string data = dataList [i];
					string sign = signatures [i];

					// Trap deserialization errors
					try {
						// Deserialize data
						purchase = SimpleJson.DeserializeObject<Purchase> (data); //JsonConvert
					}
					catch(Exception e) {
						// Log error
						Logger.Error ("GetPurchases Error {0}: Unable to deserialize purchase '{1}'.\n Setting Purchase.DeveloperPayload with info returned from Google.", e.ToString(), data);

						// Create an empty purchase on error and dump raw data into
						// the developer package
						purchase = new Purchase();
						purchase.DeveloperPayload = data;
					}  

					// Trap any validation errors
					try {
						// Is this a valid purchase?
						if (purchase.ProductId.Contains("android.test.")) {
							// This is one of the reserved test products, automatically consider it valid
							// and return it to the list of purchases.
							purchases.Add (purchase);
						} else if (Security.VerifyPurchase (PublicKey, data, sign)) {
							// Yes, deserialize the json data into a purchase object and add to
							// the collection of purchases
							purchases.Add (purchase);
						} else {
							// Inform caller of error
							RaiseOnPurchaseFailedValidation(purchase, data, sign);
						}
					}
					catch (Exception e) {
						// Report error to user and abort
						RaiseInAppBillingProcessingError(String.Format("Error validating previous purchase {0}: {1}",purchase.ProductId, e.ToString()));
						return null;
					}
				}

				// Trap any errors getting the continuation token
				try {
					// Get the continuation token from the response
					continuationToken = ownedItems.GetString (Response.InAppContinuationToken);
					Logger.Debug ("Continuation Token: {0}", continuationToken);
				}
				catch{
					// There has been an error, stop searching for returned product
					continuationToken = string.Empty;
				}

			} while(!string.IsNullOrWhiteSpace(continuationToken));

			// Return all valid purchases
			return purchases;
		}

		/// <summary>
		/// Verifies that the given purchased bundle valid and contains an item list, data list and
		/// a signature list.
		/// </summary>
		/// <returns><c>true</c>, if owned items was valided, <c>false</c> otherwise.</returns>
		/// <param name="purchased">Purchased.</param>
		static bool ValidOwnedItems (Bundle purchased)
		{
			return	purchased.ContainsKey (Response.InAppPurchaseItemList)
				&& purchased.ContainsKey (Response.InAppPurchaseDataList)
				&& purchased.ContainsKey (Response.InAppDataSignatureList);
		}

		/// <summary>
		/// Handles the activity result.
		/// </summary>
		/// <param name="requestCode">Request code.</param>
		/// <param name="resultCode">Result code.</param>
		/// <param name="data">Data.</param>
		public void HandleActivityResult (int requestCode, Result resultCode, Intent data)
		{
			Purchase purchase;
			int responseCode = 0;
			string purchaseData = "";
			string purchaseSign = "";

			// Is the returned result for a Google Play purchase?
			if (PurchaseRequestCode != requestCode || data == null) {
				// No, abort processing
				return;
			}

			// Trap decoding issues
			try {
				// Decode packet
				responseCode = data.GetReponseCodeFromIntent ();

				// Take action based on the returned code
				switch(responseCode) {
				case BillingResult.OK:
					// Decode packet
					purchaseData = data.GetStringExtra (Response.InAppPurchaseData);
					purchaseSign = data.GetStringExtra (Response.InAppDataSignature);
					break;
				case BillingResult.UserCancelled:
					// Inform caller that the user canceled the purchase
					RaiseOnUserCanceled();
					return;
				default:
					// Inform caller
					RaiseBuyProductError(responseCode,"unknown");
					return;
				}
			}
			catch (Exception e) {
				// Report error to user
				RaiseInAppBillingProcessingError(String.Format("Error Decoding Returned Packet Information: {0}", e.ToString()));

				// Abort processing
				return;
			}

			// Trap deserialization errors
			try {
				// Deserialize data
				purchase = SimpleJson.DeserializeObject<Purchase> (purchaseData);
			}
			catch(Exception e) {
				// Log error and inform caller
				Logger.Error ("Completed Purchase Error {0}: Unable to deserialize purchase '{1}'.\nSetting Purchase.DeveloperPayload with info returned from Google.", e.ToString(), purchaseData);
				RaiseInAppBillingProcessingError (String.Format ("Unable to deserialize purchase: {0}\nError: {1}", purchaseData, e.ToString ()));

				// Create an empty purchase on error and dump raw data into
				// the developer package
				purchase = new Purchase();
				purchase.DeveloperPayload = purchaseData;

				// Return to caller
				RaiseOnPurchaseFailedValidation(purchase, purchaseData, purchaseSign);
				return;
			}

			// Trap validation issues
			try {
				// Is this a valid purchase?
				if (purchase.ProductId.Contains("android.test.")) {
					// This is one of the reserved test products, automatically consider it valid
					// and inform caller of the completed purchase cycle
					RaiseOnProductPurchased (responseCode, purchase, purchaseData, purchaseSign);
				} else if (Security.VerifyPurchase (PublicKey, purchaseData, purchaseSign)) {
					// Yes, deserialize the json data into a purchase object and 
					// and inform caller of the completed purchase cycle
					RaiseOnProductPurchased (responseCode, purchase, purchaseData, purchaseSign);
				} else {
					// Inform caller of error
					RaiseOnPurchaseFailedValidation(purchase, purchaseData, purchaseSign);
				}
			}
			catch (Exception e) {
				// Report error to user
				RaiseInAppBillingProcessingError(String.Format("Error Decoding Returned Packet Information: {0}", e.ToString()));
			}
		}
		#endregion 

		#region Events
		/// <summary>
		/// Raised where there is an error getting previously purchased products from the Google Play Services.
		/// </summary>
		/// <param name="responseCode">Response code.</param>
		/// <param name="ownedItems">Owned items.</param>
		public delegate void OnGetProductsErrorDelegate (int responseCode, Bundle ownedItems);

		/// <summary>
		/// Raised where there is an error getting previously purchased products from the Google Play Services.
		/// </summary>
		public event OnGetProductsErrorDelegate OnGetProductsError;

		/// <summary>
		/// Raises the on get products error event.
		/// </summary>
		/// <param name="responseCode">Response code.</param>
		/// <param name="ownedItems">Owned items.</param>
		internal void RaiseOnGetProductsError(int responseCode, Bundle ownedItems) {
			// Inform caller
			if (this.OnGetProductsError != null)
				this.OnGetProductsError (responseCode, ownedItems);
		}

		/// <summary>
		/// Occurs when there is an error querying inventory from Google Play Services.
		/// </summary>
		public delegate void QueryInventoryErrorDelegate(int responseCode, Bundle skuDetails);

		/// <summary>
		/// Occurs when there is an error querying inventory from Google Play Services.
		/// </summary>
		public event QueryInventoryErrorDelegate QueryInventoryError;

		/// <summary>
		/// Raises the query inventory error event.
		/// </summary>
		/// <param name="responseCode">Response code.</param>
		/// <param name="skuDetails">Sku details.</param>
		internal void RaiseQueryInventoryError(int responseCode, Bundle skuDetails) {

			// Inform caller
			if (this.QueryInventoryError != null)
				this.QueryInventoryError (responseCode, skuDetails);
		}

		/// <summary>
		/// Occurs when the user attempts to buy a product and there is an error.
		/// </summary>
		public delegate void BuyProductErrorDelegate(int responseCode, string sku);

		/// <summary>
		/// Occurs when the user attempts to buy a product and there is an error.
		/// </summary>
		public event BuyProductErrorDelegate BuyProductError;

		/// <summary>
		/// Raises the buy product error event.
		/// </summary>
		/// <param name="responseCode">Response code.</param>
		/// <param name="sku">Sku.</param>
		internal void RaiseBuyProductError(int responseCode, string sku) {

			// Inform caller
			if (this.BuyProductError != null)
				this.BuyProductError (responseCode, sku);
		}

		/// <summary>
		/// Occurs when there is an in app billing procesing error.
		/// </summary>
		/// <param name="message">Message.</param>
		public delegate void InAppBillingProcessingErrorDelegate (string message);

		/// <summary>
		/// Occurs when there is an in app billing procesing error.
		/// </summary>
		public event InAppBillingProcessingErrorDelegate InAppBillingProcesingError;

		/// <summary>
		/// Raises the in app billing processing error event
		/// </summary>
		/// <param name="message">Message.</param>
		internal void RaiseInAppBillingProcessingError(string message) {
			// Inform caller
			if (this.InAppBillingProcesingError != null) {
				this.InAppBillingProcesingError (message);
			}
		}

		/// <summary>
		/// Raised when Google Play Services returns an invalid bundle from previously purchased items
		/// </summary>
		/// <param name="ownedItems">Owned items.</param>
		public delegate void OnInvalidOwnedItemsBundleReturnedDelegate(Bundle ownedItems);

		/// <summary>
		/// Raised when Google Play Services returns an invalid bundle from previously purchased items
		/// </summary>
		public event OnInvalidOwnedItemsBundleReturnedDelegate OnInvalidOwnedItemsBundleReturned;

		/// <summary>
		/// Raises the on invalid owned items bundle returned.
		/// </summary>
		/// <param name="ownedItems">Owned items.</param>
		internal void RaiseOnInvalidOwnedItemsBundleReturned (Bundle ownedItems){
			// Inform Caller
			if (this.OnInvalidOwnedItemsBundleReturned != null)
				this.OnInvalidOwnedItemsBundleReturned (ownedItems);
		}

		/// <summary>
		/// Occurs when the is an error on a product purchase attempt.
		/// </summary>
		/// <param name="responseCode">Response code.</param>
		/// <param name="sku">Sku.</param>
		/// <remarks>The <c>responseCode</c> will be a value from <see cref="Xamarin.InAppBilling.BillingResult"/>.</remarks>
		public delegate void OnProductPurchaseErrorDelegate (int responseCode, string sku);

		/// <summary>
		/// Occurs when the is an error on a product purchase attempt.
		/// </summary>
		public event OnProductPurchaseErrorDelegate OnProductPurchasedError;

		/// <summary>
		/// Raises the on product purchased error event.
		/// </summary>
		/// <param name="responseCode">Response code.</param>
		/// <param name="sku">Sku.</param>
		/// <remarks>The <c>responseCode</c> will be a value from <see cref="Xamarin.InAppBilling.BillingResult"/>.</remarks>
		internal void RaiseOnProductPurchasedError (int responseCode, string sku)
		{
			// Inform caller of disconnection
			if (this.OnProductPurchasedError != null)
				this.OnProductPurchasedError (responseCode, sku);
		}

		/// <summary>
		/// Occurs when a previously purchased product fails to validate.
		/// </summary>
		/// <param name="purchase">The purchase information for the product</param>
		public delegate void OnPurchaseFailedValidationDelegate(Purchase purchase, string purchaseData, string purchaseSignature);

		/// <summary>
		/// Occurs when a previously purchased product fails to validate.
		/// </summary>
		/// <param name="purchase">The purchase information for the product</param>
		public event OnPurchaseFailedValidationDelegate OnPurchaseFailedValidation;

		/// <summary>
		/// Raises the OnPurchaseFailedValidation event
		/// </summary>
		/// <param name="purchase">The purchase information for the product</param>
		internal void RaiseOnPurchaseFailedValidation(Purchase purchase, string purchaseData, string purchaseSignature) {
			// Inform caller of failure
			if (this.OnPurchaseFailedValidation != null) {
				this.OnPurchaseFailedValidation (purchase, purchaseData, purchaseSignature);
			}
		}

		/// <summary>
		/// Occurs after a product has been successfully purchased Google Play.
		/// </summary>
		/// <param name="response">The response code returned from Google Play Services.</param>
		/// <param name="purchase">Information about the purchase.</param>
		/// <remarks>This event is fired after a <c>OnProductPurchased</c> which is raised when the user successfully 
		/// logs an intent to purchase with Google Play.</remarks>
		public delegate void OnProductPurchasedDelegate(int response, Purchase purchase, string purchaseData, string purchaseSignature);

		/// <summary>
		/// Occurs after a product has been successfully purchased Google Play.
		/// </summary>
		/// <remarks>This event is fired after a <c>OnProductPurchased</c> which is raised when the user successfully 
		/// logs an intent to purchase with Google Play.</remarks>
		public event OnProductPurchasedDelegate OnProductPurchased;

		/// <summary>
		/// Raises the on product purchase completed event.
		/// </summary>
		/// <param name="response">The response code returned from Google Play Services.</param>
		/// <param name="purchase">Information about the purchase.</param>
		internal void RaiseOnProductPurchased(int response, Purchase purchase, string purchaseData, string purchaseSignature) {

			// Inform caller of event
			if (this.OnProductPurchased != null) {
				this.OnProductPurchased (response, purchase, purchaseData, purchaseSignature);
			}
		}

		/// <summary>
		/// Occurs when there is an error consuming a product.
		/// </summary>
		/// <param name="responseCode">Response code.</param>
		/// <param name="token">Token.</param>
		/// <remarks>The <c>responseCode</c> will be a value from <see cref="Xamarin.InAppBilling.BillingResult"/>.</remarks>
		public delegate void OnPurchaseConsumedErrorDelegate(int responseCode, string token);

		/// <summary>
		/// Occurs when there is an error consuming a product.
		/// </summary>
		public event OnPurchaseConsumedErrorDelegate OnPurchaseConsumedError;

		/// <summary>
		/// Raises the on product consumed error.
		/// </summary>
		/// <param name="responseCode">Response code.</param>
		/// <param name="token">Token.</param>
		/// <remarks>The <c>responseCode</c> will be a value from <see cref="Xamarin.InAppBilling.BillingResult"/>.</remarks>
		internal void RaiseOnPurchaseConsumedError (int responseCode, string token)
		{
			// Inform caller of disconnection
			if (this.OnPurchaseConsumedError != null)
				this.OnPurchaseConsumedError (responseCode, token);
		}

		/// <summary>
		/// Occurs when on product consumed.
		/// </summary>
		/// <param name="token">Token.</param>
		public delegate void OnPurchaseConsumedDelegate (string token);

		/// <summary>
		/// Occurs when on product consumed.
		/// </summary>
		public event OnPurchaseConsumedDelegate OnPurchaseConsumed;

		/// <summary>
		/// Raises the on product consumed.
		/// </summary>
		/// <param name="token">Token.</param>
		internal void RaiseOnPurchaseConsumed(string token){

			// Inform caller of the event
			if (this.OnPurchaseConsumed != null)
				this.OnPurchaseConsumed (token);
		}

		/// <summary>
		/// Occurs when the user cancels an In App Billing purchase.
		/// </summary>
		public delegate void OnUserCanceledDelegate();

		/// <summary>
		/// Occurs when on user canceled.
		/// </summary>
		public event OnUserCanceledDelegate OnUserCanceled;

		/// <summary>
		/// Raises the on user canceled event.
		/// </summary>
		internal void RaiseOnUserCanceled() {

			// Inform caller of the event
			if (this.OnUserCanceled != null)
				this.OnUserCanceled ();
		}
		#endregion
	}
}


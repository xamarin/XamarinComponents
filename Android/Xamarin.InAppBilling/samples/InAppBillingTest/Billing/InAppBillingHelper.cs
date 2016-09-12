using Android.Content;
using System.Collections.Generic;
using Android.OS;
using Com.Android.Vending.Billing;
using Android.App;
using System.Threading.Tasks;
using System;

namespace InAppService
{
	/// <summary>
	/// In app billing service helper.
	/// </summary>
	public class InAppBillingHelper
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="InAppService.InAppBillingHelper"/> class.
		/// </summary>
		/// <param name="activity">Activity.</param>
		/// <param name="billingService">Billing service.</param>
		public InAppBillingHelper (Activity activity, IInAppBillingService billingService)
		{
			_billingService = billingService;
			_activity = activity;
		}

		/// <summary>
		/// Queries the inventory asynchronously.
		/// </summary>
		/// <returns>List of strings</returns>
		/// <param name="skuList">Sku list.</param>
		/// <param name="itemType">Item type.</param>
		public Task<IList<string>> QueryInventoryAsync (List<string> skuList, string itemType)
		{

			var getSkuDetailsTask = Task.Factory.StartNew<IList<string>> (() => {

				Bundle querySku = new Bundle ();
				querySku.PutStringArrayList (Constants.ItemIdList, skuList);


				Bundle skuDetails = _billingService.GetSkuDetails (Constants.APIVersion, _activity.PackageName, itemType, querySku);
				
				if (skuDetails.ContainsKey (Constants.SkuDetailsList)) {
					return skuDetails.GetStringArrayList (Constants.SkuDetailsList);
				}

				return null;
			});

			return getSkuDetailsTask;
		}

		/// <summary>
		/// Buys an items
		/// </summary>
		/// <param name="product">Product.</param>
		/// <param name="payload">Payload.</param>
		public void LaunchPurchaseFlow (Product product)
		{
			_payload = Guid.NewGuid ().ToString ();
			LaunchPurchaseFlow (product.ProductId, product.Type, _payload);
		}

		/// <summary>
		/// Buys an item.
		/// </summary>
		/// <param name="sku">Sku.</param>
		/// <param name="itemType">Item type.</param>
		/// <param name="payload">Payload.</param>
		public void LaunchPurchaseFlow (string sku, string itemType, string payload)
		{

//#if DEBUG
//			var consume = _billingService.ConsumePurchase(Constants.APIVersion, _activity.PackageName, "inapp:com.xamarin.InAppService:android.test.purchased");
//			Console.WriteLine ("Consumed: {0}", consume);
//#endif

			var buyIntentBundle = _billingService.GetBuyIntent (Constants.APIVersion, _activity.PackageName, sku, itemType, payload);
			var response = GetResponseCodeFromBundle (buyIntentBundle);

			if (response != BillingResult.OK) {
				return;
			}

			var pendingIntent = buyIntentBundle.GetParcelable (Response.BuyIntent) as PendingIntent;
			if (pendingIntent != null) {
				_activity.StartIntentSenderForResult (pendingIntent.IntentSender, PurchaseRequestCode, new Intent (), 0, 0, 0);
			}
		}

		public void GetPurchases (string itemType)
		{
			Bundle ownedItems = _billingService.GetPurchases (Constants.APIVersion, _activity.PackageName, itemType, null);
			var response = GetResponseCodeFromBundle (ownedItems);

			if (response != BillingResult.OK) {
				return;
			}

			var list = ownedItems.GetStringArrayList (Response.InAppPurchaseItemList);
			var data = ownedItems.GetStringArrayList (Response.InAppPurchaseDataList);
			Console.WriteLine (list);

			//TODO: Get more products if continuation token is not null
		}

		public void HandleActivityResult (int requestCode, Result resultCode, Intent data)
		{
			if (PurchaseRequestCode != requestCode || data == null) {
				return;
			}

			var response = GetReponseCodeFromIntent (data);
			var purchaseData = data.GetStringExtra (Response.InAppPurchaseData);
			var purchaseSign = data.GetStringExtra (Response.InAppDataSignature);
		}

		int GetReponseCodeFromIntent (Intent intent)
		{
			object response = intent.Extras.Get (Response.Code);

			if (response == null) {
				//Bundle with null response code, assuming OK (known issue)
				return BillingResult.OK;
			}

			if (response is Java.Lang.Number) {
				return ((Java.Lang.Number)response).IntValue ();
			}

			return BillingResult.Error;
		}

		int GetResponseCodeFromBundle (Bundle bunble)
		{
			object response = bunble.Get (Response.Code);
			if (response == null) {
				//Bundle with null response code, assuming OK (known issue)
				return BillingResult.OK;
			}

			if (response is Java.Lang.Number) {
				return ((Java.Lang.Number)response).IntValue ();
			}

			return BillingResult.Error;
		}

		Activity _activity;
		string _payload;
		IInAppBillingService _billingService;
		const int PurchaseRequestCode = 1001;

	}
}


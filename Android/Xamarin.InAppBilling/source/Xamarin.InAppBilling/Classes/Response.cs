using System;

namespace Xamarin.InAppBilling
{
	/// <summary>
	/// List of response codes available within the Google Play In-App Billing API.
	/// </summary>
	public static class Response
	{
		/// <summary>
		/// Gets the response code.
		/// </summary>
		/// <value>The response code.</value>
		public const string Code = "RESPONSE_CODE";

		/// <summary>
		/// Gets the buy intent.
		/// </summary>
		/// <value>The buy intent.</value>
		public const string BuyIntent = "BUY_INTENT";

		/// <summary>
		/// Gets the in app purchase data.
		/// </summary>
		/// <value>The in app purchase data.</value>
		public const string InAppPurchaseData = "INAPP_PURCHASE_DATA";

		/// <summary>
		/// Gets the in app data signature.
		/// </summary>
		/// <value>The in app data signature.</value>
		public const string InAppDataSignature = "INAPP_DATA_SIGNATURE";

		/// <summary>
		/// Gets the in app data signature list.
		/// </summary>
		/// <value>The in app data signature list.</value>
		public const string InAppDataSignatureList = "INAPP_DATA_SIGNATURE_LIST";

		/// <summary>
		/// Gets the in app purchase item list.
		/// </summary>
		/// <value>The in app purchase item list.</value>
		public const string InAppPurchaseItemList = "INAPP_PURCHASE_ITEM_LIST";

		/// <summary>
		/// Gets the in app purchase data list.
		/// </summary>
		/// <value>The in app purchase data list.</value>
		public const string InAppPurchaseDataList = "INAPP_PURCHASE_DATA_LIST";

		/// <summary>
		/// Gets the in app continuation token.
		/// </summary>
		/// <value>The in app continuation token.</value>
		public const string InAppContinuationToken = "INAPP_CONTINUATION_TOKEN";
	}
}


using System;

namespace Xamarin.InAppBilling
{
	/// <summary>
	/// Contains the reserved product IDs use to test In-App Billing via Google Play without actually making a
	/// purchase. To test your implementation with static responses, you make an In-app Billing request using 
	/// a special item that has a reserved product ID. Each reserved product ID returns a specific static response 
	/// from Google Play. No money is transferred when you make In-app Billing requests with the reserved product
	/// IDs. Also, you cannot specify the form of payment when you make a billing request with a reserved product ID.
	/// </summary>
	public static class ReservedTestProductIDs
	{
		#region Computed Properties
		/// <summary>
		/// When you make an In-app Billing request with this product ID, Google Play responds as though you successfully purchased an item.
		/// </summary>
		/// <value>android.test.purchased</value>
		/// <remarks>The response includes a JSON string, which contains fake purchase information (for example, a fake order ID). In some cases, 
		/// the JSON string is signed and the response includes the signature so you can test your signature verification implementation 
		/// using these responses.</remarks>
		public const string Purchased = "android.test.purchased";

		/// <summary>
		/// When you make an In-app Billing request with this product ID Google Play responds as though the purchase was canceled. 
		/// </summary>
		/// <value>android.test.canceled</value>
		/// <remarks>This can occur when an error is encountered in the order process, such as an invalid credit card, or when you 
		/// cancel a user's order before it is charged.</remarks>
		public const string Canceled = "android.test.canceled";

		/// <summary>
		/// When you make an In-app Billing request with this product ID, Google Play responds as though the purchase was refunded.
		/// </summary>
		/// <value>android.test.refunded</value>
		/// <remarks>Refunds cannot be initiated through Google Play's in-app billing service. Refunds must be initiated by you 
		/// (the merchant). After you process a refund request through your Google Wallet merchant account, a refund message is 
		/// sent to your application by Google Play. This occurs only when Google Play gets notification from Google Wallet that
		///  a refund has been made.</remarks>
		public const string Refunded = "android.test.refunded";

		/// <summary>
		/// When you make an In-app Billing request with this product ID, Google Play responds as though the item being purchased 
		/// was not listed in your application's product list.
		/// </summary>
		/// <value>android.test.item_unavailable</value>
		public const string Unavailable = "android.test.item_unavailable";
		#endregion
	}
}


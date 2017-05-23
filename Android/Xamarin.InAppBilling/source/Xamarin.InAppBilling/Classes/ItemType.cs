using System;

namespace Xamarin.InAppBilling
{
	/// <summary>
	/// Defines the types of items that can be purchased using Google Play In-App Billing.
	/// </summary>
	public static class ItemType
	{
		/// <summary>
		/// A standard consumable or non-consumable product.
		/// </summary>
		public const string Product = "inapp";

		/// <summary>
		/// A subscription product such as a magazine.
		/// </summary>
		public const string Subscription = "subs";
	}
}
using System;

namespace Xamarin.InAppBilling
{
	/// <summary>
	/// Returns information about the Google Play In-App Billing API used in the <c>Xamarin.Android.InAppBilling</c> component.
	/// </summary>
	public static class Billing
	{
		/// <summary>
		/// Gets the API version.
		/// </summary>
		/// <value>3</value>
		public const int APIVersion = 3;

		/// <summary>
		/// Gets the sku details list.
		/// </summary>
		/// <value>DETAILS_LIST</value>
		public const string SkuDetailsList = "DETAILS_LIST";

		/// <summary>
		/// Gets the item identifier list.
		/// </summary>
		/// <value>ITEM_ID_LIST</value>
		public const string ItemIdList = "ITEM_ID_LIST";

	}
}


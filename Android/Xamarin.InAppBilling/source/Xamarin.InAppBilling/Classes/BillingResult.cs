using System;

namespace Xamarin.InAppBilling
{
	/// <summary>
	/// Avalible response codes returned by methods from the <c>InAppBillingService</c> functions that are
	/// part of Google Play In-App Billing.
	/// </summary>
	/// <remarks><c>BillingResult</c> was represented as a sealed class with computed properties instead of an enum
	/// to better work with the Google Play InAppBillingService interface and Json.net.</remarks>
	public static class BillingResult {
		/// <summary>
		/// The transaction completed successfully.
		/// </summary>
		public const int OK = 0;

		/// <summary>
		/// The user canceled the transaction.
		/// </summary>
		/// <value>The user cancelled - 1</value>
		public const int UserCancelled = 1;

		/// <summary>
		/// Network connection is down.
		/// </summary>
		public const int ServiceUnavailable = 2;

		/// <summary>
		/// In-App Billing is not supported on the given Android device.
		/// </summary>
		public const int BillingUnavailable = 3;

		/// <summary>
		/// The requested item is unavailable for purchase.
		/// </summary>
		public const int ItemUnavailable = 4;

		/// <summary>
		/// An invalid argument has been passed to the API or the app was not correctly signed, properly set up for In-app Billing in Google Play Dashboard, or does not have the necessary permissions in its manifest.
		/// </summary>
		public const int DeveloperError = 5;

		/// <summary>
		/// A fatal error occurred during an API action.
		/// </summary>
		public const int Error = 6;

		/// <summary>
		/// The user already owns the given item.
		/// </summary>
		public const int ItemAlreadyOwned = 7;

		/// <summary>
		/// The given item has not been purchased by the user.
		/// </summary>
		public const int ItemNotOwned = 8;
	}

}


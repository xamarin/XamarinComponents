using System;

namespace Xamarin.InAppBilling
{
	/// <summary>
	/// Defines the types of errors that con be returned from a <see cref="Xamarin.InAppBilling.InAppBillingServiceConnection"/> when processing
	/// billing requests.
	/// </summary>
	public enum InAppBillingErrorType
	{
		/// <summary>
		/// In App Billing is not supported on the current device.
		/// </summary>
		BillingNotSupported,
		/// <summary>
		/// Subscriptions are not supported on the current device.
		/// </summary>
		SubscriptionsNotSupported,
		/// <summary>
		/// An unknown error has occurred.
		/// </summary>
		UnknownError
	}
}
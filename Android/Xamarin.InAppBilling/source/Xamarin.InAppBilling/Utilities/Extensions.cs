using Android.OS;
using Android.Content;

namespace Xamarin.InAppBilling.Utilities
{
	/// <summary>
	/// Adds extension helper methods to several built in classes used for handling In-App Billing
	/// with Google Play.
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// Gets the response code from bundle.
		/// </summary>
		/// <returns>The response code from bundle.</returns>
		/// <param name="bunble">Bunble.</param>
		public static int GetResponseCodeFromBundle (this Bundle bunble)
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

		/// <summary>
		/// Gets the reponse code from intent.
		/// </summary>
		/// <returns>The reponse code from intent.</returns>
		/// <param name="intent">Intent.</param>
		public static int GetReponseCodeFromIntent (this Intent intent)
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
	}
}

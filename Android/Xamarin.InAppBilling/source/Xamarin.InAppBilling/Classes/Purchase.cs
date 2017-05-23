using System;

namespace Xamarin.InAppBilling
{
	/// <summary>
	/// Holds all information about a product purchased from Google Play for the current user.
	/// </summary>
	public class Purchase
	{
		#region Computed Properties
		/// <summary>
		/// Gets or sets the name of the package.
		/// </summary>
		/// <value>The name of the package.</value>
		public string PackageName { get; set; }

		/// <summary>
		/// Gets or sets the order identifier.
		/// </summary>
		/// <value>The order identifier.</value>
		public string OrderId { get; set; }

		/// <summary>
		/// Gets or sets the product identifier.
		/// </summary>
		/// <value>The product identifier.</value>
		public string ProductId { get; set; }

		/// <summary>
		/// Gets or sets the developer payload.
		/// </summary>
		/// <value>The developer payload.</value>
		public string DeveloperPayload { get; set; }

		/// <summary>
		/// Gets or sets the purchase time.
		/// </summary>
		/// <value>The purchase time.</value>
		public long PurchaseTime { get; set; }

		/// <summary>
		/// Gets or sets the state of the purchase.
		/// </summary>
		/// <value>The state of the purchase.</value>
		public int PurchaseState { get; set; }

		/// <summary>
		/// Gets or sets the purchase token.
		/// </summary>
		/// <value>The purchase token.</value>
		public string PurchaseToken { get; set; }
		#endregion 

		#region Override Methods
		/// <summary>
		/// Converts the {Purchase} into a `string`
		/// </summary>
		/// <returns>The `string` representation of the {Product}.</returns>
		public override string ToString ()
		{
			return string.Format ("[Purchase: PackageName={0}, OrderId={1}, ProductId={2}, DeveloperPayload={3}, PurchaseTime={4}, PurchaseState={5}, PurchaseToken={6}]", PackageName, OrderId, ProductId, DeveloperPayload, PurchaseTime, PurchaseState, PurchaseToken);
		}
		#endregion 
	}
}

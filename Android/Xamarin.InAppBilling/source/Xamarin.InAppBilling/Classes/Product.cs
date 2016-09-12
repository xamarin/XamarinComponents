using System;

namespace Xamarin.InAppBilling
{
	/// <summary>
	/// Holds all information about an In-App Billing product available on the Google Play store.
	/// </summary>
	public class Product
	{
		#region Computed Properties
		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		public string Title { get; set; }

		/// <summary>
		/// Gets or sets the price.
		/// </summary>
		/// <value>The price.</value>
		public string Price { get; set; }

		/// <summary>
		/// Price in micro-units, where 1,000,000 micro-units equal one unit of the currency. For example, if price is "€7.99", price_amount_micros is "7990000".
		/// </summary>
		/// <value>The price amount micros.</value>
		public string Price_Amount_Micros { get; set; }

		/// <summary>
		/// ISO 4217 currency code for price. For example, if price is specified in British pounds sterling, price_currency_code is "GBP".
		/// </summary>
		/// <value>The price currency code.</value>
		public string Price_Currency_Code { get; set; }

		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		/// <value>The type.</value>
		public string Type { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>The description.</value>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the product identifier.
		/// </summary>
		/// <value>The product identifier.</value>
		public string ProductId { get; set; }
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Xamarin.InAppBilling.Product"/> class.
		/// </summary>
		public Product () {

		}
		#endregion 

		#region Override Methods
		public override string ToString ()
		{
			return string.Format ("[Product: Title={0}, Price={1}, Price_Amount_Micros={2}, Price_Currency_Code={3}, Type={4}, Description={5}, ProductId={6}]", Title, Price, Price_Amount_Micros, Price_Currency_Code, Type, Description, ProductId);
		}
		#endregion
	}

}


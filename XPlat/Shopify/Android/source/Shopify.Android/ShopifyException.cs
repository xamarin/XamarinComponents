using System;
using Square.Retrofit;

namespace Shopify.Buy
{
	public class ShopifyException : Exception
	{
		public ShopifyException()
		{
		}

		public ShopifyException(string message)
			: base(message)
		{
		}

		public ShopifyException(RetrofitError error)
			: this(error.Body?.ToString() ?? error.GetKind().ToString())
		{
			this.Error = error;
		}

		public ShopifyException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		public RetrofitError Error { get; private set; }
	}
}

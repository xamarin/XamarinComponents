using ObjCRuntime;

namespace Shopify.Buy
{
	[Native]
	public enum ThemeStyle : long
	{
		Light,
		Dark
	}

	[Native]
	public enum CollectionSort : ulong
	{
		CollectionDefault,
		BestSelling,
		TitleAscending,
		TitleDescending,
		PriceAscending,
		PriceDescending,
		CreatedAscending,
		CreatedDescending
	}

	[Native]
	public enum Status : ulong
	{
		Complete = 200,
		Processing = 202,
		NotFound = 404,
		PreconditionFailed = 412,
		Failed = 424,
		Unknown
	}

	[Native]
	public enum CheckoutError : ulong
	{
		CartFetchError,
		NoShippingMethodsToAddress,
		NoProductSpecified,
		InvalidProductID,
		NoCollectionIdSpecified,
		NoGiftCardSpecified,
		NoCreditCardSpecified,
		NoApplePayTokenSpecified,
		InvalidCheckoutObject
	}

	[Native]
	public enum PaymentButtonStyle : long
	{
		White = 0,
		WhiteOutline,
		Black
	}

	[Native]
	public enum PaymentButtonType : long
	{
		Plain = 0,
		Buy,
		Setup
	}

	[Native]
	public enum CheckoutType : ulong
	{
		Normal,
		ApplePay,
		Cancel
	}
}

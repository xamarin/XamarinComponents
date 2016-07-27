using ObjCRuntime;

namespace Shopify
{
	[Native]
	public enum BUYThemeStyle : long
	{
		Light,
		Dark
	}

	[Native]
	public enum BUYCollectionSort : ulong
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
	public enum BUYStatus : ulong
	{
		Complete = 200,
		Processing = 202,
		NotFound = 404,
		PreconditionFailed = 412,
		Failed = 424,
		Unknown
	}

	[Native]
	public enum BUYCheckoutError : ulong
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
	public enum BUYPaymentButtonStyle : long
	{
		White = 0,
		WhiteOutline,
		Black
	}

	[Native]
	public enum BUYPaymentButtonType : long
	{
		Plain = 0,
		Buy,
		Setup
	}

	[Native]
	public enum BUYCheckoutType : ulong
	{
		Normal,
		ApplePay,
		Cancel
	}
}

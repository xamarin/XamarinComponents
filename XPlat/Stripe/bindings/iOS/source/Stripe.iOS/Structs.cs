using System;
using System.Runtime.InteropServices;
using ObjCRuntime;

namespace Stripe.iOS
{
	[Native]
	public enum ShippingType : ulong
	{
		Shipping,
		Delivery
	}

	[Native]
	public enum ShippingStatus : ulong
	{
		Valid,
		Invalid
	}

	[Native]
	public enum PaymentStatus : ulong
	{
		Success,
		Error,
		UserCancellation
	}

	[Native]
	public enum FilePurpose : long
	{
		IdentityDocument,
		DisputeEvidence,
		Unknown
	}

	[Native]
	public enum BillingAddressFields : ulong
	{
		None,
		Zip,
		Full
	}

	[Native]
	public enum PaymentMethodType : ulong
	{
		None = 0,
		ApplePay = 1 << 0,
		All = ApplePay
	}

	[Native]
	public enum BankAccountHolderType : long
	{
		Individual,
		Company
	}

	[Native]
	public enum BankAccountStatus : long
	{
		New,
		Validated,
		Verified,
		VerificationFailed,
		Errored
	}

	[Native]
	public enum CardBrand : long
	{
		Visa,
		Amex,
		MasterCard,
		Discover,
		Jcb,
		DinersClub,
		Unknown
	}

	[Native]
	public enum CardFundingType : long
	{
		Debit,
		Credit,
		Prepaid,
		Other
	}

	[Native]
	public enum CardValidationState : long
	{
		Valid,
		Invalid,
		Incomplete
	}

	[Native]
	public enum RedirectContextState : ulong
	{
		NotStarted,
		InProgress,
		Cancelled,
		Completed
	}

	[Native]
	public enum SourceCard3DSecureStatus : long
	{
		Required,
		Optional,
		NotSupported,
		Unknown
	}

	[Native]
	public enum SourceFlow : long
	{
		None,
		Redirect,
		CodeVerification,
		Receiver,
		Unknown
	}

	[Native]
	public enum SourceUsage : long
	{
		Reusable,
		SingleUse,
		Unknown
	}

	[Native]
	public enum SourceStatus : long
	{
		Pending,
		Chargeable,
		Consumed,
		Canceled,
		Failed,
		Unknown
	}

	[Native]
	public enum SourceType : long
	{
		Bancontact,
		Bitcoin,
		Card,
		Giropay,
		Ideal,
		SEPADebit,
		Sofort,
		ThreeDSecure,
		Alipay,
		P24,
		Unknown
	}

	[Native]
	public enum SourceRedirectStatus : long
	{
		Pending,
		Succeeded,
		Failed,
		Unknown
	}

	[Native]
	public enum SourceVerificationStatus : long
	{
		Pending,
		Succeeded,
		Failed,
		Unknown
	}

	[Native]
	public enum ErrorCode : long
	{
		ConnectionError = 40,
		InvalidRequestError = 50,
		APIError = 60,
		CardError = 70,
		CancellationError = 80,
		EphemeralKeyDecodingError = 1000
	}
}

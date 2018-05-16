using System;
using Contacts;
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using PassKit;
using Stripe;
using UIKit;

namespace Stripe.iOS
{
	// typedef void (^STPVoidBlock)();
	delegate void STPVoidBlock ();

	// typedef void (^STPErrorBlock)(NSError * _Nullable);
	delegate void STPErrorBlock ([NullAllowed] NSError arg0);

	// typedef void (^STPJSONResponseCompletionBlock)(NSDictionary * _Nullable, NSError * _Nullable);
	delegate void JsonResponseCompletionBlock ([NullAllowed] NSDictionary arg0, [NullAllowed] NSError arg1);

	// typedef void (^STPTokenCompletionBlock)(STPToken * _Nullable, NSError * _Nullable);
	delegate void TokenCompletionBlock ([NullAllowed] Token arg0, [NullAllowed] NSError arg1);

	// typedef void (^STPSourceCompletionBlock)(STPSource * _Nullable, NSError * _Nullable);
	delegate void SourceCompletionBlock ([NullAllowed] Source arg0, [NullAllowed] NSError arg1);

	// typedef void (^STPSourceProtocolCompletionBlock)(id<STPSourceProtocol> _Nullable, NSError * _Nullable);
	delegate void SourceProtocolCompletionBlock ([NullAllowed] ISourceProtocol arg0, [NullAllowed] NSError arg1);

	// typedef void (^STPShippingMethodsCompletionBlock)(STPShippingStatus, NSError * _Nullable, NSArray<PKShippingMethod *> * _Nullable, PKShippingMethod * _Nullable);
	delegate void ShippingMethodsCompletionBlock (ShippingStatus arg0, [NullAllowed] NSError arg1, [NullAllowed] PKShippingMethod[] arg2, [NullAllowed] PKShippingMethod arg3);

	// typedef void (^STPFileCompletionBlock)(STPFile * _Nullable, NSError * _Nullable);
	delegate void FileCompletionBlock ([NullAllowed] StripeFile arg0, [NullAllowed] NSError arg1);

	// typedef void (^STPCustomerCompletionBlock)(STPCustomer * _Nullable, NSError * _Nullable);
	delegate void CustomerCompletionBlock ([NullAllowed] Customer arg0, [NullAllowed] NSError arg1);

	interface IApiResponseDecodable { }

	// @protocol STPAPIResponseDecodable <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "STPAPIResponseDecodable")]
	interface ApiResponseDecodable
	{
		// @required +(NSArray * _Nonnull)requiredFields;
		[Static, Abstract]
		[Export ("requiredFields")]
		string[] RequiredFields { get; }

		// @required +(instancetype _Nullable)decodedObjectFromAPIResponse:(NSDictionary * _Nullable)response;
		[Static/*, Abstract*/]
		[Export ("decodedObjectFromAPIResponse:")]
		[return: NullAllowed]
		IApiResponseDecodable GetDecodedObject ([NullAllowed] NSDictionary response);

		// @required @property (readonly, copy, nonatomic) NSDictionary * _Nonnull allResponseFields;
		[Abstract]
		[Export ("allResponseFields", ArgumentSemantic.Copy)]
		NSDictionary AllResponseFields { get; }
	}

	// @interface STPFile : NSObject <STPAPIResponseDecodable>
	[BaseType (typeof (NSObject), Name = "STPFile")]
	interface StripeFile : ApiResponseDecodable
	{
		// @property (readonly, nonatomic) NSString * _Nonnull fileId;
		[Export ("fileId")]
		string FileId { get; }

		// @property (readonly, nonatomic) NSDate * _Nonnull created;
		[Export ("created")]
		NSDate Created { get; }

		// @property (readonly, nonatomic) STPFilePurpose purpose;
		[Export ("purpose")]
		FilePurpose Purpose { get; }

		// @property (readonly, nonatomic) NSNumber * _Nonnull size;
		[Export ("size")]
		NSNumber Size { get; }

		// @property (readonly, nonatomic) NSString * _Nonnull type;
		[Export ("type")]
		string Type { get; }

		// +(NSString * _Nullable)stringFromPurpose:(STPFilePurpose)purpose;
		[Static]
		[Export ("stringFromPurpose:")]
		[return: NullAllowed]
		string StringFromPurpose (FilePurpose purpose);

		// Leave this in place as long as ApiResponseDecodable contains this method
		// @required +(instancetype _Nullable)decodedObjectFromAPIResponse:(NSDictionary * _Nullable)response;
		[Static/*, Abstract*/]
		[Export ("decodedObjectFromAPIResponse:")]
		[return: NullAllowed]
		StripeFile GetDecodedObject ([NullAllowed] NSDictionary response);
	}

	// @interface Stripe : NSObject
	[BaseType (typeof (NSObject), Name = "Stripe")]
	interface StripeSdk
	{
		// +(NSString * _Nullable)defaultPublishableKey;
		// +(void)setDefaultPublishableKey:(NSString * _Nonnull)publishableKey;
		[Static]
		[NullAllowed, Export ("defaultPublishableKey")]
		string DefaultPublishableKey { get; set; }

		// +(BOOL)canSubmitPaymentRequest:(PKPaymentRequest * _Nonnull)paymentRequest;
		[Static]
		[Export ("canSubmitPaymentRequest:")]
		bool CanSubmitPaymentRequest (PKPaymentRequest paymentRequest);

		// +(BOOL)deviceSupportsApplePay;
		[Static]
		[Export ("deviceSupportsApplePay")]
		bool DeviceSupportsApplePay { get; }

		// +(PKPaymentRequest * _Nonnull)paymentRequestWithMerchantIdentifier:(NSString * _Nonnull)merchantIdentifier __attribute__((deprecated("")));
		[Static]
		[Export ("paymentRequestWithMerchantIdentifier:")]
		PKPaymentRequest CreatePaymentRequest (string merchantIdentifier);

		// +(PKPaymentRequest * _Nonnull)paymentRequestWithMerchantIdentifier:(NSString * _Nonnull)merchantIdentifier country:(NSString * _Nonnull)countryCode currency:(NSString * _Nonnull)currencyCode __attribute__((availability(ios, introduced=8_0)));
		[Introduced (PlatformName.iOS, 8, 0)]
		[Static]
		[Export ("paymentRequestWithMerchantIdentifier:country:currency:")]
		PKPaymentRequest CreatePaymentRequest (string merchantIdentifier, string countryCode, string currencyCode);

		// +(BOOL)handleStripeURLCallbackWithURL:(NSURL * _Nonnull)url;
		[Static]
		[Export ("handleStripeURLCallbackWithURL:")]
		bool HandleStripeUrlCallback (NSUrl url);
	}

	// @interface STPAPIClient : NSObject
	[BaseType (typeof (NSObject), Name = "STPAPIClient")]
	interface ApiClient
	{
		// +(instancetype _Nonnull)sharedClient;
		[Static]
		[Export ("sharedClient")]
		ApiClient SharedClient { get; }

		// -(instancetype _Nonnull)initWithConfiguration:(STPPaymentConfiguration * _Nonnull)configuration __attribute__((objc_designated_initializer));
		[Export ("initWithConfiguration:")]
		[DesignatedInitializer]
		IntPtr Constructor (PaymentConfiguration configuration);

		// -(instancetype _Nonnull)initWithPublishableKey:(NSString * _Nonnull)publishableKey;
		[Export ("initWithPublishableKey:")]
		IntPtr Constructor (string publishableKey);

		// @property (copy, nonatomic) NSString * _Nullable publishableKey;
		[NullAllowed, Export ("publishableKey")]
		string PublishableKey { get; set; }

		// @property (copy, nonatomic) STPPaymentConfiguration * _Nonnull configuration;
		[Export ("configuration", ArgumentSemantic.Copy)]
		PaymentConfiguration Configuration { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable stripeAccount;
		[NullAllowed, Export ("stripeAccount")]
		string StripeAccount { get; set; }

		// -(void)createTokenWithBankAccount:(STPBankAccountParams * _Nonnull)bankAccount completion:(STPTokenCompletionBlock _Nullable)completion;
		[Export ("createTokenWithBankAccount:completion:")]
		void CreateToken (BankAccountParams bankAccount, [NullAllowed] TokenCompletionBlock completion);

		// -(void)createTokenWithPersonalIDNumber:(NSString * _Nonnull)pii completion:(STPTokenCompletionBlock _Nullable)completion;
		[Export ("createTokenWithPersonalIDNumber:completion:")]
		void CreateToken (string pii, [NullAllowed] TokenCompletionBlock completion);

		// -(void)uploadImage:(UIImage * _Nonnull)image purpose:(STPFilePurpose)purpose completion:(STPFileCompletionBlock _Nullable)completion;
		[Export ("uploadImage:purpose:completion:")]
		void UploadImage (UIImage image, FilePurpose purpose, [NullAllowed] FileCompletionBlock completion);

		// -(void)createTokenWithCard:(STPCardParams * _Nonnull)card completion:(STPTokenCompletionBlock _Nullable)completion;
		[Export ("createTokenWithCard:completion:")]
		void CreateToken (CardParams card, [NullAllowed] TokenCompletionBlock completion);

		// -(void)createSourceWithParams:(STPSourceParams * _Nonnull)params completion:(STPSourceCompletionBlock _Nonnull)completion;
		[Export ("createSourceWithParams:completion:")]
		void CreateSource (SourceParams @params, SourceCompletionBlock completion);

		// -(void)retrieveSourceWithId:(NSString * _Nonnull)identifier clientSecret:(NSString * _Nonnull)secret completion:(STPSourceCompletionBlock _Nonnull)completion;
		[Export ("retrieveSourceWithId:clientSecret:completion:")]
		void RetrieveSource (string identifier, string secret, SourceCompletionBlock completion);

		// -(void)startPollingSourceWithId:(NSString * _Nonnull)identifier clientSecret:(NSString * _Nonnull)secret timeout:(NSTimeInterval)timeout completion:(STPSourceCompletionBlock _Nonnull)completion __attribute__((deprecated("You should poll your own backend to update based on source status change webhook events it may receive."))) __attribute__((availability(ios_app_extension, unavailable))) __attribute__((availability(macos_app_extension, unavailable)));
		[Export ("startPollingSourceWithId:clientSecret:timeout:completion:")]
		void StartPollingSource (string identifier, string secret, double timeout, SourceCompletionBlock completion);

		// -(void)stopPollingSourceWithId:(NSString * _Nonnull)identifier __attribute__((deprecated(""))) __attribute__((availability(ios_app_extension, unavailable))) __attribute__((availability(macos_app_extension, unavailable)));
		[Export ("stopPollingSourceWithId:")]
		void StopPollingSource (string identifier);

		// -(void)createTokenWithPayment:(PKPayment * _Nonnull)payment completion:(STPTokenCompletionBlock _Nonnull)completion;
		[Export ("createTokenWithPayment:completion:")]
		void CreateToken (PKPayment payment, TokenCompletionBlock completion);

		// -(void)createSourceWithPayment:(PKPayment * _Nonnull)payment completion:(STPSourceCompletionBlock _Nonnull)completion;
		[Export ("createSourceWithPayment:completion:")]
		void CreateSource (PKPayment payment, SourceCompletionBlock completion);
	}

	//// @interface BankAccounts (STPAPIClient)
	//[Category]
	//[BaseType (typeof (ApiClient))]
	//interface STPAPIClient_BankAccounts
	//{
	//	All of these have been moved to ApiClient
	//	// -(void)createTokenWithBankAccount:(STPBankAccountParams * _Nonnull)bankAccount completion:(STPTokenCompletionBlock _Nullable)completion;
	//	[Export ("createTokenWithBankAccount:completion:")]
	//	void CreateTokenWithBankAccount (BankAccountParams bankAccount, [NullAllowed] TokenCompletionBlock completion);
	//}

	//// @interface PII (STPAPIClient)
	//[Category]
	//[BaseType (typeof (ApiClient))]
	//interface STPAPIClient_PII
	//{
	//	All of these have been moved to ApiClient
	//	// -(void)createTokenWithPersonalIDNumber:(NSString * _Nonnull)pii completion:(STPTokenCompletionBlock _Nullable)completion;
	//	[Export ("createTokenWithPersonalIDNumber:completion:")]
	//	void CreateTokenWithPersonalIDNumber (string pii, [NullAllowed] TokenCompletionBlock completion);
	//}

	//// @interface Upload (STPAPIClient)
	//[Category]
	//[BaseType (typeof (ApiClient))]
	//interface STPAPIClient_Upload
	//{
	//	All of these have been moved to ApiClient
	//	// -(void)uploadImage:(UIImage * _Nonnull)image purpose:(STPFilePurpose)purpose completion:(STPFileCompletionBlock _Nullable)completion;
	//	[Export ("uploadImage:purpose:completion:")]
	//	void UploadImage (UIImage image, FilePurpose purpose, [NullAllowed] FileCompletionBlock completion);
	//}

	//// @interface CreditCards (STPAPIClient)
	//[Category]
	//[BaseType (typeof (ApiClient))]
	//interface STPAPIClient_CreditCards
	//{
	//	All of these have been moved to ApiClient
	//	// -(void)createTokenWithCard:(STPCardParams * _Nonnull)card completion:(STPTokenCompletionBlock _Nullable)completion;
	//	[Export ("createTokenWithCard:completion:")]
	//	void CreateTokenWithCard (CardParams card, [NullAllowed] TokenCompletionBlock completion);
	//}

	// @interface ApplePay (Stripe)
	//[Category]
	//[BaseType (typeof (StripeSdk))]
	//interface Stripe_ApplePay
	//{
	// All of these have been moved to StripeSdk

	//// +(BOOL)canSubmitPaymentRequest:(PKPaymentRequest * _Nonnull)paymentRequest;
	//[Static]
	//[Export ("canSubmitPaymentRequest:")]
	//bool CanSubmitPaymentRequest (PKPaymentRequest paymentRequest);

	//// +(BOOL)deviceSupportsApplePay;
	//[Static]
	//[Export ("deviceSupportsApplePay")]
	//bool DeviceSupportsApplePay { get; }

	//// +(PKPaymentRequest * _Nonnull)paymentRequestWithMerchantIdentifier:(NSString * _Nonnull)merchantIdentifier __attribute__((deprecated("")));
	//[Static]
	//[Export ("paymentRequestWithMerchantIdentifier:")]
	//PKPaymentRequest PaymentRequestWithMerchantIdentifier (string merchantIdentifier);

	//// +(PKPaymentRequest * _Nonnull)paymentRequestWithMerchantIdentifier:(NSString * _Nonnull)merchantIdentifier country:(NSString * _Nonnull)countryCode currency:(NSString * _Nonnull)currencyCode __attribute__((availability(ios, introduced=8_0)));
	//[Introduced (PlatformName.iOS, 8, 0)]
	//[Static]
	//[Export ("paymentRequestWithMerchantIdentifier:country:currency:")]
	//PKPaymentRequest PaymentRequestWithMerchantIdentifier (string merchantIdentifier, string countryCode, string currencyCode);
	//}

	// @interface Sources (STPAPIClient)
	//[Category]
	//[BaseType (typeof (ApiClient))]
	//interface STPAPIClient_Sources
	//{
	//	All of these have been moved to ApiClient

	//// -(void)createSourceWithParams:(STPSourceParams * _Nonnull)params completion:(STPSourceCompletionBlock _Nonnull)completion;
	//[Export ("createSourceWithParams:completion:")]
	//void CreateSourceWithParams (SourceParams @params, SourceCompletionBlock completion);

	//// -(void)retrieveSourceWithId:(NSString * _Nonnull)identifier clientSecret:(NSString * _Nonnull)secret completion:(STPSourceCompletionBlock _Nonnull)completion;
	//[Export ("retrieveSourceWithId:clientSecret:completion:")]
	//void RetrieveSourceWithId (string identifier, string secret, SourceCompletionBlock completion);

	//// -(void)startPollingSourceWithId:(NSString * _Nonnull)identifier clientSecret:(NSString * _Nonnull)secret timeout:(NSTimeInterval)timeout completion:(STPSourceCompletionBlock _Nonnull)completion __attribute__((deprecated("You should poll your own backend to update based on source status change webhook events it may receive."))) __attribute__((availability(ios_app_extension, unavailable))) __attribute__((availability(macos_app_extension, unavailable)));
	//[Export ("startPollingSourceWithId:clientSecret:timeout:completion:")]
	//void StartPollingSourceWithId (string identifier, string secret, double timeout, SourceCompletionBlock completion);

	//// -(void)stopPollingSourceWithId:(NSString * _Nonnull)identifier __attribute__((deprecated(""))) __attribute__((availability(ios_app_extension, unavailable))) __attribute__((availability(macos_app_extension, unavailable)));
	//[Export ("stopPollingSourceWithId:")]
	//void StopPollingSourceWithId (string identifier);
	//}

	// @interface STPURLCallbackHandlerAdditions (Stripe)
	//[Category]
	//[BaseType (typeof (StripeSdk))]
	//interface Stripe_STPURLCallbackHandlerAdditions
	//{
	// All of these have been moved to StripeSdk

	//// +(BOOL)handleStripeURLCallbackWithURL:(NSURL * _Nonnull)url;
	//[Static]
	//[Export ("handleStripeURLCallbackWithURL:")]
	//bool HandleStripeURLCallbackWithURL (NSUrl url);
	//}

	// @interface STPAddress : NSObject <STPAPIResponseDecodable>
	[BaseType (typeof (NSObject), Name = "STPAddress")]
	interface Address : ApiResponseDecodable
	{
		// @property (copy, nonatomic) NSString * _Nullable name;
		[NullAllowed, Export ("name")]
		string Name { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable line1;
		[NullAllowed, Export ("line1")]
		string Line1 { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable line2;
		[NullAllowed, Export ("line2")]
		string Line2 { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable city;
		[NullAllowed, Export ("city")]
		string City { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable state;
		[NullAllowed, Export ("state")]
		string State { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable postalCode;
		[NullAllowed, Export ("postalCode")]
		string PostalCode { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable country;
		[NullAllowed, Export ("country")]
		string Country { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable phone;
		[NullAllowed, Export ("phone")]
		string Phone { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable email;
		[NullAllowed, Export ("email")]
		string Email { get; set; }

		// +(NSDictionary * _Nullable)shippingInfoForChargeWithAddress:(STPAddress * _Nullable)address shippingMethod:(PKShippingMethod * _Nullable)method;
		[Static]
		[Export ("shippingInfoForChargeWithAddress:shippingMethod:")]
		[return: NullAllowed]
		NSDictionary GetShippingInfoForCharge ([NullAllowed] Address address, [NullAllowed] PKShippingMethod method);

		// ABRecordRef could be CNContact, CNGroup, or CNContainer.  Using NSObject
		// -(instancetype _Nonnull)initWithABRecord:(ABRecordRef _Nonnull)record;
		[Export ("initWithABRecord:")]
		IntPtr Constructor (NSObject record);

		// ABRecordRef could be CNContact, CNGroup, or CNContainer.  Using NSObject
		// -(ABRecordRef _Nonnull)ABRecordValue;
		[Export ("ABRecordValue")]
		NSObject ABRecordValue { get; }

		// -(instancetype _Nonnull)initWithPKContact:(PKContact * _Nonnull)contact __attribute__((availability(ios, introduced=9_0)));
		[Introduced (PlatformName.iOS, 9, 0)]
		[Export ("initWithPKContact:")]
		IntPtr Constructor (PKContact contact);

		// -(PKContact * _Nonnull)PKContactValue __attribute__((availability(ios, introduced=9_0)));
		[Introduced (PlatformName.iOS, 9, 0)]
		[Export ("PKContactValue")]
		PKContact PKContactValue { get; }

		// -(instancetype _Nonnull)initWithCNContact:(CNContact * _Nonnull)contact __attribute__((availability(ios, introduced=9_0)));
		[Introduced (PlatformName.iOS, 9, 0)]
		[Export ("initWithCNContact:")]
		IntPtr Constructor (CNContact contact);

		// -(BOOL)containsRequiredFields:(STPBillingAddressFields)requiredFields;
		[Export ("containsRequiredFields:")]
		bool ContainsRequiredFields (BillingAddressFields requiredFields);

		// -(BOOL)containsRequiredShippingAddressFields:(PKAddressField)requiredFields;
		[Export ("containsRequiredShippingAddressFields:")]
		bool ContainsRequiredShippingAddressFields (PKAddressField requiredFields);

		// +(PKAddressField)applePayAddressFieldsFromBillingAddressFields:(STPBillingAddressFields)billingAddressFields;
		[Static]
		[Export ("applePayAddressFieldsFromBillingAddressFields:")]
		PKAddressField GetApplePayAddressFields (BillingAddressFields billingAddressFields);

		// Leave this in place as long as ApiResponseDecodable contains this method
		// @required +(instancetype _Nullable)decodedObjectFromAPIResponse:(NSDictionary * _Nullable)response;
		[Static/*, Abstract*/]
		[Export ("decodedObjectFromAPIResponse:")]
		[return: NullAllowed]
		Address GetDecodedObject ([NullAllowed] NSDictionary response);
	}

	interface IFormEncodable { }

	// @protocol STPFormEncodable <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "STPFormEncodable")]
	interface FormEncodable
	{
		// @required +(NSString * _Nullable)rootObjectName;
		[Static, Abstract]
		[NullAllowed, Export ("rootObjectName")]
		string RootObjectName { get; }

		// @required +(NSDictionary * _Nonnull)propertyNamesToFormFieldNamesMapping;
		[Static, Abstract]
		[Export ("propertyNamesToFormFieldNamesMapping")]
		NSDictionary PropertyNamesToFormFieldNamesMapping { get; }

		// @required @property (readwrite, copy, nonatomic) NSDictionary * _Nonnull additionalAPIParameters;
		[Abstract]
		[Export ("additionalAPIParameters", ArgumentSemantic.Copy)]
		NSDictionary AdditionalApiParameters { get; set; }
	}

	// @interface STPCardParams : NSObject <STPFormEncodable>
	[BaseType (typeof (NSObject), Name = "STPCardParams")]
	interface CardParams : FormEncodable
	{
		// @property (copy, nonatomic) NSString * _Nullable number;
		[NullAllowed, Export ("number")]
		string Number { get; set; }

		// -(NSString * _Nullable)last4;
		[NullAllowed, Export ("last4")]
		string Last4 { get; }

		// @property (nonatomic) NSUInteger expMonth;
		[Export ("expMonth")]
		nuint ExpMonth { get; set; }

		// @property (nonatomic) NSUInteger expYear;
		[Export ("expYear")]
		nuint ExpYear { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable cvc;
		[NullAllowed, Export ("cvc")]
		string Cvc { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable name;
		[NullAllowed, Export ("name")]
		string Name { get; set; }

		// @property (nonatomic, strong) STPAddress * _Nonnull address;
		[Export ("address", ArgumentSemantic.Strong)]
		Address Address { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable currency;
		[NullAllowed, Export ("currency")]
		string Currency { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable addressLine1 __attribute__((deprecated("Use address.line1")));
		[NullAllowed, Export ("addressLine1")]
		string AddressLine1 { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable addressLine2 __attribute__((deprecated("Use address.line2")));
		[NullAllowed, Export ("addressLine2")]
		string AddressLine2 { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable addressCity __attribute__((deprecated("Use address.city")));
		[NullAllowed, Export ("addressCity")]
		string AddressCity { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable addressState __attribute__((deprecated("Use address.state")));
		[NullAllowed, Export ("addressState")]
		string AddressState { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable addressZip __attribute__((deprecated("Use address.postalCode")));
		[NullAllowed, Export ("addressZip")]
		string AddressZip { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable addressCountry __attribute__((deprecated("Use address.country")));
		[NullAllowed, Export ("addressCountry")]
		string AddressCountry { get; set; }
	}

	// @interface STPCoreViewController : UIViewController
	[BaseType (typeof (UIViewController), Name = "STPCoreViewController")]
	interface CoreViewController
	{
		// -(instancetype _Nonnull)initWithTheme:(STPTheme * _Nonnull)theme __attribute__((objc_designated_initializer));
		[Export ("initWithTheme:")]
		[DesignatedInitializer]
		IntPtr Constructor (Theme theme);

		// -(instancetype _Nonnull)initWithNibName:(NSString * _Nullable)nibNameOrNil bundle:(NSBundle * _Nullable)nibBundleOrNil __attribute__((objc_designated_initializer));
		[Export ("initWithNibName:bundle:")]
		[DesignatedInitializer]
		IntPtr Constructor ([NullAllowed] string nibNameOrNil, [NullAllowed] NSBundle nibBundleOrNil);

		// Genearted by default
		//// -(instancetype _Nullable)initWithCoder:(NSCoder * _Nonnull)aDecoder __attribute__((objc_designated_initializer));
		//[Export ("initWithCoder:")]
		//[DesignatedInitializer]
		//IntPtr Constructor (NSCoder aDecoder);
	}

	// @interface STPCoreScrollViewController : STPCoreViewController
	[BaseType (typeof (CoreViewController), Name = "STPCoreScrollViewController")]
	interface CoreScrollViewController
	{
	}

	// @interface STPCoreTableViewController : STPCoreScrollViewController
	[BaseType (typeof (CoreScrollViewController), Name = "STPCoreTableViewController")]
	interface CoreTableViewController
	{
	}

	interface ISourceProtocol { }

	// @protocol STPSourceProtocol <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "STPSourceProtocol")]
	interface SourceProtocol
	{
		// @required @property (readonly, nonatomic) NSString * _Nonnull stripeID;
		[Abstract]
		[Export ("stripeID")]
		string StripeId { get; }
	}

	// @interface STPCustomer : NSObject <STPAPIResponseDecodable>
	[BaseType (typeof (NSObject), Name = "STPCustomer")]
	interface Customer : ApiResponseDecodable
	{
		// +(instancetype _Nonnull)customerWithStripeID:(NSString * _Nonnull)stripeID defaultSource:(id<STPSourceProtocol> _Nullable)defaultSource sources:(NSArray<id<STPSourceProtocol>> * _Nonnull)sources;
		[Static]
		[Export ("customerWithStripeID:defaultSource:sources:")]
		Customer GetCustomer (string stripeId, [NullAllowed] ISourceProtocol defaultSource, ISourceProtocol[] sources);

		// @property (readonly, copy, nonatomic) NSString * _Nonnull stripeID;
		[Export ("stripeID")]
		string StripeId { get; }

		// @property (readonly, nonatomic) id<STPSourceProtocol> _Nullable defaultSource;
		[NullAllowed, Export ("defaultSource")]
		ISourceProtocol DefaultSource { get; }

		// @property (readonly, nonatomic) NSArray<id<STPSourceProtocol>> * _Nonnull sources;
		[Export ("sources")]
		ISourceProtocol[] Sources { get; }

		// @property (readonly, nonatomic) STPAddress * _Nullable shippingAddress;
		[NullAllowed, Export ("shippingAddress")]
		Address ShippingAddress { get; }

		// Leave this in place as long as ApiResponseDecodable contains this method
		// @required +(instancetype _Nullable)decodedObjectFromAPIResponse:(NSDictionary * _Nullable)response;
		[Static/*, Abstract*/]
		[Export ("decodedObjectFromAPIResponse:")]
		[return: NullAllowed]
		Customer GetDecodedObject ([NullAllowed] NSDictionary response);
	}

	// @interface STPCustomerDeserializer : NSObject
	[BaseType (typeof (NSObject), Name = "STPCustomerDeserializer")]
	interface CustomerDeserializer
	{
		// -(instancetype _Nonnull)initWithData:(NSData * _Nullable)data urlResponse:(NSURLResponse * _Nullable)urlResponse error:(NSError * _Nullable)error;
		[Export ("initWithData:urlResponse:error:")]
		IntPtr Constructor ([NullAllowed] NSData data, [NullAllowed] NSUrlResponse urlResponse, [NullAllowed] NSError error);

		// -(instancetype _Nonnull)initWithJSONResponse:(id _Nonnull)json;
		[Export ("initWithJSONResponse:")]
		IntPtr Constructor (NSObject json);

		// @property (readonly, nonatomic) STPCustomer * _Nullable customer;
		[NullAllowed, Export ("customer")]
		Customer Customer { get; }

		// @property (readonly, nonatomic) NSError * _Nullable error;
		[NullAllowed, Export ("error")]
		NSError Error { get; }
	}

	interface IBackendApiAdapter { }

	// @protocol STPBackendAPIAdapter <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "STPBackendAPIAdapter")]
	interface BackendApiAdapter
	{
		// @required -(void)retrieveCustomer:(STPCustomerCompletionBlock _Nullable)completion;
		[Abstract]
		[Export ("retrieveCustomer:")]
		void RetrieveCustomer ([NullAllowed] CustomerCompletionBlock completion);

		// @required -(void)attachSourceToCustomer:(id<STPSourceProtocol> _Nonnull)source completion:(STPErrorBlock _Nonnull)completion;
		[Abstract]
		[Export ("attachSourceToCustomer:completion:")]
		void AttachSourceToCustomer (ISourceProtocol source, STPErrorBlock completion);

		// @required -(void)selectDefaultCustomerSource:(id<STPSourceProtocol> _Nonnull)source completion:(STPErrorBlock _Nonnull)completion;
		[Abstract]
		[Export ("selectDefaultCustomerSource:completion:")]
		void SelectDefaultCustomerSource (ISourceProtocol source, STPErrorBlock completion);

		// @optional -(void)detachSourceFromCustomer:(id<STPSourceProtocol> _Nonnull)source completion:(STPErrorBlock _Nullable)completion;
		[Export ("detachSourceFromCustomer:completion:")]
		void DetachSource (ISourceProtocol source, [NullAllowed] STPErrorBlock completion);

		// @optional -(void)updateCustomerWithShippingAddress:(STPAddress * _Nonnull)shipping completion:(STPErrorBlock _Nullable)completion;
		[Export ("updateCustomerWithShippingAddress:completion:")]
		void UpdateCustomer (Address shipping, [NullAllowed] STPErrorBlock completion);
	}

	interface IPaymentMethod { }

	// @protocol STPPaymentMethod <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "STPPaymentMethod")]
	interface PaymentMethod
	{
		// @required @property (readonly, nonatomic, strong) UIImage * _Nonnull image;
		[Abstract]
		[Export ("image", ArgumentSemantic.Strong)]
		UIImage Image { get; }

		// @required @property (readonly, nonatomic, strong) UIImage * _Nonnull templateImage;
		[Abstract]
		[Export ("templateImage", ArgumentSemantic.Strong)]
		UIImage TemplateImage { get; }

		// @required @property (readonly, nonatomic, strong) NSString * _Nonnull label;
		[Abstract]
		[Export ("label", ArgumentSemantic.Strong)]
		string Label { get; }
	}

	// @interface STPTheme : NSObject <NSCopying>
	[BaseType (typeof (NSObject), Name = "STPTheme")]
	interface Theme : INSCopying
	{
		// +(STPTheme * _Nonnull)defaultTheme;
		[Static]
		[Export ("defaultTheme")]
		Theme DefaultTheme { get; }

		// @property (copy, nonatomic) UIColor * _Null_unspecified primaryBackgroundColor;
		[Export ("primaryBackgroundColor", ArgumentSemantic.Copy)]
		UIColor PrimaryBackgroundColor { get; set; }

		// @property (copy, nonatomic) UIColor * _Null_unspecified secondaryBackgroundColor;
		[Export ("secondaryBackgroundColor", ArgumentSemantic.Copy)]
		UIColor SecondaryBackgroundColor { get; set; }

		// @property (readonly, nonatomic) UIColor * _Nonnull tertiaryBackgroundColor;
		[Export ("tertiaryBackgroundColor")]
		UIColor TertiaryBackgroundColor { get; }

		// @property (readonly, nonatomic) UIColor * _Nonnull quaternaryBackgroundColor;
		[Export ("quaternaryBackgroundColor")]
		UIColor QuaternaryBackgroundColor { get; }

		// @property (copy, nonatomic) UIColor * _Null_unspecified primaryForegroundColor;
		[Export ("primaryForegroundColor", ArgumentSemantic.Copy)]
		UIColor PrimaryForegroundColor { get; set; }

		// @property (copy, nonatomic) UIColor * _Null_unspecified secondaryForegroundColor;
		[Export ("secondaryForegroundColor", ArgumentSemantic.Copy)]
		UIColor SecondaryForegroundColor { get; set; }

		// @property (readonly, nonatomic) UIColor * _Nonnull tertiaryForegroundColor;
		[Export ("tertiaryForegroundColor")]
		UIColor TertiaryForegroundColor { get; }

		// @property (copy, nonatomic) UIColor * _Null_unspecified accentColor;
		[Export ("accentColor", ArgumentSemantic.Copy)]
		UIColor AccentColor { get; set; }

		// @property (copy, nonatomic) UIColor * _Null_unspecified errorColor;
		[Export ("errorColor", ArgumentSemantic.Copy)]
		UIColor ErrorColor { get; set; }

		// @property (copy, nonatomic) UIFont * _Null_unspecified font;
		[Export ("font", ArgumentSemantic.Copy)]
		UIFont Font { get; set; }

		// @property (copy, nonatomic) UIFont * _Null_unspecified emphasisFont;
		[Export ("emphasisFont", ArgumentSemantic.Copy)]
		UIFont EmphasisFont { get; set; }

		// @property (nonatomic) UIBarStyle barStyle;
		[Export ("barStyle", ArgumentSemantic.Assign)]
		UIBarStyle BarStyle { get; set; }

		// @property (nonatomic) BOOL translucentNavigationBar;
		[Export ("translucentNavigationBar")]
		bool TranslucentNavigationBar { get; set; }

		// @property (readonly, nonatomic) UIFont * _Nonnull smallFont;
		[Export ("smallFont")]
		UIFont SmallFont { get; }

		// @property (readonly, nonatomic) UIFont * _Nonnull largeFont;
		[Export ("largeFont")]
		UIFont LargeFont { get; }
	}

	// @interface STPPaymentConfiguration : NSObject <NSCopying>
	[BaseType (typeof (NSObject), Name = "STPPaymentConfiguration")]
	interface PaymentConfiguration : INSCopying
	{
		// +(instancetype _Nonnull)sharedConfiguration;
		[Static]
		[Export ("sharedConfiguration")]
		PaymentConfiguration SharedConfiguration { get; }

		// @property (readwrite, copy, nonatomic) NSString * _Nonnull publishableKey;
		[Export ("publishableKey")]
		string PublishableKey { get; set; }

		// @property (assign, readwrite, nonatomic) STPPaymentMethodType additionalPaymentMethods;
		[Export ("additionalPaymentMethods", ArgumentSemantic.Assign)]
		PaymentMethodType AdditionalPaymentMethods { get; set; }

		// @property (assign, readwrite, nonatomic) STPBillingAddressFields requiredBillingAddressFields;
		[Export ("requiredBillingAddressFields", ArgumentSemantic.Assign)]
		BillingAddressFields RequiredBillingAddressFields { get; set; }

		// @property (assign, readwrite, nonatomic) PKAddressField requiredShippingAddressFields;
		[Export ("requiredShippingAddressFields", ArgumentSemantic.Assign)]
		PKAddressField RequiredShippingAddressFields { get; set; }

		// @property (assign, readwrite, nonatomic) BOOL verifyPrefilledShippingAddress;
		[Export ("verifyPrefilledShippingAddress")]
		bool VerifyPrefilledShippingAddress { get; set; }

		// @property (assign, readwrite, nonatomic) STPShippingType shippingType;
		[Export ("shippingType", ArgumentSemantic.Assign)]
		ShippingType ShippingType { get; set; }

		// @property (readwrite, copy, nonatomic) NSString * _Nonnull companyName;
		[Export ("companyName")]
		string CompanyName { get; set; }

		// @property (readwrite, copy, nonatomic) NSString * _Nullable appleMerchantIdentifier;
		[NullAllowed, Export ("appleMerchantIdentifier")]
		string AppleMerchantIdentifier { get; set; }

		// @property (assign, readwrite, nonatomic) BOOL canDeletePaymentMethods;
		[Export ("canDeletePaymentMethods")]
		bool CanDeletePaymentMethods { get; set; }
	}

	// @interface STPUserInformation : NSObject <NSCopying>
	[BaseType (typeof (NSObject), Name = "STPUserInformation")]
	interface UserInformation : INSCopying
	{
		// @property (nonatomic, strong) STPAddress * _Nullable billingAddress;
		[NullAllowed, Export ("billingAddress", ArgumentSemantic.Strong)]
		Address BillingAddress { get; set; }

		// @property (nonatomic, strong) STPAddress * _Nullable shippingAddress;
		[NullAllowed, Export ("shippingAddress", ArgumentSemantic.Strong)]
		Address ShippingAddress { get; set; }
	}

	// @interface STPAddCardViewController : STPCoreTableViewController
	[BaseType (typeof (CoreTableViewController), Name = "STPAddCardViewController")]
	interface AddCardViewController
	{
		// -(instancetype _Nonnull)initWithConfiguration:(STPPaymentConfiguration * _Nonnull)configuration theme:(STPTheme * _Nonnull)theme;
		[Export ("initWithConfiguration:theme:")]
		IntPtr Constructor (PaymentConfiguration configuration, Theme theme);

		// @property (nonatomic, weak) id<STPAddCardViewControllerDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IAddCardViewControllerDelegate Delegate { get; set; }

		// @property (nonatomic, strong) STPUserInformation * _Nullable prefilledInformation;
		[NullAllowed, Export ("prefilledInformation", ArgumentSemantic.Strong)]
		UserInformation PrefilledInformation { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable managedAccountCurrency;
		[NullAllowed, Export ("managedAccountCurrency")]
		string ManagedAccountCurrency { get; set; }

		// @property (nonatomic, strong) UIView * _Nullable customFooterView;
		[NullAllowed, Export ("customFooterView", ArgumentSemantic.Strong)]
		UIView CustomFooterView { get; set; }
	}

	interface IAddCardViewControllerDelegate { }

	// @protocol STPAddCardViewControllerDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "STPAddCardViewControllerDelegate")]
	interface AddCardViewControllerDelegate
	{
		// @required -(void)addCardViewControllerDidCancel:(STPAddCardViewController * _Nonnull)addCardViewController;
		[Abstract]
		[Export ("addCardViewControllerDidCancel:")]
		void AddCardViewControllerCancelled (AddCardViewController addCardViewController);

		// @required -(void)addCardViewController:(STPAddCardViewController * _Nonnull)addCardViewController didCreateToken:(STPToken * _Nonnull)token completion:(STPErrorBlock _Nonnull)completion;
		[Abstract]
		[Export ("addCardViewController:didCreateToken:completion:")]
		void AddCardViewControllerCreatedToken (AddCardViewController addCardViewController, Token token, STPErrorBlock completion);
	}

	// @interface ApplePay (STPAPIClient)
	//[Category]
	//[BaseType (typeof (ApiClient))]
	//interface STPAPIClient_ApplePay
	//{
	//	All of these have been moved to ApiClient

	//	// -(void)createTokenWithPayment:(PKPayment * _Nonnull)payment completion:(STPTokenCompletionBlock _Nonnull)completion;
	//	[Export ("createTokenWithPayment:completion:")]
	//	void CreateTokenWithPayment (PKPayment payment, TokenCompletionBlock completion);

	//	// -(void)createSourceWithPayment:(PKPayment * _Nonnull)payment completion:(STPSourceCompletionBlock _Nonnull)completion;
	//	[Export ("createSourceWithPayment:completion:")]
	//	void CreateSourceWithPayment (PKPayment payment, SourceCompletionBlock completion);
	//}

	// @interface STPApplePayPaymentMethod : NSObject <STPPaymentMethod>
	[BaseType (typeof (NSObject), Name = "STPApplePayPaymentMethod")]
	interface ApplePayPaymentMethod : PaymentMethod
	{
	}

	// @interface STPBankAccountParams : NSObject <STPFormEncodable>
	[BaseType (typeof (NSObject), Name = "STPBankAccountParams")]
	interface BankAccountParams : FormEncodable
	{
		// @property (copy, nonatomic) NSString * _Nullable accountNumber;
		[NullAllowed, Export ("accountNumber")]
		string AccountNumber { get; set; }

		// @property (readonly, nonatomic) NSString * _Nullable last4;
		[NullAllowed, Export ("last4")]
		string Last4 { get; }

		// @property (copy, nonatomic) NSString * _Nullable routingNumber;
		[NullAllowed, Export ("routingNumber")]
		string RoutingNumber { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable country;
		[NullAllowed, Export ("country")]
		string Country { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable currency;
		[NullAllowed, Export ("currency")]
		string Currency { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable accountHolderName;
		[NullAllowed, Export ("accountHolderName")]
		string AccountHolderName { get; set; }

		// @property (assign, nonatomic) STPBankAccountHolderType accountHolderType;
		[Export ("accountHolderType", ArgumentSemantic.Assign)]
		BankAccountHolderType AccountHolderType { get; set; }
	}

	// @interface STPBankAccount : NSObject <STPAPIResponseDecodable, STPSourceProtocol>
	[BaseType (typeof (NSObject), Name = "STPBankAccount")]
	[DisableDefaultCtor]
	interface BankAccount : ApiResponseDecodable, SourceProtocol
	{
		// @property (readonly, nonatomic) NSString * _Nullable routingNumber;
		[NullAllowed, Export ("routingNumber")]
		string RoutingNumber { get; }

		// @property (readonly, nonatomic) NSString * _Nonnull country;
		[Export ("country")]
		string Country { get; }

		// @property (readonly, nonatomic) NSString * _Nonnull currency;
		[Export ("currency")]
		string Currency { get; }

		// @property (readonly, nonatomic) NSString * _Nonnull last4;
		[Export ("last4")]
		string Last4 { get; }

		// @property (readonly, nonatomic) NSString * _Nonnull bankName;
		[Export ("bankName")]
		string BankName { get; }

		// @property (readonly, nonatomic) NSString * _Nullable accountHolderName;
		[NullAllowed, Export ("accountHolderName")]
		string AccountHolderName { get; }

		// @property (readonly, nonatomic) STPBankAccountHolderType accountHolderType;
		[Export ("accountHolderType")]
		BankAccountHolderType AccountHolderType { get; }

		// @property (readonly, nonatomic) NSString * _Nullable fingerprint;
		[NullAllowed, Export ("fingerprint")]
		string Fingerprint { get; }

		// @property (readonly, copy, nonatomic) NSDictionary<NSString *,NSString *> * _Nullable metadata;
		[NullAllowed, Export ("metadata", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSString> Metadata { get; }

		// @property (readonly, nonatomic) STPBankAccountStatus status;
		[Export ("status")]
		BankAccountStatus Status { get; }

		// @property (readonly, nonatomic) NSString * _Nonnull bankAccountId __attribute__((deprecated("Use stripeID (defined in STPSourceProtocol)")));
		[Export ("bankAccountId")]
		string BankAccountId { get; }

		// Leave this in place as long as ApiResponseDecodable contains this method
		// @required +(instancetype _Nullable)decodedObjectFromAPIResponse:(NSDictionary * _Nullable)response;
		[Static/*, Abstract*/]
		[Export ("decodedObjectFromAPIResponse:")]
		[return: NullAllowed]
		BankAccount GetDecodedObject ([NullAllowed] NSDictionary response);
	}

	// @interface STPCard : NSObject <STPAPIResponseDecodable, STPPaymentMethod, STPSourceProtocol>
	[BaseType (typeof (NSObject), Name = "STPCard")]
	[DisableDefaultCtor]
	interface Card : ApiResponseDecodable, PaymentMethod, SourceProtocol
	{
		// @property (readonly, nonatomic) NSString * _Nonnull last4;
		[Export ("last4")]
		string Last4 { get; }

		// @property (readonly, nonatomic) NSString * _Nullable dynamicLast4;
		[NullAllowed, Export ("dynamicLast4")]
		string DynamicLast4 { get; }

		// @property (readonly, nonatomic) BOOL isApplePayCard;
		[Export ("isApplePayCard")]
		bool IsApplePayCard { get; }

		// @property (readonly, nonatomic) NSUInteger expMonth;
		[Export ("expMonth")]
		nuint ExpMonth { get; }

		// @property (readonly, nonatomic) NSUInteger expYear;
		[Export ("expYear")]
		nuint ExpYear { get; }

		// @property (readonly, nonatomic) NSString * _Nullable name;
		[NullAllowed, Export ("name")]
		string Name { get; }

		// @property (readonly, nonatomic) STPAddress * _Nonnull address;
		[Export ("address")]
		Address Address { get; }

		// @property (readonly, nonatomic) STPCardBrand brand;
		[Export ("brand")]
		CardBrand Brand { get; }

		// @property (readonly, nonatomic) STPCardFundingType funding;
		[Export ("funding")]
		CardFundingType Funding { get; }

		// @property (readonly, nonatomic) NSString * _Nullable country;
		[NullAllowed, Export ("country")]
		string Country { get; }

		// @property (readonly, nonatomic) NSString * _Nullable currency;
		[NullAllowed, Export ("currency")]
		string Currency { get; }

		// @property (readonly, copy, nonatomic) NSDictionary<NSString *,NSString *> * _Nullable metadata;
		[NullAllowed, Export ("metadata", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSString> Metadata { get; }

		// +(NSString * _Nonnull)stringFromBrand:(STPCardBrand)brand;
		[Static]
		[Export ("stringFromBrand:")]
		string StringFromBrand (CardBrand brand);

		// +(STPCardBrand)brandFromString:(NSString * _Nonnull)string;
		[Static]
		[Export ("brandFromString:")]
		CardBrand BrandFromString (string @string);

		// @property (readonly, nonatomic) NSString * _Nonnull cardId __attribute__((deprecated("Use stripeID (defined in STPSourceProtocol)")));
		[Export ("cardId")]
		string CardId { get; }

		// @property (readonly, nonatomic) NSString * _Nullable addressLine1 __attribute__((deprecated("Use address.line1")));
		[NullAllowed, Export ("addressLine1")]
		string AddressLine1 { get; }

		// @property (readonly, nonatomic) NSString * _Nullable addressLine2 __attribute__((deprecated("Use address.line2")));
		[NullAllowed, Export ("addressLine2")]
		string AddressLine2 { get; }

		// @property (readonly, nonatomic) NSString * _Nullable addressCity __attribute__((deprecated("Use address.city")));
		[NullAllowed, Export ("addressCity")]
		string AddressCity { get; }

		// @property (readonly, nonatomic) NSString * _Nullable addressState __attribute__((deprecated("Use address.state")));
		[NullAllowed, Export ("addressState")]
		string AddressState { get; }

		// @property (readonly, nonatomic) NSString * _Nullable addressZip __attribute__((deprecated("Use address.postalCode")));
		[NullAllowed, Export ("addressZip")]
		string AddressZip { get; }

		// @property (readonly, nonatomic) NSString * _Nullable addressCountry __attribute__((deprecated("Use address.country")));
		[NullAllowed, Export ("addressCountry")]
		string AddressCountry { get; }

		// -(instancetype _Nonnull)initWithID:(NSString * _Nonnull)cardID brand:(STPCardBrand)brand last4:(NSString * _Nonnull)last4 expMonth:(NSUInteger)expMonth expYear:(NSUInteger)expYear funding:(STPCardFundingType)funding __attribute__((deprecated("You cannot directly instantiate an STPCard. You should only use one that has been returned from an STPAPIClient callback.")));
		[Export ("initWithID:brand:last4:expMonth:expYear:funding:")]
		IntPtr Constructor (string cardId, CardBrand brand, string last4, nuint expMonth, nuint expYear, CardFundingType funding);

		// +(STPCardFundingType)fundingFromString:(NSString * _Nonnull)string __attribute__((deprecated("")));
		[Static]
		[Export ("fundingFromString:")]
		CardFundingType FundingFromString (string @string);

		// Leave this in place as long as ApiResponseDecodable contains this method
		// @required +(instancetype _Nullable)decodedObjectFromAPIResponse:(NSDictionary * _Nullable)response;
		[Static/*, Abstract*/]
		[Export ("decodedObjectFromAPIResponse:")]
		[return: NullAllowed]
		Card GetDecodedObject ([NullAllowed] NSDictionary response);
	}

	// @interface STPCardValidator : NSObject
	[BaseType (typeof (NSObject), Name = "STPCardValidator")]
	interface CardValidator
	{
		// +(NSString * _Nonnull)sanitizedNumericStringForString:(NSString * _Nonnull)string;
		[Static]
		[Export ("sanitizedNumericStringForString:")]
		string GetSanitizedNumericString (string @string);

		// +(BOOL)stringIsNumeric:(NSString * _Nonnull)string;
		[Static]
		[Export ("stringIsNumeric:")]
		bool StringIsNumeric (string @string);

		// +(STPCardValidationState)validationStateForNumber:(NSString * _Nullable)cardNumber validatingCardBrand:(BOOL)validatingCardBrand;
		[Static]
		[Export ("validationStateForNumber:validatingCardBrand:")]
		CardValidationState GetValidationState ([NullAllowed] string cardNumber, bool validatingCardBrand);

		// +(STPCardBrand)brandForNumber:(NSString * _Nonnull)cardNumber;
		[Static]
		[Export ("brandForNumber:")]
		CardBrand GetBrand (string cardNumber);

		// +(NSSet<NSNumber *> * _Nonnull)lengthsForCardBrand:(STPCardBrand)brand;
		[Static]
		[Export ("lengthsForCardBrand:")]
		NSSet<NSNumber> GetCardBrandLengths (CardBrand brand);

		// +(NSInteger)maxLengthForCardBrand:(STPCardBrand)brand;
		[Static]
		[Export ("maxLengthForCardBrand:")]
		nint GetCardBrandMaxLength (CardBrand brand);

		// +(NSInteger)fragmentLengthForCardBrand:(STPCardBrand)brand;
		[Static]
		[Export ("fragmentLengthForCardBrand:")]
		nint GetCardBrandFragmentLength (CardBrand brand);

		// +(STPCardValidationState)validationStateForExpirationMonth:(NSString * _Nonnull)expirationMonth;
		[Static]
		[Export ("validationStateForExpirationMonth:")]
		CardValidationState ValidationState (string expirationMonth);

		// +(STPCardValidationState)validationStateForExpirationYear:(NSString * _Nonnull)expirationYear inMonth:(NSString * _Nonnull)expirationMonth;
		[Static]
		[Export ("validationStateForExpirationYear:inMonth:")]
		CardValidationState GetValidationState (string expirationYear, string expirationMonth);

		// +(NSUInteger)maxCVCLengthForCardBrand:(STPCardBrand)brand;
		[Static]
		[Export ("maxCVCLengthForCardBrand:")]
		nuint GetCardBrandMaxCVCLength (CardBrand brand);

		// +(STPCardValidationState)validationStateForCVC:(NSString * _Nonnull)cvc cardBrand:(STPCardBrand)brand;
		[Static]
		[Export ("validationStateForCVC:cardBrand:")]
		CardValidationState GetValidationState (string cvc, CardBrand brand);

		// +(STPCardValidationState)validationStateForCard:(STPCardParams * _Nonnull)card;
		[Static]
		[Export ("validationStateForCard:")]
		CardValidationState GetCardValidationState (CardParams card);
	}

	// @interface STPCustomerContext : NSObject <STPBackendAPIAdapter>
	[BaseType (typeof (NSObject), Name = "STPCustomerContext")]
	interface CustomerContext : BackendApiAdapter
	{
		// -(instancetype _Nonnull)initWithKeyProvider:(id<STPEphemeralKeyProvider> _Nonnull)keyProvider;
		[Export ("initWithKeyProvider:")]
		IntPtr Constructor (IEphemeralKeyProvider keyProvider);

		// -(void)clearCachedCustomer;
		[Export ("clearCachedCustomer")]
		void ClearCachedCustomer ();
	}

	interface IEphemeralKeyProvider { }

	// @protocol STPEphemeralKeyProvider <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "STPEphemeralKeyProvider")]
	interface EphemeralKeyProvider
	{
		// @required -(void)createCustomerKeyWithAPIVersion:(NSString * _Nonnull)apiVersion completion:(STPJSONResponseCompletionBlock _Nonnull)completion;
		[Abstract]
		[Export ("createCustomerKeyWithAPIVersion:completion:")]
		void CreateCustomerKey (string apiVersion, JsonResponseCompletionBlock completion);
	}

	// @interface STPImageLibrary : NSObject
	[BaseType (typeof (NSObject), Name = "STPImageLibrary")]
	interface ImageLibrary
	{
		// +(UIImage * _Nonnull)applePayCardImage;
		[Static]
		[Export ("applePayCardImage")]
		UIImage ApplePayCardImage { get; }

		// +(UIImage * _Nonnull)amexCardImage;
		[Static]
		[Export ("amexCardImage")]
		UIImage AmexCardImage { get; }

		// +(UIImage * _Nonnull)dinersClubCardImage;
		[Static]
		[Export ("dinersClubCardImage")]
		UIImage DinersClubCardImage { get; }

		// +(UIImage * _Nonnull)discoverCardImage;
		[Static]
		[Export ("discoverCardImage")]
		UIImage DiscoverCardImage { get; }

		// +(UIImage * _Nonnull)jcbCardImage;
		[Static]
		[Export ("jcbCardImage")]
		UIImage JcbCardImage { get; }

		// +(UIImage * _Nonnull)masterCardCardImage;
		[Static]
		[Export ("masterCardCardImage")]
		UIImage MasterCardCardImage { get; }

		// +(UIImage * _Nonnull)visaCardImage;
		[Static]
		[Export ("visaCardImage")]
		UIImage VisaCardImage { get; }

		// +(UIImage * _Nonnull)unknownCardCardImage;
		[Static]
		[Export ("unknownCardCardImage")]
		UIImage UnknownCardCardImage { get; }

		// +(UIImage * _Nonnull)brandImageForCardBrand:(STPCardBrand)brand;
		[Static]
		[Export ("brandImageForCardBrand:")]
		UIImage GetBrandImage (CardBrand brand);

		// +(UIImage * _Nonnull)templatedBrandImageForCardBrand:(STPCardBrand)brand;
		[Static]
		[Export ("templatedBrandImageForCardBrand:")]
		UIImage GetTemplatedBrandImage (CardBrand brand);

		// +(UIImage * _Nonnull)cvcImageForCardBrand:(STPCardBrand)brand;
		[Static]
		[Export ("cvcImageForCardBrand:")]
		UIImage GetCvcImage (CardBrand brand);

		// +(UIImage * _Nonnull)errorImageForCardBrand:(STPCardBrand)brand;
		[Static]
		[Export ("errorImageForCardBrand:")]
		UIImage GetErrorImage (CardBrand brand);
	}

	// @interface STPPaymentActivityIndicatorView : UIView
	[BaseType (typeof (UIView), Name = "STPPaymentActivityIndicatorView")]
	interface PaymentActivityIndicatorView
	{
		// -(void)setAnimating:(BOOL)animating animated:(BOOL)animated;
		[Export ("setAnimating:animated:")]
		void SetAnimating (bool animating, bool animated);

		// @property (nonatomic) BOOL animating;
		[Export ("animating")]
		bool Animating { get; set; }

		// @property (nonatomic) BOOL hidesWhenStopped;
		[Export ("hidesWhenStopped")]
		bool HidesWhenStopped { get; set; }
	}

	// @interface STPPaymentCardTextField : UIControl <UIKeyInput>
	[BaseType (typeof (UIControl), Name = "STPPaymentCardTextField")]
	interface PaymentCardTextField : IUIKeyInput
	{
		// @property (nonatomic, weak) id<STPPaymentCardTextFieldDelegate> _Nullable delegate __attribute__((iboutlet));
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPaymentCardTextFieldDelegate Delegate { get; set; }

		// @property (copy, nonatomic) UIFont * _Null_unspecified font __attribute__((annotate("ui_appearance_selector")));
		[Export ("font", ArgumentSemantic.Copy)]
		UIFont Font { get; set; }

		// @property (copy, nonatomic) UIColor * _Null_unspecified textColor __attribute__((annotate("ui_appearance_selector")));
		[Export ("textColor", ArgumentSemantic.Copy)]
		UIColor TextColor { get; set; }

		// @property (copy, nonatomic) UIColor * _Null_unspecified textErrorColor __attribute__((annotate("ui_appearance_selector")));
		[Export ("textErrorColor", ArgumentSemantic.Copy)]
		UIColor TextErrorColor { get; set; }

		// @property (copy, nonatomic) UIColor * _Null_unspecified placeholderColor __attribute__((annotate("ui_appearance_selector")));
		[Export ("placeholderColor", ArgumentSemantic.Copy)]
		UIColor PlaceholderColor { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable numberPlaceholder;
		[NullAllowed, Export ("numberPlaceholder")]
		string NumberPlaceholder { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable expirationPlaceholder;
		[NullAllowed, Export ("expirationPlaceholder")]
		string ExpirationPlaceholder { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable cvcPlaceholder;
		[NullAllowed, Export ("cvcPlaceholder")]
		string CvcPlaceholder { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable postalCodePlaceholder;
		[NullAllowed, Export ("postalCodePlaceholder")]
		string PostalCodePlaceholder { get; set; }

		// @property (copy, nonatomic) UIColor * _Null_unspecified cursorColor __attribute__((annotate("ui_appearance_selector")));
		[Export ("cursorColor", ArgumentSemantic.Copy)]
		UIColor CursorColor { get; set; }

		// @property (copy, nonatomic) UIColor * _Nullable borderColor __attribute__((annotate("ui_appearance_selector")));
		[NullAllowed, Export ("borderColor", ArgumentSemantic.Copy)]
		UIColor BorderColor { get; set; }

		// @property (assign, nonatomic) CGFloat borderWidth __attribute__((annotate("ui_appearance_selector")));
		[Export ("borderWidth")]
		nfloat BorderWidth { get; set; }

		// @property (assign, nonatomic) CGFloat cornerRadius __attribute__((annotate("ui_appearance_selector")));
		[Export ("cornerRadius")]
		nfloat CornerRadius { get; set; }

		// @property (assign, nonatomic) UIKeyboardAppearance keyboardAppearance __attribute__((annotate("ui_appearance_selector")));
		[Export ("keyboardAppearance", ArgumentSemantic.Assign)]
		UIKeyboardAppearance KeyboardAppearance { get; set; }

		// @property (nonatomic, strong) UIView * _Nullable inputView;
		[NullAllowed, Export ("inputView", ArgumentSemantic.Strong)]
		UIView InputView { get; set; }

		// @property (nonatomic, strong) UIView * _Nullable inputAccessoryView;
		[NullAllowed, Export ("inputAccessoryView", ArgumentSemantic.Strong)]
		UIView InputAccessoryView { get; set; }

		// @property (readonly, nonatomic) UIImage * _Nullable brandImage;
		[NullAllowed, Export ("brandImage")]
		UIImage BrandImage { get; }

		// @property (readonly, nonatomic) BOOL isValid;
		[Export ("isValid")]
		bool IsValid { get; }

		// @property (getter = isEnabled, nonatomic) BOOL enabled;
		[Export ("enabled")]
		bool Enabled { [Bind ("isEnabled")] get; set; }

		// @property (readonly, nonatomic) NSString * _Nullable cardNumber;
		[NullAllowed, Export ("cardNumber")]
		string CardNumber { get; }

		// @property (readonly, nonatomic) NSUInteger expirationMonth;
		[Export ("expirationMonth")]
		nuint ExpirationMonth { get; }

		// @property (readonly, nonatomic) NSString * _Nullable formattedExpirationMonth;
		[NullAllowed, Export ("formattedExpirationMonth")]
		string FormattedExpirationMonth { get; }

		// @property (readonly, nonatomic) NSUInteger expirationYear;
		[Export ("expirationYear")]
		nuint ExpirationYear { get; }

		// @property (readonly, nonatomic) NSString * _Nullable formattedExpirationYear;
		[NullAllowed, Export ("formattedExpirationYear")]
		string FormattedExpirationYear { get; }

		// @property (readonly, nonatomic) NSString * _Nullable cvc;
		[NullAllowed, Export ("cvc")]
		string Cvc { get; }

		// @property (readonly, nonatomic) NSString * _Nullable postalCode;
		[NullAllowed, Export ("postalCode")]
		string PostalCode { get; }

		// @property (assign, readwrite, nonatomic) BOOL postalCodeEntryEnabled;
		[Export ("postalCodeEntryEnabled")]
		bool PostalCodeEntryEnabled { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable countryCode;
		[NullAllowed, Export ("countryCode")]
		string CountryCode { get; set; }

		// @property (readwrite, nonatomic, strong) STPCardParams * _Nonnull cardParams;
		[Export ("cardParams", ArgumentSemantic.Strong)]
		CardParams CardParams { get; set; }

		// -(BOOL)becomeFirstResponder;
		[Export ("becomeFirstResponder")]
		bool BecomeFirstResponder ();

		// -(BOOL)resignFirstResponder;
		[Export ("resignFirstResponder")]
		bool ResignFirstResponder ();

		// -(void)clear;
		[Export ("clear")]
		void Clear ();

		// +(UIImage * _Nullable)cvcImageForCardBrand:(STPCardBrand)cardBrand;
		[Static]
		[Export ("cvcImageForCardBrand:")]
		[return: NullAllowed]
		UIImage GetCardBrandCvcImage (CardBrand cardBrand);

		// +(UIImage * _Nullable)brandImageForCardBrand:(STPCardBrand)cardBrand;
		[Static]
		[Export ("brandImageForCardBrand:")]
		[return: NullAllowed]
		UIImage GetCardBrandBrandImage (CardBrand cardBrand);

		// +(UIImage * _Nullable)errorImageForCardBrand:(STPCardBrand)cardBrand;
		[Static]
		[Export ("errorImageForCardBrand:")]
		[return: NullAllowed]
		UIImage GetCardBrandErrorImage (CardBrand cardBrand);

		// -(CGRect)brandImageRectForBounds:(CGRect)bounds;
		[Export ("brandImageRectForBounds:")]
		CGRect GetBrandImageRect (CGRect bounds);

		// -(CGRect)fieldsRectForBounds:(CGRect)bounds;
		[Export ("fieldsRectForBounds:")]
		CGRect GetFieldsRect (CGRect bounds);
	}

	interface IPaymentCardTextFieldDelegate { }

	// @protocol STPPaymentCardTextFieldDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "STPPaymentCardTextFieldDelegate")]
	interface PaymentCardTextFieldDelegate
	{
		// @optional -(void)paymentCardTextFieldDidChange:(STPPaymentCardTextField * _Nonnull)textField;
		[Export ("paymentCardTextFieldDidChange:")]
		void PaymentCardTextFieldChanged (PaymentCardTextField textField);

		// @optional -(void)paymentCardTextFieldDidBeginEditing:(STPPaymentCardTextField * _Nonnull)textField;
		[Export ("paymentCardTextFieldDidBeginEditing:")]
		void PaymentCardTextFieldDidBeginEditing (PaymentCardTextField textField);

		// @optional -(void)paymentCardTextFieldDidEndEditing:(STPPaymentCardTextField * _Nonnull)textField;
		[Export ("paymentCardTextFieldDidEndEditing:")]
		void PaymentCardTextFieldDidEndEditing (PaymentCardTextField textField);

		// @optional -(void)paymentCardTextFieldDidBeginEditingNumber:(STPPaymentCardTextField * _Nonnull)textField;
		[Export ("paymentCardTextFieldDidBeginEditingNumber:")]
		void PaymentCardTextFieldDidBeginEditingNumber (PaymentCardTextField textField);

		// @optional -(void)paymentCardTextFieldDidEndEditingNumber:(STPPaymentCardTextField * _Nonnull)textField;
		[Export ("paymentCardTextFieldDidEndEditingNumber:")]
		void PaymentCardTextFieldDidEndEditingNumber (PaymentCardTextField textField);

		// @optional -(void)paymentCardTextFieldDidBeginEditingCVC:(STPPaymentCardTextField * _Nonnull)textField;
		[Export ("paymentCardTextFieldDidBeginEditingCVC:")]
		void PaymentCardTextFieldDidBeginEditingCVC (PaymentCardTextField textField);

		// @optional -(void)paymentCardTextFieldDidEndEditingCVC:(STPPaymentCardTextField * _Nonnull)textField;
		[Export ("paymentCardTextFieldDidEndEditingCVC:")]
		void PaymentCardTextFieldDidEndEditingCVC (PaymentCardTextField textField);

		// @optional -(void)paymentCardTextFieldDidBeginEditingExpiration:(STPPaymentCardTextField * _Nonnull)textField;
		[Export ("paymentCardTextFieldDidBeginEditingExpiration:")]
		void PaymentCardTextFieldDidBeginEditingExpiration (PaymentCardTextField textField);

		// @optional -(void)paymentCardTextFieldDidEndEditingExpiration:(STPPaymentCardTextField * _Nonnull)textField;
		[Export ("paymentCardTextFieldDidEndEditingExpiration:")]
		void PaymentCardTextFieldDidEndEditingExpiration (PaymentCardTextField textField);

		// @optional -(void)paymentCardTextFieldDidBeginEditingPostalCode:(STPPaymentCardTextField * _Nonnull)textField;
		[Export ("paymentCardTextFieldDidBeginEditingPostalCode:")]
		void PaymentCardTextFieldDidBeginEditingPostalCode (PaymentCardTextField textField);

		// @optional -(void)paymentCardTextFieldDidEndEditingPostalCode:(STPPaymentCardTextField * _Nonnull)textField;
		[Export ("paymentCardTextFieldDidEndEditingPostalCode:")]
		void PaymentCardTextFieldDidEndEditingPostalCode (PaymentCardTextField textField);
	}

	// @interface STPPaymentResult : NSObject
	[BaseType (typeof (NSObject), Name = "STPPaymentResult")]
	interface PaymentResult
	{
		// @property (readonly, nonatomic) id<STPSourceProtocol> _Nonnull source;
		[Export ("source")]
		ISourceProtocol Source { get; }

		// -(instancetype _Nonnull)initWithSource:(id<STPSourceProtocol> _Nonnull)source;
		[Export ("initWithSource:")]
		IntPtr Constructor (ISourceProtocol source);
	}

	// @interface STPPaymentContext : NSObject
	[BaseType (typeof (NSObject), Name = "STPPaymentContext")]
	interface PaymentContext
	{
		// -(instancetype _Nonnull)initWithCustomerContext:(STPCustomerContext * _Nonnull)customerContext;
		[Export ("initWithCustomerContext:")]
		IntPtr Constructor (CustomerContext customerContext);

		// -(instancetype _Nonnull)initWithCustomerContext:(STPCustomerContext * _Nonnull)customerContext configuration:(STPPaymentConfiguration * _Nonnull)configuration theme:(STPTheme * _Nonnull)theme;
		[Export ("initWithCustomerContext:configuration:theme:")]
		IntPtr Constructor (CustomerContext customerContext, PaymentConfiguration configuration, Theme theme);

		// -(instancetype _Nonnull)initWithAPIAdapter:(id<STPBackendAPIAdapter> _Nonnull)apiAdapter;
		[Export ("initWithAPIAdapter:")]
		IntPtr Constructor (IBackendApiAdapter apiAdapter);

		// -(instancetype _Nonnull)initWithAPIAdapter:(id<STPBackendAPIAdapter> _Nonnull)apiAdapter configuration:(STPPaymentConfiguration * _Nonnull)configuration theme:(STPTheme * _Nonnull)theme;
		[Export ("initWithAPIAdapter:configuration:theme:")]
		IntPtr Constructor (IBackendApiAdapter apiAdapter, PaymentConfiguration configuration, Theme theme);

		// @property (readonly, nonatomic) id<STPBackendAPIAdapter> _Nonnull apiAdapter;
		[Export ("apiAdapter")]
		IBackendApiAdapter ApiAdapter { get; }

		// @property (readonly, nonatomic) STPPaymentConfiguration * _Nonnull configuration;
		[Export ("configuration")]
		PaymentConfiguration Configuration { get; }

		// @property (readonly, nonatomic) STPTheme * _Nonnull theme;
		[Export ("theme")]
		Theme Theme { get; }

		// @property (nonatomic, strong) STPUserInformation * _Nullable prefilledInformation;
		[NullAllowed, Export ("prefilledInformation", ArgumentSemantic.Strong)]
		UserInformation PrefilledInformation { get; set; }

		// @property (nonatomic, weak) UIViewController * _Nullable hostViewController;
		[NullAllowed, Export ("hostViewController", ArgumentSemantic.Weak)]
		UIViewController HostViewController { get; set; }

		// @property (nonatomic, weak) id<STPPaymentContextDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPaymentContextDelegate Delegate { get; set; }

		// @property (readonly, nonatomic) BOOL loading;
		[Export ("loading")]
		bool Loading { get; }

		// @property (readonly, nonatomic) id<STPPaymentMethod> _Nullable selectedPaymentMethod;
		[NullAllowed, Export ("selectedPaymentMethod")]
		IPaymentMethod SelectedPaymentMethod { get; }

		// @property (readonly, nonatomic) NSArray<id<STPPaymentMethod>> * _Nullable paymentMethods;
		[NullAllowed, Export ("paymentMethods")]
		IPaymentMethod[] PaymentMethods { get; }

		// @property (readonly, nonatomic) PKShippingMethod * _Nullable selectedShippingMethod;
		[NullAllowed, Export ("selectedShippingMethod")]
		PKShippingMethod SelectedShippingMethod { get; }

		// @property (readonly, nonatomic) NSArray<PKShippingMethod *> * _Nullable shippingMethods;
		[NullAllowed, Export ("shippingMethods")]
		PKShippingMethod[] ShippingMethods { get; }

		// @property (readonly, nonatomic) STPAddress * _Nullable shippingAddress;
		[NullAllowed, Export ("shippingAddress")]
		Address ShippingAddress { get; }

		// @property (nonatomic) NSInteger paymentAmount;
		[Export ("paymentAmount")]
		nint PaymentAmount { get; set; }

		// @property (copy, nonatomic) NSString * _Nonnull paymentCurrency;
		[Export ("paymentCurrency")]
		string PaymentCurrency { get; set; }

		// @property (copy, nonatomic) NSString * _Nonnull paymentCountry;
		[Export ("paymentCountry")]
		string PaymentCountry { get; set; }

		// @property (copy, nonatomic) NSArray<PKPaymentSummaryItem *> * _Nonnull paymentSummaryItems;
		[Export ("paymentSummaryItems", ArgumentSemantic.Copy)]
		PKPaymentSummaryItem[] PaymentSummaryItems { get; set; }

		// @property (assign, nonatomic) UIModalPresentationStyle modalPresentationStyle;
		[Export ("modalPresentationStyle", ArgumentSemantic.Assign)]
		UIModalPresentationStyle ModalPresentationStyle { get; set; }

		// @property (nonatomic, strong) UIView * _Nonnull paymentMethodsViewControllerFooterView;
		[Export ("paymentMethodsViewControllerFooterView", ArgumentSemantic.Strong)]
		UIView PaymentMethodsViewControllerFooterView { get; set; }

		// @property (nonatomic, strong) UIView * _Nonnull addCardViewControllerFooterView;
		[Export ("addCardViewControllerFooterView", ArgumentSemantic.Strong)]
		UIView AddCardViewControllerFooterView { get; set; }

		// -(void)retryLoading;
		[Export ("retryLoading")]
		void RetryLoading ();

		// -(void)presentPaymentMethodsViewController;
		[Export ("presentPaymentMethodsViewController")]
		void PresentPaymentMethodsViewController ();

		// -(void)pushPaymentMethodsViewController;
		[Export ("pushPaymentMethodsViewController")]
		void PushPaymentMethodsViewController ();

		// -(void)presentShippingViewController;
		[Export ("presentShippingViewController")]
		void PresentShippingViewController ();

		// -(void)pushShippingViewController;
		[Export ("pushShippingViewController")]
		void PushShippingViewController ();

		// -(void)requestPayment;
		[Export ("requestPayment")]
		void RequestPayment ();
	}

	interface IPaymentContextDelegate { }

	// @protocol STPPaymentContextDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "STPPaymentContextDelegate")]
	interface PaymentContextDelegate
	{
		// @required -(void)paymentContext:(STPPaymentContext * _Nonnull)paymentContext didFailToLoadWithError:(NSError * _Nonnull)error;
		[Abstract]
		[Export ("paymentContext:didFailToLoadWithError:")]
		void PaymentContext (PaymentContext paymentContext, NSError error);

		// @required -(void)paymentContextDidChange:(STPPaymentContext * _Nonnull)paymentContext;
		[Abstract]
		[Export ("paymentContextDidChange:")]
		void PaymentContextDidChange (PaymentContext paymentContext);

		// @required -(void)paymentContext:(STPPaymentContext * _Nonnull)paymentContext didCreatePaymentResult:(STPPaymentResult * _Nonnull)paymentResult completion:(STPErrorBlock _Nonnull)completion;
		[Abstract]
		[Export ("paymentContext:didCreatePaymentResult:completion:")]
		void PaymentContext (PaymentContext paymentContext, PaymentResult paymentResult, STPErrorBlock completion);

		// @required -(void)paymentContext:(STPPaymentContext * _Nonnull)paymentContext didFinishWithStatus:(STPPaymentStatus)status error:(NSError * _Nullable)error;
		[Abstract]
		[Export ("paymentContext:didFinishWithStatus:error:")]
		void PaymentContext (PaymentContext paymentContext, PaymentStatus status, [NullAllowed] NSError error);

		// @optional -(void)paymentContext:(STPPaymentContext * _Nonnull)paymentContext didUpdateShippingAddress:(STPAddress * _Nonnull)address completion:(STPShippingMethodsCompletionBlock _Nonnull)completion;
		[Export ("paymentContext:didUpdateShippingAddress:completion:")]
		void PaymentContext (PaymentContext paymentContext, Address address, ShippingMethodsCompletionBlock completion);
	}

	// @interface STPPaymentMethodsViewController : STPCoreViewController
	[BaseType (typeof (CoreViewController), Name = "STPPaymentMethodsViewController")]
	interface PaymentMethodsViewController
	{
		// @property (readonly, nonatomic, weak) id<STPPaymentMethodsViewControllerDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPaymentMethodsViewControllerDelegate Delegate { get; }

		// -(instancetype _Nonnull)initWithPaymentContext:(STPPaymentContext * _Nonnull)paymentContext;
		[Export ("initWithPaymentContext:")]
		IntPtr Constructor (PaymentContext paymentContext);

		// -(instancetype _Nonnull)initWithConfiguration:(STPPaymentConfiguration * _Nonnull)configuration theme:(STPTheme * _Nonnull)theme customerContext:(STPCustomerContext * _Nonnull)customerContext delegate:(id<STPPaymentMethodsViewControllerDelegate> _Nonnull)delegate;
		[Export ("initWithConfiguration:theme:customerContext:delegate:")]
		IntPtr Constructor (PaymentConfiguration configuration, Theme theme, CustomerContext customerContext, IPaymentMethodsViewControllerDelegate @delegate);

		// -(instancetype _Nonnull)initWithConfiguration:(STPPaymentConfiguration * _Nonnull)configuration theme:(STPTheme * _Nonnull)theme apiAdapter:(id<STPBackendAPIAdapter> _Nonnull)apiAdapter delegate:(id<STPPaymentMethodsViewControllerDelegate> _Nonnull)delegate;
		[Export ("initWithConfiguration:theme:apiAdapter:delegate:")]
		IntPtr Constructor (PaymentConfiguration configuration, Theme theme, IBackendApiAdapter apiAdapter, IPaymentMethodsViewControllerDelegate @delegate);

		// @property (nonatomic, strong) STPUserInformation * _Nullable prefilledInformation;
		[NullAllowed, Export ("prefilledInformation", ArgumentSemantic.Strong)]
		UserInformation PrefilledInformation { get; set; }

		// @property (nonatomic, strong) UIView * _Nonnull paymentMethodsViewControllerFooterView;
		[Export ("paymentMethodsViewControllerFooterView", ArgumentSemantic.Strong)]
		UIView PaymentMethodsViewControllerFooterView { get; set; }

		// @property (nonatomic, strong) UIView * _Nonnull addCardViewControllerFooterView;
		[Export ("addCardViewControllerFooterView", ArgumentSemantic.Strong)]
		UIView AddCardViewControllerFooterView { get; set; }

		// -(void)dismissWithCompletion:(STPVoidBlock _Nullable)completion;
		[Export ("dismissWithCompletion:")]
		void Dismiss ([NullAllowed] STPVoidBlock completion);
	}

	interface IPaymentMethodsViewControllerDelegate { }

	// @protocol STPPaymentMethodsViewControllerDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "STPPaymentMethodsViewControllerDelegate")]
	interface PaymentMethodsViewControllerDelegate
	{
		// @required -(void)paymentMethodsViewController:(STPPaymentMethodsViewController * _Nonnull)paymentMethodsViewController didFailToLoadWithError:(NSError * _Nonnull)error;
		[Abstract]
		[Export ("paymentMethodsViewController:didFailToLoadWithError:")]
		void PaymentMethodsViewControllerFailedToLoad (PaymentMethodsViewController paymentMethodsViewController, NSError error);

		// @required -(void)paymentMethodsViewControllerDidFinish:(STPPaymentMethodsViewController * _Nonnull)paymentMethodsViewController;
		[Abstract]
		[Export ("paymentMethodsViewControllerDidFinish:")]
		void PaymentMethodsViewControllerFinished (PaymentMethodsViewController paymentMethodsViewController);

		// @required -(void)paymentMethodsViewControllerDidCancel:(STPPaymentMethodsViewController * _Nonnull)paymentMethodsViewController;
		[Abstract]
		[Export ("paymentMethodsViewControllerDidCancel:")]
		void PaymentMethodsViewControllerCancelled (PaymentMethodsViewController paymentMethodsViewController);

		// @optional -(void)paymentMethodsViewController:(STPPaymentMethodsViewController * _Nonnull)paymentMethodsViewController didSelectPaymentMethod:(id<STPPaymentMethod> _Nonnull)paymentMethod;
		[Export ("paymentMethodsViewController:didSelectPaymentMethod:")]
		void PaymentMethodsViewControllerSelectedPaymentMethod (PaymentMethodsViewController paymentMethodsViewController, IPaymentMethod paymentMethod);
	}

	// typedef void (^STPRedirectContextCompletionBlock)(NSString * _Nonnull, NSString * _Nonnull, NSError * _Nonnull);
	delegate void RedirectContextCompletionBlock (string arg0, string arg1, NSError arg2);

	// @interface STPRedirectContext : NSObject
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "STPRedirectContext")]
	interface RedirectContext
	{
		// @property (readonly, nonatomic) STPRedirectContextState state;
		[Export ("state")]
		RedirectContextState State { get; }

		// -(instancetype _Nullable)initWithSource:(STPSource * _Nonnull)source completion:(STPRedirectContextCompletionBlock _Nonnull)completion;
		[Export ("initWithSource:completion:")]
		IntPtr Constructor (Source source, RedirectContextCompletionBlock completion);

		// -(void)startRedirectFlowFromViewController:(UIViewController * _Nonnull)presentingViewController;
		[Export ("startRedirectFlowFromViewController:")]
		void StartRedirectFlow (UIViewController presentingViewController);

		// -(void)startSafariViewControllerRedirectFlowFromViewController:(UIViewController * _Nonnull)presentingViewController __attribute__((availability(ios, introduced=9_0)));
		[Introduced (PlatformName.iOS, 9, 0)]
		[Export ("startSafariViewControllerRedirectFlowFromViewController:")]
		void StartSafariViewControllerRedirectFlow (UIViewController presentingViewController);

		// -(void)startSafariAppRedirectFlow;
		[Export ("startSafariAppRedirectFlow")]
		void StartSafariAppRedirectFlow ();

		// -(void)cancel;
		[Export ("cancel")]
		void Cancel ();
	}

	// @interface STPShippingAddressViewController : STPCoreTableViewController
	[BaseType (typeof (CoreTableViewController), Name = "STPShippingAddressViewController")]
	interface ShippingAddressViewController
	{
		// -(instancetype _Nonnull)initWithPaymentContext:(STPPaymentContext * _Nonnull)paymentContext;
		[Export ("initWithPaymentContext:")]
		IntPtr Constructor (PaymentContext paymentContext);

		// -(instancetype _Nonnull)initWithConfiguration:(STPPaymentConfiguration * _Nonnull)configuration theme:(STPTheme * _Nonnull)theme currency:(NSString * _Nullable)currency shippingAddress:(STPAddress * _Nullable)shippingAddress selectedShippingMethod:(PKShippingMethod * _Nullable)selectedShippingMethod prefilledInformation:(STPUserInformation * _Nullable)prefilledInformation;
		[Export ("initWithConfiguration:theme:currency:shippingAddress:selectedShippingMethod:prefilledInformation:")]
		IntPtr Constructor (PaymentConfiguration configuration, Theme theme, [NullAllowed] string currency, [NullAllowed] Address shippingAddress, [NullAllowed] PKShippingMethod selectedShippingMethod, [NullAllowed] UserInformation prefilledInformation);

		// @property (nonatomic, weak) id<STPShippingAddressViewControllerDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IShippingAddressViewControllerDelegate Delegate { get; set; }

		// -(void)dismissWithCompletion:(STPVoidBlock _Nullable)completion;
		[Export ("dismissWithCompletion:")]
		void Dismiss ([NullAllowed] STPVoidBlock completion);
	}

	interface IShippingAddressViewControllerDelegate { }

	// @protocol STPShippingAddressViewControllerDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "STPShippingAddressViewControllerDelegate")]
	interface ShippingAddressViewControllerDelegate
	{
		// @required -(void)shippingAddressViewControllerDidCancel:(STPShippingAddressViewController * _Nonnull)addressViewController;
		[Abstract]
		[Export ("shippingAddressViewControllerDidCancel:")]
		void ShippingAddressViewControllerCancelled (ShippingAddressViewController addressViewController);

		// @required -(void)shippingAddressViewController:(STPShippingAddressViewController * _Nonnull)addressViewController didEnterAddress:(STPAddress * _Nonnull)address completion:(STPShippingMethodsCompletionBlock _Nonnull)completion;
		[Abstract]
		[Export ("shippingAddressViewController:didEnterAddress:completion:")]
		void ShippingAddressViewControllerEnteredAddress (ShippingAddressViewController addressViewController, Address address, ShippingMethodsCompletionBlock completion);

		// @required -(void)shippingAddressViewController:(STPShippingAddressViewController * _Nonnull)addressViewController didFinishWithAddress:(STPAddress * _Nonnull)address shippingMethod:(PKShippingMethod * _Nullable)method;
		[Abstract]
		[Export ("shippingAddressViewController:didFinishWithAddress:shippingMethod:")]
		void ShippingAddressViewControllerFinished (ShippingAddressViewController addressViewController, Address address, [NullAllowed] PKShippingMethod method);
	}

	// @interface STPSourceCardDetails : NSObject <STPAPIResponseDecodable>
	[BaseType (typeof (NSObject), Name = "STPSourceCardDetails")]
	[DisableDefaultCtor]
	interface SourceCardDetails : ApiResponseDecodable
	{
		// @property (readonly, nonatomic) NSString * _Nullable last4;
		[NullAllowed, Export ("last4")]
		string Last4 { get; }

		// @property (readonly, nonatomic) NSUInteger expMonth;
		[Export ("expMonth")]
		nuint ExpMonth { get; }

		// @property (readonly, nonatomic) NSUInteger expYear;
		[Export ("expYear")]
		nuint ExpYear { get; }

		// @property (readonly, nonatomic) STPCardBrand brand;
		[Export ("brand")]
		CardBrand Brand { get; }

		// @property (readonly, nonatomic) STPCardFundingType funding;
		[Export ("funding")]
		CardFundingType Funding { get; }

		// @property (readonly, nonatomic) NSString * _Nullable country;
		[NullAllowed, Export ("country")]
		string Country { get; }

		// @property (readonly, nonatomic) STPSourceCard3DSecureStatus threeDSecure;
		[Export ("threeDSecure")]
		SourceCard3DSecureStatus ThreeDSecure { get; }

		// @property (readonly, nonatomic) BOOL isApplePayCard;
		[Export ("isApplePayCard")]
		bool IsApplePayCard { get; }

		// Leave this in place as long as ApiResponseDecodable contains this method
		// @required +(instancetype _Nullable)decodedObjectFromAPIResponse:(NSDictionary * _Nullable)response;
		[Static/*, Abstract*/]
		[Export ("decodedObjectFromAPIResponse:")]
		[return: NullAllowed]
		SourceCardDetails GetDecodedObject ([NullAllowed] NSDictionary response);
	}

	// @interface STPSourceOwner : NSObject <STPAPIResponseDecodable>
	[BaseType (typeof (NSObject), Name = "STPSourceOwner")]
	[DisableDefaultCtor]
	interface SourceOwner : ApiResponseDecodable
	{
		// @property (readonly, nonatomic) STPAddress * _Nullable address;
		[NullAllowed, Export ("address")]
		Address Address { get; }

		// @property (readonly, nonatomic) NSString * _Nullable email;
		[NullAllowed, Export ("email")]
		string Email { get; }

		// @property (readonly, nonatomic) NSString * _Nullable name;
		[NullAllowed, Export ("name")]
		string Name { get; }

		// @property (readonly, nonatomic) NSString * _Nullable phone;
		[NullAllowed, Export ("phone")]
		string Phone { get; }

		// @property (readonly, nonatomic) STPAddress * _Nullable verifiedAddress;
		[NullAllowed, Export ("verifiedAddress")]
		Address VerifiedAddress { get; }

		// @property (readonly, nonatomic) NSString * _Nullable verifiedEmail;
		[NullAllowed, Export ("verifiedEmail")]
		string VerifiedEmail { get; }

		// @property (readonly, nonatomic) NSString * _Nullable verifiedName;
		[NullAllowed, Export ("verifiedName")]
		string VerifiedName { get; }

		// @property (readonly, nonatomic) NSString * _Nullable verifiedPhone;
		[NullAllowed, Export ("verifiedPhone")]
		string VerifiedPhone { get; }

		// Leave this in place as long as ApiResponseDecodable contains this method
		// @required +(instancetype _Nullable)decodedObjectFromAPIResponse:(NSDictionary * _Nullable)response;
		[Static/*, Abstract*/]
		[Export ("decodedObjectFromAPIResponse:")]
		[return: NullAllowed]
		SourceOwner GetDecodedObject ([NullAllowed] NSDictionary response);
	}

	// @interface STPSourceReceiver : NSObject <STPAPIResponseDecodable>
	[BaseType (typeof (NSObject), Name = "STPSourceReceiver")]
	[DisableDefaultCtor]
	interface SourceReceiver : ApiResponseDecodable
	{
		// @property (readonly, nonatomic) NSString * _Nullable address;
		[NullAllowed, Export ("address")]
		string Address { get; }

		// @property (readonly, nonatomic) NSNumber * _Nullable amountCharged;
		[NullAllowed, Export ("amountCharged")]
		NSNumber AmountCharged { get; }

		// @property (readonly, nonatomic) NSNumber * _Nullable amountReceived;
		[NullAllowed, Export ("amountReceived")]
		NSNumber AmountReceived { get; }

		// @property (readonly, nonatomic) NSNumber * _Nullable amountReturned;
		[NullAllowed, Export ("amountReturned")]
		NSNumber AmountReturned { get; }

		// Leave this in place as long as ApiResponseDecodable contains this method
		// @required +(instancetype _Nullable)decodedObjectFromAPIResponse:(NSDictionary * _Nullable)response;
		[Static/*, Abstract*/]
		[Export ("decodedObjectFromAPIResponse:")]
		[return: NullAllowed]
		SourceReceiver GetDecodedObject ([NullAllowed] NSDictionary response);
	}

	// @interface STPSourceRedirect : NSObject <STPAPIResponseDecodable>
	[BaseType (typeof (NSObject), Name = "STPSourceRedirect")]
	[DisableDefaultCtor]
	interface SourceRedirect : ApiResponseDecodable
	{
		// @property (readonly, nonatomic) NSURL * _Nullable returnURL;
		[NullAllowed, Export ("returnURL")]
		NSUrl ReturnUrl { get; }

		// @property (readonly, nonatomic) STPSourceRedirectStatus status;
		[Export ("status")]
		SourceRedirectStatus Status { get; }

		// @property (readonly, nonatomic) NSURL * _Nullable url;
		[NullAllowed, Export ("url")]
		NSUrl Url { get; }

		// Leave this in place as long as ApiResponseDecodable contains this method
		// @required +(instancetype _Nullable)decodedObjectFromAPIResponse:(NSDictionary * _Nullable)response;
		[Static/*, Abstract*/]
		[Export ("decodedObjectFromAPIResponse:")]
		[return: NullAllowed]
		SourceRedirect GetDecodedObject ([NullAllowed] NSDictionary response);
	}

	// @interface STPSourceSEPADebitDetails : NSObject <STPAPIResponseDecodable>
	[BaseType (typeof (NSObject), Name = "STPSourceSEPADebitDetails")]
	[DisableDefaultCtor]
	interface SourceSEPADebitDetails : ApiResponseDecodable
	{
		// @property (readonly, nonatomic) NSString * _Nullable last4;
		[NullAllowed, Export ("last4")]
		string Last4 { get; }

		// @property (readonly, nonatomic) NSString * _Nullable bankCode;
		[NullAllowed, Export ("bankCode")]
		string BankCode { get; }

		// @property (readonly, nonatomic) NSString * _Nullable country;
		[NullAllowed, Export ("country")]
		string Country { get; }

		// @property (readonly, nonatomic) NSString * _Nullable fingerprint;
		[NullAllowed, Export ("fingerprint")]
		string Fingerprint { get; }

		// @property (readonly, nonatomic) NSString * _Nullable mandateReference;
		[NullAllowed, Export ("mandateReference")]
		string MandateReference { get; }

		// @property (readonly, nonatomic) NSURL * _Nullable mandateURL;
		[NullAllowed, Export ("mandateURL")]
		NSUrl MandateUrl { get; }

		// Leave this in place as long as ApiResponseDecodable contains this method
		// @required +(instancetype _Nullable)decodedObjectFromAPIResponse:(NSDictionary * _Nullable)response;
		[Static/*, Abstract*/]
		[Export ("decodedObjectFromAPIResponse:")]
		[return: NullAllowed]
		SourceSEPADebitDetails GetDecodedObject ([NullAllowed] NSDictionary response);
	}

	// @interface STPSourceVerification : NSObject <STPAPIResponseDecodable>
	[BaseType (typeof (NSObject), Name = "STPSourceVerification")]
	[DisableDefaultCtor]
	interface SourceVerification : ApiResponseDecodable
	{
		// @property (readonly, nonatomic) NSNumber * _Nullable attemptsRemaining;
		[NullAllowed, Export ("attemptsRemaining")]
		NSNumber AttemptsRemaining { get; }

		// @property (readonly, nonatomic) STPSourceVerificationStatus status;
		[Export ("status")]
		SourceVerificationStatus Status { get; }

		// Leave this in place as long as ApiResponseDecodable contains this method
		// @required +(instancetype _Nullable)decodedObjectFromAPIResponse:(NSDictionary * _Nullable)response;
		[Static/*, Abstract*/]
		[Export ("decodedObjectFromAPIResponse:")]
		[return: NullAllowed]
		SourceVerification GetDecodedObject ([NullAllowed] NSDictionary response);
	}

	// @interface STPSource : NSObject <STPAPIResponseDecodable, STPSourceProtocol>
	[BaseType (typeof (NSObject), Name = "STPSource")]
	[DisableDefaultCtor]
	interface Source : ApiResponseDecodable, SourceProtocol
	{
		// @property (readonly, nonatomic) NSNumber * _Nullable amount;
		[NullAllowed, Export ("amount")]
		NSNumber Amount { get; }

		// @property (readonly, nonatomic) NSString * _Nullable clientSecret;
		[NullAllowed, Export ("clientSecret")]
		string ClientSecret { get; }

		// @property (readonly, nonatomic) NSDate * _Nullable created;
		[NullAllowed, Export ("created")]
		NSDate Created { get; }

		// @property (readonly, nonatomic) NSString * _Nullable currency;
		[NullAllowed, Export ("currency")]
		string Currency { get; }

		// @property (readonly, nonatomic) STPSourceFlow flow;
		[Export ("flow")]
		SourceFlow Flow { get; }

		// @property (readonly, nonatomic) BOOL livemode;
		[Export ("livemode")]
		bool Livemode { get; }

		// @property (readonly, copy, nonatomic) NSDictionary<NSString *,NSString *> * _Nullable metadata;
		[NullAllowed, Export ("metadata", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSString> Metadata { get; }

		// @property (readonly, nonatomic) STPSourceOwner * _Nullable owner;
		[NullAllowed, Export ("owner")]
		SourceOwner Owner { get; }

		// @property (readonly, nonatomic) STPSourceReceiver * _Nullable receiver;
		[NullAllowed, Export ("receiver")]
		SourceReceiver Receiver { get; }

		// @property (readonly, nonatomic) STPSourceRedirect * _Nullable redirect;
		[NullAllowed, Export ("redirect")]
		SourceRedirect Redirect { get; }

		// @property (readonly, nonatomic) STPSourceStatus status;
		[Export ("status")]
		SourceStatus Status { get; }

		// @property (readonly, nonatomic) STPSourceType type;
		[Export ("type")]
		SourceType Type { get; }

		// @property (readonly, nonatomic) STPSourceUsage usage;
		[Export ("usage")]
		SourceUsage Usage { get; }

		// @property (readonly, nonatomic) STPSourceVerification * _Nullable verification;
		[NullAllowed, Export ("verification")]
		SourceVerification Verification { get; }

		// @property (readonly, nonatomic) NSDictionary * _Nullable details;
		[NullAllowed, Export ("details")]
		NSDictionary Details { get; }

		// @property (readonly, nonatomic) STPSourceCardDetails * _Nullable cardDetails;
		[NullAllowed, Export ("cardDetails")]
		SourceCardDetails CardDetails { get; }

		// @property (readonly, nonatomic) STPSourceSEPADebitDetails * _Nullable sepaDebitDetails;
		[NullAllowed, Export ("sepaDebitDetails")]
		SourceSEPADebitDetails SepaDebitDetails { get; }

		// Leave this in place as long as ApiResponseDecodable contains this method
		// @required +(instancetype _Nullable)decodedObjectFromAPIResponse:(NSDictionary * _Nullable)response;
		[Static/*, Abstract*/]
		[Export ("decodedObjectFromAPIResponse:")]
		[return: NullAllowed]
		Source GetDecodedObject ([NullAllowed] NSDictionary response);
	}

	// @interface STPSourceParams : NSObject <STPFormEncodable, NSCopying>
	[BaseType (typeof (NSObject), Name = "STPSourceParams")]
	interface SourceParams : FormEncodable, INSCopying
	{
		// @property (assign, nonatomic) STPSourceType type;
		[Export ("type", ArgumentSemantic.Assign)]
		SourceType Type { get; set; }

		// @property (copy, nonatomic) NSString * _Nonnull rawTypeString;
		[Export ("rawTypeString")]
		string RawTypeString { get; set; }

		// @property (copy, nonatomic) NSNumber * _Nullable amount;
		[NullAllowed, Export ("amount", ArgumentSemantic.Copy)]
		NSNumber Amount { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable currency;
		[NullAllowed, Export ("currency")]
		string Currency { get; set; }

		// @property (assign, nonatomic) STPSourceFlow flow;
		[Export ("flow", ArgumentSemantic.Assign)]
		SourceFlow Flow { get; set; }

		// @property (copy, nonatomic) NSDictionary * _Nullable metadata;
		[NullAllowed, Export ("metadata", ArgumentSemantic.Copy)]
		NSDictionary Metadata { get; set; }

		// @property (copy, nonatomic) NSDictionary * _Nullable owner;
		[NullAllowed, Export ("owner", ArgumentSemantic.Copy)]
		NSDictionary Owner { get; set; }

		// @property (copy, nonatomic) NSDictionary * _Nullable redirect;
		[NullAllowed, Export ("redirect", ArgumentSemantic.Copy)]
		NSDictionary Redirect { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable token;
		[NullAllowed, Export ("token")]
		string Token { get; set; }

		// @property (assign, nonatomic) STPSourceUsage usage;
		[Export ("usage", ArgumentSemantic.Assign)]
		SourceUsage Usage { get; set; }

		// +(STPSourceParams * _Nonnull)bancontactParamsWithAmount:(NSUInteger)amount name:(NSString * _Nonnull)name returnURL:(NSString * _Nonnull)returnURL statementDescriptor:(NSString * _Nullable)statementDescriptor;
		[Static]
		[Export ("bancontactParamsWithAmount:name:returnURL:statementDescriptor:")]
		SourceParams CreateBancontactParams (nuint amount, string name, string returnUrl, [NullAllowed] string statementDescriptor);

		// +(STPSourceParams * _Nonnull)bitcoinParamsWithAmount:(NSUInteger)amount currency:(NSString * _Nonnull)currency email:(NSString * _Nonnull)email;
		[Static]
		[Export ("bitcoinParamsWithAmount:currency:email:")]
		SourceParams CreateBitcoinParams (nuint amount, string currency, string email);

		// +(STPSourceParams * _Nonnull)cardParamsWithCard:(STPCardParams * _Nonnull)card;
		[Static]
		[Export ("cardParamsWithCard:")]
		SourceParams CreateCardParams (CardParams card);

		// +(STPSourceParams * _Nonnull)giropayParamsWithAmount:(NSUInteger)amount name:(NSString * _Nonnull)name returnURL:(NSString * _Nonnull)returnURL statementDescriptor:(NSString * _Nullable)statementDescriptor;
		[Static]
		[Export ("giropayParamsWithAmount:name:returnURL:statementDescriptor:")]
		SourceParams CreateGiropayParams (nuint amount, string name, string returnUrl, [NullAllowed] string statementDescriptor);

		// +(STPSourceParams * _Nonnull)idealParamsWithAmount:(NSUInteger)amount name:(NSString * _Nonnull)name returnURL:(NSString * _Nonnull)returnURL statementDescriptor:(NSString * _Nullable)statementDescriptor bank:(NSString * _Nullable)bank;
		[Static]
		[Export ("idealParamsWithAmount:name:returnURL:statementDescriptor:bank:")]
		SourceParams CreateIdealParams (nuint amount, string name, string returnUrl, [NullAllowed] string statementDescriptor, [NullAllowed] string bank);

		// +(STPSourceParams * _Nonnull)sepaDebitParamsWithName:(NSString * _Nonnull)name iban:(NSString * _Nonnull)iban addressLine1:(NSString * _Nullable)addressLine1 city:(NSString * _Nullable)city postalCode:(NSString * _Nullable)postalCode country:(NSString * _Nullable)country;
		[Static]
		[Export ("sepaDebitParamsWithName:iban:addressLine1:city:postalCode:country:")]
		SourceParams CreateSepaDebitParams (string name, string iban, [NullAllowed] string addressLine1, [NullAllowed] string city, [NullAllowed] string postalCode, [NullAllowed] string country);

		// +(STPSourceParams * _Nonnull)sofortParamsWithAmount:(NSUInteger)amount returnURL:(NSString * _Nonnull)returnURL country:(NSString * _Nonnull)country statementDescriptor:(NSString * _Nullable)statementDescriptor;
		[Static]
		[Export ("sofortParamsWithAmount:returnURL:country:statementDescriptor:")]
		SourceParams CreateSofortParams (nuint amount, string returnUrl, string country, [NullAllowed] string statementDescriptor);

		// +(STPSourceParams * _Nonnull)threeDSecureParamsWithAmount:(NSUInteger)amount currency:(NSString * _Nonnull)currency returnURL:(NSString * _Nonnull)returnURL card:(NSString * _Nonnull)card;
		[Static]
		[Export ("threeDSecureParamsWithAmount:currency:returnURL:card:")]
		SourceParams CreateThreeDSecureParams (nuint amount, string currency, string returnUrl, string card);

		// +(STPSourceParams * _Nonnull)alipayParamsWithAmount:(NSUInteger)amount currency:(NSString * _Nonnull)currency returnURL:(NSString * _Nonnull)returnURL;
		[Static]
		[Export ("alipayParamsWithAmount:currency:returnURL:")]
		SourceParams CreateAlipayParams (nuint amount, string currency, string returnUrl);

		// +(STPSourceParams * _Nonnull)p24ParamsWithAmount:(NSUInteger)amount currency:(NSString * _Nonnull)currency email:(NSString * _Nonnull)email name:(NSString * _Nullable)name returnURL:(NSString * _Nonnull)returnURL;
		[Static]
		[Export ("p24ParamsWithAmount:currency:email:name:returnURL:")]
		SourceParams CreateP24Params (nuint amount, string currency, string email, [NullAllowed] string name, string returnUrl);
	}

	// @interface STPToken : NSObject <STPAPIResponseDecodable, STPSourceProtocol>
	[BaseType (typeof (NSObject), Name = "STPToken")]
	[DisableDefaultCtor]
	interface Token : ApiResponseDecodable, SourceProtocol
	{
		// @property (readonly, nonatomic) NSString * _Nonnull tokenId;
		[Export ("tokenId")]
		string TokenId { get; }

		// @property (readonly, nonatomic) BOOL livemode;
		[Export ("livemode")]
		bool Livemode { get; }

		// @property (readonly, nonatomic) STPCard * _Nullable card;
		[NullAllowed, Export ("card")]
		Card Card { get; }

		// @property (readonly, nonatomic) STPBankAccount * _Nullable bankAccount;
		[NullAllowed, Export ("bankAccount")]
		BankAccount BankAccount { get; }

		// @property (readonly, nonatomic) NSDate * _Nullable created;
		[NullAllowed, Export ("created")]
		NSDate Created { get; }

		// Leave this in place as long as ApiResponseDecodable contains this method
		// @required +(instancetype _Nullable)decodedObjectFromAPIResponse:(NSDictionary * _Nullable)response;
		[Static/*, Abstract*/]
		[Export ("decodedObjectFromAPIResponse:")]
		[return: NullAllowed]
		Token GetDecodedObject ([NullAllowed] NSDictionary response);
	}

	[Static]
	partial interface StripeConstants
	{
		// extern NSString *const _Nonnull StripeDomain;
		[Field ("StripeDomain", "__Internal")]
		NSString StripeDomain { get; }
	}

	[Static]
	partial interface UserInfoKeys
	{
		// extern NSString *const _Nonnull STPErrorMessageKey;
		[Field ("STPErrorMessageKey", "__Internal")]
		NSString ErrorMessageKey { get; }

		// extern NSString *const _Nonnull STPCardErrorCodeKey;
		[Field ("STPCardErrorCodeKey", "__Internal")]
		NSString CardErrorCodeKey { get; }

		// extern NSString *const _Nonnull STPErrorParameterKey;
		[Field ("STPErrorParameterKey", "__Internal")]
		NSString ErrorParameterKey { get; }

		// extern NSString *const _Nonnull STPStripeErrorCodeKey;
		[Field ("STPStripeErrorCodeKey", "__Internal")]
		NSString StripeErrorCodeKey { get; }

		// extern NSString *const _Nonnull STPStripeErrorTypeKey;
		[Field ("STPStripeErrorTypeKey", "__Internal")]
		NSString StripeErrorTypeKey { get; }
	}

	[Static]
	partial interface CardErrorCodeKeys
	{
		// extern STPCardErrorCode  _Nonnull const STPInvalidNumber;
		[Field ("STPInvalidNumber", "__Internal")]
		NSString InvalidNumber { get; }

		// extern STPCardErrorCode  _Nonnull const STPInvalidExpMonth;
		[Field ("STPInvalidExpMonth", "__Internal")]
		NSString InvalidExpMonth { get; }

		// extern STPCardErrorCode  _Nonnull const STPInvalidExpYear;
		[Field ("STPInvalidExpYear", "__Internal")]
		NSString InvalidExpYear { get; }

		// extern STPCardErrorCode  _Nonnull const STPInvalidCVC;
		[Field ("STPInvalidCVC", "__Internal")]
		NSString InvalidCVC { get; }

		// extern STPCardErrorCode  _Nonnull const STPIncorrectNumber;
		[Field ("STPIncorrectNumber", "__Internal")]
		NSString IncorrectNumber { get; }

		// extern STPCardErrorCode  _Nonnull const STPExpiredCard;
		[Field ("STPExpiredCard", "__Internal")]
		NSString ExpiredCard { get; }

		// extern STPCardErrorCode  _Nonnull const STPCardDeclined;
		[Field ("STPCardDeclined", "__Internal")]
		NSString CardDeclined { get; }

		// extern STPCardErrorCode  _Nonnull const STPIncorrectCVC;
		[Field ("STPIncorrectCVC", "__Internal")]
		NSString IncorrectCVC { get; }

		// extern STPCardErrorCode  _Nonnull const STPProcessingError;
		[Field ("STPProcessingError", "__Internal")]
		NSString ProcessingError { get; }
	}

	// @interface Stripe (NSError)
	[Category]
	[BaseType (typeof (NSError))]
	interface NSError_Stripe
	{
		// +(NSError * _Nullable)stp_errorFromStripeResponse:(NSDictionary * _Nullable)jsonDictionary;
		[Static]
		[Export ("stp_errorFromStripeResponse:")]
		[return: NullAllowed]
		NSError CreateErrorFromStripeResponse ([NullAllowed] NSDictionary jsonDictionary);
	}

	// @interface Stripe_Theme (UINavigationBar)
	[Category]
	[BaseType (typeof (UINavigationBar))]
	interface UINavigationBar_Stripe_Theme
	{
		// -(void)stp_setTheme:(STPTheme * _Nonnull)theme __attribute__((deprecated("Use the `stp_theme` property.")));
		[Export ("stp_setTheme:")]
		void StripeSetTheme (Theme theme);

		// @property (nonatomic, strong) STPTheme * _Nullable stp_theme;
		[NullAllowed, Export ("stp_theme")]
		Theme GetStripeTheme ();
		[NullAllowed, Export ("setStp_theme:")]
		void SetStripeTheme (Theme theme);
	}
}

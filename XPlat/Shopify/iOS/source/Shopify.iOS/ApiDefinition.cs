using System;

using AddressBook;
using CoreFoundation;
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using PassKit;
using UIKit;

namespace Shopify.Buy
{
	// @interface BUYObject : NSObject
	[BaseType (typeof(NSObject), Name = "BUYObject")]
	interface Object
	{
		// @property (readonly, nonatomic, strong) NSNumber * identifier;
		[Export ("identifier", ArgumentSemantic.Strong)]
		NSNumber Identifier { get; }

		// @property (readonly, getter = isDirty, nonatomic) BOOL dirty;
		[Export ("dirty")]
		bool Dirty { [Bind ("isDirty")] get; }

		// -(instancetype)initWithDictionary:(NSDictionary *)dictionary;
		[Export ("initWithDictionary:")]
		IntPtr Constructor (NSDictionary dictionary);

		// -(void)updateWithDictionary:(NSDictionary *)dictionary;
		[Export ("updateWithDictionary:")]
		void UpdateWithDictionary (NSDictionary dictionary);

		// +(NSArray *)convertJSONArray:(NSArray *)json block:(void (^)(id))createdBlock;
		[Static]
		[Export ("convertJSONArray:block:")]
		NSObject[] ConvertJSONArray (NSObject[] json, Action<NSObject> createdBlock);

		// +(NSArray *)convertJSONArray:(NSArray *)json;
		[Static]
		[Export ("convertJSONArray:")]
		NSObject[] ConvertJSONArray (NSObject[] json);

		// +(instancetype)convertObject:(id)object;
		[Static]
		[Export ("convertObject:")]
		Object ConvertObject (NSObject @object);

		// TODO: DirtyProperties might be string[]

		// -(NSSet *)dirtyProperties;
		[Export ("dirtyProperties")]
		NSSet DirtyProperties { get; }

		// -(void)markPropertyAsDirty:(NSString *)property;
		[Export ("markPropertyAsDirty:")]
		void MarkPropertyAsDirty (string property);

		// -(void)markAsClean;
		[Export ("markAsClean")]
		void MarkAsClean ();

		// +(void)trackDirtyProperties;
		[Static]
		[Export ("trackDirtyProperties")]
		void TrackDirtyProperties ();
	}

	interface ISerializable
	{

	}

	// @protocol BUYSerializable <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject), Name = "BUYSerializable")]
	interface Serializable
	{
		// @required -(NSDictionary *)jsonDictionaryForCheckout;
		[Abstract]
		[Export ("jsonDictionaryForCheckout")]
		NSDictionary JsonDictionaryForCheckout { get; }
	}

	// @interface BUYAddress : BUYObject <BUYSerializable>
	[BaseType (typeof(Object), Name = "BUYAddress")]
	interface Address : Serializable
	{
		// @property (copy, nonatomic) NSString * address1;
		[Export ("address1")]
		string Address1 { get; set; }

		// @property (copy, nonatomic) NSString * address2;
		[Export ("address2")]
		string Address2 { get; set; }

		// @property (copy, nonatomic) NSString * city;
		[Export ("city")]
		string City { get; set; }

		// @property (copy, nonatomic) NSString * company;
		[Export ("company")]
		string Company { get; set; }

		// @property (copy, nonatomic) NSString * firstName;
		[Export ("firstName")]
		string FirstName { get; set; }

		// @property (copy, nonatomic) NSString * lastName;
		[Export ("lastName")]
		string LastName { get; set; }

		// @property (copy, nonatomic) NSString * phone;
		[Export ("phone")]
		string Phone { get; set; }

		// @property (copy, nonatomic) NSString * country;
		[Export ("country")]
		string Country { get; set; }

		// @property (copy, nonatomic) NSString * countryCode;
		[Export ("countryCode")]
		string CountryCode { get; set; }

		// @property (copy, nonatomic) NSString * province;
		[Export ("province")]
		string Province { get; set; }

		// @property (copy, nonatomic) NSString * provinceCode;
		[Export ("provinceCode")]
		string ProvinceCode { get; set; }

		// @property (copy, nonatomic) NSString * zip;
		[Export ("zip")]
		string Zip { get; set; }


		// @interface ApplePay (BUYAddress)

		// +(NSString * _Nullable)buy_emailFromRecord:(ABRecordRef _Nullable)record;
		[Static]
		[Export ("buy_emailFromRecord:")]
		[return: NullAllowed]
		string ApplePayEmailFromRecord ([NullAllowed] ABRecord record);

		// +(BUYAddress * _Nonnull)buy_addressFromRecord:(ABRecordRef _Nullable)record __attribute__((availability(ios, introduced=8_0, deprecated=9_0)));
		[Introduced (PlatformName.iOS, 8, 0, message: "Use the CNContact backed `buy_addressFromContact:` instead")]
		[Deprecated (PlatformName.iOS, 9, 0, message: "Use the CNContact backed `buy_addressFromContact:` instead")]
		[Static]
		[Export ("buy_addressFromRecord:")]
		Address ApplePayAddressFromRecord ([NullAllowed] ABRecord record);

		// +(BUYAddress * _Nonnull)buy_addressFromContact:(PKContact * _Nullable)contact __attribute__((availability(ios, introduced=9_0)));
		[Introduced (PlatformName.iOS, 9, 0)]
		[Static]
		[Export ("buy_addressFromContact:")]
		Address ApplePayAddressFromContact ([NullAllowed] PKContact contact);

	}

	// @interface Additions (BUYAddress)
	[Category]
	[BaseType (typeof(Address))]
	interface AddressExtensions
	{
		// -(BOOL)isPartialAddress;
		[Export ("isPartialAddress")]
		bool IsPartialAddress ();

		// -(BOOL)isValidAddressForShippingRates;
		[Export ("isValidAddressForShippingRates")]
		bool IsValidAddressForShippingRates ();
	}

	// @interface BUYCheckout : BUYObject <BUYSerializable>
	[BaseType (typeof(Object), Name = "BUYCheckout")]
	[DisableDefaultCtor]
	interface Checkout : Serializable
	{
		// @property (copy, nonatomic) NSString * email;
		[Export ("email")]
		string Email { get; set; }

		// @property (readonly, copy, nonatomic) NSString * token;
		[Export ("token")]
		string Token { get; }

		// @property (readonly, copy, nonatomic) NSString * cartToken;
		[Export ("cartToken")]
		string CartToken { get; }

		// @property (readonly, assign, nonatomic) BOOL requiresShipping;
		[Export ("requiresShipping")]
		bool RequiresShipping { get; }

		// @property (readonly, assign, nonatomic) BOOL taxesIncluded;
		[Export ("taxesIncluded")]
		bool TaxesIncluded { get; }

		// @property (readonly, copy, nonatomic) NSString * currency;
		[Export ("currency")]
		string Currency { get; }

		// @property (readonly, nonatomic, strong) NSDecimalNumber * subtotalPrice;
		[Export ("subtotalPrice", ArgumentSemantic.Strong)]
		NSDecimalNumber SubtotalPrice { get; }

		// @property (readonly, nonatomic, strong) NSDecimalNumber * totalTax;
		[Export ("totalTax", ArgumentSemantic.Strong)]
		NSDecimalNumber TotalTax { get; }

		// @property (readonly, nonatomic, strong) NSDecimalNumber * totalPrice;
		[Export ("totalPrice", ArgumentSemantic.Strong)]
		NSDecimalNumber TotalPrice { get; }

		// @property (readonly, nonatomic, strong) NSString * paymentSessionId;
		[Export ("paymentSessionId", ArgumentSemantic.Strong)]
		string PaymentSessionId { get; }

		// @property (readonly, nonatomic, strong) NSURL * paymentURL;
		[Export ("paymentURL", ArgumentSemantic.Strong)]
		NSUrl PaymentURL { get; }

		// @property (nonatomic, strong) NSNumber * reservationTime;
		[Export ("reservationTime", ArgumentSemantic.Strong)]
		NSNumber ReservationTime { get; set; }

		// @property (readonly, nonatomic, strong) NSNumber * reservationTimeLeft;
		[Export ("reservationTimeLeft", ArgumentSemantic.Strong)]
		NSNumber ReservationTimeLeft { get; }

		// @property (readonly, nonatomic, strong) NSDecimalNumber * paymentDue;
		[Export ("paymentDue", ArgumentSemantic.Strong)]
		NSDecimalNumber PaymentDue { get; }

		// @property (readonly, copy, nonatomic) NSArray<__kindof BUYLineItem *> * lineItems;
		[Export ("lineItems", ArgumentSemantic.Copy)]
		LineItem[] LineItems { get; }

		// @property (readonly, copy, nonatomic) NSArray<BUYTaxLine *> * taxLines;
		[Export ("taxLines", ArgumentSemantic.Copy)]
		TaxLine[] TaxLines { get; }

		// @property (nonatomic, strong) BUYAddress * billingAddress;
		[Export ("billingAddress", ArgumentSemantic.Strong)]
		Address BillingAddress { get; set; }

		// @property (nonatomic, strong) BUYAddress * shippingAddress;
		[Export ("shippingAddress", ArgumentSemantic.Strong)]
		Address ShippingAddress { get; set; }

		// @property (nonatomic, strong) BUYShippingRate * shippingRate;
		[Export ("shippingRate", ArgumentSemantic.Strong)]
		ShippingRate ShippingRate { get; set; }

		// @property (readonly, nonatomic) NSString * shippingRateId  DEPRECATED_MSG_ATTRIBUTE("Use shippingRate.shippingRateIdentifier");
		[Export ("shippingRateId")]
		string ShippingRateId { get; }

		// @property (nonatomic, strong) BUYDiscount * discount;
		[Export ("discount", ArgumentSemantic.Strong)]
		Discount Discount { get; set; }

		// @property (readonly, nonatomic, strong) NSArray<BUYGiftCard *> * giftCards;
		[Export ("giftCards", ArgumentSemantic.Strong)]
		GiftCard[] GiftCards { get; }

		// @property (nonatomic, strong) NSString * channelId;
		[Export ("channelId", ArgumentSemantic.Strong)]
		string ChannelId { get; set; }

		// @property (nonatomic, strong) NSDictionary * marketingAttribution;
		[Export ("marketingAttribution", ArgumentSemantic.Strong)]
		NSDictionary MarketingAttribution { get; set; }

		// @property (readonly, nonatomic, strong) NSURL * webCheckoutURL;
		[Export ("webCheckoutURL", ArgumentSemantic.Strong)]
		NSUrl WebCheckoutURL { get; }

		// @property (nonatomic, strong) NSString * webReturnToURL;
		[Export ("webReturnToURL", ArgumentSemantic.Strong)]
		string WebReturnToURL { get; set; }

		// @property (nonatomic, strong) NSString * webReturnToLabel;
		[Export ("webReturnToLabel", ArgumentSemantic.Strong)]
		string WebReturnToLabel { get; set; }

		// @property (readonly, copy, nonatomic) NSDate * createdAtDate;
		[Export ("createdAtDate", ArgumentSemantic.Copy)]
		NSDate CreatedAtDate { get; }

		// @property (readonly, copy, nonatomic) NSDate * updatedAtDate;
		[Export ("updatedAtDate", ArgumentSemantic.Copy)]
		NSDate UpdatedAtDate { get; }

		// @property (readonly, nonatomic, strong) NSURL * privacyPolicyURL;
		[Export ("privacyPolicyURL", ArgumentSemantic.Strong)]
		NSUrl PrivacyPolicyURL { get; }

		// @property (readonly, nonatomic, strong) NSURL * refundPolicyURL;
		[Export ("refundPolicyURL", ArgumentSemantic.Strong)]
		NSUrl RefundPolicyURL { get; }

		// @property (readonly, nonatomic, strong) NSURL * termsOfServiceURL;
		[Export ("termsOfServiceURL", ArgumentSemantic.Strong)]
		NSUrl TermsOfServiceURL { get; }

		// @property (readonly, copy, nonatomic) NSString * sourceName;
		[Export ("sourceName")]
		string SourceName { get; }

		// @property (readonly, copy, nonatomic) NSString * sourceIdentifier;
		[Export ("sourceIdentifier")]
		string SourceIdentifier { get; }

		// @property (readonly, nonatomic, strong) BUYMaskedCreditCard * creditCard;
		[Export ("creditCard", ArgumentSemantic.Strong)]
		MaskedCreditCard CreditCard { get; }

		// @property (readonly, copy, nonatomic) NSString * customerId;
		[Export ("customerId")]
		string CustomerId { get; }

		// @property (copy, nonatomic) NSString * note;
		[Export ("note")]
		string Note { get; set; }

		// @property (nonatomic, copy) NSArray <BUYCheckoutAttribute *> *attributes;
		[Export ("attributes", ArgumentSemantic.Copy)]
		CheckoutAttribute[] Attributes { get; }

		// @property (readonly, nonatomic, strong) BUYOrder * order;
		[Export ("order", ArgumentSemantic.Strong)]
		Order Order { get; }

		// @property (assign, nonatomic) BOOL partialAddresses;
		[Export ("partialAddresses")]
		bool PartialAddresses { get; set; }

		// -(instancetype)initWithCart:(BUYCart *)cart;
		[Export ("initWithCart:")]
		IntPtr Constructor (Cart cart);

		// -(instancetype)initWithCartToken:(NSString *)cartToken;
		[Export ("initWithCartToken:")]
		IntPtr Constructor (string cartToken);

		// -(BOOL)hasToken;
		[Export ("hasToken")]
		bool HasToken { get; }

		// @property (readonly, copy, nonatomic) NSNumber * orderId __attribute__((deprecated("Available on the BUYOrder object")));
		[Export ("orderId", ArgumentSemantic.Copy)]
		NSNumber OrderId { get; }

		// @property (readonly, nonatomic, strong) NSURL * orderStatusURL __attribute__((deprecated("Available on the BUYOrder object")));
		[Export ("orderStatusURL", ArgumentSemantic.Strong)]
		NSUrl OrderStatusURL { get; }
	}

	// @interface BUYCheckoutAttribute : BUYObject <BUYSerializable>
	[BaseType (typeof(Object), Name = "BUYCheckoutAttribute")]
	interface CheckoutAttribute : Serializable
	{
		// @property (nonatomic, strong, nonnull) NSString *name;
		[Export ("name", ArgumentSemantic.Strong)]
		string Name { get; set; }

		// @property (nonatomic, strong, nonnull) NSString *value;
		[Export ("value", ArgumentSemantic.Strong)]
		string Vitle { get; set; }
	}

	// @interface BUYShippingRate : BUYObject <BUYSerializable>
	[BaseType (typeof(Object), Name = "BUYShippingRate")]
	interface ShippingRate : Serializable
	{
		// @property (readonly, nonatomic, strong) NSString * shippingRateIdentifier;
		[Export ("shippingRateIdentifier", ArgumentSemantic.Strong)]
		string ShippingRateIdentifier { get; }

		// @property (readonly, nonatomic, strong) NSString * title;
		[Export ("title", ArgumentSemantic.Strong)]
		string Title { get; }

		// @property (readonly, nonatomic, strong) NSDecimalNumber * price;
		[Export ("price", ArgumentSemantic.Strong)]
		NSDecimalNumber Price { get; }

		// TODO: DeliveryRange might be NSDate[]

		// @property (readonly, nonatomic, strong) NSArray * deliveryRange;
		[Export ("deliveryRange", ArgumentSemantic.Strong)]
		NSObject[] DeliveryRange { get; }


		// @interface ApplePay (BUYShippingRate)

		// +(NSArray<PKShippingMethod *> * _Nonnull)buy_convertShippingRatesToShippingMethods:(NSArray<BUYShippingRate *> * _Nonnull)rates;
		[Static]
		[Export ("buy_convertShippingRatesToShippingMethods:")]
		PKShippingMethod[] ApplePayConvertShippingRatesToShippingMethods (ShippingRate[] rates);

	}

	// @interface ApplePay (BUYCheckout)
	[Category]
	[BaseType (typeof(Checkout), Name = "BUYCheckout")]
	interface CheckoutApplePayExtensions
	{
		// -(NSArray<PKPaymentSummaryItem *> * _Nonnull)buy_summaryItems;
		[Export ("buy_summaryItems")]
		PKPaymentSummaryItem[] ApplePaySummaryItems ();

		// -(NSArray<PKPaymentSummaryItem *> * _Nonnull)buy_summaryItemsWithShopName:(NSString * _Nullable)shopName;
		[Export ("buy_summaryItemsWithShopName:")]
		PKPaymentSummaryItem[] ApplePaySummaryItems ([NullAllowed] string shopName);
	}

	// @interface BUYApplePayHelpers : NSObject <PKPaymentAuthorizationViewControllerDelegate>
	[BaseType (typeof(NSObject), Name = "BUYApplePayHelpers")]
	interface ApplePayHelpers : IPKPaymentAuthorizationViewControllerDelegate
	{
		// -(instancetype)initWithClient:(BUYClient *)client checkout:(BUYCheckout *)checkout;
		[Export ("initWithClient:checkout:")]
		IntPtr Constructor (BuyClient client, Checkout checkout);

		// -(instancetype)initWithClient:(BUYClient *)client checkout:(BUYCheckout *)checkout shop:(BUYShop *)shop;
		[Export ("initWithClient:checkout:shop:")]
		IntPtr Constructor (BuyClient client, Checkout checkout, Shop shop);

		// -(void)updateAndCompleteCheckoutWithPayment:(PKPayment *)payment completion:(void (^)(PKPaymentAuthorizationStatus))completion __attribute__((deprecated("BUYApplePayHelpers now implements PKPaymentAuthorizationViewControllerDelegate instead")));
		[Export ("updateAndCompleteCheckoutWithPayment:completion:")]
		void UpdateAndCompleteCheckoutWithPayment (PKPayment payment, Action<PKPaymentAuthorizationStatus> completion);

		// -(void)updateCheckoutWithShippingMethod:(PKShippingMethod *)shippingMethod completion:(void (^)(PKPaymentAuthorizationStatus, NSArray *))completion __attribute__((deprecated("BUYApplePayHelpers now implements PKPaymentAuthorizationViewControllerDelegate instead")));
		[Export ("updateCheckoutWithShippingMethod:completion:")]
		void UpdateCheckoutWithShippingMethod (PKShippingMethod shippingMethod, Action<PKPaymentAuthorizationStatus, NSArray> completion);

		// -(void)updateCheckoutWithAddress:(ABRecordRef)address completion:(void (^)(PKPaymentAuthorizationStatus, NSArray *, NSArray *))completion __attribute__((availability(ios, introduced=8_0, deprecated=9_0)));
		[Introduced (PlatformName.iOS, 8, 0, message: "Use the CNContact backed `updateCheckoutWithContact:completion:` instead")]
		[Deprecated (PlatformName.iOS, 9, 0, message: "Use the CNContact backed `updateCheckoutWithContact:completion:` instead")]
		[Export ("updateCheckoutWithAddress:completion:")]
		void UpdateCheckoutWithAddress (ABRecord address, Action<PKPaymentAuthorizationStatus, NSArray, NSArray> completion);

		// -(void)updateCheckoutWithContact:(PKContact *)contact completion:(void (^)(PKPaymentAuthorizationStatus, NSArray *, NSArray *))completion __attribute__((deprecated("BUYApplePayHelpers now implements PKPaymentAuthorizationViewControllerDelegate instead")));
		[Export ("updateCheckoutWithContact:completion:")]
		void UpdateCheckoutWithContact (PKContact contact, Action<PKPaymentAuthorizationStatus, NSArray, NSArray> completion);

		// @property (readonly, nonatomic, strong) BUYCheckout * checkout;
		[Export ("checkout", ArgumentSemantic.Strong)]
		Checkout Checkout { get; }

		// @property (readonly, nonatomic, strong) BUYClient * client;
		[Export ("client", ArgumentSemantic.Strong)]
		BuyClient Client { get; }

		// @property (readonly, nonatomic, strong) NSError * lastError;
		[Export ("lastError", ArgumentSemantic.Strong)]
		NSError LastError { get; }

		// @property (readonly, nonatomic, strong) BUYShop * shop;
		[Export ("shop", ArgumentSemantic.Strong)]
		Shop Shop { get; }
	}

	// @interface BUYCart : NSObject <BUYSerializable>
	[BaseType (typeof(NSObject), Name = "BUYCart")]
	interface Cart : Serializable
	{
		// @property (readonly, nonatomic, strong) NSArray<BUYCartLineItem *> * _Nonnull lineItems;
		[Export ("lineItems", ArgumentSemantic.Strong)]
		CartLineItem[] LineItems { get; }

		// -(BOOL)isValid;
		[Export ("isValid")]
		bool IsValid { get; }

		// -(void)clearCart;
		[Export ("clearCart")]
		void ClearCart ();

		// -(void)addVariant:(BUYProductVariant * _Nonnull)variant;
		[Export ("addVariant:")]
		void AddVariant (ProductVariant variant);

		// -(void)removeVariant:(BUYProductVariant * _Nonnull)variant;
		[Export ("removeVariant:")]
		void RemoveVariant (ProductVariant variant);

		// -(void)setVariant:(BUYProductVariant * _Nonnull)variant withTotalQuantity:(NSInteger)quantity;
		[Export ("setVariant:withTotalQuantity:")]
		void SetVariant (ProductVariant variant, nint quantity);
	}

	// @interface BUYLineItem : BUYObject <BUYSerializable>
	[BaseType (typeof(Object), Name = "BUYLineItem")]
	interface LineItem : Serializable
	{
		// @property (readonly, nonatomic, strong) NSString * lineItemIdentifier;
		[Export ("lineItemIdentifier", ArgumentSemantic.Strong)]
		string LineItemIdentifier { get; }

		// @property (readonly, nonatomic, strong) NSNumber * variantId;
		[Export ("variantId", ArgumentSemantic.Strong)]
		NSNumber VariantId { get; }

		// @property (readonly, nonatomic, strong) NSNumber * productId;
		[Export ("productId", ArgumentSemantic.Strong)]
		NSNumber ProductId { get; }

		// @property (nonatomic, strong) NSDecimalNumber * quantity;
		[Export ("quantity", ArgumentSemantic.Strong)]
		NSDecimalNumber Quantity { get; set; }

		// @property (readonly, nonatomic, strong) NSDecimalNumber * grams;
		[Export ("grams", ArgumentSemantic.Strong)]
		NSDecimalNumber Grams { get; }

		// @property (nonatomic, strong) NSDecimalNumber * price;
		[Export ("price", ArgumentSemantic.Strong)]
		NSDecimalNumber Price { get; set; }

		// @property (nonatomic, strong) NSDecimalNumber * linePrice;
		[Export ("linePrice", ArgumentSemantic.Strong)]
		NSDecimalNumber LinePrice { get; set; }

		// @property (readonly, nonatomic, strong) NSDecimalNumber * compareAtPrice;
		[Export ("compareAtPrice", ArgumentSemantic.Strong)]
		NSDecimalNumber CompareAtPrice { get; }

		// @property (copy, nonatomic) NSString * title;
		[Export ("title")]
		string Title { get; set; }

		// @property (copy, nonatomic) NSString * variantTitle;
		[Export ("variantTitle")]
		string VariantTitle { get; set; }

		// @property (nonatomic, strong) NSNumber * requiresShipping;
		[Export ("requiresShipping", ArgumentSemantic.Strong)]
		NSNumber RequiresShipping { get; set; }

		// @property (readonly, copy, nonatomic) NSString * sku;
		[Export ("sku")]
		string Sku { get; }

		// @property (readonly, assign, nonatomic) BOOL taxable;
		[Export ("taxable")]
		bool Taxable { get; }

		// @property (copy, nonatomic) NSDictionary * properties;
		[Export ("properties", ArgumentSemantic.Copy)]
		NSDictionary Properties { get; set; }

		// @property (readonly, copy, nonatomic) NSString * fulfillmentService;
		[Export ("fulfillmentService")]
		string FulfillmentService { get; }

		// -(instancetype)initWithVariant:(BUYProductVariant *)variant;
		[Export ("initWithVariant:")]
		IntPtr Constructor (ProductVariant variant);
	}

	// @interface BUYCartLineItem : BUYLineItem
	[BaseType (typeof(LineItem), Name = "BUYCartLineItem")]
	interface CartLineItem
	{
		// @property (readonly, nonatomic, strong) BUYProductVariant * variant;
		[Export ("variant", ArgumentSemantic.Strong)]
		ProductVariant Variant { get; }
	}

	// @interface BUYTheme : NSObject
	[BaseType (typeof(NSObject), Name = "BUYTheme")]
	interface Theme
	{
		// extern const CGFloat kBuyPaddingSmall;
		[Static]
		[Field ("kBuyPaddingSmall", "__Internal")]
		nfloat kBuyPaddingSmall { get; }

		// extern const CGFloat kBuyPaddingMedium;
		[Static]
		[Field ("kBuyPaddingMedium", "__Internal")]
		nfloat kBuyPaddingMedium { get; }

		// extern const CGFloat kBuyPaddingLarge;
		[Static]
		[Field ("kBuyPaddingLarge", "__Internal")]
		nfloat kBuyPaddingLarge { get; }

		// extern const CGFloat kBuyPaddingExtraLarge;
		[Static]
		[Field ("kBuyPaddingExtraLarge", "__Internal")]
		nfloat kBuyPaddingExtraLarge { get; }

		// extern const CGFloat kBuyTopGradientViewHeight;
		[Static]
		[Field ("kBuyTopGradientViewHeight", "__Internal")]
		nfloat kBuyTopGradientViewHeight { get; }

		// extern const CGFloat kBuyCheckoutButtonHeight;
		[Static]
		[Field ("kBuyCheckoutButtonHeight", "__Internal")]
		nfloat kBuyCheckoutButtonHeight { get; }

		// extern const CGFloat kBuyPageControlHeight;
		[Static]
		[Field ("kBuyPageControlHeight", "__Internal")]
		nfloat kBuyPageControlHeight { get; }

		// extern const CGFloat kBuyBottomGradientHeightWithPageControl;
		[Static]
		[Field ("kBuyBottomGradientHeightWithPageControl", "__Internal")]
		nfloat kBuyBottomGradientHeightWithPageControl { get; }

		// extern const CGFloat kBuyBottomGradientHeightWithoutPageControl;
		[Static]
		[Field ("kBuyBottomGradientHeightWithoutPageControl", "__Internal")]
		nfloat kBuyBottomGradientHeightWithoutPageControl { get; }


		// @property (nonatomic, strong) UIColor * tintColor;
		[Export ("tintColor", ArgumentSemantic.Strong)]
		UIColor TintColor { get; set; }

		// @property (assign, nonatomic) BUYThemeStyle style;
		[Export ("style", ArgumentSemantic.Assign)]
		ThemeStyle Style { get; set; }

		// @property (assign, nonatomic) BOOL showsProductImageBackground;
		[Export ("showsProductImageBackground")]
		bool ShowsProductImageBackground { get; set; }


		// interface BUYTheme_Additions

		[Static]
		[Export ("topGradientViewTopColor")]
		UIColor TopGradientViewTopColor ();

		[Static]
		[Export ("comparePriceTextColor")]
		UIColor ComparePriceTextColor ();

		[Static]
		[Export ("descriptionTextColor")]
		UIColor DescriptionTextColor ();

		[Static]
		[Export ("variantPriceTextColor")]
		UIColor VariantPriceTextColor ();

		[Static]
		[Export ("variantSoldOutTextColor")]
		UIColor VariantSoldOutTextColor ();

		[Static]
		[Export ("variantBreadcrumbsTextColor")]
		UIColor VariantBreadcrumbsTextColor ();

		[Static]
		[Export ("productTitleFont")]
		UIFont ProductTitleFont ();

		[Static]
		[Export ("productPriceFont")]
		UIFont ProductPriceFont ();

		[Static]
		[Export ("productComparePriceFont")]
		UIFont ProductComparePriceFont ();

		[Static]
		[Export ("variantOptionNameFont")]
		UIFont VariantOptionNameFont ();

		[Static]
		[Export ("variantOptionValueFont")]
		UIFont VariantOptionValueFont ();

		[Static]
		[Export ("variantOptionPriceFont")]
		UIFont VariantOptionPriceFont ();

		[Static]
		[Export ("variantBreadcrumbsFont")]
		UIFont VariantBreadcrumbsFont ();

		[Static]
		[Export ("errorLabelFont")]
		UIFont ErrorLabelFont ();

	}

	interface IThemeable
	{

	}

	// @protocol BUYThemeable <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject), Name = "BUYThemeable")]
	interface Themeable
	{
		// @required -(void)setTheme:(BUYTheme *)theme;
		[Abstract]
		[Export ("setTheme:")]
		void SetTheme (Theme theme);
	}

	// @interface BUYCheckoutButton : UIButton <BUYThemeable>
	[BaseType (typeof(UIButton), Name = "BUYCheckoutButton")]
	interface CheckoutButton : Themeable
	{
		// -(void)showActivityIndicator:(BOOL)show;
		[Export ("showActivityIndicator:")]
		void ShowActivityIndicator (bool show);
	}

	delegate void DataCreditCardBlock (Checkout checkout, string paymentSessionId, NSError error);

	delegate void DataCheckoutBlock (Checkout checkout, NSError error);

	delegate void DataCheckoutStatusBlock (Status status, NSError error);

	delegate void DataShippingRatesBlock (ShippingRate[] shippingRates, Status status, NSError error);

	delegate void DataShopBlock (Shop shop, NSError error);

	delegate void DataProductBlock (Product product, NSError error);

	delegate void DataProductsBlock (Product[] products, NSError error);

	delegate void DataCollectionsBlock (Collection[] collections, NSError error);

	delegate void DataCollectionsListBlock (Collection[] collections, nuint page, bool reachedEnd, NSError error);

	delegate void DataProductListBlock (Product[] productList, nuint page, bool reachedEnd, NSError error);

//	delegate void DataImagesListBlock (BUYProductImage[] imagesList, NSError error);

	delegate void DataGiftCardBlock (GiftCard giftCard, NSError error);

	[BaseType (typeof(NSObject), Name = "BUYClient")]
	[DisableDefaultCtor]
	interface BuyClient
	{
		[Static]
		[Field ("BUYVersionString", "__Internal")]
		NSString BUYVersionString { get; }


		[Export ("initWithShopDomain:apiKey:channelId:")]
		[DesignatedInitializer]
		IntPtr Constructor (string shopDomain, string apiKey, string channelId);

		[Export ("queue", ArgumentSemantic.Strong)]
		DispatchQueue Queue { get; set; }

		[Export ("pageSize")]
		nuint PageSize { get; set; }

		[Export ("shopDomain", ArgumentSemantic.Strong)]
		string ShopDomain { get; }

		[Export ("apiKey", ArgumentSemantic.Strong)]
		string ApiKey { get; }

		[Export ("channelId", ArgumentSemantic.Strong)]
		string ChannelId { get; }

		[Export ("merchantId", ArgumentSemantic.Strong)]
		string MerchantId { get; }

		[Export ("applicationName", ArgumentSemantic.Strong)]
		string ApplicationName { get; set; }

		[Export ("urlScheme", ArgumentSemantic.Strong)]
		string UrlScheme { get; set; }

		[Export ("getShop:")]
		NSUrlSessionDataTask GetShop (DataShopBlock block);

		[Export ("getProductsPage:completion:")]
		NSUrlSessionDataTask GetProductsPage (nuint page, DataProductListBlock block);

		[Export ("getProductById:completion:")]
		NSUrlSessionDataTask GetProduct (string productId, DataProductBlock block);

		[Export ("getProductsByIds:completion:")]
		NSUrlSessionDataTask GetProducts (string[] productIds, DataProductsBlock block);

		[Export ("getCollections:")]
		NSUrlSessionDataTask GetCollections (DataCollectionsBlock block);

		[Export("getCollectionsPage:completion:")]
		NSUrlSessionDataTask GetCollectionsPage(nuint page, DataCollectionsListBlock block);

		[Export ("getProductsPage:inCollection:completion:")]
		NSUrlSessionDataTask GetProductsPage (nuint page, NSNumber collectionId, DataProductListBlock block);

		[Export ("getProductsPage:inCollection:sortOrder:completion:")]
		NSUrlSessionDataTask GetProductsPage (nuint page, NSNumber collectionId, CollectionSort sortOrder, DataProductListBlock block);

		[Export ("createCheckout:completion:")]
		NSUrlSessionDataTask CreateCheckout (Checkout checkout, DataCheckoutBlock block);

		[Export ("createCheckoutWithCartToken:completion:")]
		NSUrlSessionDataTask CreateCheckout (string cartToken, DataCheckoutBlock block);

		[Export ("applyGiftCardWithCode:toCheckout:completion:")]
		NSUrlSessionDataTask ApplyGiftCard (string giftCardCode, Checkout checkout, DataCheckoutBlock block);

		[Export ("removeGiftCard:fromCheckout:completion:")]
		NSUrlSessionDataTask RemoveGiftCard (GiftCard giftCard, Checkout checkout, DataCheckoutBlock block);

		[Export ("getCheckout:completion:")]
		NSUrlSessionDataTask GetCheckout (Checkout checkout, DataCheckoutBlock block);

		[Export ("updateCheckout:completion:")]
		NSUrlSessionDataTask UpdateCheckout (Checkout checkout, DataCheckoutBlock block);

		[Export ("completeCheckout:completion:")]
		NSUrlSessionDataTask CompleteCheckout (Checkout checkout, DataCheckoutBlock block);

		[Export ("completeCheckout:withApplePayToken:completion:")]
		NSUrlSessionDataTask CompleteCheckout (Checkout checkout, PKPaymentToken token, DataCheckoutBlock block);

		[Export ("getCompletionStatusOfCheckout:completion:")]
		NSUrlSessionDataTask GetCompletionStatusOfCheckout (Checkout checkout, DataCheckoutStatusBlock block);

		[Export ("getCompletionStatusOfCheckoutURL:completion:")]
		NSUrlSessionDataTask GetCompletionStatusOfCheckoutURL (NSUrl url, DataCheckoutStatusBlock block);

		[Export ("getShippingRatesForCheckout:completion:")]
		NSUrlSessionDataTask GetShippingRatesForCheckout (Checkout checkout, DataShippingRatesBlock block);

		[Export ("storeCreditCard:checkout:completion:")]
		NSUrlSessionDataTask StoreCreditCard (ISerializable creditCard, Checkout checkout, DataCreditCardBlock block);

		[Export ("removeProductReservationsFromCheckout:completion:")]
		NSUrlSessionDataTask RemoveProductReservationsFromCheckout (Checkout checkout, DataCheckoutBlock block);

		[Export ("enableApplePayWithMerchantId:")]
		void EnableApplePay (string merchantId);

	}

	[Category]
	[BaseType (typeof(BuyClient), Name = "BUYClient")]
	interface ClientTestExtensions
	{
		[Export ("testIntegrationWithMerchantId:")]
		bool TestIntegration (string merchantId);

		[Export ("testIntegration")]
		bool TestIntegration ();

	}

	[BaseType (typeof(Object), Name = "BUYCollection")]
	interface Collection
	{
		[Export ("title", ArgumentSemantic.Strong)]
		string Title { get; }

		[Export ("collectionId", ArgumentSemantic.Strong)]
		NSNumber CollectionId { get; }

		[Export ("htmlDescription", ArgumentSemantic.Strong)]
		string HtmlDescription { get; }

		[Export ("imageURL", ArgumentSemantic.Strong)]
		NSUrl ImageURL { get; }

		[Export ("handle", ArgumentSemantic.Strong)]
		string CollectionHandle { get; }

		[Export ("published")]
		bool Published { get; }

		[Export ("createdAtDate", ArgumentSemantic.Copy)]
		NSDate CreatedAtDate { get; }

		[Export ("updatedAtDate", ArgumentSemantic.Copy)]
		NSDate UpdatedAtDate { get; }

		[Export ("publishedAtDate", ArgumentSemantic.Copy)]
		NSDate PublishedAtDate { get; }


		// interface BUYCollection_Additions

		[Static]
		[Export ("sortOrderParameterForCollectionSort:")]
		string SortOrderParameterForCollectionSort (CollectionSort sort);

	}

	[BaseType (typeof(NSObject), Name = "BUYCreditCard")]
	interface CreditCard : Serializable
	{
		[Export ("nameOnCard")]
		string NameOnCard { get; set; }

		[Export ("number")]
		string Number { get; set; }

		[Export ("expiryMonth")]
		string ExpiryMonth { get; set; }

		[Export ("expiryYear")]
		string ExpiryYear { get; set; }

		[Export ("cvv")]
		string Cvv { get; set; }

	}

	[BaseType (typeof(Object), Name = "BUYDiscount")]
	interface Discount : Serializable
	{
		[Export ("code")]
		string Code { get; set; }

		[Export ("amount", ArgumentSemantic.Strong)]
		NSDecimalNumber Amount { get; set; }

		[Export ("applicable")]
		bool Applicable { get; set; }

		[Export ("initWithCode:")]
		IntPtr Constructor (string code);

	}

	[BaseType (typeof(NSError), Name = "BUYError")]
	interface Error
	{
		[Static]
		[Field ("BUYShopifyError", "__Internal")]
		NSString BUYShopifyError { get; }

	}

	[BaseType (typeof(Object), Name = "BUYGiftCard")]
	interface GiftCard : Serializable
	{
		[Export ("code")]
		string Code { get; }

		[Export ("lastCharacters")]
		string LastCharacters { get; }

		[Export ("balance", ArgumentSemantic.Strong)]
		NSDecimalNumber Balance { get; }

		[Export ("amountUsed", ArgumentSemantic.Strong)]
		NSDecimalNumber AmountUsed { get; }

	}

	[BaseType (typeof(UIView), Name = "BUYGradientView")]
	interface GradientView
	{
		[Export ("topColor", ArgumentSemantic.Strong)]
		UIColor TopColor { get; set; }

		[Export ("bottomColor", ArgumentSemantic.Strong)]
		UIColor BottomColor { get; set; }

	}

	[BaseType (typeof(Object), Name = "BUYImage")]
	interface Image
	{
		[Export ("src")]
		string Src { get; }

		[Export ("variantIds", ArgumentSemantic.Copy)]
		NSNumber[] VariantIds { get; }

		[Export ("createdAtDate", ArgumentSemantic.Copy)]
		NSDate CreatedAtDate { get; }

		[Export ("updatedAtDate", ArgumentSemantic.Copy)]
		NSDate UpdatedAtDate { get; }

		[Export ("position", ArgumentSemantic.Copy)]
		NSNumber Position { get; }

		[Export ("productId", ArgumentSemantic.Copy)]
		NSNumber ProductId { get; }

	}

	[BaseType (typeof(NSObject), Name = "BUYImageKit")]
	interface ImageKit
	{
		[Static]
		[Export ("imageOfVariantCloseImageWithFrame:")]
		UIImage ImageOfVariantCloseImageWithFrame (CGRect frame);

		[Static]
		[Export ("imageOfPreviousSelectionIndicatorImageWithFrame:")]
		UIImage ImageOfPreviousSelectionIndicatorImageWithFrame (CGRect frame);

		[Static]
		[Export ("imageOfDisclosureIndicatorImageWithFrame:color:")]
		UIImage ImageOfDisclosureIndicatorImageWithFrame (CGRect frame, UIColor color);

		[Static]
		[Export ("imageOfProductViewCloseImageWithFrame:color:hasShadow:")]
		UIImage ImageOfProductViewCloseImageWithFrame (CGRect frame, UIColor color, bool hasShadow);

		[Static]
		[Export ("imageOfVariantBackImageWithFrame:")]
		UIImage ImageOfVariantBackImageWithFrame (CGRect frame);

	}

	[BaseType (typeof(UIImageView), Name = "BUYImageView")]
	interface ImageView : Themeable
	{
		[Static]
		[Field ("imageDuration", "__Internal")]
		float imageDuration { get; }


		[Export ("showsActivityIndicator")]
		bool ShowsActivityIndicator { get; set; }

		[Export ("loadImageWithURL:completion:")]
		void LoadImageWithURL (NSUrl imageURL, Action<UIImage, NSError> completion);

		[Export ("loadImageWithURL:animateChange:completion:")]
		void LoadImageWithURL (NSUrl imageURL, bool animateChange, Action<UIImage, NSError> completion);

		[Export ("cancelImageTask")]
		void CancelImageTask ();

		[Export ("isPortraitOrSquare")]
		bool IsPortraitOrSquare { get; }

	}

	[BaseType (typeof(Object), Name = "BUYMaskedCreditCard")]
	interface MaskedCreditCard
	{
		[Export ("firstName")]
		string FirstName { get; set; }

		[Export ("lastName")]
		string LastName { get; set; }

		[Export ("firstDigits")]
		string FirstDigits { get; set; }

		[Export ("lastDigits")]
		string LastDigits { get; set; }

		[Export ("expiryYear", ArgumentSemantic.Copy)]
		NSNumber ExpiryYear { get; set; }

		[Export ("expiryMonth", ArgumentSemantic.Copy)]
		NSNumber ExpiryMonth { get; set; }

	}

	interface INavigationControllerDelegate
	{

	}

	[Protocol, Model]
	[BaseType (typeof(NSObject), Name = "BUYNavigationControllerDelegate")]
	interface NavigationControllerDelegate
	{
		[Abstract]
		[Export ("presentationControllerWillDismiss:")]
		void PresentationControllerWillDismiss (UIPresentationController presentationController);

		[Abstract]
		[Export ("presentationControllerDidDismiss:")]
		void PresentationControllerDidDismiss (UIPresentationController presentationController);

	}

	[BaseType (typeof(UINavigationController), Name = "BUYNavigationController")]
	interface NavigationController : Themeable
	{
		[Export ("updateCloseButtonImageWithTintColor:duration:")]
		void UpdateCloseButtonImageWithTintColor (bool tintColor, nfloat duration);

		[NullAllowed, Export ("navigationDelegate", ArgumentSemantic.Weak)]
		INavigationControllerDelegate NavigationDelegate { get; set; }
	}

	[BaseType (typeof(Object), Name = "BUYOption")]
	interface Option
	{
		[Export ("name")]
		string Name { get; }

		[Export ("position", ArgumentSemantic.Strong)]
		NSNumber Position { get; }

		[Export ("productId", ArgumentSemantic.Copy)]
		NSNumber ProductId { get; }

	}

	[BaseType (typeof(UIView), Name = "BUYVariantOptionBreadCrumbsView")]
	interface VariantOptionBreadCrumbsView : Themeable
	{
		[Export ("hiddenConstraint", ArgumentSemantic.Strong)]
		NSLayoutConstraint HiddenConstraint { get; set; }

		[Export ("visibleConstraint", ArgumentSemantic.Strong)]
		NSLayoutConstraint VisibleConstraint { get; set; }

		[Export ("setSelectedBuyOptionValues:")]
		void SetSelectedBuyOptionValues (string[] optionValuesNames);

	}

	[BaseType (typeof(NavigationController), Name = "BUYOptionSelectionNavigationController")]
	interface OptionSelectionNavigationController : Themeable
	{
		[Export ("dismissWithCancelAnimation")]
		bool DismissWithCancelAnimation { get; set; }

		[Export ("breadsCrumbsView", ArgumentSemantic.Strong)]
		VariantOptionBreadCrumbsView BreadsCrumbsView { get; set; }

	}

	interface IOptionSelectionDelegate
	{

	}

	[Protocol, Model]
	[BaseType (typeof(NSObject), Name = "BUYOptionSelectionDelegate")]
	interface OptionSelectionDelegate
	{
		[Abstract]
		[Export ("optionSelectionController:didSelectOptionValue:")]
		void OptionSelectionController (OptionSelectionViewController controller, OptionValue optionValue);

		[Abstract]
		[Export ("optionSelectionControllerDidBackOutOfChoosingOption:")]
		void OptionSelectionControllerDidBackOutOfChoosingOption (OptionSelectionViewController controller);

	}

	[BaseType (typeof(UITableViewController), Name = "BUYOptionSelectionViewController")]
	interface OptionSelectionViewController : Themeable
	{
		[Export ("initWithOptionValues:filteredProductVariantsForSelectionOption:")]
		IntPtr Constructor (OptionValue[] optionValues, ProductVariant[] filteredProductVariantsForSelectionOption);

		[Export ("optionValues", ArgumentSemantic.Strong)]
		OptionValue[] OptionValues { get; }

		[Export ("selectedOptionValue", ArgumentSemantic.Strong)]
		OptionValue SelectedOptionValue { get; set; }

		[Export ("isLastOption")]
		bool IsLastOption { get; set; }

		[Export ("filteredProductVariantsForSelectionOption", ArgumentSemantic.Strong)]
		ProductVariant[] FilteredProductVariantsForSelectionOption { get; }

		[NullAllowed, Export ("currencyFormatter", ArgumentSemantic.Weak)]
		NSNumberFormatter CurrencyFormatter { get; set; }

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IOptionSelectionDelegate Delegate { get; set; }

	}

	[BaseType (typeof(Object), Name = "BUYOptionValue")]
	interface OptionValue
	{
		[Export ("name")]
		string Name { get; }

		[Export ("value", ArgumentSemantic.Strong)]
		string Value { get; }

		[Export ("optionId", ArgumentSemantic.Strong)]
		NSNumber OptionId { get; }

	}

	[BaseType (typeof(UITableViewCell), Name = "BUYOptionValueCell")]
	interface OptionValueCell
	{
		[Export ("selectedImageView", ArgumentSemantic.Strong)]
		UIImageView SelectedImageView { get; set; }

		[Export ("setOptionValue:productVariant:currencyFormatter:theme:")]
		void SetOptionValue (OptionValue optionValue, ProductVariant productVariant, NSNumberFormatter currencyFormatter, Theme theme);

	}

	[BaseType (typeof(Object), Name = "BUYOrder")]
	interface Order
	{
		[Export ("statusURL", ArgumentSemantic.Strong)]
		NSUrl StatusURL { get; }

		[Export ("name", ArgumentSemantic.Strong)]
		string Name { get; }

	}

	[BaseType (typeof(UIButton), Name = "BUYPaymentButton")]
	[DisableDefaultCtor]
	interface PaymentButton
	{
		[Static]
		[Export ("buttonWithType:style:")]
		UIButton Create (PaymentButtonType buttonType, PaymentButtonStyle buttonStyle);

	}

	[BaseType (typeof(UIPresentationController), Name = "BUYPresentationControllerForVariantSelection")]
	interface PresentationControllerForVariantSelection
	{
		[Export ("backgroundView", ArgumentSemantic.Strong)]
		UIVisualEffectView BackgroundView { get; set; }

	}

	[BaseType (typeof(UIPresentationController), Name = "BUYPresentationControllerWithNavigationController")]
	interface PresentationControllerWithNavigationController : IUIAdaptivePresentationControllerDelegate, Themeable
	{
		[NullAllowed, Export ("navigationDelegate", ArgumentSemantic.Weak)]
		INavigationControllerDelegate NavigationDelegate { get; set; }

		[Static]
		[Export ("adaptivePresentationStyle")]
		UIModalPresentationStyle AdaptivePresentationStyle { get; }

	}

	[BaseType (typeof(Object), Name = "BUYProduct")]
	interface Product
	{
		[Export ("productId", ArgumentSemantic.Copy)]
		NSNumber ProductId { get; }

		[Export ("title")]
		string Title { get; }

		[Export ("handle")]
		string ProductHandle { get; }

		[Export ("vendor")]
		string Vendor { get; }

		[Export ("productType")]
		string ProductType { get; }

		[Export ("variants", ArgumentSemantic.Copy)]
		ProductVariant[] Variants { get; }

		[Export ("images", ArgumentSemantic.Copy)]
		Image[] Images { get; }

		[Export ("options", ArgumentSemantic.Copy)]
		Option[] Options { get; }

		[Export ("htmlDescription")]
		string HtmlDescription { get; }

		[Export ("available")]
		bool Available { get; }

		[Export ("tags", ArgumentSemantic.Copy)]
		NSSet<NSString> Tags { get; }

		[Export ("published")]
		bool Published { get; }

		[Export ("createdAtDate", ArgumentSemantic.Copy)]
		NSDate CreatedAtDate { get; }

		[Export ("updatedAtDate", ArgumentSemantic.Copy)]
		NSDate UpdatedAtDate { get; }

		[Export ("publishedAtDate", ArgumentSemantic.Copy)]
		NSDate PublishedAtDate { get; }

	}

	[Category]
	[BaseType (typeof(Product), Name = "BUYProduct")]
	interface ProductOptionsExtensions
	{
		[Export ("valuesForOption:variants:")]
		OptionValue[] ValuesForOption (Option option, ProductVariant[] variants);

		[Export ("variantWithOptions:")]
		ProductVariant VariantWithOptions (OptionValue[] options);

		[Export ("isDefaultVariant")]
		bool IsDefaultVariant ();

	}

	[BaseType (typeof(UITableViewCell), Name = "BUYProductDescriptionCell")]
	interface ProductDescriptionCell : Themeable
	{
		[Export ("setDescriptionHTML:")]
		void SetDescriptionHTML (string html);

	}

	[BaseType (typeof(UITableViewCell), Name = "BUYProductHeaderCell")]
	interface ProductHeaderCell : Themeable
	{
		[Export ("setProductVariant:withCurrencyFormatter:")]
		void SetProductVariant (ProductVariant productVariant, NSNumberFormatter currencyFormatter);

	}

	[BaseType (typeof(UICollectionViewCell), Name = "BUYProductImageCollectionViewCell")]
	interface ProductImageCollectionViewCell
	{
		[Export ("productImageView", ArgumentSemantic.Strong)]
		ImageView ProductImageView { get; set; }

		[Export ("productImageViewConstraintHeight", ArgumentSemantic.Strong)]
		NSLayoutConstraint ProductImageViewConstraintHeight { get; set; }

		[Export ("productImageViewConstraintBottom", ArgumentSemantic.Strong)]
		NSLayoutConstraint ProductImageViewConstraintBottom { get; set; }

		[Export ("setContentOffset:")]
		void SetContentOffset (CGPoint offset);

	}

	[BaseType (typeof(Object), Name = "BUYProductVariant")]
	interface ProductVariant
	{
		[Export ("product", ArgumentSemantic.Strong)]
		Product Product { get; set; }

		[Export ("title")]
		string Title { get; }

		[Export ("options", ArgumentSemantic.Copy)]
		OptionValue[] Options { get; }

		[Export ("price", ArgumentSemantic.Strong)]
		NSDecimalNumber Price { get; }

		[Export ("compareAtPrice", ArgumentSemantic.Strong)]
		NSDecimalNumber CompareAtPrice { get; }

		[Export ("grams", ArgumentSemantic.Strong)]
		NSDecimalNumber Grams { get; }

		[Export ("requiresShipping", ArgumentSemantic.Strong)]
		NSNumber RequiresShipping { get; }

		[Export ("sku", ArgumentSemantic.Strong)]
		string Sku { get; }

		[Export ("taxable", ArgumentSemantic.Strong)]
		NSNumber Taxable { get; }

		[Export ("position", ArgumentSemantic.Strong)]
		NSNumber Position { get; }

		[Export ("available")]
		bool Available { get; }

		// interface BUYProductVariant_Options
		[Static]
		[Export ("filterProductVariants:forOptionValue:")]
		ProductVariant[] FilterProductVariants (ProductVariant[] productVariants, OptionValue optionValue);

	}

	[Category]
	[BaseType (typeof(ProductVariant), Name = "BUYProductVariant")]
	interface ProductVariantOptionsExtensions
	{
		[Export ("optionValueForName:")]
		OptionValue OptionValueForName (string optionName);

	}

	[BaseType (typeof(UITableViewCell), Name = "BUYProductVariantCell")]
	interface ProductVariantCell : Themeable
	{
		[Export ("setOptionsForProductVariant:")]
		void SetOptionsForProductVariant (ProductVariant productVariant);

	}

	[BaseType (typeof(UIView), Name = "BUYProductView")]
	interface ProductView
	{
		[Export ("tableView", ArgumentSemantic.Strong)]
		UITableView TableView { get; set; }

		[Export ("stickyFooterView", ArgumentSemantic.Strong)]
		UIView StickyFooterView { get; set; }

		[Export ("footerHeightLayoutConstraint", ArgumentSemantic.Strong)]
		NSLayoutConstraint FooterHeightLayoutConstraint { get; set; }

		[Export ("footerOffsetLayoutConstraint", ArgumentSemantic.Strong)]
		NSLayoutConstraint FooterOffsetLayoutConstraint { get; set; }

		[Export ("productViewHeader", ArgumentSemantic.Strong)]
		ProductViewHeader ProductViewHeader { get; set; }

		[Export ("backgroundImageView", ArgumentSemantic.Strong)]
		ProductViewHeaderBackgroundImageView BackgroundImageView { get; set; }

		[Export ("productViewFooter", ArgumentSemantic.Strong)]
		ProductViewFooter ProductViewFooter { get; set; }

		[Export ("topGradientView", ArgumentSemantic.Strong)]
		GradientView TopGradientView { get; set; }

		[NullAllowed, Export ("theme", ArgumentSemantic.Weak)]
		Theme Theme { get; set; }

		[Export ("hasSetVariantOnCollectionView")]
		bool HasSetVariantOnCollectionView { get; set; }

		[Export ("initWithFrame:product:theme:shouldShowApplePaySetup:")]
		IntPtr Constructor (CGRect rect, Product product, Theme theme, bool showApplePaySetup);

		[Export ("scrollViewDidScroll:")]
		void ScrollViewDidScroll (UIScrollView scrollView);

		[Export ("updateBackgroundImage:")]
		void UpdateBackgroundImage (Image[] images);

		[Export ("showErrorWithMessage:")]
		void ShowErrorWithMessage (string errorMessage);

		[Export ("setInsets:appendToCurrentInset:")]
		void SetInsets (UIEdgeInsets edgeInsets, bool appendToCurrentInset);

		[Export ("setTopInset:")]
		void SetTopInset (nfloat topInset);

	}

	interface IViewControllerDelegate
	{

	}

	[Protocol, Model]
	[BaseType (typeof(NSObject), Name = "BUYViewControllerDelegate")]
	interface ViewControllerDelegate
	{
		[Export ("controller:failedToCreateCheckout:")]
		void ControllerFailedToCreateCheckout (ViewController controller, NSError error);

		[Export ("controllerFailedToStartApplePayProcess:")]
		void ControllerFailedToStartApplePayProcess (ViewController controller);

		[Export ("controller:failedToUpdateCheckout:withError:")]
		void ControllerFailedToUpdateCheckout (ViewController controller, Checkout checkout, NSError error);

		[Export ("controller:failedToGetShippingRates:withError:")]
		void ControllerFailedToGetShippingRates (ViewController controller, Checkout checkout, NSError error);

		[Export ("controller:failedToCompleteCheckout:withError:")]
		void ControllerFailedToCompleteCheckout (ViewController controller, Checkout checkout, NSError error);

		[Export ("controller:didCompleteCheckout:status:")]
		void ControllerDidCompleteCheckout (ViewController controller, Checkout checkout, Status status);

		[Export ("controller:didDismissApplePayControllerWithStatus:forCheckout:")]
		void ControllerDidDismissApplePayController (ViewController controller, PKPaymentAuthorizationStatus status, Checkout checkout);

		[Export ("controller:didDismissWebCheckout:")]
		void ControllerDidDismissWebCheckout (ViewController controller, Checkout checkout);

		[Export ("didDismissViewController:")]
		void DidDismissViewController (ViewController controller);

		[Export ("controllerWillCheckoutViaWeb:")]
		void ControllerWillCheckoutViaWeb (ViewController viewController);

		[Export ("controllerWillCheckoutViaApplePay:")]
		void ControllerWillCheckoutViaApplePay (ViewController viewController);

	}

	[BaseType (typeof(UIViewController), Name = "BUYViewController")]
	[DisableDefaultCtor]
	interface ViewController : IPKPaymentAuthorizationViewControllerDelegate
	{
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IViewControllerDelegate Delegate { get; set; }

		[Export ("client", ArgumentSemantic.Strong)]
		BuyClient Client { get; set; }

		[Export ("shop", ArgumentSemantic.Strong)]
		Shop Shop { get; set; }

		[Export ("merchantId", ArgumentSemantic.Strong)]
		string MerchantId { get; set; }

		[Export ("isApplePayAvailable")]
		bool IsApplePayAvailable { get; }

		[Export ("allowApplePaySetup")]
		bool AllowApplePaySetup { get; set; }

		[Export ("canShowApplePaySetup")]
		bool CanShowApplePaySetup { get; }

		[Export ("shouldShowApplePayButton")]
		bool ShouldShowApplePayButton { get; }

		[Export ("shouldShowApplePaySetup")]
		bool ShouldShowApplePaySetup { get; }

		[Export ("checkout", ArgumentSemantic.Strong)]
		Checkout Checkout { get; }

		[Export ("loadShopWithCallback:")]
		void LoadShopWithCallback (Action<bool, NSError> block);

		[Export ("supportedNetworks", ArgumentSemantic.Copy)]
		NSString[] SupportedNetworks { get; set; }

		[Export ("paymentRequest")]
		PKPaymentRequest PaymentRequest { get; }

		[Export ("initWithClient:")]
		IntPtr Constructor (BuyClient client);

		[Export ("startApplePayCheckout:")]
		void StartApplePayCheckout (Checkout checkout);

		[Export ("startWebCheckout:")]
		void StartWebCheckout (Checkout checkout);

		[Export ("startCheckoutWithCartToken:")]
		void StartCheckoutWithCartToken (string token);

		[Export ("checkoutCompleted:status:")]
		void CheckoutCompleted (Checkout checkout, Status status);

		[Static]
		[Export ("completeCheckoutFromLaunchURL:")]
		void CompleteCheckoutFromLaunchURL (NSUrl url);

	}

	[BaseType (typeof(ViewController), Name = "BUYProductViewController")]
	interface ProductViewController : Themeable
	{
		[Export ("initWithClient:theme:")]
		IntPtr Constructor (BuyClient client, Theme theme);

		[Export ("loadProduct:completion:")]
		void LoadProduct (string productId, Action<bool, NSError> completion);

		[Export ("loadWithProduct:completion:")]
		void LoadWithProduct (Product product, Action<bool, NSError> completion);

		[Export ("productId", ArgumentSemantic.Strong)]
		string ProductId { get; }

		[Export ("product", ArgumentSemantic.Strong)]
		Product Product { get; }

		[Export ("isLoading")]
		bool IsLoading { get; }

		[Export ("presentPortraitInViewController:")]
		void PresentPortraitInViewController (UIViewController controller);

	}

	[BaseType (typeof(UIView), Name = "BUYProductViewErrorView")]
	interface ProductViewErrorView
	{
		[Export ("errorLabel", ArgumentSemantic.Strong)]
		UILabel ErrorLabel { get; set; }

		[Export ("hiddenConstraint", ArgumentSemantic.Strong)]
		NSLayoutConstraint HiddenConstraint { get; set; }

		[Export ("visibleConstraint", ArgumentSemantic.Strong)]
		NSLayoutConstraint VisibleConstraint { get; set; }

		[Export ("initWithTheme:")]
		IntPtr Constructor (Theme theme);

	}

	[BaseType (typeof(UIView), Name = "BUYProductViewFooter")]
	interface ProductViewFooter
	{
		[Export ("initWithTheme:showApplePaySetup:")]
		IntPtr Constructor (Theme theme, bool showApplePaySetup);

		[Export ("checkoutButton", ArgumentSemantic.Strong)]
		CheckoutButton CheckoutButton { get; set; }

		[Export ("buyPaymentButton", ArgumentSemantic.Strong)]
		PaymentButton BuyPaymentButton { get; set; }

		[Export ("setApplePayButtonVisible:")]
		void SetApplePayButtonVisible (bool isApplePayAvailable);

		[Export ("updateButtonsForProductVariant:")]
		void UpdateButtonsForProductVariant (ProductVariant productVariant);

	}

	[BaseType (typeof(UIView), Name = "BUYProductViewHeader")]
	interface ProductViewHeader
	{
		[Export ("collectionView", ArgumentSemantic.Strong)]
		UICollectionView CollectionView { get; set; }

		[Export ("productViewHeaderOverlay", ArgumentSemantic.Strong)]
		ProductViewHeaderOverlay ProductViewHeaderOverlay { get; set; }

		[Export ("initWithFrame:theme:")]
		IntPtr Constructor (CGRect frame, Theme theme);

		[Export ("imageHeightWithScrollViewDidScroll:")]
		nfloat ImageHeightWithScrollViewDidScroll (UIScrollView scrollView);

		[Export ("setCurrentPage:")]
		void SetCurrentPage (nint currentPage);

		[Export ("setImageForSelectedVariant:withImages:")]
		void SetImageForSelectedVariant (ProductVariant productVariant, Image[] images);

	}

	[BaseType (typeof(UIView), Name = "BUYProductViewHeaderBackgroundImageView")]
	interface ProductViewHeaderBackgroundImageView
	{
		[Export ("initWithTheme:")]
		IntPtr Constructor (Theme theme);

		[Export ("setBackgroundProductImage:")]
		void SetBackgroundProductImage (Image image);

	}

	[BaseType (typeof(UIView), Name = "BUYProductViewHeaderOverlay")]
	interface ProductViewHeaderOverlay
	{
		[Export ("initWithTheme:")]
		IntPtr Constructor (Theme theme);

		[Export ("scrollViewDidScroll:withNavigationBarHeight:")]
		void ScrollViewDidScroll (UIScrollView scrollView, nfloat navigationBarHeight);

	}

	[BaseType (typeof(Object), Name = "BUYShop")]
	interface Shop
	{
		[Export ("name")]
		string Name { get; }

		[Export ("city")]
		string City { get; }

		[Export ("province")]
		string Province { get; }

		[Export ("country")]
		string Country { get; }

		[Export ("currency")]
		string Currency { get; }

		[Export ("moneyFormat")]
		string MoneyFormat { get; }

		[Export ("domain")]
		string Domain { get; }

		[Export ("shopDescription")]
		string ShopDescription { get; }

		[Export ("shipsToCountries", ArgumentSemantic.Copy)]
		string[] ShipsToCountries { get; }

		[Export ("shopURL")]
		NSUrl ShopURL { get; }

		[Export ("myShopifyURL")]
		NSUrl MyShopifyURL { get; }

	}

	delegate void CheckoutTypeBlock (CheckoutType checkoutType);

	interface IStoreViewControllerDelegate
	{

	}

	[Protocol, Model]
	[BaseType(typeof(NSObject), Name = "BUYStoreViewControllerDelegate")]
	interface StoreViewControllerDelegate : ViewControllerDelegate
	{
		[Abstract]
		[Export ("controller:shouldProceedWithCheckoutType:")]
		void ShouldProceedWithCheckoutType (StoreViewController controller, CheckoutTypeBlock completionHandler);

	}

	[BaseType (typeof(ViewController), Name = "BUYStoreViewController")]
	interface StoreViewController
	{
		[Export ("initWithClient:url:")]
		IntPtr Constructor (BuyClient client, NSUrl url);

		[Export ("reloadHomePage")]
		void ReloadHomePage ();

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IStoreViewControllerDelegate Delegate { get; set; }

	}

	[BaseType (typeof(Object), Name = "BUYTaxLine")]
	interface TaxLine
	{
		[Export ("price", ArgumentSemantic.Strong)]
		NSDecimalNumber Price { get; set; }

		[Export ("rate", ArgumentSemantic.Strong)]
		NSDecimalNumber Rate { get; set; }

		[Export ("title")]
		string Title { get; set; }

	}

	[Category]
	[BaseType (typeof(Theme), Name = "BUYTheme")]
	interface ThemeExtensions
	{
		[Export ("backgroundColor")]
		UIColor BackgroundColor ();

		[Export ("selectedBackgroundColor")]
		UIColor SelectedBackgroundColor ();

		[Export ("separatorColor")]
		UIColor SeparatorColor ();

		[Export ("disclosureIndicatorColor")]
		UIColor DisclosureIndicatorColor ();

		[Export ("checkoutButtonTextColor")]
		UIColor CheckoutButtonTextColor ();

		[Export ("errorTintOverlayColor")]
		UIColor ErrorTintOverlayColor ();

		[Export ("navigationBarTitleColor")]
		UIColor NavigationBarTitleColor ();

		[Export ("navigationBarTitleVariantSelectionColor")]
		UIColor NavigationBarTitleVariantSelectionColor ();

		[Export ("productTitleColor")]
		UIColor ProductTitleColor ();

		[Export ("variantOptionNameTextColor")]
		UIColor VariantOptionNameTextColor ();

		[Export ("variantBreadcrumbsBackground")]
		UIColor VariantBreadcrumbsBackground ();

		[Export ("blurEffect")]
		UIBlurEffect BlurEffect ();

		[Export ("activityIndicatorViewStyle")]
		UIActivityIndicatorViewStyle ActivityIndicatorViewStyle ();

		[Export ("navigationBarStyle")]
		UIBarStyle NavigationBarStyle ();

		[Export ("paymentButtonStyle")]
		PaymentButtonStyle PaymentButtonStyle ();

	}

	// @interface BUYVariantOptionView : UIView <BUYThemeable>
	[BaseType (typeof(UIView), Name = "BUYVariantOptionView")]
	interface VariantOptionView : Themeable
	{
		// -(void)setTextForOptionValue:(BUYOptionValue *)optionValue;
		[Export ("setTextForOptionValue:")]
		void SetTextForOptionValue (OptionValue optionValue);
	}

	interface IVariantSelectionDelegate
	{

	}

	// @protocol BUYVariantSelectionDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject), Name = "BUYVariantSelectionDelegate")]
	interface VariantSelectionDelegate
	{
		// @required -(void)variantSelectionController:(BUYVariantSelectionViewController *)controller didSelectVariant:(BUYProductVariant *)variant;
		[Abstract]
		[Export ("variantSelectionController:didSelectVariant:")]
		void VariantSelectionController (VariantSelectionViewController controller, ProductVariant variant);

		// @required -(void)variantSelectionControllerDidCancelVariantSelection:(BUYVariantSelectionViewController *)controller atOptionIndex:(NSUInteger)optionIndex;
		[Abstract]
		[Export ("variantSelectionControllerDidCancelVariantSelection:atOptionIndex:")]
		void VariantSelectionControllerDidCancelVariantSelection (VariantSelectionViewController controller, nuint optionIndex);
	}

	// @interface BUYVariantSelectionViewController : UIViewController
	[BaseType (typeof(UIViewController), Name = "BUYVariantSelectionViewController")]
	interface VariantSelectionViewController
	{
		// -(instancetype)initWithProduct:(BUYProduct *)product theme:(BUYTheme *)theme;
		[Export ("initWithProduct:theme:")]
		IntPtr Constructor (Product product, Theme theme);

		// @property (readonly, nonatomic, strong) BUYProduct * product;
		[Export ("product", ArgumentSemantic.Strong)]
		Product Product { get; }

		// @property (nonatomic, strong) BUYProductVariant * selectedProductVariant;
		[Export ("selectedProductVariant", ArgumentSemantic.Strong)]
		ProductVariant SelectedProductVariant { get; set; }

		// @property (nonatomic, weak) NSNumberFormatter * _Nullable currencyFormatter;
		[NullAllowed, Export ("currencyFormatter", ArgumentSemantic.Weak)]
		NSNumberFormatter CurrencyFormatter { get; set; }

		// @property (nonatomic, weak) id<BUYVariantSelectionDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IVariantSelectionDelegate Delegate { get; set; }
	}

//	// @interface BUYAdditions (NSDate)
//	[Category]
//	[BaseType (typeof(NSDate))]
//	interface NSDate_BUYAdditions
//	{
//		// +(NSInteger)daysBetweenDate:(NSDate *)fromDateTime andDate:(NSDate *)toDateTime;
//		[Static]
//		[Export ("daysBetweenDate:andDate:")]
//		nint DaysBetweenDate (NSDate fromDateTime, NSDate toDateTime);
//	}

//	// @interface BUYAdditions (NSDateFormatter)
//	[Category]
//	[BaseType (typeof(NSDateFormatter))]
//	interface NSDateFormatter_BUYAdditions
//	{
//		// +(NSDateFormatter *)dateFormatterForShippingRates;
//		[Static]
//		[Export ("dateFormatterForShippingRates")]
//		NSDateFormatter DateFormatterForShippingRates ();
//
//		// +(NSDateFormatter *)dateFormatterForPublications;
//		[Static]
//		[Export ("dateFormatterForPublications")]
//		NSDateFormatter DateFormatterForPublications ();
//	}

//	// @interface BUYAdditions (NSDecimalNumber)
//	[Category]
//	[BaseType (typeof(NSDecimalNumber))]
//	interface NSDecimalNumber_BUYAdditions
//	{
//		// +(NSDecimalNumber *)buy_decimalNumberOrZeroWithString:(NSString *)string;
//		[Static]
//		[Export ("buy_decimalNumberOrZeroWithString:")]
//		NSDecimalNumber Buy_decimalNumberOrZeroWithString (string @string);
//
//		// +(NSDecimalNumber *)buy_decimalNumberFromJSON:(id)valueFromJSON;
//		[Static]
//		[Export ("buy_decimalNumberFromJSON:")]
//		NSDecimalNumber Buy_decimalNumberFromJSON (NSObject valueFromJSON);
//
//		// -(NSDecimalNumber *)buy_decimalNumberAsNegative;
//		[Export ("buy_decimalNumberAsNegative")]
//		NSDecimalNumber Buy_decimalNumberAsNegative ();
//	}

//	// @interface Additions (NSDictionary)
//	[Category]
//	[BaseType (typeof(NSDictionary))]
//	interface NSDictionary_Additions
//	{
//		// -(id)buy_objectForKey:(NSString *)key;
//		[Export ("buy_objectForKey:")]
//		NSObject Buy_objectForKey (string key);
//	}

//	// @interface Trim (NSString)
//	[Category]
//	[BaseType (typeof(NSString))]
//	interface NSString_Trim
//	{
//		// -(NSString *)buy_trim;
//		[Export ("buy_trim")]
//		string Buy_trim ();
//	}

//	// @interface BUYAdditions (NSURL)
//	[Category]
//	[BaseType (typeof(NSUrl))]
//	interface NSURL_BUYAdditions
//	{
//		// +(NSURL *)buy_urlWithString:(NSString *)string;
//		[Static]
//		[Export ("buy_urlWithString:")]
//		NSUrl Buy_urlWithString (string @string);
//	}

//	// @interface BUYAdditions (NSURLComponents)
//	[Category]
//	[BaseType (typeof(NSUrlComponents))]
//	interface NSURLComponents_BUYAdditions
//	{
//		// -(void)setQueryItemsWithDictionary:(NSDictionary *)namesAndValues;
//		[Export ("setQueryItemsWithDictionary:")]
//		void SetQueryItemsWithDictionary (NSDictionary namesAndValues);
//	}

//	// @interface BUYAdditions (UIFont)
//	[Category]
//	[BaseType (typeof(UIFont))]
//	interface UIFont_BUYAdditions
//	{
//		// +(UIFont *)preferredFontForTextStyle:(NSString *)style increasedPointSize:(CGFloat)size;
//		[Static]
//		[Export ("preferredFontForTextStyle:increasedPointSize:")]
//		UIFont PreferredFontForTextStyle (string style, nfloat size);
//	}
}

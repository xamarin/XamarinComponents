# Getting Started with Shopify Mobile Buy SDK for Xamarin.iOS

## Adding the Mobile App sales channel

Before you can get started with the iOS Buy SDK outside of Shopify, 
you'll need to enable the Mobile App sales channel in your Shopify admin. 
 
The Mobile App sales channel page contains your:

 - API Key
 - App ID
 - Channel ID

To add the channel:

 1. From your Shopify admin, click **Settings**, and then click **Sales Channels**.
 2. Under **Installed sales channels**, click **Add a new sales channel**.
 3. On the **Add sales channel** dialog, find **Mobile App**, and then click **Learn more**.
 4. In the dialog window that opens, click **Add channel**. This brings you to the
    **Integration** page.

The **Integration** page is where you'll find your API Key. After the mobile 
app has launched, you can review data that is specific to the Mobile App's 
sales from your Shopify Home.

## Initializing the Buy client

```csharp
string ShopDomain = "myshopexample.myshopify.com";
string ApiKey = "1234567890abcdef1234567890abcdef";
string ChannelId = "12345678";

BuyClient myClient = new BuyClient (ShopDomain, ApiKey, ChannelId);
```

## Presenting a Storefront

The Buy SDK provides access to shop data that a mobile app can use to present a 
native mobile storefront. This includes details about the shop itself, the 
product catalog, and customer accounts. 

### Getting the Shop

The shop, represented by the `Shop` class, includes useful information that describes the 
shop, including the mailing address, web address, and currency. The currency information 
is essential for correctly displaying product prices. The shop does not change often. It 
is reasonable to fetch it once per user session, and cache it—or just the properties you 
need—somewhere easy to access. Commonly, you might store it in your application delegate, 
and pass a reference along to other objects that need it.

```csharp
Shop myShop = await myClient.GetShopAsync();
```

### Getting Collections

Collections and products are provided in paginated lists. The page size is configurable 
using the pageSize property of `BuyClient`. Pages are numbered starting at 1. To get a 
partial list of collections, provide the desired page number:

```csharp
var result = await myClient.GetCollectionsPageAsync ();
Collection[] myCollections = result.Collections;
```

The Collection entity defines two relationships: image and products. The URL to the image 
is stored in the string `SourceUrl` property. You must make a separate request for the 
image assigned to a collection using this URL. 

### Getting Products

Products returned from the API include all of their subordinate objects, including variants, 
options, and option values.

To get a partial list of products, use `GetProductsPageAsync` or `GetProductsPage`.

```csharp
var result = await myClient.GetProductsPageAsync (1);
Product[] myProducts = result.Products;
```

To get products in a single collection, provide the collection identifier for the 
collection. To get a product or products using their identifiers, invoke `GetProductAsync` 
or `GetProduct`.

## Performing a Checkout

The checkout process requires you to create a Checkout object with all the necessary 
components:

 - Line items
 - Billing address
 - Shipping address
 - Shipping charges
 - Discount, if applicable
 - Payment details

Once all of that information has been gathered, complete the checkout to create an order.

### Building a Cart

`Cart` consists of one or more line items from product variants retrieved in the product 
list.

```csharp
// create a cart
Cart myCart = new Cart ();

// add a product variant to the cart
Product product = myProducts [0];
myCart.AddVariant (product.Variants [0]);
```

### Creating a Checkout

When the customer is ready to make a purchase, a `Checkout` must be created from 
the `Cart` object.

```csharp
// create the checkout
Checkout myCheckout = new Checkout (myCart);

// sync the checkout with Shopify
myCheckout = await myClient.CreateCheckoutAsync (myCheckout);
```

### Preparing the Checkout

Adding the shipping address:

```csharp
// create the address
Address myAddress = new Address {
    Address1 = "150 Elgin Street",
    Address2 = "8th Floor",
    City = "Ottawa",
    Company = "Shopify Inc.",
    FirstName = "Egon",
    LastName = "Spengler",
    Phone = "1-555-555-5555",
    CountryCode = "CA",
    ProvinceCode = "ON",
    Zip = "K1N5T5"
};

// set the addresses
myCheckout.ShippingAddress = myAddress;
myCheckout.BillingAddress = myAddress;

// set an email
myCheckout.Email = "me@mail.com";

// update the checkout
myCheckout = await client.UpdateCheckoutAsync (myCheckout);
```

Once we have set the addresses, we can add the shipping rates:

```csharp
// make sure if we need to add shipping
if (checkout.RequiresShipping)
{
    // get the shipping rates
    var result = await client.GetShippingRatesForCheckoutAsync (myCheckout);
    ShippingRates[] myShippingRates = result.ShippingRates;
    
    if (myShippingRates.Length > 0) {
        // add the shipping
        myCheckout.ShippingRate = myShippingRates [0];
        
        // update the checkout
        myCheckout = await client.UpdateCheckoutAsync (myCheckout);
    }
}
```

We can optionally add discount codes or gift vouchers:

```csharp
// discounts
myCheckout.Discount = new Discount ("DISCOUNT-CODE");

// gift codes
myCheckout = await myClient.ApplyGiftCardAsync ("GIFT-CARD", myCheckout);

// update the checkout
myCheckout = await myClient.UpdateCheckoutAsync (myCheckout);
```

### Completing the Checkout

At this point you are ready to collect the payment information from the customer. If 
using Apple Pay, keep a reference to the `PKPaymentToken`. If using a credit card payment, 
prompt the customer to enter the credit card information.

To get started with Apple Pay with Shopify, check out the https://help.shopify.com/api/sdks/mobile-buy-sdk/ios/enable-apple-pay 

Although the SDK supports payment by credit card, we highly recommend that you use Apple 
Pay as the only source of payment within your app. 

The are three ways to complete a checkout:

 - Native
 - Web
 - Apple Pay

#### Native Checkout

Now that the checkout is complete, we can charge the credit card:

```csharp
// create the card
CreditCard myCreditCard = new CreditCard {
    Number = "4242424242424242",
    ExpiryMonth = "12",
    ExpiryYear = "2020",
    Cvv = "123",
    NameOnCard = "John Smith",
};

// update the checkout
var result = await myClient.StoreCreditCardAsync (myCreditCard, myCheckout);
myCheckout = result.Checkout;

// charge the card
myCheckout = await myClient.CompleteCheckoutAsync (myCheckout);

// get the final checkout
if (await myClient.GetCompletionStatusOfCheckoutAsync (myCheckout) == Status.Complete)) {
    myCheckout = await myClient.GetCheckoutAsync (myCheckout);
}
```

#### Web Checkout

In addition to the native checkout, we can transition to th web browser-based checkout.
First we implement the `ISFSafariViewControllerDelegate` interface:

```csharp
[Export ("safariViewControllerDidFinish:")]
public async virtual void SafariViewControllerDidFinish (SFSafariViewController controller)
{
    myCheckout = await myClient.GetCheckoutAsync (myCheckout);
}
```

Then, we can create and present the Safari view controller:

```csharp
var safariViewController = new SFSafariViewController (myCheckout.WebCheckoutUrl);
safariViewController.Delegate = this;
PresentViewController (safariViewController, true, null);
```

#### Apple Pay Checkout

First we create the payment request:

```csharp
const string MerchantId = "merchant.com.mycompany.myshopexample";

var myPaymentRequest = new PKPaymentRequest {
    MerchantIdentifier = MerchantId,
    RequiredBillingAddressFields = PKAddressField.All,
    RequiredShippingAddressFields = myCheckout.RequiresShipping ? 
        PKAddressField.All : 
        PKAddressField.Email | PKAddressField.Phone,
    SupportedNetworks = new[] {
	    PKPaymentNetwork.Visa,
	    PKPaymentNetwork.MasterCard
    },
    MerchantCapabilities = PKMerchantCapability.ThreeDS,
    CountryCode = myShop.Country,
    CurrencyCode = myShop.Currency,
    PaymentSummaryItems = myCheckout.ApplePaySummaryItems (myShop.Name)
};
```

Next, we use the `ApplePayHelpers` to start the payment: 

```csharp
ApplePayHelpers myApplePayHelper = new ApplePayHelpers (myClient, myCheckout, myShop);

// Add additional methods if needed and forward the callback to ApplePayHelpers
PKPaymentAuthorizationViewController myPaymentController = new PKPaymentAuthorizationViewController (myPaymentRequest);
// add the events, and then forward it to the helpers
myPaymentController.PaymentAuthorizationViewControllerDidFinish += (_, args) => {
    myApplePayHelper.PaymentAuthorizationViewControllerDidFinish (myPaymentController);
};
myPaymentController.DidSelectShippingAddress += (_, args) => {
    myApplePayHelper.DidSelectShippingAddress (myPaymentController, args.Address, args.Completion);
};
myPaymentController.DidSelectShippingContact += (_, args) => {
    myApplePayHelper.DidSelectShippingContact (myPaymentController, args.Contact, args.Completion);
};
myPaymentController.DidSelectShippingMethod += (_, args) => {
    myApplePayHelper.DidSelectShippingMethod (myPaymentController, args.ShippingMethod, args.Completion);
};
// add the final step event
myPaymentController.DidAuthorizePayment += async (_, args) => {
    // forward the event
    myApplePayHelper.DidAuthorizePayment (myPaymentController, args.Payment, args.Completion);

    // continue processing the result
    myCheckout = myApplePayHelper.Checkout;
    myCheckout = await myClient.GetCheckoutAsync (myCheckout);
};
PresentViewController (myPaymentController, true, null);
```

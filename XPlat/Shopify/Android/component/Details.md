# Shopify Mobile Buy SDK for Xamarin.Android

## Adding the Mobile App sales channel

Before you can get started with the Android Buy SDK outside of Shopify, 
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
const string ShopDomain = "myshopexample.myshopify.com";
const string ApiKey = "1234567890abcdef1234567890abcdef";
const string ChannelId = "12345678";
const string AndroidPackageName = "com.mycompany.myshop";

BuyClient myBuyClient = BuyClientFactory.GetBuyClient (ShopDomain, ApiKey, ChannelId, AndroidPackageName);
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
Shop myShop = await myClient.GetShopAsync ();
```

### Getting Collections

Collections and products are provided in paginated lists. The page size is configurable 
using the `SetPageSize` method of `BuyClient`. Pages are numbered starting at 1. To get a 
partial list of collections, provide the desired page number:

```csharp
IEnumerable<Collection> myCollections = await myClient.GetCollectionPageAsync (1);
```

The Collection entity defines two relationships: image and products. The URL to the image 
is stored in the string `SourceUrl` property. You must make a separate request for the 
image assigned to a collection using this URL. 

### Getting Products

Products returned from the API include all of their subordinate objects, including variants, 
options, and option values.

```csharp
IEnumerable<Product> myProducts = await myClient.GetProductPageAsync (1);
```

To get products in a single collection, provide the collection identifier for the 
collection. To get a product or products using their identifiers, invoke `GetProductAsync` 
or `GetProductsAsync`.

## Performing a Checkout

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

Check out the Getting Started guide for more information on completing a checkout. 

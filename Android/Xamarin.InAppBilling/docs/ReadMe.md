#Xamarin.InAppBilling API Documentation

####Version 02.02.00

##Table of Contents

* [Summary](#Summary)
* [Remarks](#Remarks)
* [Namespaces](#Namespaces)
* [Types](#Types)
* [Fields](#Fields)
* [Properties](#Properties)
* [Methods](#Methods)
* [Events](#Events)

<a name="Summary"></a>
##Summary

In-app Billing is a Google Play service that lets you sell a wide range digital content such as new game levels, downloadable media, or premium features directly from inside your Xamarin.Android mobile application. These purchases can either be standard, one-time billed products or recurring, automatically billed subscriptions.

The `Xamarin.InAppBilling` component simplifies the process of supporting [In-App Billing (Version 3 API)](http://developer.android.com/google/play/billing/api.html) by reducing the amount of common, repetitive code required and by adding several useful, helper functions.

###Features###

`Xamarin.InAppBilling` supports the following features:

* **Connect** - Attaches your Xamarin.Android app to Google Play to process In-App Purchases.
* **QueryInventoryAsync** - Given a list of product IDs, this routine will return all available products matching those IDs not already purchased by the current user.
* **GetPurchases** - Returns a list of all purchases made by the current user.
* **BuyProduct** - Calls Google Play to purchase the given product.
* **ConsumePurchase** - Given a purchased product, this routine informs Google Play that the given product has been consumed and is available for purchase again.
* **Disconnect** - Unbinds from Google Play when your Activity ends.

###Events###

`Xamarin.InAppBilling` defines the following events that you can monitor and respond to:

* **OnConnected** - Raised when the component attaches to Google Play.
* **OnDisconnected** - Raised when the component detaches from Google Play.
* **OnInAppBillingError** - Raised when an error occurs inside the component.
* **OnProductPurchasedError** - Raised when there is an error purchasing a product or subscription.
* **OnProductPurchase** - Raised when a product is successfully purchased.
* **OnPurchaseConsumedError** - Raised when there is an error consuming a purchase.
* **OnPurchaseConsumed** - Raised when a purchase is successfully consumed.

###Secure Transactions###

When developing Xamarin.Android applications that support In-App Billing there are several steps that must be taken to protect your app from being hacked by a malicious user and keep unlocked content safe (See Google's [Security and Design](http://developer.android.com/google/play/billing/billing_best_practices.html) for more details).

While the best practice is to perform signature verification on a remote server and not on a device, this might not always be possible. Another technique is to obfuscate your Google Play public key and never store the assembled key in memory. 

`Xamarin.InAppBilling` provides a quick and simple method to break your public key into a several pieces and to obfuscate those pieces. Once a public key has been provided to `Xamarin.InAppBilling` it is never stored as plain text, it is always encrypted in memory.

<a name="Remarks"></a>
##Remarks

Here is an example of adding In-App Billing to an Android **Activity** using `Xamarin.InAppBilling`:

```
using Xamarin.InAppBilling;
using Xamarin.InAppBilling.Utilities;
...

// Create a new connection to the Google Play Service
_serviceConnection = new InAppBillingServiceConnection (this, "<public-key>");
_serviceConnection.OnConnected += () => `
				
	// Load inventory or available products
	GetInventory();

	// Load any items already purchased
	LoadPurchasedItems();
`;

// Attempt to connect to the service
_serviceConnection.Connect ();
...

private async Task GetInventory () `
		ReservedTestProductIDs.Purchased,
		ReservedTestProductIDs.Canceled,
		ReservedTestProductIDs.Refunded,
		ReservedTestProductIDs.Unavailable
	`, ItemType.Product);

	// Were any products returned?
	if (_products == null) `
		// No, abort
		return;
	`
	...

...

private void LoadPurchasedItems () `
	// Ask the open connection's billing handler to get any purchases
	var purchases = _serviceConnection.BillingHandler.GetPurchases (ItemType.Product);

	// Display any existing purchases
	...
`
...

// Configure buy button
_buyButton.Click += (sender, e) => `
	// Ask the open connection's billing handler to purchase the selected product
	_serviceConnection.BillingHandler.BuyProduct(_selectedProduct);
`;

```

<a name="Namespaces"></a>
#Namespaces


---

<a name="e2d55e0f-ba0c-46ac-823e-988a232f432d"></a>
##Com.Android.Vending.Billing

<p><table style='width:100%'><tr><th style='width:25%'>Type</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#2c2de562-b76b-4126-8476-c2b22a4ccde1'>IInAppBillingService</a></td><td style='width:75%' ><p>The public <code>IInAppBillingService</code> interface inherits from the <code>` class and is defined in the</code>Com.Android.Vending.Billing` namespace. It defines no fields, no properties, 5 methods and no events.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#e452919a-6bfb-4cf1-b5e6-88131704b783'>IInAppBillingServiceStub</a></td><td style='width:75%' class='def'><p>The public <code>IInAppBillingServiceStub</code> class inherits from the <code>Android.OS.Binder</code> class and is defined in the <code>Com.Android.Vending.Billing</code> namespace. It defines 6 fields, no properties, 9 methods and no events.</p>
</td></tr></table></p>


---

<a name="d902b774-7a8e-40de-b748-f6186e8d30df"></a>
##System.Security.Cryptography

<p><table style='width:100%'><tr><th style='width:25%'>Type</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#7b127d1b-7daa-4f26-b23a-5e7236a4af07'>Crypto</a></td><td style='width:75%' ><p>The <a href="#7b127d1b-7daa-4f26-b23a-5e7236a4af07">Crypto</a> provides an easy way encrypt and decrypt data using a simple password.</p>
</td></tr></table></p>


---

<a name="ab56f628-3601-4cd6-b1e2-74873a9b2ed8"></a>
##Xamarin.InAppBilling

<p><table style='width:100%'><tr><th style='width:25%'>Type</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#3f017bc8-3b0a-4dfa-9fe4-13f5c185b2e9'>Billing</a></td><td style='width:75%' ><p>Returns information about the Google Play In-App Billing API used in the <code>Xamarin.Android.InAppBilling</code> component.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#ef5b6352-1a73-4ee5-a808-a03384c31044'>BillingResult</a></td><td style='width:75%' class='def'><p>Avalible response codes returned by methods from the <code>InAppBillingService</code> functions that are part of Google Play In-App Billing.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#9b4f25fc-45a1-4e40-900d-3c3aa0998f95'>IInAppBillingHandler</a></td><td style='width:75%' ><p>Defines the interfance that all InAppBillingHandlers used with Google Play In-App Billing need to support.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#88311aab-37d5-4381-95ce-c4aa0303ac7f'>IJsonSerializerStrategy</a></td><td style='width:75%' class='def'><p>The private <code>IJsonSerializerStrategy</code> interface inherits from the <code>` class and is defined in the</code>Xamarin.InAppBilling` namespace. It defines no fields, no properties, 2 methods and no events.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#4d25d02c-abf5-434f-870c-5e52e68701aa'>InAppBillingErrorType</a></td><td style='width:75%' ><p>Defines the types of errors that con be returned from a <a href="#80c1612e-cf5e-41b3-a216-ca89246674e9">InAppBillingServiceConnection</a> when processing billing requests.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#789accaa-919d-4249-924f-e54076856b08'>InAppBillingHandler</a></td><td style='width:75%' class='def'><p>The <a href="#789accaa-919d-4249-924f-e54076856b08">InAppBillingHandler</a> is a helper class that handles communication with the Google Play services to provide support for getting a list of available products, buying a product, consuming a product, and getting a list of already owned products.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#80c1612e-cf5e-41b3-a216-ca89246674e9'>InAppBillingServiceConnection</a></td><td style='width:75%' ><p>The <a href="#80c1612e-cf5e-41b3-a216-ca89246674e9">InAppBillingServiceConnection</a> class binds your <code>Activity</code> to the Google Play’s In-app Billing  service to send In-app Billing requests to Google Play from your application. As part of the setup process,  the <a href="#80c1612e-cf5e-41b3-a216-ca89246674e9">InAppBillingServiceConnection</a> also checks if the In-app Billing Version 3 API is supported by Google Play.  If the API version is not supported, or if an error occured while establishing the service binding, the listener is notified and passed an error message.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#cb80eb77-1e36-46b1-9262-60453abc97dc'>ItemType</a></td><td style='width:75%' class='def'><p>Defines the types of items that can be purchased using Google Play In-App Billing.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#fc61b8f7-b351-48f2-9773-888e050fffcb'>JsonArray</a></td><td style='width:75%' ><p>Represents the json array.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#4db2613b-bdf6-4cff-a6da-804b56455cbe'>JsonObject</a></td><td style='width:75%' class='def'><p>Represents the json object.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#1ac2d0dc-01c8-489d-9039-4374c33d0124'>PocoJsonSerializerStrategy</a></td><td style='width:75%' ><p>The private <code>PocoJsonSerializerStrategy</code> class inherits from the <code>System.Object</code> class and is defined in the <code>Xamarin.InAppBilling</code> namespace. It defines 6 fields, no properties, 11 methods and no events.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#98d67a2a-c250-4548-8a90-9e3722fef483'>Product</a></td><td style='width:75%' class='def'><p>Holds all information about an In-App Billing product available on the Google Play store.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#03489ba4-1d9d-4430-8e09-b3bff9875fac'>Purchase</a></td><td style='width:75%' ><p>Holds all information about a product purchased from Google Play for the current user.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#a529bb4b-0d3b-4e89-8013-793e0948fe6e'>ReservedTestProductIDs</a></td><td style='width:75%' class='def'><p>Contains the reserved product IDs use to test In-App Billing via Google Play without actually making a purchase. To test your implementation with static responses, you make an In-app Billing request using  a special item that has a reserved product ID. Each reserved product ID returns a specific static response  from Google Play. No money is transferred when you make In-app Billing requests with the reserved product IDs. Also, you cannot specify the form of payment when you make a billing request with a reserved product ID.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#5d7b3738-d0ca-47aa-bf21-6d4aa1b59051'>Resource</a></td><td style='width:75%' ><p>The public <code>Resource</code> class inherits from the <code>System.Object</code> class and is defined in the <code>Xamarin.InAppBilling</code> namespace. It defines no fields, no properties, 2 methods and no events.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#6e46a1cf-089e-4410-842b-cef6050c45c7'>Response</a></td><td style='width:75%' class='def'><p>List of response codes available within the Google Play In-App Billing API.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f'>SimpleJson</a></td><td style='width:75%' ><p>This class encodes and decodes JSON strings. Spec. details, see http://www.json.org/ JSON uses Arrays and Objects. These correspond here to the datatypes JsonArray(IList&lt;object&gt;) and JsonObject(IDictionary&lt;string,object&gt;). All numbers are parsed to doubles.</p>
</td></tr></table></p>


---

<a name="7789afcb-6894-431d-8675-dd6bd066b6b8"></a>
##Xamarin.InAppBilling.InAppBillingHandler

<p><table style='width:100%'><tr><th style='width:25%'>Type</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#f9e8ef42-cdf1-4484-81d8-5a7e438e4294'>BuyProductErrorDelegate</a></td><td style='width:75%' ><p>Occurs when the user attempts to buy a product and there is an error.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#7b5d0609-2f0c-4def-b7a4-27677dc3798b'>InAppBillingProcessingErrorDelegate</a></td><td style='width:75%' class='def'><p>Occurs when there is an in app billing procesing error.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#d6aede4f-36f7-4c26-8e11-e25f23ee3c95'>OnGetProductsErrorDelegate</a></td><td style='width:75%' ><p>Raised where there is an error getting previously purchased products from the Google Play Services.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#8758fba9-3d12-446b-ab23-b71e32b92354'>OnInvalidOwnedItemsBundleReturnedDelegate</a></td><td style='width:75%' class='def'><p>Raised when Google Play Services returns an invalid bundle from previously purchased items</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#2ed54e69-6056-4702-86c8-ae945939f92b'>OnProductPurchasedDelegate</a></td><td style='width:75%' ><p>Occurs after a product has been successfully purchased Google Play.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#09a88fb4-2a98-4e4b-a2c6-aa7749c450cc'>OnProductPurchaseErrorDelegate</a></td><td style='width:75%' class='def'><p>Occurs when the is an error on a product purchase attempt.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#fd7ef9f9-bb27-4eff-a319-a3193670f137'>OnPurchaseConsumedDelegate</a></td><td style='width:75%' ><p>Occurs when on product consumed.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#db1cca38-9f7c-4d1a-97c3-e9da2b715763'>OnPurchaseConsumedErrorDelegate</a></td><td style='width:75%' class='def'><p>Occurs when there is an error consuming a product.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#e34de5bc-5059-4b1c-820c-c3a2363b472f'>OnPurchaseFailedValidationDelegate</a></td><td style='width:75%' ><p>Occurs when a previously purchased product fails to validate.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#8e757b02-94e2-431f-bfe3-56483c436f0e'>OnUserCanceledDelegate</a></td><td style='width:75%' class='def'><p>Occurs when the user cancels an In App Billing purchase.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#2debbb37-b92f-4714-92c2-47510bad06c2'>QueryInventoryErrorDelegate</a></td><td style='width:75%' ><p>Occurs when there is an error querying inventory from Google Play Services.</p>
</td></tr></table></p>


---

<a name="3bdc2924-4b86-4011-b92d-0c7e46427cd0"></a>
##Xamarin.InAppBilling.InAppBillingServiceConnection

<p><table style='width:100%'><tr><th style='width:25%'>Type</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#f81adae0-3a0b-42a2-a3cf-1136a9a6c0c4'>OnConnectedDelegate</a></td><td style='width:75%' ><p>Occurs when on connected.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#6187252d-c6be-4eb8-8bac-62d7efeeca97'>OnDisconnectedDelegate</a></td><td style='width:75%' class='def'><p>Occurs when on disconnected.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#b2fa7c05-cb97-400a-a254-a2f0e67a90f2'>OnInAppBillingErrorDelegate</a></td><td style='width:75%' ><p>Occurs when on in app billing error.</p>
</td></tr></table></p>


---

<a name="28604adb-bac9-4422-8e59-f8f58e49b978"></a>
##Xamarin.InAppBilling.Reflection

<p><table style='width:100%'><tr><th style='width:25%'>Type</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#6e848a6a-ea7d-4af0-b023-cd70a7e96d45'>ReflectionUtils</a></td><td style='width:75%' ><p>The private <code>ReflectionUtils</code> class inherits from the <code>System.Object</code> class and is defined in the <code>Xamarin.InAppBilling.Reflection</code> namespace. It defines one field, no properties, 39 methods and no events.</p>
</td></tr></table></p>


---

<a name="809cf835-9470-46b0-9c02-adb4bd944057"></a>
##Xamarin.InAppBilling.Utilities

<p><table style='width:100%'><tr><th style='width:25%'>Type</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#9cf0d2a4-88c3-454f-98a0-7517c246be94'>Extensions</a></td><td style='width:75%' ><p>Adds extension helper methods to several built in classes used for handling In-App Billing with Google Play.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#4e163874-6eb3-4f60-8c44-9e0260d939fb'>Logger</a></td><td style='width:75%' class='def'><p>Helper class to support logging within the In-App Purchases routines</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#9e6b0e98-9aae-449f-a739-3b32774c3e76'>Security</a></td><td style='width:75%' ><p>Utility class to support secure transactions for Google Play In-App Billing</p>
</td></tr></table></p>

<a name="Types"></a>
#Types


---

<a name="3f017bc8-3b0a-4dfa-9fe4-13f5c185b2e9"></a>
##Public Static Class Billing

###Namespace

[Xamarin.InAppBilling](#ab56f628-3601-4cd6-b1e2-74873a9b2ed8)

###Inherits From

`System.Object`

###Summary

Returns information about the Google Play In-App Billing API used in the `Xamarin.Android.InAppBilling` component.
<p><table style='width:100%'><tr><th style='width:25%'>Fields</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#63c38554-d7e7-46f0-949b-51b82fe250a2'>APIVersion</a></td><td style='width:75%' ><p>Gets the API version.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#6b179f85-9d91-45dd-b86b-bc8d0332817b'>ItemIdList</a></td><td style='width:75%' class='def'><p>Gets the item identifier list.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#c1efd7b6-1eaa-4584-b5e6-b063f95bc09f'>SkuDetailsList</a></td><td style='width:75%' ><p>Gets the sku details list.</p>
</td></tr></table></p>


---

<a name="ef5b6352-1a73-4ee5-a808-a03384c31044"></a>
##Public Static Class BillingResult

###Namespace

[Xamarin.InAppBilling](#ab56f628-3601-4cd6-b1e2-74873a9b2ed8)

###Inherits From

`System.Object`

###Summary

Avalible response codes returned by methods from the `InAppBillingService` functions that are part of Google Play In-App Billing.
<p><table style='width:100%'><tr><th style='width:25%'>Fields</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#f0729a0d-19bc-4c6f-a57c-e6cec26b23fc'>BillingUnavailable</a></td><td style='width:75%' ><p>In-App Billing is not supported on the given Android device.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#4d7c484d-d3d7-48b2-a724-872b99217444'>DeveloperError</a></td><td style='width:75%' class='def'><p>An invalid argument has been passed to the API or the app was not correctly signed, properly set up for In-app Billing in Google Play Dashboard, or does not have the necessary permissions in its manifest.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#0ce665f3-7763-4068-88df-3ddeb4698e4d'>Error</a></td><td style='width:75%' ><p>A fatal error occurred during an API action.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#b2559685-8241-41a9-abdd-61ff3e8bf6e8'>ItemAlreadyOwned</a></td><td style='width:75%' class='def'><p>The user already owns the given item.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#dcd61a06-70a3-42de-8039-afada9b728fc'>ItemNotOwned</a></td><td style='width:75%' ><p>The given item has not been purchased by the user.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#494866fb-aed3-4c6f-8bb5-7c6ca825853b'>ItemUnavailable</a></td><td style='width:75%' class='def'><p>The requested item is unavailable for purchase.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#750277ee-52c3-4dd1-bccd-fd2063c539c3'>OK</a></td><td style='width:75%' ><p>The transaction completed successfully.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#e7d79702-9549-4279-9963-b3955c443ec8'>ServiceUnavailable</a></td><td style='width:75%' class='def'><p>Network connection is down.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#04f71f6a-fddc-4281-a019-b6490248d893'>UserCancelled</a></td><td style='width:75%' ><p>The user canceled the transaction.</p>
</td></tr></table></p>

###Remarks

`BillingResult` was represented as a sealed class with computed properties instead of an enum to better work with the Google Play InAppBillingService interface and Json.net.

---

<a name="f9e8ef42-cdf1-4484-81d8-5a7e438e4294"></a>
##BuyProductErrorDelegate

###Namespace

[Xamarin.InAppBilling.InAppBillingHandler](#7789afcb-6894-431d-8675-dd6bd066b6b8)

###Summary

Occurs when the user attempts to buy a product and there is an error.

---

<a name="7b127d1b-7daa-4f26-b23a-5e7236a4af07"></a>
##Private Static Class Crypto

###Namespace

[System.Security.Cryptography](#d902b774-7a8e-40de-b748-f6186e8d30df)

###Inherits From

`System.Object`

###Summary

The [Crypto](#7b127d1b-7daa-4f26-b23a-5e7236a4af07) provides an easy way encrypt and decrypt data using a simple password.
<p><table style='width:100%'><tr><th style='width:25%'>Fields</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#b45157bb-f222-4b9d-af9a-ec27edc0bd2e'>salt</a></td><td style='width:75%' ><p>The private static <code>salt</code> field of the <code>Crypto</code> class holds a <code>System.Byte[]</code> value.</p>
</td></tr></table></p>

<p><table style='width:100%'><tr><th style='width:25%'>Methods</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#cac96627-af46-45e9-a32b-aac412de5ca1'>Crypto</a></td><td style='width:75%' ><p>The private static <code>Crypto ()</code> constructor for the <code>Crypto</code> class.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#185f0384-9ac9-424d-b6fc-0e7af62eea64'>Decrypt</a></td><td style='width:75%' class='def'><p>Takes the given encrypted text string and decrypts it using the given password</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#0ac1600d-8aa4-43e1-98d3-e1e03776dec0'>Encrypt</a></td><td style='width:75%' ><p>Takes the given text string and encrypts it using the given password.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#bc6a49f4-912f-4345-856f-fded9a07b48d'>GetAlgorithm</a></td><td style='width:75%' class='def'><p>Defines a RijndaelManaged algorithm and sets its key and Initialization Vector (IV)  values based on the encryptionPassword received.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#ebafe101-416d-4d1b-af9c-b8b26c20c85f'>InMemoryCrypt</a></td><td style='width:75%' ><p>Performs an in-memory encrypt/decrypt transformation on a byte array.</p>
</td></tr></table></p>

###Remarks

Code based on the book "C# 3.0 in a nutshell by Joseph Albahari" (pages 630-632) and from this StackOverflow post by somebody called Brett http://stackoverflow.com/questions/202011/encrypt-decrypt-string-in-net/2791259#2791259

---

<a name="9cf0d2a4-88c3-454f-98a0-7517c246be94"></a>
##Public Static Class Extensions

###Namespace

[Xamarin.InAppBilling.Utilities](#809cf835-9470-46b0-9c02-adb4bd944057)

###Inherits From

`System.Object`

###Summary

Adds extension helper methods to several built in classes used for handling In-App Billing with Google Play.
<p><table style='width:100%'><tr><th style='width:25%'>Methods</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#8a8c090b-3e5b-4558-977e-b71b9ddc3f13'>GetReponseCodeFromIntent</a></td><td style='width:75%' ><p>Gets the reponse code from intent.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#4f60ac02-b79f-4661-a719-24bf2e8cba12'>GetResponseCodeFromBundle</a></td><td style='width:75%' class='def'><p>Gets the response code from bundle.</p>
</td></tr></table></p>


---

<a name="9b4f25fc-45a1-4e40-900d-3c3aa0998f95"></a>
##Public Interface IInAppBillingHandler

###Namespace

[Xamarin.InAppBilling](#ab56f628-3601-4cd6-b1e2-74873a9b2ed8)

###Summary

Defines the interfance that all InAppBillingHandlers used with Google Play In-App Billing need to support.
<p><table style='width:100%'><tr><th style='width:25%'>Methods</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#702667b2-c59b-4585-a151-b487437389d0'>BuyProduct</a></td><td style='width:75%' ><p>Buys an item.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#6fc40d53-813f-46da-b068-0b1ae45b466a'>BuyProduct</a></td><td style='width:75%' class='def'><p>Buys an items</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#67efc742-c297-4e1e-90ae-b96780e84f2b'>ConsumePurchase</a></td><td style='width:75%' ><p>Consumes the purchased item</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#0a15eb2e-2d49-446a-b4e1-31d0cec4575d'>ConsumePurchase</a></td><td style='width:75%' class='def'><p>Consumes the purchased item</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#f26a57e5-e2b0-4b61-8ee9-ea10306911b4'>GetPurchases</a></td><td style='width:75%' ><p>Gets the purchases.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#aab21c53-a710-4f46-89eb-24c728c8ccc7'>HandleActivityResult</a></td><td style='width:75%' class='def'><p>Handles the activity result.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#7daf36e3-87e5-4dec-85ac-c5871a4a5d5e'>QueryInventoryAsync</a></td><td style='width:75%' ><p>Queries the inventory asynchronously.</p>
</td></tr></table></p>


---

<a name="2c2de562-b76b-4126-8476-c2b22a4ccde1"></a>
##Public Interface IInAppBillingService

###Namespace

[Com.Android.Vending.Billing](#e2d55e0f-ba0c-46ac-823e-988a232f432d)

###Summary

The public `IInAppBillingService` interface inherits from the `` class and is defined in the `Com.Android.Vending.Billing` namespace. It defines no fields, no properties, 5 methods and no events.
<p><table style='width:100%'><tr><th style='width:25%'>Methods</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#fd8c3a06-36dd-4a7e-8aa1-b67dd854e08c'>ConsumePurchase</a></td><td style='width:75%' ><p>The public virtual <code>ConsumePurchase (System.Int32, System.String, System.String)</code> member of the <code>IInAppBillingService</code> interface returns a <code>System.Int32</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#7a0eac28-3746-4e85-975f-82192553abd9'>GetBuyIntent</a></td><td style='width:75%' class='def'><p>The public virtual <code>GetBuyIntent (System.Int32, System.String, System.String, System.String, System.String)</code> member of the <code>IInAppBillingService</code> interface returns a <code>Android.OS.Bundle</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#34963404-7e55-4a28-8220-f26715b02c51'>GetPurchases</a></td><td style='width:75%' ><p>The public virtual <code>GetPurchases (System.Int32, System.String, System.String, System.String)</code> member of the <code>IInAppBillingService</code> interface returns a <code>Android.OS.Bundle</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#1c1136ea-30b9-458a-a7fd-c520b7a03c08'>GetSkuDetails</a></td><td style='width:75%' class='def'><p>The public virtual <code>GetSkuDetails (System.Int32, System.String, System.String, Android.OS.Bundle)</code> member of the <code>IInAppBillingService</code> interface returns a <code>Android.OS.Bundle</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#6a62d15d-e891-4948-904a-e2bc2f31a6a6'>IsBillingSupported</a></td><td style='width:75%' ><p>The public virtual <code>IsBillingSupported (System.Int32, System.String, System.String)</code> member of the <code>IInAppBillingService</code> interface returns a <code>System.Int32</code> value.</p>
</td></tr></table></p>


---

<a name="e452919a-6bfb-4cf1-b5e6-88131704b783"></a>
##Public Class IInAppBillingServiceStub

###Namespace

[Com.Android.Vending.Billing](#e2d55e0f-ba0c-46ac-823e-988a232f432d)

###Inherits From

`Android.OS.Binder`

###Summary

The public `IInAppBillingServiceStub` class inherits from the `Android.OS.Binder` class and is defined in the `Com.Android.Vending.Billing` namespace. It defines 6 fields, no properties, 9 methods and no events.
<p><table style='width:100%'><tr><th style='width:25%'>Fields</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#257beac5-3438-4014-bda2-bc9ab5b69808'>descriptor</a></td><td style='width:75%' ><p>The private static constant <code>descriptor</code> field of the <code>IInAppBillingServiceStub</code> class holds a <code>System.String</code> value of <code>com.android.vending.billing.IInAppBillingService</code>.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#eb933e8c-aa79-47f3-b4c1-0e8683774254'>TransactionConsumePurchase</a></td><td style='width:75%' class='def'><p>The  static constant <code>TransactionConsumePurchase</code> field of the <code>IInAppBillingServiceStub</code> class holds a <code>System.Int32</code> value of <code>5</code>.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#cbb929c2-0dc1-45c8-a672-089768d163a4'>TransactionGetBuyIntent</a></td><td style='width:75%' ><p>The  static constant <code>TransactionGetBuyIntent</code> field of the <code>IInAppBillingServiceStub</code> class holds a <code>System.Int32</code> value of <code>3</code>.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#bf1cbef8-ec7d-4af5-961e-bf1b8e8c75e9'>TransactionGetPurchases</a></td><td style='width:75%' class='def'><p>The  static constant <code>TransactionGetPurchases</code> field of the <code>IInAppBillingServiceStub</code> class holds a <code>System.Int32</code> value of <code>4</code>.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#f7dff411-83ae-4471-914b-4eb7f2af8519'>TransactionGetSkuDetails</a></td><td style='width:75%' ><p>The  static constant <code>TransactionGetSkuDetails</code> field of the <code>IInAppBillingServiceStub</code> class holds a <code>System.Int32</code> value of <code>2</code>.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#16b36d2d-6054-41ae-8938-57d36251d157'>TransactionIsBillingSupported</a></td><td style='width:75%' class='def'><p>The  static constant <code>TransactionIsBillingSupported</code> field of the <code>IInAppBillingServiceStub</code> class holds a <code>System.Int32</code> value of <code>1</code>.</p>
</td></tr></table></p>

<p><table style='width:100%'><tr><th style='width:25%'>Methods</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#3165cdf3-3431-4012-a242-358d5555153a'>AsBinder</a></td><td style='width:75%' ><p>The public virtual <code>AsBinder ()</code> member of the <code>IInAppBillingServiceStub</code> class returns a <code>Android.OS.IBinder</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#0f4a1c58-bb17-40a3-91e7-6018ce3af619'>AsInterface</a></td><td style='width:75%' class='def'><p>The public static <code>AsInterface (Android.OS.IBinder)</code> member of the <code>IInAppBillingServiceStub</code> class returns a <code>Com.Android.Vending.Billing.IInAppBillingService</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#cc63fa9b-53fe-4160-af48-67daa4dd5c9d'>ConsumePurchase</a></td><td style='width:75%' ><p>The public virtual <code>ConsumePurchase (System.Int32, System.String, System.String)</code> member of the <code>IInAppBillingServiceStub</code> class returns a <code>System.Int32</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#e1d65b86-37f6-4570-be96-912e4dece945'>GetBuyIntent</a></td><td style='width:75%' class='def'><p>The public virtual <code>GetBuyIntent (System.Int32, System.String, System.String, System.String, System.String)</code> member of the <code>IInAppBillingServiceStub</code> class returns a <code>Android.OS.Bundle</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#40404746-a646-42df-988a-bbb546533f62'>GetPurchases</a></td><td style='width:75%' ><p>The public virtual <code>GetPurchases (System.Int32, System.String, System.String, System.String)</code> member of the <code>IInAppBillingServiceStub</code> class returns a <code>Android.OS.Bundle</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#de9672aa-917a-484a-8b64-13f74058aa4a'>GetSkuDetails</a></td><td style='width:75%' class='def'><p>The public virtual <code>GetSkuDetails (System.Int32, System.String, System.String, Android.OS.Bundle)</code> member of the <code>IInAppBillingServiceStub</code> class returns a <code>Android.OS.Bundle</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#3bd13de6-9ef9-491b-ad7b-422715955a3f'>IInAppBillingServiceStub</a></td><td style='width:75%' ><p>The public <code>IInAppBillingServiceStub ()</code> constructor for the <code>IInAppBillingServiceStub</code> class.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#f5a697b8-a414-489c-94f9-012e7f6a8c38'>IsBillingSupported</a></td><td style='width:75%' class='def'><p>The public virtual <code>IsBillingSupported (System.Int32, System.String, System.String)</code> member of the <code>IInAppBillingServiceStub</code> class returns a <code>System.Int32</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#0db293b8-0221-4769-8fa6-ea1d65852406'>OnTransact</a></td><td style='width:75%' ><p>The  virtual <code>OnTransact (System.Int32, Android.OS.Parcel, Android.OS.Parcel, System.Int32)</code> member of the <code>IInAppBillingServiceStub</code> class returns a <code>System.Boolean</code> value.</p>
</td></tr></table></p>


---

<a name="88311aab-37d5-4381-95ce-c4aa0303ac7f"></a>
##Private Interface IJsonSerializerStrategy

###Namespace

[Xamarin.InAppBilling](#ab56f628-3601-4cd6-b1e2-74873a9b2ed8)

###Summary

The private `IJsonSerializerStrategy` interface inherits from the `` class and is defined in the `Xamarin.InAppBilling` namespace. It defines no fields, no properties, 2 methods and no events.
<p><table style='width:100%'><tr><th style='width:25%'>Methods</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#fb602161-89ef-4b4f-b376-4c28714199e9'>DeserializeObject</a></td><td style='width:75%' ><p>The public virtual <code>DeserializeObject (System.Object, System.Type)</code> member of the <code>IJsonSerializerStrategy</code> interface returns a <code>System.Object</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#4e1c841a-79ef-4307-947c-4543c9b8b925'>TrySerializeNonPrimitiveObject</a></td><td style='width:75%' class='def'><p>The public virtual <code>TrySerializeNonPrimitiveObject (System.Object, System.Object&amp;)</code> member of the <code>IJsonSerializerStrategy</code> interface returns a <code>System.Boolean</code> value.</p>
</td></tr></table></p>


---

<a name="4d25d02c-abf5-434f-870c-5e52e68701aa"></a>
##Public Enum InAppBillingErrorType

###Namespace

[Xamarin.InAppBilling](#ab56f628-3601-4cd6-b1e2-74873a9b2ed8)

###Inherits From

`System.Enum`

###Summary

Defines the types of errors that con be returned from a [InAppBillingServiceConnection](#80c1612e-cf5e-41b3-a216-ca89246674e9) when processing billing requests.
<p><table style='width:100%'><tr><th style='width:25%'>Fields</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#4962182e-cd42-4b71-b1a9-7df25da6e8b8'>BillingNotSupported</a></td><td style='width:75%' ><p>In App Billing is not supported on the current device.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#3ed0c13d-a683-4358-928f-3d7740fa2040'>SubscriptionsNotSupported</a></td><td style='width:75%' class='def'><p>Subscriptions are not supported on the current device.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#bbd18cbb-3bdf-4cdd-910e-0d6e69718574'>UnknownError</a></td><td style='width:75%' ><p>An unknown error has occurred.</p>
</td></tr></table></p>


---

<a name="789accaa-919d-4249-924f-e54076856b08"></a>
##Public Class InAppBillingHandler

###Namespace

[Xamarin.InAppBilling](#ab56f628-3601-4cd6-b1e2-74873a9b2ed8)

###Inherits From

`System.Object`

###Summary

The [InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08) is a helper class that handles communication with the Google Play services to provide support for getting a list of available products, buying a product, consuming a product, and getting a list of already owned products.
<p><table style='width:100%'><tr><th style='width:25%'>Fields</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#be218856-1385-41c7-b13f-46400f492070'>_activity</a></td><td style='width:75%' ><p>The private <code>_activity</code> field of the <code>InAppBillingHandler</code> class holds a <code>Android.App.Activity</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#4bfcbd92-dd06-4cf0-8cc5-36a73b030fd6'>_billingService</a></td><td style='width:75%' class='def'><p>The private <code>_billingService</code> field of the <code>InAppBillingHandler</code> class holds a <code>Com.Android.Vending.Billing.IInAppBillingService</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#ffaec3aa-0aae-4025-9ec7-fe8cff259e73'>_payload</a></td><td style='width:75%' ><p>The private <code>_payload</code> field of the <code>InAppBillingHandler</code> class holds a <code>System.String</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#0f35d8fc-b414-4b5d-9a7b-528b2a7c8896'>_publicKey</a></td><td style='width:75%' class='def'><p>The private <code>_publicKey</code> field of the <code>InAppBillingHandler</code> class holds a <code>System.String</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#a69f9023-8c11-43f1-8938-1d999179966d'>BuyProductError</a></td><td style='width:75%' ><p>The private <code>BuyProductError</code> field of the <code>InAppBillingHandler</code> class holds a <code>Xamarin.InAppBilling.InAppBillingHandler.BuyProductErrorDelegate</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#eb7f93f4-dc3a-44f6-b4b2-b427e0f6204c'>InAppBillingProcesingError</a></td><td style='width:75%' class='def'><p>The private <code>InAppBillingProcesingError</code> field of the <code>InAppBillingHandler</code> class holds a <code>Xamarin.InAppBilling.InAppBillingHandler.InAppBillingProcessingErrorDelegate</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#f2ab75ec-aeb4-4733-ae04-60d2cfa270de'>OnGetProductsError</a></td><td style='width:75%' ><p>The private <code>OnGetProductsError</code> field of the <code>InAppBillingHandler</code> class holds a <code>Xamarin.InAppBilling.InAppBillingHandler.OnGetProductsErrorDelegate</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#6eed2e6b-6d3e-4810-aed9-116e3573199c'>OnInvalidOwnedItemsBundleReturned</a></td><td style='width:75%' class='def'><p>The private <code>OnInvalidOwnedItemsBundleReturned</code> field of the <code>InAppBillingHandler</code> class holds a <code>Xamarin.InAppBilling.InAppBillingHandler.OnInvalidOwnedItemsBundleReturnedDelegate</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#ca025071-7739-4703-b802-fec251d52514'>OnProductPurchased</a></td><td style='width:75%' ><p>The private <code>OnProductPurchased</code> field of the <code>InAppBillingHandler</code> class holds a <code>Xamarin.InAppBilling.InAppBillingHandler.OnProductPurchasedDelegate</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#12fd8c50-d215-40cd-8eac-aafcd49db91f'>OnProductPurchasedError</a></td><td style='width:75%' class='def'><p>The private <code>OnProductPurchasedError</code> field of the <code>InAppBillingHandler</code> class holds a <code>Xamarin.InAppBilling.InAppBillingHandler.OnProductPurchaseErrorDelegate</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#43a8065a-823e-4e96-83a0-a25f6699d9e3'>OnPurchaseConsumed</a></td><td style='width:75%' ><p>The private <code>OnPurchaseConsumed</code> field of the <code>InAppBillingHandler</code> class holds a <code>Xamarin.InAppBilling.InAppBillingHandler.OnPurchaseConsumedDelegate</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#f6f74317-715a-4d3a-9dde-7806899a1fa2'>OnPurchaseConsumedError</a></td><td style='width:75%' class='def'><p>The private <code>OnPurchaseConsumedError</code> field of the <code>InAppBillingHandler</code> class holds a <code>Xamarin.InAppBilling.InAppBillingHandler.OnPurchaseConsumedErrorDelegate</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#72186108-e70e-4127-95aa-88e245419573'>OnPurchaseFailedValidation</a></td><td style='width:75%' ><p>The private <code>OnPurchaseFailedValidation</code> field of the <code>InAppBillingHandler</code> class holds a <code>Xamarin.InAppBilling.InAppBillingHandler.OnPurchaseFailedValidationDelegate</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#f6ddea15-f29d-4f42-82e1-ff78d4fcbbd1'>OnUserCanceled</a></td><td style='width:75%' class='def'><p>The private <code>OnUserCanceled</code> field of the <code>InAppBillingHandler</code> class holds a <code>Xamarin.InAppBilling.InAppBillingHandler.OnUserCanceledDelegate</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#53e86237-1467-44d4-9dc0-5d527f931fd6'>PurchaseRequestCode</a></td><td style='width:75%' ><p>The private static constant <code>PurchaseRequestCode</code> field of the <code>InAppBillingHandler</code> class holds a <code>System.Int32</code> value of <code>1001</code>.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#a6a7272e-5f6a-4a03-8298-b5fab81604a6'>QueryInventoryError</a></td><td style='width:75%' class='def'><p>The private <code>QueryInventoryError</code> field of the <code>InAppBillingHandler</code> class holds a <code>Xamarin.InAppBilling.InAppBillingHandler.QueryInventoryErrorDelegate</code> value.</p>
</td></tr></table></p>

<p><table style='width:100%'><tr><th style='width:25%'>Properties</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#22ace037-b644-4484-bb31-6052f88f8018'>PublicKey</a></td><td style='width:75%' ><p>Gets or sets the Google Play Service public key used for In-App Billing</p>
</td></tr></table></p>

<p><table style='width:100%'><tr><th style='width:25%'>Methods</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#a9596c24-42c5-4f9b-af9a-5763e6e0bcbe'>BuyProduct</a></td><td style='width:75%' ><p>Buys a product based on the given product SKU and Item Type attaching the given payload</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#b947f8e6-a4f3-4f03-8531-5da94ac47d71'>BuyProduct</a></td><td style='width:75%' class='def'><p>Buys the given <a href="#98d67a2a-c250-4548-8a90-9e3722fef483">Product</a> </p>
</td></tr><tr><td style='width:25%' class='term'><a href='#db035b38-2c81-4026-919c-b0bcdbc9fb6e'>BuyProduct</a></td><td style='width:75%' ><p>Buys the given <a href="#98d67a2a-c250-4548-8a90-9e3722fef483">Product</a> and attaches the given developer payload to the  purchase.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#2c9f2030-e9d9-451e-b763-b7617c541bb4'>ConsumePurchase</a></td><td style='width:75%' class='def'><p>Consumes the purchased item</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#7267a298-bece-48fc-b446-dd15961c82bc'>ConsumePurchase</a></td><td style='width:75%' ><p>Consumes the purchased item.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#c55b3a4d-c5fc-49a5-96f6-f6e116251fe9'>GetPurchases</a></td><td style='width:75%' class='def'><p>Gets a list of all products of a given item type purchased by the current user.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#52e80b9b-54f2-4c84-af40-792664732727'>HandleActivityResult</a></td><td style='width:75%' ><p>Handles the activity result.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#c756b4d9-c163-42d0-8596-d57d03a40eff'>InAppBillingHandler</a></td><td style='width:75%' class='def'><p>Initializes a new instance of the <a href="#789accaa-919d-4249-924f-e54076856b08">InAppBillingHandler</a> class.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#6b7986a4-ffed-4c0c-92e8-d1888318f740'>QueryInventoryAsync</a></td><td style='width:75%' ><p>Queries the inventory asynchronously and returns a list of <a href="#98d67a2a-c250-4548-8a90-9e3722fef483">Product</a>s matching  the given list of SKU numbers.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#9adbfa47-fbdd-4510-998c-1a14de496b01'>RaiseBuyProductError</a></td><td style='width:75%' class='def'><p>Raises the buy product error event.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#bd9493c5-1d5e-40dd-bf64-1bb83381bddd'>RaiseInAppBillingProcessingError</a></td><td style='width:75%' ><p>Raises the in app billing processing error event</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#c84ad7d8-0a8b-42f8-a0d3-b3cd916bbe18'>RaiseOnGetProductsError</a></td><td style='width:75%' class='def'><p>Raises the on get products error event.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#910951ee-a17f-4c06-a8f8-6427c8f9b9d0'>RaiseOnInvalidOwnedItemsBundleReturned</a></td><td style='width:75%' ><p>Raises the on invalid owned items bundle returned.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#76d7cfa8-761d-4b26-a410-a9a54ed7c767'>RaiseOnProductPurchased</a></td><td style='width:75%' class='def'><p>Raises the on product purchase completed event.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#9bdd94ec-3427-4de9-b92c-41b1fba34ab3'>RaiseOnProductPurchasedError</a></td><td style='width:75%' ><p>Raises the on product purchased error event.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#532bff74-4d9d-4ed9-a14f-978671b923a0'>RaiseOnPurchaseConsumed</a></td><td style='width:75%' class='def'><p>Raises the on product consumed.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#b666c409-58de-4e81-b8f5-22dac1bddb80'>RaiseOnPurchaseConsumedError</a></td><td style='width:75%' ><p>Raises the on product consumed error.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#14ae91f3-9acd-432e-8810-c1a9712c7c83'>RaiseOnPurchaseFailedValidation</a></td><td style='width:75%' class='def'><p>Raises the OnPurchaseFailedValidation event</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#02554a0b-642a-4afc-89a3-c9dbd95681b8'>RaiseOnUserCanceled</a></td><td style='width:75%' ><p>Raises the on user canceled event.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#157134bf-2bf9-4ee1-bbd0-ce0164c8930a'>RaiseQueryInventoryError</a></td><td style='width:75%' class='def'><p>Raises the query inventory error event.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#b8a7d82a-e1ce-4c40-8065-cab637914695'>ValidOwnedItems</a></td><td style='width:75%' ><p>Verifies that the given purchased bundle valid and contains an item list, data list and a signature list.</p>
</td></tr></table></p>

<p><table style='width:100%'><tr><th style='width:25%'>Events</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#2c0fa63e-e57d-4407-ac3e-b4ecc9a5e573'>BuyProductError</a></td><td style='width:75%' ><p>Occurs when the user attempts to buy a product and there is an error.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#78b79514-5e0f-4513-89b7-5d544d130332'>InAppBillingProcesingError</a></td><td style='width:75%' class='def'><p>Occurs when there is an in app billing procesing error.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#3f46a95d-ef7d-4682-8e77-ed0995311217'>OnGetProductsError</a></td><td style='width:75%' ><p>Raised where there is an error getting previously purchased products from the Google Play Services.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#6a10c879-72b8-428a-94cd-550523dee32c'>OnInvalidOwnedItemsBundleReturned</a></td><td style='width:75%' class='def'><p>Raised when Google Play Services returns an invalid bundle from previously purchased items</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#13ff87b5-627a-4e6e-98f6-c89702c41304'>OnProductPurchased</a></td><td style='width:75%' ><p>Occurs after a product has been successfully purchased Google Play.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#b9079b27-e928-4217-8003-f88521686b85'>OnProductPurchasedError</a></td><td style='width:75%' class='def'><p>Occurs when the is an error on a product purchase attempt.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#6cede3c7-d79d-430e-b730-42ea79ee5768'>OnPurchaseConsumed</a></td><td style='width:75%' ><p>Occurs when on product consumed.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#cc0abc89-53b4-45d7-9e5a-c9c6a2b2b338'>OnPurchaseConsumedError</a></td><td style='width:75%' class='def'><p>Occurs when there is an error consuming a product.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#38cafdb8-1577-4438-812d-9bec5bcb474b'>OnPurchaseFailedValidation</a></td><td style='width:75%' ><p>Occurs when a previously purchased product fails to validate.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#b0f95064-5eb4-444c-96b4-505e43f1eac0'>OnUserCanceled</a></td><td style='width:75%' class='def'><p>Occurs when on user canceled.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#da55a78e-3d01-45f0-98bb-f5e17f00b3cd'>QueryInventoryError</a></td><td style='width:75%' ><p>Occurs when there is an error querying inventory from Google Play Services.</p>
</td></tr></table></p>


---

<a name="7b5d0609-2f0c-4def-b7a4-27677dc3798b"></a>
##InAppBillingProcessingErrorDelegate

###Namespace

[Xamarin.InAppBilling.InAppBillingHandler](#7789afcb-6894-431d-8675-dd6bd066b6b8)

###Summary

Occurs when there is an in app billing procesing error.

---

<a name="80c1612e-cf5e-41b3-a216-ca89246674e9"></a>
##Public Class InAppBillingServiceConnection

###Namespace

[Xamarin.InAppBilling](#ab56f628-3601-4cd6-b1e2-74873a9b2ed8)

###Inherits From

`Java.Lang.Object`

###Summary

The [InAppBillingServiceConnection](#80c1612e-cf5e-41b3-a216-ca89246674e9) class binds your `Activity` to the Google Play’s In-app Billing  service to send In-app Billing requests to Google Play from your application. As part of the setup process,  the [InAppBillingServiceConnection](#80c1612e-cf5e-41b3-a216-ca89246674e9) also checks if the In-app Billing Version 3 API is supported by Google Play.  If the API version is not supported, or if an error occured while establishing the service binding, the listener is notified and passed an error message.
<p><table style='width:100%'><tr><th style='width:25%'>Fields</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#9715f2f4-0c76-4703-b9cf-6fb827d46817'>_activity</a></td><td style='width:75%' ><p>The backing store for the activity.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#53da338a-73a3-4d45-9ab1-37e452a8b156'>_publicKey</a></td><td style='width:75%' class='def'><p>The backing store for the public key.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#0e5ec3af-96a0-4f6f-a3be-088178f29eb6'>OnConnected</a></td><td style='width:75%' ><p>The private <code>OnConnected</code> field of the <code>InAppBillingServiceConnection</code> class holds a <code>Xamarin.InAppBilling.InAppBillingServiceConnection.OnConnectedDelegate</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#e20915e0-631e-461f-98d7-7487bd12ffbe'>OnDisconnected</a></td><td style='width:75%' class='def'><p>The private <code>OnDisconnected</code> field of the <code>InAppBillingServiceConnection</code> class holds a <code>Xamarin.InAppBilling.InAppBillingServiceConnection.OnDisconnectedDelegate</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#c03fc43d-7717-46dc-b1e6-3f220e0a9ae9'>OnInAppBillingError</a></td><td style='width:75%' ><p>The private <code>OnInAppBillingError</code> field of the <code>InAppBillingServiceConnection</code> class holds a <code>Xamarin.InAppBilling.InAppBillingServiceConnection.OnInAppBillingErrorDelegate</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#f18c6f48-90e6-4e8f-a56c-10fc62890032'>Tag</a></td><td style='width:75%' class='def'><p>The constant identifier from the <a href="#80c1612e-cf5e-41b3-a216-ca89246674e9">InAppBillingServiceConnection</a></p>
</td></tr></table></p>

<p><table style='width:100%'><tr><th style='width:25%'>Properties</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#780cf78a-b27c-4335-a607-f37e53dbdfaf'>BillingHandler</a></td><td style='width:75%' ><p>Gets the <a href="#789accaa-919d-4249-924f-e54076856b08">InAppBillingHandler</a> used to communicate with the Google Play Service</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#ccc6bd37-37cb-4efe-b8e6-3e36b7c77f6e'>Connected</a></td><td style='width:75%' class='def'><p>Gets a value indicating whether this <a href="#80c1612e-cf5e-41b3-a216-ca89246674e9">InAppBillingServiceConnection</a> is connected to the Google Play service</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#0c0561d8-04c3-4c2d-b827-4d7d7298687f'>PublicKey</a></td><td style='width:75%' ><p>Gets or sets the Google Play Service public key used for In-App Billing</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#1caa8556-74b2-41e3-902b-8c2248d3d8f8'>Service</a></td><td style='width:75%' class='def'><p>Gets the Google Play <code>InAppBillingService</code> interface.</p>
</td></tr></table></p>

<p><table style='width:100%'><tr><th style='width:25%'>Methods</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#5ae05ea1-e624-408b-bc8f-47b0caae536a'>Connect</a></td><td style='width:75%' ><p>Connect this instance to the Google Play service to support In-App Billing in your application</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#3b72d411-115d-46b7-818f-74a751170de6'>Disconnect</a></td><td style='width:75%' class='def'><p>Disconnects this instance from the Google Play service.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#5403932a-65a4-4e53-a9a5-2d8a50befe1d'>InAppBillingServiceConnection</a></td><td style='width:75%' ><p>Initializes a new instance of the <a href="#80c1612e-cf5e-41b3-a216-ca89246674e9">InAppBillingServiceConnection</a> class.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#fcf0b617-edb8-44f8-829e-488145939579'>OnServiceConnected</a></td><td style='width:75%' class='def'><p>Raises the service connected event.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#514ac181-bc06-43a7-b301-c4c54213d20f'>OnServiceDisconnected</a></td><td style='width:75%' ><p>Raises the service disconnected event.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#ddf4cc61-73a5-443b-a42c-0c673beaf15e'>RaiseOnConnected</a></td><td style='width:75%' class='def'><p>Raises the on connected event.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#d24c2542-2fe0-4fa3-b17f-d715b345ed30'>RaiseOnDisconnected</a></td><td style='width:75%' ><p>Raises the on disconnected event.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#37a5dc82-9f88-4ee7-92f0-61ba7a392726'>RaiseOnInAppBillingError</a></td><td style='width:75%' class='def'><p>Raises the on in app billing error.</p>
</td></tr></table></p>

<p><table style='width:100%'><tr><th style='width:25%'>Events</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#2da4661b-d73d-4574-aece-598b3240f31e'>OnConnected</a></td><td style='width:75%' ><p>Occurs when on connected.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#b98a04dd-3ae7-4845-938e-d9d18a115b42'>OnDisconnected</a></td><td style='width:75%' class='def'><p>Occurs when on disconnected.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#ec062eeb-b6b9-41c9-a203-05e187969bec'>OnInAppBillingError</a></td><td style='width:75%' ><p>Occurs when on in app billing error.</p>
</td></tr></table></p>


---

<a name="cb80eb77-1e36-46b1-9262-60453abc97dc"></a>
##Public Static Class ItemType

###Namespace

[Xamarin.InAppBilling](#ab56f628-3601-4cd6-b1e2-74873a9b2ed8)

###Inherits From

`System.Object`

###Summary

Defines the types of items that can be purchased using Google Play In-App Billing.
<p><table style='width:100%'><tr><th style='width:25%'>Fields</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#57fc1696-3e5c-4e34-ab13-6fc3d2c1e886'>Product</a></td><td style='width:75%' ><p>A standard consumable or non-consumable product.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#3c39d52e-c47e-4a9c-b6f5-50b94a9b40c2'>Subscription</a></td><td style='width:75%' class='def'><p>A subscription product such as a magazine.</p>
</td></tr></table></p>


---

<a name="fc61b8f7-b351-48f2-9773-888e050fffcb"></a>
##Private Class JsonArray

###Namespace

[Xamarin.InAppBilling](#ab56f628-3601-4cd6-b1e2-74873a9b2ed8)

###Inherits From

`System.Collections.Generic.List<System.Object>`

###Summary

Represents the json array.
<p><table style='width:100%'><tr><th style='width:25%'>Methods</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#6f43217e-ee3f-43b4-9ee1-4229c5946070'>JsonArray</a></td><td style='width:75%' ><p>Initializes a new instance of the <a href="#fc61b8f7-b351-48f2-9773-888e050fffcb">JsonArray</a> class. </p>
</td></tr><tr><td style='width:25%' class='term'><a href='#344f33eb-8d65-4ae4-811d-06fb732c899d'>JsonArray</a></td><td style='width:75%' class='def'><p>Initializes a new instance of the <a href="#fc61b8f7-b351-48f2-9773-888e050fffcb">JsonArray</a> class. </p>
</td></tr><tr><td style='width:25%' class='term'><a href='#f932e257-ceda-49a1-89b0-64119d61303e'>ToString</a></td><td style='width:75%' ><p>The json representation of the array.</p>
</td></tr></table></p>


---

<a name="4db2613b-bdf6-4cff-a6da-804b56455cbe"></a>
##Private Class JsonObject

###Namespace

[Xamarin.InAppBilling](#ab56f628-3601-4cd6-b1e2-74873a9b2ed8)

###Inherits From

`System.Object`

###Summary

Represents the json object.
<p><table style='width:100%'><tr><th style='width:25%'>Fields</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#5b5208b9-3b67-4705-8acb-ff85e1550525'>_members</a></td><td style='width:75%' ><p>The internal member dictionary.</p>
</td></tr></table></p>

<p><table style='width:100%'><tr><th style='width:25%'>Properties</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#a7a332aa-7b7b-471f-9b22-ee1990a99d59'>Count</a></td><td style='width:75%' ><p>Gets the count.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#5080c2e7-34bf-4ba3-99dd-4e9580d25d9a'>IsReadOnly</a></td><td style='width:75%' class='def'><p>Gets a value indicating whether this instance is read only.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#7f814223-e53e-4a5c-82ee-41045b92d8d6'>Item(System.Int32)</a></td><td style='width:75%' ><p>Gets the <code>Object</code> at the specified index.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#1b0bbab2-b2cc-4377-85d1-3c8b906ef621'>Item(System.String)</a></td><td style='width:75%' class='def'><p>Gets or sets the <code>Object</code> with the specified key.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#bcdd8bd4-d94f-49d1-82f1-beafc2314646'>Keys</a></td><td style='width:75%' ><p>Gets the keys.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#b35ddd29-9a7e-425f-b2e9-0e8ad4942c52'>Values</a></td><td style='width:75%' class='def'><p>Gets the values.</p>
</td></tr></table></p>

<p><table style='width:100%'><tr><th style='width:25%'>Methods</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#8bc8f851-2466-4411-923d-1da23b2d886b'>Add</a></td><td style='width:75%' ><p>Adds the specified item.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#92d9e6e9-c46f-44dd-ba1f-06edad5d1b29'>Add</a></td><td style='width:75%' class='def'><p>Adds the specified key.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#67c0971b-9d94-4247-b5ef-6e5cdf5176fc'>Clear</a></td><td style='width:75%' ><p>Clears this instance.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#96ce05b1-2819-4a31-b29d-57208a60084a'>Contains</a></td><td style='width:75%' class='def'><p>Determines whether [contains] [the specified item].</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#021cd2c3-e85b-47f3-b704-c2902708092c'>ContainsKey</a></td><td style='width:75%' ><p>Determines whether the specified key contains key.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#96c7a88d-78cf-4bfa-8b85-760a3941c8c0'>CopyTo</a></td><td style='width:75%' class='def'><p>Copies to.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#772647e6-1d03-4686-a815-d560ba54c116'>GetAtIndex</a></td><td style='width:75%' ><p>The  static <code>GetAtIndex (System.Collections.Generic.IDictionary&lt;System.String, System.Object&gt;, System.Int32)</code> member of the <code>JsonObject</code> class returns a <code>System.Object</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#80dee118-beb5-4b24-b68b-5cd07421c129'>GetEnumerator</a></td><td style='width:75%' class='def'><p>Gets the enumerator.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#120b9086-4869-4079-8b65-3aa3e5c8ef20'>JsonObject</a></td><td style='width:75%' ><p>Initializes a new instance of <a href="#4db2613b-bdf6-4cff-a6da-804b56455cbe">JsonObject</a>.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#f8c9b61e-59e2-463b-a00f-279fb156d1f5'>JsonObject</a></td><td style='width:75%' class='def'><p>Initializes a new instance of <a href="#4db2613b-bdf6-4cff-a6da-804b56455cbe">JsonObject</a>.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#fdccc60b-a4e5-4c8c-9eac-e479cd264a01'>Remove</a></td><td style='width:75%' ><p>Removes the specified item.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#b884cac8-1e8f-41d8-8173-58b7c7ca373f'>Remove</a></td><td style='width:75%' class='def'><p>Removes the specified key.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#e465c3b5-233d-4274-8420-6bc8c02271fa'>System#Collections#IEnumerable#GetEnumerator</a></td><td style='width:75%' ><p>Returns an enumerator that iterates through a collection.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#7408a5e1-6d8b-46fb-911a-454d37cc0386'>ToString</a></td><td style='width:75%' class='def'><p>Returns a json <code>String</code> that represents the current <code>Object</code>.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#a09e2734-5e52-4141-b862-5c2c6f8dd24d'>TryGetValue</a></td><td style='width:75%' ><p>Tries the get value.</p>
</td></tr></table></p>


---

<a name="4e163874-6eb3-4f60-8c44-9e0260d939fb"></a>
##Private Class Logger

###Namespace

[Xamarin.InAppBilling.Utilities](#809cf835-9470-46b0-9c02-adb4bd944057)

###Inherits From

`System.Object`

###Summary

Helper class to support logging within the In-App Purchases routines
<p><table style='width:100%'><tr><th style='width:25%'>Fields</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#94429889-8685-4b30-a148-74857b31e725'>Tag</a></td><td style='width:75%' ><p>The private static constant <code>Tag</code> field of the <code>Logger</code> class holds a <code>System.String</code> value of <code>InApp-Billing</code>.</p>
</td></tr></table></p>

<p><table style='width:100%'><tr><th style='width:25%'>Methods</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#88ca6ed3-40ab-45d4-b8a9-032e8e6be4f8'>Debug</a></td><td style='width:75%' ><p>Writes debug information to the log</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#67d9a5f2-6ad5-48a3-828f-c0f1062b0bb6'>Error</a></td><td style='width:75%' class='def'><p>Writes error information to the log</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#f47924e9-0915-4c24-a366-b94ce61c1da7'>Info</a></td><td style='width:75%' ><p>Writes general information to the log</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#df51e3d7-0b24-4616-97ff-c4ad263b783b'>Logger</a></td><td style='width:75%' class='def'><p>The public <code>Logger ()</code> constructor for the <code>Logger</code> class.</p>
</td></tr></table></p>


---

<a name="f81adae0-3a0b-42a2-a3cf-1136a9a6c0c4"></a>
##OnConnectedDelegate

###Namespace

[Xamarin.InAppBilling.InAppBillingServiceConnection](#3bdc2924-4b86-4011-b92d-0c7e46427cd0)

###Summary

Occurs when on connected.

---

<a name="6187252d-c6be-4eb8-8bac-62d7efeeca97"></a>
##OnDisconnectedDelegate

###Namespace

[Xamarin.InAppBilling.InAppBillingServiceConnection](#3bdc2924-4b86-4011-b92d-0c7e46427cd0)

###Summary

Occurs when on disconnected.

---

<a name="d6aede4f-36f7-4c26-8e11-e25f23ee3c95"></a>
##OnGetProductsErrorDelegate

###Namespace

[Xamarin.InAppBilling.InAppBillingHandler](#7789afcb-6894-431d-8675-dd6bd066b6b8)

###Summary

Raised where there is an error getting previously purchased products from the Google Play Services.

---

<a name="b2fa7c05-cb97-400a-a254-a2f0e67a90f2"></a>
##OnInAppBillingErrorDelegate

###Namespace

[Xamarin.InAppBilling.InAppBillingServiceConnection](#3bdc2924-4b86-4011-b92d-0c7e46427cd0)

###Summary

Occurs when on in app billing error.

---

<a name="8758fba9-3d12-446b-ab23-b71e32b92354"></a>
##OnInvalidOwnedItemsBundleReturnedDelegate

###Namespace

[Xamarin.InAppBilling.InAppBillingHandler](#7789afcb-6894-431d-8675-dd6bd066b6b8)

###Summary

Raised when Google Play Services returns an invalid bundle from previously purchased items

---

<a name="2ed54e69-6056-4702-86c8-ae945939f92b"></a>
##OnProductPurchasedDelegate

###Namespace

[Xamarin.InAppBilling.InAppBillingHandler](#7789afcb-6894-431d-8675-dd6bd066b6b8)

###Summary

Occurs after a product has been successfully purchased Google Play.
###Remarks

This event is fired after a `OnProductPurchased` which is raised when the user successfully  logs an intent to purchase with Google Play.

---

<a name="09a88fb4-2a98-4e4b-a2c6-aa7749c450cc"></a>
##OnProductPurchaseErrorDelegate

###Namespace

[Xamarin.InAppBilling.InAppBillingHandler](#7789afcb-6894-431d-8675-dd6bd066b6b8)

###Summary

Occurs when the is an error on a product purchase attempt.
###Remarks

The `responseCode` will be a value from [BillingResult](#ef5b6352-1a73-4ee5-a808-a03384c31044).

---

<a name="fd7ef9f9-bb27-4eff-a319-a3193670f137"></a>
##OnPurchaseConsumedDelegate

###Namespace

[Xamarin.InAppBilling.InAppBillingHandler](#7789afcb-6894-431d-8675-dd6bd066b6b8)

###Summary

Occurs when on product consumed.

---

<a name="db1cca38-9f7c-4d1a-97c3-e9da2b715763"></a>
##OnPurchaseConsumedErrorDelegate

###Namespace

[Xamarin.InAppBilling.InAppBillingHandler](#7789afcb-6894-431d-8675-dd6bd066b6b8)

###Summary

Occurs when there is an error consuming a product.
###Remarks

The `responseCode` will be a value from [BillingResult](#ef5b6352-1a73-4ee5-a808-a03384c31044).

---

<a name="e34de5bc-5059-4b1c-820c-c3a2363b472f"></a>
##OnPurchaseFailedValidationDelegate

###Namespace

[Xamarin.InAppBilling.InAppBillingHandler](#7789afcb-6894-431d-8675-dd6bd066b6b8)

###Summary

Occurs when a previously purchased product fails to validate.

---

<a name="8e757b02-94e2-431f-bfe3-56483c436f0e"></a>
##OnUserCanceledDelegate

###Namespace

[Xamarin.InAppBilling.InAppBillingHandler](#7789afcb-6894-431d-8675-dd6bd066b6b8)

###Summary

Occurs when the user cancels an In App Billing purchase.

---

<a name="1ac2d0dc-01c8-489d-9039-4374c33d0124"></a>
##Private Class PocoJsonSerializerStrategy

###Namespace

[Xamarin.InAppBilling](#ab56f628-3601-4cd6-b1e2-74873a9b2ed8)

###Inherits From

`System.Object`

###Summary

The private `PocoJsonSerializerStrategy` class inherits from the `System.Object` class and is defined in the `Xamarin.InAppBilling` namespace. It defines 6 fields, no properties, 11 methods and no events.
<p><table style='width:100%'><tr><th style='width:25%'>Fields</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#e6e51c88-972e-413e-8919-8d187ec1ab78'>ArrayConstructorParameterTypes</a></td><td style='width:75%' ><p>The  static <code>ArrayConstructorParameterTypes</code> field of the <code>PocoJsonSerializerStrategy</code> class holds a <code>System.Type[]</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#3225d596-d807-4ea9-9dae-328b671d630c'>ConstructorCache</a></td><td style='width:75%' class='def'><p>The  <code>ConstructorCache</code> field of the <code>PocoJsonSerializerStrategy</code> class holds a <code>System.Collections.Generic.IDictionary&lt;System.Type,Xamarin.InAppBilling.Reflection.ReflectionUtils.ConstructorDelegate&gt;</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#aebf7507-9b38-4bae-96aa-395ace698c96'>EmptyTypes</a></td><td style='width:75%' ><p>The  static <code>EmptyTypes</code> field of the <code>PocoJsonSerializerStrategy</code> class holds a <code>System.Type[]</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#68ddbe44-da25-4e40-a158-1d7df0e9b1db'>GetCache</a></td><td style='width:75%' class='def'><p>The  <code>GetCache</code> field of the <code>PocoJsonSerializerStrategy</code> class holds a <code>System.Collections.Generic.IDictionary&lt;System.Type,System.Collections.Generic.IDictionary&lt;System.String,Xamarin.InAppBilling.Reflection.ReflectionUtils.GetDelegate&gt;&gt;</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#8b5e1098-4729-4b4f-af3a-e77a93d9bd09'>Iso8601Format</a></td><td style='width:75%' ><p>The private static <code>Iso8601Format</code> field of the <code>PocoJsonSerializerStrategy</code> class holds a <code>System.String[]</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#844cffe1-1f93-4b16-a521-fab59cee8999'>SetCache</a></td><td style='width:75%' class='def'><p>The  <code>SetCache</code> field of the <code>PocoJsonSerializerStrategy</code> class holds a <code>System.Collections.Generic.IDictionary&lt;System.Type,System.Collections.Generic.IDictionary&lt;System.String,System.Collections.Generic.KeyValuePair&lt;System.Type,Xamarin.InAppBilling.Reflection.ReflectionUtils.SetDelegate&gt;&gt;&gt;</code> value.</p>
</td></tr></table></p>

<p><table style='width:100%'><tr><th style='width:25%'>Methods</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#19e63867-c591-409e-b4f7-8d4726fa2323'>ContructorDelegateFactory</a></td><td style='width:75%' ><p>The  virtual <code>ContructorDelegateFactory (System.Type)</code> member of the <code>PocoJsonSerializerStrategy</code> class returns a <code>Xamarin.InAppBilling.Reflection.ReflectionUtils.ConstructorDelegate</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#1f1239a0-23c0-4147-8901-a0ba7353fd9a'>DeserializeObject</a></td><td style='width:75%' class='def'><p>The public virtual <code>DeserializeObject (System.Object, System.Type)</code> member of the <code>PocoJsonSerializerStrategy</code> class returns a <code>System.Object</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#08e6d3bb-7710-47a9-b912-85e3d147ed8e'>GetterValueFactory</a></td><td style='width:75%' ><p>The  virtual <code>GetterValueFactory (System.Type)</code> member of the <code>PocoJsonSerializerStrategy</code> class returns a <code>System.Collections.Generic.IDictionary&lt;System.String,Xamarin.InAppBilling.Reflection.ReflectionUtils.GetDelegate&gt;</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#c452ce8e-65ef-4573-aee8-bae9cdffc32a'>MapClrMemberNameToJsonFieldName</a></td><td style='width:75%' class='def'><p>The  virtual <code>MapClrMemberNameToJsonFieldName (System.String)</code> member of the <code>PocoJsonSerializerStrategy</code> class returns a <code>System.String</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#ab862f9d-504f-4a3d-b3ee-e4d8e316f9a2'>PocoJsonSerializerStrategy</a></td><td style='width:75%' ><p>The public <code>PocoJsonSerializerStrategy ()</code> constructor for the <code>PocoJsonSerializerStrategy</code> class.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#f94c841d-968a-448c-8580-0dcfb5819a7c'>SerializeEnum</a></td><td style='width:75%' class='def'><p>The  virtual <code>SerializeEnum (System.Enum)</code> member of the <code>PocoJsonSerializerStrategy</code> class returns a <code>System.Object</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#7ca1292b-7e74-40ec-a069-ee7d3edea58a'>SetterValueFactory</a></td><td style='width:75%' ><p>The  virtual <code>SetterValueFactory (System.Type)</code> member of the <code>PocoJsonSerializerStrategy</code> class returns a <code>System.Collections.Generic.IDictionary&lt;System.String,System.Collections.Generic.KeyValuePair&lt;System.Type,Xamarin.InAppBilling.Reflection.ReflectionUtils.SetDelegate&gt;&gt;</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#9674997b-0b65-440e-97d2-7909ee6200c2'>TrySerializeKnownTypes</a></td><td style='width:75%' class='def'><p>The  virtual <code>TrySerializeKnownTypes (System.Object, System.Object&amp;)</code> member of the <code>PocoJsonSerializerStrategy</code> class returns a <code>System.Boolean</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#5dc6ead3-9405-482f-924d-e62c1c6e3176'>TrySerializeNonPrimitiveObject</a></td><td style='width:75%' ><p>The public virtual <code>TrySerializeNonPrimitiveObject (System.Object, System.Object&amp;)</code> member of the <code>PocoJsonSerializerStrategy</code> class returns a <code>System.Boolean</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#9e7f4979-8815-47b2-85fb-edf62be0f0ea'>TrySerializeUnknownTypes</a></td><td style='width:75%' class='def'><p>The  virtual <code>TrySerializeUnknownTypes (System.Object, System.Object&amp;)</code> member of the <code>PocoJsonSerializerStrategy</code> class returns a <code>System.Boolean</code> value.</p>
</td></tr></table></p>


---

<a name="98d67a2a-c250-4548-8a90-9e3722fef483"></a>
##Public Class Product

###Namespace

[Xamarin.InAppBilling](#ab56f628-3601-4cd6-b1e2-74873a9b2ed8)

###Inherits From

`System.Object`

###Summary

Holds all information about an In-App Billing product available on the Google Play store.
<p><table style='width:100%'><tr><th style='width:25%'>Properties</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#6516623f-d7c9-46e5-9722-99e6bed4c0db'>Description</a></td><td style='width:75%' ><p>Gets or sets the description.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#2b92b987-b2ab-4ae5-80eb-75315136c952'>Price</a></td><td style='width:75%' class='def'><p>Gets or sets the price.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#7448e262-2b48-448a-a9e2-43c9f97cef07'>Price_Amount_Micros</a></td><td style='width:75%' ><p>Price in micro-units, where 1,000,000 micro-units equal one unit of the currency. For example, if price is "€7.99", price<em>amount</em>micros is "7990000".</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#a9c164f4-6953-45ff-b8f9-4ea6ee6f15b1'>Price_Currency_Code</a></td><td style='width:75%' class='def'><p>ISO 4217 currency code for price. For example, if price is specified in British pounds sterling, price<em>currency</em>code is "GBP".</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#dffe56e7-ba74-4b3a-9053-65107ae3a8e2'>ProductId</a></td><td style='width:75%' ><p>Gets or sets the product identifier.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#783ad0e3-0c42-4943-a9f0-3140b898198b'>Title</a></td><td style='width:75%' class='def'><p>Gets or sets the title.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#e58a4321-7ec4-4aca-8716-d46c467e8d87'>Type</a></td><td style='width:75%' ><p>Gets or sets the type.</p>
</td></tr></table></p>

<p><table style='width:100%'><tr><th style='width:25%'>Methods</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#dde87bcd-f40c-4615-86ce-8215b747cef6'>Product</a></td><td style='width:75%' ><p>Initializes a new instance of the <a href="#98d67a2a-c250-4548-8a90-9e3722fef483">Product</a> class.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#95ec6289-97d0-4579-bcd1-96125a0ad84d'>ToString</a></td><td style='width:75%' class='def'><p>The public virtual <code>ToString ()</code> member of the <code>Product</code> class returns a <code>System.String</code> value.</p>
</td></tr></table></p>


---

<a name="03489ba4-1d9d-4430-8e09-b3bff9875fac"></a>
##Public Class Purchase

###Namespace

[Xamarin.InAppBilling](#ab56f628-3601-4cd6-b1e2-74873a9b2ed8)

###Inherits From

`System.Object`

###Summary

Holds all information about a product purchased from Google Play for the current user.
<p><table style='width:100%'><tr><th style='width:25%'>Properties</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#a95065b5-4c5f-4c19-a287-e6ddef7de8bd'>DeveloperPayload</a></td><td style='width:75%' ><p>Gets or sets the developer payload.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#5db954bd-d6eb-4487-b830-9a174df85e2b'>OrderId</a></td><td style='width:75%' class='def'><p>Gets or sets the order identifier.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#1e2d2e03-8f8e-45f5-95bd-a39fea754d6f'>PackageName</a></td><td style='width:75%' ><p>Gets or sets the name of the package.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#5687d267-a288-4133-9106-cfe7328d22c1'>ProductId</a></td><td style='width:75%' class='def'><p>Gets or sets the product identifier.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#51c56559-7255-42ff-83b9-60ae9c953cc4'>PurchaseState</a></td><td style='width:75%' ><p>Gets or sets the state of the purchase.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#1b7e1671-1b14-4753-b3da-0ca7fe5ce9e2'>PurchaseTime</a></td><td style='width:75%' class='def'><p>Gets or sets the purchase time.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#ae8bc34b-e558-44dd-98e4-161887243b0a'>PurchaseToken</a></td><td style='width:75%' ><p>Gets or sets the purchase token.</p>
</td></tr></table></p>

<p><table style='width:100%'><tr><th style='width:25%'>Methods</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#9db0762f-aba1-4acf-8c3d-fa35f07df217'>Purchase</a></td><td style='width:75%' ><p>The public <code>Purchase ()</code> constructor for the <code>Purchase</code> class.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#42bdff02-49db-4410-b90c-9c25532455ac'>ToString</a></td><td style='width:75%' class='def'><p>Converts the <a href="#03489ba4-1d9d-4430-8e09-b3bff9875fac">Purchase</a> into a <code>string</code></p>
</td></tr></table></p>


---

<a name="2debbb37-b92f-4714-92c2-47510bad06c2"></a>
##QueryInventoryErrorDelegate

###Namespace

[Xamarin.InAppBilling.InAppBillingHandler](#7789afcb-6894-431d-8675-dd6bd066b6b8)

###Summary

Occurs when there is an error querying inventory from Google Play Services.

---

<a name="6e848a6a-ea7d-4af0-b023-cd70a7e96d45"></a>
##Private Class ReflectionUtils

###Namespace

[Xamarin.InAppBilling.Reflection](#28604adb-bac9-4422-8e59-f8f58e49b978)

###Inherits From

`System.Object`

###Summary

The private `ReflectionUtils` class inherits from the `System.Object` class and is defined in the `Xamarin.InAppBilling.Reflection` namespace. It defines one field, no properties, 39 methods and no events.
<p><table style='width:100%'><tr><th style='width:25%'>Fields</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#9fa7828d-2c70-433e-9917-5cf158b9da9f'>EmptyObjects</a></td><td style='width:75%' ><p>The private static <code>EmptyObjects</code> field of the <code>ReflectionUtils</code> class holds a <code>System.Object[]</code> value.</p>
</td></tr></table></p>

<p><table style='width:100%'><tr><th style='width:25%'>Methods</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#9473dfaf-799f-4182-a73d-7d3b01265567'>Assign</a></td><td style='width:75%' ><p>The public static <code>Assign (System.Linq.Expressions.Expression, System.Linq.Expressions.Expression)</code> member of the <code>ReflectionUtils</code> class returns a <code>System.Linq.Expressions.BinaryExpression</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#34ba3d7e-6dc0-4de8-adb4-7af8d76a42db'>GetAttribute</a></td><td style='width:75%' class='def'><p>The public static <code>GetAttribute (System.Reflection.MemberInfo, System.Type)</code> member of the <code>ReflectionUtils</code> class returns a <code>System.Attribute</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#abbac326-fd9d-4bce-b05a-5a375c3f9a71'>GetAttribute</a></td><td style='width:75%' ><p>The public static <code>GetAttribute (System.Type, System.Type)</code> member of the <code>ReflectionUtils</code> class returns a <code>System.Attribute</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#045bb72c-0c32-440c-905c-e1e03cc8ad1d'>GetConstructorByExpression</a></td><td style='width:75%' class='def'><p>The public static <code>GetConstructorByExpression (System.Reflection.ConstructorInfo)</code> member of the <code>ReflectionUtils</code> class returns a <code>Xamarin.InAppBilling.Reflection.ReflectionUtils.ConstructorDelegate</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#3bd7687b-18de-49f1-a60f-4edf86644e65'>GetConstructorByExpression</a></td><td style='width:75%' ><p>The public static <code>GetConstructorByExpression (System.Type, System.Type[])</code> member of the <code>ReflectionUtils</code> class returns a <code>Xamarin.InAppBilling.Reflection.ReflectionUtils.ConstructorDelegate</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#5eb65d1d-dc17-4a79-a298-8ae5caa4d0ec'>GetConstructorByReflection</a></td><td style='width:75%' class='def'><p>The public static <code>GetConstructorByReflection (System.Reflection.ConstructorInfo)</code> member of the <code>ReflectionUtils</code> class returns a <code>Xamarin.InAppBilling.Reflection.ReflectionUtils.ConstructorDelegate</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#529a440c-b323-4c7d-b8d0-e67a2bc05ebc'>GetConstructorByReflection</a></td><td style='width:75%' ><p>The public static <code>GetConstructorByReflection (System.Type, System.Type[])</code> member of the <code>ReflectionUtils</code> class returns a <code>Xamarin.InAppBilling.Reflection.ReflectionUtils.ConstructorDelegate</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#6a9910d2-9932-409f-a124-53eb41085aef'>GetConstructorInfo</a></td><td style='width:75%' class='def'><p>The public static <code>GetConstructorInfo (System.Type, System.Type[])</code> member of the <code>ReflectionUtils</code> class returns a <code>System.Reflection.ConstructorInfo</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#504a71e4-74bd-4d4e-8790-3efc84c2627f'>GetConstructors</a></td><td style='width:75%' ><p>The public static <code>GetConstructors (System.Type)</code> member of the <code>ReflectionUtils</code> class returns a <code>System.Collections.Generic.IEnumerable&lt;System.Reflection.ConstructorInfo&gt;</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#b41d1260-6848-43c9-94a4-0538235b4ed1'>GetContructor</a></td><td style='width:75%' class='def'><p>The public static <code>GetContructor (System.Reflection.ConstructorInfo)</code> member of the <code>ReflectionUtils</code> class returns a <code>Xamarin.InAppBilling.Reflection.ReflectionUtils.ConstructorDelegate</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#dee6137f-7a0b-4c9a-9af7-6eab7c7c03b3'>GetContructor</a></td><td style='width:75%' ><p>The public static <code>GetContructor (System.Type, System.Type[])</code> member of the <code>ReflectionUtils</code> class returns a <code>Xamarin.InAppBilling.Reflection.ReflectionUtils.ConstructorDelegate</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#fcd0f17a-71a5-43f8-b471-32579cfc2397'>GetFields</a></td><td style='width:75%' class='def'><p>The public static <code>GetFields (System.Type)</code> member of the <code>ReflectionUtils</code> class returns a <code>System.Collections.Generic.IEnumerable&lt;System.Reflection.FieldInfo&gt;</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#f531e7fd-ceca-4f1d-a2a8-504295a82e13'>GetGenericListElementType</a></td><td style='width:75%' ><p>The public static <code>GetGenericListElementType (System.Type)</code> member of the <code>ReflectionUtils</code> class returns a <code>System.Type</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#4faccad4-baed-44e3-95ef-a3e4c8feb2b1'>GetGenericTypeArguments</a></td><td style='width:75%' class='def'><p>The public static <code>GetGenericTypeArguments (System.Type)</code> member of the <code>ReflectionUtils</code> class returns a <code>System.Type[]</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#9544d245-90d7-483d-893a-8933a0299f79'>GetGetMethod</a></td><td style='width:75%' ><p>The public static <code>GetGetMethod (System.Reflection.FieldInfo)</code> member of the <code>ReflectionUtils</code> class returns a <code>Xamarin.InAppBilling.Reflection.ReflectionUtils.GetDelegate</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#62b047bc-62b9-49b8-9c33-4f22b07f4368'>GetGetMethod</a></td><td style='width:75%' class='def'><p>The public static <code>GetGetMethod (System.Reflection.PropertyInfo)</code> member of the <code>ReflectionUtils</code> class returns a <code>Xamarin.InAppBilling.Reflection.ReflectionUtils.GetDelegate</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#1e442e98-aede-49df-beba-79959009e014'>GetGetMethodByExpression</a></td><td style='width:75%' ><p>The public static <code>GetGetMethodByExpression (System.Reflection.FieldInfo)</code> member of the <code>ReflectionUtils</code> class returns a <code>Xamarin.InAppBilling.Reflection.ReflectionUtils.GetDelegate</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#23770d47-d798-4816-9925-de6f12e65c80'>GetGetMethodByExpression</a></td><td style='width:75%' class='def'><p>The public static <code>GetGetMethodByExpression (System.Reflection.PropertyInfo)</code> member of the <code>ReflectionUtils</code> class returns a <code>Xamarin.InAppBilling.Reflection.ReflectionUtils.GetDelegate</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#638bbcab-ad0f-4352-99c2-2285597c6cc4'>GetGetMethodByReflection</a></td><td style='width:75%' ><p>The public static <code>GetGetMethodByReflection (System.Reflection.FieldInfo)</code> member of the <code>ReflectionUtils</code> class returns a <code>Xamarin.InAppBilling.Reflection.ReflectionUtils.GetDelegate</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#bb481d7f-c1ee-4207-8399-140a27ec672a'>GetGetMethodByReflection</a></td><td style='width:75%' class='def'><p>The public static <code>GetGetMethodByReflection (System.Reflection.PropertyInfo)</code> member of the <code>ReflectionUtils</code> class returns a <code>Xamarin.InAppBilling.Reflection.ReflectionUtils.GetDelegate</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#635e0d55-4dcd-4c0f-b880-08b46734b9bf'>GetGetterMethodInfo</a></td><td style='width:75%' ><p>The public static <code>GetGetterMethodInfo (System.Reflection.PropertyInfo)</code> member of the <code>ReflectionUtils</code> class returns a <code>System.Reflection.MethodInfo</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#801d0d68-9414-448b-abeb-2c853d09ec9b'>GetProperties</a></td><td style='width:75%' class='def'><p>The public static <code>GetProperties (System.Type)</code> member of the <code>ReflectionUtils</code> class returns a <code>System.Collections.Generic.IEnumerable&lt;System.Reflection.PropertyInfo&gt;</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#7af47516-e804-4e14-987d-06f19ea90994'>GetSetMethod</a></td><td style='width:75%' ><p>The public static <code>GetSetMethod (System.Reflection.FieldInfo)</code> member of the <code>ReflectionUtils</code> class returns a <code>Xamarin.InAppBilling.Reflection.ReflectionUtils.SetDelegate</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#5e846f9a-2cf3-4068-a51e-78d003692900'>GetSetMethod</a></td><td style='width:75%' class='def'><p>The public static <code>GetSetMethod (System.Reflection.PropertyInfo)</code> member of the <code>ReflectionUtils</code> class returns a <code>Xamarin.InAppBilling.Reflection.ReflectionUtils.SetDelegate</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#58f97d67-d89b-47ca-b673-e0d36c02abfe'>GetSetMethodByExpression</a></td><td style='width:75%' ><p>The public static <code>GetSetMethodByExpression (System.Reflection.FieldInfo)</code> member of the <code>ReflectionUtils</code> class returns a <code>Xamarin.InAppBilling.Reflection.ReflectionUtils.SetDelegate</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#33d1496c-b517-440c-96c9-fa4017f5acaa'>GetSetMethodByExpression</a></td><td style='width:75%' class='def'><p>The public static <code>GetSetMethodByExpression (System.Reflection.PropertyInfo)</code> member of the <code>ReflectionUtils</code> class returns a <code>Xamarin.InAppBilling.Reflection.ReflectionUtils.SetDelegate</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#217ec0d9-1bfe-443a-ae08-af849e3ef9b0'>GetSetMethodByReflection</a></td><td style='width:75%' ><p>The public static <code>GetSetMethodByReflection (System.Reflection.FieldInfo)</code> member of the <code>ReflectionUtils</code> class returns a <code>Xamarin.InAppBilling.Reflection.ReflectionUtils.SetDelegate</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#48309f63-520f-4686-80f5-3f49e7732607'>GetSetMethodByReflection</a></td><td style='width:75%' class='def'><p>The public static <code>GetSetMethodByReflection (System.Reflection.PropertyInfo)</code> member of the <code>ReflectionUtils</code> class returns a <code>Xamarin.InAppBilling.Reflection.ReflectionUtils.SetDelegate</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#d19a2818-eafc-4128-bea0-3248573d595c'>GetSetterMethodInfo</a></td><td style='width:75%' ><p>The public static <code>GetSetterMethodInfo (System.Reflection.PropertyInfo)</code> member of the <code>ReflectionUtils</code> class returns a <code>System.Reflection.MethodInfo</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#bd0b96a4-26e7-42ae-8cdd-e11d2d1193df'>GetTypeInfo</a></td><td style='width:75%' class='def'><p>The public static <code>GetTypeInfo (System.Type)</code> member of the <code>ReflectionUtils</code> class returns a <code>System.Type</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#e3f9a2c5-327f-428c-a9d0-ea32ffa67de9'>IsAssignableFrom</a></td><td style='width:75%' ><p>The public static <code>IsAssignableFrom (System.Type, System.Type)</code> member of the <code>ReflectionUtils</code> class returns a <code>System.Boolean</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#9e98d740-7d82-479e-9f69-fb1ba10cc4e8'>IsNullableType</a></td><td style='width:75%' class='def'><p>The public static <code>IsNullableType (System.Type)</code> member of the <code>ReflectionUtils</code> class returns a <code>System.Boolean</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#3b047da0-4d16-4c9a-b8e7-94e967b597d0'>IsTypeDictionary</a></td><td style='width:75%' ><p>The public static <code>IsTypeDictionary (System.Type)</code> member of the <code>ReflectionUtils</code> class returns a <code>System.Boolean</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#e0160977-6558-440a-88b3-3b3e1475ebda'>IsTypeGeneric</a></td><td style='width:75%' class='def'><p>The public static <code>IsTypeGeneric (System.Type)</code> member of the <code>ReflectionUtils</code> class returns a <code>System.Boolean</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#fe8ba144-0cfd-4e98-9d0e-3b5fbaaca1b2'>IsTypeGenericeCollectionInterface</a></td><td style='width:75%' ><p>The public static <code>IsTypeGenericeCollectionInterface (System.Type)</code> member of the <code>ReflectionUtils</code> class returns a <code>System.Boolean</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#85229dd8-6cd1-444c-b9f2-e335d2831721'>IsValueType</a></td><td style='width:75%' class='def'><p>The public static <code>IsValueType (System.Type)</code> member of the <code>ReflectionUtils</code> class returns a <code>System.Boolean</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#e569f918-7846-4d35-9c68-aca3ee8c0b4a'>ReflectionUtils</a></td><td style='width:75%' ><p>The public <code>ReflectionUtils ()</code> constructor for the <code>ReflectionUtils</code> class.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#6d304afb-856b-4ba4-8ada-b4149bf17be3'>ToNullableType</a></td><td style='width:75%' class='def'><p>The public static <code>ToNullableType (System.Object, System.Type)</code> member of the <code>ReflectionUtils</code> class returns a <code>System.Object</code> value.</p>
</td></tr></table></p>


---

<a name="a529bb4b-0d3b-4e89-8013-793e0948fe6e"></a>
##Public Static Class ReservedTestProductIDs

###Namespace

[Xamarin.InAppBilling](#ab56f628-3601-4cd6-b1e2-74873a9b2ed8)

###Inherits From

`System.Object`

###Summary

Contains the reserved product IDs use to test In-App Billing via Google Play without actually making a purchase. To test your implementation with static responses, you make an In-app Billing request using  a special item that has a reserved product ID. Each reserved product ID returns a specific static response  from Google Play. No money is transferred when you make In-app Billing requests with the reserved product IDs. Also, you cannot specify the form of payment when you make a billing request with a reserved product ID.
<p><table style='width:100%'><tr><th style='width:25%'>Fields</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#edf43e09-4f7f-4b98-b901-b74792e6b452'>Canceled</a></td><td style='width:75%' ><p>When you make an In-app Billing request with this product ID Google Play responds as though the purchase was canceled. </p>
</td></tr><tr><td style='width:25%' class='term'><a href='#a811cca8-875f-4100-8581-c711f1efee0d'>Purchased</a></td><td style='width:75%' class='def'><p>When you make an In-app Billing request with this product ID, Google Play responds as though you successfully purchased an item.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#54d3bde0-fc01-4988-994d-027aac35728f'>Refunded</a></td><td style='width:75%' ><p>When you make an In-app Billing request with this product ID, Google Play responds as though the purchase was refunded.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#82946414-39a9-41d2-81f5-f5ee1fb2fa15'>Unavailable</a></td><td style='width:75%' class='def'><p>When you make an In-app Billing request with this product ID, Google Play responds as though the item being purchased  was not listed in your application's product list.</p>
</td></tr></table></p>


---

<a name="5d7b3738-d0ca-47aa-bf21-6d4aa1b59051"></a>
##Public Class Resource

###Namespace

[Xamarin.InAppBilling](#ab56f628-3601-4cd6-b1e2-74873a9b2ed8)

###Inherits From

`System.Object`

###Summary

The public `Resource` class inherits from the `System.Object` class and is defined in the `Xamarin.InAppBilling` namespace. It defines no fields, no properties, 2 methods and no events.
<p><table style='width:100%'><tr><th style='width:25%'>Methods</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#ed7159d1-c0bb-43c5-872b-4d494db50c46'>Resource</a></td><td style='width:75%' ><p>The private static <code>Resource ()</code> constructor for the <code>Resource</code> class.</p>
</td></tr></table></p>


---

<a name="6e46a1cf-089e-4410-842b-cef6050c45c7"></a>
##Public Static Class Response

###Namespace

[Xamarin.InAppBilling](#ab56f628-3601-4cd6-b1e2-74873a9b2ed8)

###Inherits From

`System.Object`

###Summary

List of response codes available within the Google Play In-App Billing API.
<p><table style='width:100%'><tr><th style='width:25%'>Fields</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#f4254a87-4e2a-4c61-989c-75e11a3457f1'>BuyIntent</a></td><td style='width:75%' ><p>Gets the buy intent.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#40712cab-ec73-42a4-bb05-71ef68d1585a'>Code</a></td><td style='width:75%' class='def'><p>Gets the response code.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#4aef4552-2ce3-46d4-8a4f-3f3485f460e8'>InAppContinuationToken</a></td><td style='width:75%' ><p>Gets the in app continuation token.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#a1d253ae-a1a8-4513-8e89-e3a58f6f17b5'>InAppDataSignature</a></td><td style='width:75%' class='def'><p>Gets the in app data signature.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#61a6ead1-3227-4ab4-bb58-ae0fdfb8b49f'>InAppDataSignatureList</a></td><td style='width:75%' ><p>Gets the in app data signature list.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#c4f268d0-e477-4c90-a939-2694ab52685a'>InAppPurchaseData</a></td><td style='width:75%' class='def'><p>Gets the in app purchase data.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#4150a2b2-76ec-4437-8d64-f92b0cdbf35e'>InAppPurchaseDataList</a></td><td style='width:75%' ><p>Gets the in app purchase data list.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#51f8400c-04fd-48fc-95c6-4c0bfbce8b57'>InAppPurchaseItemList</a></td><td style='width:75%' class='def'><p>Gets the in app purchase item list.</p>
</td></tr></table></p>


---

<a name="9e6b0e98-9aae-449f-a739-3b32774c3e76"></a>
##Public Class Security

###Namespace

[Xamarin.InAppBilling.Utilities](#809cf835-9470-46b0-9c02-adb4bd944057)

###Inherits From

`System.Object`

###Summary

Utility class to support secure transactions for Google Play In-App Billing
<p><table style='width:100%'><tr><th style='width:25%'>Fields</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#1a41192d-5545-46b9-9b83-498f253b60cd'>KeyFactoryAlgorithm</a></td><td style='width:75%' ><p>The private static constant <code>KeyFactoryAlgorithm</code> field of the <code>Security</code> class holds a <code>System.String</code> value of <code>RSA</code>.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#294359fb-cc16-43a3-a5df-010e593e6cee'>SignatureAlgorithm</a></td><td style='width:75%' class='def'><p>The private static constant <code>SignatureAlgorithm</code> field of the <code>Security</code> class holds a <code>System.String</code> value of <code>SHA1withRSA</code>.</p>
</td></tr></table></p>

<p><table style='width:100%'><tr><th style='width:25%'>Methods</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#8df0e6ae-b628-428e-b039-459f00ea7d4d'>GeneratePublicKey</a></td><td style='width:75%' ><p>Generates the public key.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#75a32fc8-a2a9-4d7a-acce-a9a0868f05b5'>Security</a></td><td style='width:75%' class='def'><p>The public <code>Security ()</code> constructor for the <code>Security</code> class.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#fb7f0e87-0daf-457b-bbb6-e78804ffa522'>Unify</a></td><td style='width:75%' ><p>Recombines the given elements and segments to reconstruct an obfuscated string.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#5d3b6ddd-b9a0-4543-a132-24b781bd3f3c'>Unify</a></td><td style='width:75%' class='def'><p>Recombines the given elements, segments and hashes to reconstruct an obfuscated string.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#94b5a545-929e-4de8-a050-7148e03926f4'>Verify</a></td><td style='width:75%' ><p>Verify the specified publicKey, signedData and signature.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#922a9b7c-be06-46dd-966a-4ff5756fba9c'>VerifyPurchase</a></td><td style='width:75%' class='def'><p>Verifies the purchase.</p>
</td></tr></table></p>


---

<a name="72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f"></a>
##Private Static Class SimpleJson

###Namespace

[Xamarin.InAppBilling](#ab56f628-3601-4cd6-b1e2-74873a9b2ed8)

###Inherits From

`System.Object`

###Summary

This class encodes and decodes JSON strings. Spec. details, see http://www.json.org/ JSON uses Arrays and Objects. These correspond here to the datatypes JsonArray(IList&lt;object&gt;) and JsonObject(IDictionary&lt;string,object&gt;). All numbers are parsed to doubles.
<p><table style='width:100%'><tr><th style='width:25%'>Fields</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#530e3f52-ac04-4ebf-b791-1a2d084fc03d'>_currentJsonSerializerStrategy</a></td><td style='width:75%' ><p>The private static <code>_currentJsonSerializerStrategy</code> field of the <code>SimpleJson</code> class holds a <code>Xamarin.InAppBilling.IJsonSerializerStrategy</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#2f6f7af9-45e6-4530-9382-988d2bb97d3a'>_pocoJsonSerializerStrategy</a></td><td style='width:75%' class='def'><p>The private static <code>_pocoJsonSerializerStrategy</code> field of the <code>SimpleJson</code> class holds a <code>Xamarin.InAppBilling.PocoJsonSerializerStrategy</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#22ff6669-5c9a-4afb-b7f2-01367d2aa7ba'>BUILDER_CAPACITY</a></td><td style='width:75%' ><p>The private static constant <code>BUILDER_CAPACITY</code> field of the <code>SimpleJson</code> class holds a <code>System.Int32</code> value of <code>2000</code>.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#28ede31c-752d-4a69-92fe-0897a6450a28'>EscapeCharacters</a></td><td style='width:75%' class='def'><p>The private static <code>EscapeCharacters</code> field of the <code>SimpleJson</code> class holds a <code>System.Char[]</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#7c27a343-7a34-4632-9252-22173829961b'>EscapeCharactersString</a></td><td style='width:75%' ><p>The private static <code>EscapeCharactersString</code> field of the <code>SimpleJson</code> class holds a <code>System.String</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#11c71cfc-bfff-488c-80f7-12dc2fe2e39d'>EscapeTable</a></td><td style='width:75%' class='def'><p>The private static <code>EscapeTable</code> field of the <code>SimpleJson</code> class holds a <code>System.Char[]</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#cff461bb-a303-45a1-a62f-badb7bd55c65'>TOKEN_COLON</a></td><td style='width:75%' ><p>The private static constant <code>TOKEN_COLON</code> field of the <code>SimpleJson</code> class holds a <code>System.Int32</code> value of <code>5</code>.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#a6040616-3293-45d2-ae18-3d21ff6c022a'>TOKEN_COMMA</a></td><td style='width:75%' class='def'><p>The private static constant <code>TOKEN_COMMA</code> field of the <code>SimpleJson</code> class holds a <code>System.Int32</code> value of <code>6</code>.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#43deb380-4e00-4935-8f76-8d00011e9681'>TOKEN_CURLY_CLOSE</a></td><td style='width:75%' ><p>The private static constant <code>TOKEN_CURLY_CLOSE</code> field of the <code>SimpleJson</code> class holds a <code>System.Int32</code> value of <code>2</code>.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#8998ca3c-0fad-4234-99bd-6254232852af'>TOKEN_CURLY_OPEN</a></td><td style='width:75%' class='def'><p>The private static constant <code>TOKEN_CURLY_OPEN</code> field of the <code>SimpleJson</code> class holds a <code>System.Int32</code> value of <code>1</code>.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#51ed89e1-ef25-462b-9e3e-09ad96938c7b'>TOKEN_FALSE</a></td><td style='width:75%' ><p>The private static constant <code>TOKEN_FALSE</code> field of the <code>SimpleJson</code> class holds a <code>System.Int32</code> value of <code>10</code>.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#4bd27718-eec5-45ed-9490-133fa2cbc4ca'>TOKEN_NONE</a></td><td style='width:75%' class='def'><p>The private static constant <code>TOKEN_NONE</code> field of the <code>SimpleJson</code> class holds a <code>System.Int32</code> value of <code>0</code>.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#156a5da9-10d2-4fca-9510-b5d02a954280'>TOKEN_NULL</a></td><td style='width:75%' ><p>The private static constant <code>TOKEN_NULL</code> field of the <code>SimpleJson</code> class holds a <code>System.Int32</code> value of <code>11</code>.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#e6340248-3250-4fed-8793-e90fd53839ec'>TOKEN_NUMBER</a></td><td style='width:75%' class='def'><p>The private static constant <code>TOKEN_NUMBER</code> field of the <code>SimpleJson</code> class holds a <code>System.Int32</code> value of <code>8</code>.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#23b3422c-5501-4461-8b24-b4f02cef47c2'>TOKEN_SQUARED_CLOSE</a></td><td style='width:75%' ><p>The private static constant <code>TOKEN_SQUARED_CLOSE</code> field of the <code>SimpleJson</code> class holds a <code>System.Int32</code> value of <code>4</code>.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#8d03342e-d8aa-447e-a2b8-3074c0e84770'>TOKEN_SQUARED_OPEN</a></td><td style='width:75%' class='def'><p>The private static constant <code>TOKEN_SQUARED_OPEN</code> field of the <code>SimpleJson</code> class holds a <code>System.Int32</code> value of <code>3</code>.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#5051c82c-c5b9-4513-b81a-d3ac7d484872'>TOKEN_STRING</a></td><td style='width:75%' ><p>The private static constant <code>TOKEN_STRING</code> field of the <code>SimpleJson</code> class holds a <code>System.Int32</code> value of <code>7</code>.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#71908a00-d5cc-4074-ab7e-b0b92fb4708f'>TOKEN_TRUE</a></td><td style='width:75%' class='def'><p>The private static constant <code>TOKEN_TRUE</code> field of the <code>SimpleJson</code> class holds a <code>System.Int32</code> value of <code>9</code>.</p>
</td></tr></table></p>

<p><table style='width:100%'><tr><th style='width:25%'>Properties</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#1920c9b5-d2e0-4627-88f4-463a09003f00'>CurrentJsonSerializerStrategy</a></td><td style='width:75%' ><p>The public static <code>CurrentJsonSerializerStrategy</code> property of the <code>SimpleJson</code> class has a <code>Xamarin.InAppBilling.IJsonSerializerStrategy</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#72501502-7a61-4a5a-b5c6-2a89ce9b8982'>PocoJsonSerializerStrategy</a></td><td style='width:75%' class='def'><p>The public static read only <code>PocoJsonSerializerStrategy</code> property of the <code>SimpleJson</code> class has a <code>Xamarin.InAppBilling.PocoJsonSerializerStrategy</code> value.</p>
</td></tr></table></p>

<p><table style='width:100%'><tr><th style='width:25%'>Methods</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'><a href='#43fdc044-9481-4834-854b-37cda82cd440'>ConvertFromUtf32</a></td><td style='width:75%' ><p>The private static <code>ConvertFromUtf32 (System.Int32)</code> member of the <code>SimpleJson</code> class returns a <code>System.String</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#fa25b7d4-fa90-4a12-94b6-2284bbae8000'>DeserializeObject</a></td><td style='width:75%' class='def'><p>Parses the string json into a value</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#18f5265c-884e-45c3-b948-9b9854c74540'>DeserializeObject</a></td><td style='width:75%' ><p>The public static <code>DeserializeObject (System.String, System.Type)</code> member of the <code>SimpleJson</code> class returns a <code>System.Object</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#7376b15a-5f29-4b57-a35b-92ecb9d5220b'>DeserializeObject</a></td><td style='width:75%' class='def'><p>The public static <code>DeserializeObject (System.String, System.Type, Xamarin.InAppBilling.IJsonSerializerStrategy)</code> member of the <code>SimpleJson</code> class returns a <code>System.Object</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#1ab83c75-a631-4dc7-87bf-2e89412b67a0'>DeserializeObject</a></td><td style='width:75%' ><p>The public static <code>DeserializeObject (System.String, Xamarin.InAppBilling.IJsonSerializerStrategy)</code> member of the <code>SimpleJson</code> class returns a <code>T</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#da112460-7185-4309-b88f-2d80955c5d81'>EatWhitespace</a></td><td style='width:75%' class='def'><p>The private static <code>EatWhitespace (System.Char[], System.Int32&amp;)</code> member of the <code>SimpleJson</code> class.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#427b1d91-75c7-4ffc-8a74-cc2a0c41e05e'>EscapeToJavascriptString</a></td><td style='width:75%' ><p>The public static <code>EscapeToJavascriptString (System.String)</code> member of the <code>SimpleJson</code> class returns a <code>System.String</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#f6ff6834-2997-4116-855c-22dabfe9ae21'>GetLastIndexOfNumber</a></td><td style='width:75%' class='def'><p>The private static <code>GetLastIndexOfNumber (System.Char[], System.Int32)</code> member of the <code>SimpleJson</code> class returns a <code>System.Int32</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#676d92f7-8f3f-44a9-a8ed-8a31496b0b68'>IsNumeric</a></td><td style='width:75%' ><p>Determines if a given object is numeric in any way (can be integer, double, null, etc).</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#930c090c-305d-40a8-ad0f-8a699a223e99'>LookAhead</a></td><td style='width:75%' class='def'><p>The private static <code>LookAhead (System.Char[], System.Int32)</code> member of the <code>SimpleJson</code> class returns a <code>System.Int32</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#426c95c0-30c9-4ecb-9df8-6ae2a1c79dc3'>NextToken</a></td><td style='width:75%' ><p>The private static <code>NextToken (System.Char[], System.Int32&amp;)</code> member of the <code>SimpleJson</code> class returns a <code>System.Int32</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#ea65bac0-b90b-42af-9ceb-5c4056efdbc3'>ParseArray</a></td><td style='width:75%' class='def'><p>The private static <code>ParseArray (System.Char[], System.Int32&amp;, System.Boolean&amp;)</code> member of the <code>SimpleJson</code> class returns a <code>Xamarin.InAppBilling.JsonArray</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#78d2d383-7a63-4317-aca7-d4fdc79d374a'>ParseNumber</a></td><td style='width:75%' ><p>The private static <code>ParseNumber (System.Char[], System.Int32&amp;, System.Boolean&amp;)</code> member of the <code>SimpleJson</code> class returns a <code>System.Object</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#db9514e6-e8a7-4ab9-83df-de206ee8ee01'>ParseObject</a></td><td style='width:75%' class='def'><p>The private static <code>ParseObject (System.Char[], System.Int32&amp;, System.Boolean&amp;)</code> member of the <code>SimpleJson</code> class returns a <code>System.Collections.Generic.IDictionary&lt;System.String,System.Object&gt;</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#5284aa61-f92c-4ebc-80df-f85ab4a174ce'>ParseString</a></td><td style='width:75%' ><p>The private static <code>ParseString (System.Char[], System.Int32&amp;, System.Boolean&amp;)</code> member of the <code>SimpleJson</code> class returns a <code>System.String</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#4d7b06d2-00ff-49ff-a8a8-340c1d8dc0b9'>ParseValue</a></td><td style='width:75%' class='def'><p>The private static <code>ParseValue (System.Char[], System.Int32&amp;, System.Boolean&amp;)</code> member of the <code>SimpleJson</code> class returns a <code>System.Object</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#684f93d5-629a-452f-9e28-d58040a26f22'>SerializeArray</a></td><td style='width:75%' ><p>The private static <code>SerializeArray (Xamarin.InAppBilling.IJsonSerializerStrategy, System.Collections.IEnumerable, System.Text.StringBuilder)</code> member of the <code>SimpleJson</code> class returns a <code>System.Boolean</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#1cf78588-e157-437b-b680-31ea43683d44'>SerializeNumber</a></td><td style='width:75%' class='def'><p>The private static <code>SerializeNumber (System.Object, System.Text.StringBuilder)</code> member of the <code>SimpleJson</code> class returns a <code>System.Boolean</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#87d6934f-60b1-4ebe-8af6-23345ced1067'>SerializeObject</a></td><td style='width:75%' ><p>The public static <code>SerializeObject (System.Object)</code> member of the <code>SimpleJson</code> class returns a <code>System.String</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#b9b98901-5dce-438c-8008-1d7e9ca09cd3'>SerializeObject</a></td><td style='width:75%' class='def'><p>Converts a IDictionary&lt;string,object&gt; / IList&lt;object&gt; object into a JSON string</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#5ccfc8e9-fd12-44b1-99a1-b9262dc029d3'>SerializeObject</a></td><td style='width:75%' ><p>The private static <code>SerializeObject (Xamarin.InAppBilling.IJsonSerializerStrategy, System.Collections.IEnumerable, System.Collections.IEnumerable, System.Text.StringBuilder)</code> member of the <code>SimpleJson</code> class returns a <code>System.Boolean</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#699a8683-8ff4-458e-a41a-c91b8c40c274'>SerializeString</a></td><td style='width:75%' class='def'><p>The private static <code>SerializeString (System.String, System.Text.StringBuilder)</code> member of the <code>SimpleJson</code> class returns a <code>System.Boolean</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#a6b0f1f2-7ca7-47e8-8927-df30e8f8ab71'>SerializeValue</a></td><td style='width:75%' ><p>The private static <code>SerializeValue (Xamarin.InAppBilling.IJsonSerializerStrategy, System.Object, System.Text.StringBuilder)</code> member of the <code>SimpleJson</code> class returns a <code>System.Boolean</code> value.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#328d7c85-5b43-46c9-998f-8956bdf4ea92'>SimpleJson</a></td><td style='width:75%' class='def'><p>The private static <code>SimpleJson ()</code> constructor for the <code>SimpleJson</code> class.</p>
</td></tr><tr><td style='width:25%' class='term'><a href='#96f1b67d-25a2-411c-9117-b5e2018d05cf'>TryDeserializeObject</a></td><td style='width:75%' ><p>Try parsing the json string into a value.</p>
</td></tr></table></p>

<a name="Fields"></a>
#Fields


---

<a name="9715f2f4-0c76-4703-b9cf-6fb827d46817"></a>
##Private _activity

###Value

`Android.App.Activity`

###Member of Type

[InAppBillingServiceConnection](#80c1612e-cf5e-41b3-a216-ca89246674e9)

###Summary

The backing store for the activity.

---

<a name="be218856-1385-41c7-b13f-46400f492070"></a>
##Private _activity

###Value

`Android.App.Activity`

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

The private `_activity` field of the `InAppBillingHandler` class holds a `Android.App.Activity` value.

---

<a name="4bfcbd92-dd06-4cf0-8cc5-36a73b030fd6"></a>
##Private _billingService

###Value

[Com.Android.Vending.Billing.IInAppBillingService](#2c2de562-b76b-4126-8476-c2b22a4ccde1)

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

The private `_billingService` field of the `InAppBillingHandler` class holds a `Com.Android.Vending.Billing.IInAppBillingService` value.

---

<a name="530e3f52-ac04-4ebf-b791-1a2d084fc03d"></a>
##Private Static _currentJsonSerializerStrategy

###Value

[Xamarin.InAppBilling.IJsonSerializerStrategy](#88311aab-37d5-4381-95ce-c4aa0303ac7f)

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

The private static `_currentJsonSerializerStrategy` field of the `SimpleJson` class holds a `Xamarin.InAppBilling.IJsonSerializerStrategy` value.

---

<a name="5b5208b9-3b67-4705-8acb-ff85e1550525"></a>
##Private _members

###Value

`System.Collections.Generic.Dictionary<System.String,System.Object>`

###Member of Type

[JsonObject](#4db2613b-bdf6-4cff-a6da-804b56455cbe)

###Summary

The internal member dictionary.

---

<a name="ffaec3aa-0aae-4025-9ec7-fe8cff259e73"></a>
##Private _payload

###Value

`System.String`

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

The private `_payload` field of the `InAppBillingHandler` class holds a `System.String` value.

---

<a name="2f6f7af9-45e6-4530-9382-988d2bb97d3a"></a>
##Private Static _pocoJsonSerializerStrategy

###Value

[Xamarin.InAppBilling.PocoJsonSerializerStrategy](#1ac2d0dc-01c8-489d-9039-4374c33d0124)

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

The private static `_pocoJsonSerializerStrategy` field of the `SimpleJson` class holds a `Xamarin.InAppBilling.PocoJsonSerializerStrategy` value.

---

<a name="53da338a-73a3-4d45-9ab1-37e452a8b156"></a>
##Private _publicKey

###Value

`System.String`

###Member of Type

[InAppBillingServiceConnection](#80c1612e-cf5e-41b3-a216-ca89246674e9)

###Summary

The backing store for the public key.

---

<a name="0f35d8fc-b414-4b5d-9a7b-528b2a7c8896"></a>
##Private _publicKey

###Value

`System.String`

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

The private `_publicKey` field of the `InAppBillingHandler` class holds a `System.String` value.

---

<a name="63c38554-d7e7-46f0-949b-51b82fe250a2"></a>
##Public Static Constant APIVersion

###Value

`System.Int32` of 3

###Member of Type

[Billing](#3f017bc8-3b0a-4dfa-9fe4-13f5c185b2e9)

###Summary

Gets the API version.

---

<a name="e6e51c88-972e-413e-8919-8d187ec1ab78"></a>
##Static ArrayConstructorParameterTypes

###Value

`System.Type[]`

###Member of Type

[PocoJsonSerializerStrategy](#1ac2d0dc-01c8-489d-9039-4374c33d0124)

###Summary

The  static `ArrayConstructorParameterTypes` field of the `PocoJsonSerializerStrategy` class holds a `System.Type[]` value.

---

<a name="4962182e-cd42-4b71-b1a9-7df25da6e8b8"></a>
##Public Static Constant BillingNotSupported

###Value

[Xamarin.InAppBilling.InAppBillingErrorType](#4d25d02c-abf5-434f-870c-5e52e68701aa) of 0

###Member of Type

[InAppBillingErrorType](#4d25d02c-abf5-434f-870c-5e52e68701aa)

###Summary

In App Billing is not supported on the current device.

---

<a name="f0729a0d-19bc-4c6f-a57c-e6cec26b23fc"></a>
##Public Static Constant BillingUnavailable

###Value

`System.Int32` of 3

###Member of Type

[BillingResult](#ef5b6352-1a73-4ee5-a808-a03384c31044)

###Summary

In-App Billing is not supported on the given Android device.

---

<a name="22ff6669-5c9a-4afb-b7f2-01367d2aa7ba"></a>
##Private Static Constant BUILDER_CAPACITY

###Value

`System.Int32` of 2000

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

The private static constant `BUILDER_CAPACITY` field of the `SimpleJson` class holds a `System.Int32` value of `2000`.

---

<a name="f4254a87-4e2a-4c61-989c-75e11a3457f1"></a>
##Public Static Constant BuyIntent

###Value

`System.String` of BUY_INTENT

###Member of Type

[Response](#6e46a1cf-089e-4410-842b-cef6050c45c7)

###Summary

Gets the buy intent.

---

<a name="a69f9023-8c11-43f1-8938-1d999179966d"></a>
##Private BuyProductError

###Value

[Xamarin.InAppBilling.InAppBillingHandler.BuyProductErrorDelegate](#f9e8ef42-cdf1-4484-81d8-5a7e438e4294)

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

The private `BuyProductError` field of the `InAppBillingHandler` class holds a `Xamarin.InAppBilling.InAppBillingHandler.BuyProductErrorDelegate` value.

---

<a name="edf43e09-4f7f-4b98-b901-b74792e6b452"></a>
##Public Static Constant Canceled

###Value

`System.String` of android.test.canceled

###Member of Type

[ReservedTestProductIDs](#a529bb4b-0d3b-4e89-8013-793e0948fe6e)

###Summary

When you make an In-app Billing request with this product ID Google Play responds as though the purchase was canceled. 
###Remarks

This can occur when an error is encountered in the order process, such as an invalid credit card, or when you  cancel a user's order before it is charged.

---

<a name="40712cab-ec73-42a4-bb05-71ef68d1585a"></a>
##Public Static Constant Code

###Value

`System.String` of RESPONSE_CODE

###Member of Type

[Response](#6e46a1cf-089e-4410-842b-cef6050c45c7)

###Summary

Gets the response code.

---

<a name="3225d596-d807-4ea9-9dae-328b671d630c"></a>
##ConstructorCache

###Value

`System.Collections.Generic.IDictionary<System.Type,Xamarin.InAppBilling.Reflection.ReflectionUtils.ConstructorDelegate>`

###Member of Type

[PocoJsonSerializerStrategy](#1ac2d0dc-01c8-489d-9039-4374c33d0124)

###Summary

The  `ConstructorCache` field of the `PocoJsonSerializerStrategy` class holds a `System.Collections.Generic.IDictionary<System.Type,Xamarin.InAppBilling.Reflection.ReflectionUtils.ConstructorDelegate>` value.

---

<a name="257beac5-3438-4014-bda2-bc9ab5b69808"></a>
##Private Static Constant descriptor

###Value

`System.String` of com.android.vending.billing.IInAppBillingService

###Member of Type

[IInAppBillingServiceStub](#e452919a-6bfb-4cf1-b5e6-88131704b783)

###Summary

The private static constant `descriptor` field of the `IInAppBillingServiceStub` class holds a `System.String` value of `com.android.vending.billing.IInAppBillingService`.

---

<a name="4d7c484d-d3d7-48b2-a724-872b99217444"></a>
##Public Static Constant DeveloperError

###Value

`System.Int32` of 5

###Member of Type

[BillingResult](#ef5b6352-1a73-4ee5-a808-a03384c31044)

###Summary

An invalid argument has been passed to the API or the app was not correctly signed, properly set up for In-app Billing in Google Play Dashboard, or does not have the necessary permissions in its manifest.

---

<a name="9fa7828d-2c70-433e-9917-5cf158b9da9f"></a>
##Private Static EmptyObjects

###Value

`System.Object[]`

###Member of Type

[ReflectionUtils](#6e848a6a-ea7d-4af0-b023-cd70a7e96d45)

###Summary

The private static `EmptyObjects` field of the `ReflectionUtils` class holds a `System.Object[]` value.

---

<a name="aebf7507-9b38-4bae-96aa-395ace698c96"></a>
##Static EmptyTypes

###Value

`System.Type[]`

###Member of Type

[PocoJsonSerializerStrategy](#1ac2d0dc-01c8-489d-9039-4374c33d0124)

###Summary

The  static `EmptyTypes` field of the `PocoJsonSerializerStrategy` class holds a `System.Type[]` value.

---

<a name="0ce665f3-7763-4068-88df-3ddeb4698e4d"></a>
##Public Static Constant Error

###Value

`System.Int32` of 6

###Member of Type

[BillingResult](#ef5b6352-1a73-4ee5-a808-a03384c31044)

###Summary

A fatal error occurred during an API action.

---

<a name="28ede31c-752d-4a69-92fe-0897a6450a28"></a>
##Private Static EscapeCharacters

###Value

`System.Char[]`

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

The private static `EscapeCharacters` field of the `SimpleJson` class holds a `System.Char[]` value.

---

<a name="7c27a343-7a34-4632-9252-22173829961b"></a>
##Private Static EscapeCharactersString

###Value

`System.String`

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

The private static `EscapeCharactersString` field of the `SimpleJson` class holds a `System.String` value.

---

<a name="11c71cfc-bfff-488c-80f7-12dc2fe2e39d"></a>
##Private Static EscapeTable

###Value

`System.Char[]`

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

The private static `EscapeTable` field of the `SimpleJson` class holds a `System.Char[]` value.

---

<a name="68ddbe44-da25-4e40-a158-1d7df0e9b1db"></a>
##GetCache

###Value

`System.Collections.Generic.IDictionary<System.Type,System.Collections.Generic.IDictionary<System.String,Xamarin.InAppBilling.Reflection.ReflectionUtils.GetDelegate>>`

###Member of Type

[PocoJsonSerializerStrategy](#1ac2d0dc-01c8-489d-9039-4374c33d0124)

###Summary

The  `GetCache` field of the `PocoJsonSerializerStrategy` class holds a `System.Collections.Generic.IDictionary<System.Type,System.Collections.Generic.IDictionary<System.String,Xamarin.InAppBilling.Reflection.ReflectionUtils.GetDelegate>>` value.

---

<a name="eb7f93f4-dc3a-44f6-b4b2-b427e0f6204c"></a>
##Private InAppBillingProcesingError

###Value

[Xamarin.InAppBilling.InAppBillingHandler.InAppBillingProcessingErrorDelegate](#7b5d0609-2f0c-4def-b7a4-27677dc3798b)

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

The private `InAppBillingProcesingError` field of the `InAppBillingHandler` class holds a `Xamarin.InAppBilling.InAppBillingHandler.InAppBillingProcessingErrorDelegate` value.

---

<a name="4aef4552-2ce3-46d4-8a4f-3f3485f460e8"></a>
##Public Static Constant InAppContinuationToken

###Value

`System.String` of INAPP_CONTINUATION_TOKEN

###Member of Type

[Response](#6e46a1cf-089e-4410-842b-cef6050c45c7)

###Summary

Gets the in app continuation token.

---

<a name="a1d253ae-a1a8-4513-8e89-e3a58f6f17b5"></a>
##Public Static Constant InAppDataSignature

###Value

`System.String` of INAPP_DATA_SIGNATURE

###Member of Type

[Response](#6e46a1cf-089e-4410-842b-cef6050c45c7)

###Summary

Gets the in app data signature.

---

<a name="61a6ead1-3227-4ab4-bb58-ae0fdfb8b49f"></a>
##Public Static Constant InAppDataSignatureList

###Value

`System.String` of INAPP_DATA_SIGNATURE_LIST

###Member of Type

[Response](#6e46a1cf-089e-4410-842b-cef6050c45c7)

###Summary

Gets the in app data signature list.

---

<a name="c4f268d0-e477-4c90-a939-2694ab52685a"></a>
##Public Static Constant InAppPurchaseData

###Value

`System.String` of INAPP_PURCHASE_DATA

###Member of Type

[Response](#6e46a1cf-089e-4410-842b-cef6050c45c7)

###Summary

Gets the in app purchase data.

---

<a name="4150a2b2-76ec-4437-8d64-f92b0cdbf35e"></a>
##Public Static Constant InAppPurchaseDataList

###Value

`System.String` of INAPP_PURCHASE_DATA_LIST

###Member of Type

[Response](#6e46a1cf-089e-4410-842b-cef6050c45c7)

###Summary

Gets the in app purchase data list.

---

<a name="51f8400c-04fd-48fc-95c6-4c0bfbce8b57"></a>
##Public Static Constant InAppPurchaseItemList

###Value

`System.String` of INAPP_PURCHASE_ITEM_LIST

###Member of Type

[Response](#6e46a1cf-089e-4410-842b-cef6050c45c7)

###Summary

Gets the in app purchase item list.

---

<a name="8b5e1098-4729-4b4f-af3a-e77a93d9bd09"></a>
##Private Static Iso8601Format

###Value

`System.String[]`

###Member of Type

[PocoJsonSerializerStrategy](#1ac2d0dc-01c8-489d-9039-4374c33d0124)

###Summary

The private static `Iso8601Format` field of the `PocoJsonSerializerStrategy` class holds a `System.String[]` value.

---

<a name="b2559685-8241-41a9-abdd-61ff3e8bf6e8"></a>
##Public Static Constant ItemAlreadyOwned

###Value

`System.Int32` of 7

###Member of Type

[BillingResult](#ef5b6352-1a73-4ee5-a808-a03384c31044)

###Summary

The user already owns the given item.

---

<a name="6b179f85-9d91-45dd-b86b-bc8d0332817b"></a>
##Public Static Constant ItemIdList

###Value

`System.String` of ITEM_ID_LIST

###Member of Type

[Billing](#3f017bc8-3b0a-4dfa-9fe4-13f5c185b2e9)

###Summary

Gets the item identifier list.

---

<a name="dcd61a06-70a3-42de-8039-afada9b728fc"></a>
##Public Static Constant ItemNotOwned

###Value

`System.Int32` of 8

###Member of Type

[BillingResult](#ef5b6352-1a73-4ee5-a808-a03384c31044)

###Summary

The given item has not been purchased by the user.

---

<a name="494866fb-aed3-4c6f-8bb5-7c6ca825853b"></a>
##Public Static Constant ItemUnavailable

###Value

`System.Int32` of 4

###Member of Type

[BillingResult](#ef5b6352-1a73-4ee5-a808-a03384c31044)

###Summary

The requested item is unavailable for purchase.

---

<a name="1a41192d-5545-46b9-9b83-498f253b60cd"></a>
##Private Static Constant KeyFactoryAlgorithm

###Value

`System.String` of RSA

###Member of Type

[Security](#9e6b0e98-9aae-449f-a739-3b32774c3e76)

###Summary

The private static constant `KeyFactoryAlgorithm` field of the `Security` class holds a `System.String` value of `RSA`.

---

<a name="750277ee-52c3-4dd1-bccd-fd2063c539c3"></a>
##Public Static Constant OK

###Value

`System.Int32` of 0

###Member of Type

[BillingResult](#ef5b6352-1a73-4ee5-a808-a03384c31044)

###Summary

The transaction completed successfully.

---

<a name="0e5ec3af-96a0-4f6f-a3be-088178f29eb6"></a>
##Private OnConnected

###Value

[Xamarin.InAppBilling.InAppBillingServiceConnection.OnConnectedDelegate](#f81adae0-3a0b-42a2-a3cf-1136a9a6c0c4)

###Member of Type

[InAppBillingServiceConnection](#80c1612e-cf5e-41b3-a216-ca89246674e9)

###Summary

The private `OnConnected` field of the `InAppBillingServiceConnection` class holds a `Xamarin.InAppBilling.InAppBillingServiceConnection.OnConnectedDelegate` value.

---

<a name="e20915e0-631e-461f-98d7-7487bd12ffbe"></a>
##Private OnDisconnected

###Value

[Xamarin.InAppBilling.InAppBillingServiceConnection.OnDisconnectedDelegate](#6187252d-c6be-4eb8-8bac-62d7efeeca97)

###Member of Type

[InAppBillingServiceConnection](#80c1612e-cf5e-41b3-a216-ca89246674e9)

###Summary

The private `OnDisconnected` field of the `InAppBillingServiceConnection` class holds a `Xamarin.InAppBilling.InAppBillingServiceConnection.OnDisconnectedDelegate` value.

---

<a name="f2ab75ec-aeb4-4733-ae04-60d2cfa270de"></a>
##Private OnGetProductsError

###Value

[Xamarin.InAppBilling.InAppBillingHandler.OnGetProductsErrorDelegate](#d6aede4f-36f7-4c26-8e11-e25f23ee3c95)

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

The private `OnGetProductsError` field of the `InAppBillingHandler` class holds a `Xamarin.InAppBilling.InAppBillingHandler.OnGetProductsErrorDelegate` value.

---

<a name="c03fc43d-7717-46dc-b1e6-3f220e0a9ae9"></a>
##Private OnInAppBillingError

###Value

[Xamarin.InAppBilling.InAppBillingServiceConnection.OnInAppBillingErrorDelegate](#b2fa7c05-cb97-400a-a254-a2f0e67a90f2)

###Member of Type

[InAppBillingServiceConnection](#80c1612e-cf5e-41b3-a216-ca89246674e9)

###Summary

The private `OnInAppBillingError` field of the `InAppBillingServiceConnection` class holds a `Xamarin.InAppBilling.InAppBillingServiceConnection.OnInAppBillingErrorDelegate` value.

---

<a name="6eed2e6b-6d3e-4810-aed9-116e3573199c"></a>
##Private OnInvalidOwnedItemsBundleReturned

###Value

[Xamarin.InAppBilling.InAppBillingHandler.OnInvalidOwnedItemsBundleReturnedDelegate](#8758fba9-3d12-446b-ab23-b71e32b92354)

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

The private `OnInvalidOwnedItemsBundleReturned` field of the `InAppBillingHandler` class holds a `Xamarin.InAppBilling.InAppBillingHandler.OnInvalidOwnedItemsBundleReturnedDelegate` value.

---

<a name="ca025071-7739-4703-b802-fec251d52514"></a>
##Private OnProductPurchased

###Value

[Xamarin.InAppBilling.InAppBillingHandler.OnProductPurchasedDelegate](#2ed54e69-6056-4702-86c8-ae945939f92b)

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

The private `OnProductPurchased` field of the `InAppBillingHandler` class holds a `Xamarin.InAppBilling.InAppBillingHandler.OnProductPurchasedDelegate` value.

---

<a name="12fd8c50-d215-40cd-8eac-aafcd49db91f"></a>
##Private OnProductPurchasedError

###Value

[Xamarin.InAppBilling.InAppBillingHandler.OnProductPurchaseErrorDelegate](#09a88fb4-2a98-4e4b-a2c6-aa7749c450cc)

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

The private `OnProductPurchasedError` field of the `InAppBillingHandler` class holds a `Xamarin.InAppBilling.InAppBillingHandler.OnProductPurchaseErrorDelegate` value.

---

<a name="43a8065a-823e-4e96-83a0-a25f6699d9e3"></a>
##Private OnPurchaseConsumed

###Value

[Xamarin.InAppBilling.InAppBillingHandler.OnPurchaseConsumedDelegate](#fd7ef9f9-bb27-4eff-a319-a3193670f137)

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

The private `OnPurchaseConsumed` field of the `InAppBillingHandler` class holds a `Xamarin.InAppBilling.InAppBillingHandler.OnPurchaseConsumedDelegate` value.

---

<a name="f6f74317-715a-4d3a-9dde-7806899a1fa2"></a>
##Private OnPurchaseConsumedError

###Value

[Xamarin.InAppBilling.InAppBillingHandler.OnPurchaseConsumedErrorDelegate](#db1cca38-9f7c-4d1a-97c3-e9da2b715763)

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

The private `OnPurchaseConsumedError` field of the `InAppBillingHandler` class holds a `Xamarin.InAppBilling.InAppBillingHandler.OnPurchaseConsumedErrorDelegate` value.

---

<a name="72186108-e70e-4127-95aa-88e245419573"></a>
##Private OnPurchaseFailedValidation

###Value

[Xamarin.InAppBilling.InAppBillingHandler.OnPurchaseFailedValidationDelegate](#e34de5bc-5059-4b1c-820c-c3a2363b472f)

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

The private `OnPurchaseFailedValidation` field of the `InAppBillingHandler` class holds a `Xamarin.InAppBilling.InAppBillingHandler.OnPurchaseFailedValidationDelegate` value.

---

<a name="f6ddea15-f29d-4f42-82e1-ff78d4fcbbd1"></a>
##Private OnUserCanceled

###Value

[Xamarin.InAppBilling.InAppBillingHandler.OnUserCanceledDelegate](#8e757b02-94e2-431f-bfe3-56483c436f0e)

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

The private `OnUserCanceled` field of the `InAppBillingHandler` class holds a `Xamarin.InAppBilling.InAppBillingHandler.OnUserCanceledDelegate` value.

---

<a name="57fc1696-3e5c-4e34-ab13-6fc3d2c1e886"></a>
##Public Static Constant Product

###Value

`System.String` of inapp

###Member of Type

[ItemType](#cb80eb77-1e36-46b1-9262-60453abc97dc)

###Summary

A standard consumable or non-consumable product.

---

<a name="a811cca8-875f-4100-8581-c711f1efee0d"></a>
##Public Static Constant Purchased

###Value

`System.String` of android.test.purchased

###Member of Type

[ReservedTestProductIDs](#a529bb4b-0d3b-4e89-8013-793e0948fe6e)

###Summary

When you make an In-app Billing request with this product ID, Google Play responds as though you successfully purchased an item.
###Remarks

The response includes a JSON string, which contains fake purchase information (for example, a fake order ID). In some cases,  the JSON string is signed and the response includes the signature so you can test your signature verification implementation  using these responses.

---

<a name="53e86237-1467-44d4-9dc0-5d527f931fd6"></a>
##Private Static Constant PurchaseRequestCode

###Value

`System.Int32` of 1001

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

The private static constant `PurchaseRequestCode` field of the `InAppBillingHandler` class holds a `System.Int32` value of `1001`.

---

<a name="a6a7272e-5f6a-4a03-8298-b5fab81604a6"></a>
##Private QueryInventoryError

###Value

[Xamarin.InAppBilling.InAppBillingHandler.QueryInventoryErrorDelegate](#2debbb37-b92f-4714-92c2-47510bad06c2)

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

The private `QueryInventoryError` field of the `InAppBillingHandler` class holds a `Xamarin.InAppBilling.InAppBillingHandler.QueryInventoryErrorDelegate` value.

---

<a name="54d3bde0-fc01-4988-994d-027aac35728f"></a>
##Public Static Constant Refunded

###Value

`System.String` of android.test.refunded

###Member of Type

[ReservedTestProductIDs](#a529bb4b-0d3b-4e89-8013-793e0948fe6e)

###Summary

When you make an In-app Billing request with this product ID, Google Play responds as though the purchase was refunded.
###Remarks

Refunds cannot be initiated through Google Play's in-app billing service. Refunds must be initiated by you  (the merchant). After you process a refund request through your Google Wallet merchant account, a refund message is  sent to your application by Google Play. This occurs only when Google Play gets notification from Google Wallet that a refund has been made.

---

<a name="b45157bb-f222-4b9d-af9a-ec27edc0bd2e"></a>
##Private Static salt

###Value

`System.Byte[]`

###Member of Type

[Crypto](#7b127d1b-7daa-4f26-b23a-5e7236a4af07)

###Summary

The private static `salt` field of the `Crypto` class holds a `System.Byte[]` value.

---

<a name="e7d79702-9549-4279-9963-b3955c443ec8"></a>
##Public Static Constant ServiceUnavailable

###Value

`System.Int32` of 2

###Member of Type

[BillingResult](#ef5b6352-1a73-4ee5-a808-a03384c31044)

###Summary

Network connection is down.

---

<a name="844cffe1-1f93-4b16-a521-fab59cee8999"></a>
##SetCache

###Value

`System.Collections.Generic.IDictionary<System.Type,System.Collections.Generic.IDictionary<System.String,System.Collections.Generic.KeyValuePair<System.Type,Xamarin.InAppBilling.Reflection.ReflectionUtils.SetDelegate>>>`

###Member of Type

[PocoJsonSerializerStrategy](#1ac2d0dc-01c8-489d-9039-4374c33d0124)

###Summary

The  `SetCache` field of the `PocoJsonSerializerStrategy` class holds a `System.Collections.Generic.IDictionary<System.Type,System.Collections.Generic.IDictionary<System.String,System.Collections.Generic.KeyValuePair<System.Type,Xamarin.InAppBilling.Reflection.ReflectionUtils.SetDelegate>>>` value.

---

<a name="294359fb-cc16-43a3-a5df-010e593e6cee"></a>
##Private Static Constant SignatureAlgorithm

###Value

`System.String` of SHA1withRSA

###Member of Type

[Security](#9e6b0e98-9aae-449f-a739-3b32774c3e76)

###Summary

The private static constant `SignatureAlgorithm` field of the `Security` class holds a `System.String` value of `SHA1withRSA`.

---

<a name="c1efd7b6-1eaa-4584-b5e6-b063f95bc09f"></a>
##Public Static Constant SkuDetailsList

###Value

`System.String` of DETAILS_LIST

###Member of Type

[Billing](#3f017bc8-3b0a-4dfa-9fe4-13f5c185b2e9)

###Summary

Gets the sku details list.

---

<a name="3c39d52e-c47e-4a9c-b6f5-50b94a9b40c2"></a>
##Public Static Constant Subscription

###Value

`System.String` of subs

###Member of Type

[ItemType](#cb80eb77-1e36-46b1-9262-60453abc97dc)

###Summary

A subscription product such as a magazine.

---

<a name="3ed0c13d-a683-4358-928f-3d7740fa2040"></a>
##Public Static Constant SubscriptionsNotSupported

###Value

[Xamarin.InAppBilling.InAppBillingErrorType](#4d25d02c-abf5-434f-870c-5e52e68701aa) of 1

###Member of Type

[InAppBillingErrorType](#4d25d02c-abf5-434f-870c-5e52e68701aa)

###Summary

Subscriptions are not supported on the current device.

---

<a name="f18c6f48-90e6-4e8f-a56c-10fc62890032"></a>
##Private Static Constant Tag

###Value

`System.String` of Iab Helper

###Member of Type

[InAppBillingServiceConnection](#80c1612e-cf5e-41b3-a216-ca89246674e9)

###Summary

The constant identifier from the [InAppBillingServiceConnection](#80c1612e-cf5e-41b3-a216-ca89246674e9)

---

<a name="94429889-8685-4b30-a148-74857b31e725"></a>
##Private Static Constant Tag

###Value

`System.String` of InApp-Billing

###Member of Type

[Logger](#4e163874-6eb3-4f60-8c44-9e0260d939fb)

###Summary

The private static constant `Tag` field of the `Logger` class holds a `System.String` value of `InApp-Billing`.

---

<a name="cff461bb-a303-45a1-a62f-badb7bd55c65"></a>
##Private Static Constant TOKEN_COLON

###Value

`System.Int32` of 5

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

The private static constant `TOKEN_COLON` field of the `SimpleJson` class holds a `System.Int32` value of `5`.

---

<a name="a6040616-3293-45d2-ae18-3d21ff6c022a"></a>
##Private Static Constant TOKEN_COMMA

###Value

`System.Int32` of 6

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

The private static constant `TOKEN_COMMA` field of the `SimpleJson` class holds a `System.Int32` value of `6`.

---

<a name="43deb380-4e00-4935-8f76-8d00011e9681"></a>
##Private Static Constant TOKEN_CURLY_CLOSE

###Value

`System.Int32` of 2

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

The private static constant `TOKEN_CURLY_CLOSE` field of the `SimpleJson` class holds a `System.Int32` value of `2`.

---

<a name="8998ca3c-0fad-4234-99bd-6254232852af"></a>
##Private Static Constant TOKEN_CURLY_OPEN

###Value

`System.Int32` of 1

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

The private static constant `TOKEN_CURLY_OPEN` field of the `SimpleJson` class holds a `System.Int32` value of `1`.

---

<a name="51ed89e1-ef25-462b-9e3e-09ad96938c7b"></a>
##Private Static Constant TOKEN_FALSE

###Value

`System.Int32` of 10

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

The private static constant `TOKEN_FALSE` field of the `SimpleJson` class holds a `System.Int32` value of `10`.

---

<a name="4bd27718-eec5-45ed-9490-133fa2cbc4ca"></a>
##Private Static Constant TOKEN_NONE

###Value

`System.Int32` of 0

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

The private static constant `TOKEN_NONE` field of the `SimpleJson` class holds a `System.Int32` value of `0`.

---

<a name="156a5da9-10d2-4fca-9510-b5d02a954280"></a>
##Private Static Constant TOKEN_NULL

###Value

`System.Int32` of 11

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

The private static constant `TOKEN_NULL` field of the `SimpleJson` class holds a `System.Int32` value of `11`.

---

<a name="e6340248-3250-4fed-8793-e90fd53839ec"></a>
##Private Static Constant TOKEN_NUMBER

###Value

`System.Int32` of 8

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

The private static constant `TOKEN_NUMBER` field of the `SimpleJson` class holds a `System.Int32` value of `8`.

---

<a name="23b3422c-5501-4461-8b24-b4f02cef47c2"></a>
##Private Static Constant TOKEN_SQUARED_CLOSE

###Value

`System.Int32` of 4

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

The private static constant `TOKEN_SQUARED_CLOSE` field of the `SimpleJson` class holds a `System.Int32` value of `4`.

---

<a name="8d03342e-d8aa-447e-a2b8-3074c0e84770"></a>
##Private Static Constant TOKEN_SQUARED_OPEN

###Value

`System.Int32` of 3

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

The private static constant `TOKEN_SQUARED_OPEN` field of the `SimpleJson` class holds a `System.Int32` value of `3`.

---

<a name="5051c82c-c5b9-4513-b81a-d3ac7d484872"></a>
##Private Static Constant TOKEN_STRING

###Value

`System.Int32` of 7

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

The private static constant `TOKEN_STRING` field of the `SimpleJson` class holds a `System.Int32` value of `7`.

---

<a name="71908a00-d5cc-4074-ab7e-b0b92fb4708f"></a>
##Private Static Constant TOKEN_TRUE

###Value

`System.Int32` of 9

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

The private static constant `TOKEN_TRUE` field of the `SimpleJson` class holds a `System.Int32` value of `9`.

---

<a name="eb933e8c-aa79-47f3-b4c1-0e8683774254"></a>
##Static Constant TransactionConsumePurchase

###Value

`System.Int32` of 5

###Member of Type

[IInAppBillingServiceStub](#e452919a-6bfb-4cf1-b5e6-88131704b783)

###Summary

The  static constant `TransactionConsumePurchase` field of the `IInAppBillingServiceStub` class holds a `System.Int32` value of `5`.

---

<a name="cbb929c2-0dc1-45c8-a672-089768d163a4"></a>
##Static Constant TransactionGetBuyIntent

###Value

`System.Int32` of 3

###Member of Type

[IInAppBillingServiceStub](#e452919a-6bfb-4cf1-b5e6-88131704b783)

###Summary

The  static constant `TransactionGetBuyIntent` field of the `IInAppBillingServiceStub` class holds a `System.Int32` value of `3`.

---

<a name="bf1cbef8-ec7d-4af5-961e-bf1b8e8c75e9"></a>
##Static Constant TransactionGetPurchases

###Value

`System.Int32` of 4

###Member of Type

[IInAppBillingServiceStub](#e452919a-6bfb-4cf1-b5e6-88131704b783)

###Summary

The  static constant `TransactionGetPurchases` field of the `IInAppBillingServiceStub` class holds a `System.Int32` value of `4`.

---

<a name="f7dff411-83ae-4471-914b-4eb7f2af8519"></a>
##Static Constant TransactionGetSkuDetails

###Value

`System.Int32` of 2

###Member of Type

[IInAppBillingServiceStub](#e452919a-6bfb-4cf1-b5e6-88131704b783)

###Summary

The  static constant `TransactionGetSkuDetails` field of the `IInAppBillingServiceStub` class holds a `System.Int32` value of `2`.

---

<a name="16b36d2d-6054-41ae-8938-57d36251d157"></a>
##Static Constant TransactionIsBillingSupported

###Value

`System.Int32` of 1

###Member of Type

[IInAppBillingServiceStub](#e452919a-6bfb-4cf1-b5e6-88131704b783)

###Summary

The  static constant `TransactionIsBillingSupported` field of the `IInAppBillingServiceStub` class holds a `System.Int32` value of `1`.

---

<a name="82946414-39a9-41d2-81f5-f5ee1fb2fa15"></a>
##Public Static Constant Unavailable

###Value

`System.String` of android.test.item_unavailable

###Member of Type

[ReservedTestProductIDs](#a529bb4b-0d3b-4e89-8013-793e0948fe6e)

###Summary

When you make an In-app Billing request with this product ID, Google Play responds as though the item being purchased  was not listed in your application's product list.

---

<a name="bbd18cbb-3bdf-4cdd-910e-0d6e69718574"></a>
##Public Static Constant UnknownError

###Value

[Xamarin.InAppBilling.InAppBillingErrorType](#4d25d02c-abf5-434f-870c-5e52e68701aa) of 2

###Member of Type

[InAppBillingErrorType](#4d25d02c-abf5-434f-870c-5e52e68701aa)

###Summary

An unknown error has occurred.

---

<a name="04f71f6a-fddc-4281-a019-b6490248d893"></a>
##Public Static Constant UserCancelled

###Value

`System.Int32` of 1

###Member of Type

[BillingResult](#ef5b6352-1a73-4ee5-a808-a03384c31044)

###Summary

The user canceled the transaction.
<a name="Properties"></a>
#Properties


---

<a name="780cf78a-b27c-4335-a607-f37e53dbdfaf"></a>
##Public Read Only BillingHandler

###Return Type

[Xamarin.InAppBilling.InAppBillingHandler](#7789afcb-6894-431d-8675-dd6bd066b6b8)

###Member of Type

[InAppBillingServiceConnection](#80c1612e-cf5e-41b3-a216-ca89246674e9)

###Summary

Gets the [InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08) used to communicate with the Google Play Service
###Return Value

The billing handler.

---

<a name="ccc6bd37-37cb-4efe-b8e6-3e36b7c77f6e"></a>
##Public Read Only Connected

###Return Type

`System.Boolean`

###Member of Type

[InAppBillingServiceConnection](#80c1612e-cf5e-41b3-a216-ca89246674e9)

###Summary

Gets a value indicating whether this [InAppBillingServiceConnection](#80c1612e-cf5e-41b3-a216-ca89246674e9) is connected to the Google Play service
###Return Value

`true` if connected; otherwise, `false`.

---

<a name="a7a332aa-7b7b-471f-9b22-ee1990a99d59"></a>
##Public Virtual Read Only Count

###Return Type

`System.Int32`

###Member of Type

[JsonObject](#4db2613b-bdf6-4cff-a6da-804b56455cbe)

###Summary

Gets the count.
###Return Value

The count.

---

<a name="1920c9b5-d2e0-4627-88f4-463a09003f00"></a>
##Public Static CurrentJsonSerializerStrategy

###Return Type

[Xamarin.InAppBilling.IJsonSerializerStrategy](#88311aab-37d5-4381-95ce-c4aa0303ac7f)

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

The public static `CurrentJsonSerializerStrategy` property of the `SimpleJson` class has a `Xamarin.InAppBilling.IJsonSerializerStrategy` value.

---

<a name="6516623f-d7c9-46e5-9722-99e6bed4c0db"></a>
##Public Description

###Return Type

`System.String`

###Member of Type

[Product](#98d67a2a-c250-4548-8a90-9e3722fef483)

###Summary

Gets or sets the description.
###Return Value

The description.

---

<a name="a95065b5-4c5f-4c19-a287-e6ddef7de8bd"></a>
##Public DeveloperPayload

###Return Type

`System.String`

###Member of Type

[Purchase](#03489ba4-1d9d-4430-8e09-b3bff9875fac)

###Summary

Gets or sets the developer payload.
###Return Value

The developer payload.

---

<a name="5080c2e7-34bf-4ba3-99dd-4e9580d25d9a"></a>
##Public Virtual Read Only IsReadOnly

###Return Type

`System.Boolean`

###Member of Type

[JsonObject](#4db2613b-bdf6-4cff-a6da-804b56455cbe)

###Summary

Gets a value indicating whether this instance is read only.
###Return Value

	`true` if this instance is read only; otherwise, `false`.

---

<a name="7f814223-e53e-4a5c-82ee-41045b92d8d6"></a>
##Public Read Only Item(System.Int32)

###Return Type

`System.Object`

###Member of Type

[JsonObject](#4db2613b-bdf6-4cff-a6da-804b56455cbe)

###Summary

Gets the `Object` at the specified index.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>index</td><td style='width:75%' ><p>The <code>index</code> parameter of the Item method takes a <code>System.Int32</code> value. </p>
</td></tr></table></p>


---

<a name="1b0bbab2-b2cc-4377-85d1-3c8b906ef621"></a>
##Public Virtual Item(System.String)

###Return Type

`System.Object`

###Member of Type

[JsonObject](#4db2613b-bdf6-4cff-a6da-804b56455cbe)

###Summary

Gets or sets the `Object` with the specified key.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>key</td><td style='width:75%' ><p>The <code>key</code> parameter of the Item method takes a <code>System.String</code> value. </p>
</td></tr></table></p>


---

<a name="bcdd8bd4-d94f-49d1-82f1-beafc2314646"></a>
##Public Virtual Read Only Keys

###Return Type

`System.Collections.Generic.ICollection<System.String>`

###Member of Type

[JsonObject](#4db2613b-bdf6-4cff-a6da-804b56455cbe)

###Summary

Gets the keys.
###Return Value

The keys.

---

<a name="5db954bd-d6eb-4487-b830-9a174df85e2b"></a>
##Public OrderId

###Return Type

`System.String`

###Member of Type

[Purchase](#03489ba4-1d9d-4430-8e09-b3bff9875fac)

###Summary

Gets or sets the order identifier.
###Return Value

The order identifier.

---

<a name="1e2d2e03-8f8e-45f5-95bd-a39fea754d6f"></a>
##Public PackageName

###Return Type

`System.String`

###Member of Type

[Purchase](#03489ba4-1d9d-4430-8e09-b3bff9875fac)

###Summary

Gets or sets the name of the package.
###Return Value

The name of the package.

---

<a name="72501502-7a61-4a5a-b5c6-2a89ce9b8982"></a>
##Public Static Read Only PocoJsonSerializerStrategy

###Return Type

[Xamarin.InAppBilling.PocoJsonSerializerStrategy](#1ac2d0dc-01c8-489d-9039-4374c33d0124)

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

The public static read only `PocoJsonSerializerStrategy` property of the `SimpleJson` class has a `Xamarin.InAppBilling.PocoJsonSerializerStrategy` value.

---

<a name="2b92b987-b2ab-4ae5-80eb-75315136c952"></a>
##Public Price

###Return Type

`System.String`

###Member of Type

[Product](#98d67a2a-c250-4548-8a90-9e3722fef483)

###Summary

Gets or sets the price.
###Return Value

The price.

---

<a name="7448e262-2b48-448a-a9e2-43c9f97cef07"></a>
##Public Price_Amount_Micros

###Return Type

`System.String`

###Member of Type

[Product](#98d67a2a-c250-4548-8a90-9e3722fef483)

###Summary

Price in micro-units, where 1,000,000 micro-units equal one unit of the currency. For example, if price is "€7.99", price_amount_micros is "7990000".
###Return Value

The price amount micros.

---

<a name="a9c164f4-6953-45ff-b8f9-4ea6ee6f15b1"></a>
##Public Price_Currency_Code

###Return Type

`System.String`

###Member of Type

[Product](#98d67a2a-c250-4548-8a90-9e3722fef483)

###Summary

ISO 4217 currency code for price. For example, if price is specified in British pounds sterling, price_currency_code is "GBP".
###Return Value

The price currency code.

---

<a name="dffe56e7-ba74-4b3a-9053-65107ae3a8e2"></a>
##Public ProductId

###Return Type

`System.String`

###Member of Type

[Product](#98d67a2a-c250-4548-8a90-9e3722fef483)

###Summary

Gets or sets the product identifier.
###Return Value

The product identifier.

---

<a name="5687d267-a288-4133-9106-cfe7328d22c1"></a>
##Public ProductId

###Return Type

`System.String`

###Member of Type

[Purchase](#03489ba4-1d9d-4430-8e09-b3bff9875fac)

###Summary

Gets or sets the product identifier.
###Return Value

The product identifier.

---

<a name="0c0561d8-04c3-4c2d-b827-4d7d7298687f"></a>
##Public PublicKey

###Return Type

`System.String`

###Member of Type

[InAppBillingServiceConnection](#80c1612e-cf5e-41b3-a216-ca89246674e9)

###Summary

Gets or sets the Google Play Service public key used for In-App Billing
###Return Value

The public key.
###Remarks

NOTE: The key will be encrypted when it is stored in memory.

---

<a name="22ace037-b644-4484-bb31-6052f88f8018"></a>
##Public PublicKey

###Return Type

`System.String`

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

Gets or sets the Google Play Service public key used for In-App Billing
###Return Value

The public key.
###Remarks

NOTE: The key will be encrypted when it is stored in memory.

---

<a name="51c56559-7255-42ff-83b9-60ae9c953cc4"></a>
##Public PurchaseState

###Return Type

`System.Int32`

###Member of Type

[Purchase](#03489ba4-1d9d-4430-8e09-b3bff9875fac)

###Summary

Gets or sets the state of the purchase.
###Return Value

The state of the purchase.

---

<a name="1b7e1671-1b14-4753-b3da-0ca7fe5ce9e2"></a>
##Public PurchaseTime

###Return Type

`System.Int64`

###Member of Type

[Purchase](#03489ba4-1d9d-4430-8e09-b3bff9875fac)

###Summary

Gets or sets the purchase time.
###Return Value

The purchase time.

---

<a name="ae8bc34b-e558-44dd-98e4-161887243b0a"></a>
##Public PurchaseToken

###Return Type

`System.String`

###Member of Type

[Purchase](#03489ba4-1d9d-4430-8e09-b3bff9875fac)

###Summary

Gets or sets the purchase token.
###Return Value

The purchase token.

---

<a name="1caa8556-74b2-41e3-902b-8c2248d3d8f8"></a>
##Public Read Only Service

###Return Type

[Com.Android.Vending.Billing.IInAppBillingService](#2c2de562-b76b-4126-8476-c2b22a4ccde1)

###Member of Type

[InAppBillingServiceConnection](#80c1612e-cf5e-41b3-a216-ca89246674e9)

###Summary

Gets the Google Play `InAppBillingService` interface.
###Return Value

The `InAppBillingService` attached to this [InAppBillingServiceConnection](#80c1612e-cf5e-41b3-a216-ca89246674e9) 

---

<a name="783ad0e3-0c42-4943-a9f0-3140b898198b"></a>
##Public Title

###Return Type

`System.String`

###Member of Type

[Product](#98d67a2a-c250-4548-8a90-9e3722fef483)

###Summary

Gets or sets the title.
###Return Value

The title.

---

<a name="e58a4321-7ec4-4aca-8716-d46c467e8d87"></a>
##Public Type

###Return Type

`System.String`

###Member of Type

[Product](#98d67a2a-c250-4548-8a90-9e3722fef483)

###Summary

Gets or sets the type.
###Return Value

The type.

---

<a name="b35ddd29-9a7e-425f-b2e9-0e8ad4942c52"></a>
##Public Virtual Read Only Values

###Return Type

`System.Collections.Generic.ICollection<System.Object>`

###Member of Type

[JsonObject](#4db2613b-bdf6-4cff-a6da-804b56455cbe)

###Summary

Gets the values.
###Return Value

The values.
<a name="Methods"></a>
#Methods


---

<a name="8bc8f851-2466-4411-923d-1da23b2d886b"></a>
##Public Virtual Void Add (System.Collections.Generic.KeyValuePair< System.String, System.Object >)

###Member of Type

[JsonObject](#4db2613b-bdf6-4cff-a6da-804b56455cbe)

###Summary

Adds the specified item.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>item</td><td style='width:75%' ><p>The item.</p>
</td></tr></table></p>


---

<a name="92d9e6e9-c46f-44dd-ba1f-06edad5d1b29"></a>
##Public Virtual Void Add (System.String, System.Object)

###Member of Type

[JsonObject](#4db2613b-bdf6-4cff-a6da-804b56455cbe)

###Summary

Adds the specified key.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>key</td><td style='width:75%' ><p>The key.</p>
</td></tr><tr><td style='width:25%' class='term'>value</td><td style='width:75%' class='def'><p>The value.</p>
</td></tr></table></p>


---

<a name="3165cdf3-3431-4012-a242-358d5555153a"></a>
##Public Virtual AsBinder ()

###Return Type

`Android.OS.IBinder`

###Member of Type

[IInAppBillingServiceStub](#e452919a-6bfb-4cf1-b5e6-88131704b783)

###Summary

The public virtual `AsBinder ()` member of the `IInAppBillingServiceStub` class returns a `Android.OS.IBinder` value.

---

<a name="0f4a1c58-bb17-40a3-91e7-6018ce3af619"></a>
##Public Static AsInterface (Android.OS.IBinder)

###Return Type

[Com.Android.Vending.Billing.IInAppBillingService](#2c2de562-b76b-4126-8476-c2b22a4ccde1)

###Member of Type

[IInAppBillingServiceStub](#e452919a-6bfb-4cf1-b5e6-88131704b783)

###Summary

The public static `AsInterface (Android.OS.IBinder)` member of the `IInAppBillingServiceStub` class returns a `Com.Android.Vending.Billing.IInAppBillingService` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>obj</td><td style='width:75%' ><p>The <code>obj</code> parameter of the AsInterface method takes a <code>Android.OS.IBinder</code> value. </p>
</td></tr></table></p>


---

<a name="9473dfaf-799f-4182-a73d-7d3b01265567"></a>
##Public Static Assign (System.Linq.Expressions.Expression, System.Linq.Expressions.Expression)

###Return Type

`System.Linq.Expressions.BinaryExpression`

###Member of Type

[ReflectionUtils](#6e848a6a-ea7d-4af0-b023-cd70a7e96d45)

###Summary

The public static `Assign (System.Linq.Expressions.Expression, System.Linq.Expressions.Expression)` member of the `ReflectionUtils` class returns a `System.Linq.Expressions.BinaryExpression` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>left</td><td style='width:75%' ><p>The <code>left</code> parameter of the Assign method takes a <code>System.Linq.Expressions.Expression</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>right</td><td style='width:75%' class='def'><p>The <code>right</code> parameter of the Assign method takes a <code>System.Linq.Expressions.Expression</code> value. </p>
</td></tr></table></p>


---

<a name="702667b2-c59b-4585-a151-b487437389d0"></a>
##Public Virtual Void BuyProduct (System.String, System.String, System.String)

###Member of Type

[IInAppBillingHandler](#9b4f25fc-45a1-4e40-900d-3c3aa0998f95)

###Summary

Buys an item.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>sku</td><td style='width:75%' ><p>Sku.</p>
</td></tr><tr><td style='width:25%' class='term'>itemType</td><td style='width:75%' class='def'><p>Item type.</p>
</td></tr><tr><td style='width:25%' class='term'>payload</td><td style='width:75%' ><p>Payload.</p>
</td></tr></table></p>


---

<a name="a9596c24-42c5-4f9b-af9a-5763e6e0bcbe"></a>
##Public Virtual Void BuyProduct (System.String, System.String, System.String)

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

Buys a product based on the given product SKU and Item Type attaching the given payload
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>sku</td><td style='width:75%' ><p>The SKU of the item to purchase.</p>
</td></tr><tr><td style='width:25%' class='term'>itemType</td><td style='width:75%' class='def'><p>The type of the item to purchase.</p>
</td></tr><tr><td style='width:25%' class='term'>payload</td><td style='width:75%' ><p>The developer payload to attach to the purchase.</p>
</td></tr></table></p>


---

<a name="6fc40d53-813f-46da-b068-0b1ae45b466a"></a>
##Public Virtual Void BuyProduct (Xamarin.InAppBilling.Product)

###Member of Type

[IInAppBillingHandler](#9b4f25fc-45a1-4e40-900d-3c3aa0998f95)

###Summary

Buys an items
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>product</td><td style='width:75%' ><p>Product.</p>
</td></tr><tr><td style='width:25%' class='term'>payload</td><td style='width:75%' class='def'><p>Payload.</p>
</td></tr></table></p>


---

<a name="b947f8e6-a4f3-4f03-8531-5da94ac47d71"></a>
##Public Virtual Void BuyProduct (Xamarin.InAppBilling.Product)

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

Buys the given [Product](#98d67a2a-c250-4548-8a90-9e3722fef483) 
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>product</td><td style='width:75%' ><p>The <a href="#98d67a2a-c250-4548-8a90-9e3722fef483">Product</a> representing the item the users wants to purchase.</p>
</td></tr></table></p>

###Remarks

This method automatically generates a unique GUID and attaches it as the developer payload for this purchase.

---

<a name="db035b38-2c81-4026-919c-b0bcdbc9fb6e"></a>
##Public Void BuyProduct (Xamarin.InAppBilling.Product, System.String)

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

Buys the given [Product](#98d67a2a-c250-4548-8a90-9e3722fef483) and attaches the given developer payload to the  purchase.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>product</td><td style='width:75%' ><p>The <a href="#98d67a2a-c250-4548-8a90-9e3722fef483">Product</a> representing the item the users wants to purchase.</p>
</td></tr><tr><td style='width:25%' class='term'>payload</td><td style='width:75%' class='def'><p>The developer payload to attach to the purchase.</p>
</td></tr></table></p>


---

<a name="67c0971b-9d94-4247-b5ef-6e5cdf5176fc"></a>
##Public Virtual Void Clear ()

###Member of Type

[JsonObject](#4db2613b-bdf6-4cff-a6da-804b56455cbe)

###Summary

Clears this instance.

---

<a name="5ae05ea1-e624-408b-bc8f-47b0caae536a"></a>
##Public Void Connect ()

###Member of Type

[InAppBillingServiceConnection](#80c1612e-cf5e-41b3-a216-ca89246674e9)

###Summary

Connect this instance to the Google Play service to support In-App Billing in your application

---

<a name="fd8c3a06-36dd-4a7e-8aa1-b67dd854e08c"></a>
##Public Virtual ConsumePurchase (System.Int32, System.String, System.String)

###Return Type

`System.Int32`

###Member of Type

[IInAppBillingService](#2c2de562-b76b-4126-8476-c2b22a4ccde1)

###Summary

The public virtual `ConsumePurchase (System.Int32, System.String, System.String)` member of the `IInAppBillingService` interface returns a `System.Int32` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>apiVersion</td><td style='width:75%' ><p>The <code>apiVersion</code> parameter of the ConsumePurchase method takes a <code>System.Int32</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>packageName</td><td style='width:75%' class='def'><p>The <code>packageName</code> parameter of the ConsumePurchase method takes a <code>System.String</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>purchaseToken</td><td style='width:75%' ><p>The <code>purchaseToken</code> parameter of the ConsumePurchase method takes a <code>System.String</code> value. </p>
</td></tr></table></p>


---

<a name="cc63fa9b-53fe-4160-af48-67daa4dd5c9d"></a>
##Public Virtual ConsumePurchase (System.Int32, System.String, System.String)

###Return Type

`System.Int32`

###Member of Type

[IInAppBillingServiceStub](#e452919a-6bfb-4cf1-b5e6-88131704b783)

###Summary

The public virtual `ConsumePurchase (System.Int32, System.String, System.String)` member of the `IInAppBillingServiceStub` class returns a `System.Int32` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>apiVersion</td><td style='width:75%' ><p>The <code>apiVersion</code> parameter of the ConsumePurchase method takes a <code>System.Int32</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>packageName</td><td style='width:75%' class='def'><p>The <code>packageName</code> parameter of the ConsumePurchase method takes a <code>System.String</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>purchaseToken</td><td style='width:75%' ><p>The <code>purchaseToken</code> parameter of the ConsumePurchase method takes a <code>System.String</code> value. </p>
</td></tr></table></p>


---

<a name="2c9f2030-e9d9-451e-b763-b7617c541bb4"></a>
##Public Virtual ConsumePurchase (System.String)

###Return Type

`System.Boolean`

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

Consumes the purchased item
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>token</td><td style='width:75%' ><p>The purchase token of the purchase to consume.</p>
</td></tr></table></p>

###Returns

`true` if the purchase is successfully consumed else returns `false`.

---

<a name="67efc742-c297-4e1e-90ae-b96780e84f2b"></a>
##Public Virtual ConsumePurchase (System.String)

###Return Type

`System.Boolean`

###Member of Type

[IInAppBillingHandler](#9b4f25fc-45a1-4e40-900d-3c3aa0998f95)

###Summary

Consumes the purchased item
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>token</td><td style='width:75%' ><p>Token.</p>
</td></tr></table></p>

###Returns

`true`, if purchased item was consumed, `false` otherwise.

---

<a name="0a15eb2e-2d49-446a-b4e1-31d0cec4575d"></a>
##Public Virtual ConsumePurchase (Xamarin.InAppBilling.Purchase)

###Return Type

`System.Boolean`

###Member of Type

[IInAppBillingHandler](#9b4f25fc-45a1-4e40-900d-3c3aa0998f95)

###Summary

Consumes the purchased item
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>purchase</td><td style='width:75%' ><p>Purchased item</p>
</td></tr></table></p>

###Returns

`true`, if purchased item was consumed, `false` otherwise.

---

<a name="7267a298-bece-48fc-b446-dd15961c82bc"></a>
##Public Virtual ConsumePurchase (Xamarin.InAppBilling.Purchase)

###Return Type

`System.Boolean`

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

Consumes the purchased item.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>purchase</td><td style='width:75%' ><p>The purchase receipt of the item to consume.</p>
</td></tr></table></p>

###Returns

`true` if the purchase is successfully consumed else returns `false`.

---

<a name="96ce05b1-2819-4a31-b29d-57208a60084a"></a>
##Public Virtual Contains (System.Collections.Generic.KeyValuePair< System.String, System.Object >)

###Return Type

`System.Boolean`

###Member of Type

[JsonObject](#4db2613b-bdf6-4cff-a6da-804b56455cbe)

###Summary

Determines whether [contains] [the specified item].
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>item</td><td style='width:75%' ><p>The item.</p>
</td></tr></table></p>

###Returns

	`true` if [contains] [the specified item]; otherwise, `false`.

---

<a name="021cd2c3-e85b-47f3-b704-c2902708092c"></a>
##Public Virtual ContainsKey (System.String)

###Return Type

`System.Boolean`

###Member of Type

[JsonObject](#4db2613b-bdf6-4cff-a6da-804b56455cbe)

###Summary

Determines whether the specified key contains key.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>key</td><td style='width:75%' ><p>The key.</p>
</td></tr></table></p>

###Returns

`true` if the specified key contains key; otherwise, `false`.

---

<a name="19e63867-c591-409e-b4f7-8d4726fa2323"></a>
##Virtual ContructorDelegateFactory (System.Type)

###Return Type

`Xamarin.InAppBilling.Reflection.ReflectionUtils.ConstructorDelegate`

###Member of Type

[PocoJsonSerializerStrategy](#1ac2d0dc-01c8-489d-9039-4374c33d0124)

###Summary

The  virtual `ContructorDelegateFactory (System.Type)` member of the `PocoJsonSerializerStrategy` class returns a `Xamarin.InAppBilling.Reflection.ReflectionUtils.ConstructorDelegate` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>key</td><td style='width:75%' ><p>The <code>key</code> parameter of the ContructorDelegateFactory method takes a <code>System.Type</code> value. </p>
</td></tr></table></p>


---

<a name="43fdc044-9481-4834-854b-37cda82cd440"></a>
##Private Static ConvertFromUtf32 (System.Int32)

###Return Type

`System.String`

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

The private static `ConvertFromUtf32 (System.Int32)` member of the `SimpleJson` class returns a `System.String` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>utf32</td><td style='width:75%' ><p>The <code>utf32</code> parameter of the ConvertFromUtf32 method takes a <code>System.Int32</code> value. </p>
</td></tr></table></p>


---

<a name="96c7a88d-78cf-4bfa-8b85-760a3941c8c0"></a>
##Public Virtual Void CopyTo (System.Collections.Generic.KeyValuePair< System.String, System.Object >[], System.Int32)

###Member of Type

[JsonObject](#4db2613b-bdf6-4cff-a6da-804b56455cbe)

###Summary

Copies to.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>array</td><td style='width:75%' ><p>The array.</p>
</td></tr><tr><td style='width:25%' class='term'>arrayIndex</td><td style='width:75%' class='def'><p>Index of the array.</p>
</td></tr></table></p>


---

<a name="cac96627-af46-45e9-a32b-aac412de5ca1"></a>
##Private Static Void Crypto ()

###Constructor for Type

[Crypto](#7b127d1b-7daa-4f26-b23a-5e7236a4af07)

###Summary

The private static `Crypto ()` constructor for the `Crypto` class.

---

<a name="88ca6ed3-40ab-45d4-b8a9-032e8e6be4f8"></a>
##Public Static Void Debug (System.String, System.Object[])

###Member of Type

[Logger](#4e163874-6eb3-4f60-8c44-9e0260d939fb)

###Summary

Writes debug information to the log
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>format</td><td style='width:75%' ><p>Format.</p>
</td></tr><tr><td style='width:25%' class='term'>args</td><td style='width:75%' class='def'><p>Arguments.</p>
</td></tr></table></p>


---

<a name="185f0384-9ac9-424d-b6fc-0e7af62eea64"></a>
##Static Decrypt (System.String, System.String)

###Return Type

`System.String`

###Member of Type

[Crypto](#7b127d1b-7daa-4f26-b23a-5e7236a4af07)

###Summary

Takes the given encrypted text string and decrypts it using the given password
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>encryptedText</td><td style='width:75%' ><p>Encrypted text.</p>
</td></tr><tr><td style='width:25%' class='term'>encryptionPassword</td><td style='width:75%' class='def'><p>Encryption password.</p>
</td></tr></table></p>


---

<a name="1f1239a0-23c0-4147-8901-a0ba7353fd9a"></a>
##Public Virtual DeserializeObject (System.Object, System.Type)

###Return Type

`System.Object`

###Member of Type

[PocoJsonSerializerStrategy](#1ac2d0dc-01c8-489d-9039-4374c33d0124)

###Summary

The public virtual `DeserializeObject (System.Object, System.Type)` member of the `PocoJsonSerializerStrategy` class returns a `System.Object` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>value</td><td style='width:75%' ><p>The <code>value</code> parameter of the DeserializeObject method takes a <code>System.Object</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>type</td><td style='width:75%' class='def'><p>The <code>type</code> parameter of the DeserializeObject method takes a <code>System.Type</code> value. </p>
</td></tr></table></p>


---

<a name="fb602161-89ef-4b4f-b376-4c28714199e9"></a>
##Public Virtual DeserializeObject (System.Object, System.Type)

###Return Type

`System.Object`

###Member of Type

[IJsonSerializerStrategy](#88311aab-37d5-4381-95ce-c4aa0303ac7f)

###Summary

The public virtual `DeserializeObject (System.Object, System.Type)` member of the `IJsonSerializerStrategy` interface returns a `System.Object` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>value</td><td style='width:75%' ><p>The <code>value</code> parameter of the DeserializeObject method takes a <code>System.Object</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>type</td><td style='width:75%' class='def'><p>The <code>type</code> parameter of the DeserializeObject method takes a <code>System.Type</code> value. </p>
</td></tr></table></p>


---

<a name="fa25b7d4-fa90-4a12-94b6-2284bbae8000"></a>
##Public Static DeserializeObject (System.String)

###Return Type

`T`

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

Parses the string json into a value
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>json</td><td style='width:75%' ><p>A JSON string.</p>
</td></tr></table></p>

###Returns

An IList&lt;object&gt;, a IDictionary&lt;string,object&gt;, a double, a string, null, true, or false

---

<a name="18f5265c-884e-45c3-b948-9b9854c74540"></a>
##Public Static DeserializeObject (System.String, System.Type)

###Return Type

`System.Object`

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

The public static `DeserializeObject (System.String, System.Type)` member of the `SimpleJson` class returns a `System.Object` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>json</td><td style='width:75%' ><p>The <code>json</code> parameter of the DeserializeObject method takes a <code>System.String</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>type</td><td style='width:75%' class='def'><p>The <code>type</code> parameter of the DeserializeObject method takes a <code>System.Type</code> value. </p>
</td></tr></table></p>


---

<a name="7376b15a-5f29-4b57-a35b-92ecb9d5220b"></a>
##Public Static DeserializeObject (System.String, System.Type, Xamarin.InAppBilling.IJsonSerializerStrategy)

###Return Type

`System.Object`

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

The public static `DeserializeObject (System.String, System.Type, Xamarin.InAppBilling.IJsonSerializerStrategy)` member of the `SimpleJson` class returns a `System.Object` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>json</td><td style='width:75%' ><p>The <code>json</code> parameter of the DeserializeObject method takes a <code>System.String</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>type</td><td style='width:75%' class='def'><p>The <code>type</code> parameter of the DeserializeObject method takes a <code>System.Type</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>jsonSerializerStrategy</td><td style='width:75%' ><p>The <code>jsonSerializerStrategy</code> parameter of the DeserializeObject method takes a <code>Xamarin.InAppBilling.IJsonSerializerStrategy</code> value. </p>
</td></tr></table></p>


---

<a name="1ab83c75-a631-4dc7-87bf-2e89412b67a0"></a>
##Public Static DeserializeObject (System.String, Xamarin.InAppBilling.IJsonSerializerStrategy)

###Return Type

`T`

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

The public static `DeserializeObject (System.String, Xamarin.InAppBilling.IJsonSerializerStrategy)` member of the `SimpleJson` class returns a `T` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>json</td><td style='width:75%' ><p>The <code>json</code> parameter of the DeserializeObject method takes a <code>System.String</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>jsonSerializerStrategy</td><td style='width:75%' class='def'><p>The <code>jsonSerializerStrategy</code> parameter of the DeserializeObject method takes a <code>Xamarin.InAppBilling.IJsonSerializerStrategy</code> value. </p>
</td></tr></table></p>


---

<a name="3b72d411-115d-46b7-818f-74a751170de6"></a>
##Public Void Disconnect ()

###Member of Type

[InAppBillingServiceConnection](#80c1612e-cf5e-41b3-a216-ca89246674e9)

###Summary

Disconnects this instance from the Google Play service.
###Remarks

Important: Remember to unbind from the In-app Billing service when you are done with your activity.  If you don’t unbind, the open service connection could cause your device’s performance to degrade. To unbind  and free your system resources, call the `Disconnect` method when your Activity gets destroyed.

---

<a name="da112460-7185-4309-b88f-2d80955c5d81"></a>
##Private Static Void EatWhitespace (System.Char[], System.Int32&)

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

The private static `EatWhitespace (System.Char[], System.Int32&)` member of the `SimpleJson` class.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>json</td><td style='width:75%' ><p>The <code>json</code> parameter of the EatWhitespace method takes a <code>System.Char[]</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>Ref index</td><td style='width:75%' class='def'><p>The <code>index</code> parameter of the EatWhitespace method takes a <code>System.Int32&amp;</code> value. Since <code>index</code> returns any modifications to the caller, it <em>must</em> be passed with the <code>ref</code> modifier.</p>
</td></tr></table></p>


---

<a name="0ac1600d-8aa4-43e1-98d3-e1e03776dec0"></a>
##Static Encrypt (System.String, System.String)

###Return Type

`System.String`

###Member of Type

[Crypto](#7b127d1b-7daa-4f26-b23a-5e7236a4af07)

###Summary

Takes the given text string and encrypts it using the given password.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>textToEncrypt</td><td style='width:75%' ><p>Text to encrypt.</p>
</td></tr><tr><td style='width:25%' class='term'>encryptionPassword</td><td style='width:75%' class='def'><p>Encryption password.</p>
</td></tr></table></p>


---

<a name="67d9a5f2-6ad5-48a3-828f-c0f1062b0bb6"></a>
##Public Static Void Error (System.String, System.Object[])

###Member of Type

[Logger](#4e163874-6eb3-4f60-8c44-9e0260d939fb)

###Summary

Writes error information to the log
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>format</td><td style='width:75%' ><p>Format.</p>
</td></tr><tr><td style='width:25%' class='term'>args</td><td style='width:75%' class='def'><p>Arguments.</p>
</td></tr></table></p>


---

<a name="427b1d91-75c7-4ffc-8a74-cc2a0c41e05e"></a>
##Public Static EscapeToJavascriptString (System.String)

###Return Type

`System.String`

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

The public static `EscapeToJavascriptString (System.String)` member of the `SimpleJson` class returns a `System.String` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>jsonString</td><td style='width:75%' ><p>The <code>jsonString</code> parameter of the EscapeToJavascriptString method takes a <code>System.String</code> value. </p>
</td></tr></table></p>


---

<a name="8df0e6ae-b628-428e-b039-459f00ea7d4d"></a>
##Public Static GeneratePublicKey (System.String)

###Return Type

`Java.Security.IPublicKey`

###Member of Type

[Security](#9e6b0e98-9aae-449f-a739-3b32774c3e76)

###Summary

Generates the public key.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>encodedPublicKey</td><td style='width:75%' ><p>Encoded public key.</p>
</td></tr></table></p>

###Returns

The public key.

---

<a name="bc6a49f4-912f-4345-856f-fded9a07b48d"></a>
##Private Static GetAlgorithm (System.String)

###Return Type

`System.Security.Cryptography.RijndaelManaged`

###Member of Type

[Crypto](#7b127d1b-7daa-4f26-b23a-5e7236a4af07)

###Summary

Defines a RijndaelManaged algorithm and sets its key and Initialization Vector (IV)  values based on the encryptionPassword received.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>encryptionPassword</td><td style='width:75%' ><p>Encryption password.</p>
</td></tr></table></p>

###Returns

The algorithm.

---

<a name="772647e6-1d03-4686-a815-d560ba54c116"></a>
##Static GetAtIndex (System.Collections.Generic.IDictionary<System.String, System.Object>, System.Int32)

###Return Type

`System.Object`

###Member of Type

[JsonObject](#4db2613b-bdf6-4cff-a6da-804b56455cbe)

###Summary

The  static `GetAtIndex (System.Collections.Generic.IDictionary<System.String, System.Object>, System.Int32)` member of the `JsonObject` class returns a `System.Object` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>obj</td><td style='width:75%' ><p>The <code>obj</code> parameter of the GetAtIndex method takes a <code>System.Collections.Generic.IDictionary&lt;System.String,System.Object&gt;</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>index</td><td style='width:75%' class='def'><p>The <code>index</code> parameter of the GetAtIndex method takes a <code>System.Int32</code> value. </p>
</td></tr></table></p>


---

<a name="34ba3d7e-6dc0-4de8-adb4-7af8d76a42db"></a>
##Public Static GetAttribute (System.Reflection.MemberInfo, System.Type)

###Return Type

`System.Attribute`

###Member of Type

[ReflectionUtils](#6e848a6a-ea7d-4af0-b023-cd70a7e96d45)

###Summary

The public static `GetAttribute (System.Reflection.MemberInfo, System.Type)` member of the `ReflectionUtils` class returns a `System.Attribute` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>info</td><td style='width:75%' ><p>The <code>info</code> parameter of the GetAttribute method takes a <code>System.Reflection.MemberInfo</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>type</td><td style='width:75%' class='def'><p>The <code>type</code> parameter of the GetAttribute method takes a <code>System.Type</code> value. </p>
</td></tr></table></p>


---

<a name="abbac326-fd9d-4bce-b05a-5a375c3f9a71"></a>
##Public Static GetAttribute (System.Type, System.Type)

###Return Type

`System.Attribute`

###Member of Type

[ReflectionUtils](#6e848a6a-ea7d-4af0-b023-cd70a7e96d45)

###Summary

The public static `GetAttribute (System.Type, System.Type)` member of the `ReflectionUtils` class returns a `System.Attribute` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>objectType</td><td style='width:75%' ><p>The <code>objectType</code> parameter of the GetAttribute method takes a <code>System.Type</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>attributeType</td><td style='width:75%' class='def'><p>The <code>attributeType</code> parameter of the GetAttribute method takes a <code>System.Type</code> value. </p>
</td></tr></table></p>


---

<a name="e1d65b86-37f6-4570-be96-912e4dece945"></a>
##Public Virtual GetBuyIntent (System.Int32, System.String, System.String, System.String, System.String)

###Return Type

`Android.OS.Bundle`

###Member of Type

[IInAppBillingServiceStub](#e452919a-6bfb-4cf1-b5e6-88131704b783)

###Summary

The public virtual `GetBuyIntent (System.Int32, System.String, System.String, System.String, System.String)` member of the `IInAppBillingServiceStub` class returns a `Android.OS.Bundle` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>apiVersion</td><td style='width:75%' ><p>The <code>apiVersion</code> parameter of the GetBuyIntent method takes a <code>System.Int32</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>packageName</td><td style='width:75%' class='def'><p>The <code>packageName</code> parameter of the GetBuyIntent method takes a <code>System.String</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>sku</td><td style='width:75%' ><p>The <code>sku</code> parameter of the GetBuyIntent method takes a <code>System.String</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>type</td><td style='width:75%' class='def'><p>The <code>type</code> parameter of the GetBuyIntent method takes a <code>System.String</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>developerPayload</td><td style='width:75%' ><p>The <code>developerPayload</code> parameter of the GetBuyIntent method takes a <code>System.String</code> value. </p>
</td></tr></table></p>


---

<a name="7a0eac28-3746-4e85-975f-82192553abd9"></a>
##Public Virtual GetBuyIntent (System.Int32, System.String, System.String, System.String, System.String)

###Return Type

`Android.OS.Bundle`

###Member of Type

[IInAppBillingService](#2c2de562-b76b-4126-8476-c2b22a4ccde1)

###Summary

The public virtual `GetBuyIntent (System.Int32, System.String, System.String, System.String, System.String)` member of the `IInAppBillingService` interface returns a `Android.OS.Bundle` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>apiVersion</td><td style='width:75%' ><p>The <code>apiVersion</code> parameter of the GetBuyIntent method takes a <code>System.Int32</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>packageName</td><td style='width:75%' class='def'><p>The <code>packageName</code> parameter of the GetBuyIntent method takes a <code>System.String</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>sku</td><td style='width:75%' ><p>The <code>sku</code> parameter of the GetBuyIntent method takes a <code>System.String</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>type</td><td style='width:75%' class='def'><p>The <code>type</code> parameter of the GetBuyIntent method takes a <code>System.String</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>developerPayload</td><td style='width:75%' ><p>The <code>developerPayload</code> parameter of the GetBuyIntent method takes a <code>System.String</code> value. </p>
</td></tr></table></p>


---

<a name="045bb72c-0c32-440c-905c-e1e03cc8ad1d"></a>
##Public Static GetConstructorByExpression (System.Reflection.ConstructorInfo)

###Return Type

`Xamarin.InAppBilling.Reflection.ReflectionUtils.ConstructorDelegate`

###Member of Type

[ReflectionUtils](#6e848a6a-ea7d-4af0-b023-cd70a7e96d45)

###Summary

The public static `GetConstructorByExpression (System.Reflection.ConstructorInfo)` member of the `ReflectionUtils` class returns a `Xamarin.InAppBilling.Reflection.ReflectionUtils.ConstructorDelegate` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>constructorInfo</td><td style='width:75%' ><p>The <code>constructorInfo</code> parameter of the GetConstructorByExpression method takes a <code>System.Reflection.ConstructorInfo</code> value. </p>
</td></tr></table></p>


---

<a name="3bd7687b-18de-49f1-a60f-4edf86644e65"></a>
##Public Static GetConstructorByExpression (System.Type, System.Type[])

###Return Type

`Xamarin.InAppBilling.Reflection.ReflectionUtils.ConstructorDelegate`

###Member of Type

[ReflectionUtils](#6e848a6a-ea7d-4af0-b023-cd70a7e96d45)

###Summary

The public static `GetConstructorByExpression (System.Type, System.Type[])` member of the `ReflectionUtils` class returns a `Xamarin.InAppBilling.Reflection.ReflectionUtils.ConstructorDelegate` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>type</td><td style='width:75%' ><p>The <code>type</code> parameter of the GetConstructorByExpression method takes a <code>System.Type</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>argsType</td><td style='width:75%' class='def'><p>The <code>argsType</code> parameter of the GetConstructorByExpression method takes a <code>System.Type[]</code> value. </p>
</td></tr></table></p>


---

<a name="5eb65d1d-dc17-4a79-a298-8ae5caa4d0ec"></a>
##Public Static GetConstructorByReflection (System.Reflection.ConstructorInfo)

###Return Type

`Xamarin.InAppBilling.Reflection.ReflectionUtils.ConstructorDelegate`

###Member of Type

[ReflectionUtils](#6e848a6a-ea7d-4af0-b023-cd70a7e96d45)

###Summary

The public static `GetConstructorByReflection (System.Reflection.ConstructorInfo)` member of the `ReflectionUtils` class returns a `Xamarin.InAppBilling.Reflection.ReflectionUtils.ConstructorDelegate` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>constructorInfo</td><td style='width:75%' ><p>The <code>constructorInfo</code> parameter of the GetConstructorByReflection method takes a <code>System.Reflection.ConstructorInfo</code> value. </p>
</td></tr></table></p>


---

<a name="529a440c-b323-4c7d-b8d0-e67a2bc05ebc"></a>
##Public Static GetConstructorByReflection (System.Type, System.Type[])

###Return Type

`Xamarin.InAppBilling.Reflection.ReflectionUtils.ConstructorDelegate`

###Member of Type

[ReflectionUtils](#6e848a6a-ea7d-4af0-b023-cd70a7e96d45)

###Summary

The public static `GetConstructorByReflection (System.Type, System.Type[])` member of the `ReflectionUtils` class returns a `Xamarin.InAppBilling.Reflection.ReflectionUtils.ConstructorDelegate` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>type</td><td style='width:75%' ><p>The <code>type</code> parameter of the GetConstructorByReflection method takes a <code>System.Type</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>argsType</td><td style='width:75%' class='def'><p>The <code>argsType</code> parameter of the GetConstructorByReflection method takes a <code>System.Type[]</code> value. </p>
</td></tr></table></p>


---

<a name="6a9910d2-9932-409f-a124-53eb41085aef"></a>
##Public Static GetConstructorInfo (System.Type, System.Type[])

###Return Type

`System.Reflection.ConstructorInfo`

###Member of Type

[ReflectionUtils](#6e848a6a-ea7d-4af0-b023-cd70a7e96d45)

###Summary

The public static `GetConstructorInfo (System.Type, System.Type[])` member of the `ReflectionUtils` class returns a `System.Reflection.ConstructorInfo` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>type</td><td style='width:75%' ><p>The <code>type</code> parameter of the GetConstructorInfo method takes a <code>System.Type</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>argsType</td><td style='width:75%' class='def'><p>The <code>argsType</code> parameter of the GetConstructorInfo method takes a <code>System.Type[]</code> value. </p>
</td></tr></table></p>


---

<a name="504a71e4-74bd-4d4e-8790-3efc84c2627f"></a>
##Public Static GetConstructors (System.Type)

###Return Type

`System.Collections.Generic.IEnumerable<System.Reflection.ConstructorInfo>`

###Member of Type

[ReflectionUtils](#6e848a6a-ea7d-4af0-b023-cd70a7e96d45)

###Summary

The public static `GetConstructors (System.Type)` member of the `ReflectionUtils` class returns a `System.Collections.Generic.IEnumerable<System.Reflection.ConstructorInfo>` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>type</td><td style='width:75%' ><p>The <code>type</code> parameter of the GetConstructors method takes a <code>System.Type</code> value. </p>
</td></tr></table></p>


---

<a name="b41d1260-6848-43c9-94a4-0538235b4ed1"></a>
##Public Static GetContructor (System.Reflection.ConstructorInfo)

###Return Type

`Xamarin.InAppBilling.Reflection.ReflectionUtils.ConstructorDelegate`

###Member of Type

[ReflectionUtils](#6e848a6a-ea7d-4af0-b023-cd70a7e96d45)

###Summary

The public static `GetContructor (System.Reflection.ConstructorInfo)` member of the `ReflectionUtils` class returns a `Xamarin.InAppBilling.Reflection.ReflectionUtils.ConstructorDelegate` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>constructorInfo</td><td style='width:75%' ><p>The <code>constructorInfo</code> parameter of the GetContructor method takes a <code>System.Reflection.ConstructorInfo</code> value. </p>
</td></tr></table></p>


---

<a name="dee6137f-7a0b-4c9a-9af7-6eab7c7c03b3"></a>
##Public Static GetContructor (System.Type, System.Type[])

###Return Type

`Xamarin.InAppBilling.Reflection.ReflectionUtils.ConstructorDelegate`

###Member of Type

[ReflectionUtils](#6e848a6a-ea7d-4af0-b023-cd70a7e96d45)

###Summary

The public static `GetContructor (System.Type, System.Type[])` member of the `ReflectionUtils` class returns a `Xamarin.InAppBilling.Reflection.ReflectionUtils.ConstructorDelegate` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>type</td><td style='width:75%' ><p>The <code>type</code> parameter of the GetContructor method takes a <code>System.Type</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>argsType</td><td style='width:75%' class='def'><p>The <code>argsType</code> parameter of the GetContructor method takes a <code>System.Type[]</code> value. </p>
</td></tr></table></p>


---

<a name="80dee118-beb5-4b24-b68b-5cd07421c129"></a>
##Public Virtual GetEnumerator ()

###Return Type

`System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.String,System.Object>>`

###Member of Type

[JsonObject](#4db2613b-bdf6-4cff-a6da-804b56455cbe)

###Summary

Gets the enumerator.

---

<a name="fcd0f17a-71a5-43f8-b471-32579cfc2397"></a>
##Public Static GetFields (System.Type)

###Return Type

`System.Collections.Generic.IEnumerable<System.Reflection.FieldInfo>`

###Member of Type

[ReflectionUtils](#6e848a6a-ea7d-4af0-b023-cd70a7e96d45)

###Summary

The public static `GetFields (System.Type)` member of the `ReflectionUtils` class returns a `System.Collections.Generic.IEnumerable<System.Reflection.FieldInfo>` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>type</td><td style='width:75%' ><p>The <code>type</code> parameter of the GetFields method takes a <code>System.Type</code> value. </p>
</td></tr></table></p>


---

<a name="f531e7fd-ceca-4f1d-a2a8-504295a82e13"></a>
##Public Static GetGenericListElementType (System.Type)

###Return Type

`System.Type`

###Member of Type

[ReflectionUtils](#6e848a6a-ea7d-4af0-b023-cd70a7e96d45)

###Summary

The public static `GetGenericListElementType (System.Type)` member of the `ReflectionUtils` class returns a `System.Type` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>type</td><td style='width:75%' ><p>The <code>type</code> parameter of the GetGenericListElementType method takes a <code>System.Type</code> value. </p>
</td></tr></table></p>


---

<a name="4faccad4-baed-44e3-95ef-a3e4c8feb2b1"></a>
##Public Static GetGenericTypeArguments (System.Type)

###Return Type

`System.Type[]`

###Member of Type

[ReflectionUtils](#6e848a6a-ea7d-4af0-b023-cd70a7e96d45)

###Summary

The public static `GetGenericTypeArguments (System.Type)` member of the `ReflectionUtils` class returns a `System.Type[]` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>type</td><td style='width:75%' ><p>The <code>type</code> parameter of the GetGenericTypeArguments method takes a <code>System.Type</code> value. </p>
</td></tr></table></p>


---

<a name="9544d245-90d7-483d-893a-8933a0299f79"></a>
##Public Static GetGetMethod (System.Reflection.FieldInfo)

###Return Type

`Xamarin.InAppBilling.Reflection.ReflectionUtils.GetDelegate`

###Member of Type

[ReflectionUtils](#6e848a6a-ea7d-4af0-b023-cd70a7e96d45)

###Summary

The public static `GetGetMethod (System.Reflection.FieldInfo)` member of the `ReflectionUtils` class returns a `Xamarin.InAppBilling.Reflection.ReflectionUtils.GetDelegate` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>fieldInfo</td><td style='width:75%' ><p>The <code>fieldInfo</code> parameter of the GetGetMethod method takes a <code>System.Reflection.FieldInfo</code> value. </p>
</td></tr></table></p>


---

<a name="62b047bc-62b9-49b8-9c33-4f22b07f4368"></a>
##Public Static GetGetMethod (System.Reflection.PropertyInfo)

###Return Type

`Xamarin.InAppBilling.Reflection.ReflectionUtils.GetDelegate`

###Member of Type

[ReflectionUtils](#6e848a6a-ea7d-4af0-b023-cd70a7e96d45)

###Summary

The public static `GetGetMethod (System.Reflection.PropertyInfo)` member of the `ReflectionUtils` class returns a `Xamarin.InAppBilling.Reflection.ReflectionUtils.GetDelegate` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>propertyInfo</td><td style='width:75%' ><p>The <code>propertyInfo</code> parameter of the GetGetMethod method takes a <code>System.Reflection.PropertyInfo</code> value. </p>
</td></tr></table></p>


---

<a name="1e442e98-aede-49df-beba-79959009e014"></a>
##Public Static GetGetMethodByExpression (System.Reflection.FieldInfo)

###Return Type

`Xamarin.InAppBilling.Reflection.ReflectionUtils.GetDelegate`

###Member of Type

[ReflectionUtils](#6e848a6a-ea7d-4af0-b023-cd70a7e96d45)

###Summary

The public static `GetGetMethodByExpression (System.Reflection.FieldInfo)` member of the `ReflectionUtils` class returns a `Xamarin.InAppBilling.Reflection.ReflectionUtils.GetDelegate` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>fieldInfo</td><td style='width:75%' ><p>The <code>fieldInfo</code> parameter of the GetGetMethodByExpression method takes a <code>System.Reflection.FieldInfo</code> value. </p>
</td></tr></table></p>


---

<a name="23770d47-d798-4816-9925-de6f12e65c80"></a>
##Public Static GetGetMethodByExpression (System.Reflection.PropertyInfo)

###Return Type

`Xamarin.InAppBilling.Reflection.ReflectionUtils.GetDelegate`

###Member of Type

[ReflectionUtils](#6e848a6a-ea7d-4af0-b023-cd70a7e96d45)

###Summary

The public static `GetGetMethodByExpression (System.Reflection.PropertyInfo)` member of the `ReflectionUtils` class returns a `Xamarin.InAppBilling.Reflection.ReflectionUtils.GetDelegate` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>propertyInfo</td><td style='width:75%' ><p>The <code>propertyInfo</code> parameter of the GetGetMethodByExpression method takes a <code>System.Reflection.PropertyInfo</code> value. </p>
</td></tr></table></p>


---

<a name="638bbcab-ad0f-4352-99c2-2285597c6cc4"></a>
##Public Static GetGetMethodByReflection (System.Reflection.FieldInfo)

###Return Type

`Xamarin.InAppBilling.Reflection.ReflectionUtils.GetDelegate`

###Member of Type

[ReflectionUtils](#6e848a6a-ea7d-4af0-b023-cd70a7e96d45)

###Summary

The public static `GetGetMethodByReflection (System.Reflection.FieldInfo)` member of the `ReflectionUtils` class returns a `Xamarin.InAppBilling.Reflection.ReflectionUtils.GetDelegate` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>fieldInfo</td><td style='width:75%' ><p>The <code>fieldInfo</code> parameter of the GetGetMethodByReflection method takes a <code>System.Reflection.FieldInfo</code> value. </p>
</td></tr></table></p>


---

<a name="bb481d7f-c1ee-4207-8399-140a27ec672a"></a>
##Public Static GetGetMethodByReflection (System.Reflection.PropertyInfo)

###Return Type

`Xamarin.InAppBilling.Reflection.ReflectionUtils.GetDelegate`

###Member of Type

[ReflectionUtils](#6e848a6a-ea7d-4af0-b023-cd70a7e96d45)

###Summary

The public static `GetGetMethodByReflection (System.Reflection.PropertyInfo)` member of the `ReflectionUtils` class returns a `Xamarin.InAppBilling.Reflection.ReflectionUtils.GetDelegate` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>propertyInfo</td><td style='width:75%' ><p>The <code>propertyInfo</code> parameter of the GetGetMethodByReflection method takes a <code>System.Reflection.PropertyInfo</code> value. </p>
</td></tr></table></p>


---

<a name="635e0d55-4dcd-4c0f-b880-08b46734b9bf"></a>
##Public Static GetGetterMethodInfo (System.Reflection.PropertyInfo)

###Return Type

`System.Reflection.MethodInfo`

###Member of Type

[ReflectionUtils](#6e848a6a-ea7d-4af0-b023-cd70a7e96d45)

###Summary

The public static `GetGetterMethodInfo (System.Reflection.PropertyInfo)` member of the `ReflectionUtils` class returns a `System.Reflection.MethodInfo` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>propertyInfo</td><td style='width:75%' ><p>The <code>propertyInfo</code> parameter of the GetGetterMethodInfo method takes a <code>System.Reflection.PropertyInfo</code> value. </p>
</td></tr></table></p>


---

<a name="f6ff6834-2997-4116-855c-22dabfe9ae21"></a>
##Private Static GetLastIndexOfNumber (System.Char[], System.Int32)

###Return Type

`System.Int32`

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

The private static `GetLastIndexOfNumber (System.Char[], System.Int32)` member of the `SimpleJson` class returns a `System.Int32` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>json</td><td style='width:75%' ><p>The <code>json</code> parameter of the GetLastIndexOfNumber method takes a <code>System.Char[]</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>index</td><td style='width:75%' class='def'><p>The <code>index</code> parameter of the GetLastIndexOfNumber method takes a <code>System.Int32</code> value. </p>
</td></tr></table></p>


---

<a name="801d0d68-9414-448b-abeb-2c853d09ec9b"></a>
##Public Static GetProperties (System.Type)

###Return Type

`System.Collections.Generic.IEnumerable<System.Reflection.PropertyInfo>`

###Member of Type

[ReflectionUtils](#6e848a6a-ea7d-4af0-b023-cd70a7e96d45)

###Summary

The public static `GetProperties (System.Type)` member of the `ReflectionUtils` class returns a `System.Collections.Generic.IEnumerable<System.Reflection.PropertyInfo>` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>type</td><td style='width:75%' ><p>The <code>type</code> parameter of the GetProperties method takes a <code>System.Type</code> value. </p>
</td></tr></table></p>


---

<a name="40404746-a646-42df-988a-bbb546533f62"></a>
##Public Virtual GetPurchases (System.Int32, System.String, System.String, System.String)

###Return Type

`Android.OS.Bundle`

###Member of Type

[IInAppBillingServiceStub](#e452919a-6bfb-4cf1-b5e6-88131704b783)

###Summary

The public virtual `GetPurchases (System.Int32, System.String, System.String, System.String)` member of the `IInAppBillingServiceStub` class returns a `Android.OS.Bundle` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>apiVersion</td><td style='width:75%' ><p>The <code>apiVersion</code> parameter of the GetPurchases method takes a <code>System.Int32</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>packageName</td><td style='width:75%' class='def'><p>The <code>packageName</code> parameter of the GetPurchases method takes a <code>System.String</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>type</td><td style='width:75%' ><p>The <code>type</code> parameter of the GetPurchases method takes a <code>System.String</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>continuationToken</td><td style='width:75%' class='def'><p>The <code>continuationToken</code> parameter of the GetPurchases method takes a <code>System.String</code> value. </p>
</td></tr></table></p>


---

<a name="34963404-7e55-4a28-8220-f26715b02c51"></a>
##Public Virtual GetPurchases (System.Int32, System.String, System.String, System.String)

###Return Type

`Android.OS.Bundle`

###Member of Type

[IInAppBillingService](#2c2de562-b76b-4126-8476-c2b22a4ccde1)

###Summary

The public virtual `GetPurchases (System.Int32, System.String, System.String, System.String)` member of the `IInAppBillingService` interface returns a `Android.OS.Bundle` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>apiVersion</td><td style='width:75%' ><p>The <code>apiVersion</code> parameter of the GetPurchases method takes a <code>System.Int32</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>packageName</td><td style='width:75%' class='def'><p>The <code>packageName</code> parameter of the GetPurchases method takes a <code>System.String</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>type</td><td style='width:75%' ><p>The <code>type</code> parameter of the GetPurchases method takes a <code>System.String</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>continuationToken</td><td style='width:75%' class='def'><p>The <code>continuationToken</code> parameter of the GetPurchases method takes a <code>System.String</code> value. </p>
</td></tr></table></p>


---

<a name="f26a57e5-e2b0-4b61-8ee9-ea10306911b4"></a>
##Public Virtual GetPurchases (System.String)

###Return Type

`System.Collections.Generic.IList<Xamarin.InAppBilling.Purchase>`

###Member of Type

[IInAppBillingHandler](#9b4f25fc-45a1-4e40-900d-3c3aa0998f95)

###Summary

Gets the purchases.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>itemType</td><td style='width:75%' ><p>Item type (inapp or subs)</p>
</td></tr></table></p>

###Returns

The purchases.

---

<a name="c55b3a4d-c5fc-49a5-96f6-f6e116251fe9"></a>
##Public Virtual GetPurchases (System.String)

###Return Type

`System.Collections.Generic.IList<Xamarin.InAppBilling.Purchase>`

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

Gets a list of all products of a given item type purchased by the current user.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>itemType</td><td style='width:75%' ><p>Item type (product or subs)</p>
</td></tr></table></p>

###Returns

A list of [Product](#98d67a2a-c250-4548-8a90-9e3722fef483)s purchased by the current user.

---

<a name="8a8c090b-3e5b-4558-977e-b71b9ddc3f13"></a>
##Public Static GetReponseCodeFromIntent (Android.Content.Intent)

###Return Type

`System.Int32`

###Member of Type

[Extensions](#9cf0d2a4-88c3-454f-98a0-7517c246be94)

###Summary

Gets the reponse code from intent.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>intent</td><td style='width:75%' ><p>Intent.</p>
</td></tr></table></p>

###Returns

The reponse code from intent.

---

<a name="4f60ac02-b79f-4661-a719-24bf2e8cba12"></a>
##Public Static GetResponseCodeFromBundle (Android.OS.Bundle)

###Return Type

`System.Int32`

###Member of Type

[Extensions](#9cf0d2a4-88c3-454f-98a0-7517c246be94)

###Summary

Gets the response code from bundle.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>bunble</td><td style='width:75%' ><p>Bunble.</p>
</td></tr></table></p>

###Returns

The response code from bundle.

---

<a name="7af47516-e804-4e14-987d-06f19ea90994"></a>
##Public Static GetSetMethod (System.Reflection.FieldInfo)

###Return Type

`Xamarin.InAppBilling.Reflection.ReflectionUtils.SetDelegate`

###Member of Type

[ReflectionUtils](#6e848a6a-ea7d-4af0-b023-cd70a7e96d45)

###Summary

The public static `GetSetMethod (System.Reflection.FieldInfo)` member of the `ReflectionUtils` class returns a `Xamarin.InAppBilling.Reflection.ReflectionUtils.SetDelegate` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>fieldInfo</td><td style='width:75%' ><p>The <code>fieldInfo</code> parameter of the GetSetMethod method takes a <code>System.Reflection.FieldInfo</code> value. </p>
</td></tr></table></p>


---

<a name="5e846f9a-2cf3-4068-a51e-78d003692900"></a>
##Public Static GetSetMethod (System.Reflection.PropertyInfo)

###Return Type

`Xamarin.InAppBilling.Reflection.ReflectionUtils.SetDelegate`

###Member of Type

[ReflectionUtils](#6e848a6a-ea7d-4af0-b023-cd70a7e96d45)

###Summary

The public static `GetSetMethod (System.Reflection.PropertyInfo)` member of the `ReflectionUtils` class returns a `Xamarin.InAppBilling.Reflection.ReflectionUtils.SetDelegate` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>propertyInfo</td><td style='width:75%' ><p>The <code>propertyInfo</code> parameter of the GetSetMethod method takes a <code>System.Reflection.PropertyInfo</code> value. </p>
</td></tr></table></p>


---

<a name="58f97d67-d89b-47ca-b673-e0d36c02abfe"></a>
##Public Static GetSetMethodByExpression (System.Reflection.FieldInfo)

###Return Type

`Xamarin.InAppBilling.Reflection.ReflectionUtils.SetDelegate`

###Member of Type

[ReflectionUtils](#6e848a6a-ea7d-4af0-b023-cd70a7e96d45)

###Summary

The public static `GetSetMethodByExpression (System.Reflection.FieldInfo)` member of the `ReflectionUtils` class returns a `Xamarin.InAppBilling.Reflection.ReflectionUtils.SetDelegate` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>fieldInfo</td><td style='width:75%' ><p>The <code>fieldInfo</code> parameter of the GetSetMethodByExpression method takes a <code>System.Reflection.FieldInfo</code> value. </p>
</td></tr></table></p>


---

<a name="33d1496c-b517-440c-96c9-fa4017f5acaa"></a>
##Public Static GetSetMethodByExpression (System.Reflection.PropertyInfo)

###Return Type

`Xamarin.InAppBilling.Reflection.ReflectionUtils.SetDelegate`

###Member of Type

[ReflectionUtils](#6e848a6a-ea7d-4af0-b023-cd70a7e96d45)

###Summary

The public static `GetSetMethodByExpression (System.Reflection.PropertyInfo)` member of the `ReflectionUtils` class returns a `Xamarin.InAppBilling.Reflection.ReflectionUtils.SetDelegate` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>propertyInfo</td><td style='width:75%' ><p>The <code>propertyInfo</code> parameter of the GetSetMethodByExpression method takes a <code>System.Reflection.PropertyInfo</code> value. </p>
</td></tr></table></p>


---

<a name="217ec0d9-1bfe-443a-ae08-af849e3ef9b0"></a>
##Public Static GetSetMethodByReflection (System.Reflection.FieldInfo)

###Return Type

`Xamarin.InAppBilling.Reflection.ReflectionUtils.SetDelegate`

###Member of Type

[ReflectionUtils](#6e848a6a-ea7d-4af0-b023-cd70a7e96d45)

###Summary

The public static `GetSetMethodByReflection (System.Reflection.FieldInfo)` member of the `ReflectionUtils` class returns a `Xamarin.InAppBilling.Reflection.ReflectionUtils.SetDelegate` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>fieldInfo</td><td style='width:75%' ><p>The <code>fieldInfo</code> parameter of the GetSetMethodByReflection method takes a <code>System.Reflection.FieldInfo</code> value. </p>
</td></tr></table></p>


---

<a name="48309f63-520f-4686-80f5-3f49e7732607"></a>
##Public Static GetSetMethodByReflection (System.Reflection.PropertyInfo)

###Return Type

`Xamarin.InAppBilling.Reflection.ReflectionUtils.SetDelegate`

###Member of Type

[ReflectionUtils](#6e848a6a-ea7d-4af0-b023-cd70a7e96d45)

###Summary

The public static `GetSetMethodByReflection (System.Reflection.PropertyInfo)` member of the `ReflectionUtils` class returns a `Xamarin.InAppBilling.Reflection.ReflectionUtils.SetDelegate` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>propertyInfo</td><td style='width:75%' ><p>The <code>propertyInfo</code> parameter of the GetSetMethodByReflection method takes a <code>System.Reflection.PropertyInfo</code> value. </p>
</td></tr></table></p>


---

<a name="d19a2818-eafc-4128-bea0-3248573d595c"></a>
##Public Static GetSetterMethodInfo (System.Reflection.PropertyInfo)

###Return Type

`System.Reflection.MethodInfo`

###Member of Type

[ReflectionUtils](#6e848a6a-ea7d-4af0-b023-cd70a7e96d45)

###Summary

The public static `GetSetterMethodInfo (System.Reflection.PropertyInfo)` member of the `ReflectionUtils` class returns a `System.Reflection.MethodInfo` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>propertyInfo</td><td style='width:75%' ><p>The <code>propertyInfo</code> parameter of the GetSetterMethodInfo method takes a <code>System.Reflection.PropertyInfo</code> value. </p>
</td></tr></table></p>


---

<a name="1c1136ea-30b9-458a-a7fd-c520b7a03c08"></a>
##Public Virtual GetSkuDetails (System.Int32, System.String, System.String, Android.OS.Bundle)

###Return Type

`Android.OS.Bundle`

###Member of Type

[IInAppBillingService](#2c2de562-b76b-4126-8476-c2b22a4ccde1)

###Summary

The public virtual `GetSkuDetails (System.Int32, System.String, System.String, Android.OS.Bundle)` member of the `IInAppBillingService` interface returns a `Android.OS.Bundle` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>apiVersion</td><td style='width:75%' ><p>The <code>apiVersion</code> parameter of the GetSkuDetails method takes a <code>System.Int32</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>packageName</td><td style='width:75%' class='def'><p>The <code>packageName</code> parameter of the GetSkuDetails method takes a <code>System.String</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>type</td><td style='width:75%' ><p>The <code>type</code> parameter of the GetSkuDetails method takes a <code>System.String</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>skusBundle</td><td style='width:75%' class='def'><p>The <code>skusBundle</code> parameter of the GetSkuDetails method takes a <code>Android.OS.Bundle</code> value. </p>
</td></tr></table></p>


---

<a name="de9672aa-917a-484a-8b64-13f74058aa4a"></a>
##Public Virtual GetSkuDetails (System.Int32, System.String, System.String, Android.OS.Bundle)

###Return Type

`Android.OS.Bundle`

###Member of Type

[IInAppBillingServiceStub](#e452919a-6bfb-4cf1-b5e6-88131704b783)

###Summary

The public virtual `GetSkuDetails (System.Int32, System.String, System.String, Android.OS.Bundle)` member of the `IInAppBillingServiceStub` class returns a `Android.OS.Bundle` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>apiVersion</td><td style='width:75%' ><p>The <code>apiVersion</code> parameter of the GetSkuDetails method takes a <code>System.Int32</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>packageName</td><td style='width:75%' class='def'><p>The <code>packageName</code> parameter of the GetSkuDetails method takes a <code>System.String</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>type</td><td style='width:75%' ><p>The <code>type</code> parameter of the GetSkuDetails method takes a <code>System.String</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>skusBundle</td><td style='width:75%' class='def'><p>The <code>skusBundle</code> parameter of the GetSkuDetails method takes a <code>Android.OS.Bundle</code> value. </p>
</td></tr></table></p>


---

<a name="08e6d3bb-7710-47a9-b912-85e3d147ed8e"></a>
##Virtual GetterValueFactory (System.Type)

###Return Type

`System.Collections.Generic.IDictionary<System.String,Xamarin.InAppBilling.Reflection.ReflectionUtils.GetDelegate>`

###Member of Type

[PocoJsonSerializerStrategy](#1ac2d0dc-01c8-489d-9039-4374c33d0124)

###Summary

The  virtual `GetterValueFactory (System.Type)` member of the `PocoJsonSerializerStrategy` class returns a `System.Collections.Generic.IDictionary<System.String,Xamarin.InAppBilling.Reflection.ReflectionUtils.GetDelegate>` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>type</td><td style='width:75%' ><p>The <code>type</code> parameter of the GetterValueFactory method takes a <code>System.Type</code> value. </p>
</td></tr></table></p>


---

<a name="bd0b96a4-26e7-42ae-8cdd-e11d2d1193df"></a>
##Public Static GetTypeInfo (System.Type)

###Return Type

`System.Type`

###Member of Type

[ReflectionUtils](#6e848a6a-ea7d-4af0-b023-cd70a7e96d45)

###Summary

The public static `GetTypeInfo (System.Type)` member of the `ReflectionUtils` class returns a `System.Type` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>type</td><td style='width:75%' ><p>The <code>type</code> parameter of the GetTypeInfo method takes a <code>System.Type</code> value. </p>
</td></tr></table></p>


---

<a name="52e80b9b-54f2-4c84-af40-792664732727"></a>
##Public Virtual Void HandleActivityResult (System.Int32, Android.App.Result, Android.Content.Intent)

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

Handles the activity result.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>requestCode</td><td style='width:75%' ><p>Request code.</p>
</td></tr><tr><td style='width:25%' class='term'>resultCode</td><td style='width:75%' class='def'><p>Result code.</p>
</td></tr><tr><td style='width:25%' class='term'>data</td><td style='width:75%' ><p>Data.</p>
</td></tr></table></p>


---

<a name="aab21c53-a710-4f46-89eb-24c728c8ccc7"></a>
##Public Virtual Void HandleActivityResult (System.Int32, Android.App.Result, Android.Content.Intent)

###Member of Type

[IInAppBillingHandler](#9b4f25fc-45a1-4e40-900d-3c3aa0998f95)

###Summary

Handles the activity result.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>requestCode</td><td style='width:75%' ><p>Request code.</p>
</td></tr><tr><td style='width:25%' class='term'>resultCode</td><td style='width:75%' class='def'><p>Result code.</p>
</td></tr><tr><td style='width:25%' class='term'>data</td><td style='width:75%' ><p>Data.</p>
</td></tr></table></p>


---

<a name="3bd13de6-9ef9-491b-ad7b-422715955a3f"></a>
##Public Void IInAppBillingServiceStub ()

###Constructor for Type

[IInAppBillingServiceStub](#e452919a-6bfb-4cf1-b5e6-88131704b783)

###Summary

The public `IInAppBillingServiceStub ()` constructor for the `IInAppBillingServiceStub` class.

---

<a name="c756b4d9-c163-42d0-8596-d57d03a40eff"></a>
##Public Void InAppBillingHandler (Android.App.Activity, Com.Android.Vending.Billing.IInAppBillingService, System.String)

###Constructor for Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

Initializes a new instance of the [InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08) class.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>activity</td><td style='width:75%' ><p>Activity.</p>
</td></tr><tr><td style='width:25%' class='term'>billingService</td><td style='width:75%' class='def'><p>Billing service.</p>
</td></tr><tr><td style='width:25%' class='term'>publicKey</td><td style='width:75%' ><p>Public key.</p>
</td></tr></table></p>


---

<a name="5403932a-65a4-4e53-a9a5-2d8a50befe1d"></a>
##Public Void InAppBillingServiceConnection (Android.App.Activity, System.String)

###Constructor for Type

[InAppBillingServiceConnection](#80c1612e-cf5e-41b3-a216-ca89246674e9)

###Summary

Initializes a new instance of the [InAppBillingServiceConnection](#80c1612e-cf5e-41b3-a216-ca89246674e9) class.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>activity</td><td style='width:75%' ><p>Activity.</p>
</td></tr><tr><td style='width:25%' class='term'>publicKey</td><td style='width:75%' class='def'><p>Public key.</p>
</td></tr></table></p>


---

<a name="f47924e9-0915-4c24-a366-b94ce61c1da7"></a>
##Public Static Void Info (System.String, System.Object[])

###Member of Type

[Logger](#4e163874-6eb3-4f60-8c44-9e0260d939fb)

###Summary

Writes general information to the log
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>format</td><td style='width:75%' ><p>Format.</p>
</td></tr><tr><td style='width:25%' class='term'>args</td><td style='width:75%' class='def'><p>Arguments.</p>
</td></tr></table></p>


---

<a name="ebafe101-416d-4d1b-af9c-b8b26c20c85f"></a>
##Private Static InMemoryCrypt (System.Byte[], System.Security.Cryptography.ICryptoTransform)

###Return Type

`System.Byte[]`

###Member of Type

[Crypto](#7b127d1b-7daa-4f26-b23a-5e7236a4af07)

###Summary

Performs an in-memory encrypt/decrypt transformation on a byte array.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>data</td><td style='width:75%' ><p>Data.</p>
</td></tr><tr><td style='width:25%' class='term'>transform</td><td style='width:75%' class='def'><p>Transform.</p>
</td></tr></table></p>

###Returns

The memory crypt.

---

<a name="e3f9a2c5-327f-428c-a9d0-ea32ffa67de9"></a>
##Public Static IsAssignableFrom (System.Type, System.Type)

###Return Type

`System.Boolean`

###Member of Type

[ReflectionUtils](#6e848a6a-ea7d-4af0-b023-cd70a7e96d45)

###Summary

The public static `IsAssignableFrom (System.Type, System.Type)` member of the `ReflectionUtils` class returns a `System.Boolean` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>type1</td><td style='width:75%' ><p>The <code>type1</code> parameter of the IsAssignableFrom method takes a <code>System.Type</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>type2</td><td style='width:75%' class='def'><p>The <code>type2</code> parameter of the IsAssignableFrom method takes a <code>System.Type</code> value. </p>
</td></tr></table></p>


---

<a name="f5a697b8-a414-489c-94f9-012e7f6a8c38"></a>
##Public Virtual IsBillingSupported (System.Int32, System.String, System.String)

###Return Type

`System.Int32`

###Member of Type

[IInAppBillingServiceStub](#e452919a-6bfb-4cf1-b5e6-88131704b783)

###Summary

The public virtual `IsBillingSupported (System.Int32, System.String, System.String)` member of the `IInAppBillingServiceStub` class returns a `System.Int32` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>apiVersion</td><td style='width:75%' ><p>The <code>apiVersion</code> parameter of the IsBillingSupported method takes a <code>System.Int32</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>packageName</td><td style='width:75%' class='def'><p>The <code>packageName</code> parameter of the IsBillingSupported method takes a <code>System.String</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>type</td><td style='width:75%' ><p>The <code>type</code> parameter of the IsBillingSupported method takes a <code>System.String</code> value. </p>
</td></tr></table></p>


---

<a name="6a62d15d-e891-4948-904a-e2bc2f31a6a6"></a>
##Public Virtual IsBillingSupported (System.Int32, System.String, System.String)

###Return Type

`System.Int32`

###Member of Type

[IInAppBillingService](#2c2de562-b76b-4126-8476-c2b22a4ccde1)

###Summary

The public virtual `IsBillingSupported (System.Int32, System.String, System.String)` member of the `IInAppBillingService` interface returns a `System.Int32` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>apiVersion</td><td style='width:75%' ><p>The <code>apiVersion</code> parameter of the IsBillingSupported method takes a <code>System.Int32</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>packageName</td><td style='width:75%' class='def'><p>The <code>packageName</code> parameter of the IsBillingSupported method takes a <code>System.String</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>type</td><td style='width:75%' ><p>The <code>type</code> parameter of the IsBillingSupported method takes a <code>System.String</code> value. </p>
</td></tr></table></p>


---

<a name="9e98d740-7d82-479e-9f69-fb1ba10cc4e8"></a>
##Public Static IsNullableType (System.Type)

###Return Type

`System.Boolean`

###Member of Type

[ReflectionUtils](#6e848a6a-ea7d-4af0-b023-cd70a7e96d45)

###Summary

The public static `IsNullableType (System.Type)` member of the `ReflectionUtils` class returns a `System.Boolean` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>type</td><td style='width:75%' ><p>The <code>type</code> parameter of the IsNullableType method takes a <code>System.Type</code> value. </p>
</td></tr></table></p>


---

<a name="676d92f7-8f3f-44a9-a8ed-8a31496b0b68"></a>
##Private Static IsNumeric (System.Object)

###Return Type

`System.Boolean`

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

Determines if a given object is numeric in any way (can be integer, double, null, etc).
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>value</td><td style='width:75%' ><p>The <code>value</code> parameter of the IsNumeric method takes a <code>System.Object</code> value. </p>
</td></tr></table></p>


---

<a name="3b047da0-4d16-4c9a-b8e7-94e967b597d0"></a>
##Public Static IsTypeDictionary (System.Type)

###Return Type

`System.Boolean`

###Member of Type

[ReflectionUtils](#6e848a6a-ea7d-4af0-b023-cd70a7e96d45)

###Summary

The public static `IsTypeDictionary (System.Type)` member of the `ReflectionUtils` class returns a `System.Boolean` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>type</td><td style='width:75%' ><p>The <code>type</code> parameter of the IsTypeDictionary method takes a <code>System.Type</code> value. </p>
</td></tr></table></p>


---

<a name="e0160977-6558-440a-88b3-3b3e1475ebda"></a>
##Public Static IsTypeGeneric (System.Type)

###Return Type

`System.Boolean`

###Member of Type

[ReflectionUtils](#6e848a6a-ea7d-4af0-b023-cd70a7e96d45)

###Summary

The public static `IsTypeGeneric (System.Type)` member of the `ReflectionUtils` class returns a `System.Boolean` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>type</td><td style='width:75%' ><p>The <code>type</code> parameter of the IsTypeGeneric method takes a <code>System.Type</code> value. </p>
</td></tr></table></p>


---

<a name="fe8ba144-0cfd-4e98-9d0e-3b5fbaaca1b2"></a>
##Public Static IsTypeGenericeCollectionInterface (System.Type)

###Return Type

`System.Boolean`

###Member of Type

[ReflectionUtils](#6e848a6a-ea7d-4af0-b023-cd70a7e96d45)

###Summary

The public static `IsTypeGenericeCollectionInterface (System.Type)` member of the `ReflectionUtils` class returns a `System.Boolean` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>type</td><td style='width:75%' ><p>The <code>type</code> parameter of the IsTypeGenericeCollectionInterface method takes a <code>System.Type</code> value. </p>
</td></tr></table></p>


---

<a name="85229dd8-6cd1-444c-b9f2-e335d2831721"></a>
##Public Static IsValueType (System.Type)

###Return Type

`System.Boolean`

###Member of Type

[ReflectionUtils](#6e848a6a-ea7d-4af0-b023-cd70a7e96d45)

###Summary

The public static `IsValueType (System.Type)` member of the `ReflectionUtils` class returns a `System.Boolean` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>type</td><td style='width:75%' ><p>The <code>type</code> parameter of the IsValueType method takes a <code>System.Type</code> value. </p>
</td></tr></table></p>


---

<a name="6f43217e-ee3f-43b4-9ee1-4229c5946070"></a>
##Public Void JsonArray ()

###Constructor for Type

[JsonArray](#fc61b8f7-b351-48f2-9773-888e050fffcb)

###Summary

Initializes a new instance of the [JsonArray](#fc61b8f7-b351-48f2-9773-888e050fffcb) class. 

---

<a name="344f33eb-8d65-4ae4-811d-06fb732c899d"></a>
##Public Void JsonArray (System.Int32)

###Constructor for Type

[JsonArray](#fc61b8f7-b351-48f2-9773-888e050fffcb)

###Summary

Initializes a new instance of the [JsonArray](#fc61b8f7-b351-48f2-9773-888e050fffcb) class. 
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>capacity</td><td style='width:75%' ><p>The capacity of the json array.</p>
</td></tr></table></p>


---

<a name="120b9086-4869-4079-8b65-3aa3e5c8ef20"></a>
##Public Void JsonObject ()

###Constructor for Type

[JsonObject](#4db2613b-bdf6-4cff-a6da-804b56455cbe)

###Summary

Initializes a new instance of [JsonObject](#4db2613b-bdf6-4cff-a6da-804b56455cbe).

---

<a name="f8c9b61e-59e2-463b-a00f-279fb156d1f5"></a>
##Public Void JsonObject (System.Collections.Generic.IEqualityComparer< System.String >)

###Constructor for Type

[JsonObject](#4db2613b-bdf6-4cff-a6da-804b56455cbe)

###Summary

Initializes a new instance of [JsonObject](#4db2613b-bdf6-4cff-a6da-804b56455cbe).
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>comparer</td><td style='width:75%' ><p>The <code>IEqualityComparer</code>1<code>implementation to use when comparing keys, or null to use the default</code>EqualityComparer<code>1</code> for the type of the key.</p>
</td></tr></table></p>


---

<a name="df51e3d7-0b24-4616-97ff-c4ad263b783b"></a>
##Public Void Logger ()

###Constructor for Type

[Logger](#4e163874-6eb3-4f60-8c44-9e0260d939fb)

###Summary

The public `Logger ()` constructor for the `Logger` class.

---

<a name="930c090c-305d-40a8-ad0f-8a699a223e99"></a>
##Private Static LookAhead (System.Char[], System.Int32)

###Return Type

`System.Int32`

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

The private static `LookAhead (System.Char[], System.Int32)` member of the `SimpleJson` class returns a `System.Int32` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>json</td><td style='width:75%' ><p>The <code>json</code> parameter of the LookAhead method takes a <code>System.Char[]</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>index</td><td style='width:75%' class='def'><p>The <code>index</code> parameter of the LookAhead method takes a <code>System.Int32</code> value. </p>
</td></tr></table></p>


---

<a name="c452ce8e-65ef-4573-aee8-bae9cdffc32a"></a>
##Virtual MapClrMemberNameToJsonFieldName (System.String)

###Return Type

`System.String`

###Member of Type

[PocoJsonSerializerStrategy](#1ac2d0dc-01c8-489d-9039-4374c33d0124)

###Summary

The  virtual `MapClrMemberNameToJsonFieldName (System.String)` member of the `PocoJsonSerializerStrategy` class returns a `System.String` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>clrPropertyName</td><td style='width:75%' ><p>The <code>clrPropertyName</code> parameter of the MapClrMemberNameToJsonFieldName method takes a <code>System.String</code> value. </p>
</td></tr></table></p>


---

<a name="426c95c0-30c9-4ecb-9df8-6ae2a1c79dc3"></a>
##Private Static NextToken (System.Char[], System.Int32&)

###Return Type

`System.Int32`

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

The private static `NextToken (System.Char[], System.Int32&)` member of the `SimpleJson` class returns a `System.Int32` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>json</td><td style='width:75%' ><p>The <code>json</code> parameter of the NextToken method takes a <code>System.Char[]</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>Ref index</td><td style='width:75%' class='def'><p>The <code>index</code> parameter of the NextToken method takes a <code>System.Int32&amp;</code> value. Since <code>index</code> returns any modifications to the caller, it <em>must</em> be passed with the <code>ref</code> modifier.</p>
</td></tr></table></p>


---

<a name="fcf0b617-edb8-44f8-829e-488145939579"></a>
##Public Virtual Void OnServiceConnected (Android.Content.ComponentName, Android.OS.IBinder)

###Member of Type

[InAppBillingServiceConnection](#80c1612e-cf5e-41b3-a216-ca89246674e9)

###Summary

Raises the service connected event.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>name</td><td style='width:75%' ><p>Name.</p>
</td></tr><tr><td style='width:25%' class='term'>service</td><td style='width:75%' class='def'><p>Service.</p>
</td></tr></table></p>


---

<a name="514ac181-bc06-43a7-b301-c4c54213d20f"></a>
##Public Virtual Void OnServiceDisconnected (Android.Content.ComponentName)

###Member of Type

[InAppBillingServiceConnection](#80c1612e-cf5e-41b3-a216-ca89246674e9)

###Summary

Raises the service disconnected event.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>name</td><td style='width:75%' ><p>Name.</p>
</td></tr></table></p>


---

<a name="0db293b8-0221-4769-8fa6-ea1d65852406"></a>
##Virtual OnTransact (System.Int32, Android.OS.Parcel, Android.OS.Parcel, System.Int32)

###Return Type

`System.Boolean`

###Member of Type

[IInAppBillingServiceStub](#e452919a-6bfb-4cf1-b5e6-88131704b783)

###Summary

The  virtual `OnTransact (System.Int32, Android.OS.Parcel, Android.OS.Parcel, System.Int32)` member of the `IInAppBillingServiceStub` class returns a `System.Boolean` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>code</td><td style='width:75%' ><p>The <code>code</code> parameter of the OnTransact method takes a <code>System.Int32</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>data</td><td style='width:75%' class='def'><p>The <code>data</code> parameter of the OnTransact method takes a <code>Android.OS.Parcel</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>reply</td><td style='width:75%' ><p>The <code>reply</code> parameter of the OnTransact method takes a <code>Android.OS.Parcel</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>flags</td><td style='width:75%' class='def'><p>The <code>flags</code> parameter of the OnTransact method takes a <code>System.Int32</code> value. </p>
</td></tr></table></p>


---

<a name="ea65bac0-b90b-42af-9ceb-5c4056efdbc3"></a>
##Private Static ParseArray (System.Char[], System.Int32&, System.Boolean&)

###Return Type

[Xamarin.InAppBilling.JsonArray](#fc61b8f7-b351-48f2-9773-888e050fffcb)

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

The private static `ParseArray (System.Char[], System.Int32&, System.Boolean&)` member of the `SimpleJson` class returns a `Xamarin.InAppBilling.JsonArray` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>json</td><td style='width:75%' ><p>The <code>json</code> parameter of the ParseArray method takes a <code>System.Char[]</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>Ref index</td><td style='width:75%' class='def'><p>The <code>index</code> parameter of the ParseArray method takes a <code>System.Int32&amp;</code> value. Since <code>index</code> returns any modifications to the caller, it <em>must</em> be passed with the <code>ref</code> modifier.</p>
</td></tr><tr><td style='width:25%' class='term'>Ref success</td><td style='width:75%' ><p>The <code>success</code> parameter of the ParseArray method takes a <code>System.Boolean&amp;</code> value. Since <code>success</code> returns any modifications to the caller, it <em>must</em> be passed with the <code>ref</code> modifier.</p>
</td></tr></table></p>


---

<a name="78d2d383-7a63-4317-aca7-d4fdc79d374a"></a>
##Private Static ParseNumber (System.Char[], System.Int32&, System.Boolean&)

###Return Type

`System.Object`

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

The private static `ParseNumber (System.Char[], System.Int32&, System.Boolean&)` member of the `SimpleJson` class returns a `System.Object` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>json</td><td style='width:75%' ><p>The <code>json</code> parameter of the ParseNumber method takes a <code>System.Char[]</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>Ref index</td><td style='width:75%' class='def'><p>The <code>index</code> parameter of the ParseNumber method takes a <code>System.Int32&amp;</code> value. Since <code>index</code> returns any modifications to the caller, it <em>must</em> be passed with the <code>ref</code> modifier.</p>
</td></tr><tr><td style='width:25%' class='term'>Ref success</td><td style='width:75%' ><p>The <code>success</code> parameter of the ParseNumber method takes a <code>System.Boolean&amp;</code> value. Since <code>success</code> returns any modifications to the caller, it <em>must</em> be passed with the <code>ref</code> modifier.</p>
</td></tr></table></p>


---

<a name="db9514e6-e8a7-4ab9-83df-de206ee8ee01"></a>
##Private Static ParseObject (System.Char[], System.Int32&, System.Boolean&)

###Return Type

`System.Collections.Generic.IDictionary<System.String,System.Object>`

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

The private static `ParseObject (System.Char[], System.Int32&, System.Boolean&)` member of the `SimpleJson` class returns a `System.Collections.Generic.IDictionary<System.String,System.Object>` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>json</td><td style='width:75%' ><p>The <code>json</code> parameter of the ParseObject method takes a <code>System.Char[]</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>Ref index</td><td style='width:75%' class='def'><p>The <code>index</code> parameter of the ParseObject method takes a <code>System.Int32&amp;</code> value. Since <code>index</code> returns any modifications to the caller, it <em>must</em> be passed with the <code>ref</code> modifier.</p>
</td></tr><tr><td style='width:25%' class='term'>Ref success</td><td style='width:75%' ><p>The <code>success</code> parameter of the ParseObject method takes a <code>System.Boolean&amp;</code> value. Since <code>success</code> returns any modifications to the caller, it <em>must</em> be passed with the <code>ref</code> modifier.</p>
</td></tr></table></p>


---

<a name="5284aa61-f92c-4ebc-80df-f85ab4a174ce"></a>
##Private Static ParseString (System.Char[], System.Int32&, System.Boolean&)

###Return Type

`System.String`

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

The private static `ParseString (System.Char[], System.Int32&, System.Boolean&)` member of the `SimpleJson` class returns a `System.String` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>json</td><td style='width:75%' ><p>The <code>json</code> parameter of the ParseString method takes a <code>System.Char[]</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>Ref index</td><td style='width:75%' class='def'><p>The <code>index</code> parameter of the ParseString method takes a <code>System.Int32&amp;</code> value. Since <code>index</code> returns any modifications to the caller, it <em>must</em> be passed with the <code>ref</code> modifier.</p>
</td></tr><tr><td style='width:25%' class='term'>Ref success</td><td style='width:75%' ><p>The <code>success</code> parameter of the ParseString method takes a <code>System.Boolean&amp;</code> value. Since <code>success</code> returns any modifications to the caller, it <em>must</em> be passed with the <code>ref</code> modifier.</p>
</td></tr></table></p>


---

<a name="4d7b06d2-00ff-49ff-a8a8-340c1d8dc0b9"></a>
##Private Static ParseValue (System.Char[], System.Int32&, System.Boolean&)

###Return Type

`System.Object`

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

The private static `ParseValue (System.Char[], System.Int32&, System.Boolean&)` member of the `SimpleJson` class returns a `System.Object` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>json</td><td style='width:75%' ><p>The <code>json</code> parameter of the ParseValue method takes a <code>System.Char[]</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>Ref index</td><td style='width:75%' class='def'><p>The <code>index</code> parameter of the ParseValue method takes a <code>System.Int32&amp;</code> value. Since <code>index</code> returns any modifications to the caller, it <em>must</em> be passed with the <code>ref</code> modifier.</p>
</td></tr><tr><td style='width:25%' class='term'>Ref success</td><td style='width:75%' ><p>The <code>success</code> parameter of the ParseValue method takes a <code>System.Boolean&amp;</code> value. Since <code>success</code> returns any modifications to the caller, it <em>must</em> be passed with the <code>ref</code> modifier.</p>
</td></tr></table></p>


---

<a name="ab862f9d-504f-4a3d-b3ee-e4d8e316f9a2"></a>
##Private Static Void PocoJsonSerializerStrategy ()

###Constructor for Type

[PocoJsonSerializerStrategy](#1ac2d0dc-01c8-489d-9039-4374c33d0124)

###Summary

The public `PocoJsonSerializerStrategy ()` constructor for the `PocoJsonSerializerStrategy` class.

---

<a name="dde87bcd-f40c-4615-86ce-8215b747cef6"></a>
##Public Void Product ()

###Constructor for Type

[Product](#98d67a2a-c250-4548-8a90-9e3722fef483)

###Summary

Initializes a new instance of the [Product](#98d67a2a-c250-4548-8a90-9e3722fef483) class.

---

<a name="9db0762f-aba1-4acf-8c3d-fa35f07df217"></a>
##Public Void Purchase ()

###Constructor for Type

[Purchase](#03489ba4-1d9d-4430-8e09-b3bff9875fac)

###Summary

The public `Purchase ()` constructor for the `Purchase` class.

---

<a name="6b7986a4-ffed-4c0c-92e8-d1888318f740"></a>
##Public Virtual QueryInventoryAsync (System.Collections.Generic.IList< System.String >, System.String)

###Return Type

`System.Threading.Tasks.Task<System.Collections.Generic.IList<Xamarin.InAppBilling.Product>>`

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

Queries the inventory asynchronously and returns a list of [Product](#98d67a2a-c250-4548-8a90-9e3722fef483)s matching  the given list of SKU numbers.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>skuList</td><td style='width:75%' ><p>Sku list.</p>
</td></tr><tr><td style='width:25%' class='term'>itemType</td><td style='width:75%' class='def'><p>The <a href="#cb80eb77-1e36-46b1-9262-60453abc97dc">ItemType</a> of product being queried.</p>
</td></tr></table></p>

###Returns

List of [Product](#98d67a2a-c250-4548-8a90-9e3722fef483)s matching the given list of SKUs.

---

<a name="7daf36e3-87e5-4dec-85ac-c5871a4a5d5e"></a>
##Public Virtual QueryInventoryAsync (System.Collections.Generic.IList< System.String >, System.String)

###Return Type

`System.Threading.Tasks.Task<System.Collections.Generic.IList<Xamarin.InAppBilling.Product>>`

###Member of Type

[IInAppBillingHandler](#9b4f25fc-45a1-4e40-900d-3c3aa0998f95)

###Summary

Queries the inventory asynchronously.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>skuList</td><td style='width:75%' ><p>Sku list.</p>
</td></tr><tr><td style='width:25%' class='term'>itemType</td><td style='width:75%' class='def'><p>Item type.</p>
</td></tr></table></p>

###Returns

List of strings

---

<a name="9adbfa47-fbdd-4510-998c-1a14de496b01"></a>
##Void RaiseBuyProductError (System.Int32, System.String)

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

Raises the buy product error event.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>responseCode</td><td style='width:75%' ><p>Response code.</p>
</td></tr><tr><td style='width:25%' class='term'>sku</td><td style='width:75%' class='def'><p>Sku.</p>
</td></tr></table></p>


---

<a name="bd9493c5-1d5e-40dd-bf64-1bb83381bddd"></a>
##Void RaiseInAppBillingProcessingError (System.String)

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

Raises the in app billing processing error event
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>message</td><td style='width:75%' ><p>Message.</p>
</td></tr></table></p>


---

<a name="ddf4cc61-73a5-443b-a42c-0c673beaf15e"></a>
##Void RaiseOnConnected ()

###Member of Type

[InAppBillingServiceConnection](#80c1612e-cf5e-41b3-a216-ca89246674e9)

###Summary

Raises the on connected event.

---

<a name="d24c2542-2fe0-4fa3-b17f-d715b345ed30"></a>
##Virtual Void RaiseOnDisconnected ()

###Member of Type

[InAppBillingServiceConnection](#80c1612e-cf5e-41b3-a216-ca89246674e9)

###Summary

Raises the on disconnected event.

---

<a name="c84ad7d8-0a8b-42f8-a0d3-b3cd916bbe18"></a>
##Void RaiseOnGetProductsError (System.Int32, Android.OS.Bundle)

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

Raises the on get products error event.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>responseCode</td><td style='width:75%' ><p>Response code.</p>
</td></tr><tr><td style='width:25%' class='term'>ownedItems</td><td style='width:75%' class='def'><p>Owned items.</p>
</td></tr></table></p>


---

<a name="37a5dc82-9f88-4ee7-92f0-61ba7a392726"></a>
##Virtual Void RaiseOnInAppBillingError (Xamarin.InAppBilling.InAppBillingErrorType, System.String)

###Member of Type

[InAppBillingServiceConnection](#80c1612e-cf5e-41b3-a216-ca89246674e9)

###Summary

Raises the on in app billing error.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>error</td><td style='width:75%' ><p>Error.</p>
</td></tr><tr><td style='width:25%' class='term'>message</td><td style='width:75%' class='def'><p>Message.</p>
</td></tr></table></p>


---

<a name="910951ee-a17f-4c06-a8f8-6427c8f9b9d0"></a>
##Void RaiseOnInvalidOwnedItemsBundleReturned (Android.OS.Bundle)

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

Raises the on invalid owned items bundle returned.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>ownedItems</td><td style='width:75%' ><p>Owned items.</p>
</td></tr></table></p>


---

<a name="76d7cfa8-761d-4b26-a410-a9a54ed7c767"></a>
##Void RaiseOnProductPurchased (System.Int32, Xamarin.InAppBilling.Purchase, System.String, System.String)

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

Raises the on product purchase completed event.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>response</td><td style='width:75%' ><p>The response code returned from Google Play Services.</p>
</td></tr><tr><td style='width:25%' class='term'>purchase</td><td style='width:75%' class='def'><p>Information about the purchase.</p>
</td></tr><tr><td style='width:25%' class='term'>purchaseData</td><td style='width:75%' ><p>The <code>purchaseData</code> parameter of the RaiseOnProductPurchased method takes a <code>System.String</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>purchaseSignature</td><td style='width:75%' class='def'><p>The <code>purchaseSignature</code> parameter of the RaiseOnProductPurchased method takes a <code>System.String</code> value. </p>
</td></tr></table></p>


---

<a name="9bdd94ec-3427-4de9-b92c-41b1fba34ab3"></a>
##Void RaiseOnProductPurchasedError (System.Int32, System.String)

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

Raises the on product purchased error event.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>responseCode</td><td style='width:75%' ><p>Response code.</p>
</td></tr><tr><td style='width:25%' class='term'>sku</td><td style='width:75%' class='def'><p>Sku.</p>
</td></tr></table></p>

###Remarks

The `responseCode` will be a value from [BillingResult](#ef5b6352-1a73-4ee5-a808-a03384c31044).

---

<a name="532bff74-4d9d-4ed9-a14f-978671b923a0"></a>
##Void RaiseOnPurchaseConsumed (System.String)

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

Raises the on product consumed.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>token</td><td style='width:75%' ><p>Token.</p>
</td></tr></table></p>


---

<a name="b666c409-58de-4e81-b8f5-22dac1bddb80"></a>
##Void RaiseOnPurchaseConsumedError (System.Int32, System.String)

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

Raises the on product consumed error.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>responseCode</td><td style='width:75%' ><p>Response code.</p>
</td></tr><tr><td style='width:25%' class='term'>token</td><td style='width:75%' class='def'><p>Token.</p>
</td></tr></table></p>

###Remarks

The `responseCode` will be a value from [BillingResult](#ef5b6352-1a73-4ee5-a808-a03384c31044).

---

<a name="14ae91f3-9acd-432e-8810-c1a9712c7c83"></a>
##Void RaiseOnPurchaseFailedValidation (Xamarin.InAppBilling.Purchase, System.String, System.String)

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

Raises the OnPurchaseFailedValidation event
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>purchase</td><td style='width:75%' ><p>The purchase information for the product</p>
</td></tr><tr><td style='width:25%' class='term'>purchaseData</td><td style='width:75%' class='def'><p>The <code>purchaseData</code> parameter of the RaiseOnPurchaseFailedValidation method takes a <code>System.String</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>purchaseSignature</td><td style='width:75%' ><p>The <code>purchaseSignature</code> parameter of the RaiseOnPurchaseFailedValidation method takes a <code>System.String</code> value. </p>
</td></tr></table></p>


---

<a name="02554a0b-642a-4afc-89a3-c9dbd95681b8"></a>
##Void RaiseOnUserCanceled ()

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

Raises the on user canceled event.

---

<a name="157134bf-2bf9-4ee1-bbd0-ce0164c8930a"></a>
##Void RaiseQueryInventoryError (System.Int32, Android.OS.Bundle)

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

Raises the query inventory error event.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>responseCode</td><td style='width:75%' ><p>Response code.</p>
</td></tr><tr><td style='width:25%' class='term'>skuDetails</td><td style='width:75%' class='def'><p>Sku details.</p>
</td></tr></table></p>


---

<a name="e569f918-7846-4d35-9c68-aca3ee8c0b4a"></a>
##Private Static Void ReflectionUtils ()

###Constructor for Type

[ReflectionUtils](#6e848a6a-ea7d-4af0-b023-cd70a7e96d45)

###Summary

The public `ReflectionUtils ()` constructor for the `ReflectionUtils` class.

---

<a name="fdccc60b-a4e5-4c8c-9eac-e479cd264a01"></a>
##Public Virtual Remove (System.Collections.Generic.KeyValuePair< System.String, System.Object >)

###Return Type

`System.Boolean`

###Member of Type

[JsonObject](#4db2613b-bdf6-4cff-a6da-804b56455cbe)

###Summary

Removes the specified item.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>item</td><td style='width:75%' ><p>The item.</p>
</td></tr></table></p>


---

<a name="b884cac8-1e8f-41d8-8173-58b7c7ca373f"></a>
##Public Virtual Remove (System.String)

###Return Type

`System.Boolean`

###Member of Type

[JsonObject](#4db2613b-bdf6-4cff-a6da-804b56455cbe)

###Summary

Removes the specified key.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>key</td><td style='width:75%' ><p>The key.</p>
</td></tr></table></p>


---

<a name="ed7159d1-c0bb-43c5-872b-4d494db50c46"></a>
##Public Void Resource ()

###Constructor for Type

[Resource](#5d7b3738-d0ca-47aa-bf21-6d4aa1b59051)

###Summary

The private static `Resource ()` constructor for the `Resource` class.

---

<a name="75a32fc8-a2a9-4d7a-acce-a9a0868f05b5"></a>
##Public Void Security ()

###Constructor for Type

[Security](#9e6b0e98-9aae-449f-a739-3b32774c3e76)

###Summary

The public `Security ()` constructor for the `Security` class.

---

<a name="684f93d5-629a-452f-9e28-d58040a26f22"></a>
##Private Static SerializeArray (Xamarin.InAppBilling.IJsonSerializerStrategy, System.Collections.IEnumerable, System.Text.StringBuilder)

###Return Type

`System.Boolean`

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

The private static `SerializeArray (Xamarin.InAppBilling.IJsonSerializerStrategy, System.Collections.IEnumerable, System.Text.StringBuilder)` member of the `SimpleJson` class returns a `System.Boolean` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>jsonSerializerStrategy</td><td style='width:75%' ><p>The <code>jsonSerializerStrategy</code> parameter of the SerializeArray method takes a <code>Xamarin.InAppBilling.IJsonSerializerStrategy</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>anArray</td><td style='width:75%' class='def'><p>The <code>anArray</code> parameter of the SerializeArray method takes a <code>System.Collections.IEnumerable</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>builder</td><td style='width:75%' ><p>The <code>builder</code> parameter of the SerializeArray method takes a <code>System.Text.StringBuilder</code> value. </p>
</td></tr></table></p>


---

<a name="f94c841d-968a-448c-8580-0dcfb5819a7c"></a>
##Virtual SerializeEnum (System.Enum)

###Return Type

`System.Object`

###Member of Type

[PocoJsonSerializerStrategy](#1ac2d0dc-01c8-489d-9039-4374c33d0124)

###Summary

The  virtual `SerializeEnum (System.Enum)` member of the `PocoJsonSerializerStrategy` class returns a `System.Object` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>p</td><td style='width:75%' ><p>The <code>p</code> parameter of the SerializeEnum method takes a <code>System.Enum</code> value. </p>
</td></tr></table></p>


---

<a name="1cf78588-e157-437b-b680-31ea43683d44"></a>
##Private Static SerializeNumber (System.Object, System.Text.StringBuilder)

###Return Type

`System.Boolean`

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

The private static `SerializeNumber (System.Object, System.Text.StringBuilder)` member of the `SimpleJson` class returns a `System.Boolean` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>number</td><td style='width:75%' ><p>The <code>number</code> parameter of the SerializeNumber method takes a <code>System.Object</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>builder</td><td style='width:75%' class='def'><p>The <code>builder</code> parameter of the SerializeNumber method takes a <code>System.Text.StringBuilder</code> value. </p>
</td></tr></table></p>


---

<a name="87d6934f-60b1-4ebe-8af6-23345ced1067"></a>
##Public Static SerializeObject (System.Object)

###Return Type

`System.String`

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

The public static `SerializeObject (System.Object)` member of the `SimpleJson` class returns a `System.String` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>json</td><td style='width:75%' ><p>The <code>json</code> parameter of the SerializeObject method takes a <code>System.Object</code> value. </p>
</td></tr></table></p>


---

<a name="b9b98901-5dce-438c-8008-1d7e9ca09cd3"></a>
##Public Static SerializeObject (System.Object, Xamarin.InAppBilling.IJsonSerializerStrategy)

###Return Type

`System.String`

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

Converts a IDictionary&lt;string,object&gt; / IList&lt;object&gt; object into a JSON string
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>json</td><td style='width:75%' ><p>A IDictionary&lt;string,object&gt; / IList&lt;object&gt;</p>
</td></tr><tr><td style='width:25%' class='term'>jsonSerializerStrategy</td><td style='width:75%' class='def'><p>Serializer strategy to use</p>
</td></tr></table></p>

###Returns

A JSON encoded string, or null if object 'json' is not serializable

---

<a name="5ccfc8e9-fd12-44b1-99a1-b9262dc029d3"></a>
##Private Static SerializeObject (Xamarin.InAppBilling.IJsonSerializerStrategy, System.Collections.IEnumerable, System.Collections.IEnumerable, System.Text.StringBuilder)

###Return Type

`System.Boolean`

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

The private static `SerializeObject (Xamarin.InAppBilling.IJsonSerializerStrategy, System.Collections.IEnumerable, System.Collections.IEnumerable, System.Text.StringBuilder)` member of the `SimpleJson` class returns a `System.Boolean` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>jsonSerializerStrategy</td><td style='width:75%' ><p>The <code>jsonSerializerStrategy</code> parameter of the SerializeObject method takes a <code>Xamarin.InAppBilling.IJsonSerializerStrategy</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>keys</td><td style='width:75%' class='def'><p>The <code>keys</code> parameter of the SerializeObject method takes a <code>System.Collections.IEnumerable</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>values</td><td style='width:75%' ><p>The <code>values</code> parameter of the SerializeObject method takes a <code>System.Collections.IEnumerable</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>builder</td><td style='width:75%' class='def'><p>The <code>builder</code> parameter of the SerializeObject method takes a <code>System.Text.StringBuilder</code> value. </p>
</td></tr></table></p>


---

<a name="699a8683-8ff4-458e-a41a-c91b8c40c274"></a>
##Private Static SerializeString (System.String, System.Text.StringBuilder)

###Return Type

`System.Boolean`

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

The private static `SerializeString (System.String, System.Text.StringBuilder)` member of the `SimpleJson` class returns a `System.Boolean` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>aString</td><td style='width:75%' ><p>The <code>aString</code> parameter of the SerializeString method takes a <code>System.String</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>builder</td><td style='width:75%' class='def'><p>The <code>builder</code> parameter of the SerializeString method takes a <code>System.Text.StringBuilder</code> value. </p>
</td></tr></table></p>


---

<a name="a6b0f1f2-7ca7-47e8-8927-df30e8f8ab71"></a>
##Private Static SerializeValue (Xamarin.InAppBilling.IJsonSerializerStrategy, System.Object, System.Text.StringBuilder)

###Return Type

`System.Boolean`

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

The private static `SerializeValue (Xamarin.InAppBilling.IJsonSerializerStrategy, System.Object, System.Text.StringBuilder)` member of the `SimpleJson` class returns a `System.Boolean` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>jsonSerializerStrategy</td><td style='width:75%' ><p>The <code>jsonSerializerStrategy</code> parameter of the SerializeValue method takes a <code>Xamarin.InAppBilling.IJsonSerializerStrategy</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>value</td><td style='width:75%' class='def'><p>The <code>value</code> parameter of the SerializeValue method takes a <code>System.Object</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>builder</td><td style='width:75%' ><p>The <code>builder</code> parameter of the SerializeValue method takes a <code>System.Text.StringBuilder</code> value. </p>
</td></tr></table></p>


---

<a name="7ca1292b-7e74-40ec-a069-ee7d3edea58a"></a>
##Virtual SetterValueFactory (System.Type)

###Return Type

`System.Collections.Generic.IDictionary<System.String,System.Collections.Generic.KeyValuePair<System.Type,Xamarin.InAppBilling.Reflection.ReflectionUtils.SetDelegate>>`

###Member of Type

[PocoJsonSerializerStrategy](#1ac2d0dc-01c8-489d-9039-4374c33d0124)

###Summary

The  virtual `SetterValueFactory (System.Type)` member of the `PocoJsonSerializerStrategy` class returns a `System.Collections.Generic.IDictionary<System.String,System.Collections.Generic.KeyValuePair<System.Type,Xamarin.InAppBilling.Reflection.ReflectionUtils.SetDelegate>>` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>type</td><td style='width:75%' ><p>The <code>type</code> parameter of the SetterValueFactory method takes a <code>System.Type</code> value. </p>
</td></tr></table></p>


---

<a name="328d7c85-5b43-46c9-998f-8956bdf4ea92"></a>
##Private Static Void SimpleJson ()

###Constructor for Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

The private static `SimpleJson ()` constructor for the `SimpleJson` class.

---

<a name="e465c3b5-233d-4274-8420-6bc8c02271fa"></a>
##Private Virtual System#Collections#IEnumerable#GetEnumerator ()

###Return Type

`System.Collections.IEnumerator`

###Member of Type

[JsonObject](#4db2613b-bdf6-4cff-a6da-804b56455cbe)

###Summary

Returns an enumerator that iterates through a collection.
###Returns

An `IEnumerator` object that can be used to iterate through the collection.

---

<a name="6d304afb-856b-4ba4-8ada-b4149bf17be3"></a>
##Public Static ToNullableType (System.Object, System.Type)

###Return Type

`System.Object`

###Member of Type

[ReflectionUtils](#6e848a6a-ea7d-4af0-b023-cd70a7e96d45)

###Summary

The public static `ToNullableType (System.Object, System.Type)` member of the `ReflectionUtils` class returns a `System.Object` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>obj</td><td style='width:75%' ><p>The <code>obj</code> parameter of the ToNullableType method takes a <code>System.Object</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>nullableType</td><td style='width:75%' class='def'><p>The <code>nullableType</code> parameter of the ToNullableType method takes a <code>System.Type</code> value. </p>
</td></tr></table></p>


---

<a name="42bdff02-49db-4410-b90c-9c25532455ac"></a>
##Public Virtual ToString ()

###Return Type

`System.String`

###Member of Type

[Purchase](#03489ba4-1d9d-4430-8e09-b3bff9875fac)

###Summary

Converts the [Purchase](#03489ba4-1d9d-4430-8e09-b3bff9875fac) into a `string`
###Returns

The `string` representation of the [Product](#98d67a2a-c250-4548-8a90-9e3722fef483).

---

<a name="7408a5e1-6d8b-46fb-911a-454d37cc0386"></a>
##Public Virtual ToString ()

###Return Type

`System.String`

###Member of Type

[JsonObject](#4db2613b-bdf6-4cff-a6da-804b56455cbe)

###Summary

Returns a json `String` that represents the current `Object`.
###Returns

A json `String` that represents the current `Object`.

---

<a name="95ec6289-97d0-4579-bcd1-96125a0ad84d"></a>
##Public Virtual ToString ()

###Return Type

`System.String`

###Member of Type

[Product](#98d67a2a-c250-4548-8a90-9e3722fef483)

###Summary

The public virtual `ToString ()` member of the `Product` class returns a `System.String` value.

---

<a name="f932e257-ceda-49a1-89b0-64119d61303e"></a>
##Public Virtual ToString ()

###Return Type

`System.String`

###Member of Type

[JsonArray](#fc61b8f7-b351-48f2-9773-888e050fffcb)

###Summary

The json representation of the array.
###Returns

The json representation of the array.

---

<a name="96f1b67d-25a2-411c-9117-b5e2018d05cf"></a>
##Public Static TryDeserializeObject (System.String, System.Object@)

###Return Type

`System.Boolean`

###Member of Type

[SimpleJson](#72ba8be5-342e-4430-bfb0-d5bc7c3b3a3f)

###Summary

Try parsing the json string into a value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>json</td><td style='width:75%' ><p>A JSON string.</p>
</td></tr><tr><td style='width:25%' class='term'>Out Ref obj</td><td style='width:75%' class='def'><p>The object.</p>
</td></tr></table></p>

###Returns

Returns true if successfull otherwise false.

---

<a name="a09e2734-5e52-4141-b862-5c2c6f8dd24d"></a>
##Public Virtual TryGetValue (System.String, System.Object@)

###Return Type

`System.Boolean`

###Member of Type

[JsonObject](#4db2613b-bdf6-4cff-a6da-804b56455cbe)

###Summary

Tries the get value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>key</td><td style='width:75%' ><p>The key.</p>
</td></tr><tr><td style='width:25%' class='term'>Out Ref value</td><td style='width:75%' class='def'><p>The value.</p>
</td></tr></table></p>


---

<a name="9674997b-0b65-440e-97d2-7909ee6200c2"></a>
##Virtual TrySerializeKnownTypes (System.Object, System.Object&)

###Return Type

`System.Boolean`

###Member of Type

[PocoJsonSerializerStrategy](#1ac2d0dc-01c8-489d-9039-4374c33d0124)

###Summary

The  virtual `TrySerializeKnownTypes (System.Object, System.Object&)` member of the `PocoJsonSerializerStrategy` class returns a `System.Boolean` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>input</td><td style='width:75%' ><p>The <code>input</code> parameter of the TrySerializeKnownTypes method takes a <code>System.Object</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>Out Ref output</td><td style='width:75%' class='def'><p>The <code>output</code> parameter of the TrySerializeKnownTypes method takes a <code>System.Object&amp;</code> value. Since <code>output</code> returns any modifications to the caller, it <em>must</em> be passed with the <code>out</code> modifier.Since <code>output</code> returns any modifications to the caller, it <em>must</em> be passed with the <code>ref</code> modifier.</p>
</td></tr></table></p>


---

<a name="5dc6ead3-9405-482f-924d-e62c1c6e3176"></a>
##Public Virtual TrySerializeNonPrimitiveObject (System.Object, System.Object&)

###Return Type

`System.Boolean`

###Member of Type

[PocoJsonSerializerStrategy](#1ac2d0dc-01c8-489d-9039-4374c33d0124)

###Summary

The public virtual `TrySerializeNonPrimitiveObject (System.Object, System.Object&)` member of the `PocoJsonSerializerStrategy` class returns a `System.Boolean` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>input</td><td style='width:75%' ><p>The <code>input</code> parameter of the TrySerializeNonPrimitiveObject method takes a <code>System.Object</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>Out Ref output</td><td style='width:75%' class='def'><p>The <code>output</code> parameter of the TrySerializeNonPrimitiveObject method takes a <code>System.Object&amp;</code> value. Since <code>output</code> returns any modifications to the caller, it <em>must</em> be passed with the <code>out</code> modifier.Since <code>output</code> returns any modifications to the caller, it <em>must</em> be passed with the <code>ref</code> modifier.</p>
</td></tr></table></p>


---

<a name="4e1c841a-79ef-4307-947c-4543c9b8b925"></a>
##Public Virtual TrySerializeNonPrimitiveObject (System.Object, System.Object&)

###Return Type

`System.Boolean`

###Member of Type

[IJsonSerializerStrategy](#88311aab-37d5-4381-95ce-c4aa0303ac7f)

###Summary

The public virtual `TrySerializeNonPrimitiveObject (System.Object, System.Object&)` member of the `IJsonSerializerStrategy` interface returns a `System.Boolean` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>input</td><td style='width:75%' ><p>The <code>input</code> parameter of the TrySerializeNonPrimitiveObject method takes a <code>System.Object</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>Out Ref output</td><td style='width:75%' class='def'><p>The <code>output</code> parameter of the TrySerializeNonPrimitiveObject method takes a <code>System.Object&amp;</code> value. Since <code>output</code> returns any modifications to the caller, it <em>must</em> be passed with the <code>out</code> modifier.Since <code>output</code> returns any modifications to the caller, it <em>must</em> be passed with the <code>ref</code> modifier.</p>
</td></tr></table></p>


---

<a name="9e7f4979-8815-47b2-85fb-edf62be0f0ea"></a>
##Virtual TrySerializeUnknownTypes (System.Object, System.Object&)

###Return Type

`System.Boolean`

###Member of Type

[PocoJsonSerializerStrategy](#1ac2d0dc-01c8-489d-9039-4374c33d0124)

###Summary

The  virtual `TrySerializeUnknownTypes (System.Object, System.Object&)` member of the `PocoJsonSerializerStrategy` class returns a `System.Boolean` value.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>input</td><td style='width:75%' ><p>The <code>input</code> parameter of the TrySerializeUnknownTypes method takes a <code>System.Object</code> value. </p>
</td></tr><tr><td style='width:25%' class='term'>Out Ref output</td><td style='width:75%' class='def'><p>The <code>output</code> parameter of the TrySerializeUnknownTypes method takes a <code>System.Object&amp;</code> value. Since <code>output</code> returns any modifications to the caller, it <em>must</em> be passed with the <code>out</code> modifier.Since <code>output</code> returns any modifications to the caller, it <em>must</em> be passed with the <code>ref</code> modifier.</p>
</td></tr></table></p>


---

<a name="fb7f0e87-0daf-457b-bbb6-e78804ffa522"></a>
##Public Static Unify (System.String[], System.Int32[])

###Return Type

`System.String`

###Member of Type

[Security](#9e6b0e98-9aae-449f-a739-3b32774c3e76)

###Summary

Recombines the given elements and segments to reconstruct an obfuscated string.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>element</td><td style='width:75%' ><p>List of elements used to reconstruct the string.</p>
</td></tr><tr><td style='width:25%' class='term'>segment</td><td style='width:75%' class='def'><p>List of segments speciying the order of the elements.</p>
</td></tr></table></p>

###Remarks

Given a list of elements `"A","B","C","D"` and segments `1,0,3,2` this function returns "BADC". This function is used to hide a string inside of a Xamarin.Android app.

---

<a name="5d3b6ddd-b9a0-4543-a132-24b781bd3f3c"></a>
##Public Static Unify (System.String[], System.Int32[], System.String[])

###Return Type

`System.String`

###Member of Type

[Security](#9e6b0e98-9aae-449f-a739-3b32774c3e76)

###Summary

Recombines the given elements, segments and hashes to reconstruct an obfuscated string.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>element</td><td style='width:75%' ><p>List of elements used to reconstruct the string.</p>
</td></tr><tr><td style='width:25%' class='term'>segment</td><td style='width:75%' class='def'><p>List of segments speciying the order of the elements.</p>
</td></tr><tr><td style='width:25%' class='term'>hash</td><td style='width:75%' ><p>Given a list of elements <code>"A","B","C123","D"</code>, segments <code>1,0,3,2</code> and hashes <code>"123","007"</code> this function returns "BADC007". This function is used to hide a string inside of a Xamarin.Android app.</p>
</td></tr></table></p>


---

<a name="b8a7d82a-e1ce-4c40-8065-cab637914695"></a>
##Private Static ValidOwnedItems (Android.OS.Bundle)

###Return Type

`System.Boolean`

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

Verifies that the given purchased bundle valid and contains an item list, data list and a signature list.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>purchased</td><td style='width:75%' ><p>Purchased.</p>
</td></tr></table></p>

###Returns

`true`, if owned items was valided, `false` otherwise.

---

<a name="94b5a545-929e-4de8-a050-7148e03926f4"></a>
##Public Static Verify (Java.Security.IPublicKey, System.String, System.String)

###Return Type

`System.Boolean`

###Member of Type

[Security](#9e6b0e98-9aae-449f-a739-3b32774c3e76)

###Summary

Verify the specified publicKey, signedData and signature.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>publicKey</td><td style='width:75%' ><p>Public key.</p>
</td></tr><tr><td style='width:25%' class='term'>signedData</td><td style='width:75%' class='def'><p>Signed data.</p>
</td></tr><tr><td style='width:25%' class='term'>signature</td><td style='width:75%' ><p>Signature.</p>
</td></tr></table></p>


---

<a name="922a9b7c-be06-46dd-966a-4ff5756fba9c"></a>
##Public Static VerifyPurchase (System.String, System.String, System.String)

###Return Type

`System.Boolean`

###Member of Type

[Security](#9e6b0e98-9aae-449f-a739-3b32774c3e76)

###Summary

Verifies the purchase.
<p><table style='width:100%'><tr><th style='width:25%'>Parameter</th><th style='width:75%'>Summary</th></tr><tr><td style='width:25%' class='term'>publicKey</td><td style='width:75%' ><p>Public key.</p>
</td></tr><tr><td style='width:25%' class='term'>signedData</td><td style='width:75%' class='def'><p>Signed data.</p>
</td></tr><tr><td style='width:25%' class='term'>signature</td><td style='width:75%' ><p>Signature.</p>
</td></tr></table></p>

###Returns

`true`, if purchase was verified, `false` otherwise.
<a name="Events"></a>
#Events


---

<a name="2c0fa63e-e57d-4407-ac3e-b4ecc9a5e573"></a>
##BuyProductError

###Return Type

[Xamarin.InAppBilling.InAppBillingHandler.BuyProductErrorDelegate](#f9e8ef42-cdf1-4484-81d8-5a7e438e4294)

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

Occurs when the user attempts to buy a product and there is an error.

---

<a name="78b79514-5e0f-4513-89b7-5d544d130332"></a>
##InAppBillingProcesingError

###Return Type

[Xamarin.InAppBilling.InAppBillingHandler.InAppBillingProcessingErrorDelegate](#7b5d0609-2f0c-4def-b7a4-27677dc3798b)

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

Occurs when there is an in app billing procesing error.

---

<a name="2da4661b-d73d-4574-aece-598b3240f31e"></a>
##OnConnected

###Return Type

[Xamarin.InAppBilling.InAppBillingServiceConnection.OnConnectedDelegate](#f81adae0-3a0b-42a2-a3cf-1136a9a6c0c4)

###Member of Type

[InAppBillingServiceConnection](#80c1612e-cf5e-41b3-a216-ca89246674e9)

###Summary

Occurs when on connected.

---

<a name="b98a04dd-3ae7-4845-938e-d9d18a115b42"></a>
##OnDisconnected

###Return Type

[Xamarin.InAppBilling.InAppBillingServiceConnection.OnDisconnectedDelegate](#6187252d-c6be-4eb8-8bac-62d7efeeca97)

###Member of Type

[InAppBillingServiceConnection](#80c1612e-cf5e-41b3-a216-ca89246674e9)

###Summary

Occurs when on disconnected.

---

<a name="3f46a95d-ef7d-4682-8e77-ed0995311217"></a>
##OnGetProductsError

###Return Type

[Xamarin.InAppBilling.InAppBillingHandler.OnGetProductsErrorDelegate](#d6aede4f-36f7-4c26-8e11-e25f23ee3c95)

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

Raised where there is an error getting previously purchased products from the Google Play Services.

---

<a name="ec062eeb-b6b9-41c9-a203-05e187969bec"></a>
##OnInAppBillingError

###Return Type

[Xamarin.InAppBilling.InAppBillingServiceConnection.OnInAppBillingErrorDelegate](#b2fa7c05-cb97-400a-a254-a2f0e67a90f2)

###Member of Type

[InAppBillingServiceConnection](#80c1612e-cf5e-41b3-a216-ca89246674e9)

###Summary

Occurs when on in app billing error.

---

<a name="6a10c879-72b8-428a-94cd-550523dee32c"></a>
##OnInvalidOwnedItemsBundleReturned

###Return Type

[Xamarin.InAppBilling.InAppBillingHandler.OnInvalidOwnedItemsBundleReturnedDelegate](#8758fba9-3d12-446b-ab23-b71e32b92354)

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

Raised when Google Play Services returns an invalid bundle from previously purchased items

---

<a name="13ff87b5-627a-4e6e-98f6-c89702c41304"></a>
##OnProductPurchased

###Return Type

[Xamarin.InAppBilling.InAppBillingHandler.OnProductPurchasedDelegate](#2ed54e69-6056-4702-86c8-ae945939f92b)

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

Occurs after a product has been successfully purchased Google Play.
###Remarks

This event is fired after a `OnProductPurchased` which is raised when the user successfully  logs an intent to purchase with Google Play.

---

<a name="b9079b27-e928-4217-8003-f88521686b85"></a>
##OnProductPurchasedError

###Return Type

[Xamarin.InAppBilling.InAppBillingHandler.OnProductPurchaseErrorDelegate](#09a88fb4-2a98-4e4b-a2c6-aa7749c450cc)

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

Occurs when the is an error on a product purchase attempt.

---

<a name="6cede3c7-d79d-430e-b730-42ea79ee5768"></a>
##OnPurchaseConsumed

###Return Type

[Xamarin.InAppBilling.InAppBillingHandler.OnPurchaseConsumedDelegate](#fd7ef9f9-bb27-4eff-a319-a3193670f137)

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

Occurs when on product consumed.

---

<a name="cc0abc89-53b4-45d7-9e5a-c9c6a2b2b338"></a>
##OnPurchaseConsumedError

###Return Type

[Xamarin.InAppBilling.InAppBillingHandler.OnPurchaseConsumedErrorDelegate](#db1cca38-9f7c-4d1a-97c3-e9da2b715763)

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

Occurs when there is an error consuming a product.

---

<a name="38cafdb8-1577-4438-812d-9bec5bcb474b"></a>
##OnPurchaseFailedValidation

###Return Type

[Xamarin.InAppBilling.InAppBillingHandler.OnPurchaseFailedValidationDelegate](#e34de5bc-5059-4b1c-820c-c3a2363b472f)

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

Occurs when a previously purchased product fails to validate.

---

<a name="b0f95064-5eb4-444c-96b4-505e43f1eac0"></a>
##OnUserCanceled

###Return Type

[Xamarin.InAppBilling.InAppBillingHandler.OnUserCanceledDelegate](#8e757b02-94e2-431f-bfe3-56483c436f0e)

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

Occurs when on user canceled.

---

<a name="da55a78e-3d01-45f0-98bb-f5e17f00b3cd"></a>
##QueryInventoryError

###Return Type

[Xamarin.InAppBilling.InAppBillingHandler.QueryInventoryErrorDelegate](#2debbb37-b92f-4714-92c2-47510bad06c2)

###Member of Type

[InAppBillingHandler](#789accaa-919d-4249-924f-e54076856b08)

###Summary

Occurs when there is an error querying inventory from Google Play Services.

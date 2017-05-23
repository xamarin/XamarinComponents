###About Xamarin.InAppBilling###

In-app Billing is a Google Play service that lets you sell a wide range digital content such as new game levels, downloadable media, or premium features directly from inside your Xamarin.Android mobile application. These purchases can either be standard, one-time billed products or recurring, automatically billed subscriptions.

The `Xamarin.InAppBilling` component simplifies the process of supporting In-App Billing (Version 3 API) by reducing the amount of common, repetitive code required and by adding several useful, helper functions.

###Features###

`Xamarin.InAppBilling` supports the following features:

* **Connect** - Attaches your Xamarin.Android app to Google Play to process In-App Billing transactions.
* **QueryInventoryAsync** - Given a list of Product IDs, this routine will return all available, active products matching those IDs not already purchased by the current user.
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
* **OnProductPurchased** - Raised when a product or subscription is fully processed by Google Play and returned.
* **OnPurchaseConsumedError** - Raised when there is an error consuming a purchase.
* **OnPurchaseConsumed** - Raised when a purchase is successfully consumed.
* **OnPurchaseFailedValidation** - Raised when a previous purchase fails validation.
* **OnGetProductsError** - Raised when a request to *GetProducts* fails.
* **OnInvalidOwnedItemsBundleReturned** - Raised when an invalid bundle of purchases is returned from Google Play.
* **InAppBillingProcessingError** - Raised when any other type of processing issue not covered by an existing event occurs.
* **OnUserCanceled** - Raised when a user cancels an In App Billing request.
* **QueryInventoryError** - Raised if there is an error querying Google Play Services for available inventory.
* **BuyProductError** - Raised if there is an error buying a product from Google Play Services.

###Secure Transactions###

When developing Xamarin.Android applications that support In-App Billing there are several steps that should be taken to protect your app from being hacked by a malicious user and keep unlocked content safe.

While the best practice is to perform signature verification on a remote server and not on a device, this might not always be possible. Another technique is to obfuscate your Google Play public key and never store the assembled key in memory. 

`Xamarin.InAppBilling` provides a quick and simple method to break your public key into a several pieces and to obfuscate those pieces. Once a public key has been provided to `Xamarin.InAppBilling` it is never stored as plain text, it is always encrypted in memory.

###Required Setup and Limitations###

While using the `Xamarin.InAppBilling` component greatly simplifies the amount of code required to use In-App Billing in your Xamarin.Android mobile application, the component still uses the Google Play In-App Billing (Version 3 API) to perform In-App Billing tasks and as such, has the same limitations and setup requirements as using the In-App Billing (Version 3 API) directly in your app.

To properly use the `Xamarin.InAppBilling` component, please follow all of the setup and testing instructions in the **Getting Started** document included with the component.

###Android Examples###

And here is a simplified example of adding In-App Billing to an Android **Activity** using `Xamarin.InAppBilling`:

```
using Xamarin.InAppBilling;
using Xamarin.InAppBilling.Utilities;
...

// Create a new connection to the Google Play Service
_serviceConnection = new InAppBillingServiceConnection (this, "<public-key>");
_serviceConnection.OnConnected += () => {
				
	// Load inventory or available products
	GetInventory();

	// Load any items already purchased
	LoadPurchasedItems();
};

// Attempt to connect to the service
_serviceConnection.Connect ();
...

private async Task GetInventory () {
	// Ask the open connection's billing handler to return a list of available products for the 
	// given list of items.
	// NOTE: We are asking for the Reserved Test Product IDs that allow you to test In-App
	// Billing without actually making a purchase.
	_products = await _serviceConnection.BillingHandler.QueryInventoryAsync (new List<string> 	{
		ReservedTestProductIDs.Purchased,
		ReservedTestProductIDs.Canceled,
		ReservedTestProductIDs.Refunded,
		ReservedTestProductIDs.Unavailable
	}, ItemType.Product);

	// Were any products returned?
	if (_products == null) {
		// No, abort
		return;
	}
	...
}
...

private void LoadPurchasedItems () {
	// Ask the open connection's billing handler to get any purchases
	var purchases = _serviceConnection.BillingHandler.GetPurchases (ItemType.Product);

	// Display any existing purchases
	...
}
...

// Configure buy button
_buyButton.Click += (sender, e) => {
	// Ask the open connection's billing handler to purchase the selected product
	_serviceConnection.BillingHandler.BuyProduct(_selectedProduct);
};

```

Please see the **Getting Started** documentation for full instructions on configuring, implementing and testing the `Xamarin.InAppBilling` component in a Xamarin.Android mobile application. The **Getting Started** documentation also includes a _Troubleshooting_ section to help diagnose and fix common issues may occur when using the component.

_Some screenshots created with [PlaceIt](http://placeit.breezi.com "PlaceIt by Breezi")._

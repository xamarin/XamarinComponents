##Xamarin.InAppBilling Component##

###Getting Started with Xamarin.InAppBilling###

To use an `Xamarin.InAppBilling` in your mobile application include the component in your project and reference the following using statements in your C# code:

```
using Xamarin.InAppBilling;
using Xamarin.InAppBilling.Utilities;
``` 

###Installing the Google Play Billing Library###

Before you can use the `Xamarin.InAppBilling` component in your Xamarin.Android mobile application, you need to ensure that the latest version of the **Google Play Billing Library** is installed on your development computer.

To install the library (or update to the latest version), do the following:

1. Start Xamarin Studio.
2. From the **Tools** menu select **Open Android SDK Manager...**
3. Open the **Extras** folder and place a check by the **Google Play Billing Library** entry.
4. Click the **Install packages...** button.
5. Approve any required licenses.
6. Download and install the required library (and any other updates that the SDK manager finds). 


###Preparing Your App for In-App Billing###

Before the `Xamarin.InAppBilling` component can be successfully added to a Xamarin.Android mobile application and used for Google Play In-App Billing, there are several initial setup steps that need to be performed. Using the **Google Play Developer Console**, you will establish an application (that will later be available for sale or free download from the Google Play Store) and manage the various digital goods that will be available for purchase from within you application.

When you create a new application in the **Developer Console**, it will automatically generate a public license key for your app, that you will later use to sign the application and provide to the `Xamarin.InAppBilling` to verify In-App Billing transactions. See the _A Special Note on Security_ and _Connecting to Google Play_ sections below for more information.

If you have not already done so, you'll need to register as a developer with the [Google Play](http://developer.android.com). Additionally, to sell In App Products, you'll also need to have a [Google Wallet](http://www.google.com/wallet/merchants.html) Merchant Account.

To add your application to the Developer Console, please do the following:

1. In a web browser navigate to the [Google Play Developer Console](http://play.google.com/apps/publish) and log in.
2. Click on the **All Applications** tab on the left hand side of the screen.
3. Click on the **+ Add New Application** button.
4. Enter a **Title** for the application that you intent to sell In-App Products from.
5. Click the **Prepare Store Listing** button. You'll be taken to the **Application Details** Page.
6. Fill out the rest of the information on this page and click the **Save** button at the top of the screen.
7. Click on the **Services & APIs** tab for your application.
8. Copy the **YOUR LICENSE KEY FOR THIS APPLICATION** key. This is your **Public Key** and you'll need it to sign your application later and to validate any In-App Billing transactions.
9. Return to Xamarin Studio and use the **Public Key** from above to establish a connection from the `Xamarin.InAppBilling` component and the Google Play Store. See the _Connecting to Google Play_ section below for more information.

_**NOTE:** Special care should be taken when copying and pasting the Public Key from the **Google Play Developer Console** to ensure that no hidden, "white space" characters are included. The key used in your application must match the one from the **Developer Console** exactly or any In-App Billing transactions will fail._

###Setting the Application's Billing Permission###

Before your application can communicate with Google Play’s billing service, it needs to have it's **Billing Permission** set.

To add the required permission to your Xamarin.Android mobile application, please do the following:

1. Open the application in Xamarin Studio or Visual Studio.
2. In the **Solution Explorer** expand the **Properties** folder.
3. Double-click the `AndroidManifest.xml` file to open it for editing.
4. Click the **Source** button at the bottom of the screen.
5. Add the `<uses-permission android:name="com.android.vending.BILLING" />` line between the `<manifest>...</manifest>` tags.
6. Save the file.

_**NOTE:** If you return to the **Application** view of the `AndroidManifest.xml` file, the **BILLING** line will be displayed in red. This is normal and not an indication of an error._

###A Special Note on Security###

When developing Xamarin.Android applications that support In-App Billing there are several steps that should be taken to protect your app from being hacked by a malicious user and keep unlocked content safe.

While the best practice is to perform signature verification on a remote server and not on a device, this might not always be possible. Another technique is to obfuscate your Google Play public key and never store the assembled key in memory.

`Xamarin.InAppBilling` provides the **Unify** routine that can be used to break your Google Play public key into two or more pieces and to obfuscate those pieces using one or more key/value pairs. In addition, `Xamarin.InAppBilling` always encrypts your private key while it's in memory.

Here is an example of using **Unify** to obfuscate a private key:

```
string value = Security.Unify (
	new string[] { "X0X0-1c...", "+123+Jq...", "//w/2jANB...", "...Kl+/ID43" }, 
	new int[] { 2, 3, 1, 0 }, 
	new string[] {  "X0X0-1", "9V4XD", "+123+", "R9eGv", "//w/2", "MIIBI", "+/ID43", "9alu4" });
```
Where the first parameter is an array of strings containing your private key broken into two or more parts in a random order. The second parameter is an array of integers listing of order that the private key parts should be assembled in. The third, optional, parameter is a list of key/value pairs that will be used to replace sequences in the assembled key.

_**Note:** There are several more steps that should be taken to secure your application, please see Google's official [Security and Design](http://developer.android.com/google/play/billing/billing_best_practices.html) document for further details._

###Connecting to Google Play###

When your application's main activity first starts, you'll need to initially establish a connection to Google Play services to support In-App Billing.

The following is an example of using `Xamarin.InAppBilling` to connect to Google Play:

```
private InAppBillingServiceConnection _serviceConnection;
...

// Create a new connection to the Google Play Service
_serviceConnection = new InAppBillingServiceConnection (this, publicKey);
_serviceConnection.OnConnected += () => {
	// Load available products and any purchases
	...
};

// Attempt to connect to the service
_serviceConnection.Connect ();
```

The _OnConnected_ event will be raised once a successful connection to Google Play Services has been established. In the event that the `Xamarin.InAppBilling` component is unable to connect to the Google Play Services API, the _OnInAppBillingError_ event will be raised on the `InAppBillingServiceConnection` object.

The connection will fail if the Android device does not have the latest version of the Google Play Store loaded or if the current device user is unable to make purchases from the store.

_**Note:** Remember to unbind from the In-app Billing service when you are done with your Activity. If you don’t unbind, the open service connection could cause an Android device’s performance to degrade and provide a bad user experience for the end user of your application._

###Establishing In-App Products for Sale###

Before you can test or use In-App Billing in your Xamarin.Android mobile application, you'll need to create the virtual products for your app in the **Google Play Developer Console** and initially upload a testing APK. For more information on testing, please see the _Testing In-App Billing in a Xamarin.Android App_ section below.

To add new In-App Products to your application, do the following:

1. Build a signed APK for your application. See Xamarin's [Publishing an Application](http://developer.xamarin.com/guides/android/deployment,_testing,_and_metrics/publishing_an_application/) documentation for the details of creating the required signed APK file. Ensure that you are using your final (not debug) certificate and private key (acquired above) to sign the application's APK.
2. Return to the **Google Play Developer Console** and open the application entry created above.
3. Click on the application's **APK** tab and select the **ALPHA TESTING** tab.
4. Click on the **Upload your first APK to Alpha** button.
5. Browse to the application's APK you just created and upload the file to the **Developer Console**. Don't publish your application yet, this step is required to setup and test In-App Products only. You'll publish the application once it has been fully tested and debugged and is ready for release to the general public.
6. Click on the application's **In-app Products** tab.
7. Click on the **+ Add new product** button at the top of the screen.
8. Select **Managed product** or **Subscription** and enter a **Product ID** (also referred to as a SKU) for the virtual product that you are creating. A **Product ID** can only be composed of lower-case letters(a-z), numbers(0-9), underline(_) and dot(.). It should also start with lower-case letters or numbers. NOTE: Google In-App Billing Version 3 only supports **Managed Products** or **Subscriptions**.
9. You'll be taken the the **Product Details** screen.
10. Fill in the rest of the information for your virtual product and click the **Save** button at the top of the screen.
11. From the **Inactive** dropdown list at the top of the screen, select **Activate**. 
12. Repeat the step above to create the rest of the virtual products that your application will require.

---
_**WARNING!** It may take up to 2-3 hours after uploading the APK for Google Play to recognize your updated APK version. If you try to test your application before your uploaded APK is recognized by Google Play, your application will receive a ‘purchase cancelled’ response with an error message “This version of the application is not enabled for In-app Billing.”_

---

###Reserved Product IDs###

In addition to the virtual Product IDs that you can create for yourself, Google provides a set of **Reserved Product IDs** that can be used to test your application In-App Billing capabilities.

The following **Reserved Product IDs** are available:

* **android.test.purchased** - When you make an In-App Billing request with this Product ID, Google Play responds as though you successfully purchased an item. The response includes a JSON string, which contains fake purchase information (for example, a fake order ID). In some cases, the JSON string is signed and the response includes the signature so you can test your signature verification implementation using these responses.
* **android.test.canceled** - When you make an In-App Billing request with this Product ID Google Play responds as though the purchase was canceled. Cancellation can occur when an error is encountered in the order process, such as an invalid credit card, or when you, the developer, cancel a user's order before it is charged.
* **android.test.refunded** - When you make an In-App Billing request with this Product ID, Google Play responds as though the purchase was refunded. Refunds cannot be initiated through Google Play's In-App Billing service API. Refunds must be initiated by you (the merchant/developer) via your Google Wallet account. After you process a refund request through your Google Wallet merchant account, a refund message is sent to your application by Google Play. This occurs only when Google Play gets notification from Google Wallet that a refund has been made.
* **android.test.item_unavailable** - When you make an In-app Billing request with this Product ID, Google Play responds as though the item being purchased was not listed in your application's product list.

When using the **Reserved Product IDs**, Google Play will act as if real transactions are occurring, however no real purchases are talking place and no actual financial transactions are being processed. `Xamarin,InAppBilling` provides a `ReservedTestProductIDs` class to make working with **Reserved Product IDs** easier in C# code.

###Requesting Available Products###

Once you have an open connection you can request a list of available products that the user can purchase by providing a list of product ID (either the ones you created above or a **Reserved Product ID**). Here is an example:

```
private IList<Product> _products;
...

_products = await _serviceConnection.BillingHandler.QueryInventoryAsync (new List<string> {
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
```
In the above example we are requesting the four predefined **Reserved Product ID** provided by Google (using the `ReservedTestProductIDs` helper class) to test an application without actually making a purchase.

If there is an error requesting available product, the _BillingHandler's_ `QueryInventoryError` event will be raised with the **Response Code** of the error. The **Response Code** can be compared against the `BillingResult` static class to determine the reason for the failure.

###Purchasing a Product###

Purchasing a product can be accomplished using the following:

```
// Ask the open connection's billing handler to purchase the selected product
_serviceConnection.BillingHandler.BuyProduct(_selectedProduct);
```
Where **_selectedProduct** is one of the products returned by the **QueryInventoryAsync** routine above. You *must* make the call to `BuyProduct` from the main thread of your Activity or it will fail.

If there is an error purchasing a product, the _BillingHandler's_ `BuyProductError` event will be raised with the **Response Code** of the error. The **Response Code** can be compared against the `BillingResult` static class to determine the reason for the failure.

###Handling the Purchase Result###

To complete the purchase cycle, you will need to override the **OnActivityResult** method of the Activity of the app that initiated the purchase and pass the result to the _HandleActivityResult_ method of the _BillingHandler_ of your open _InAppBillingServiceConnection_:

```
protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
{
	// Ask the open service connection's billing handler to process this request
	_serviceConnection.BillingHandler.HandleActivityResult (requestCode, resultCode, data);

	// TODO: Use a call back to update the purchased items
	// or listen to the OnProductPurchased event to
	// handle a successful purchase
}
```

With the above code in place, you can listen to the `OnProductPurchased` event of the _BillingHandler_ to handle the successful purchase of the product or simply request the full list of purchased products again from the _BillingHandler_.

Any issues processing a purchase will raise either the `OnProductPurchasedError`, `OnPurchaseFailedValidation` or `InAppBillingProcessingError` events with details of the issue that stopped processing. If the user cancels the purchasing of the product, the _BillingHandler's_ `OnUserCanceled` event will be raised.

---

_**PLEASE NOTE:** The `OnProductPurchased` event will not be raised unless you have overridden the `OnActivityResult` method of the Activity that initiated the purchase and passed the result to the `HandleActivityResult` for processing!_

---

###Requesting a List of Previous Purchases###

After the user has purchased one or more products, the following code will request a list of those purchases:

```
// Ask the open connection's billing handler to get any purchases
var purchases = _serviceConnection.BillingHandler.GetPurchases (ItemType.Product);
```

If there is an error getting the list of products, the _BillingHandler's_ `OnGetProductsError` event will be raised with the **Response Code** of the error. The **Response Code** can be compared against the `BillingResult` static class to determine the reason for the failure.

###Consuming a Purchase###

For products that are consumable in your application, such as coins or tokens, use the following code to inform Google Play that a given product purchase has been consumed:

```
// Attempt to consume the given product
bool result = _serviceConnection.BillingHandler.ConsumePurchase (purchasedItem);

// Was the product consumed?
if (result) {
	// Yes, update interface
	...
}
```
Where **purchasedItem** is a purchase returned by the **GetPurchases** routine above. The _OnPurchaseConsumed_ event will be raised if the product is successful purchased else the _OnPurchaseConsumedError_ event will be raised.

###Disconnecting from Google Play###

When you are done with your Activity you must remember to unbind from the In-app Billing service. If you don’t unbind, the open service connection could cause an Android device’s performance to degrade and give the user a bad impression of your application.

To do this, override you Activity's **OnDestroy** method and call the **Disconnect** routine:

```
protected override void OnDestroy () {
			
	// Are we attached to the Google Play Service?
	if (_serviceConnection != null) {
		// Yes, disconnect
		_serviceConnection.Disconnect ();
	}

	// Call base method
	base.OnDestroy ();
}
```

_**Note:** Remember to unbind from the In-app Billing service when you are done with your Activity. If you don’t unbind, the open service connection could cause an Android device’s performance to degrade and provide a bad user experience for the end user of your application._

###Available Events###

`Xamarin.InAppBilling` defines the following events that you can monitor and respond to:

* **OnConnected** - Raised when the component attaches to Google Play.
* **OnDisconnected** - Raised when the component detaches from Google Play.
* **OnInAppBillingError** - Raised when an error occurs inside the component.
* **OnProductPurchasedError** - Raised when there is an error purchasing a product or subscription.
* **OnProductPurchased** - Raised when a product or subscription is fully processed by Google Play and returned. 
* **OnPurchaseConsumedError** - Raised when there is an error consuming a purchase.
* **OnPurchaseConsumed** - Raised when a purchase is successfully consumed.
* **OnPurchaseFailedValidation** - Raised when a previous purchase fails validation. This is ususally caused when the application's Public Key does not match the Public Key in the Google Developer Console 100%.
* **OnGetProductsError** - Raised when a request to *GetProducts* fails.
* **OnInvalidOwnedItemsBundleReturned** - Raised when an invalid bundle of purchases is returned from Google Play.
* **InAppBillingProcessingError** - Raised when any other type of processing issue not covered by an existing event occurs.
* **OnUserCanceled** - Raised when a user cancels an In App Billing request.
* **QueryInventoryError** - Raised if there is an error querying Google Play Services for available inventory.
* **BuyProductError** - Raised if there is an error buying a product from Google Play Services.

##Testing In-App Billing in a Xamarin.Android App##

Because of the secure nature of using In-App Billing in a Xamarin.Android mobile application, a special set of procedures are required to properly test an In-App Billing equipped app. In addition, testing should be done early and often to ensure that the purchasing flow is easy to understand, that purchases are happening in a timely fashion and that the customer can easily see what In-App products or subscriptions they have purchased.

###Testing with Static Responses###

Initially you should test your application with the **Reserved Product IDs** provided by Google to make sure that your application properly handles the purchasing workflow and that it gracefully handles any error situation. See the _Reserved Product IDs_ section above for more information.

###Create Testing Accounts###

Before you can test products that you created in the **Google Play Developer Console**, you need to create one or more **Test Accounts** under your developer profile. You'll need to use one of these accounts when testing, since Google Play does not allow your Developer Account to directly purchase In-App Products.

To create the required testing accounts, do the following:

1. In a web browser navigate to the [Google Play Developer Console](http://play.google.com/apps/publish) and log in.
2. Click the **Settings** tab from the left hand side of the screen.
3. Click the **User accounts & rights** tab.
4. Click the **Invite new user** button at the top of the screen.
5. Enter the email address for the Test User Account that you are creating. _NOTE: This must be an active, valid Google email address only._
6. Select a role for the user from the **Role** dropdown list.
7. Place a check by any rights that want this user to have. In the case of a simple tester, leave all boxes unchecked.
8. Click the **Send Invitation** button.
9. The **Pending Invitations** screen will be displayed.
10. Repeat the steps above to add any other required Test Accounts.

The Test User Account will receive an email from Google Play that they will need to use to accept the invitation. Once the Test User Account has accepted the invitation, they will be displayed in the **Users With Access** screen of the **Google Play Developer Console** (under **Settings** > **User accounts & rights**).

If required, you can adjust the rights of any Test User Account from this screen or remove a Test Account from you Google Play Developer Account if it is no longer needed.

###Uploading Your Test APK###

With the Test Accounts in place and configured, users with these accounts will be able to make In-App Billing purchases from any applications that you manage once it has been published to the Alpha or Beta Channel in the [Google Play Developer Console](http://play.google.com/apps/publish).

A properly signed APK registered with the **Google Play Developer Console** is required to test an In-App Billing capable Xamarin.Android mobile application. Attempting to test the application in the Android Simulator or simply starting a debug build in Xamarin Studio and running on a real Android hardware device will fail.

To register your application's signed APK, do the following:

1. Open Xamarin Studio and load your Xamarin.Android mobile application's solution.
2. Build a signed APK for your application. See Xamarin's [Publishing an Application](http://developer.xamarin.com/guides/android/deployment,_testing,_and_metrics/publishing_an_application/) documentation for the details of creating the required signed APK file. Ensure that you are using your final (not debug) certificate and private key (acquired above) to sign the application's APK.
3. Return to the **Google Play Developer Console** and open the application entry created above.
4. Click on the application's **APK** tab and select the **ALPHA TESTING** tab.
5. Click on the **Upload new APK to Alpha** button.
6. Browse to the APK file created and signed above and upload the APK to the **Developer Console**.
7. Fill out all of the required fields to publish an Alpha or Beta build such as price, rating, icons, images, store description, etc.
8. You will need to **Publish** the APK to the Alpha or Beta channel so that it will be available to your testers in the Google Play Store. _Note: You're registered testers will be the only people able to see the APK in the Store._

Ensure that the new signed APK has been uploaded for your application to the **Developer Console**, and that one or more In-App Products have been associated with your app. After the APK has been published, you'll need to wait until it has been approved for inclusion in the Google Play Store.

---
_**WARNING!** It may take up to 2-3 hours after uploading the APK for Google Play to recognize your updated APK version. If your test users attempt to test In-App Billing **before** the APK has been approved and included in the Google Play Store, the In-App Billing API (used by the Xamarin.InAppBilling Component), will silently fail without warning or reporting invalid product codes on anything but the built in test codes._

---

###Installing the Test APK###

After allowing proper time for your new signed APK to be recognized by Google Play Services and to be reviewed and approved for release, your Test Users will need to install the same APK file onto their test devices. Only the Test Users that were created above and that have accepted your invitation to join your Google Play Developer Account will be able to test your In-App Billing products.

Google Play does not allow your Developer Account to directly purchase In-App Products, so any Android Device using your Developer Account will not be able to be used for testing, all In-App Billing transactions will fail.

To install the signed APK on the Test User's Android hardware device, follow the directions from the [Part 5 - Publishing Independently](http://developer.xamarin.com/guides/android/deployment,_testing,_and_metrics/publishing_an_application/part_5_-_publishing_independently/) section of Xamarin's [Publishing an Application](http://developer.xamarin.com/guides/android/deployment,_testing,_and_metrics/publishing_an_application/) documentation.

The following should be noted:

1. By default, Android prevents users from downloading and installing applications from locations other than Google Play. To allow installation from non-marketplace sources, a user must enable the **Unknown sources** setting on a device before attempting to install an application.
2. The test users **must** install the APK from the Google Play Store via the Google Play Store application on their Android device. Again, they will be the only people able to see your app in the store if it has not been previously released for sale in the store.

Please note, the Google Play Store no longer supports testing In-App Billing from APK files that have been side loaded onto an Android device.

If the Test Users have a previous version of the application currently installed on their device, they my need to uninstall that version of the application prior to installing the new version. To remove an existing Android app, go to **Settings** > **Apps** on the device. Navigate to the application and click the **Uninstall** button to remove the application.

---
_**WARNING!** The same signed APK file that was uploaded to the **Google Play Developer Console** must be the one installed on the Test User's Android hardware device, otherwise any In-App Billing transactions will fail!_

---

###Testing with Your Product IDs###

With the signed APK successfully uploaded and recognized by Google Play, and after the same signed APK has been installed on the Test User's Android hardware devices, the Test Users can finally test your In-App Billing products.

It should also be noted, that the Test User Account's Google email address (that was registered to your Google Play Developer Account above) must be the one used to log into the **Google Play Store** on the Android device and the Test User must be the primary users on the device or any In-App Billing transactions in your application will fail.

The Test Users should throughly test purchasing every In-App Product that your application supports. Test purchases made by your Test Users are real orders, and Google Play processes them in the same way as any other order. However, when these purchases are complete, Google Play prevents the orders from going to financial processing (ensuring that there are no actual charges to user accounts). Google Play will automatically cancel any completed test orders after 14 days.

For more information about testing In-App Billing, please see Google's official [Testing In-app Billing](http://developer.android.com/google/play/billing/billing_testing.html#test-purchases}) documentation.

If issues are found with your application's In-App Billing purchases or billing processes and a new, updated version is required for testing, you'll need to go through the steps outlined in the _Uploading Your Test APK_ and _Installing the Test APK_ sections again.

##Troubleshooting In-App Billing##

Given the complexity of correctly configuring, compiling, signing an APK, and setting up the **Google Play Developer Console** for any Xamarin.Android mobile application that will be using In-App Billing, issues can sometimes arise.

The following is a list of the most common things to check when having issues using In-App Billing:

* Ensure that you have the latest version of the `Xamarin.InAppBilling` component, the Android SDK, the Google Play Billing Library, Xamarin.Android and the **Google Play Store** app loaded on the device.
* Ensure that your application's **Public Key** matches the one in the **Developer Console** _exactly_ and that it contains no hidden "white space" characters.
* Before your application can communicate with Google Play’s billing service, it needs to have it's **Billing Permission** set.
* Ensure that your **Product IDs** match the ones created for your application in the **Developer Console** _exactly_ and that the products have been **Activated**.
* Ensure that your product types are either **Managed Product** or **Subscription**, as these are the only product types supported by Google Play In-App Billing Version 3.
* Ensure that you are using your final (not debug) certificate and private key (acquired above) to sign the application's APK before uploading it to the **Developer Console**.
* Ensure that you are using the **Release** configuration to compile a clean-build of your Xamarin.Android mobile application in Xamarin Studio or Visual Studio before creating the signed APK.
* Ensure that your latest signed APK file has been uploaded to the **Developer Console** and that 2-3 hours have gone by so that Google Play has had time to properly register the APK. On rare occasion, it can take up to 24 hours before a new version is recognized.
* Ensure that the APK file has been **Published** to the Alpha or Beta Channel and that it has been successfully **Reviewed and Approved** by Google.
* Ensure that the same signed APK file uploaded to the **Developer Console** is the same one installed on the Android hardware device and that it was installed from the Google Play Store app on the device. **NOTE: Side loading of the APK file is no longer supported from In-App Billing.**
* Testing of your **Product IDs** is only supported on real hardware and not the Android Simulator and running a Debug or Release build on an Android hardware device directly from Xamarin Studio will also fail.
* Ensure that your Test User Accounts have been created against live, active Google email addresses and that the Test Users have accepted your invitation to join your Google Play Developer Account as testers.
* Google Play does not allow your Developer Account to make In-App Billing purchases so a Test User Account must be used.
* Ensure that the Test User Account is the primary user of the Android Hardware device and that the Google email used to create the Test User Account is the same one used to log into the Google Play Store app on the device.
* The `OnProductPurchased` event will not be raised unless you have overridden the `OnActivityResult` method of the Activity that initiated the purchase and passed the result to the `HandleActivityResult` for processing.
* The `BuyProduct` method _must_ be called from the main thread of your main Activity or the purchase will fail.

##Examples##

For full examples of using `Xamarin.InAppBilling` in your mobile application, please see the  _Xamarin.InAppBilling_ example app included with this component.

See the API documentation for `Xamarin.InAppBilling` for a complete list of features and their usage.

## Other Resources

* [Xamarin Components](https://components.xamarin.com)
* [Implementing In-app Billing (IAB Version 3)](http://developer.android.com/google/play/billing/billing_integrate.html)
* [Security and Design](http://developer.android.com/google/play/billing/billing_best_practices.html)
* [Support](http://xamarin.com/support)
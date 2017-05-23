###Version 02.02.01###

Changing to a MIT license.  Source now available in the [Xamarin Components repo.](https://github.com/xamarin/XamarinComponents/tree/master/Android/Xamarin.InAppBilling)

###Version 02.02###

The following features and bug fixes have been added to `Xamarin.InAppBilling` in version 02.02:

* **Google Play In-App Billing API** - Upgraded to the latest version of the Google Play In-App Billing API.
* **ServiceUnavailable** - Added `ServiceUnavailable` to the `BillingResult` enum.
* **Price_Amount_Micros & Price_Currency_Code** - Added the new `Price_Amount_Micros` and `Price_Currency_Code` fields to the `Product` class.
* **Testing IAB** - Updated the _Testing In-App Billing in a Xamarin.Android App_ section _Getting Started_ documentation to include the new required step of **Publishing** an Alpha or Beta channel version of the app before In App products can be tested.


###Version 02.01###

The following features and bug fixes have been added to `Xamarin.InAppBilling` in version 02.01:

* **purchaseData** - Exposed purchase data (Response.InAppPurchaseData) to the `OnProductPurchased` and `OnPurchaseFailedValidation` events.
* **purchaseSignature** - Exposed purchase signature (Response.InAppDataSignature) to the `OnProductPurchased` and `OnPurchaseFailedValidation` events.

###Version 02.00###

The following features and bug fixes have been added to `Xamarin.InAppBilling` in version 02.00:

* **In-App Billing v3** - Switched from Google Play Services In-App Billing Version 2 to Version 3.
* **Improved Security** - Added protection to ensure that _only_ the official Google Play app can handle billing requests, thus preventing other 3rd party apps from intercepting those requests. 
* **OnProductPurchased** - Changed the `OnProductPurchased` event so that it is fired only after a product has successfully been purchased from Google Play Services.
* **OnProductPurchaseCompleted** - Removed the `OnProductPurchaseCompleted` event and replaced it with `OnProductPurchased` event as a more logical workflow.
* **OnUserCanceled** - Added the `OnUserCanceled` event that is raised when a user cancels an In App Billing request.
* **QueryInventoryError** - Added the `QueryInventoryError` event that is called if there is an error querying Google Play Services for available inventory.
* **BuyProductError** - Added the `BuyProductError` event that is called if there is an error buying a product from Google Play Services.
* **Billing** - Switched the `Billing` class to use constants so it would be easier to use in code without typecasting.
* **BillingResult** - Switched the `BillingResult` class to use constants so it would be easier to use in code without typecasting.
* **ItemType** - Switched the `ItemType` class to use constants so it would be easier to use in code without typecasting.
* **ReservedTestProductIDs** - Switched the `ReservedTestProductIDs` class to use constants so it would be easier to use in code without typecasting.
* **Response** - Switched the `Response` class to use constants so it would be easier to use in code without typecasting.
* **Null Error** - Fixed an issue that could result in a `null` crashing the component on connect if it wasn't able to connect to the Google Play Services In-App Billing API.

###Version 01.05###

The following features and bug fixes have been added to `Xamarin.InAppBilling` in version 01.05:

* **GetPurchases** - Fixes the situation that could lead to a Null Exception when a call _GetPurchases_ fails to communicate with Google Play Services. _GetPurchases_ will now fail gracefully and return a _null_.

###Version 01.04###

The following features and bug fixes have been added to `Xamarin.InAppBilling` in version 01.04:

* **GetPurchases** - Added code to gracefully handle receiving a malformed Owned Items bundle from Google Play Service. GetPurchases will now return a _null_ list and raise the _InAppBillingProcessingError_ event.

###Version 01.03###

The following features and bug fixes have been added to `Xamarin.InAppBilling` in version 01.03:

* **Removed Newtonsoft.Json** - Switched to a light-weight embedded Json parser and removed dependency on Newtonsoft.Json parser to solve conflicts with other components and projects.
* **HandleActivityResult** - Added missing documentation for _HandleActivityResult_ required to fire the _OnProductPurchaseCompleted_ event.
* **InAppBillingProcessingError** - Added the _InAppBillingProcessingError_ event that will be raised for any processing error not covered by another error event.

###Version 01.02###

The following features and bug fixes have been added to `Xamarin.InAppBilling` in version 01.02:

* **GetPurchases Continuation** - Fixes an issue with continuation of a large number of products or subscriptions (greater than 70).
* **Reserved Test Products** - Fixed an issue where the reserved test product ID's would not validate using the latest Google Play API.
* **OnGetProductsError** - Added the *OnGetProductsError* event to trap when a request to GetProducts fails.
* **OnInvalidOwnedItemsBundleReturned** - Added the *OnInvalidOwnedItemsBundleReturned* event to trap when an invalid bundle of purchases is returned from Google Play.
* **OnProductPurchased Changed** - The *OnProductPurchased* event is raised when an intent to purchase a product or subscription has been successfully sent to Google Play.
* **OnProductPurchaseCompleted** - Added *OnProductPurchaseCompleted* event that's called once a product or subscription is fully processed by Google Play and returned. NOTE: You should check the state of the response code as this event is raised for both successful and unsuccessful purchases.
* **Documentation** - Minor adjustments to the component's documentation.

###Version 01.01###

The following features and bug fixes have been added to `Xamarin.InAppBilling` in version 01.01:

* **GetPurchases Error** - Fixes an *IllegalArgumentException* that could be thrown when calling *serviceConnection.BillingHandler.GetPurchases* method in a production application.
* **OnPurchaseFailedValidation** - Added the *OnPurchaseFailedValidation* event to trap when a previously purchased item fails security validation.
* **Events** - Fixes issue when *OnProductPurchasedError* and *OnProductPurchased* events could fail to be called correctly.

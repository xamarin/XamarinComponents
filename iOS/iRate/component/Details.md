iRate helps promote your iOS App by prompting users to rate the app after they've used it
for a few days. Targeting relatively loyal users is one of the best ways to get rave
reviews.

## Setup

iRate typically requires little configuration, and will simply run automatically, using
the app bundle's ID to look up the app on the App Store.

If you have apps with matching bundle IDs on both the iOS and Mac App Stores,
the automatic lookup mechanism actually won't work, so you'll need to manually
set the `AppStoreID` property–a numeric ID that can be found in iTunes Connect.

If you wish to customize iRate, do so *before* the app has finished launching
in your AppDelegate's static constructor:

```csharp
using MTiRate;
...

static AppDelegate ()
{
  iRate.SharedInstance.DaysUntilPrompt = 5;
  iRate.SharedInstance.UsesUntilPrompt = 15;
}
```

## Localization

The default strings for iRate are already localized for many languages.

You can override iRate's default strings by using individual setter methods:

```csharp
using MTiRate;
...

static AppDelegate ()
{
  iRate.SharedInstance.MessageTitle = "Rate My App";
  iRate.SharedInstance.Message = "If you like MyApp, please take the time, etc";;
  iRate.SharedInstance.CancelButtonLabel = "No, Thanks";
  iRate.SharedInstance.RemindButtonLabel = "Remind Me Later";
  iRate.SharedInstance.RateButtonLabel = "Rate It Now";
}
```
*Screenshots assembled with [PlaceIt](http://placeit.breezi.com/).*

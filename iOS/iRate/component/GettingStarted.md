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
		
	iRate.SharedInstance.UserDidAttemptToRateApp += (sender, e) => {
		Console.WriteLine ("User is rating app now!");
	};
		
	iRate.SharedInstance.UserDidDeclineToRateApp += (sender, e) => {
		Console.WriteLine ("User does not want to rate app");
	};
		
	iRate.SharedInstance.UserDidRequestReminderToRateApp += (sender, e) => {
		Console.WriteLine ("User will rate app later");
	};
}

// Must override the Window property for iRate to work properly
public override UIWindow Window { get; set; }

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

## Configuration

There are a number of properties of the iRate class that can alter the behavior and
appearance of iRate. These should be mostly self-explanatory, but they are documented
below.

### PreviewMode

```csharp
bool PreviewMode { get; set; }
```

If set to `true`, iRate will always display the rating prompt on launch, regardless of how
long the app has been in use or whether it's the latest version. Use this to proofread
your message and check your configuration is correct during testing, but disable it for
the final release (defaults to `false`).

### AppStoreID

```csharp
uint AppStoreID { get; set; }
```

This should match the iTunes app ID of your application, which you can get from iTunes
connect after setting up your app. This value is not normally necessary and is generally
only required if you have the aforementioned conflict between bundle IDs for your Mac and
iOS apps.

### AppStoreGenreID

```csharp
uint AppStoreGenreID { get; set; }
```

This is the type of app, used to determine the default text for the rating dialog. This is
set automatically by calling an iTunes service, so you shouldn't need to set it manually
for most purposes. If you do wish to override this value, setting it to the
`"iRateAppStoreGameGenreID"` constant will cause iRate to use the "game" version of the
rating dialog, and setting it to any other value will use the "app" version of the rating
dialog.

### AppStoreCountry

```csharp
string AppStoreCountry { get; set; }
```

This is the two-letter country code used to specify which iTunes store to check. It is set
automatically from the device locale preferences, so shouldn't need to be changed in most
cases. You can override this to point to the US store, or another specific store if you
prefer, which may be a good idea if your app is only available in certain countries.

### ApplicationName

```csharp
string ApplicationName { get; set; }
```

This is the name of the app displayed in the iRate alert. It is set automatically from the
application's info.plist, but you may wish to override it with a shorter or longer
version.

### ApplicationBundleID

```csharp
string ApplicationBundleID { get; set; }
```

This is the application bundle ID, used to retrieve the `AppStoreID` and `AppStoreGenreID`
from iTunes. This is set automatically from the app's info.plist, so you shouldn't need to
change it except for testing purposes.

### DaysUntilPrompt

```csharp
float DaysUntilPrompt { get; set; }
```

This is the number of days the user must have had the app installed before they are
prompted to rate it. The time is measured from the first time the app is launched. This is
a floating point value, so it can be used to specify a fractional number of days (e.g.
0.5). The default value is 10 days.

### UsesUntilPrompt

```csharp
uint UsesUntilPrompt { get; set; }
```

This is the minimum number of times the user must launch the app before they are prompted
to rate it. This avoids the scenario where a user runs the app once, doesn't look at it
for weeks and then launches it again, only to be immediately prompted to rate it. The
minimum use count ensures that only frequent users are prompted. The prompt will appear
only after the specified number of days AND uses has been reached. This defaults to 10
events. This defaults to 10 uses.

### EventsUntilPrompt

```csharp
uint EventsUntilPrompt { get; set; }
```

For some apps, launches are not a good metric for usage. For example the app might be a
daemon that runs constantly, or a game where the user can't write an informed review until
they've reached a particular level. In this case you can manually log significant events
and have the prompt appear after a predetermined number of these events. Like the
UsesUntilPrompt setting, the prompt will appear only after the specified number of days
AND events, however once the day threshold is reached, the prompt will appear if EITHER
the event threshold OR uses threshold is reached. This defaults to 10 events.

### UsesPerWeekForPrompt

```csharp
float UsesPerWeekForPrompt { get; set; }
```

If you are less concerned with the total number of times the app is used, but would prefer
to use the *frequency* of times the app is used, you can use the `UsesPerWeekForPrompt`
property to set a minimum threshold for the number of times the user must launch the app
per week (on average) for the prompt to be shown. Note that this is the average since the
app was installed, so if the user goes for a long period without running the app, it may
throw off the average. The default value is zero.

### RemindPeriod

```csharp
float RemindPeriod { get; set; }
```

How long the app should wait before reminding a user to rate after they select the "remind
me later" option (measured in days). A value of zero means the app will remind the user
next launch. Note that this value supersedes the other criteria, so the app won't prompt
for a rating during the reminder period, even if a new version is released in the
meantime.  This defaults to 1 day.

### MessageTitle

```csharp
string MessageTitle { get; set; }
```

The title displayed for the rating prompt. If you don't want to display a title then set
this to `""` (empty string).

### Message

```csharp
string Message { get; set; }
```

The rating prompt message. This should be polite and courteous, but not too wordy. If you
don't want to display a message then set this to `""` (empty string);

### CancelButtonLabel

```csharp
string CancelButtonLabel { get; set; }
```

The button label for the button to dismiss the rating prompt without rating the app.

### RateButtonLabel

```csharp
string RateButtonLabel { get; set; }
```

The button label for the button the user presses if they want to rate the app.

### RemindButtonLabel

```csharp
string RemindButtonLabel { get; set; }
```

The button label for the button the user presses if they don't want to rate the app
immediately, but do want to be reminded about it in future. Set this to `""` if
you don't want to display the remind me button - e.g. if you don't have space on screen.

### UseAllAvailableLanguages

```csharp
bool UseAllAvailableLanguages { get; set; }
```

By default, iRate will use all available languages in the iRate.bundle, even if used in an
app that does not support localization. If you would prefer to restrict iRate to only use
the same set of languages that your application already supports, set this property to NO.
(Defaults to `true`).

### PromptAgainForEachNewVersion

```csharp
bool PromptAgainForEachNewVersion { get; set; }
```
    
Because iTunes ratings are version-specific, you ideally want users to rate each new
version of your app. However, it's debatable whether many users will actually do this, and
if you update frequently this may get annoying. Set `PromptAgainForEachNewVersion` to
`false`, and iRate won't prompt the user again each time they install an update if they've
already rated the app. It will still prompt them each new version if they have *not* rated
the app, but you can override this using the `ShouldPromptForRating` delegate method if
you wish.

### OnlyPromptIfLatestVersion

```csharp
bool OnlyPromptIfLatestVersion { get; set; }
```

Set this to `false` to enabled the rating prompt to be displayed even if the user is not
running the latest version of the app. This defaults to `true` because that way users
won't leave bad reviews due to bugs that you've already fixed, etc.

### PromptAtLaunch

```csharp
bool PromptAtLaunch { get; set; }
```

Set this to `false` to disable the rating prompt appearing automatically when the
application launches or returns from the background. The rating criteria will continue to
be tracked, but the prompt will not be displayed automatically while this setting is in
effect. You can use this option if you wish to manually control display of the rating
prompt.

### VerboseLogging

```csharp
bool VerboseLogging { get; set; }
```

This option will cause iRate to send detailed logs to the console about the prompt
decision process. If your app is not correctly prompting for a rating when you would
expect it to, this will help you figure out why. Verbose logging is enabled by default on
debug builds, and disabled on release and deployment builds.

## Advanced Properties

If the default iRate behavior doesn't meet your requirements, you can implement your own
by using the advanced properties, methods, and delegate. The properties below let you
access internal state and override it.

### RatingsURL

```csharp
NSUrl RatingsURL { get; set; }
```

The URL that the app will direct the user to so they can write a rating for the app. This
is set to the correct value for the given platform automatically. On iOS 5 and below this
takes users directly to the ratings page, but on iOS 6 it takes users to the main app
page. If you are implementing your own rating prompt, you should probably use the
`OpenRatingsPageInAppStore` method instead.


### FirstUsed

```csharp
NSDate FirstUsed { get; set; }
```

The first date on which the user launched the current version of the app. This is used to
calculate whether the DaysUntilPrompt criterion has been met.

### LastReminded

```csharp
NSDate LastReminded { get; set; }
```

The date on which the user last requested to be reminded of an update.

### UsesCount

```csharp
uint UsesCount { get; set; }
```

The number of times the current version of the app has been used (launched).

### EventCount

```csharp
uint EventCount { get; set; }
```

The number of significant application events that have been recorded since the current
version was installed. This is incremented by the LogEvent method, but can also be
manipulated directly. 

### UsesPerWeek

```csharp
float UsesPerWeek { get; set; }
```

The average number of times per week that the current version of the app has been used (launched).

### DeclinedThisVersion

```csharp
bool DeclinedThisVersion { get; set; }
```

This flag indicates whether the user has declined to rate the current version.

### DeclinedAnyVersion

```csharp
bool DeclinedAnyVersion { get; set; }
```

This flag indicates whether the user has declined to rate any previous version of the app
(true) or not (false). This is not currently used by the iRate prompting logic, but may be
useful for implementing your own rules using the `ShouldPromptForRating` delegate method.

### RatedThisVersion

```csharp
bool RatedThisVersion { get; set; }
```

This flag indicates whether the user has already rated the current version.

### RatedAnyVersion

```csharp
bool RatedAnyVersion { get; }
```

This indicates whether the user has previously rated any version of the app.

### Delegate

```csharp
IiRateDelegate Delegate { get; set; }
```

Use this to detect and/or override iRate's default behavior.

## Other Methods

Besides configuration, iRate has the following methods.

### LogEvent

```csharp
void LogEvent (bool deferPrompt);
```

This method can be called from anywhere in your app (after iRate has been configured) and
increments the iRate significant event count. When the predefined number of events is
reached, the rating prompt will be shown. The optional deferPrompt parameter is used to
determine if the prompt will be shown immediately (false) or if the app will wait until
the next launch (true).

### ShouldPromptForRating

```csharp
bool ShouldPromptForRating ();
```

Returns `true` if the prompt criteria have been met, and `false` if they have not. You can
use this to decide when to display a rating prompt if you have disabled the automatic
display at app launch.

### PromptForRating

```csharp
void PromptForRating ();
```

This method will immediately trigger the rating prompt without checking that the  app
store is available, and without calling the iRateShouldShouldPromptForRating delegate
method. Note that this method depends on the `AppStoreID` and `ApplicationGenre`
properties, which are only retrieved after polling the iTunes server, so if you intend to
call this method directly, you will need to set these properties yourself beforehand, or
use the `PromptIfNetworkAvailable ()` method instead.

### PromptIfNetworkAvailable

```csharp
void PromptIfNetworkAvailable ();
```

This method will check if the app store is available, and if it is, it will display the
rating prompt to the user. The `ShouldShouldPromptForRating` delegate method will be
called before the alert is shown, so you can intercept it. Note that if your app is
sandboxed and does not have the network access permission, this method will ignore the
network availability status, however in this case you will need to manually set the
AppStoreID or iRate cannot function.

### OpenRatingsPageInAppStore

```csharp
bool OpenRatingsPageInAppStore ();
```

This method skips the user alert and opens the application ratings page in the iOS app
store, or directly within the app, depending on which platform and OS version is running.
This method does not perform any checks to verify that the machine has network access or
that the app store is available. It also does not call any delegate methods. You should
use this method to open the ratings page instead of the RatingsURL property. Note that
this method depends on the `AppStoreID` which is only retrieved after polling the iTunes
server, so if you intend to call this method directly, you will need to set the
`AppStoreID` property yourself beforehand.

## Delegate Methods and C# Events

The iRateDelegate protocol provides the following methods that can be
used intercept iRate events and override the default behavior. All
methods are optional.

For easier usage every iRateDelegate method is mapped to a C# event with
the same name (as shown in Use iRate section above).

### CouldNotConnectToAppStore

```csharp
void CouldNotConnectToAppStore (iRate sender, NSError error);
```

This method is called if iRate cannot connect to the App Store, usually
because the network connection is down. in which case you will need to
manually set the AppStoreID so that iRate can still function.

### DidDetectAppUpdate

```csharp
void DidDetectAppUpdate (iRate sender);
```

This method is called if iRate detects that the application has been
updated since the last time it was launched.

### ShouldPromptForRating

```csharp
bool ShouldPromptForRating (iRate sender);
```

This method is called immediately before the rating prompt is displayed
to the user. You can use this method to block the standard prompt alert
and display the rating prompt in a different way, or bypass it
altogether.

### DidPromptForRating

```csharp
void DidPromptForRating (iRate sender);
```

This method is called immediately before the rating prompt is displayed. 
This is useful if you use analytics to track what percentage of users 
see the prompt and then go to the app store. This can help you fine tune 
the circumstances around when/how you show the prompt.

### UserDidAttemptToRateApp

```csharp
void UserDidAttemptToRateApp (iRate sender);
```

This is called when the user pressed the rate button in the rating
prompt. This is useful if you want to log user interaction with iRate.
This method is only called if you are using the standard iRate alert
view prompt and will not be called automatically if you provide a custom
rating implementation or call the `OpenRatingsPageInAppStore` method
directly.

### UserDidDeclineToRateApp

```csharp
void UserDidDeclineToRateApp (iRate sender);
```
    
This is called when the user declines to rate the app. This is useful if
you want to log user interaction with iRate. This method is only called
if you are using the standard iRate alert view prompt and will not be
called automatically if you provide a custom rating implementation.

### UserDidRequestReminderToRateApp

```csharp
void UserDidRequestReminderToRateApp (iRate sender);
```

This is called when the user asks to be reminded to rate the app. This
is useful if you want to log user interaction with iRate. This method is
only called if you are using the standard iRate alert view prompt and
will not be called automatically if you provide a custom rating
implementation.

### ShouldOpenAppStore

```csharp
bool ShouldOpenAppStore (iRate sender);
```
    
This method is called immediately before iRate attempts to open the app
store, either via a URL or using the StoreKit in-app product view
controller. Return `false` if you wish to implement your own ratings
page display logic.

### DidPresentStoreKitModal

```csharp
void DidPresentStoreKitModal (iRate sender);
```
    
This method is called just after iRate presents the StoreKit in-app
product view controller. It is useful if you want to implement some
additional functionality, such as displaying instructions to the user
for how to write a review, since the StoreKit controller doesn't open on
the review page. You may also wish to pause certain functionality in
your app, etc.

### DidDismissStoreKitModal
   
```csharp
void DidDismissStoreKitModal (iRate sender);
```

This method is called when the user dismisses the StoreKit in-app
product view controller. This is useful if you want to resume any
functionality that you paused when the modal was displayed.

## Localization

The default strings for iRate are already localized for many languages.
By default, iRate will use all the localizations in the iRate.bundle
even in an app that is not localized, or which is only localized to a
subset of the languages that iRate supports.

If you would prefer iRate to only use the localizations that are enabled
in your application (so that if your app only supports English, French
and Spanish, iRate will automatically be localized for those languages,
but not for German, even though iRate includes a German language file),
set the `UseAllAvailableLanguages` option to `false`.

You can override iRate's default strings like so:

```csharp
static AppDelegate ()
{
	iRate.SharedInstance.MessageTitle = "Rate My App";
	iRate.SharedInstance.Message = "If you like MyApp, please take the time, etc";;
	iRate.SharedInstance.CancelButtonLabel = "No, Thanks";
	iRate.SharedInstance.RemindButtonLabel = "Remind Me Later";
	iRate.SharedInstance.RateButtonLabel = "Rate It Now";
}
```

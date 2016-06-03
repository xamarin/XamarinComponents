
**InAppSettingsKit** is an open source solution to to easily add in-app settings to your iOS apps. 
It uses a hybrid approach by maintaining the Settings.app pane. So the user has the choice where 
to change the settings. 


## How does it work?

To support traditional Settings.app panes, the app must include a `Settings.bundle` with at 
least a `Root.plist` to specify the connection of settings UI elements with `NSUserDefaults` 
keys. InAppSettingsKit basically just uses the same Settings.bundle to do its work. This 
means there's no additional work when you want to include a new settings parameter. It 
just has to be added to the Settings.bundle and it will appear both in-app and in 
Settings.app. All settings types like text fields, sliders, toggle elements, child views etc. 
are supported.


## Using the library

Then you can display the InAppSettingsKit view controller using a navigation push, as a modal 
view controller or in a separate tab of a TabBar based application.

We can show the settings view controller in our existing navigation 
controller: 

    var settings = new AppSettingsViewController();
    settings.ShowDoneButton = false;
    NavigationController.PushViewController(settings, true);

If we want to show the settings view controller as a modal controller, we must first
set the `ShowDoneButton` to `true` and handle the 
`SettingsViewControllerDidEnd` method of the `ISettingsDelegate` interface:

    var settings = new AppSettingsViewController();
    settings.ShowDoneButton = true;
    settings.Delegate = this;
	var navController = new UINavigationController(settings);
	PresentViewController(navController, true, null);

Then, to implement the delegate method:

	public void SettingsViewControllerDidEnd(AppSettingsViewController sender)
    {
        DismissViewController (true, null);
    }
    
We can also show the settings in a `UIPopoverController`:

    var settings = new AppSettingsViewController();
    settings.ShowDoneButton = false;
	var navController = new UINavigationController(settings);
	var popover = new UIPopoverController(navController);
	popover.PresentFromBarButtonItem(sender, UIPopoverArrowDirection.Up, true);

Depending on your project it might be needed to make some changes in the startup code of your app. 
Your app has to be able to reconfigure itself at runtime if the settings are changed by the user. 
This could be done in from the `FinishedLaunching()` method as well as in the 
`SettingsViewControllerDidEnd` delegate method of `AppSettingsViewController`.


### Custom inApp plists

Since iOS 4 Settings plists can be device-dependent: `Root~ipad.plist` will be used on iPad 
and `Root~iphone.plist` on iPhone. If not existent, `Root.plist` will be used. 

InAppSettingsKit adds the possibility to override those standard files by using `.inApp.plist` 
instead of `.plist`. Alternatively, you can create a totally separate bundle named 
`InAppSettings.bundle` instead of the usual `Settings.bundle`. The latter approach is useful 
if you want to suppress the settings in Settings.app.

In summary, the plists are searched in this order:

- InAppSettings.bundle/FILE~DEVICE.inApp.plist
- InAppSettings.bundle/FILE.inApp.plist
- InAppSettings.bundle/FILE~DEVICE.plist
- InAppSettings.bundle/FILE.plist
- Settings.bundle/FILE~DEVICE.inApp.plist
- Settings.bundle/FILE.inApp.plist
- Settings.bundle/FILE~DEVICE.plist
- Settings.bundle/FILE.plist


### iOS 8+: Privacy link

On iOS 8.0 or newer, if the app includes a usage key for various privacy features such as camera or 
location access in its `Info.plist`, IASK displays a "Privacy" cell at the top of the root settings 
page. This cell opens the system Settings app and displays the settings pane for the app where the 
user can specify the privacy settings for the app.

This behavior can be disabled by setting `NeverShowPrivacySettings` to `false`.

The sample app defines `NSMicrophoneUsageDescription` to let the cell appear. Note that the settings 
page doesn't show any privacy settings yet because the app doesn't actually access the microphone. 
Privacy settings only show up in the Settings app after first use of the privacy-protected API.

### Additional Setting Types

#### IASKOpenURLSpecifier

InAppSettingsKit adds a new element that allows to open a specified URL using an external 
application (i.e. Safari or Mail).

#### IASKMailComposeSpecifier

The custom `IASKMailComposeSpecifier` element allows to send mail from within the app by opening 
a mail compose view. You can set the following (optional) parameters using the settings plist: 

 - `IASKMailComposeToRecipents`
 - `IASKMailComposeCcRecipents`
 - `IASKMailComposeBccRecipents` 
 - `IASKMailComposeSubject`
 - `IASKMailComposeBody`
 - `IASKMailComposeBodyIsHTML` 
 
Optionally, you can implement `GetMailComposeBodyForSpecifier` in your delegate to pre-fill the 
body with dynamic content (great to add device-specific data in support mails for example). An 
alert is displayed if Email is not configured on the device. `SettingsSpecifier` is the internal 
model object defining a single settings cell.

    [Export("settingsViewController:mailComposeBodyForSpecifier:")]
    public virtual string GetMailComposeBodyForSpecifier(
        ISettingsViewController settingsViewController, 
        SettingsSpecifier specifier)
    {
    }

Important `SettingsSpecifier` properties:

- `Key`: corresponds to the `Key` in the Settings plist
- `Title`: the localized title of settings key
- `Type`: corresponds to the `Type` in the Settings plist
- `DefaultValue`: corresponds to the `DefaultValue` in the Settings plist


#### IASKButtonSpecifier

InAppSettingsKit adds a `IASKButtonSpecifier` element that allows to call a custom action. Just 
add the following delegate method:

    [Export("settingsViewController:buttonTappedForSpecifier:")]
    public virtual void ButtonTappedForSpecifier(
        AppSettingsViewController sender, 
        SettingsSpecifier specifier)
    {
    }        

The sender is always an instance of `AppSettingsViewController`, a `UIViewController` subclass. 
So you can access its view property (might be handy to display an action sheet) or push another 
view controller. Another nifty feature is that the title of IASK buttons can be overriden by 
the (localizable) value from `NSUserDefaults` (or any other settings store - see below). This 
comes in handy for toggle buttons (e.g. Login/Logout).

By default, Buttons are aligned centered except if an image is specified (default: left-aligned). 
The default alignment may be overridden.

#### FooterText

The FooterText key for Group elements is available in system settings since iOS 4. It is 
supported in InAppSettingsKit as well. On top of that, we support this key for Multi Value 
elements as well. The footer text is displayed below the table of multi value options.

#### IASKCustomViewSpecifier

You can specify your own `UITableViewCell` within InAppSettingsKit by using the type 
`IASKCustomViewSpecifier`. A mandatory field in this case is the `Key` attribute. Also, you 
have to support the `ISettingsDelegate` protocol and implement these methods:

    [Export("tableView:heightForSpecifier:")]
	public virtual nfloat GetHeightForSpecifier(
        UITableView tableView, 
        SettingsSpecifier specifier)
    {
    }
    
	[Export("tableView:cellForSpecifier:")]
	public virtual UITableViewCell GetCellForSpecifier(
        UITableView tableView, 
        SettingsSpecifier specifier)
    {
    }

Both methods are called for all your `IASKCustomViewSpecifier` entries. To differentiate them, 
you can access the `Key` attribute using `specifier.Key`. In the first method you return the 
height of the cell, in the second method the cell itself. You should use reusable 
`UITableViewCell` objects as usual in table view programming. 

Optionally you can implement `DidSelectCustomViewSpecifier` to catch tap events for your 
custom view:

    [Export("settingsViewController:tableView:didSelectCustomViewSpecifier:")]
    public virtual void DidSelectCustomViewSpecifier(
        AppSettingsViewController sender, 
        UITableView tableView, 
        SettingsSpecifier specifier)
    {
    }

#### Custom Group Header Views

You can define custom headers for `PSGroupSpecifier` segments by adding a `Key` attribute and 
implementing these methods in your `ISettingsDelegate`:

    [Export("settingsViewController:tableView:heightForHeaderForSection:")]
    public virtual nfloat GetHeightForHeaderForSection(
        ISettingsViewController settingsViewController, 
        UITableView tableView, 
        nint section)
    {
    }
    
	[Export ("settingsViewController:tableView:viewForHeaderForSection:")]
	public virtual UIView GetViewForHeaderForSection(
        ISettingsViewController settingsViewController, 
        UITableView tableView, 
        nint section)
    {
    }

The behaviour is similar to the custom cells except that the methods get the key directly as 
a string, not via a `SettingsSpecifier` object. (The reason being that custom group header views 
are meant to be static.)

#### Custom View Controllers

##### Class

For child pane elements (`PSChildPaneSpecifier`), Apple requires a `file` key that specifies 
the child plist. InAppSettingsKit allow to alternatively specify `IASKViewControllerClass` and 
`IASKViewControllerSelector`. In this case, the child pane is displayed by instantiating a 
`UIViewController` subclass of the specified class and initializing it using the constructor 
specified in the `IASKViewControllerSelector`. 

The selector must have two arguments: an `NSString` argument for the file name in the Settings 
bundle and the `SettingsSpecifier`. The custom view controller is then pushed onto the navigation 
stack.

##### Storyboards

Alternatively specify `IASKViewControllerStoryBoardId` to initiate a viewcontroller from 
MainStoryboard. Specifiy `IASKViewControllerStoryBoardFile` to use a story board other than 
MainStoryboard file.


#### Subtitles

The `IASKSubtitle` key allows to define subtitles for these elements: 
 - Toggle
 - ChildPane
 - OpenURL
 - MailCompose
 - Button

Note: Using a subtitle implies left alignment.


#### Text alignment

For some element types, a `IASKTextAlignment` attribute may be added with the following 
values to override the default alignment:

- `IASKUITextAlignmentLeft` (ChildPane, TextField, Buttons, OpenURL, MailCompose)
- `IASKUITextAlignmentCenter` (ChildPane, Buttons, OpenURL)
- `IASKUITextAlignmentRight` (ChildPane, TextField, Buttons, OpenURL, MailCompose)


#### Variable font size

By default, the labels in the settings table are displayed in a variable font size, especially 
handy to squeeze-in long localizations (beware: this might break the look in Settings.app if 
labels are too long!).

To disable this behavior, add a `IASKAdjustsFontSizeToFitWidth` Boolean attribute with value `NO`.

#### Icons

All element types (except sliders which already have a `MinimumValueImage`) support an icon image 
on the left side of the cell. You can specify the image name in an optional `IASKCellImage` 
attribute. The ".png" or "@2x.png" suffix is automatically appended and will be searched in 
the project. 

Optionally, you can add an image with suffix "Highlighted.png" or "Highlighted@2x.png" 
to the project and it will be automatically used as a highlight image when the cell is 
selected (for Buttons and ChildPanes).


#### Settings Storage

The default behaviour of IASK is to store the settings in `NSUserDefaults.StandardUserDefaults`. 
However, it is possible to change this behavior by setting the `SettingsStore` property on an 
`AppSettingsViewController`. IASK comes with two store implementations: 
 - `IASKSettingsStoreUserDefaults` (the default one) 
 - `IASKSettingsStoreFile`, which read and write the settings in a file of the path you choose. 
 
If you need something more specific, you can also choose to create your own store. The easiest 
way to create your own store is to create a subclass of `AbstractSettingsStore`. Only 3 
methods are required to override:
 
 - `SetObject()`
 - `GetObject()`
 - `Synchronize()`


#### Notifications

There's a `AppSettingChangedNotification` notification that is sent for every changed settings 
key. The `Object` of the notification is the UserDefaults key (`NSString`). The `UserInfo` 
dictionary contains the new value of the key.

#### Dynamic cell hiding

Sometimes, options depend on each other. For instance, you might want to have an "Auto Connect" 
switch, and let the user set username and password if enabled. To react on changes of a 
specific setting, use the `AppSettingChangedNotification` notification explained above.

To hide a set of cells use one of two members on `AppSettingsViewController`:

    SetHiddenKeys(string[] hiddenKeys, bool animated);

or the non-animated version:

	NSSet HiddenKeys { get; set; }

Note that InAppSettingsKit uses Settings schema, not TableView semantics: If you want to 
hide a group of cells, you have to include the Group entry as well as the member entries.


#### Subclassing notes

If you'd like to customize the appearance of InAppSettingsKit, you might want to subclass 
`AppSettingsViewController` and override some `UITableViewDataSource` or `UITableViewDelegate` 
methods.


## The License

We released the code under the liberal BSD license in order to make it possible to include 
it in every project, be it a free or paid app. The only thing we ask for is giving the 
[original developers](http://www.inappsettingskit.com/about) some credit. The easiest way 
to include credits is by leaving the "Powered by InAppSettingsKit" notice in the code. 

If you decide to remove this notice, a noticeable mention on the App Store description 
page or homepage is fine, too. To gain some exposure for your app we suggest 
[adding your app](http://www.inappsettingskit.com/apps) to our list.

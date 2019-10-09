**InAppSettingsKit** is an open source solution to to easily add in-app 
settings to your iOS apps. It uses a hybrid approach by maintaining the 
Settings.app pane. So the user has the choice where to change the 
settings. 


## How does it work?

To support traditional Settings.app panes, the app must include a 
`Settings.bundle` with at least a `Root.plist` to specify the connection 
of settings UI elements with `NSUserDefaults` keys. InAppSettingsKit 
basically just uses the same Settings.bundle to do its work. This 
means there's no additional work when you want to include a new settings 
parameter. It just has to be added to the Settings.bundle and it will 
appear both in-app and in Settings.app. All settings types like text 
fields, sliders, toggle elements, child views etc. are supported.


## Using the library

Then you can display the InAppSettingsKit view controller using a navigation 
push, as a modal view controller or in a separate tab of a TabBar based 
application.

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

See the Getting Started guide for much more information.

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

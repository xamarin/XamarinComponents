## Adding Windows Azure Messaging to your iOS and Android Apps

The Windows Azure Messaging SDK allows you to register your apps' devices with a Notification Hub in Azure.  


### Getting the Samples to work

It's important to note that out of the box, these samples cannot just work.  You must first follow the steps for each specific platform to setup an Azure Mobile Services instance, as Apple Push Notifications and/or Google Cloud messaging for your app, and configuring your Azure Mobile Services instance to work with each platform.  If you do not follow this setup, you will see `NotImplementedException` exceptions raised.

### iOS Setup

If you have not already done so, you need to setup your app and generate your Push certificate(s) on the Apple Developer portal, as well as create and/or configure a Notification Hub on Azure to use your push certificate(s).  You can follow the guide here: [Get started with Notification Hubs for Xamarin.iOS](http://azure.microsoft.com/en-us/documentation/articles/partner-xamarin-notification-hubs-ios-get-started/).  Only steps 1 through 4 are relevant to this component.

When you are ready to connect your Xamarin.iOS app to the Notification Hub, you should add the following code to your `FinishedLaunching` override in your `AppDelegate`:

```
public override bool FinishedLaunching (UIApplication app, NSDictionary options)
{
	// Process any potential notification data from launch
	ProcessNotification (options);

	// Register for Notifications
	UIApplication.SharedApplication.RegisterForRemoteNotificationTypes (
		UIRemoteNotificationType.Alert |
		UIRemoteNotificationType.Badge |
		UIRemoteNotificationType.Sound);

	// ...
	// Your other code here
	// ...
}
```

You should also override these other methods:

```
public override void RegisteredForRemoteNotifications (UIApplication app, NSData deviceToken)
{
	// Connection string from your azure dashboard
	var cs = SBConnectionString.CreateListenAccess(
		new NSUrl("sb://yourservicebus-ns.servicebus.windows.net/"),
		"YOUR-KEY");

	// Register our info with Azure
	var hub = new SBNotificationHub (cs, "your-hub-name");
	hub.RegisterNative (deviceToken, null, err => {
		if (err != null)
			Console.WriteLine("Error: " + err.Description);
		else
			Console.WriteLine("Success");
	});
}

public override void ReceivedRemoteNotification (UIApplication app, NSDictionary userInfo)
{
	// Process a notification received while the app was already open
	ProcessNotification (userInfo);
}
```

Notice how in both `FinishedLaunching` and `ReceivedRemoteNotification` we call the method `ProcessNotification(NSDictionary userInfo)`.  This is because `ReceivedRemoteNotification` will only be called when a Push Notification is received and the app is already running/in the foreground.  When an app is launched because the user has acted on a notification, the `options` parameter in `FinishedLaunching` will contain the notification information instead.



### Android Setup

If you haven't done so, you should create a Google Console project for your app and enable Google Cloud Messaging on it.  You can follow the guide here: [Get started with Notification Hubs for Xamarin.Android](http://azure.microsoft.com/en-us/documentation/articles/partner-xamarin-notification-hubs-android-get-started/).  Only steps 1 and 2 should be followed.

First of all, you need to define connections string and hub name variables (CONNECTION_STRING and HUB_NAME)

```
// TODO: Customize these values to your own
const string CONNECTION_STRING = "YOUR-HUB-CONNECTION-STRING";
const string HUB_NAME = "YOUR-HUB-NAME";
```

In your application's main `Activity` you should Initialize the Hub and ensure your device is registered. Adding this to the `OnCreate` override is a reasonable choice

```
NotificationHub.Initialize(Application, HUB_NAME, CONNECTION_STRING);
```

To receive notification on your device you should:

1. Add class which implements `INotificationListenerInterface`
2. Add implementation for `INotificationListener.OnPushNotificationReceived`
```
public partial class NotificationListener : Java.Lang.Object, INotificationListener
{
	void INotificationListener.OnPushNotificationReceived(Context context, INotificationMessage message)
	{
		//Push Notification arrived - print out the keys/values
		var data = message.Data;
		if (data != null)
		{
			foreach (var entity in data)
			{
				Android.Util.Log.Debug("AZURE-NOTIFICATION-HUBS", "Key: {0}, Value: {1}", entity.Key, entity.Value);
			}
		}
	}
}
```
3. Register your class as Notification Hub Listener. Adding this to the `OnCreate` override is a reasonable choice
```
NotificationHub.SetListener(new NotificationListener());
```

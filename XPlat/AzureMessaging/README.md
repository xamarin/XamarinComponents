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
	hub.RegisterNativeAsync (deviceToken, null, err => {
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

When you are ready to connect you Xamarin.Android app to the Notification Hub, you should subclass `GcmServiceBase` and `GcmBroadcastReceiverBase<TGcmServiceBase>` in your application.  Note the methods for Initializing the `NotificationHub` and registering with Gcm in the sample.  Also note it's **VERY IMPORTANT** that your Application's package name must not start with an upper case letter, or you will receive a deploy error!

You should add the following permission to your app (you can just include this assembly level attribute, or add it to the Android Manifest):
```csharp
[assembly: UsesPermission (Android.Manifest.Permission.ReceiveBootCompleted)]
```

Here is an example implementation you can copy and paste and change to suit your needs:

```csharp
[BroadcastReceiver(Permission=Constants.PERMISSION_GCM_INTENTS)]
[IntentFilter(new[] { Intent.ActionBootCompleted })] // Allow GCM on boot and when app is closed   
[IntentFilter(new string[] { Constants.INTENT_FROM_GCM_MESSAGE },
	Categories = new string[] { "@PACKAGE_NAME@" })]
[IntentFilter(new string[] { Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK },
	Categories = new string[] { "@PACKAGE_NAME@" })]
[IntentFilter(new string[] { Constants.INTENT_FROM_GCM_LIBRARY_RETRY },
	Categories = new string[] { "@PACKAGE_NAME@" })]
public class SampleGcmBroadcastReceiver : GcmBroadcastReceiverBase<SampleGcmService>
{
	//IMPORTANT: Change this to your own Sender ID!
	//The SENDER_ID is your Google API Console App Project Number
	public static string[] SENDER_IDS = { "1234567890" };
}

[Service] //Must use the service tag
public class SampleGcmService : GcmServiceBase
{
	static NotificationHub hub;

	public static void Initialize(Context context)
	{
		// Call this from our main activity
		var cs = ConnectionString.CreateUsingSharedAccessKeyWithListenAccess (
			new Java.Net.URI ("sb://yourservicebus-ns.servicebus.windows.net/"),
			"YOUR-KEY");

		var hubName = "your-hub-name";

		hub = new NotificationHub (hubName, cs, context);
	}

	public static void Register(Context Context)
	{
		// Makes this easier to call from our Activity
		GcmClient.Register (Context, SampleGcmBroadcastReceiver.SENDER_IDS);
	}

	public SampleGcmService() : base(SampleGcmBroadcastReceiver.SENDER_IDS)
	{
	}

	protected override void OnRegistered (Context context, string registrationId)
	{
		//Receive registration Id for sending GCM Push Notifications to

		if (hub != null)
			hub.Register (registrationId, "TEST");
	}

	protected override void OnUnRegistered (Context context, string registrationId)
	{
		if (hub != null)
			hub.Unregister ();
	}

	protected override void OnMessage (Context context, Intent intent)
	{
		Console.WriteLine ("Received Notification");

		//Push Notification arrived - print out the keys/values
		if (intent != null || intent.Extras != null) {

			var keyset = intent.Extras.KeySet ();

			foreach (var key in intent.Extras.KeySet())
				Console.WriteLine ("Key: {0}, Value: {1}", key, intent.Extras.GetString(key));
		}
	}

	protected override bool OnRecoverableError (Context context, string errorId)
	{
		//Some recoverable error happened
		return true;
	}

	protected override void OnError (Context context, string errorId)
	{
		//Some more serious error happened
	}
}
```

Finally, in your application's main `Activity` you should Initialize the Hub and ensure your device is registered.  Adding this to the `OnCreate` override is a reasonable choice:

```
protected override void OnCreate (Bundle bundle)
{
	base.OnCreate (bundle);

	// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

	// Initialize our Gcm Service Hub
	SampleGcmService.Initialize (this);

	// Register for GCM
	SampleGcmService.Register (this);
}
```

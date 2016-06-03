The Core API provides a flexible way to read and write to Dropbox. It includes support for advanced functionality like search, revisions, and restoring files. While Drop-ins are simpler to integrate and use, the Core API can be a better fit for deeper integration.

The Core API is based on HTTP and OAuth and provides low-level calls to access and manipulate a user's Dropbox account.

## Authenticating with Dropbox

Add the following lines of code to link a user's Dropbox account to your
app:

### In AppDelegate.cs

```csharp
using Dropbox.CoreApi.iOS;
...

// To get your credentials, create your own Drobox App.
// Visit the following link: https://www.dropbox.com/developers/apps
string appKey = "DB_APP_KEY";
string appSecret = "DB_APP_SECRET";

public override bool FinishedLaunching (UIApplication app, NSDictionary options)
{
	
	var session = new Session (appKey, appSecret, Session.RootDropbox);
	// The session that you have just created, will live through all the app
	Session.SharedSession = session;
	
	// ...
	
	return true;
}

public override bool OpenUrl (UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
{
	if (Session.SharedSession.HandleOpenUrl (url) && Session.SharedSession.IsLinked) {
		// Do your magic after the app gets linked
	}
}

```

### In Info.plist

You'll need to register the url scheme "db-APP_KEY" to complete the
authentication flow. Double-click on your app's Info.plist file, select
the Advanced Tab, find the URL Types Section, then click Add URL Type
and set URL Schemes to db-APP_KEY (i.e. "db-aaa111bbb2222").

### Link the user

Now we're all set to start the authentication flow. We'll start by calling the `Session.LinkFromController (UIViewController)` method which will ask the user to authorize your app. The linking process will switch to the Dropbox mobile app if it's installed so the user doesn't need to log in again. Otherwise, a Dropbox authorization view will be presented from the specified view controller:

```csharp
btnLink.TouchUpInside += (sender, e) => {
	if (!Session.SharedSession.IsLinked)
		// Ask for linking the app
		Session.SharedSession.LinkFromController (this);
};
```
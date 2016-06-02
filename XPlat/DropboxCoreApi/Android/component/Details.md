The Core API provides a flexible way to read and write to Dropbox. It includes support for advanced functionality like search, revisions, and restoring files. While Drop-ins are simpler to integrate and use, the Core API can be a better fit for deeper integration.

The Core API is based on HTTP and OAuth and provides low-level calls to access and manipulate a user's Dropbox account.

### Dropbox Keys

The first thing you have to do is to register a new app on the App Console. You'll need the app key and secret to access the Core API.

### Add AuthActivity to AndroidManifest

Once you have the app keys, we'll need to enter the following snippet in your AndroidManifest.xml in order for the Dropbox SDK to finish the authentication process. Insert the following code under the <application> section, replacing YOUR_APP_KEY with your app key:

```xml

<activity
  android:name="com.dropbox.client2.android.AuthActivity"
  android:launchMode="singleTask"
  android:configChanges="orientation|keyboard">
  <intent-filter>
    <!-- Change this to be db- followed by your app key -->
    <data android:scheme="db-YOUR_APP_KEY" />
    <action android:name="android.intent.action.VIEW" />
    <category android:name="android.intent.category.BROWSABLE"/>
    <category android:name="android.intent.category.DEFAULT" />
  </intent-filter>
</activity>

```

Also make sure that your app has the internet permission by ensuring you have the following under the <manifest> section of AndroidManifest.xml:

```xml

<uses-permission android:name="android.permission.INTERNET"></uses-permission>

```

Now you're all set to start interacting with Dropbox.

## Authenticating with Dropbox

You'll need to provide the app key and secret you received when creating the app.

Add the following lines of code to link a user's Dropbox account to your
app:

```csharp

string AppKey = "YOUR_APP_KEY";
string AppSecret = "YOUR_APP_SECRET";
DropboxAPI dropboxApi;

protected async override void OnStart ()
{
	base.OnStart ();
	
	AppKeyPair appKeys = new AppKeyPair(AppKey, AppSecret);
	AndroidAuthSession session = new AndroidAuthSession(appKeys);
	dropboxApi = new DropboxAPI (session);
	(DropboxApi.Session as AndroidAuthSession).StartOAuth2Authentication (this);
}

```

Upon authentication, users are returned to the activity from which they came. To finish authentication after the user returns to your app, you'll need to put the following code in your onResume function.

```csharp

protected async override void OnResume ()
{
	base.OnResume ();

	// After you allowed to link the app with Dropbox,
	// you need to finish the Authentication process
	var session = DropboxApi.Session as AndroidAuthSession;
	if (!session.AuthenticationSuccessful ())
		return;
	
	try {
		// Call this method to finish the authentication process
		session.FinishAuthentication ();
	} catch (IllegalStateException ex) {
		Toast.MakeText (this, ex.LocalizedMessage, ToastLength.Short).Show ();
	}
}

```

Once you've added the code above, you're ready to link the user's
Dropbox account from your UI.
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
		// Will bind the user's access token to the session.
		session.FinishAuthentication ();
		
		// Save the Access Token somewhere
		var accessToken = session.OAuth2AccessToken;
	} catch (IllegalStateException ex) {
		Toast.MakeText (this, ex.LocalizedMessage, ToastLength.Short).Show ();
	}
}

```

You'll need the access token again after your app closes, so it's important to save it for future access (though it's not shown here). If you don't, the user will have to re-authenticate every time they use your app.

Once you've added the code above, you're ready to link the user's
Dropbox account from your UI.

### Invoke methods on background

Most methods in the Core API makes a network call, so make sure to invoke it on a background thread. (If you run this on the main thread, you'll see a NetworkOnMainThreadException.)


## Retrieving metadata (Files or folder)

Once you've linked your app to a Dropbox account, you may want to list
the contents of your app's exclusive Dropbox folder. To achieve this,
you just need to call the `Metadata` method:

```csharp

// Remember to invoke this on a background thread because makes a network call
var metadata = DropboxApi.Metadata (DropboxFolderPath, fileLimit, hash, true, rev);

```

- The first parameter specifies the path where you want to get the files, it could be a file path or a folder path.
- The second parameter specifies the limit of files that you want to download
- The third parameter specifies the hash of the folder or file
- The fourth parameter specifies if you want to get all the info of the files if true, in case that is a folder path
- The fifth parameter specifies the revision of the file that you want to download

For more info, see the documentation[1]

[1]: https://www.dropboxstatic.com/static/developers/dropbox-android-sdk-1.6.3-docs/com/dropbox/client2/DropboxAPI.html#metadata(java.lang.String,%20int,%20java.lang.String,%20boolean,%20java.lang.String)


## Working with files

### Downloading files

Until now, you only have the metadata of the file or directory, but you don't
have the file itself. To download the desired file, you just need 
the `GetFile` method, the DropboxPath and a file stream where you 
want it to be downloaded:

```csharp

using (var output = File.OpenWrite (localPath)) {
	// Gets the file from Dropbox and saves it to the local folder
	DropboxApi.GetFile (DropboxFilePath, null, output, null);
}

```

For images is a similar process, the difference is that you need to specify the size of the image and the format:

```csharp

using (var output = File.OpenWrite (localPath)) {
	// Gets the image from Dropbox and saves it to the local folder
	DropBoxApi.GetThumbnail (DropboxImagePath, output, DropboxApi.ThumbSize.BestFit1024x768, DropboxApi.ThumbFormat.Jpeg, null);
}

```

### Uploading files

Uploading files to Dropbox is a similar process to downloading files. You need to Open the file in a read mode and upload it to Dropbox with the `PutFile` method:

```csharp

using (var input = File.OpenRead (localPath)) {
	// Gets the local file and upload it to Dropbox
	DropBoxApi.PutFile (DropboxFilePath, input, input.Length, null, null);
}

```
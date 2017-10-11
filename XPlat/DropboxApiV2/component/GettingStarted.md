The Core API provides a flexible way to read and write to Dropbox. 
It includes support for advanced functionality like search, revisions, 
and restoring files. While Drop-ins are simpler to integrate and use, 
the Core API can be a better fit for deeper integration.

The Core API is based on HTTP and OAuth and provides low-level calls 
to access and manipulate a user's Dropbox account.

## Authenticating with Dropbox

You'll need to provide your app key and secret as well as the permission 
you selected when creating the app. The permission will be represented 
by either the kDBRootAppFolder or kDBRootDropbox constants.

Once you have your app key and secret, you can create the DBSession 
object for your app. To do this, add the following code in your 
application delegate file:

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
	
	// Create a new Dropbox Session, choose the type of access that your app has to your folders.
	// Session.RootAppFolder = The app will only have access to its own folder located in /Applications/AppName/
	// Session.RootDropbox = The app will have access to all the files that you have granted permission
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

Now we're all set to start the authentication flow. We'll start by calling 
the `Session.LinkFromController (UIViewController)` method which will ask 
the user to authorize your app. The linking process will switch to the 
Dropbox mobile app if it's installed so the user doesn't need to log in again. 
Otherwise, a Dropbox authorization view will be presented from the specified 
view controller:

```csharp
btnLink.TouchUpInside += (sender, e) => {
	if (!Session.SharedSession.IsLinked)
		// Ask for linking the app
		Session.SharedSession.LinkFromController (this);
};
```

## Listing folders and files

Once you've linked your app to a Dropbox account, you may want to list
the contents of your app's exclusive Dropbox folder. To achieve this,
you just need to create a RestClient object:

```csharp
var restClient = new RestClient (Session.SharedSession);
```
and then call the following method:

```csharp
restClient.LoadMetadata (dropboxPath);
```

All the methods on RestClient are asynchronous, meaning they don't 
immediately return the data they are meant to load. Each method has 
at least two corresponding events that get called when a request 
either succeeds or fails.

To handle files and folders received, you need to register a method 
to `MetadataLoaded` event;

```csharp
restClient.MetadataLoaded += (object sender, RestClientMetadataLoadedEventArgs e) => {
	// If the path is a file, there is nothing to search
	if (!e.Metadata.IsDirectory) {
		// Alert the user that specified path is a file, not a directory
	}
	
	foreach (var item in e.Metadata.Contents) {
		// Work with each metadata file or folder
	}
};
```

The RestClient also have an event if something goes wrong when trying to get the files and folders:

```csharp
restClient.LoadMetadataFailed += (object sender, RestClientErrorEventArgs e) => {
	// Handle the error
};
```

## Working with files

### Downloading files

Until now, you only have the Path to the file or directory, but you don't
have the file itself. To download the desired file, you just need 
the `LoadFile` method, the DropboxPath and a local path where you 
want it to be downloaded. The local path must include the local name
that will be saved:

```csharp
restClient.LoadFile (dropboxPath, localPath);
```

As well as the listing files process, downloading a file has its
success and fail events:

```csharp
restClient.FileLoaded += (object sender, RestClientFileLoadedEventArgs e) => {
	// Do something when the file is loaded
};

restClient.LoadFileFailed += (object sender, RestClientErrorEventArgs e) => {
	// Do something if the request failed
};
```

### Uploading files

To upload a file to Dropbox, you just need to call the following method:

```csharp
restClient.UploadFile (filename, dropboxPath, null, localPath);
```

When calling this method, filename is the name of the file and dropboxPath
is the folder where this file will be placed in the user's Dropbox. If you
are uploading a new file, set the parentRev to null, which will prevent
uploads from overwriting existing files, if you are working with an existing
file, pass the revision value of the metadata file, so the file can be
updated correctly, localPath is where the file lives in the device.

As well as the other process, to handle the success and fail events,
use the following events:



Initially, your app's folder in your user's Dropbox won't contain any
files, so you'll need to create one:

```csharp
restClient.FileUploaded += (sender, e) => {
	// Once the file is on Dropbox, do something
};

restClient.LoadFileFailed += (sender, e) => {
	// Handle if something went wrong with the upload of the file
};
```
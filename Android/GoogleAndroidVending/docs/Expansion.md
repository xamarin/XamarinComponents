# APK Expansion Files

[![NuGet](https://img.shields.io/nuget/vpre/Xamarin.Google.Android.Vending.Expansion.Downloader.svg)][1]
[![NuGet](https://img.shields.io/nuget/dt/Xamarin.Google.Android.Vending.Expansion.Downloader.svg)][1]

Google Play currently requires that our APK file be no more than 50MB. For most apps, this is plenty
of space for all the app's code and assets. However, some apps need more space for high-fidelity
graphics, media files, or other large assets. Google Play allows us to attach two large expansion
files that supplement our APK.

Google Play hosts the expansion files for our app and serves them to the device at no cost to us.
The expansion files are saved to the device's shared storage location (the SD card or USB-mountable
partition, also known as the "external" storage) where our app can access them. On most devices,
Google Play downloads the expansion file(s) at the same time it downloads the APK, so our app has
everything it needs when the user opens it for the first time. In some cases, however, our app must
download the files from Google Play when our app starts.

We can obtain the library from [NuGet.org][1] or we can compile the code directly from [GitHub][3].

The official Android documentation can be found on the [Android Developers website][5]. Additional
information can also be found on the [Xamarin Developer website][6].

## The Expansion Files

Expansion files are treated as opaque binary blobs (obb) and each may be up to 2GB in size. Android
does not perform any special processing on these files after they are downloaded - the files can be
in any format that is appropriate for the app. Conceptually, each expansion file plays a different
role:

 - The **main** expansion file is the primary expansion file for additional resources.
 - The **patch** expansion file is optional and intended for small updates to the main
   expansion file.

When Google Play downloads our expansion files to a device, it saves them to the system's shared
storage location. To ensure proper behavior, we must not delete, move, or rename the expansion
files. In the event that our app must perform the download from Google Play itself, we must save
the files to the exact same location. Any updates to the expansion files overwrite the existing
files.

The specific location for our expansion files is:

```
[shared-storage]/Android/obb/[package-name]/
```

 - `[shared-storage]` is the path to the shared storage space, available from
   `Environment.ExternalStorageDirectory`.
 - `[package-name]` is our app's Java-style package name, available from `PackageName`.

Each expansion file we upload will be renamed to match the pattern:

```
[main|patch].[expansion-version].[package-name].obb
```

 - `[main|patch]` specifies whether the file is the main or patch expansion file. There can be
   only one main file and one patch file for each APK.
 - `[expansion-version]` is an integer that matches the version code of the APK with which the
   expansion is **first** associated.
    - "First" is emphasized because although the Developer Console allows we to reuse an
      uploaded expansion file with a new APK, the expansion file's name does not change - it
      retains the version applied to it when we first uploaded the file.
 - `[package-name]` the Java-style app package name.

For example, suppose our APK version is 23 and our package name is com.example.app. If we upload
a main expansion file, the file is renamed to `main.23.com.example.app.obb`.

## Getting Things Ready

We will need to obtain the base64-encoded RSA public key. More information on this in the
[Getting Things Ready section of the Licensing documentation][4].

### Implementing the Downloader

Once we have installed the library and have our key, we need to ensure that the app has the
appropriate permissions to access Play, the licensing service, the Internet, the network state
and the external storage:

```csharp
[assembly: UsesPermission(Android.Manifest.Permission.Internet)]
[assembly: UsesPermission(Android.Manifest.Permission.WriteExternalStorage)]
// required to poll the state of the network connection
// and respond to changes
[assembly: UsesPermission(Android.Manifest.Permission.AccessNetworkState)]
// required to keep CPU alive while downloading files
[assembly: UsesPermission(Android.Manifest.Permission.WakeLock)]
[assembly: UsesPermission(Android.Manifest.Permission.AccessWifiState)]
[assembly: UsesPermission("com.android.vending.CHECK_LICENSE")]
```

Once we have permission, we can then create a `DownloaderService` service that will manage the
downloads of the expansion files. In addition to the download management, the service will
create an alarm to resume downloads and build and update a notification that displays download
progress. We can create the service by simply overriding three properties:

```csharp
[Service]
public class SampleDownloaderService : DownloaderService
{
    // the API key used to access Play
    public override string PublicKey
        => "Base64 API Public Key";

    // the random salt used to encrypt the cached server response
    public override byte[] GetSalt()
        => new byte[] { 98, 100, 12, 43, 2, 8, 4, 9, 5, 106, 108, 33, 45, 1, 84 };

    // the name of the broadcast reciever that will resume 
    // the downloads if the service is stopped
    public override string AlarmReceiverClassName
        => Java.Lang.Class.FromType(typeof(SampleAlarmReceiver)).CanonicalName;
}
```

Finally, we will need to implement a `BroadcastReciever` that will be used to resume any
downloads if the service is stopped:

```csharp
[BroadcastReceiver(Exported = false)]
public class SampleAlarmReceiver : BroadcastReceiver
{
    public override void OnReceive(Context context, Intent intent)
    {
        // start the service if necessary
        DownloaderService.StartDownloadServiceIfRequired(
            context, intent, typeof(SampleDownloaderService));
    }
}
```

### Starting the Check

Once we have the download service and the broadcast receiver, we can then start the
downloading. Before we start any downloads, we should make sure that we have not already
downloaded all the files:

```csharp

// flag to indicate if all downloads are complete
var downloadComplete = true;

// get a list of all the downloaded expansion files
var downloads = DownloadsDB.GetDB().GetDownloads();
if (downloads == null || !downloads.Any())
{
    // start the download as nothing is here
    downloadComplete = false;
}

if (downloads != null)
{
    foreach (var file in downloads)
    {
        if (!Helpers.DoesFileExist(this, file.FileName, file.TotalBytes, false))
        {
            // start the download as this file is incomplete
            downloadComplete = false;
            break;
        }
    }
}
```

If the expansion files are not there or not complete, we need to start the download from
the `OnCreate` method. In order to start the service, we need to provide an `Intent`
that will be used to launch the app when the user taps the notification:

```csharp
// build the intent that launches this activity.
var launchIntent = this.Intent;
var intent = new Intent(this, typeof(MainActivity));
intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTop);
intent.SetAction(launchIntent.Action);
if (launchIntent.Categories != null)
{
    foreach (string category in launchIntent.Categories)
    {
        intent.AddCategory(category);
    }
}

// build PendingIntent used to open this activity when user taps the notification.
var pendingIntent = PendingIntent.GetActivity(
    this, 0, intent, PendingIntentFlags.UpdateCurrent);
```

Now that we have the `Intent`, we can start the service. Starting the service will return
a result that indicates whether the service was started:

```csharp
// request to start the download
var startResult = DownloaderService.StartDownloadServiceIfRequired(
    this, pendingIntent, typeof(SampleDownloaderService));
```

If the service was started, we obtain an `IDownloaderServiceConnection` to the service
which will be used to communicate with the service from the activity:

```csharp
// the DownloaderService has started downloading the files
if (startResult != DownloaderServiceRequirement.NoDownloadRequired)
{
    // create the connection to the service so that we can show progress.
    // when creating the marshaller, we pass in the IDownloaderClient
    // that will be used to handle the updates from the service
    connection = DownloaderClientMarshaller.CreateStub(
        this, typeof(SampleDownloaderService));
}
else
{
    // all files have finished downloading already
}
```

Once we have started the downloader and created a connection, we need to connect and disconnect
in line with the activity's lifecycle. In the `OnResume` method of the activity, we connect to
the service:

```csharp
if (connection != null)
{
    connection.Connect(this);
}
```

And, in the `OnStop` method, we disconnect from the service:

```csharp
if (connection != null)
{
    connection.Disconnect(this);
}
```

### Receiving Download Progress Updates

As the download progresses, we will receive updates such as the number of bytes downloaded, the
download speed and various network states. We can use all of this to display information on the
user interface. This is all in addition to the notification that is automatically created and
managed by the service.

In order to receive updates, we need to get hold of an `IDownloaderService` from the 
`IDownloaderServiceConnection`. To do this, we have to implement the 
`IDownloaderClient` somewhere, such as on the activity:

```csharp
public class MainActivity : Activity, IDownloaderClient
{
    public void OnServiceConnected(Messenger m)
    {
        // create the proxy that is used to communicate with the service
        service = DownloaderServiceMarshaller.CreateProxy(m);

        // let the service know about us and request an update
        service.OnClientUpdated(connection.GetMessenger());
    }

    public void OnDownloadProgress(DownloadProgressInfo progress)
    {
        // handle download progress updates
    }

    public void OnDownloadStateChanged(DownloaderClientState newState)
    {
        // handle download states, such as completion or pause
    }
}
```

### Managing the Service

Once we have the service started and the download going, we may want to be able to pause the
download. We can request the download be paused using the `IDownloaderService`:

```csharp
service.RequestPauseDownload();
```

Similarly, we can resume a download:

```csharp
service.RequestContinueDownload();
```

We may also want to change various properties, such as whether to download over mobile or not
when the download is paused due to Wi-Fi being unavailable. To do this we use the
`SetDownloadFlags` method on the `IDownloaderService`:

```csharp
public void OnDownloadStateChanged(DownloaderClientState newState)
{
    if (newState == DownloaderClientState.PausedNeedCellularPermission ||
        newState == DownloaderClientState.PausedWifiDisabledNeedCellularPermission)
    {
        // let the service know that it can download over mobile
        service.SetDownloadFlags(DownloaderServiceFlags.DownloadOverCellular);

        // resume the download
        service.RequestContinueDownload();
    }
}
```

## Reading the Expansion Files

Because the expansion files are saved to a specific location, namely
`[shared-storage]/Android/obb/[package-name]/`, we could read the files using any file
means available.

If we must unpack the contents of our expansion files, do not delete the `.obb` expansion
files afterwards and do not save the unpacked data in the same directory. We should save our
unpacked files in the directory specified by `GetExternalFilesDir()`. However, if possible,
it's best if we use an expansion file format that allows we to read directly from the file
instead of requiring we to unpack the data. The reason for this is that the expansion files
will now exist twice on the device.

### Using a ContentProvider

One way in which we can read the expansion files is to make use of a content provider that can
read them. In the library, there is a special content provider, the `APEZProvider`, that can read
uncompressed zip files. If all the expansion resources are bundled in an uncompressed, storage
zip archive, this provider allows access to the individual resources without having to first
extract them.

Using this provider is simple and easy to implement:

```csharp
[ContentProvider(new[] { ProviderAuthority }, Exported = false)]
[MetaData(APEZProvider.MetaData.MainVersion, Value = "14")]
[MetaData(APEZProvider.MetaData.PatchVersion, Value = "14")]
public class ZipFileContentProvider : APEZProvider
{
    internal const string ProviderAuthority = "com.package.name.ZipFileContentProvider";

    public override string Authority => ProviderAuthority;
}
```

The `[MetaData]` attributes let the provider know what version of the expansion files to load.
For example, if we have uploaded the app with a version number of 5 and new expansion files,
the version we place in the attributes will be 5. When we update the app, we may choose to use
the same expansion files. So, our new app version will be 6, but because we selected the old
expansion files, they are still version 5. Thus, the provider will still use the version numbers
of 5 in the attributes.

If we want to access a file in this provider, we can use a `Uri` to the resource in the provider:

```csharp
var path = "content://{0}/relative/path/to/movie.mp4";
var uri = Android.Net.Uri.Parse(string.Format(path, ZipFileContentProvider.ProviderAuthority));
videoView.SetVideoURI(uri);
```

It is important to note that the provider uses file descriptors and cannot support reading
compressed expansion files.

### Using the Zip Archive

Another way to access the files is to access them directly by filename. We can use the
`APKExpansionSupport` type to obtain the files stored in each expansion file:

```csharp
var files = APKExpansionSupport.GetAPKExpansionZipFile(context, 14, 14);
using (var stream = files.GetInputStream("relative/path/to/file.extension"))
{
    // process the stream of the contained file
}
```

Files accessed this way can be stored in a compressed format as a decompression stream is
provided. This stream can be processed using typical .NET means, such as providing this
stream to a deserializer or some other reader.

The `APKExpansionSupport.GetAPKExpansionZipFile` method returns a combined collection of all
the items in either of the expansion files. We can then query this for a specific item using
the `GetInputStream` method on the collection. We pass a relative path to the item in expansion
file. Once the entry is returned, we can read the stream of the item.

## Testing the Downloader

Testing the download manager is very similar to testing the licensing. More information on this
in the [Testing the Licensing section of the Licensing documentation][7]. The downloader 
performs the license check internally, and in the response, it receives the expansion files 
URIs from Play. It then uses these to initiate the download.

In order to be able to download the expansion files, we have to have uploaded them when updating
the app. The first upload does not allow the expansion files to be added, but we can just 
re-upload the same package twice. On the second time, we can upload the expansion files with 
the actual app package, remembering to increase the version number beforehand.

We can start testing our app by checking its ability to read the expansion files. We can do this
by placing and naming the files just as the downloader would. Because the downloader always places
the files at the `[shared-storage]/Android/obb/[package-name]/` location, with the name
`[main|patch].[expansion-version].[package-name].obb`, we just have to place our files there. By
skipping the download process, we don't have to upload the app first.

Once we are sure that the app can access and use the expansion files, we can upload the files
with the app to Play and publish to any channel to test the download. Any channel can be used,
including Alpha and Beta.

## Important Things to Remember

Testing the expansion files, from downloading to reading, requires that we have the various bits
in place:

 - The **app version** on the device must be the same as the app that is on the store.
 - Make sure that the app is indeed **published**, and not in draft.
 - Ensure that the Expansion files have been **associated with the app**.
 - Make sure that the response in **"Settings"** is set to **"RESPOND_NORMALLY"** as this is
   the only response that returns the expansion files.
 - Make sure the app has all the required permissions, there are at least 6.

[1]: https://www.nuget.org/packages/Xamarin.Google.Android.Vending.Expansion.Downloader
[2]: https://www.nuget.org/packages/Xamarin.Google.Android.Vending.Expansion.ZipFile
[3]: https://github.com/xamarin/XamarinComponents/tree/master/Android/GoogleAndroidVending
[4]: Licensing.md#getting-things-ready
[5]: https://developer.android.com/google/play/expansion-files.html
[6]: https://developer.xamarin.com/guides/android/deployment,_testing,_and_metrics/publishing_an_application/part_5_-_apk_expansion_files
[7]: Licensing.md#testing-the-licensing

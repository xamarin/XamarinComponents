## Application Licensing

[![NuGet](https://img.shields.io/nuget/vpre/Xamarin.Google.Android.Vending.Licensing.svg)][3]
[![NuGet](https://img.shields.io/nuget/dt/Xamarin.Google.Android.Vending.Licensing.svg)][3]

Google Play offers a licensing service that lets us enforce licensing policies for applications
that we publish on Google Play. With Google Play Licensing, our application can query Google Play
at runtime to obtain the licensing status for the current user, then allow or disallow further
use as appropriate.

The Google Play Licensing service is primarily intended for paid applications that wish to verify
that the current user did in fact pay for the application on Google Play. However, any app
(including free apps) may use the licensing service to initiate the download of an APK expansion
file.

### [Install the NuGet][3]

### [Read more...][1]

## APK Expansion Files

[![NuGet](https://img.shields.io/nuget/vpre/Xamarin.Google.Android.Vending.Expansion.Downloader.svg)][4]
[![NuGet](https://img.shields.io/nuget/dt/Xamarin.Google.Android.Vending.Expansion.Downloader.svg)][4]

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

### [Install the NuGet][4]

### [Read more...][2]

## API Changes

There are several breaking changes when updating from v1.x to v2.0. Although the API is fairly 
similar, there are a few namespace and type changes. All the [changes are documented here][5], 
along with their replacements.

### [Read more...][5]

[1]: docs/Licensing.md
[2]: docs/Expansion.md
[3]: https://www.nuget.org/packages/Xamarin.Google.Android.Vending.Licensing
[4]: https://www.nuget.org/packages/Xamarin.Google.Android.Vending.Expansion.Downloader
[5]: docs/API-Changes.md

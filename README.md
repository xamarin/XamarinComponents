# Open Source Components for Xamarin
Open Source Components for Xamarin are a collection of open source components (including bindings and plugins) created by Xamarin and others in the community.

## Xamarin Supported Open Source Components

Xamarin Supported Open Source components are a collection of Xamarin built bindings and libraries.

| Name                                  | Description                                                                      | Source                                                           |
|---------------------------------------|----------------------------------------------------------------------------------|------------------------------------------------------------------|
| Android Support Libraries             | Bindings for Google's Android Support Libraries                                  | [GitHub](https://github.com/xamarin/AndroidSupportComponents)    |
| Google Play Services Client Libraries | Bindings for Google's Play Services Client Libraries                             | [GitHub](https://github.com/xamarin/GooglePlayServicesComponents)|
| Google API's for iOS                  | Bindings for Google's API's for iOS Libraries                                    | [GitHub](https://github.com/xamarin/GoogleAPIsForiOSComponents)  |
| Facebook SDK's                        | Bindings for Facebook's iOS & Android SDK's                                      | [GitHub](https://github.com/xamarin/FacebookComponents)          |
| Xamarin.Auth                          | Cross-platform API for authenticating users and storing their accounts.          | [GitHub](https://github.com/xamarin/Xamarin.Auth)                |


### [Xamarin.Essentials](https://github.com/xamarin/essentials)

Xamarin.Essentials gives developers essential cross-platform APIs for their mobile applications. Xamarin.Essentials exposes over 25 native APIs in a single cross-platform package for developers to consume in their iOS, Android, UWP, or Xamarin.Forms application. Browse through the [documentation](https://docs.microsoft.com/en-us/xamarin/essentials) on how to get started today.

## Community Provided Open Source Plugins

Plugins for Xamarin are community built NuGet and Components that add cross-platform functionality or abstracts platform specific functionality to a common API. These are both completely cross-platform and extremely small (i.e., they do 1 or 2 things really well with minimal-to-no dependencies). The Plugin API can be accessed on each platform, however, you will most likely only use the common API in a Portable Class Library or Shared Code project.

**Notice**: Plugins for Xamarin featured here are produced by the amazing open source community of Xamarin developers. Xamarin does not provide official support for these plugins, please contact their creator with any issues.


### Popular Plugins
Browse through the most popular plugins out there today!

|Name|Description|NuGet|Docs & Source|Creator|
| ------------------- | --------------------------------- | :-----------: | :-----------: |-------------------- |
|Audio Recorder|Record audio on a device's microphone input.|[NuGet](https://www.nuget.org/packages/Plugin.AudioRecorder)|[GitHub](https://github.com/NateRickard/Plugin.AudioRecorder)|[@NateRickard](https://github.com/NateRickard)|
|Battery Status|Gather battery level, charging status, and type.|[NuGet](https://www.nuget.org/packages/Xam.Plugin.Battery/)|[GitHub](https://github.com/jamesmontemagno/BatteryPlugin)|[@JamesMontemagno](http://www.twitter.com/jamesmontemagno)|
|Barcode Scanner|Scan and create barcodes with ZXing.NET.Mobile.|[NuGet](https://www.nuget.org/packages/ZXing.Net.Mobile)|[GitHub](https://github.com/Redth/ZXing.Net.Mobile)|[@Redth](https://twitter.com/redth)|
|Bluetooth LE|Scan and connect to Bluetooth devices.|[NuGet](https://www.nuget.org/packages/Plugin.BluetoothLE/)|[GitHub](https://github.com/aritchie/bluetoothle)|[@allanritchie911](https://twitter.com/allanritchie911)|
|Calendar|Query and modify device calendars|[NuGet](https://www.nuget.org/packages/CClarke.Plugin.Calendars)|[GitHub](https://github.com/TheAlmightyBob/Calendars/)|[Caleb Clarke](https://github.com/TheAlmightyBob)|
|Compass|Access device compass heading.|[NuGet](https://www.nuget.org/packages/Plugin.Compass/)|[GitHub](https://github.com/JarleySoft/CompassPlugin)|[@cbartonnh](http://www.twitter.com/cbartonnh) & [@JamesMontemagno](http://www.twitter.com/jamesmontemagno)|
|Config|Define the settings of the application for each environment in which it will run.|[NuGet](https://www.nuget.org/packages/Xamarin.ConfigPlugin/)|[GitHub](https://github.com/AgustinBonilla/ConfigPlugin)|[@abonilla93](https://twitter.com/abonilla93)|
|Connectivity|Get network connectivity info such as type and if connection is available.|[NuGet](https://www.nuget.org/packages/Xam.Plugin.Connectivity/)|[GitHub](https://github.com/jamesmontemagno/ConnectivityPlugin)|[@JamesMontemagno](http://www.twitter.com/jamesmontemagno)|
|Cryptography|PCL Crypto provides a consistent, portable set of crypto APIs.|[NuGet](https://www.nuget.org/packages/pclcrypto)|[GitHub](https://github.com/aarnott/pclcrypto)|[@aarnott](https://twitter.com/aarnott)|
|Custom Vision|Runs CoreML and TensorFlow models from https://CustomVision.ai on device|[NuGet](https://www.nuget.org/packages/Xam.Plugins.OnDeviceCustomVision/)|[GitHub](https://github.com/jimbobbennett/Xam.Plugins.OnDeviceCustomVision)|[@JimBobBennett](https://twitter.com/jimbobbennett)|
|Device Info|Properties about device such as OS, Model, and Id.|[NuGet](https://www.nuget.org/packages/Xam.Plugin.DeviceInfo/)|[GitHub](https://github.com/jamesmontemagno/DeviceInfoPlugin)|[@JamesMontemagno](http://www.twitter.com/jamesmontemagno)|
|Device Motion|Provides access to Accelerometer, Gyroscope, Magnetometer, and Compass.|[NuGet](https://www.nuget.org/packages/Xam.Plugin.DeviceMotion/)|[GitHub](https://github.com/CrossGeeks/DeviceSensorsPlugin)|[@rdelrosario](https://github.com/rdelrosario)|
|Embedded Resource|Unpack embedded resource cross-platform.|[NuGet](https://www.nuget.org/packages/Xam.Plugin.EmbeddedResource/)|[GitHub](https://github.com/JosephHill/EmbeddedResourcePlugin)|[@JosephHill](http://www.twitter.com/josephhill)|
|External Maps|Launch external maps from Lat/Long or Address.|[NuGet](https://www.nuget.org/packages/Xam.Plugin.ExternalMaps/)|[GitHub](https://github.com/jamesmontemagno/LaunchMapsPlugin)|[@JamesMontemagno](http://www.twitter.com/jamesmontemagno)|
|File Storage/File System|PCL Storage offers cross platform storage APIs.|[NuGet](https://www.nuget.org/packages/PCLStorage/)|[GitHub](https://github.com/dsplaisted/PCLStorage)|[@dsplaisted](http://www.twitter.com/dsplaisted)|
|File Picker|Pick and save files.|[NuGet](https://www.nuget.org/packages/Xam.Plugin.FilePicker)|[GitHub](https://github.com/Studyxnet/FilePicker-Plugin-for-Xamarin-and-Windows)|[@studyxnet](http://www.twitter.com/studyxnet)|
|File Uploader|Upload files using multipart request.|[NuGet](https://www.nuget.org/packages/Plugin.FileUploader/)|[GitHub](https://github.com/CrossGeeks/FileUploaderPlugin)|[@rdelrosario](http://www.twitter.com/rdelrosario)|
|Fingerprint|Access Fingerprint sensor on iOS, Android, and Windows.|[NuGet](https://www.nuget.org/packages/Plugin.Fingerprint/)|[GitHub](https://github.com/smstuebe/xamarin-fingerprint)|[@smstuebe](https://github.com/smstuebe)|
|FFImageLoading|Image loading with caching, placeholders, transformations and more|[NuGet](https://www.nuget.org/packages/Xamarin.FFImageLoading.Forms/)|[GitHub](https://github.com/molinch/FFImageLoading)|[@molinch](https://github.com/molinch), [@daniel-luberda](https://github.com/daniel-luberda/)|
|Geofencing|Monitor regions when user enters/exits.|[NuGet](https://www.nuget.org/packages/Acr.Geofencing/)|[GitHub](https://github.com/aritchie/geofences)|[@allanritchie911](https://twitter.com/allanritchie911)|
|Geolocator|Easily detect GPS location of device.|[NuGet](https://www.nuget.org/packages/Xam.Plugin.Geolocator/)|[GitHub](https://github.com/jamesmontemagno/GeolocatorPlugin)|[@JamesMontemagno](http://www.twitter.com/jamesmontemagno)|
|HTTP Transfers|Download or upload files in the background. Provides speed metrics and estimates|[NuGet](https://www.nuget.org/packages/Plugin.HttpTransferTasks/)|[GitHub](https://github.com/aritchie/httptransfertasks)|[@allanritchie911](https://twitter.com/allanritchie911)|
|iBeacon & Estimote|Range and monitor Bluetooth beacons.|[NuGet](https://www.nuget.org/packages/Estimotes.Xplat/)|[GitHub](https://github.com/aritchie/estimotes-xplat)|[@allanritchie911](https://twitter.com/allanritchie911)|
|Identity document scanning|Enable scanning of various identity documents in your app.|[NuGet](https://www.nuget.org/packages/BlinkID.Forms/)|[GitHub](https://github.com/blinkid/blinkid-xamarin)|[@microblink](https://twitter.com/microblink)|
|In-App Billing|Make, Query, and get In-App Purchases and Subscriptions.|[NuGet](https://www.nuget.org/packages/Plugin.InAppBilling/)|[GitHub](https://github.com/jamesmontemagno/InAppBillingPlugin)|[@JamesMontemagno](http://www.twitter.com/jamesmontemagno)|
|Lamp|Access to LED|[NuGet](https://www.nuget.org/packages/kphillpotts.Lamp.Plugin/)|[GitHub](https://github.com/kphillpotts/Xamarin.Plugins/tree/master/Lamp)|[@kphillpotts](http://www.twitter.com/kphillpotts)|
|Latest Version|Detect if your users are using the latest version of your app.|[NuGet](https://www.nuget.org/packages/Xam.Plugin.LatestVersion/)|[GitHub](https://github.com/edsnider/LatestVersionPlugin)|[@EdSnider](http://www.twitter.com/EdSnider)|
|Local Notifications|Show local notifications|[NuGet](https://www.nuget.org/packages/Xam.Plugins.Notifier/)|[GitHub](https://github.com/edsnider/LocalNotificationsPlugin)|[@EdSnider](http://www.twitter.com/EdSnider), [@JamesMontemagno](http://www.twitter.com/JamesMontemagno)|
|Local Notifications|Manage local notifications|[NuGet](https://www.nuget.org/packages/Plugin.Notifications/)|[GitHub](https://github.com/aritchie/notifications)|[@allanritchie911](https://twitter.com/allanritchie911)|
|Manage Sleep|Manage auto sleep/auto lock.|[NuGet](https://www.nuget.org/packages/Xam.Plugins.ManageSleep/)|[GitHub](https://github.com/molinch/Xam.Plugins.ManageSleep)|[@molinch0](http://www.twitter.com/molinch0)|
|Media|Take or pick photos and videos.|[NuGet](https://www.nuget.org/packages/Xam.Plugin.Media/)|[GitHub](https://github.com/jamesmontemagno/MediaPlugin)|[@JamesMontemagno](http://www.twitter.com/jamesmontemagno)|
|Media Manager|Playback for Audio and Video.|[NuGet](https://www.nuget.org/packages/Plugin.MediaManager/)|[GitHub](https://github.com/martijn00/XamarinMediaManager)|[@mhvdijk](https://twitter.com/mhvdijk)|
|Messaging|Make phone call, send sms, and send e-mail|[NuGet](https://www.nuget.org/packages/Xam.Plugins.Messaging/)|[GitHub](https://github.com/cjlotz/Xamarin.Plugins)|[@cjlotz](http://www.twitter.com/cjlotz)|
|Microsoft Band|Connect and communicate with the Microsoft Band from shared code!|[NuGet](https://www.nuget.org/packages/Xamarin.Microsoft.Band/)|[GitHub](https://github.com/mattleibow/Microsoft-Band-SDK-Bindings)|[@mattleibow](https://twitter.com/mattleibow)|
|Multilingual Plugin|Simple cross platform plugin for handling language localization.|[NuGet](https://www.nuget.org/packages/Plugin.Multilingual)|[GitHub](https://github.com/CrossGeeks/MultilingualPlugin)|[@CrossGeeks](https://github.com/CrossGeeks/)|
|Permissions|Easily check and request runtime permissions.|[NuGet](http://www.nuget.org/packages/Plugin.Permissions)|[GitHub](https://github.com/jamesmontemagno/PermissionsPlugin)|[@JamesMontemagno](http://www.twitter.com/jamesmontemagno)|
|Portable Razor|Lightweight implemenation of ASP.NET MVC APIs for mobile.|[NuGet](https://www.nuget.org/packages/PortableRazor/)|[GitHub](https://github.com/xamarin/PortableRazor)|[@JosephHill](http://www.twitter.com/josephhill)|
|Push Notifications|Cross platform iOS and Android Push Notifications.|[NuGet](https://www.nuget.org/packages/Plugin.PushNotification)|[GitHub](https://github.com/CrossGeeks/PushNotificationPlugin)|[@rdelrosario](https://github.com/rdelrosario)|
|Secure Storage|Provides secure storage for key value pair string data|[NuGet](http://www.nuget.org/packages/sameerIOTApps.Plugin.SecureStorage/)|[GitHub](https://github.com/sameerkapps/SecureStorage)|[@sameerIOTApps](https://twitter.com/sameerIOTApps)|
|Settings|Simple & Consistent cross platform settings API.|[NuGet](https://www.nuget.org/packages/Xam.Plugins.Settings/)|[GitHub](https://github.com/jamesmontemagno/SettingsPlugin)|[@JamesMontemagno](http://www.twitter.com/jamesmontemagno)|
|Share|Easily share text, links, or open a browser.|[NuGet](https://www.nuget.org/packages/Plugin.Share/)|[GitHub](https://github.com/jguertl/SharePlugin)|[@JamesMontemagno](http://www.twitter.com/jamesmontemagno) & [@Jakob Gürtl](https://github.com/jguertl)|
|Sensors|Device sensors - accelerometer, ambient light, barometer, compass, device orientation, gyroscope, magnetometer, pedometer, and proximity|[NuGet](https://www.nuget.org/packages/Plugin.Sensors)|[GitHub](https://github.com/aritchie/sensors)|[@allanritchie911](https://twitter.com/allanritchie911)|
|Sockets|TCP & UDP Listeners and Clients + UDP multicast.|[NuGet](https://www.nuget.org/packages/rda.SocketsForPCL)|[GitHub](https://github.com/rdavisau/sockets-for-pcl)|[@rdavis_au](http://www.twitter.com/rdavis_au)|
|Speech Recognition|Speech to Text.|[NuGet](https://www.nuget.org/packages/Plugin.SpeechRecognition/)|[GitHub](https://github.com/aritchie/speechrecognition)|[@allanritchie911](https://twitter.com/allanritchie911)|
|Simple Audio Player|Play multiple MP3 or wave files from a shared library.|[NuGet](https://www.nuget.org/packages/Xam.Plugin.SimpleAudioPlayer/)|[GitHub](https://github.com/adrianstevens/Xamarin-Plugins/tree/master/SimpleAudioPlayer)|[@adrianstevens](https://github.com/adrianstevens)|
|Store Review|Ask for store review or launch app store page.|[NuGet](https://www.nuget.org/packages/Plugin.StoreReview/)|[GitHub](https://github.com/jamesmontemagno/StoreReviewPlugin)|[@jamesmontemagno](https://github.com/jamesmontemagno)|
|Text To Speech|Talk back text from shared code.|[NuGet](https://www.nuget.org/packages/Xam.Plugins.TextToSpeech/)|[GitHub](https://github.com/jamesmontemagno/TextToSpeechPlugin)|[@JamesMontemagno](http://www.twitter.com/jamesmontemagno)|
|Toast|A simple way of showing toast/pop-up notifications.|[NuGet](https://www.nuget.org/packages/Toasts.Forms.Plugin)|[GitHub](https://github.com/EgorBo/Toasts.Forms.Plugin)|[@AdamPed](https://github.com/AdamPed) & [@EgorBo](https://github.com/EgorBo)|
|User Dialogs|Message box style dialogs.|[NuGet](https://www.nuget.org/packages/Acr.UserDialogs/)|[GitHub](https://github.com/aritchie/userdialogs)|[@allanritchie911](https://twitter.com/allanritchie911)|
|Version Tracking|Track which versions of your app a user has previously installed.|[NuGet](https://www.nuget.org/packages/Plugin.VersionTracking/1.0.1)|[GitHub](https://github.com/colbylwilliams/VersionTrackingPlugin)|[@ColbyLWilliams](https://twitter.com/colbylwilliams)|
|Vibrate|Vibrate any device.|[NuGet](https://www.nuget.org/packages/Xam.Plugins.Vibrate/)|[GitHub](https://github.com/jamesmontemagno/VibratePlugin)|[@JamesMontemagno](http://www.twitter.com/jamesmontemagno)|
|Device Orientation|Plugin to work with screen orientation (change and lock)|[NuGet](https://www.nuget.org/packages/Plugin.DeviceOrientation/)|[GitHub](https://github.com/wcoder/Xamarin.Plugin.DeviceOrientation)|[@YauheniPakala](https://github.com/wcoder)

### Data Caching & Databases

|Name|Description|NuGet|Docs & Source|Creator|
| ------------------- | --------------------------------- | :-----------: | :-----------: |-------------------- |
|Akavache|An asynchronous, persistent (i.e. writes to disk) key-value store.|[NuGet](https://www.nuget.org/packages/akavache/)|[GitHub](https://github.com/akavache/Akavache)|[@paulcbetts](http://www.twitter.com/paulcbetts)|
|Azure Mobile Apps|Offline sync-enabled Windows and Xamarin mobile apps that connect to Azure Mobile App.|[NuGet](https://www.nuget.org/packages/Microsoft.Azure.Mobile.Client.SQLiteStore/)|[GitHub](https://github.com/Azure/azure-mobile-apps-net-client)|[@microsoft](http://www.twitter.com/microsoft)|
|Cosmos DB|Azure Cosmos DB is a globally distributed, multi-model database service.|[NuGet](https://www.nuget.org/packages/Microsoft.Azure.DocumentDB.Core/)|[GitHub](https://github.com/Azure/azure-documentdb-dotnet)|[@azurecosmosdb](http://www.twitter.com/azurecosmosdb)|
|LiteDB|LiteDB - A .NET NoSQL Document Store in a single data file|[NuGet](https://www.nuget.org/packages/LiteDB/)|[GitHub](https://github.com/mbdavid/LiteDB)|[@mbdavid](https://github.com/mbdavid)|
|🐒 Monkey Cache|Cache any data structure for a specific amount of time with minimal dependencies.|[NuGet](https://www.nuget.org/packages?q=monkeycache)|[GitHub](https://github.com/jamesmontemagno/monkey-cache)|[@jamesmontemagno](https://github.com/jamesmontemagno)|
|Mono.Data.Sqlite|Add Mono.Data.Sqlite to any Xamarin or Windows .NET app.|[NuGet](https://www.nuget.org/packages/Mono.Data.Sqlite.Portable)|[GitHub](https://github.com/mattleibow/Mono.Data.Sqlite)|[@mattleibow](https://twitter.com/mattleibow)|
|Realm|Realm is a mobile database: a replacement for SQLite and ORMs.|[NuGet](https://www.nuget.org/packages/Realm.Database/)|[GitHub](https://github.com/realm/realm-dotnet)|[@realm](https://twitter.com/realm)|
|SQLite-net|Simple, powerful, cross-platform SQLite client and ORM for .NET|[NuGet](https://www.nuget.org/packages/sqlite-net-pcl)|[GitHub](https://github.com/praeclarum/sqlite-net)|[@praeclarum](https://twitter.com/praeclarum)|


### Create a Plugin for Xamarin
If you are looking to create a plugin be sure to browse through NuGet first and ensure that the plugin doesn't exist. If one does join in on the fun and collaborate. If it doesn't and you want to start building a Plugin here are some tools and guidelines to get you started.

**Tools to get Started**
* [Visual Studio Plugin Templates](https://visualstudiogallery.msdn.microsoft.com/afead421-3fbf-489a-a4e8-4a244ecdbb1e): Provides a complete plugin template and easy NuGet specification to publish.
* [Using & Developing Plugins for Xamarin](https://university.xamarin.com/guestlectures/using-developing-plugins-for-xamarin): Join Developer Evangelist James Montemagno as he walks you through creating a plugin from scratch on Xamarin University

**Requirements of a Plugin**
* Open source on GitHub
* Documentation on GitHub's README file
* Name: "FEATURE_NAME Plugin for Xamarin and Windows"
* Namespace: Plugin.FEATURE_NAME
* App-store friendly OSS license (we like MIT)
* No dependency on Xamarin.Forms
* Have a list of supported and unsupported OSs in its GitHub wiki

#### Perks
* Help out fellow developers speed up development
* Have fun and learn new tools
* Our undying love

# API Changes

The switch from the C# port of this library is pretty painless, but there are several changes that 
you need to be aware of. We believe the switch to the native library is for the better for several 
reasons:

 - Official Google code
 - Google will keep it up-to-date with Android releases
 - Fixes several issues (crashes and leaks)

After switching, there will be a few compiler errors that you are most likely to hit:

 - Namespaces have changed to `Google.Android.Vending.*`
 - **Licensing**
    - `AesObfuscator` is now `AESObfuscator`
    - `ApkExpansionPolicy` is now `APKExpansionPolicy`
    - `CallbackErrorCode` is now `LicenseCheckerErrorCode`
    - `PolicyServerResponse` is now `PolicyResponse`
 - **Expansion.Downloader**
    - `DownloaderService` abstract members are now `public`
    - `DownloaderService.Salt` is now a method `DownloaderService.GetSalt()`
    - `DownloaderState` is now `DownloaderClientState`
    - `ServiceMarshaller` is now `DownloaderServiceMarshaller`
    - `DownloadsDatabase` is now `DownloadsDB`
    - `DownloadsDB.GetDownloads()` is now `GetDownloadsList()`
    - `DownloadServiceRequirement` is now `DownloaderServiceRequirement`
    - `ClientMarshaller` is now `DownloaderClientMarshaller`
    - `ServiceFlags` is now `DownloaderServiceFlags`

Although this is a few changes, the compiler will pick them all up and intellisense will find the 
replacement type.

If there are any missing members or changes in behavior, please open an issue so that 
we can rectify this.

## Xamarin.Google.Android.Vending.Licensing

**Namespace**

 - All the namespaces have been merged into `Google.Android.Vending.Licensing`
    - Changed from `LicenseVerificationLibrary` to `Google.Android.Vending.Licensing`
    - Changed from `LicenseVerificationLibrary.Policy` to `Google.Android.Vending.Licensing`
    - Changed from `LicenseVerificationLibrary.Obfuscator` to `Google.Android.Vending.Licensing`
    - Changed from `LicenseVerificationLibrary.DeviceLimiter` to `Google.Android.Vending.Licensing`

**Types**

 - Changed from `AesObfuscator` to `AESObfuscator`
 - Changed from `ApkExpansionPolicy` to `APKExpansionPolicy`
 - Changed from `CallbackErrorCode` to `LicenseCheckerErrorCode`
    - Changed enum item from `ErrorNonMatchingUid` to `NonMatchingUid`
 - Changed from `PolicyServerResponse` to `PolicyResponse`
 - Type `ServerManagedPolicy` no longer has prroperty setters
    - Method `ServerManagedPolicy.ResetPolicy()` is no longer available

## Xamarin.Google.Android.Vending.Expansion.Downloader

**Namespace**

 - All the namespaces have been merged into `Google.Android.Vending.Expansion.Downloader`
    - Changed from `ExpansionDownloader` to `Google.Android.Vending.Expansion.Downloader`
    - Changed from `ExpansionDownloader.Client` to `Google.Android.Vending.Expansion.Downloader`
    - Changed from `ExpansionDownloader.Database` to `Google.Android.Vending.Expansion.Downloader`
    - Changed from `ExpansionDownloader.Service` to `Google.Android.Vending.Expansion.Downloader`

**Types**

 - Changed from `ClientMarshaller` to `DownloaderClientMarshaller`
 - Changed from `ClientMessages` to `DownloaderClientMarshallerMessage`
 - Changed from `ClientMessageParameters` to `DownloaderClientMarshallerParameter`
 - Changed from `DownloaderState` to `DownloaderClientState`
 - Type `DownloaderService` 
    - Abstract members are now `public` instead of `protected`
    - Property `Salt` is now a method `GetSalt()`
 - Changed from `DownloaderServiceActions` to `DownloaderServiceAction`
 - Changed from `ControlAction` to `DownloaderServiceControlAction`
 - Changed from `ServiceFlags` to `DownloaderServiceFlags`
    - Changed enum item from `FlagsDownloadOverCellular` to `DownloadOverCellular`
 - Changed from `ServiceMarshaller` to `DownloaderServiceMarshaller`
 - Changed from `ServiceParameters` to `DownloaderServiceMarshallerParameter`
 - Changed from `NetworkDisabledState` to `DownloaderServiceNetworkAvailability`
 - Changed from `DownloadServiceRequirement` to `DownloaderServiceRequirement`
 - Changed from `ExpansionDownloadStatus` to `DownloaderServiceStatus`
 - Changed from `DownloadStatusExtras` to `DownloaderServiceStatusExtras`
 - Changed from `DownloadsDatabase` to `DownloadsDB`
    - Type `DownloadsDB` is no longer a pure static type
       - The database is now obtained by `DownloadsDB.GetDB()`, so `DownloadsDB.GetDownloads()` 
         now becomes `DownloadsDB.GetDB().GetDownloads()`
    - Method `GetDownloads()` will return `null` if there are no downloads
    - Method `DownloadsDB.GetDownloadsList()` will function the same as the 
      old `DownloadsDatabase.GetDownloads()`

# Application Licensing

[![NuGet](https://img.shields.io/nuget/vpre/Xamarin.Google.Android.Vending.Licensing.svg)][1]
[![NuGet](https://img.shields.io/nuget/dt/Xamarin.Google.Android.Vending.Licensing.svg)][1]

Google Play offers a licensing service that lets us enforce licensing policies for applications
that we publish on Google Play. With Google Play Licensing, our application can query Google Play
at runtime to obtain the licensing status for the current user, then allow or disallow further
use as appropriate.

The Google Play Licensing service is primarily intended for paid applications that wish to verify
that the current user did in fact pay for the application on Google Play. However, any app
(including free apps) may use the licensing service to initiate the download of an APK expansion
file.

The official Android documentation can be found on the [Android Developers website][4]. Additional
information can also be found on the [Xamarin Developer website][5].

## Getting Things Ready

In order to add licensing to our app, all we will need is the licensing verification library. We
can get this from [NuGet.org][1] or we can build the source using the [GitHub repository][2].

After adding the library, we need to get hold of our API key for this app:

  1. Browse to the [Google Play Developer Console][3].
  2. Select **"All applications"** from the sidebar/hamburger menu.
  2. Select the app we want to implement licensing for.
  3. Select **"Development tools"** > **"Services & APIs"** from the left menu.
  4. Scroll to the **"Licensing & In-app billing"** section.
  5. Under the **"Your license key for this application"** heading, copy the base64-encoded 
     RSA public key.
  6. Paste it into a string constant in the app code.

```csharp
const string ApiKey =
    "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAhpTdnmbkaUasM85RWsqjR/p2dxz" +
    "CZjGRoGPe/RglfPjXbO2uwhVC43x2d2NWCytHBhdLYmDVS6XRdev2wnkqqeRh8iBnHZKvcT" +
    "IaQb9FkFUJHCmi9kTlQXk53hz9dKD+jlbO4FoNKHVP3UQfwvmehYv1yVM64qMpsSQ4UcjiE" +
    "8B5qN4j5JZB9sRk9dS3vcwojOBYkazNljWco5hn/FT1WBaaN5TZZ45Qmty/YFSsyu4pUvU5" +
    "KUz7MCq7fhZgN1LzrXrmsvYqd2+EPCm3pN86Zj+y8l4RktFdM3+mvN0Ucr9MKHYXDr2yhtM" +
    "TyWFC88enNhR2Qmn67Qhta/xUUiA9XwIDAQAB";
```

## Adding the License Handlers

Once we have installed the library and have our key, we need to ensure that the app has the
appropriate permissions to access Play and the licensing service:

```csharp
[assembly: UsesPermission(Android.Manifest.Permission.Internet)]

[assembly: UsesPermission("com.android.vending.CHECK_LICENSE")]
// OR
[assembly: UsesPermission(LicenseChecker.Manifest.Permission.CheckLicense)]
```

Once we have permission, We can then implement the `ILicenseCheckerCallback` interface. This
can be implemented on the activity, but does not have to:

```csharp
public class MainActivity : Activity, ILicenseCheckerCallback
{
    public void Allow(PolicyResponse response)
    {
        // Play has determined that the app is owned,
        // either purchased or free
    }

    public void DontAllow(PolicyResponse response)
    {
        // Play has determined that the app should not be available to the user,
        // either because they haven't paid for it or it is not a valid app
        
        // However, there may have been a problem when Play tried to connect,
        // so if this is the case, allow the user to try again
        if (response == PolicyResponse.Retry)
        {
            // try the check again
        }
    }

    public void ApplicationError(LicenseCheckerErrorCode errorCode)
    {
        // There was an error accessing the license
    }
}
```

## Starting the Check

Once we have implemented the interface, all we need to do now is start the check. There are
two basic methods provided in order do this, one with caching and one without.

### Using StrictPolicy

To make things easier to start off with, I will first demonstrate the one without caching,
the `StrictPolicy`:

```csharp
// create the policy we want to use
var policy = new StrictPolicy();

// instantiate a checker, passing a Context, an IPolicy and the Public Key
var checker = new LicenseChecker(this, policy, "Base64 Public Key");

// start the actual check, passing the callback
checker.CheckAccess(this);
```

As soon as the check has completed, either with an error or successfully, one of the methods
on the callback will be called, either `Allow`, `DontAllow` or `ApplicationError`:

 - `Allow` will receive a `Licensed` response  
 - `DontAllow` will receive a `NotLicensed` response  
 - `ApplicationError` will have the reason for the error, such as
   `NotMarketManaged`, `InvalidPublicKey` or some other reason.

### Using ServerManagedPolicy

Although checking with Play each time the app launches is not a problem, doing so requires
additional time and resources before the app can start. Usually, we can use the one with caching,
the `ServerManagedPolicy` policy. This is very much the same as the `StrictPolicy`, but with an
additional step to provide an `IObfuscator` to store the response:

```csharp
// create a random salt to be used by the AES encryption process
var salt = new byte[] { 46, 65, 30, 128, 103, 57, 74, 64, 51, 88, 95, 45, 77, 117, 36 };

// create a app-unique identifer to prevent other apps from decrypting the responses
var appId = this.PackageName;

// create a device-unique identifier to prevent other devices from decrypting the responses
var deviceId = Settings.Secure.GetString(ContentResolver, Settings.Secure.AndroidId);
```

```csharp
// create the obfuscator that will read and write the saved responses, 
// passing the salt, the package name and the device identifier
var obfuscator = new AESObfuscator(salt, appId, deviceId);

// create the policy, passing a Context and the obfuscator
var policy = new ServerManagedPolicy(this, obfuscator);

// create the checker
var checker = new LicenseChecker(this, policy, Base64PublicKey);

// start the actual check, passing the callback
checker.CheckAccess(this); 
```

As soon as the checker has returned and we know that we can start the application, we should
destroy the checker in order to free up resources and close connections:

```csharp
// free resources and close connections
checker.OnDestroy();
```

## Testing the Licensing

The last thing that is needed is testing. To do this, we have to be sure that we have uploaded
**and published** the app to Play. Publishing to any of the channels, including Alpha and Beta,
will work. 

In order for the Alpha or Beta channels to be used on devices other than the publisher's device,
those people have to be added to the Alph or Beta testers group.

If we want to test different responses that our app may receive from Play, we can select the
desired response from the settings:

  1. Select **"Settings"** from the sidebar.
  2. Select **"Developer account"** > **"Account details"** from the left menu.
  3. Scroll down to the **"License Testing"** section.
  4. Select the desired response from the drop down titled **"License Test Response"**.
      * For other testers, make sure we enter their Google account email address in the text
        area above titled **"Gmail accounts with testing access"**.

## Important Things to Remember

Testing licensing is easy to do, provided we have all the required bits in place. Here are
some common things that we may have to check:

### The Play Store

  1. The app **version** on the device must be the same as the app that is on the store.
  2. Provide **enough time** for the app to appear on the store, this can be determined by a
     little exclamation sign next to the app title when viewing the app details.
  3. Make sure that the app is indeed **published**, and not in **draft**.

### For Testers

  1. Ensure that the **testers have been added** to the Alpha or Beta testers group.
  2. If we are testing custom responses, make sure that their emails are added to the
     **"Gmail accounts with testing access"** text area in "Settings".
  3. All testers will have to download the app from the store at least once, including
     the developer.

### For The App

  1. Make sure we have the Android permission: `com.android.vending.CHECK_LICENSE`.
  2. Ensure that the app has the same **version code/number** and package name as that which is
     on the store.


[1]: https://www.nuget.org/packages/Xamarin.Google.Android.Vending.Licensing
[2]: https://github.com/xamarin/XamarinComponents/tree/master/Android/GoogleAndroidVending
[3]: https://play.google.com/apps/publish
[4]: https://developer.android.com/google/play/licensing
[5]: https://developer.xamarin.com/guides/android/deployment,_testing,_and_metrics/publishing_an_application/part_4_-_google_licensing_services/

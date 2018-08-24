# Crashlytics for Xamarin.Android

Crashlytics is part of Fabric.io and now Firebase.  This folder contains bindings to enable Crashlytics to be used in Xamarin.Android apps.


## Setup

1. Make sure you have a project setup in the [Firebase Developer Console](http://console.firebase.google.com).
2. Download your project's `google-services.json` file and place it in the folder of your Xamarin.Android project.
3. Add the `Xamarin.Android.Crashlytics` NuGet package.
4. Restart your IDE
5. Add `google-services.json` to your Xamarin.Android app project.
6. Set the *build action* of `google-services.json` to `GoogleServicesJson`
7. Create a string resource with the name `com.crashlytics.android.build_id`.  The value can be whatever you want to uniquely identify a particular build with.
8. Clean and Rebuild your solution.


## Initialization

You can initialize the Crashlytics SDK with the following code in your main activity's `OnCreate`:

```csharp
Fabric.Fabric.With(this, new Crashlytics.Crashlytics());
```

Optionally, you can use the included Mono Exception helper to catch unhandled android exceptions and format them more nicely:

```csharp
Crashlytics.Crashlytics.HandleManagedExceptions();
```

# Mapbox Access Token

An access token is necessary to use Mapbox services and APIs, such as maps, directions, and geocoding. Your access tokens can be managed in your account settings, where you can retrieve current tokens and generate new ones. You should create a new token for each of your apps, which will help you track usage and minimize disruption in the event a token needs to be revoked.

Visit http://mapbox.com to create an account and generate an access token.


# iOS

Once you have generated an access token, you need to set it up within your app.  There are two ways to provide an access token in your app:

  1. In the ***Info.plist*** file set ***MGLMapboxAccessToken*** with the value of your token.
  2. In the ***AppDelegate.FinishedLaunching*** method, call `AccountManager.AccessToken = "YOUR-TOKEN";`
  

## Metrics Opt-Out

The Mapbox Terms of Service require your app to provide users with a way to individually opt out of Mapbox Metrics. You can provide this opt out in one of two ways:

  1. Add a setting to your app’s section in the system Settings app (via a Settings.bundle in your application bundle).  You can download a [sample Settings.bundle file](https://www.mapbox.com/guides/data/ios/Settings.bundle.zip) to help you implement this.
  2. Integrate the setting directly into your app. Hook the UISwitch control up to the `MGLMapboxMetricsEnabled` *Boolean* user default, which should be *YES* by default. Then set `MGLMapboxMetricsEnabledSettingShownInApp` to *YES* in your app’s *Info.plist* file.
  

## Location Services

In order to show the user’s position on the map, you must first ask for their permission. In iOS 8 and above, this is accomplished by creating and setting the `NSLocationAlwaysUsageDescription` key in the *Info.plist* file.

For developers on the free Starter plan, background location services must also be enabled. You can find instructions on how to do this in our [First Steps guide](https://www.mapbox.com/guides/first-steps-ios-sdk/#background-location).


## Maps

Creating a map is very simple using the `MapView` class:

```csharp
// Create a MapView and set the coordinates/zoom
mapView = new MapView (View.Bounds);
mapView.SetCenterCoordinate (new CLLocationCoordinate2D (40.7326808, -73.9843407), false);
mapView.SetZoomLevel (11, false);

View.AddSubview (mapView);
```

You can add annotations to your map like this:
```csharp
// Add new annotation
mapView.AddAnnotation (new PointAnnotation {
    Coordinate = new CLLocationCoordinate2D (40.7326808, -73.9843407),
    Title = "Sample Marker",
    Subtitle = "This is the subtitle"
});
```




# Android

## Permissions

Mapbox for Android requires a couple of permissions to work out of the box.  You can add these permissions to the `AndroidManifest.xml` file manually, or add these assembly level attributes somewhere in your C# code:

```csharp
[assembly: UsesPermission (Android.Manifest.Permission.AccessNetworkState)]
[assembly: UsesPermission (Android.Manifest.Permission.Internet)]
```

If you would like to show the user's position on the map, you must first ask for their permisson.  This is done by adding the *Access Course Location* and *Access Fine Location* permissions either manually to the `AndroidManifest.xml` file or by adding these assembly level attributes in your C# code:

```csharp
[assembly: UsesPermission (Android.Manifest.Permission.AccessCoarseLocation)]
[assembly: UsesPermission (Android.Manifest.Permission.AccessFineLocation)]
```


## Map

You can add a `MapView` to your layouts with the following xml:

```xml
 <com.mapbox.mapboxsdk.maps.MapView
    xmlns:mapbox="http://schemas.android.com/apk/res-auto"
    android:id="@+id/mapView"
    android:layout_width="fill_parent"
    android:layout_height="fill_parent"
    mapbox:access_token="YOUR-ACCESS-TOKEN" />
```

Note that you should replace the `accessToken` attribute with the value of your own access token!


In your activity's `OnCreate` you can set the map up like this:

```csharp
mapView = FindViewById<MapView> (Resource.Id.mapview);
mapView.StyleUrl = Style.Emerald;
mapView.OnCreate (savedInstanceState);

var mapboxMap = await mapView.GetMapAsync ();

var position = new CameraPosition.Builder ()
    .Target (new LatLng (41.885, -87.679)) // Sets the new camera position
    .Zoom (11) // Sets the zoom
    .Build (); // Creates a CameraPosition from the builder

mapboxMap.AnimateCamera (CameraUpdateFactory.NewCameraPosition (position), 3000);
```

Adding annotations is just as easy:

```csharp
mapboxMap.AddMarker (new MarkerOptions ()
    .SetTitle ("Test Marker")
    .SetPosition (new LatLng (41.885, -87.679)));
```

It's also important to implement the other Activity lifecycle events and pass them along to your MapView:

```csharp
protected override void OnPause ()
{
    base.OnPause ();
    mapView.OnPause ();
}

protected override void OnResume ()
{            
    base.OnResume ();
    if (mapView != null)
    mapView.OnResume ();
}

public override void OnLowMemory ()
{
    base.OnLowMemory ();
    mapView.OnLowMemory ();
}

protected override void OnDestroy ()
{            
    base.OnDestroy ();
    mapView.OnDestroy ();
}

protected override void OnSaveInstanceState (Bundle outState)
{            
    base.OnSaveInstanceState (outState);
    mapView.OnSaveInstanceState (outState);
}
```


# More information

For more information, visit the [Mapbox Developers Website](https://www.mapbox.com/developers/).
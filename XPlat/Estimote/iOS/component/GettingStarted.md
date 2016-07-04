## Configuration
In your app’s AppDelegate on FinishedLaunching you can specify your Estimote app config:
```csharp
Config.SetupAppID (“appId”, “appToken”);
```

**iOS8**
You must specify `NSLocationAlwaysUsageDescription` or `NSLocationWhenInUseUsageDescription` in you info.plst file with a description that will be prompted to your users. Additionally, you must call the BeaconManager's `RequestAlwaysAuthorization` or `RequestWhenInUseAuthorization` methods. 

## iBeacons

The following code shows an example of how to use the `BeaconManager` to range for beacons.

```csharp
BeaconManager beaconManager;
BeaconRegion region;

public async override void ViewDidLoad ()
{
	base.ViewDidLoad ();
	this.Title = "Select Beacon";
	beaconManager = new BeaconManager ();
	beaconManager.ReturnAllRangedBeaconsAtOnce = true;
	var uuid = new NSUuid ("8492E75F-4FD6-469D-B132-043FE94921D8");
	region = new BeaconRegion (uuid, "BeaconSample");
	beaconManager.StartRangingBeacons(region);
	beaconManager.RangedBeacons += (sender, e) => 
	{
		new UIAlertView("Beacons Found", "Just found: " + e.Beacons.Length + " beacons.", null, "OK").Show();
	};
}
```


### Authorization
You can use the Estimote SDK for iOS to request authorization:

You can subscribe to authorazation changes on the BeaconManager:

```csharp
beaconManager.AuthorizationStatusChanged += (sender, e) => 
{
	StartRangingBeacons();
};
```

Additionally, you can request authorization with:

```
private void StartRangingBeacons()
{
	var status = BeaconManager.AuthorizationStatus ();
	if (status == CLAuthorizationStatus.NotDetermined)
	{
		if (!UIDevice.CurrentDevice.CheckSystemVersion(8, 0)) {

			beaconManager.StartRangingBeacons(region);
		} else {

			beaconManager.RequestAlwaysAuthorization ();
		}
	}
	else if(status == CLAuthorizationStatus.Authorized)
	{
		beaconManager.StartRangingBeacons(region);
	}
	else if(status == CLAuthorizationStatus.Denied)
	{
		new UIAlertView ("Access Denied", "You have denied access to location services. Change this in app settings.", null, "OK").Show ();
	}
	else if(status == CLAuthorizationStatus.Restricted)
	{
		new UIAlertView ("Location Not Available", "You have no access to location services.", null, "OK").Show ();
	}
}
```



## Nearables
The Estimote SDK for iOS can also be used with Estimote Stickers. Here is an example of using the `NearableManager` to range for wearables.

**On iOS 8**
You must specify `NSLocationAlwaysUsageDescription` or `NSLocationWhenInUseUsageDescription` in you info.plst file with a description that will be prompted to your users. 

```csharp
NearableManager manager;
public override void ViewDidLoad ()
{
	base.ViewDidLoad ();

	TableView.WeakDataSource = this;
	TableView.WeakDelegate = this;

	manager = new NearableManager ();

	manager.RangedNearables += (sender, e) => 
	{
		new UIAlertView("Nearables Found", "Just found: " + e.Nearables.Length + " nearables.", null, "OK").Show();
	};

	manager.StartRanging (NearableType.All);
}

```

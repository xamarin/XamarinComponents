The system requirements are Android 4.3 and Bluetooth Low Energy.

## Quick start for beacon ranging

The `BeaconManager` class is the primary means of interating with Estimotes. Create an instance of this class, and use the `.Connect` method, passing it `BeaconManager.IServiceReadyCallback` object. When the BeaconManager is up and running, it will notify clients by call `BeaconManager.IServiceReadyCallback.OnServiceReady()`. At this point the client can start ranging or monitoring for the Estimotes.

The following code shows an example of how to use the `BeaconManager`.

```csharp
using Estimote;

namespace Estimotes.Droid
{
    [Activity(Label = "Notify Demo")]	
    public class NotifyDemoActivity : Activity, BeaconManager.IServiceReadyCallback
    {
        static readonly int NOTIFICATION_ID = 123321;

        BeaconManager _beaconManager;
        Region _region;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.notify_demo);

            _region = this.GetBeaconAndRegion().Item2;
            _beaconManager = new BeaconManager(this);

            // Default values are 5s of scanning and 25s of waiting time to save CPU cycles.
            // In order for this demo to be more responsive and immediate we lower down those values.
            _beaconManager.SetBackgroundScanPeriod(TimeUnit.Seconds.ToMillis(1), 0);
            _beaconManager.EnteredRegion += (sender, e) => {
                // Do something as the device has entered in region for the Estimote.
            };
            _beaconManager.ExitedRegion += (sender, e) => {
                // Do something as the device has left the region for the Estimote.            
            };
        
        }

        protected override void OnResume()
        {
            base.OnResume();
            _beaconManager.Connect(this);
        }

        public void OnServiceReady()
        {
            // This method is called when BeaconManager is up and running.
            _beaconManager.StartMonitoring(_region);
        }

        protected override void OnDestroy()
        {
        	// Make sure we disconnect from the Estimote.
            _beaconManager.Disconnect();
            base.OnDestroy();
        }
    }
}

```



## Quick start for nearable discovery
```csharp
public class NearableActivity : Activity, BeaconManager.IServiceReadyCallback
{
	BeaconManager beaconManager;
    string scanId;
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        // Create beacon manager
        beaconManager = new BeaconManager(this);

        // Wearables will be triggered when nearables are found
        beaconManager.Nearable += (sender, e) => 
            {
                ActionBar.Subtitle = string.Format("Found {0} nearables.", e.Nearables.Count;
            };

        //Connect to beacon manager to start scanning
	   beaconManager.Connect(this);
    }

    protected override void OnStop()
    {
        base.OnStop();
        if (!isScanning)
            return;
        
        isScanning = false;
        beaconManager.StopNearableDiscovery(scanId);
    }

    public void OnServiceReady()
    {
        isScanning = true;
        scanId = beaconManager.StartNearableDiscovery();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        beaconManager.Disconnect();
    }
}
```

## Quick start for Eddystone

[Eddystone](https://developers.google.com/beacons) is an open protocol BLE protocol from Google. Estimote Beacons can broadcast the Eddystone packet.

With Estimote SDK you can:
 - find nearby Eddystone beacons 
 - configure Eddystone ralated properties:
   - URL property of `Eddystone-URL` 
   - namespace & instance properties of `Eddystone-UID` 
 - configure broadcasting scheme of beacon to `Estimote Default`, `Eddystone-UID` or `Eddystone-URL`

Note that you can play with Estimote Beacons broadcasting the Eddystone packet and change their configuration via [Estimote app on Google Play](https://play.google.com/store/apps/details?id=com.estimote.apps.main).

In order to start playing with Eddystone you need to update firmware of your existing Estimote beacons to `3.1.1`. Easiest way is through [Estimote app on Google Play](https://play.google.com/store/apps/details?id=com.estimote.apps.main). Than you can change broadcasting scheme on your beacon to Eddystone-URL or Eddystone-UID.

Following code snippet shows you how you can start discovering nearby Estimote beacons broadcasting Eddystone packet:

```csharp
public class NearableActivity : Activity, BeaconManager.IServiceReadyCallback
{
    BeaconManager beaconManager;
    string scanId;
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        // Create beacon manager
        beaconManager = new BeaconManager(this);

        // Wearables will be triggered when nearables are found
        beaconManager.Eddystone += (sender, e) => 
            {
                ActionBar.Subtitle = string.Format("Found {0} eddystones.", e.Eddystones.Count;
            };

        //Connect to beacon manager to start scanning
       beaconManager.Connect(this);
    }

    protected override void OnStop()
    {
        base.OnStop();
        if (!isScanning)
            return;
        
        isScanning = false;
        beaconManager.StopEddystoneScanning(scanId);
    }

    public void OnServiceReady()
    {
        isScanning = true;
        scanId = beaconManager.StartEddystoneScanning();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        beaconManager.Disconnect();
    }
}

```


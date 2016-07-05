using System;
using System.Collections.Generic;
using System.Linq;

using Android.Content;
using Android.OS;
using Android.Util;
using Android.Widget;

using EstimoteSdk;

using JavaObject = Java.Lang.Object;

namespace Estimotes.Droid
{
    class FindSpecificBeacon : BeaconFinder, BeaconManager.IRangingListener
    {
        static readonly string Tag = typeof(FindSpecificBeacon).FullName;
        Beacon _beacon;
        bool _isSearching;
        Region _region;
        public EventHandler<BeaconFoundEventArgs> BeaconFound = delegate { };

        public FindSpecificBeacon(Context context) : base(context)
        {
            BeaconManager.SetRangingListener(this);
        }

        public void OnBeaconsDiscovered(Region region, IList<Beacon> beacons)
        {
            Log.Debug(Tag, "Found {0} beacons", beacons.Count);
            Beacon foundBeacon = (from b in beacons
                                  where b.MacAddress.Equals(_beacon.MacAddress)
                                  select b).FirstOrDefault();
            if (foundBeacon != null)
            {
                BeaconFound(this, new BeaconFoundEventArgs(foundBeacon));
            }
        }

        public override void OnServiceReady()
        {
            if (_region == null)
            {
                throw new Exception("What happened to the _region?");
            }
            try
            {
                BeaconManager.StartRanging(_region);
                Log.Debug(Tag, "Looking for beacons in the region.");
                _isSearching = true;
            }
            catch (RemoteException e)
            {
                _isSearching = false;
                Toast.MakeText(Context, "Cannot start ranging, something terrible happened!", ToastLength.Long).Show();
                Log.Error(Tag, "Cannot start ranging, {0}", e);
            }
        }

        public void LookForBeacon(Region region, Beacon beacon)
        {
            _beacon = beacon;
            _region = region;
            BeaconManager.Connect(this);
        }

        public override void Stop()
        {
            if (_isSearching)
            {
                BeaconManager.StopRanging(_region);
                base.Stop();
                _region = null;
                _beacon = null;
                _isSearching = false;
            }
        }
    }
}

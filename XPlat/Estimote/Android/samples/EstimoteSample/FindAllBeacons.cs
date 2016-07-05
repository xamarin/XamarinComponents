using System;
using System.Collections.Generic;
using System.Linq;

using Android.Content;
using Android.Util;

using EstimoteSdk;

namespace Estimotes.Droid
{
    class FindAllBeacons : BeaconFinder
    {
        public static readonly Region ALL_ESTIMOTE_BEACONS_REGION = new Region("rid", "B9407F30-F5F8-466E-AFF9-25556B57FE6D");

        static readonly string TAG = typeof(FindAllBeacons).Name;

        public EventHandler<BeaconsFoundEventArgs> BeaconsFound = delegate { };

        public FindAllBeacons(Context context) : base(context)
        {
            BeaconManager.Ranging += HandleRanging;
        }

        public override void OnServiceReady()
        {
            BeaconManager.StartRanging(ALL_ESTIMOTE_BEACONS_REGION);
        }

        protected virtual void HandleRanging(object sender, BeaconManager.RangingEventArgs e)
        {
            Log.Debug(TAG, "Found {0} beacons.", e.Beacons.Count);
//            IEnumerable<Beacon> beacons = from item in e.Beacons
//                                          let uuid = item.ProximityUUID
//                                          where uuid.Equals(EstimoteBeacons.EstimoteProximityUuid, StringComparison.OrdinalIgnoreCase) ||
//                                                uuid.Equals(EstimoteBeacons.EstimoteIosProximityUuid, StringComparison.OrdinalIgnoreCase)
//                                          select item;
            BeaconsFound(this, new BeaconsFoundEventArgs(e.Beacons));
        }

        public void FindBeacons(Context context)
        {
            //TODO: Properly detect BT Enabled
            var btEnabled = true;
            if (!btEnabled)
            {
                throw new Exception("Bluetooth is not enabled.");
            }
            BeaconManager.Connect(this);
        }

        public override void Stop()
        {
            BeaconManager.StopRanging(ALL_ESTIMOTE_BEACONS_REGION);
            
            base.Stop();
        }
    }
}

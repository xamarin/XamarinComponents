using System;

using Android.App;
using Android.Content;
using Android.Widget;

using EstimoteSdk;

using JavaInteger = Java.Lang.Integer;

namespace Estimotes.Droid
{
    public static class ActivityHelpers
    {
        static readonly string EXTRAS_BEACON = "extrasBeacon";

        public static Beacon GetBeacon(this Activity activity)
        {
            Beacon beacon = activity.Intent.GetParcelableExtra(EXTRAS_BEACON) as Beacon;
            if (beacon == null)
            {
                Toast.MakeText(activity, "Beacon not found in intent extras.", ToastLength.Long).Show();
                activity.Finish();
                return null;
            }
            return beacon;
        }

        public static void StartActivityForBeacon<TActivity>(this Activity activity, Beacon beacon) where TActivity : Activity
        {
            Type type = typeof(TActivity);
            Intent intent = new Intent(activity, type);
            intent.PutExtra(EXTRAS_BEACON, beacon);
            activity.StartActivity(intent);
        }

        public static Region CreateRegion(this Beacon beacon)
        {
            Region region = new Region("region_id", beacon.ProximityUUID, new JavaInteger(beacon.Major), new JavaInteger(beacon.Minor));
            return region;
        }

        public static Tuple<Beacon, Region> GetBeaconAndRegion(this Activity activity)
        {
            Beacon beacon = GetBeacon(activity);
            Region region = beacon.CreateRegion();

            return new Tuple<Beacon, Region>(beacon, region);
        }
    }
}

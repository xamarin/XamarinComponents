using System;
using Android.Runtime;

namespace EstimoteSdk.Observation.Region.Beacon
{
	public partial class BeaconRegion
    {

		public BeaconRegion(string identifier, string proximityUUID)
            : this(identifier, Java.Util.UUID.FromString (proximityUUID), null, null)
        {
            
        }

		public BeaconRegion(string identifier, string proximityUUID, int major)
            : this(identifier, Java.Util.UUID.FromString (proximityUUID), new Java.Lang.Integer(major), null)
        {

        }

		public BeaconRegion(string identifier, string proximityUUID, int major, int minor)
            : this(identifier, Java.Util.UUID.FromString (proximityUUID), new Java.Lang.Integer(major), new Java.Lang.Integer(minor))
        {

        }

        public int? Major {
            get {
                var major = this._MajorInternal ();

                if (major == null)
                    return null;
                
                return major.IntValue ();
            }
        }

        public int? Minor {
            get {
                var minor = this._MinorInternal ();

                if (minor == null)
                    return null;
                
                return minor.IntValue ();
            }
        }
    }
}


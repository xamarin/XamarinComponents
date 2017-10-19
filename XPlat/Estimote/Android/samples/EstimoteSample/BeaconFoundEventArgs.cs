using System;

using EstimoteSdk.Recognition.Packets;

namespace Estimotes.Droid
{
    class BeaconFoundEventArgs : EventArgs
    {
        public BeaconFoundEventArgs(Beacon beacon)
        {
            FoundBeacon = beacon;
        }

        public Beacon FoundBeacon { get; private set; }
    }
}

using System;
using Android.Runtime;
using Android.App;
using Android.OS;
using Android.Content;

namespace Mapbox.Telemetry
{
    [Register ("com/mapbox/mapboxsdk/telemetry/TelemetryService", DoNotGenerateAcw=true)]
    [Service (Name="com.mapbox.mapboxsdk.telemetry.TelemetryService")]
    internal class _InternalTelemetryService : Service
    {
        public override IBinder OnBind (Intent intent)
        {
            return default (IBinder);
        }
    }
}


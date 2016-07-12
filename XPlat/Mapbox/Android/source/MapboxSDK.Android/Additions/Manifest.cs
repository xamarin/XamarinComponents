using System;
using Android.Runtime;
using Android.App;
using Android.OS;
using Android.Content;

// Mapzen Lost
[assembly: UsesPermission (Android.Manifest.Permission.AccessCoarseLocation)]
[assembly: UsesPermission (Android.Manifest.Permission.AccessFineLocation)]
[assembly: UsesFeature ("android.hardware.sensor.accelerometer", Required = false)]
[assembly: UsesFeature ("android.hardware.location")]
[assembly: UsesFeature ("android.hardware.location.gps")]
[assembly: UsesFeature ("android.hardware.telephony", Required = false)]
[assembly: UsesFeature ("android.hardware.wifi")]

// Mapbox
[assembly: UsesFeature (GLESVersion=0x00020000, Required=true)]
[assembly: UsesPermission (Android.Manifest.Permission.Internet)]
[assembly: UsesPermission (Android.Manifest.Permission.AccessNetworkState)]
[assembly: UsesPermission (Android.Manifest.Permission.AccessWifiState)]

namespace Mapbox.Telemetry
{
    [Register ("com/mapbox/mapboxsdk/telemetry/TelemetryService", DoNotGenerateAcw = true)]
    [Service (Name = "com.mapbox.mapboxsdk.telemetry.TelemetryService")]
    partial class TelemetryService
    {
    }
}

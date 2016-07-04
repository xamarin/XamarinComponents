using Android;
using Android.App;

[assembly: UsesPermission(Manifest.Permission.Bluetooth)]
[assembly: UsesPermission(Manifest.Permission.BluetoothAdmin)]
[assembly: UsesFeature("android.hardware.bluetooth_le", Required = false)]

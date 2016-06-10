using System;
using Android.App;

[assembly: UsesFeature (GLESVersion=0x00020000, Required=true)]
[assembly: UsesPermission (Android.Manifest.Permission.Nfc)]
[assembly: UsesPermission (Android.Manifest.Permission.ReadExternalStorage)]
[assembly: UsesPermission (Android.Manifest.Permission.WriteExternalStorage)]

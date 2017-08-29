# Google ARCore Binding for Xamarin.Android

With ARCore, shape brand new experiences that seamlessly blend the digital and physical worlds. Transform the future of work and play at Android scale.

[Google ARCore - More Information](https://developers.google.com/ar/)

## Building

Use the `build.cake` script to build.  You can execute it via one of the bootstrappers (`build.sh` on mac or `build.ps1` on windows):

Mac:
```
sh ../../build.sh --target libs
```

Windows:
```
powershell ..\..\build.ps1 -Target libs
```

## Install the ARCore Services app

The ARCore Service app includes the Android system level service which is required for ARCore capabilities.

[Download](https://github.com/google-ar/arcore-android-sdk/releases/download/sdk-preview/arcore-preview.apk) the ARCore Service and install it by running the following adb command:

```
adb install -r -d arcore-preview.apk
```


# Xamarin Exposure Notification

![Nuget](https://img.shields.io/nuget/v/Xamarin.ExposureNotification?label=Cross-Platform)
![Nuget](https://img.shields.io/nuget/v/Xamarin.GooglePlayServices.Nearby.ExposureNotification?label=Android)
![Nuget](https://img.shields.io/nuget/v/Xamarin.iOS.ExposureNotification?label=iOS)

Apple and Google are both creating API’s for a compatible BLE based Contact Tracing implementation which relies heavily on generating and storing rolling unique identifiers on a device, which are broadcast to nearby devices.  Devices which detect nearby identifiers then store these identifiers as they come into range (or contact) with for up to 14 days.

When a person has confirmed a diagnosis, they tell their device which then submits the locally stored, self-generated rolling unique identifiers from the last 14 days to a back end service provided by the app implementing the API.

Devices continually request the keys submitted by diagnosed people from the backend server.  The device then compares these keys to the unique identifiers of other devices it has been near in the last 14 days.

## Xamarin.ExposureNotification

This project contains the cross platform wrapper API around the native Android and iOS API's.  The sample app uses this library to implement the exposure notification code one time for both platforms.

## Bindings to Native APIs _(NuGet)_

We also have NuGet packages available with bindings to the native Android and iOS Exposure Notifaction API's

 - iOS: [Xamarin.iOS.ExposureNotification](https://www.nuget.org/packages/Xamarin.iOS.ExposureNotification/) (Requires XCode 11.5 beta1 or newer)
 - Android: [Xamarin.GooglePlayServices.Nearby.ExposureNotification](https://www.nuget.org/packages/Xamarin.GooglePlayServices.Nearby.ExposureNotification/)

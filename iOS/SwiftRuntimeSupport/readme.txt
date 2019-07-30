/////////////////////////////////////
// Xamarin.iOS.SwiftRuntimeSupport //
/////////////////////////////////////

This NuGet has the intention to support Xamarin apps that uses iOS components
with some dependency on Swift. In general, this NuGet will scan all frameworks
used by the app, sign and copy the necessary Swift libraries in the right place
when building your app or when generating the archive of the app (not the IPA).

This NuGet, does not copy the Swift libraries in the right place when generating
the IPA using Visual Studio in any way, causing the rejection of the App Store
when uploading the IPA.

To be able to generate a correct IPA of a Xamarin app with some dependency on
Swift, you will need to use the Xcode IPA wizard. To do so, please, follow
these steps:

1. In Visual Studio, select a valid iOS device before archiving.
2. Go to *Build* menu / *Archive for Publishing*
3. Once done, open Xcode and go to *Window* / *Organizer*
4. Select the *Archives* tab
5. On the left side of the window, select your app
6. Click on *Distribute App* button and follow the wizard 

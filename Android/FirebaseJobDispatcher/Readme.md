
Xamarin.Android binding for [Firebase Job Dispatcher][1]. The Firebase Job Dispatcher is a library from Google that provides a fluent API to simplify scheduling background work. It uses a JobScheduler-like API and is intended to be the replacement for Google Cloud Manager.

Prebuilt versions of this binding are available on [NuGet.org][2].

Documentation for using this binding may be found at the [Xamarin Developer Portal][3].

## Command line aide-mémoire:

This section assumes that a Mac is being used to build the application. Ensure that XCode (with the Command Line tools) and Visual Studio for Mac are installed.

* To fetch the Firebase Job Dispatcher .AAR &ndash; `../../build.sh --target externals`
* To build to Xamarin.Android binding &ndash; `../../build.sh --target libs`
* To build the NuGet package &ndash; `../../build.sh --target nuget`

[1]: https://github.com/firebase/firebase-jobdispatcher-android
[2]: https://www.nuget.org/packages/Xamarin.FirebaseJobDispatcher
[3]: https://developer.xamarin.com/guides/android/platform_features/firebase-job-dispatcher

# Open Source Components for Xamarin

[![GitHub License](https://img.shields.io/badge/license-MIT-lightgrey.svg)](https://github.com/xamarin/XamarinComponents/blob/master/LICENSE)

Open Source Components for Xamarin are a collection of open source components (including bindings and plugins) created by Xamarin and others in the community.

## Status

Please note that this repository of bindings for third-party mobile libraries is considered unsupported and unmaintained.  We have moved many of the most popular and essential bindings to other repositories where they will be supported: https://github.com/xamarin/XamarinComponents/pull/1417.

Existing bindings published from this repository will continue to be available on NuGet, but will not receive fixes or updates to newer versions.

For details of bindings officially supported by Microsoft, see the [Support](https://github.com/xamarin/XamarinComponents/blob/main/Support.md) page.

## Building

### Prerequisites

Before building the libraries and samples in this repository, you will need to install [.NET Core](https://dotnet.microsoft.com/download) and the [Cake .NET Core Tool](http://cakebuild.net):

```sh
dotnet tool install -g cake.tool
```

When building on macOS, you may also need to install [CocoaPods](https://cocoapods.org/):

```sh
# Homebrew
brew install cocoapods

# Ruby Gems
gem install cocoapods
```

### Compiling

You can either build all the libraries and samples in the repository from the root:

```sh
dotnet cake --name=<name-from-manifest>
```

Or, you can build each component separately:

```sh
cd <path-to-component>
dotnet cake
```

The name of each component can be [found in the manifest.yaml](https://github.com/xamarin/XamarinComponents/blob/master/manifest.yaml). For example, to build the `ARCore` component, you can either start the build from the root:

```sh
dotnet cake --name=ARCore --target=nuget
```

Or, you can navigate to the folder and run it from there:

```sh
cd Android/ARCore
dotnet cake --target=nuget
```

The following targets can be specified using the `--target=<target-name>`:

 - `libs` builds the class library bindings (depends on `externals`)
 - `externals` downloads and builds the external dependencies
 - `samples` builds all of the samples (depends on `libs`)
 - `nuget` builds the nuget packages (depends on `libs`)
 - `clean` cleans up everything


### Working in Visual Studio

Before the `.sln` files will compile in the IDEs, the external dependencies need to be downloaded. This can be done by running the `externals` target:

```sh
dotnet cake --target=externals
```

After the externals are downloaded and built, the `.sln` files should compile in your IDE.

---

## Support & Getting Help

The following libraries are [supported](Support.md). 

| Name                                  | Description                                                                      | Source                                                           |
|---------------------------------------|----------------------------------------------------------------------------------|------------------------------------------------------------------|
| AndroidX Libraries                    | Bindings for Google's AndroidX Libraries                                         | [GitHub](https://github.com/xamarin/AndroidX)                    |
| Google Play Services Client Libraries | Bindings for Google's Play Services Client Libraries                             | [GitHub](https://github.com/xamarin/GooglePlayServicesComponents)|
| Google API's for iOS                  | Bindings for Google's API's for iOS Libraries                                    | [GitHub](https://github.com/xamarin/GoogleAPIsForiOSComponents)  |
| Facebook SDK's                        | Bindings for Facebook's iOS & Android SDK's                                      | [GitHub](https://github.com/xamarin/FacebookComponents)          |
| Xamarin.Auth                          | Cross-platform API for authenticating users and storing their accounts.          | [GitHub](https://github.com/xamarin/Xamarin.Auth)                |

To get help, visit the Xamarin area of [Microsoft Q&A](https://docs.microsoft.com/en-us/answers/products/dotnet).


### Xamarin.Essentials

Xamarin.Essentials gives developers essential cross-platform APIs for their mobile applications. Xamarin.Essentials exposes over 60 native APIs in a single cross-platform package for developers to consume in their iOS, Android, UWP, or Xamarin.Forms application. Browse through the [documentation](https://docs.microsoft.com/xamarin/essentials) on how to get started today.

The repository for Xamarin.Essentials can be found at https://github.com/xamarin/Essentials. If you have any suggestions or feature requests, or if you find any issues, please open a new issue.

---


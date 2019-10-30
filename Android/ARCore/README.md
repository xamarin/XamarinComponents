# Google ARCore Binding for Xamarin.Android

With ARCore, shape brand new experiences that seamlessly blend the digital and physical worlds. Transform the future of work and play at Android scale.

[Google ARCore - More Information](https://developers.google.com/ar/)

## Building

### Prerequisites

Before building the libraries and samples in this repository, you will need to install [.NET Core](https://dotnet.microsoft.com/download) and the [Cake .NET Core Tool](http://cakebuild.net):

```sh
dotnet tool install -g cake.tool
```

### Compiling

To build the `ARCore` component, you can either start the build from the root:

```sh
dotnet cake --name=ARCore --target=libs
```

Or, you can navigate to the folder and run it from there:

```sh
cd Android/ARCore
dotnet cake --target=libs
```

The following targets can be specified using the `--target=<target-name>`:

- `externals` downloads and builds the external dependencies
- `libs` builds the class library bindings (depends on `externals`)
- `nuget` builds the nuget packages (depends on `libs`)
- `samples` builds all of the samples (depends on `nuget`)
- `clean` cleans up everything

### Working in Visual Studio

Before the `.sln` files will compile in the IDEs, the external dependencies need to be downloaded. This can be done by running the `externals` target:

```sh
dotnet cake --target=externals
```

After the externals are downloaded and built, the `.sln` files should compile in your IDE.

## Running ARCore

You can run AR apps on a [supported device](https://developers.google.com/ar/discover/#supported_devices) or an emulator.

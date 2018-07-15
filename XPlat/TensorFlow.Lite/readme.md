# Dotnet new template of the Xamarin Components team

This is folder structure used by Xamarin Components team:


*   source
    
    bindings libraries and/or cross platform libraries with extensions

    NOTE: assembly-names and namespaces are prepared for nuget bait-n-switch

*   samples

    Samples for the libraries

*   tests

    *   unit tests

    *   ui tests




## Structure

    ├── External-Dependency-Info.txt
    ├── LICENSE.md
    ├── build.cake
    ├── docs
    ├── nuget
    │   └── HolisticWare.Xamarin.TensorFlow.Lite.nuspec
    ├── readme.md
    ├── samples
    │   ├── Xamarin.TensorFlow.Lite.Sample.XamarinAndroid
    │   │   ├── Assets
    │   │   │   └── AboutAssets.txt
    │   │   ├── MainActivity.cs
    │   │   ├── Properties
    │   │   │   ├── AndroidManifest.xml
    │   │   │   └── AssemblyInfo.cs
    │   │   ├── Resources
    │   │   │   ├── AboutResources.txt
    │   │   │   ├── Resource.designer.cs
    │   │   │   ├── drawable
    │   │   │   ├── layout
    │   │   │   │   └── Main.axml
    │   │   │   ├── mipmap-hdpi
    │   │   │   │   └── Icon.png
    │   │   │   ├── mipmap-mdpi
    │   │   │   │   └── Icon.png
    │   │   │   ├── mipmap-xhdpi
    │   │   │   │   └── Icon.png
    │   │   │   ├── mipmap-xxhdpi
    │   │   │   │   └── Icon.png
    │   │   │   ├── mipmap-xxxhdpi
    │   │   │   │   └── Icon.png
    │   │   │   └── values
    │   │   │       └── Strings.xml
    │   │   ├── Xamarin.TensorFlow.Lite.Sample.XamarinAndroid.csproj
    │   │   ├── Xamarin.TensorFlow.Lite.Sample.XamarinAndroid.csproj.user
    │   │   └── packages.config
    │   ├── Xamarin.TensorFlow.Lite.Sample.XamarinIOS
    │   │   ├── AppDelegate.cs
    │   │   ├── Assets.xcassets
    │   │   │   ├── AppIcon.appiconset
    │   │   │   │   └── Contents.json
    │   │   │   ├── Contents.json
    │   │   │   ├── First.imageset
    │   │   │   │   ├── Contents.json
    │   │   │   │   └── vector.pdf
    │   │   │   └── Second.imageset
    │   │   │       ├── Contents.json
    │   │   │       └── vector.pdf
    │   │   ├── Entitlements.plist
    │   │   ├── FirstViewController.cs
    │   │   ├── FirstViewController.designer.cs
    │   │   ├── Info.plist
    │   │   ├── LaunchScreen.storyboard
    │   │   ├── Main.cs
    │   │   ├── Main.storyboard
    │   │   ├── Resources
    │   │   ├── SecondViewController.cs
    │   │   ├── SecondViewController.designer.cs
    │   │   ├── Xamarin.TensorFlow.Lite.Sample.XamarinIOS.csproj
    │   │   └── packages.config
    │   └── Xamarin.TensorFlow.Lite.Samples.sln
    ├── source
    │   ├── Xamarin.TensorFlow.Lite.Bindings.NetStandard10
    │   │   └── Xamarin.TensorFlow.Lite.Bindings.NetStandard10.csproj
    │   ├── Xamarin.TensorFlow.Lite.Bindings.XamarinAndroid
    │   │   ├── Additions
    │   │   │   └── AboutAdditions.txt
    │   │   ├── Jars
    │   │   │   └── AboutJars.txt
    │   │   ├── Properties
    │   │   │   └── AssemblyInfo.cs
    │   │   ├── Transforms
    │   │   │   ├── EnumFields.xml
    │   │   │   ├── EnumMethods.xml
    │   │   │   └── Metadata.xml
    │   │   └── Xamarin.TensorFlow.Lite.Bindings.XamarinAndroid.csproj
    │   ├── Xamarin.TensorFlow.Lite.Bindings.XamarinIOS
    │   │   ├── ApiDefinition.cs
    │   │   ├── Properties
    │   │   │   └── AssemblyInfo.cs
    │   │   ├── Structs.cs
    │   │   └── Xamarin.TensorFlow.Lite.Bindings.XamarinIOS.csproj
    │   ├── Xamarin.TensorFlow.Lite.NetStandard10
    │   │   └── Xamarin.TensorFlow.Lite.NetStandard10.csproj
    │   ├── Xamarin.TensorFlow.Lite.Source.sln
    │   ├── Xamarin.TensorFlow.Lite.XamarinAndroid
    │   │   ├── Properties
    │   │   │   └── AssemblyInfo.cs
    │   │   ├── Resources
    │   │   │   ├── AboutResources.txt
    │   │   │   ├── Resource.designer.cs
    │   │   │   └── values
    │   │   │       └── Strings.xml
    │   │   └── Xamarin.TensorFlow.Lite.XamarinAndroid.csproj
    │   └── Xamarin.TensorFlow.Lite.XamarinIOS
    │       ├── Properties
    │       │   └── AssemblyInfo.cs
    │       ├── Resources
    │       └── Xamarin.TensorFlow.Lite.XamarinIOS.csproj
    └── tests
        ├── ui-tests
        │   ├── Xamarin.TensorFlow.Lite.Sample.XamarinAndroid.UITests
        │   │   ├── Tests.cs
        │   │   ├── Xamarin.TensorFlow.Lite.Sample.XamarinAndroid.UITests.csproj
        │   │   └── packages.config
        │   ├── Xamarin.TensorFlow.Lite.Sample.XamarinIOS.UITests
        │   │   ├── Tests.cs
        │   │   ├── Xamarin.TensorFlow.Lite.Sample.XamarinIOS.UITests.csproj
        │   │   └── packages.config
        │   └── Xamarin.TensorFlow.Lite.UITests.sln
        └── unit-tests
            └── Xamarin.TensorFlow.Lite.UnitTests.sln

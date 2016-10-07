
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

// var ANDROID_VERSION = "4.1.1";
// var ANDROID_NUGET_VERSION = "4.1.1.0";
var IOS_VERSION = "0.4.2";
var IOS_NUGET_VERSION = "0.4.2";

// var MAPBOX_VERSION = "4.1.1";
// var MAPBOX_ANDROID = string.Format ("http://search.maven.org/remotecontent?filepath=com/mapbox/mapboxsdk/mapbox-android-sdk/{0}/mapbox-android-sdk-{0}.aar", MAPBOX_VERSION);

var PODFILE = new List<string> {
	"platform :ios, '8.0'",
	"install! 'cocoapods', :integrate_targets => false",
	"target 'Xamarin' do",
	string.Format ("  pod 'AppAuth', '{0}'", IOS_VERSION),
	"end",
};

var buildSpec = new BuildSpec {
	Libs = new [] {
		new IOSSolutionBuilder {
			SolutionPath = "./iOS/source/OpenId.AppAuth.iOS.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./iOS/source/OpenId.AppAuth.iOS/bin/Release/OpenId.AppAuth.iOS.dll",
				}
			}
		},
		// new DefaultSolutionBuilder {
		// 	SolutionPath = "./Android/source/MapboxSDK.Android.sln",
		// 	OutputFiles = new [] { 
		// 		new OutputFileCopy {
		// 			FromFile = "./Android/source/MapboxSDK.Android/bin/Release/MapboxSDK.Android.dll",
		// 		}
		// 	}
		// }
	},

	NuGets = new [] {
		// new NuGetInfo { NuSpec = "./nuget/Xamarin.MapboxSDK.Android.nuspec", Version = ANDROID_NUGET_VERSION },
		new NuGetInfo { NuSpec = "./nuget/OpenId.AppAuth.iOS.nuspec", Version = IOS_NUGET_VERSION },
	},

	Samples = new [] {
		new IOSSolutionBuilder { SolutionPath = "./iOS/samples/OpenIdAuthSampleiOS.sln", Configuration = "Release|iPhone" },
		// new DefaultSolutionBuilder { SolutionPath = "./Android/samples/MapboxSampleAndroid.sln" }
	},

	Components = new [] {
		new Component { ManifestDirectory = "./iOS/component" }
		// new Component { ManifestDirectory = "./Android/component" }
	}
};

// Task ("externals-android")
// 	.WithCriteria (!FileExists ("./externals/android/mapbox-android-sdk.aar"))
// 	.Does (() => 
// {
// 	EnsureDirectoryExists ("./externals/android");

// 	DownloadFile (MAPBOX_ANDROID, "./externals/android/temp.aar");

// 	Unzip ("./externals/android/temp.aar", "./externals/android/temp");
// 	DeleteDirectory ("./externals/android/temp/jni/mips", true);
// 	Zip ("./externals/android/temp", "./externals/android/mapbox-android-sdk.aar");
// 	DeleteDirectory ("./externals/android/temp", true);
// });
Task ("externals-ios")
	.WithCriteria (!FileExists ("./externals/ios/libAppAuth.a"))
	.Does (() => 
{
	if (CocoaPodVersion (new CocoaPodSettings ()) < new System.Version (1, 0))
		PODFILE.RemoveAt (1);

	EnsureDirectoryExists ("./externals/ios");

	FileWriteLines ("./externals/ios/Podfile", PODFILE.ToArray ());

	CocoaPodRepoUpdate ();
	
	CocoaPodInstall ("./externals/ios", new CocoaPodInstallSettings { NoIntegrate = true });

	XCodeBuild (new XCodeBuildSettings {
		Project = "./externals/ios/Pods/Pods.xcodeproj",
		Target = "AppAuth",
		Sdk = "iphoneos",
		Configuration = "Release",
	});

	XCodeBuild (new XCodeBuildSettings {
		Project = "./externals/ios/Pods/Pods.xcodeproj",
		Target = "AppAuth",
		Sdk = "iphonesimulator",
		Configuration = "Release",
	});

	RunLipoCreate ("./", 
		"./externals/ios/libAppAuth.a",
		"./externals/ios/build/Release-iphoneos/AppAuth/libAppAuth.a",
		"./externals/ios/build/Release-iphonesimulator/AppAuth/libAppAuth.a");
});
Task ("externals")
	// .IsDependentOn ("externals-android")
	.IsDependentOn ("externals-ios");

Task ("clean").IsDependentOn ("clean-base").Does (() => 
{
	if (DirectoryExists ("./externals"))
		DeleteDirectory ("./externals", true);
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);
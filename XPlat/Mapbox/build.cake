
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var ANDROID_VERSION = "4.1.1";
var ANDROID_NUGET_VERSION = "4.1.1.0";
var IOS_VERSION = "3.3.3";
var IOS_NUGET_VERSION = "3.3.3";

var MAPBOX_VERSION = "4.1.1";
var LOST_VERSION = "1.1.0";

var LOST_AAR = string.Format ("http://search.maven.org/remotecontent?filepath=com/mapzen/android/lost/{0}/lost-{0}.aar", LOST_VERSION);
var MAPBOX_ANDROID = string.Format ("http://search.maven.org/remotecontent?filepath=com/mapbox/mapboxsdk/mapbox-android-sdk/{0}/mapbox-android-sdk-{0}.aar", MAPBOX_VERSION);

var PODFILE = new List<string> {
	"platform :ios, '8.0'",
	"install! 'cocoapods', :integrate_targets => false",
	"target 'Xamarin' do",
	"  pod 'Mapbox-iOS-SDK', '3.2.3'",
	"  use_frameworks!",
	"end",
};

var buildSpec = new BuildSpec {
	Libs = new [] {
		new IOSSolutionBuilder {
			SolutionPath = "./iOS/source/MapboxSDK.iOS.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./iOS/source/MapboxSDK.iOS/bin/Release/MapboxSDK.iOS.dll",
				}
			}
		},
		new DefaultSolutionBuilder {
			SolutionPath = "./Android/source/MapboxSDK.Android.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./Android/source/MapboxSDK.Android/bin/Release/MapboxSDK.Android.dll",
				}
			}
		}
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.MapboxSDK.Android.nuspec", Version = ANDROID_NUGET_VERSION },
		new NuGetInfo { NuSpec = "./nuget/Xamarin.MapboxSDK.iOS.nuspec", Version = IOS_NUGET_VERSION },
	},

	Samples = new [] {
		new IOSSolutionBuilder { SolutionPath = "./iOS/samples/MapboxSampleiOS.sln" },	
		new DefaultSolutionBuilder { SolutionPath = "./Android/samples/MapboxSampleAndroid.sln" }
	},

	Components = new [] {
		new Component { ManifestDirectory = "./component" }
	}
};

Task ("externals-android")
	.WithCriteria (!FileExists ("./externals/android/lost.jar"))
	.WithCriteria (!FileExists ("./externals/android/mapbox-android-sdk.aar"))
	.Does (() => 
{
	EnsureDirectoryExists ("./externals/android");

	DownloadFile (LOST_AAR, "./externals/android/lost.aar");
	Unzip ("./externals/android/lost.aar", "./externals/android/lost");
	CopyFile ("./externals/android/lost/classes.jar", "./externals/android/lost.jar");

	DownloadFile (MAPBOX_ANDROID, "./externals/android/temp.aar");

	Unzip ("./externals/android/temp.aar", "./externals/android/temp");
	DeleteDirectory ("./externals/android/temp/jni/mips", true);
	Zip ("./externals/android/temp", "./externals/android/mapbox-android-sdk.aar");
	DeleteDirectory ("./externals/android/temp", true);
});
Task ("externals-ios")
	.WithCriteria (!DirectoryExists ("./externals/ios/Pods/Mapbox-iOS-SDK"))
	.Does (() => 
{
	if (CocoaPodVersion (new CocoaPodSettings ()) < new System.Version (1, 0))
		PODFILE.RemoveAt (1);

	EnsureDirectoryExists ("./externals/ios");

	FileWriteLines ("./externals/ios/Podfile", PODFILE.ToArray ());

	CocoaPodRepoUpdate ();
	
	CocoaPodInstall ("./externals/ios", new CocoaPodInstallSettings { NoIntegrate = true });
});
Task ("externals").IsDependentOn ("externals-android").IsDependentOn ("externals-ios");


Task ("clean").IsDependentOn ("clean-base").Does (() => 
{
	if (DirectoryExists ("./externals"))
		DeleteDirectory ("./externals", true);
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);
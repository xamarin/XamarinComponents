
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var ANDROID_VERSION = "3.3.9";
var ANDROID_NUGET_VERSION = "3.3.9";
var IOS_VERSION = "3.6.0";
var IOS_NUGET_VERSION = "3.6.0";

var ANDROID_SDK_URL = string.Format ("http://wikifiles.gigya.com/SDKs/Android/AndroidSDK_{0}.zip", ANDROID_VERSION);
var IOS_SDK_URL = string.Format ("https://s3.amazonaws.com/wikifiles.gigya.com/SDKs/iPhone/{0}/GigyaSDK.framework_{0}.zip", IOS_VERSION);

var buildSpec = new BuildSpec {
	Libs = new [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./iOS/source/GigyaSDK.iOS.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./iOS/source/GigyaSDK.iOS/bin/Release/GigyaSDK.iOS.dll",
				}
			}
		},
		new DefaultSolutionBuilder {
			SolutionPath = "./Android/source/GigyaSDK.Android.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./Android/source/GigyaSDK.Android/bin/Release/GigyaSDK.Android.dll",
				}
			}
		}
	},

	// NuGets = new [] {
	// 	new NuGetInfo { NuSpec = "./nuget/Xamarin.GigyaSDK.Android.nuspec", Version = ANDROID_NUGET_VERSION },
	// 	new NuGetInfo { NuSpec = "./nuget/Xamarin.GigyaSDK.iOS.nuspec", Version = IOS_NUGET_VERSION },
	// },

	Samples = new [] {
		new IOSSolutionBuilder { SolutionPath = "./iOS/samples/GigyaSDKSampleiOS.sln",  Configuration = "Release", Platform="iPhone" },
		new DefaultSolutionBuilder { SolutionPath = "./Android/samples/GigyaSDKSampleAndroid.sln" }
	},

	// Components = new [] {
	// 	new Component { ManifestDirectory = "./component" }
	// }
};

Task ("externals-android")
	.WithCriteria (!FileExists ("./externals/android/gigya.jar"))
	.Does (() => 
{
	EnsureDirectoryExists ("./externals/android");

	DownloadFile (ANDROID_SDK_URL, "./externals/android/sdk.zip");
	Unzip ("./externals/android/sdk.zip", "./externals/android/sdk");
	CopyFile ("./externals/android/sdk/" + ANDROID_VERSION + "/gigya-sdk-" + ANDROID_VERSION + ".jar", "./externals/android/gigya.jar");

	// depends on:
	//  - package id="Xamarin.Facebook.Android" version="4.16.1"
	//  - package id="Xamarin.GooglePlayServices.Auth" version="32.961.0"
	//  - package id="Xamarin.Android.Support.v4" version="24.2.1"
});
Task ("externals-ios")
	.WithCriteria (!FileExists ("./externals/ios/sdk/GigyaSDK.framework/GigyaSDK"))
	.Does (() => 
{
	EnsureDirectoryExists ("./externals/ios");

	DownloadFile (IOS_SDK_URL, "./externals/ios/sdk.zip");
	Unzip ("./externals/ios/sdk.zip", "./externals/ios/sdk");
});
Task ("externals").IsDependentOn ("externals-android").IsDependentOn ("externals-ios");


Task ("clean").IsDependentOn ("clean-base").Does (() => 
{
	if (DirectoryExists ("./externals"))
		DeleteDirectory ("./externals", true);
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

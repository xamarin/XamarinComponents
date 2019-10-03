#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var IOS_VERSION = "2.0.4";
var IOS_NUGET_VERSION = IOS_VERSION;
var IOS_URL = $"https://github.com/Azure/azure-notificationhubs-ios/releases/download/{IOS_VERSION}/WindowsAzureMessaging.framework.zip";

var ANDROID_VERSION = "0.5";
var ANDROID_NUGET_VERSION = "0.5.0";
var ANDROID_URL = string.Format ("https://dl.bintray.com/microsoftazuremobile/SDK/com/microsoft/azure/notification-hubs-android-sdk/{0}/notification-hubs-android-sdk-{0}.aar", ANDROID_VERSION);

var buildSpec = new BuildSpec {

	Libs = new [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./iOS/source/Xamarin.Azure.NotificationHubs.iOS.sln",
			OutputFiles = new [] { 
				new OutputFileCopy { FromFile = "./iOS/source/bin/unified/Release/Xamarin.Azure.NotificationHubs.iOS.dll" },
			}
		},
		new DefaultSolutionBuilder {
			SolutionPath = "./Android/source/Xamarin.Azure.NotificationHubs.Android.sln",
			OutputFiles = new [] { 
				new OutputFileCopy { FromFile = "./Android/source/bin/Release/Xamarin.Azure.NotificationHubs.Android.dll" },
			}
		},
	},

	Samples = new [] {
		new IOSSolutionBuilder { SolutionPath = "./iOS/samples/NotificationHubsSampleiOS.sln",  Configuration = "Release", Platform="iPhone" },
		new DefaultSolutionBuilder { SolutionPath = "./Android/samples/NotificationHubsSampleAndroid.sln" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Azure.NotificationHubs.iOS.nuspec", Version = IOS_NUGET_VERSION, RequireLicenseAcceptance = true  },
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Azure.NotificationHubs.Android.nuspec", Version = ANDROID_NUGET_VERSION , RequireLicenseAcceptance = true },
	},

	Components = new [] {
		new Component { ManifestDirectory = "./component" },
	}
};

Task ("externals-ios")
	.WithCriteria (!FileExists ("./iOS/externals/sdk.zip"))
	.Does (() => 
{
	EnsureDirectoryExists ("./iOS/externals");

	DownloadFile (IOS_URL, "./iOS/externals/sdk.zip");

	Unzip ("./iOS/externals/sdk.zip", "./iOS/externals");
});
Task ("externals-android")
	.WithCriteria (!FileExists ("./externals/Android/notificationhubs.aar"))
	.Does (() => 
{
	EnsureDirectoryExists ("./Android/externals");

	DownloadFile (ANDROID_URL, "./Android/externals/notificationhubs.aar");
});
Task ("externals").IsDependentOn ("externals-ios").IsDependentOn ("externals-android");

Task ("clean").IsDependentOn ("clean-base").Does (() => 
{
	if (DirectoryExists ("./Android/externals"))
		DeleteDirectory ("./Android/externals", true);

	if (DirectoryExists ("./iOS/externals"))
		DeleteDirectory ("./iOS/externals", true);
});


SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

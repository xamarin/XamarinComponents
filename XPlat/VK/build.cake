
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var ANDROID_VERSION = "1.6.7";
var ANDROID_NUGET_VERSION = "1.6.7";
var IOS_VERSION = "1.3.16";
var IOS_NUGET_VERSION = "1.3.16";

var AAR_URL = string.Format ("http://search.maven.org/remotecontent?filepath=com/vk/androidsdk/{0}/androidsdk-{0}.aar", ANDROID_VERSION);

var PODFILE = new List<string> {
	"platform :ios, '6.0'",
	"install! 'cocoapods', :integrate_targets => false",
	"target 'Xamarin' do",
	string.Format ("  pod 'VK-ios-sdk', '{0}'", IOS_VERSION),
	"end",
};

var buildSpec = new BuildSpec {
	Libs = new [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./iOS/source/VKontakte.iOS.sln",
			OutputFiles = new [] { 
				new OutputFileCopy { FromFile = "./iOS/source/VKontakte.iOS/bin/Release/VKontakte.iOS.dll", }
			}
		},
		new DefaultSolutionBuilder {
			SolutionPath = "./Android/source/VKontakte.Android.sln",
			OutputFiles = new [] { 
				new OutputFileCopy { FromFile = "./Android/source/VKontakte.Android/bin/Release/VKontakte.Android.dll", }
			}
		}
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/VKontakte.Android.nuspec", Version = ANDROID_NUGET_VERSION },
		new NuGetInfo { NuSpec = "./nuget/VKontakte.iOS.nuspec", Version = IOS_NUGET_VERSION },
		new NuGetInfo { NuSpec = "./nuget/VKontakte.nuspec" },
	},

	Samples = new [] {
		new IOSSolutionBuilder { SolutionPath = "./iOS/samples/VKontakteSampleiOS.sln" },
		new DefaultSolutionBuilder { SolutionPath = "./Android/samples/VKontakteSampleAndroid.sln" }
	},

	// Components = new [] {
	// 	new Component { ManifestDirectory = "./iOS/component" }
	// 	new Component { ManifestDirectory = "./Android/component" }
	// }
};

Task ("externals-android")
	.WithCriteria (!FileExists ("./externals/android/vk.aar"))
	.Does (() => 
{
	EnsureDirectoryExists ("./externals/android");

	DownloadFile (AAR_URL, "./externals/android/vk.aar");
});
Task ("externals-ios")
	.WithCriteria (!FileExists ("./externals/ios/libVK-ios-sdk.a"))
	.Does (() => 
{
	if (CocoaPodVersion (new CocoaPodSettings ()) < new System.Version (1, 0))
		PODFILE.RemoveAt (1);

	EnsureDirectoryExists ("./externals/ios");

	FileWriteLines ("./externals/ios/Podfile", PODFILE.ToArray ());

	CocoaPodRepoUpdate ();
	
	CocoaPodInstall ("./externals/ios", new CocoaPodInstallSettings { NoIntegrate = true });

	BuildXCodeFatLibrary ("./Pods/Pods.xcodeproj", "VK-ios-sdk", "VK-ios-sdk", null, "./externals/ios/", "VK-ios-sdk");
});
Task ("externals")
	.IsDependentOn ("externals-android")
	.IsDependentOn ("externals-ios");

Task ("clean").IsDependentOn ("clean-base").Does (() => 
{
	if (DirectoryExists ("./externals"))
		DeleteDirectory ("./externals", true);
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

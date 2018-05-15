#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var NUGET_VERSION = "2.2.0";

// compile 'com.android.support:wear:27.0.0' (From Support libs)
// compile 'com.google.android.support:wearable:2.1.0' (EmbeddedJar)
// provided 'com.google.android.wearable:wearable:2.1.0' //ReferenceJar
var SUPPORT_WEARABLE_VERSION = "2.2.0";
var SUPPORT_WEARABLE_URL = string.Format("https://dl.google.com/dl/android/maven2/com/google/android/support/wearable/{0}/wearable-{0}.aar", SUPPORT_WEARABLE_VERSION);
var WEARABLE_WEARABLE_VERSION = "2.2.0";
var WEARABLE_WEARABLE_URL = string.Format("https://dl.google.com/dl/android/maven2/com/google/android/wearable/wearable/{0}/wearable-{0}.jar", WEARABLE_WEARABLE_VERSION);

var buildSpec = new BuildSpec () {
	Libs = new [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./Android.Wear.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/bin/Release/Xamarin.Android.Wear.dll",
				}
			}
		}
	},

	Samples = new [] {
		new DefaultSolutionBuilder { SolutionPath = "./samples/WatchFaceSample.sln" },
		new DefaultSolutionBuilder { SolutionPath = "./samples/MultiPageSample.sln" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Android.Wear.nuspec", Version = NUGET_VERSION },
	},
};

Task ("externals")
	.Does (() => 
{
	if (!DirectoryExists ("./externals/"))
		CreateDirectory ("./externals");

	if (!FileExists ("./externals/support_wearable.aar")) {
		DownloadFile (SUPPORT_WEARABLE_URL, "./externals/support_wearable.aar");
		Unzip("./externals/support_wearable.aar", "./externals/support_wearable/");
	}

	if (!FileExists ("./externals/wearable_wearable.jar"))
		DownloadFile (WEARABLE_WEARABLE_URL, "./externals/wearable_wearable.jar");
});

Task ("clean")
	.IsDependentOn ("clean-base")
	.Does (() => 
{	
	if (DirectoryExists ("./externals"))
		DeleteDirectory ("./externals", true);
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

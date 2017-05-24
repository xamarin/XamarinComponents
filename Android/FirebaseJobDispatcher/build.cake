
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var ANDROID_VERSION = "0.6.0";
var ANDROID_NUGET_VERSION = "0.6.0.0-beta2";
var ANDROID_URL = string.Format ("https://jcenter.bintray.com/com/firebase/firebase-jobdispatcher/{0}/firebase-jobdispatcher-{0}.aar", ANDROID_VERSION);
var ANDROID_FILE = "firebase-dispatcher.aar";

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			BuildsOn = BuildPlatforms.Mac | BuildPlatforms.Windows,
			SolutionPath = "./source/Firebase.JobDispatcher.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/Firebase.JobDispatcher/bin/Release/Xamarin.Firebase.JobDispatcher.dll",
				}
			}
		}
	},

	Samples = new ISolutionBuilder [] {
		new DefaultSolutionBuilder { SolutionPath = "./samples/FJDTestApp.sln" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Firebase.JobDispatcher.nuspec", Version = ANDROID_NUGET_VERSION },
	},
};

Task ("externals")
	.Does (() => 
{
	if (!DirectoryExists ("./externals/"))
		CreateDirectory ("./externals/");
		
	DownloadFile (ANDROID_URL, "./externals/" + ANDROID_FILE);
});

Task ("clean").IsDependentOn ("clean-base").Does (() => 
{	
	DeleteFiles ("./externals/*.aar");
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

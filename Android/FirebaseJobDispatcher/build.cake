
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "ci"));

var ANDROID_VERSION = "0.8.5";
var ANDROID_NUGET_VERSION = "0.8.5";
var ANDROID_URL = $"https://jcenter.bintray.com/com/firebase/firebase-jobdispatcher/{ANDROID_VERSION}/firebase-jobdispatcher-{ANDROID_VERSION}.aar";
var ANDROID_FILE = "firebase-dispatcher.aar";

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			BuildsOn = BuildPlatforms.Mac | BuildPlatforms.Windows,
			SolutionPath = "./Xamarin.Firebase.JobDispatcher.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/Firebase.JobDispatcher/bin/Release/Xamarin.Firebase.JobDispatcher.dll",
				}
			}
		}
	},

	Samples = new ISolutionBuilder [] {
		new DefaultSolutionBuilder { SolutionPath = "./Xamarin.Firebase.JobDispatcher.sln" },
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
		
	if (IsRunningOnWindows())
	{
		var fileDownload = "./externals/" + "Temp_" + ANDROID_FILE;
		DownloadFile (ANDROID_URL, fileDownload);

		var tempFolder = "./externals/firebase-dispatcher";

		if (FileExists(fileDownload))
		{ 
			Unzip(fileDownload,tempFolder);

			DeleteFiles (tempFolder + "/annotations.zip");

			var aarFile = "./externals/" +  ANDROID_FILE;

			Zip(tempFolder, aarFile);

			DeleteDirectory(tempFolder, new DeleteDirectorySettings {
				Recursive = true,
				Force = true
			});

			DeleteFile(fileDownload);
		}
	}
	else
	{
		DownloadFile (ANDROID_URL, "./externals/" + ANDROID_FILE);

		StartProcess ("/usr/bin/zip", "-d + ./externals/firebase-dispatcher.aar annotations.zip");
	}
	
	
	

	

});

Task ("clean").IsDependentOn ("clean-base").Does (() => 
{	
	DeleteFiles ("./externals/*.aar");
});

Task("ci")
	.IsDependentOn("nuget");

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

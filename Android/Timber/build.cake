
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var ANDROID_VERSION = "4.6.0";
var ANDROID_NUGET_VERSION = "4.6.0";
var ANDROID_URL = string.Format ("http://search.maven.org/remotecontent?filepath=com/jakewharton/timber/timber/{0}/timber-{0}.aar", ANDROID_VERSION);
var ANDROID_FILE = "jakewharton.timber.aar";

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/Timber.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/Timber/bin/Release/Timber.dll",
				}
			}
		}
	},

	Samples = new ISolutionBuilder [] {
		new DefaultSolutionBuilder { SolutionPath = "./samples/TimberSample.sln" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.JakeWharton.Timber.nuspec", Version = ANDROID_NUGET_VERSION },
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

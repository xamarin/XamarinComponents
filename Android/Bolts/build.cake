
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var BOLTS_VERSION = "1.4.0";

var BOLTSTASKS_URL = string.Format ("http://search.maven.org/remotecontent?filepath=com/parse/bolts/bolts-tasks/{0}/bolts-tasks-{0}.jar", BOLTS_VERSION);
var BOLTSAPPLINKS_URL = string.Format ("http://search.maven.org/remotecontent?filepath=com/parse/bolts/bolts-applinks/{0}/bolts-applinks-{0}.aar", BOLTS_VERSION);

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./Bolts.sln",
			OutputFiles = new [] {
				new OutputFileCopy { FromFile = "./source/Bolts.Tasks/bin/Release/Bolts.Tasks.dll" },
				new OutputFileCopy { FromFile = "./source/Bolts.AppLinks/bin/Release/Bolts.AppLinks.dll" },
			}
		}
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Bolts.nuspec" },
	},
};

Task ("externals")
	.WithCriteria (!FileExists ("./externals/bolts-applinks.aar"))
	.Does (() =>
{
	if (!DirectoryExists ("./externals/"))
		CreateDirectory ("./externals");

	// Download Bolts
	DownloadFile (BOLTSAPPLINKS_URL, "./externals/bolts-applinks.aar");
	DownloadFile (BOLTSTASKS_URL, "./externals/bolts-tasks.jar");
});


Task ("clean").IsDependentOn ("clean-base").Does (() =>
{
	if (DirectoryExists ("./externals/"))
		DeleteDirectory ("./externals", true);
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

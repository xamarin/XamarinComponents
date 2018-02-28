
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var JAR_VERSION = "1.2.0";
var JAR_URL = string.Format ("http://search.maven.org/remotecontent?filepath=com/daimajia/swipelayout/library/{0}/library-{0}.aar", JAR_VERSION);
var JAR_DEST = "./externals/AndroidSwipeLayout.aar";

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/AndroidSwipeLayout.sln",
			OutputFiles = new [] { 
				new OutputFileCopy { FromFile = "./source/AndroidSwipeLayout/bin/Release/AndroidSwipeLayout.dll" }
			}
		}
	},

	Samples = new ISolutionBuilder [] {
		new DefaultSolutionBuilder { SolutionPath = "./samples/AndroidSwipeLayoutSample.sln" },
	},

	Components = new [] {
		new Component { ManifestDirectory = "./component" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.AndroidSwipeLayout.nuspec" },
	},
};

Task ("externals")
	.Does (() => 
{
	if (!DirectoryExists ("./externals/"))
		CreateDirectory ("./externals");

	if (!FileExists (JAR_DEST))
		DownloadFile (JAR_URL, JAR_DEST);
});


Task ("clean").IsDependentOn ("clean-base").Does (() => 
{	
	DeleteFiles ("./externals/*.aar");
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

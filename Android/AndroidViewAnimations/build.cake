
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var JAR_VERSION = "1.1.3";
var JAR_URL = string.Format ("http://search.maven.org/remotecontent?filepath=com/daimajia/androidanimations/library/{0}/library-{0}.aar", JAR_VERSION);
var JAR_DEST = "./externals/AndroidViewAnimations.aar";

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/AndroidViewAnimations.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/AndroidViewAnimations/bin/Release/AndroidViewAnimations.dll",
				}
			}
		}
	},

	Samples = new ISolutionBuilder [] {
		new DefaultSolutionBuilder { SolutionPath = "./samples/AndroidViewAnimationsSample.sln" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/AndroidViewAnimations.nuspec" },
	},

	Components = new [] {
		new Component {ManifestDirectory = "./component"},
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

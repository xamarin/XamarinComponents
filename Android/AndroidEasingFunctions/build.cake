
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var JAR_VERSION = "1.0.2";
var JAR_URL = string.Format ("http://search.maven.org/remotecontent?filepath=com/daimajia/easing/library/{0}/library-{0}.aar", JAR_VERSION);
var JAR_DEST = "./externals/AndroidEasingFunctions.aar";

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/AndroidEasingFunctions.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/AndroidEasingFunctions/bin/Release/AndroidEasingFunctions.dll",
				}
			}
		}
	},

	Samples = new ISolutionBuilder [] {
		new DefaultSolutionBuilder { SolutionPath = "./samples/AndroidEasingFunctionsSample.sln" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/AndroidEasingFunctions.nuspec" },
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

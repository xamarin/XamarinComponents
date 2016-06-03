
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var JAR_VERSION = "1.0.5";
var JAR_URL = string.Format ("http://search.maven.org/remotecontent?filepath=com/wnafee/vector-compat/{0}/vector-compat-{0}.aar", JAR_VERSION);
var JAR_DEST = "./externals/VectorCompat.aar";

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/VectorCompat.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/VectorCompat/bin/Release/VectorCompat.dll",
				}
			}
		}
	},

	Samples = new ISolutionBuilder [] {
		new DefaultSolutionBuilder { SolutionPath = "./samples/VectorCompatSample.sln" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/VectorCompat.nuspec" },
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

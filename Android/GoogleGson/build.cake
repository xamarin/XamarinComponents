
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var JAR_VERSION = "2.8.1";
var JAR_URL = string.Format ("http://search.maven.org/remotecontent?filepath=com/google/code/gson/gson/{0}/gson-{0}.jar", JAR_VERSION);
var JAR_DEST = "./externals/gson.jar";

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/GoogleGson.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/GoogleGson/bin/Release/GoogleGson.dll",
				}
			}
		}
	},

	Samples = new ISolutionBuilder [] {
		new DefaultSolutionBuilder { SolutionPath = "./samples/GoogleGsonSample.sln" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/GoogleGson.nuspec" },
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
	DeleteFiles ("./externals/*.jar");
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

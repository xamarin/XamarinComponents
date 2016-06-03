
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var JAR_VERSION = "0.9.4";
var JAR_URL = string.Format ("http://search.maven.org/remotecontent?filepath=com/eclipsesource/minimal-json/minimal-json/{0}/minimal-json-{0}.jar", JAR_VERSION);
var JAR_DEST = "./externals/MinimalJson.jar";

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/MinimalJson.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/MinimalJson/bin/Release/MinimalJson.dll",
				}
			}
		}
	},

	Samples = new ISolutionBuilder [] {
		new DefaultSolutionBuilder { SolutionPath = "./samples/MinimalJsonSample.sln" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/MinimalJson.nuspec" },
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

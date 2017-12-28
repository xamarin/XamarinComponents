#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var JS_VERSION = "2.7.4";
var JS_URL = string.Format ("http://search.maven.org/remotecontent?filepath=com/fasterxml/jackson/core/jackson-core/{0}/jackson-core-{0}.jar", JS_VERSION);

var JAR_JS_FILE = string.Format ("./externals/jackson-core-{0}.jar", JS_VERSION);

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/Jackson.Core/Jackson.Core.sln",
			Configuration = "Release",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/Jackson.Core/bin/Release/Jackson.Core.dll",
					ToDirectory = "./output/"
				}
			}
		},
	},
	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Jackson.Core.nuspec" },
	},
};

Task ("externals")
	.WithCriteria (!FileExists (JAR_JS_FILE))
	.Does (() => 
{
	if (!DirectoryExists ("./externals/"))
		CreateDirectory ("./externals");

	DownloadFile (JS_URL, JAR_JS_FILE);
});

Task ("clean").IsDependentOn ("clean-base").Does (() => 
{
	if (DirectoryExists ("./externals/"))
		DeleteDirectory ("./externals", true);
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);


#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var NUGET_VERSION = "3.3.0";

var JAR_VERSION = "3.3.0";
var JAR_URL = string.Format ("http://search.maven.org/remotecontent?filepath=com/google/zxing/core/{0}/core-{0}.jar", JAR_VERSION);
var JAR_DEST = "./externals/zxing.core.jar";

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/GoogleZXing.sln",
			OutputFiles = new [] {
				new OutputFileCopy { FromFile = "./source/Google.ZXing.Core/bin/Release/Google.ZXing.Core.dll" }
			}
		}
	},

	Samples = new ISolutionBuilder [] {
		new DefaultSolutionBuilder { SolutionPath = "./samples/GoogleZXingSample.sln" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Google.ZXing.Core.nuspec", Version = NUGET_VERSION },
	},
};

Task ("externals")
	.Does (() => 
{
	EnsureDirectoryExists ("./externals/");

	if (!FileExists (JAR_DEST))
		DownloadFile (JAR_URL, JAR_DEST);
});


Task ("clean").IsDependentOn ("clean-base").Does (() => 
{
	DeleteFiles ("./externals/*.jar");
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

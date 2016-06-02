
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var JAR_VERSION = "0.2.1";
var JAR_URL = string.Format ("http://search.maven.org/remotecontent?filepath=me/grantland/autofittextview/{0}/autofittextview-{0}.aar", JAR_VERSION);
var JAR_DEST = "./externals/AutoFitTextView.aar";

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/AutoFitTextView.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/AutoFitTextView/bin/Release/AutoFitTextView.dll",
				}
			}
		}
	},

	Samples = new ISolutionBuilder [] {
		new DefaultSolutionBuilder { SolutionPath = "./samples/AutoFitTextViewSample.sln" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/AutoFitTextView.nuspec" },
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

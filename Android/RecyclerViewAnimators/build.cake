
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var JAR_VERSION = "2.1.0";
var JAR_URL = string.Format ("https://bintray.com/artifact/download/wasabeef/maven/jp/wasabeef/recyclerview-animators/{0}/recyclerview-animators-{0}.aar", JAR_VERSION);
var JAR_DEST = "./externals/RecyclerViewAnimators.aar";

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/RecyclerViewAnimators.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/RecyclerViewAnimators/bin/Release/RecyclerViewAnimators.dll",
				}
			}
		}
	},

	Samples = new ISolutionBuilder [] {
		new DefaultSolutionBuilder { SolutionPath = "./samples/RecyclerViewAnimatorsSample.sln" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/RecyclerViewAnimators.nuspec" },
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

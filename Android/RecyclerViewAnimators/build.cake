
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "ci"));

var JAR_VERSION = "4.0.1";
var JAR_URL = $"https://repo1.maven.org/maven2/jp/wasabeef/recyclerview-animators/{JAR_VERSION}/recyclerview-animators-{JAR_VERSION}.aar";
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

	Information($"Downloading :");
	Information($"		{JAR_URL}");
	Information($"to :");
	Information($"		{JAR_DEST}");

	if (!FileExists (JAR_DEST))
		DownloadFile (JAR_URL, JAR_DEST);
});


Task ("clean").IsDependentOn ("clean-base").Does (() => 
{	
	DeleteFiles ("./externals/*.aar");
});

Task("ci")
	.IsDependentOn("nuget");

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);


#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "ci"));

var AAR_VERSION = "1.1.5";
var AAR_URL = $"https://bintray.com/artifact/download/jlmd/maven/com/github/jlmd/AnimatedCircleLoadingView/{AAR_VERSION}/AnimatedCircleLoadingView-{AAR_VERSION}.aar";
var AAR_DEST = "./externals/AnimatedCircleLoadingView.aar";

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/AnimatedCircleLoadingView.sln",
			OutputFiles = new [] { 
				new OutputFileCopy { FromFile = "./source/AnimatedCircleLoadingView/bin/Release/AnimatedCircleLoadingView.dll" }
			}
		}
	},

	Samples = new ISolutionBuilder [] {
		new DefaultSolutionBuilder { SolutionPath = "./samples/AnimatedCircleLoadingViewSample.sln" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Android.AnimatedCircleLoadingView.nuspec" },
	},

	Components = new [] {
		new Component { ManifestDirectory = "./component" },
	},
};

Task ("externals").Does (() => 
{
	EnsureDirectoryExists ("./externals/");

	Information($"Downloading :");
	Information($"		{AAR_URL}");
	Information($"to :");
	Information($"		{AAR_DEST}");

	if (!FileExists (AAR_DEST))
	{	
		DownloadFile (AAR_URL, AAR_DEST);
	}
});


Task ("clean").IsDependentOn ("clean-base").Does (() => 
{	
	DeleteFiles ("./externals/*.aar");
});

Task("ci")
	//.IsDependentOn("nuget")
	.Does 
	(
		() => 
		{
			Warning($"Not available (moljac 2021-05-08) :");
			Information($"		{AAR_URL}");
		}
	);

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

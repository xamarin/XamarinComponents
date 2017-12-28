
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var AAR_VERSION = "1.1.5";
var AAR_URL = string.Format ("https://bintray.com/artifact/download/jlmd/maven/com/github/jlmd/AnimatedCircleLoadingView/{0}/AnimatedCircleLoadingView-{0}.aar", AAR_VERSION);
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
	if (!FileExists (AAR_DEST))
		DownloadFile (AAR_URL, AAR_DEST);
});


Task ("clean").IsDependentOn ("clean-base").Does (() => 
{	
	DeleteFiles ("./externals/*.aar");
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

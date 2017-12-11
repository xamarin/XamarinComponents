
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new IOSSolutionBuilder {
			SolutionPath = "./source/XamDialogs.sln",
			Configuration = "Release",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/XamDialogs/bin/Release/XamDialog.dll",
					ToDirectory = "./output"
				},
			}
		},	
	},

	Samples = new ISolutionBuilder [] {
		new IOSSolutionBuilder { SolutionPath = "./samples/XamDialogsSample.sln", Configuration = "Release", Platform="iPhone", BuildsOn = BuildPlatforms.Mac },
	},

	Components = new [] {
		new Component {ManifestDirectory = "./component", BuildsOn = BuildPlatforms.Mac},
	},
};

Task ("externals").IsDependentOn ("externals-base").Does (() =>
{
	RunMake ("./externals/", "all");
	BuildXCodeFatLibrary ("MBAlertView.xcodeproj", "MBAlertView", null, null, null, null);
});

Task ("clean").IsDependentOn ("clean-base").Does (() =>
{
	CleanXCodeBuild ("./externals/", null);
	RunMake ("./externals/", "clean");
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);


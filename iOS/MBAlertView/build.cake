
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new IOSSolutionBuilder {
			SolutionPath = "./source/MBAlertView.sln",
			Configuration = "Release",
			BuildsOn = BuildPlatforms.Mac,
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/MBAlertView/bin/unified/Release/AlertView.dll",
					ToDirectory = "./output/unified/"
				},
				new OutputFileCopy {
					FromFile = "./source/MBAlertView/bin/classic/Release/AlertView.dll",
					ToDirectory = "./output/classic/"
				}
			}
		},	
	},

	Samples = new ISolutionBuilder [] {
		new IOSSolutionBuilder { SolutionPath = "./samples/MBAlertViewDemo/MBAlertViewDemo.sln", BuildsOn = BuildPlatforms.Mac },
		new IOSSolutionBuilder { SolutionPath = "./samples/MBAlertViewDemo/MBAlertViewDemo-classic.sln", BuildsOn = BuildPlatforms.Mac },
	},

	Components = new [] {
		new Component {ManifestDirectory = "./component", BuildsOn = BuildPlatforms.Mac},
	},
};

Task ("externals").IsDependentOn ("externals-base").Does (() =>
{
	RunMake ("./externals/", "all");
	BuildXCodeFatLibrary ("MBAlertView.xcodeproj", "MBAlertView", null, null, null);
});

Task ("clean").IsDependentOn ("clean-base").Does (() =>
{
	CleanXCodeBuild ("./externals/", null);
	RunMake ("./externals/", "clean");
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);



#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new IOSSolutionBuilder {
			SolutionPath = "./SlackTextViewController.sln",
			Configuration = "Release",
			BuildsOn = BuildPlatforms.Mac,
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/SlackTextViewController/bin/unified/Release/SlackTextViewController.dll",
					ToDirectory = "./output/unified/"
				},
				new OutputFileCopy {
					FromFile = "./source/SlackTextViewController/bin/classic/Release/SlackTextViewController.dll",
					ToDirectory = "./output/classic/"
				}
			}
		},	
	},

	Samples = new ISolutionBuilder [] {
		new IOSSolutionBuilder { SolutionPath = "./samples/SlackTextViewControllerSample.sln", BuildsOn = BuildPlatforms.Mac },	
	},

	Components = new [] {
		new Component {ManifestDirectory = "./component", BuildsOn = BuildPlatforms.Mac},
	},
};

Task ("externals").IsDependentOn ("externals-base").Does (() =>
{
	CreatePodfile ("./externals/", "ios", "7.0", new Dictionary<string, string> {
        { "SlackTextViewController", "1.9.1" }
	});
	if (!FileExists ("./externals/Podfile.lock")) {
		CocoaPodInstall ("./externals/", new CocoaPodInstallSettings { NoIntegrate = true });
	}

	BuildXCodeFatLibrary ("Pods/Pods.xcodeproj", "SlackTextViewController", null, null, null);
});

Task ("clean").IsDependentOn ("clean-base").Does (() =>
{
	DeleteFiles ("./externals/Podfile");
	DeleteFiles ("./externals/Podfile.lock");
	CleanXCodeBuild ("./Pods/", null);
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

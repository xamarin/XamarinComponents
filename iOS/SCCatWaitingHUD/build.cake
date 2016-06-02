
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new IOSSolutionBuilder {
			SolutionPath = "./SCCatWaitingHUD.sln",
			Configuration = "Release",
			BuildsOn = BuildPlatforms.Mac,
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/SCCatWaitingHUD/bin/unified/Release/SCCatWaitingHUD.dll",
					ToDirectory = "./output/unified/"
				},
				new OutputFileCopy {
					FromFile = "./source/SCCatWaitingHUD/bin/classic/Release/SCCatWaitingHUD.dll",
					ToDirectory = "./output/classic/"
				}
			}
		},	
	},

	Samples = new ISolutionBuilder [] {
		new IOSSolutionBuilder { SolutionPath = "./samples/SCCatWaitingHUDSample.sln", BuildsOn = BuildPlatforms.Mac },
	},

	Components = new [] {
		new Component {ManifestDirectory = "./component", BuildsOn = BuildPlatforms.Mac},
	},
};

Task ("externals").IsDependentOn ("externals-base").Does (() =>
{
	CreatePodfile ("./externals/", "ios", "8.0", new Dictionary<string, string> {
        { "SCCatWaitingHUD", "0.1.6" }
	});
	if (!FileExists ("./externals/Podfile.lock")) {
		CocoaPodInstall ("./externals/", new CocoaPodInstallSettings { NoIntegrate = true });
	}

	BuildXCodeFatLibrary ("Pods/Pods.xcodeproj", "SCCatWaitingHUD", null, null, null);
	//BuildXCode ("./Pods/Pods.xcodeproj", "Pods", "Pods-SCCatWaitingHUD", "libSCCatWaitingHUD.a");
});

Task ("clean").IsDependentOn ("clean-base").Does (() =>
{
	DeleteFiles ("./externals/Podfile.lock");
	CleanXCodeBuild ("./Pods/", null);
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

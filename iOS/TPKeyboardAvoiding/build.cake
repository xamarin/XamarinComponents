
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new IOSSolutionBuilder {
			SolutionPath = "./TPKeyboardAvoiding.sln",
			Configuration = "Release",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/TPKeyboardAvoiding/bin/unified/Release/TPKeyboardAvoiding.dll",
					ToDirectory = "./output/unified/"
				},
				new OutputFileCopy {
					FromFile = "./source/TPKeyboardAvoiding/bin/classic/Release/TPKeyboardAvoiding.dll",
					ToDirectory = "./output/classic/"
				}
			}
		},
	},

	Samples = new ISolutionBuilder [] {
		new IOSSolutionBuilder { SolutionPath = "./samples/TPKeyboardAvoidingSample.sln" },	
	},

	Components = new [] {
		new Component {ManifestDirectory = "./component", BuildsOn = BuildPlatforms.Mac},
	},
};

Task ("externals").IsDependentOn ("externals-base").Does (() =>
{
	CreatePodfile ("./externals/", "ios", "7.0", new Dictionary<string, string> {
        { "TPKeyboardAvoiding", "1.2.11" }
	});
	if (!FileExists ("./externals/Podfile.lock")) {
		CocoaPodInstall ("./externals/", new CocoaPodInstallSettings { NoIntegrate = true });
	}

	BuildXCodeFatLibrary ("Pods/Pods.xcodeproj", "TPKeyboardAvoiding", null, null, null);
});

Task ("clean").IsDependentOn ("clean-base").Does (() =>
{
	DeleteFiles ("./externals/Podfile.lock");
	CleanXCodeBuild ("./Pods/", null);
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

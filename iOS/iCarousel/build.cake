
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new IOSSolutionBuilder {
			SolutionPath = "./source/iCarousel.sln",
			Configuration = "Release",
			BuildsOn = BuildPlatforms.Mac,
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/iCarousel/bin/unified/Release/iCarousel.dll",
					ToDirectory = "./output/unified/"
				},
				new OutputFileCopy {
					FromFile = "./source/iCarousel/bin/classic/Release/iCarousel.dll",
					ToDirectory = "./output/classic/"
				}
			}
		},	
	},

	Samples = new ISolutionBuilder [] {
		new IOSSolutionBuilder { SolutionPath = "./samples/iCarouselSamples/iCarouselSamples.sln", BuildsOn = BuildPlatforms.Mac },
		new IOSSolutionBuilder { SolutionPath = "./samples/iCarouselSamples/iCarouselSamples-Classic.sln", BuildsOn = BuildPlatforms.Mac },
	},

	Components = new [] {
		new Component {ManifestDirectory = "./component", BuildsOn = BuildPlatforms.Mac},
	},
};

Task ("externals").IsDependentOn ("externals-base").Does (() =>
{
	CreatePodfile ("./externals/", "ios", "6.0", new Dictionary<string, string> {
        { "iCarousel", "1.8.2" }
	});
	if (!FileExists ("./externals/Podfile.lock")) {
		CocoaPodInstall ("./externals/", new CocoaPodInstallSettings { NoIntegrate = true });
	}

	BuildXCodeFatLibrary ("Pods/Pods.xcodeproj", "iCarousel", null, null, null);
});

Task ("clean").IsDependentOn ("clean-base").Does (() =>
{
	DeleteFiles ("./externals/Podfile.lock");
	CleanXCodeBuild ("./Pods/", null);
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

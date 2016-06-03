
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new IOSSolutionBuilder {
			SolutionPath = "./source/SDWebImage/SDWebImage.sln",
			Configuration = "Release",
			BuildsOn = BuildPlatforms.Mac,
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/SDWebImage/bin/unified/Release/SDWebImage.dll",
					ToDirectory = "./output/unified/"
				},
				new OutputFileCopy {
					FromFile = "./source/SDWebImage/bin/classic/Release/SDWebImage.dll",
					ToDirectory = "./output/classic/"
				}
			}
		},	
	},

	Samples = new ISolutionBuilder [] {
		new IOSSolutionBuilder { SolutionPath = "./samples/SDWebImageMTDialogSample/SDWebImageMTDialogSample.sln", BuildsOn = BuildPlatforms.Mac },
		new IOSSolutionBuilder { SolutionPath = "./samples/SDWebImageMTDialogSample/SDWebImageMTDialogSample-Classic.sln", BuildsOn = BuildPlatforms.Mac },
		
		new IOSSolutionBuilder { SolutionPath = "./samples/SDWebImageSample/SDWebImageSample.sln", BuildsOn = BuildPlatforms.Mac },
		new IOSSolutionBuilder { SolutionPath = "./samples/SDWebImageSample/SDWebImageSample-Classic.sln", BuildsOn = BuildPlatforms.Mac },
		
		new IOSSolutionBuilder { SolutionPath = "./samples/SDWebImageSimpleSample/SDWebImageSimpleSample.sln", BuildsOn = BuildPlatforms.Mac },
		new IOSSolutionBuilder { SolutionPath = "./samples/SDWebImageSimpleSample/SDWebImageSimpleSample-Classic.sln", BuildsOn = BuildPlatforms.Mac },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/SDWebImage.nuspec", BuildsOn = BuildPlatforms.Mac },
	},

	Components = new [] {
		new Component {ManifestDirectory = "./component", BuildsOn = BuildPlatforms.Mac},
	},
};

Task ("externals").IsDependentOn ("externals-base").Does (() =>
{
	CreatePodfile ("./externals/", "ios", "5.0", new Dictionary<string, string> {
        { "SDWebImage", "3.7.5" },
        { "SDWebImage/MapKit", "3.7.5" },
        { "SDWebImage/WebP", "3.7.5" },
	});
	if (!FileExists ("./externals/Podfile.lock")) {
		CocoaPodInstall ("./externals/", new CocoaPodInstallSettings { NoIntegrate = true });
	}

	BuildXCodeFatLibrary ("Pods/Pods.xcodeproj", "libwebp", null, null, null);
	BuildXCodeFatLibrary ("Pods/Pods.xcodeproj", "SDWebImage", null, null, null);
	
	RunLibtoolStatic("./externals/", "libSDWebImage.a", "libSDWebImage.a", "liblibwebp.a"); 
});

Task ("clean").IsDependentOn ("clean-base").Does (() =>
{
	DeleteFiles ("./externals/Podfile.lock");
	CleanXCodeBuild ("./Pods/", null);
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);



#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new IOSSolutionBuilder {
			PreBuildAction = () => {
				NuGetRestore ("./samples/DZNEmptyDataSetSample.sln");
			},
			SolutionPath = "./DZNEmptyDataSet.sln",
			Configuration = "Release",
			BuildsOn = BuildPlatforms.Mac,
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/DZNEmptyDataSet/bin/unified/Release/DZNEmptyDataSet.dll",
					ToDirectory = "./output/unified/"
				},
				new OutputFileCopy {
					FromFile = "./source/DZNEmptyDataSet/bin/classic/Release/DZNEmptyDataSet.dll",
					ToDirectory = "./output/classic/"
				}
			}
		},	
	},

	Samples = new ISolutionBuilder [] {
		new IOSSolutionBuilder { SolutionPath = "./samples/DZNEmptyDataSetSample.sln", BuildsOn = BuildPlatforms.Mac },
	},

	Components = new [] {
		new Component {ManifestDirectory = "./component", BuildsOn = BuildPlatforms.Mac},
	},
};

Task ("externals").IsDependentOn ("externals-base").Does (() =>
{
	CreatePodfile ("./externals/", "ios", "6.0", new Dictionary<string, string> {
        { "DZNEmptyDataSet", "1.7.3" }
	});
	if (!FileExists ("./externals/Podfile.lock")) {
		CocoaPodInstall ("./externals/", new CocoaPodInstallSettings { NoIntegrate = true });
	}

	BuildXCodeFatLibrary ("Pods/Pods.xcodeproj", "DZNEmptyDataSet", null, null, null);
});

Task ("clean").IsDependentOn ("clean-base").Does (() =>
{
	DeleteFiles ("./externals/Podfile.lock");
	CleanXCodeBuild ("./Pods/", null);
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

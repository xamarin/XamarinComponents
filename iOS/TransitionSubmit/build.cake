
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new IOSSolutionBuilder {
			SolutionPath = "./TransitionSubmit.sln",
			Configuration = "Release",
			BuildsOn = BuildPlatforms.Mac,
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/TransitionSubmit/bin/Release/TransitionSubmit.dll",
					ToDirectory = "./output/unified/"
				},
			}
		},	
	},

	Samples = new ISolutionBuilder [] {
		new IOSSolutionBuilder { SolutionPath = "./samples/TransitionSubmitSample/TransitionSubmitSample.sln", Configuration = "Release", Platform="iPhone", BuildsOn = BuildPlatforms.Mac },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.TransitionSubmit.nuspec" },
	},

	Components = new [] {
		new Component {ManifestDirectory = "./component", BuildsOn = BuildPlatforms.Mac},
	},
};

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

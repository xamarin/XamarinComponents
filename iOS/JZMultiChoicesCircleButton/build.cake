
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new IOSSolutionBuilder {
			SolutionPath = "./source/JZMultiChoice.sln",
			Configuration = "Release",
			BuildsOn = BuildPlatforms.Mac,
			OutputFiles = new [] { 
				new OutputFileCopy { 
					FromFile = "./source/JZMultiChoice/bin/Release/JZMultiChoice.dll", 
					ToDirectory = "./output/" 
				},	
			}
		},	
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.JZMultiChoicesCircleButton.nuspec", BuildsOn = BuildPlatforms.Mac },
	},
	
	Samples = new ISolutionBuilder [] {
		new IOSSolutionBuilder { SolutionPath = "./samples/JZMultiChoicesCircleButtonSample.sln", Configuration = "Release", Platform="iPhone", BuildsOn = BuildPlatforms.Mac },
	},

	Components = new [] {
		new Component {ManifestDirectory = "./component", BuildsOn = BuildPlatforms.Mac},
	},
};

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

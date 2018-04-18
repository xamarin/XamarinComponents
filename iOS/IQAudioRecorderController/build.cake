#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var buildSpec = new BuildSpec {

	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/IQAudioRecorderController.sln",
			Configuration = "Release",
			OutputFiles = new [] { 
				new OutputFileCopy { 
					FromFile = "./source/IQAudioRecorderController/bin/Release/IQAudioRecorderController.dll", 
					ToDirectory = "./output/" 
				},
			}
		},	
	},

	Samples = new ISolutionBuilder [] {
		new IOSSolutionBuilder { SolutionPath = "./samples/IQAudioRecorderControllerSample.sln", Configuration = "Release", Platform = "iPhone"  },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.IQAudioRecorderController.nuspec"},
	},
	
	Components = new [] {
		new Component { ManifestDirectory = "./component/" },
	},
};

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);
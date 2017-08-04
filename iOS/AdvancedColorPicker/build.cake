
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/AdvancedColorPicker.sln",
			Configuration = "Release",
			BuildsOn = BuildPlatforms.Mac,
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/AdvancedColorPicker/bin/Release/AdvancedColorPicker.dll",
					ToDirectory = "./output/"
				},
				new OutputFileCopy {
					FromFile = "./source/AdvancedColorPicker/bin/Release/AdvancedColorPicker.xml",
					ToDirectory = "./output/"
				},
			}
		},	
	},

	Samples = new ISolutionBuilder [] {
		new IOSSolutionBuilder {
			SolutionPath = "./samples/AdvancedColorPickerDemo/AdvancedColorPickerDemo.sln",
			Configuration = "Release", Platform="iPhone",
			BuildsOn = BuildPlatforms.Mac
		},
	},

	NuGets = new [] {
		new NuGetInfo {
			NuSpec = "./nuget/AdvancedColorPicker.nuspec",
			BuildsOn = BuildPlatforms.Mac
		},
	},

	Components = new [] {
		new Component {
			ManifestDirectory = "./component", 
			BuildsOn = BuildPlatforms.Mac
		},
	},
};

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

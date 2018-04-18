
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var buildSpec = new BuildSpec {

	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/Xamarin.Themes.TrackBeam.sln",
			Configuration = "Release",
			OutputFiles = new [] { 
				new OutputFileCopy { 
					FromFile = "./source/Xamarin.Themes.TrackBeam/bin/unified/Release/Xamarin.Themes.TrackBeam.dll", 
					ToDirectory = "./output/" 
				},
			}
		},	
	},

	Samples = new ISolutionBuilder [] {
		new IOSSolutionBuilder { SolutionPath = "./samples/TrackBeamTheme-Sample-iOS/TrackBeamTheme-Sample-iOS.sln", Configuration = "Release", Platform = "iPhone"  },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Themes.TrackBeam.nuspec"},
	},
	
	Components = new [] {
		new Component { ManifestDirectory = "./component/" },
	},
};

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

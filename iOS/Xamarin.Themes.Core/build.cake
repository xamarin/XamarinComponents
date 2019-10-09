
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var buildSpec = new BuildSpec {

	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/Xamarin.Themes.Core.sln",
			Configuration = "Release",
			OutputFiles = new [] { 
				new OutputFileCopy { 
					FromFile = "./source/Xamarin.Themes.Core/bin/unified/Release/Xamarin.Themes.Core.dll", 
					ToDirectory = "./output/unified/" 
				},
			}
		},	
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Themes.Core.nuspec"},
	},

	Components = new [] {
		new Component { ManifestDirectory = "./component/" },
	},
};

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

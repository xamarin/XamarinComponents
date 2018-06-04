
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/AMScrollingNavbar.sln",
			OutputFiles = new [] { 
				new OutputFileCopy { FromFile = "./source/AMScrollingNavbar/bin/Release/AMScrollingNavbar.dll" },
			}
		},
	},

	Samples = new ISolutionBuilder [] {
		new IOSSolutionBuilder { SolutionPath = "./samples/AMScrollingNavbarSample/AMScrollingNavbarSample.sln" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.AMScrollingNavbar.nuspec" },
	},

	Components = new [] {
		new Component { ManifestDirectory = "./component" },
	},
};

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

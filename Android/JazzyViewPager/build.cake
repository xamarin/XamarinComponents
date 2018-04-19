
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/JazzyViewPager.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/JazzyViewPager/bin/Release/JazzyViewPager.dll",
				}
			}
		}
	},

	Samples = new ISolutionBuilder [] {
		new DefaultSolutionBuilder { SolutionPath = "./samples/JazzyViewPagerSample.sln" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.JazzyViewPager.nuspec" },
	},

	Components = new [] {
		new Component {ManifestDirectory = "./component"},
	},
};

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);


#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var buildSpec = new BuildSpec {
	Libs = new [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/RadialProgress.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/RadialProgress.Android/bin/Release/RadialProgress.Android.dll",
				},
				new OutputFileCopy {
					FromFile = "./source/RadialProgress.iOS/bin/unified/Release/RadialProgress.iOS.dll",
				}
			}
		},
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.RadialProgress.nuspec" },
	},

	Samples = new [] {
		new IOSSolutionBuilder { SolutionPath = "./samples/RadialProgress.iOS.Sample/RadialProgress.iOS.Sample.sln",  Configuration = "Release", Platform="iPhone" },
		new DefaultSolutionBuilder { SolutionPath = "./samples/RadialProgress.Android.Sample/RadialProgress.Android.Sample.sln" }
	},

	Components = new [] {
		new Component { ManifestDirectory = "./component" }
	}
};

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);
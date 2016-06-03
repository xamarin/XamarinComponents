
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/DeviceYearClass.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/DeviceYearClass/bin/Release/DeviceYearClass.dll",
				}
			}
		}
	},

	Samples = new ISolutionBuilder [] {
		new DefaultSolutionBuilder { SolutionPath = "./samples/DeviceYearClassSample.sln" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/DeviceYearClass.nuspec" },
	},

	Components = new [] {
		new Component {ManifestDirectory = "./component"},
	},
};

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

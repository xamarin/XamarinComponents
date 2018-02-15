
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/HDNotification/HDNotification.sln",
			Configuration = "Release",
			BuildsOn = BuildPlatforms.Mac,
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/HDNotification/bin/Release/HDNotification.dll",
					ToDirectory = "./output/"
				},
			}
		},
	},

	Samples = new ISolutionBuilder [] {
		new IOSSolutionBuilder { SolutionPath = "./samples/HDNotificationDemo/HDNotificationDemo.sln", Configuration = "Release", Platform="iPhone", BuildsOn = BuildPlatforms.Mac },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.HDNotification.nuspec", BuildsOn = BuildPlatforms.Mac },
	},

	Components = new [] {
		new Component {ManifestDirectory = "./component", BuildsOn = BuildPlatforms.Mac},
	},
};

Task ("externals").IsDependentOn ("externals-base").Does (() =>
{
	RunMake ("./externals/", "all");
});

Task ("clean").IsDependentOn ("clean-base").Does (() =>
{
	RunMake ("./externals/", "clean");
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

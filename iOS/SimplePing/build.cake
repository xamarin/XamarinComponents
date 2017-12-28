
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var buildSpec = new BuildSpec () {
	Libs = new [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/Xamarin.SimplePing.sln",
			OutputFiles = new [] {
				new OutputFileCopy { FromFile = "./source/Xamarin.SimplePing.iOS/bin/Release/Xamarin.SimplePing.iOS.dll" },
				new OutputFileCopy { FromFile = "./source/Xamarin.SimplePing.Mac/bin/Release/Xamarin.SimplePing.Mac.dll" },
				new OutputFileCopy { FromFile = "./source/Xamarin.SimplePing.tvOS/bin/Release/Xamarin.SimplePing.tvOS.dll" },
			}
		},
	},

	Samples = new [] {
		new IOSSolutionBuilder { SolutionPath = "./samples/SimplePingSample.iOS/SimplePingSample.iOS.sln" },
		new IOSSolutionBuilder { SolutionPath = "./samples/SimplePingSample.Mac/SimplePingSample.Mac.sln", Platform = "x64" },
		new IOSSolutionBuilder { SolutionPath = "./samples/SimplePingSample.tvOS/SimplePingSample.tvOS.sln" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.SimplePing.nuspec" },
	},

	Components = new [] {
		new Component { ManifestDirectory = "./component" },
	},
};

Task ("externals")
	.IsDependentOn ("externals-base")
	.Does (() =>
{
	if (!IsRunningOnUnix ()) {
		return;
	}

	EnsureDirectoryExists ("./externals/");

	// iOS
	BuildXCodeFatLibrary_iOS (
		xcodeProject: "./SimplePing.xcodeproj",
		target: "SimplePing",
		workingDirectory: "./externals/Xamarin.SimplePing/");

	// macOS
	BuildXCodeFatLibrary_macOS (
		xcodeProject: "./SimplePing.xcodeproj",
		target: "SimplePingMac",
		workingDirectory: "./externals/Xamarin.SimplePing/");

	// tvOS
	BuildXCodeFatLibrary_tvOS (
		xcodeProject: "./SimplePing.xcodeproj",
		target: "SimplePingTV",
		workingDirectory: "./externals/Xamarin.SimplePing/");
});

Task ("clean")
	.IsDependentOn ("clean-base")
	.Does (() =>
{
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

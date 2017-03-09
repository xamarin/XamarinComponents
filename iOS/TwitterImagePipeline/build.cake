
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var IOS_PODS = new List<string> {
	"platform :ios, '7.0'",
	"install! 'cocoapods', :integrate_targets => false",
	"target 'Xamarin' do",
	"pod 'TwitterImagePipeline', '2.2.2'",
	"end",
};

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new IOSSolutionBuilder {
			SolutionPath = "./source/TwitterImagePipeline.sln",
			Configuration = "Release",
			BuildsOn = BuildPlatforms.Mac,
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/TwitterImagePipeline/bin/Release/TwitterImagePipeline.dll",
					ToDirectory = "./output/"
				},
			}
		},
	},

	Samples = new ISolutionBuilder [] {
		new IOSSolutionBuilder { 
			SolutionPath = "./samples/TwitterImagePipelineDemo/TwitterImagePipelineDemo.sln", 
			Configuration = "Release|iPhone", 
			BuildsOn = BuildPlatforms.Mac
		},
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.TwitterImagePipeline.nuspec" },
	},

	Components = new [] {
		new Component { ManifestDirectory = "./component", BuildsOn = BuildPlatforms.Mac },
	},
};

Task ("externals").IsDependentOn ("externals-base").Does (() =>
{
	EnsureDirectoryExists ("./externals");

	if (CocoaPodVersion () < new System.Version (1, 0))
		IOS_PODS.RemoveAt (1);

	FileWriteLines ("./externals/Podfile", IOS_PODS.ToArray ());

	CocoaPodInstall ("./externals", new CocoaPodInstallSettings { NoIntegrate = true });

	BuildXCodeFatLibrary ("./Pods/Pods.xcodeproj", "TwitterImagePipeline", "TwitterImagePipeline", null, null, "TwitterImagePipeline");
});

Task ("clean").IsDependentOn ("clean-base").Does (() =>
{
	DeleteFiles ("./externals/Podfile.lock");
	CleanXCodeBuild ("./Pods/", null);
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

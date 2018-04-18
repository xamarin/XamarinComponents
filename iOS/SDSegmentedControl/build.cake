
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var PODS = new List<string> {
	"platform :ios, '6.0'",
	"install! 'cocoapods', :integrate_targets => false",
	"target 'Xamarin' do",
	"  pod 'SDSegmentedControl', '1.0.4'",
	"  use_frameworks!",
	"end",
};

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/SDSegmentedControl.sln",
			Configuration = "Release",
			BuildsOn = BuildPlatforms.Mac,
			OutputFiles = new [] { 
				new OutputFileCopy { FromFile = "./source/SDSegmentedControl/bin/Release/SegmentedControl.dll", ToDirectory = "./output/" }
			}
		},	
	},

	Samples = new ISolutionBuilder [] {
		new IOSSolutionBuilder { SolutionPath = "./samples/SDSegmentedControlSample.sln", Configuration = "Release", Platform="iPhone", BuildsOn = BuildPlatforms.Mac },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.SDSegmentedControl.nuspec", BuildsOn = BuildPlatforms.Mac },
	},

	Components = new [] {
		new Component { ManifestDirectory = "./component", BuildsOn = BuildPlatforms.Mac },
	},
};

Task ("externals")
	.WithCriteria (!FileExists ("./externals/build/universal/SDSegmentedControl/SDSegmentedControl.framework/SDSegmentedControl"))
	.IsDependentOn ("externals-base")
	.Does (() => 
{
	EnsureDirectoryExists ("./externals");

	if (CocoaPodVersion () < new System.Version (1, 0))
		PODS.RemoveAt (1);

	FileWriteLines ("./externals/Podfile", PODS.ToArray ());

	CocoaPodInstall ("./externals", new CocoaPodInstallSettings { NoIntegrate = true });

	var config = "Release";

	var targetsSdks = new Dictionary<string, string> {
		{ "armv7", "iphoneos" },
		{ "armv7s", "iphoneos" },
		{ "arm64", "iphoneos" },
		{ "i386", "iphonesimulator" },
		{ "x86_64", "iphonesimulator" },
	};

	foreach (var targetSdk in targetsSdks) {
		XCodeBuild (new XCodeBuildSettings {
			Project = "./externals/Pods/Pods.xcodeproj",
			Target = "SDSegmentedControl",
			Sdk = targetSdk.Value,
			Arch = targetSdk.Key,
			Configuration = config,
		});

		MoveFile ("./externals/build/" + config + "-" + targetSdk.Value + "/SDSegmentedControl/SDSegmentedControl.framework/SDSegmentedControl",
			"./externals/libSDSegmentedControl-" + targetSdk.Key + ".a");
	}

	EnsureDirectoryExists ("./externals/build/universal");

	CopyDirectory ("./externals/build/" + config + "-iphoneos", "./externals/build/universal");

	RunLipoCreate ("./externals", 
	 	"./build/universal/SDSegmentedControl/SDSegmentedControl.framework/SDSegmentedControl", 
	 	"./libSDSegmentedControl-armv7.a",
	 	"./libSDSegmentedControl-armv7s.a",
	 	"./libSDSegmentedControl-arm64.a",
	 	"./libSDSegmentedControl-i386.a",
	 	"./libSDSegmentedControl-x86_64.a");
});

Task ("clean").IsDependentOn ("clean-base").Does (() =>
{
	DeleteFiles ("./externals/Podfile.lock");
	CleanXCodeBuild ("./Pods/", null);
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

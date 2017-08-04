
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var IOS_PODS = new List<string> {
	"platform :ios, '6.0'",
	"install! 'cocoapods', :integrate_targets => false",
	"target 'Xamarin' do",
	"pod 'Masonry', '0.6.4'",
	"end",
};

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new IOSSolutionBuilder {
			SolutionPath = "./Masonry.sln",
			Configuration = "Release",
			BuildsOn = BuildPlatforms.Mac,
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/Masonry/bin/unified/Release/Masonry.dll",
					ToDirectory = "./output/unified/"
				},
			}
		},	
	},

	Samples = new ISolutionBuilder [] {
		new IOSSolutionBuilder { SolutionPath = "./samples/MasonrySample.sln", Configuration = "Release", Platform="iPhone", BuildsOn = BuildPlatforms.Mac },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Masonry.nuspec", BuildsOn = BuildPlatforms.Mac },
	},

	Components = new [] {
		new Component {ManifestDirectory = "./component", BuildsOn = BuildPlatforms.Mac},
	},
};

Task ("externals").IsDependentOn ("externals-base").Does (() =>
{
	EnsureDirectoryExists ("./externals");

	if (CocoaPodVersion () < new System.Version (1, 0))
		IOS_PODS.RemoveAt (1);

	FileWriteLines ("./externals/Podfile", IOS_PODS.ToArray ());

	CocoaPodInstall ("./externals", new CocoaPodInstallSettings { NoIntegrate = true });

	BuildXCodeFatLibrary ("./Pods/Pods.xcodeproj", "Masonry", "Masonry", null, null, "Masonry");
});

Task ("clean").IsDependentOn ("clean-base").Does (() =>
{
	DeleteFiles ("./externals/Podfile");
	DeleteFiles ("./externals/Podfile.lock");
	CleanXCodeBuild ("./Pods/", null);
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

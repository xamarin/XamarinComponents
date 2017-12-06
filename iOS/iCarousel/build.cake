
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var POD_VERSION = "1.8.3";

var CreatePodSpec = new Action<string, string> ((platform, version) => {
	var v1 = CocoaPodVersion () >= new System.Version (1, 0);

	var podspec = new List<string> {
		"platform :" + platform + ", '" + version + "'",
		(v1 ? "install! 'cocoapods', :integrate_targets => false" : ""),
		"target 'Xamarin' do",
		"  pod 'iCarousel', '" + POD_VERSION + "'",
		"end",
	};

	FileWriteLines ("./externals/" + platform + "/Podfile", podspec.ToArray ());
	CocoaPodInstall ("./externals/" + platform, new CocoaPodInstallSettings { NoIntegrate = true });
});

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/iCarousel.sln",
			OutputFiles = new [] { 
				new OutputFileCopy { FromFile = "./source/iCarousel.iOS/bin/Release/iCarousel.iOS.dll" },
				new OutputFileCopy { FromFile = "./source/iCarousel.macOS/bin/Release/iCarousel.macOS.dll" },
			}
		},
	},

	Samples = new ISolutionBuilder [] {
		new IOSSolutionBuilder { SolutionPath = "./samples/iCarouselSample.sln" },
		new DefaultSolutionBuilder { SolutionPath = "./samples/iCarouselSampleMac.sln" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.iCarousel.nuspec" },
	},

	Components = new [] {
		new Component { ManifestDirectory = "./component" },
	},
};

Task ("externals").IsDependentOn ("externals-base").Does (() =>
{
	// iOS
	EnsureDirectoryExists ("./externals/ios");
	CreatePodSpec ("ios", "8.0");
	BuildXCodeFatLibrary_iOS ("./Pods/Pods.xcodeproj", "iCarousel", "iCarousel", null, "./externals/ios/", "iCarousel");

	// macOS
	EnsureDirectoryExists ("./externals/osx");
	CreatePodSpec ("osx", "10.8");
	BuildXCodeFatLibrary_macOS ("./Pods/Pods.xcodeproj", "iCarousel", "iCarousel", null, "./externals/osx/", "iCarousel");
});

Task ("clean").IsDependentOn ("clean-base").Does (() =>
{
	DeleteFiles ("./externals/ios/Podfile");
	DeleteFiles ("./externals/ios/Podfile.lock");
	CleanXCodeBuild ("./ios/Pods/", null);

	DeleteFiles ("./externals/osx/Podfile");
	DeleteFiles ("./externals/osx/Podfile.lock");
	CleanXCodeBuild ("./osx/Pods/", null);
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

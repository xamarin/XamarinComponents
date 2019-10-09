
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var POD_VERSION = "1.1.0";

var CreatePodSpec = new Action<string, string> ((platform, version) => {
	var v1 = CocoaPodVersion () >= new System.Version (1, 0);

	var podspec = new List<string> {
		"platform :" + platform + ", '" + version + "'",
		(v1 ? "install! 'cocoapods', :integrate_targets => false" : ""),
		"target 'Xamarin' do",
		"  pod 'Masonry', '" + POD_VERSION + "'",
		"end",
	};

	FileWriteLines ("./externals/" + platform + "/Podfile", podspec.ToArray ());
	CocoaPodInstall ("./externals/" + platform, new CocoaPodInstallSettings { NoIntegrate = true });
});

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/Masonry.sln",
			OutputFiles = new [] { 
				new OutputFileCopy { FromFile = "./source/Masonry.iOS/bin/Release/Masonry.iOS.dll" },
				new OutputFileCopy { FromFile = "./source/Masonry.tvOS/bin/Release/Masonry.tvOS.dll" },
				new OutputFileCopy { FromFile = "./source/Masonry.macOS/bin/Release/Masonry.macOS.dll" },
			}
		},
	},

	Samples = new ISolutionBuilder [] {
		new IOSSolutionBuilder { SolutionPath = "./samples/MasonrySample.sln" },
		new DefaultSolutionBuilder { SolutionPath = "./samples/MasonrySampleMac.sln", Platform = "x86" },
		new IOSSolutionBuilder { SolutionPath = "./samples/MasonrySampleTV.sln" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Masonry.nuspec", RequireLicenseAcceptance = true },
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
	BuildXCodeFatLibrary_iOS ("./Pods/Pods.xcodeproj", "Masonry", "Masonry", null, "./externals/ios/", "Masonry");

	// tvOS
	EnsureDirectoryExists ("./externals/tvos");
	CreatePodSpec ("tvos", "9.0");
	BuildXCodeFatLibrary_tvOS ("./Pods/Pods.xcodeproj", "Masonry", "Masonry", null, "./externals/tvos/", "Masonry");

	// macOS
	EnsureDirectoryExists ("./externals/osx");
	CreatePodSpec ("osx", "10.11");
	BuildXCodeFatLibrary_macOS ("./Pods/Pods.xcodeproj", "Masonry", "Masonry", null, "./externals/osx/", "Masonry");
});

Task ("clean").IsDependentOn ("clean-base").Does (() =>
{
	DeleteFiles ("./externals/ios/Podfile");
	DeleteFiles ("./externals/ios/Podfile.lock");
	CleanXCodeBuild ("./ios/Pods/", null);

	DeleteFiles ("./externals/tvos/Podfile");
	DeleteFiles ("./externals/tvos/Podfile.lock");
	CleanXCodeBuild ("./tvos/Pods/", null);

	DeleteFiles ("./externals/osx/Podfile");
	DeleteFiles ("./externals/osx/Podfile.lock");
	CleanXCodeBuild ("./osx/Pods/", null);
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

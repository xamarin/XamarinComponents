
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var POD_VERSION = "1.4.0";

var CreatePodSpec = new Action<string, string> ((platform, version) => {
	var v1 = CocoaPodVersion () >= new System.Version (1, 0);

	var podspec = new List<string> {
		"platform :" + platform + ", '" + version + "'",
		(v1 ? "install! 'cocoapods', :integrate_targets => false" : ""),
		"target 'Xamarin' do",
		"  pod 'ZipArchive', '" + POD_VERSION + "'",
		"end",
	};

	FileWriteLines ("./externals/" + platform + "/Podfile", podspec.ToArray ());
	CocoaPodInstall ("./externals/" + platform, new CocoaPodInstallSettings { NoIntegrate = true });
});

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/ZipArchive.sln",
			OutputFiles = new [] { 
				new OutputFileCopy { FromFile = "./source/ZipArchive.iOS/bin/Release/ZipArchive.iOS.dll" },
				new OutputFileCopy { FromFile = "./source/ZipArchive.tvOS/bin/Release/ZipArchive.tvOS.dll" },
				new OutputFileCopy { FromFile = "./source/ZipArchive.macOS/bin/Release/ZipArchive.macOS.dll" },
			}
		},
	},

	Samples = new ISolutionBuilder [] {
		new IOSSolutionBuilder { SolutionPath = "./samples/ZipArchiveSample.sln" },
		new DefaultSolutionBuilder { SolutionPath = "./samples/ZipArchiveSampleMac.sln", Platform = "x86" },
		new IOSSolutionBuilder { SolutionPath = "./samples/ZipArchiveSampleTV.sln" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.ZipArchive.nuspec" },
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
	BuildXCodeFatLibrary_iOS ("./Pods/Pods.xcodeproj", "ZipArchive", "ZipArchive", null, "./externals/ios/", "ZipArchive");

	// tvOS
	EnsureDirectoryExists ("./externals/tvos");
	CreatePodSpec ("tvos", "9.0");
	BuildXCodeFatLibrary_tvOS ("./Pods/Pods.xcodeproj", "ZipArchive", "ZipArchive", null, "./externals/tvos/", "ZipArchive");

	// macOS
	EnsureDirectoryExists ("./externals/osx");
	CreatePodSpec ("osx", "10.11");
	BuildXCodeFatLibrary_macOS ("./Pods/Pods.xcodeproj", "ZipArchive", "ZipArchive", null, "./externals/osx/", "ZipArchive");
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

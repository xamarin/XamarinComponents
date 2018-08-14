
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var IOS_PODS = new List<string> {
	"platform :ios, '8.0'",
	"install! 'cocoapods', :integrate_targets => false",
	"target 'Xamarin' do",
	"pod 'SDWebImage', '4.4'",
	"pod 'SDWebImage/MapKit', '4.4'",
	"pod 'SDWebImage/WebP', '4.4'",
	"end",
};

var POD_VERSION = "4.4.0";

var CreatePodSpec = new Action<string, string> ((platform, version) => {
	var v1 = CocoaPodVersion () >= new System.Version (1, 0);

	var mapkit = new [] { "ios", "osx", "tvos" }.Contains (platform);
	var gif = new [] { "ios" }.Contains (platform);
	var webp = new [] { "ios", "osx", "tvos" }.Contains (platform);

	var podspec = new List<string> {
		"platform :" + platform + ", '" + version + "'",
		(v1 ? "install! 'cocoapods', :integrate_targets => false" : ""),
		"target 'Xamarin' do",
		"  pod 'SDWebImage', '" + POD_VERSION + "'",
		(mapkit ? "  pod 'SDWebImage/MapKit', '" + POD_VERSION + "'" : ""),
		(gif ? "  pod 'SDWebImage/GIF', '" + POD_VERSION + "'" : ""),
		(webp ? "  pod 'SDWebImage/WebP', '" + POD_VERSION + "'" : ""),
		"end",
	};

	FileWriteLines ("./externals/" + platform + "/Podfile", podspec.ToArray ());
	CocoaPodInstall ("./externals/" + platform, new CocoaPodInstallSettings { NoIntegrate = true });
});

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/SDWebImage.sln",
			OutputFiles = new [] { 
				new OutputFileCopy { FromFile = "./source/SDWebImage.iOS/bin/Release/SDWebImage.iOS.dll" },
				new OutputFileCopy { FromFile = "./source/SDWebImage.macOS/bin/Release/SDWebImage.macOS.dll" },
				new OutputFileCopy { FromFile = "./source/SDWebImage.tvOS/bin/Release/SDWebImage.tvOS.dll" },
			}
		},
	},

	Samples = new ISolutionBuilder [] {
		new DefaultSolutionBuilder { SolutionPath = "./samples/SDWebImageMapKitSample/SDWebImageMapKitSample.sln", Platform = "iPhone" },
		new DefaultSolutionBuilder { SolutionPath = "./samples/SDWebImageSample/SDWebImageSample.sln", Platform = "iPhone" },		
		new DefaultSolutionBuilder { SolutionPath = "./samples/SDWebImageSampleTV/SDWebImageSampleTV.sln", Platform = "iPhone" },
		new DefaultSolutionBuilder { SolutionPath = "./samples/SDWebImageSimpleSample/SDWebImageSimpleSample.sln", Platform = "iPhone" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/SDWebImage.nuspec" },
	},

	Components = new [] {
		new Component { ManifestDirectory = "./component" },
	},
};

Task ("externals")
	.IsDependentOn ("externals-base")
	.Does (() =>
{
	// iOS
	EnsureDirectoryExists ("./externals/ios");
	CreatePodSpec ("ios", "8.0");
	BuildXCode ("./Pods/Pods.xcodeproj", "SDWebImage", "SDWebImage", "./externals/ios/", TargetOS.iOS);
	BuildXCode ("./Pods/Pods.xcodeproj", "libwebp", "libwebp", "./externals/ios/", TargetOS.iOS);
	BuildXCode ("./Pods/Pods.xcodeproj", "FLAnimatedImage", "FLAnimatedImage", "./externals/ios/", TargetOS.iOS);

	// macOS
	EnsureDirectoryExists ("./externals/osx");
	CreatePodSpec ("osx", "10.10");
	BuildXCode ("./Pods/Pods.xcodeproj", "SDWebImage", "SDWebImage", "./externals/osx/", TargetOS.Mac);
	BuildXCode ("./Pods/Pods.xcodeproj", "libwebp", "libwebp", "./externals/osx/", TargetOS.Mac);

	// tvOS
	EnsureDirectoryExists ("./externals/tvos");
	CreatePodSpec ("tvos", "9.0");
	BuildXCode ("./Pods/Pods.xcodeproj", "SDWebImage", "SDWebImage", "./externals/tvos/", TargetOS.tvOS);
	BuildXCode ("./Pods/Pods.xcodeproj", "libwebp", "libwebp", "./externals/tvos/", TargetOS.tvOS);
});

Task ("clean").IsDependentOn ("clean-base").Does (() =>
{
	if (DirectoryExists ("./externals/"))
		DeleteDirectory ("./externals", true);
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);


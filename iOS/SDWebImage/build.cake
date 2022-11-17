
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var POD_VERSION = "5.11.1";

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
		"  pod 'SDWebImageAVIFCoder', '0.9.0'",
		"  pod 'SDWebImageFLIFCoder', '0.4.0'",
		"  pod 'SDWebImageFLPlugin', '0.5.0'",
		"  pod 'SDWebImageHEIFCoder', '0.10.1'",
		"  pod 'SDWebImagePDFCoder', '0.8.0'",
		"  pod 'SDWebImagePhotosPlugin', '1.2.0'",
		"  pod 'SDWebImageVideoCoder', '0.2.0'",
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
		new NuGetInfo { NuSpec = "./nuget/SDWebImage.nuspec", RequireLicenseAcceptance = true  },
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
	CreatePodSpec ("ios", "9.0");
	BuildXCode ("./Pods/Pods.xcodeproj", "SDWebImage", "SDWebImage", "./externals/ios/", TargetOS.iOS);
	BuildXCode ("./Pods/Pods.xcodeproj", "libavif", "libavif", "./externals/ios/", TargetOS.iOS);
	BuildXCode ("./Pods/Pods.xcodeproj", "libheif", "libheif", "./externals/ios/", TargetOS.iOS);
	BuildXCode ("./Pods/Pods.xcodeproj", "libwebp", "libwebp", "./externals/ios/", TargetOS.iOS);
	BuildXCode ("./Pods/Pods.xcodeproj", "FLAnimatedImage", "FLAnimatedImage", "./externals/ios/", TargetOS.iOS);

	// macOS
	EnsureDirectoryExists ("./externals/osx");
	CreatePodSpec ("osx", "10.11");
	BuildXCode ("./Pods/Pods.xcodeproj", "SDWebImage", "SDWebImage", "./externals/osx/", TargetOS.Mac);
	BuildXCode ("./Pods/Pods.xcodeproj", "libavif", "libavif", "./externals/osx/", TargetOS.Mac);
	BuildXCode ("./Pods/Pods.xcodeproj", "libheif", "libheif", "./externals/osx/", TargetOS.Mac);
	BuildXCode ("./Pods/Pods.xcodeproj", "libwebp", "libwebp", "./externals/osx/", TargetOS.Mac);

	// tvOS
	var buildSettings = new Dictionary<string, string> {
		{ "BITCODE_GENERATION_MODE", "bitcode" },
	};
	EnsureDirectoryExists ("./externals/tvos");
	CreatePodSpec ("tvos", "9.2");
	BuildXCode ("./Pods/Pods.xcodeproj", "SDWebImage", "SDWebImage", "./externals/tvos/", TargetOS.tvOS, buildSettings);
	BuildXCode ("./Pods/Pods.xcodeproj", "libavif", "libavif", "./externals/tvos/", TargetOS.tvOS);
	BuildXCode ("./Pods/Pods.xcodeproj", "libheif", "libheif", "./externals/tvos/", TargetOS.tvOS);
	BuildXCode ("./Pods/Pods.xcodeproj", "libwebp", "libwebp", "./externals/tvos/", TargetOS.tvOS, buildSettings);
});

Task ("clean").IsDependentOn ("clean-base").Does (() =>
{
	if (DirectoryExists ("./externals/"))
		DeleteDirectory ("./externals", true);
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);


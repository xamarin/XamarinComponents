
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var IOS_PODS = new List<string> {
	"platform :ios, '7.0'",
	"install! 'cocoapods', :integrate_targets => false",
	"target 'Xamarin' do",
	"pod 'SDWebImage', '4.0.0'",
	"pod 'SDWebImage/MapKit', '4.0.0'",
	"pod 'SDWebImage/GIF', '4.0.0'",
	"pod 'SDWebImage/WebP', '4.0.0'",
	"end",
};

var OSX_PODS = new List<string> {
	"platform :osx, '10.8'",
	"install! 'cocoapods', :integrate_targets => false",
	"target 'Xamarin' do",
	"pod 'SDWebImage', '4.0.0'",
	"pod 'SDWebImage/MapKit', '4.0.0'",
	"pod 'SDWebImage/WebP', '4.0.0'",
	"end",
};

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new IOSSolutionBuilder {
			SolutionPath = "./source/SDWebImage.sln",
			Configuration = "Release",
			BuildsOn = BuildPlatforms.Mac,
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/SDWebImage/bin/unified/Release/SDWebImage.dll",
					ToDirectory = "./output/unified/"
				},
				new OutputFileCopy {
					FromFile = "./source/SDWebImage.MacOS/bin/Release/SDWebImage.dll ",
					ToDirectory = "./output/macos/"
				},
			}
		},	
	},

	Samples = new ISolutionBuilder [] {
		new IOSSolutionBuilder { SolutionPath = "./samples/SDWebImageMTDialogSample/SDWebImageMTDialogSample.sln", Configuration = "Release|iPhone", BuildsOn = BuildPlatforms.Mac },
		
		new IOSSolutionBuilder { SolutionPath = "./samples/SDWebImageSample/SDWebImageSample.sln", Configuration = "Release|iPhone", BuildsOn = BuildPlatforms.Mac },
		
		new IOSSolutionBuilder { SolutionPath = "./samples/SDWebImageSimpleSample/SDWebImageSimpleSample.sln", Configuration = "Release|iPhone", BuildsOn = BuildPlatforms.Mac },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/SDWebImage.nuspec", BuildsOn = BuildPlatforms.Mac },
	},

	Components = new [] {
		new Component {ManifestDirectory = "./component", BuildsOn = BuildPlatforms.Mac},
	},
};

Task ("externals-ios")
	.IsDependentOn ("externals-base")
	.WithCriteria (!FileExists ("./externals-ios/libSDWebImage.a"))
	.Does (() =>
{
	EnsureDirectoryExists ("./externals-ios");

	if (CocoaPodVersion () < new System.Version (1, 0))
		IOS_PODS.RemoveAt (1);

	FileWriteLines ("./externals-ios/Podfile", IOS_PODS.ToArray ());

	CocoaPodRepoUpdate ();

	CocoaPodInstall ("./externals-ios", new CocoaPodInstallSettings { NoIntegrate = true });

	BuildXCodeFatLibrary ("./Pods/Pods.xcodeproj", "libwebp", "libwebp", null, "./externals-ios", "libwebp");
	BuildXCodeFatLibrary ("./Pods/Pods.xcodeproj", "FLAnimatedImage", "FLAnimatedImage", null, "./externals-ios", "FLAnimatedImage");
	BuildXCodeFatLibrary ("./Pods/Pods.xcodeproj", "SDWebImage", "SDWebImage", null, "./externals-ios", "SDWebImage");

	RunLibtoolStatic("./externals-ios/", "libSDWebImage.a", "libSDWebImage.a", "liblibwebp.a", "libFLAnimatedImage.a"); 
});

Task ("externals-osx")
	.IsDependentOn ("externals-base")
	.WithCriteria (!FileExists ("./externals-osx/libSDWebImage.a"))
	.Does (() =>
{
	if (!IsRunningOnUnix())
	{
		Warning("{0} is not available on the current platform.", "xcodebuild");
		return;
	}
	
	EnsureDirectoryExists ("./externals-osx");

	if (CocoaPodVersion () < new System.Version (1, 0))
		OSX_PODS.RemoveAt (1);

	FileWriteLines ("./externals-osx/Podfile", OSX_PODS.ToArray ());

	CocoaPodRepoUpdate ();

	CocoaPodInstall ("./externals-osx", new CocoaPodInstallSettings { NoIntegrate = true });

	XCodeBuild(new XCodeBuildSettings
	{
		Project = new DirectoryPath ("./externals-osx/").CombineWithFilePath ("./Pods/Pods.xcodeproj").ToString (),
		Target = "SDWebImage",
		Sdk = "macosx",
		Arch = "x86_64",
		Configuration = "Release",
	});

	XCodeBuild(new XCodeBuildSettings
	{
		Project = new DirectoryPath ("./externals-osx/").CombineWithFilePath ("./Pods/Pods.xcodeproj").ToString (),
		Target = "libwebp",
		Sdk = "macosx",
		Arch = "x86_64",
		Configuration = "Release",
	});

	CopyFile("./externals-osx/build/Release/SDWebImage/libSDWebImage.a", "./externals-osx/libSDWebImage.a");
	CopyFile("./externals-osx/build/Release/libwebp/liblibwebp.a", "./externals-osx/liblibwebp.a");
	
});

Task ("externals")
	.IsDependentOn ("externals-ios")
	.IsDependentOn ("externals-osx");

Task ("clean").IsDependentOn ("clean-base").Does (() =>
{
	if (DirectoryExists ("./externals-ios/"))
		DeleteDirectory ("./externals-ios", true);
	if (DirectoryExists ("./externals-osx/"))
		DeleteDirectory ("./externals-osx", true);
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);


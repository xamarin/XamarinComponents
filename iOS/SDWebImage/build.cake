
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var IOS_PODS = new List<string> {
	"platform :ios, '5.0'",
	"install! 'cocoapods', :integrate_targets => false",
	"target 'Xamarin' do",
	"pod 'SDWebImage', '3.7.5'",
	"pod 'SDWebImage/MapKit', '3.7.5'",
	"pod 'SDWebImage/WebP', '3.7.5'",
	"end",
};

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new IOSSolutionBuilder {
			SolutionPath = "./source/SDWebImage/SDWebImage.sln",
			Configuration = "Release",
			BuildsOn = BuildPlatforms.Mac,
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/SDWebImage/bin/unified/Release/SDWebImage.dll",
					ToDirectory = "./output/unified/"
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

Task ("externals").IsDependentOn ("externals-base").Does (() =>
{
	EnsureDirectoryExists ("./externals");

	if (CocoaPodVersion () < new System.Version (1, 0))
		IOS_PODS.RemoveAt (1);

	FileWriteLines ("./externals/Podfile", IOS_PODS.ToArray ());

	CocoaPodInstall ("./externals", new CocoaPodInstallSettings { NoIntegrate = true });

	BuildXCodeFatLibrary ("./Pods/Pods.xcodeproj", "libwebp", "libwebp", null, null, "libwebp");
	BuildXCodeFatLibrary ("./Pods/Pods.xcodeproj", "SDWebImage", "SDWebImage", null, null, "SDWebImage");

	RunLibtoolStatic("./externals/", "libSDWebImage.a", "libSDWebImage.a", "liblibwebp.a"); 
});

Task ("clean").IsDependentOn ("clean-base").Does (() =>
{
	DeleteFiles ("./externals/Podfile.lock");
	CleanXCodeBuild ("./Pods/", null);
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);


#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var IOS_PODS = new List<string> {
	"platform :ios, '8.0'",
	"install! 'cocoapods', :integrate_targets => false",
	"use_frameworks!",
	"target 'Xamarin' do",
	"pod 'MaterialComponents', '72.2.0'",
	"end",
};

var buildSpec = new BuildSpec {

	Libs = new ISolutionBuilder [] {
		new IOSSolutionBuilder {
			SolutionPath = "source/MaterialComponents/MaterialComponents.sln",
			Configuration = "Release",
			Platform = "Any CPU",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/MaterialComponents/bin/Release/MaterialComponents.dll",
				}
			}
		},
	},

	Samples = new ISolutionBuilder [] {
		new IOSSolutionBuilder { 
			SolutionPath = "./samples/MaterialSample/MaterialSample.sln",
			Configuration = "Release", Platform="iPhone",
			BuildsOn = BuildPlatforms.Mac
		},
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.iOS.MaterialComponents.nuspec", BuildsOn = BuildPlatforms.Mac, RequireLicenseAcceptance = true },
	},
};

Task ("externals").IsDependentOn ("externals-base")
	.WithCriteria (!FileExists ("./externals/libMaterialComponents.a"))
	.Does (() => 
{
	EnsureDirectoryExists ("./externals");

	if (CocoaPodVersion () < new System.Version (1, 0))
		IOS_PODS.RemoveAt (1);

	FileWriteLines ("./externals/Podfile", IOS_PODS.ToArray ());

	CocoaPodInstall ("./externals", new CocoaPodInstallSettings { NoIntegrate = true });

	BuildDynamicXCode ("./Pods/Pods.xcodeproj", "MotionInterchange", "MotionInterchange", "./externals/", TargetOS.iOS);
	BuildDynamicXCode ("./Pods/Pods.xcodeproj", "MDFInternationalization", "MDFInternationalization", "./externals/", TargetOS.iOS);
	BuildDynamicXCode ("./Pods/Pods.xcodeproj", "MotionTransitioning", "MotionTransitioning", "./externals/", TargetOS.iOS);
	BuildDynamicXCode ("./Pods/Pods.xcodeproj", "MDFTextAccessibility", "MDFTextAccessibility", "./externals/", TargetOS.iOS);
	BuildDynamicXCode ("./Pods/Pods.xcodeproj", "MaterialComponents", "MaterialComponents", "./externals/", TargetOS.iOS);
	BuildDynamicXCode ("./Pods/Pods.xcodeproj", "MotionAnimator", "MotionAnimator", "./externals/", TargetOS.iOS);
});

Task ("clean").IsDependentOn ("clean-base").Does (() => 
{
	if (DirectoryExists ("./externals/"))
		DeleteDirectory ("./externals/", true);
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

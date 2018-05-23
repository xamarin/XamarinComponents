#load "../../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var IOS_VERSION = "0.8.0";
var IOS_NUGET_VERSION = "0.8.0-beta1";
var IOS_PODS = new List<string> {
	"platform :ios, '8.0'",
	"install! 'cocoapods', :integrate_targets => false",
	"target 'Xamarin' do",
	"pod 'GVRSDK', '" + IOS_VERSION + "'",
	"  use_frameworks!",
	"end",
};

var buildSpec = new BuildSpec {

	Libs = new [] {
		new IOSSolutionBuilder {
			SolutionPath = "./source/Google.VR.iOS.sln",
			OutputFiles = new [] {
				new OutputFileCopy { FromFile = "./source/Google.VR.iOS/bin/Release/Xamarin.Google.VR.iOS.dll" },
			}
		}
	},

	Samples = new [] {
		new IOSSolutionBuilder { SolutionPath = "./samples/VideoWidgetDemo.sln", Configuration = "Release", Platform="iPhone" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Google.VR.iOS.nuspec", Version = IOS_NUGET_VERSION, RequireLicenseAcceptance = true },
	}
};

Task ("externals")
	.WithCriteria (!DirectoryExists ("./externals/ios/Pods/GVRSDK"))
	.Does (() => 
{
	EnsureDirectoryExists ("./externals/ios");

	if (CocoaPodVersion () < new System.Version (1, 0))
		IOS_PODS.RemoveAt (1);

	FileWriteLines ("./externals/ios/Podfile", IOS_PODS.ToArray ());

	CocoaPodInstall ("./externals/ios", new CocoaPodInstallSettings { NoIntegrate = true });
});

Task ("clean").IsDependentOn ("clean-base").Does (() => 
{
	if (DirectoryExists ("./externals"))
		DeleteDirectory ("./externals", true);
});


SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);
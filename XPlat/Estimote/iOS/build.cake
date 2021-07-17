#load "../../../common.cake"

var NUGET_VERSION = "4.26.3.0";
var ESTIMOTE_SDK_VERSION = "4.26.3";

var COCOAPODS = new List<string> {
	"platform :ios, '7.0'",
	"install! 'cocoapods', :integrate_targets => false",
	"target 'Xamarin' do",
	"pod 'EstimoteSDK', '" + ESTIMOTE_SDK_VERSION + "'",
	"  use_frameworks!",
	"end",
};

var TARGET = Argument ("t", Argument ("target", "libs"));

var buildSpec = new BuildSpec {
	Libs = new [] {
		new IOSSolutionBuilder {
			SolutionPath = "./Xamarin.Estimote.iOS.sln",
			OutputFiles = new [] { 
				new OutputFileCopy { FromFile = "./source/bin/Release/Xamarin.Estimote.iOS.dll" }
			}
		}
	},

	Samples = new [] {
		new IOSSolutionBuilder { SolutionPath = "./samples/BeaconExample.sln",  Configuration = "Release", Platform="iPhone" },
		new IOSSolutionBuilder { SolutionPath = "./samples/NearableExample.sln",  Configuration = "Release", Platform="iPhone" }
	},

	NuGets  = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Estimote.iOS.nuspec", Version = NUGET_VERSION },
	},

	Components = new [] {
		new Component { ManifestDirectory = "./component" },
	}
};

Task ("externals")
	.IsDependentOn ("externals-base")
	.WithCriteria (!FileExists ("./externals/Podfile.lock"))
	.Does (() =>
{
	EnsureDirectoryExists ("./externals");

	if (CocoaPodVersion () < new System.Version (1, 0))
		COCOAPODS.RemoveAt (1);

	FileWriteLines ("./externals/Podfile", COCOAPODS.ToArray ());
	
	CocoaPodRepoUpdate ();

	CocoaPodInstall ("./externals", new CocoaPodInstallSettings { NoIntegrate = true });
});

Task ("clean")
	.IsDependentOn ("clean-base")
	.Does (() => 
{
	if (DirectoryExists ("./externals"))
		DeleteDirectory ("./externals", true);
});

Task("ci")
	.IsDependentOn("nuget");

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

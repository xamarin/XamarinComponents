#load "../../../common.cake"

var VERSION = "5.4.1";
var NUGET_VERSION = "5.4.1";

var URL = string.Format ("https://github.com/card-io/card.io-iOS-SDK/archive/{0}.zip", VERSION);

var TARGET = Argument ("t", Argument ("target", "Default"));

var buildSpec = new BuildSpec {
	Libs = new [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/CardIO.iOS.sln",
			Configuration = "Release",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/CardIO.iOS/bin/Release/Card.IO.dll",
					ToDirectory = "./output"
				},
			}
		},	
	},

	Samples = new [] {
		new IOSSolutionBuilder { SolutionPath = "./samples/CardIOSampleiOS.sln",  Configuration = "Release", Platform="iPhone" },
	},

	Components = new [] {
		new Component { ManifestDirectory = "./component/" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.CardIO.iOS.nuspec", Version = NUGET_VERSION },
	},
};


Task ("externals")
	.IsDependentOn ("externals-base")
	.WithCriteria (!FileExists ("./externals/cardio.ios.zip"))
	.Does (() => 
{
	EnsureDirectoryExists ("./externals/tmp/");

	if (!FileExists ("./externals/tmp/cardio.ios.zip")) {
		DownloadFile (URL, "./externals/tmp/cardio.ios.zip");
		Unzip ("./externals/tmp/cardio.ios.zip", "./externals/tmp/");

		CopyFiles (string.Format ("./externals/tmp/card.io-IOS-SDK-{0}/CardIO/*.a", VERSION), "./externals/");
	}

	//xcodebuild -project CardIOSharp/CardIOSharp.xcodeproj -target CardIOSharp -sdk iphoneos -configuration Release clean build
	XCodeBuild (new XCodeBuildSettings {
		Project = "./externals/CardIOSharp/CardIOSharp.xcodeproj",
		Target = "CardIOSharp",
		Sdk = "iphoneos",
		Configuration = "Release",
	});

	// xcodebuild -project CardIOSharp/CardIOSharp.xcodeproj -target CardIOSharp -sdk iphonesimulator  -configuration Release clean build
	XCodeBuild (new XCodeBuildSettings {
		Project = "./externals/CardIOSharp/CardIOSharp.xcodeproj",
		Target = "CardIOSharp",
		Sdk = "iphonesimulator",
		Configuration = "Release",
	});

	RunLipoCreate ("./", 
		"./externals/CardIOSharp/build/Release-iphoneos/CardIOSharp.framework/CardIOSharp", // target
		"./externals/CardIOSharp/build/Release-iphoneos/CardIOSharp.framework/CardIOSharp", //inputs
		"./externals/CardIOSharp/build/Release-iphonesimulator/CardIOSharp.framework/CardIOSharp");

	CopyDirectory ("./externals/CardIOSharp/build/Release-iphoneos/CardIOSharp.framework/",
		"./externals/CardIOSharp.framework");
});

Task ("clean").IsDependentOn ("clean-base").Does (() =>
{
	DeleteFiles ("./externals/*.a");
	
	if (DirectoryExists ("./externals/CardIOSharp.framework"))
		DeleteDirectory ("./externals/CardIOSharp.framework", true);

	if (DirectoryExists ("./externals/CardIOSharp/build/"))
		DeleteDirectory ("./externals/CardIOSharp/build/", true);

	if (DirectoryExists ("./externals/tmp/"))
		DeleteDirectory ("./externals/tmp", true);
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

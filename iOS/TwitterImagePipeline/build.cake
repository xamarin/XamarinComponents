
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var IOS_PODS = new List<string> {
	"platform :ios, '7.0'",
	"install! 'cocoapods', :integrate_targets => false",
	"target 'Xamarin' do",
	"pod 'TwitterImagePipeline', '2.2.2'",
	"end",
};

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/TwitterImagePipeline.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/TwitterImagePipeline/bin/Release/TwitterImagePipeline.dll",
					ToDirectory = "./output/"
				},
			}
		},
	},

	Samples = new ISolutionBuilder [] {
		new IOSSolutionBuilder { 
			SolutionPath = "./samples/TwitterImagePipelineDemo/TwitterImagePipelineDemo.sln", 
		},
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.TwitterImagePipeline.nuspec" },
	},

	Components = new [] {
		new Component { ManifestDirectory = "./component" },
	},
};

Task ("externals").IsDependentOn ("externals-base").Does (() =>
{
	EnsureDirectoryExists ("./externals");

	if (CocoaPodVersion () < new System.Version (1, 0))
		IOS_PODS.RemoveAt (1);

	FileWriteLines ("./externals/Podfile", IOS_PODS.ToArray ());

	CocoaPodInstall ("./externals", new CocoaPodInstallSettings { NoIntegrate = true });

	// inject our special logger somewhere
	var loggerFile = "./externals/Pods/TwitterImagePipeline/TwitterImagePipeline/TIPLogger.h";
	var simpleLoggerFile = "./native/TIPSimpleLogger.h";
	if (!FileExists(loggerFile) || FindTextInFiles (loggerFile, "TIPSimpleLogger").Length == 0) {
		// we might not have write permissions
		StartProcess ("chmod", new ProcessSettings { Arguments = "+w " + loggerFile });
		var simpleLoggerContents = FileReadText (simpleLoggerFile);
		FileAppendText (loggerFile, simpleLoggerContents);
	}
	var loggerImpl = "./externals/Pods/TwitterImagePipeline/TwitterImagePipeline/TIPGlobalConfiguration.m";
	var simpleLoggerImpl = "./native/TIPSimpleLogger.m";
	if (!FileExists(loggerImpl) || FindTextInFiles (loggerImpl, "TIPSimpleLogger").Length == 0) {
		// we might not have write permissions
		StartProcess ("chmod", new ProcessSettings { Arguments = "+w " + loggerImpl });
		var simpleLoggerContents = FileReadText (simpleLoggerImpl);
		FileAppendText (loggerImpl, simpleLoggerContents);
	}

	BuildXCodeFatLibrary ("./Pods/Pods.xcodeproj", "TwitterImagePipeline", "TwitterImagePipeline", null, null, "TwitterImagePipeline");
});

Task ("clean").IsDependentOn ("clean-base").Does (() =>
{
	DeleteFiles ("./externals/Podfile.lock");
	CleanXCodeBuild ("./Pods/", null);
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

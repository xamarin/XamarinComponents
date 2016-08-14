
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/Tango.sln",
			BuildsOn = BuildPlatforms.Mac | BuildPlatforms.Windows | BuildPlatforms.Linux,
			OutputFiles = new [] { 
				new OutputFileCopy { FromFile = "./source/Tango/bin/Release/Tango.dll" },
				new OutputFileCopy { FromFile = "./source/Tango.UX/bin/Release/Tango.UX.dll" },
				new OutputFileCopy { FromFile = "./source/Tango.Support/bin/Release/Tango.Support.dll" },
			}
		}
	},

	Samples = new ISolutionBuilder [] {
		new DefaultSolutionBuilder { SolutionPath = "./samples/HelloAreaDescription.sln" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Google.Tango.nuspec" },
	},

	Components = new [] {
		new Component {ManifestDirectory = "./component"},
	},
};

Task ("externals")
	.Does (() => 
{
	if (!DirectoryExists ("./externals/"))
		CreateDirectory ("./externals");
		
	// download the files
	var version = "Tania-borealis";
	// TODO: these links don't work outside browser - https://developers.google.com/tango/downloads
	if (!FileExists ("./externals/TangoSDK_Java.jar")) {
		DownloadFile ("https://developers.google.com/tango/downloads/TangoSDK_"+version+"_Java.jar", "./externals/TangoSDK_Java.jar");
	}
	if (!FileExists ("./externals/TangoUX_Java.aar")) {
		DownloadFile ("https://developers.google.com/tango/downloads/TangoUX_"+version+"_Java.aar", "./externals/TangoUX_Java.aar");
	}
	if (!FileExists ("./externals/TangoSupport_Java.aar")) {
		DownloadFile ("https://developers.google.com/tango/downloads/TangoSupport_Tania-borealis_Java.aar", "./externals/TangoSupport_Java.aar");
	}

	// build the cloud service stub
	if (!FileExists ("./externals/TangoCloudService.jar")) {
		FilePath tool = IsRunningOnWindows () ? "gradlew.bat" : "gradlew";
		DirectoryPath dir = "./source/TangoCloudService/";
		StartProcess (MakeAbsolute (dir.CombineWithFilePath (tool)), new ProcessSettings {
			WorkingDirectory = dir,
			Arguments = "jarReleaseClasses"
		});
		CopyFile ("./source/TangoCloudService/app/build/intermediates/packaged/release/classes.jar", "./externals/TangoCloudService.jar");
	}
});

Task ("clean").IsDependentOn ("clean-base").Does (() => 
{	
	DeleteFiles ("./externals/*");
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);


#load "../../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var SDK_VERSION = "1.3.13";
var SDK_URL = string.Format ("https://www.dropboxstatic.com/static/developers/dropbox-ios-sdk-{0}.zip", SDK_VERSION);
var SDK_ZIP = string.Format ("./externals/dropbox-ios-sdk-{0}.zip", SDK_VERSION);
var SDK_PATH = string.Format ("./externals/dropbox-ios-sdk-{0}", SDK_VERSION);

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/Dropbox.CoreApi.iOS/Dropbox.CoreApi.iOS.sln",
			Configuration = "Release",
			BuildsOn = BuildPlatforms.Mac,
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/Dropbox.CoreApi.iOS/bin/unified/Release/Dropbox.CoreApi.iOS.dll",
					ToDirectory = "./output/unified/"
				}
			}
		}	
	},

	Samples = new ISolutionBuilder [] {
		new IOSSolutionBuilder { SolutionPath = "./samples/DropboxCoreApiSample/DropboxCoreApiSample.sln", Configuration = "Release", Platform="iPhone", BuildsOn = BuildPlatforms.Mac, }
	},

	Components = new [] {
		new Component {ManifestDirectory = "./component", BuildsOn = BuildPlatforms.Mac},
	},

};

Task ("externals")
	.WithCriteria (!FileExists ("./externals/DropboxSDK.a"))
	.Does (() => 
{
	if (!DirectoryExists ("./externals/"))
		CreateDirectory ("./externals");

	DownloadFile (SDK_URL, SDK_ZIP);

	Unzip (SDK_ZIP, "./externals/");

	CopyFile (SDK_PATH + "/DropboxSDK.framework/DropboxSDK", "./externals/DropboxSDK.a");
});

Task ("clean").IsDependentOn ("clean-base").Does (() => 
{
	if (DirectoryExists ("./externals/"))
		DeleteDirectory ("./externals", true);
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

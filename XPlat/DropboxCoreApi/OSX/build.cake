
#load "../../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var SDK_VERSION = "1.3.14";
var SDK_URL = string.Format ("https://www.dropboxstatic.com/static/developers/dropbox-osx-sdk-{0}.zip", SDK_VERSION);
var SDK_ZIP = string.Format ("./externals/dropbox-osx-sdk-{0}.zip", SDK_VERSION);
var SDK_PATH = string.Format ("./externals/dropbox-osx-sdk-{0}", SDK_VERSION);

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new IOSSolutionBuilder {
			SolutionPath = "./source/Dropbox.CoreApi.OSX/Dropbox.CoreApi.OSX.sln",
			Configuration = "Release",
			BuildsOn = BuildPlatforms.Mac,
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/Dropbox.CoreApi.OSX/bin/unified/Release/Dropbox.CoreApi.OSX.dll",
					ToDirectory = "./output/unified/"
				}
			}
		}	
	},

	Samples = new ISolutionBuilder [] {
		new IOSSolutionBuilder { SolutionPath = "./samples/DropboxCoreApiSample/DropboxCoreApiSample.sln", Configuration = "Release|iPhone", BuildsOn = BuildPlatforms.Mac, }
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
});

Task ("clean").IsDependentOn ("clean-base").Does (() => 
{
	if (DirectoryExists ("./externals/"))
		DeleteDirectory ("./externals", true);
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

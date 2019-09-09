
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var JAR_VERSION = "1.6.0";
var JAR_URL = string.Format ("https://csspeechstorage.blob.core.windows.net/maven/com/microsoft/cognitiveservices/speech/client-sdk/{0}/client-sdk-{0}.aar", JAR_VERSION);
var JAR_DEST = "./externals/AzureSpeech.aar";

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/AndroidAzureSpeech.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/AndroidAzureSpeech/bin/Release/AndroidAzureSpeech.dll",
				}
			}
		}
	},

	Samples = new ISolutionBuilder [] {
		new DefaultSolutionBuilder { SolutionPath = "./samples/AndroidAzureSpeechSample.sln" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/AndroidAzureSpeech.nuspec" },
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

	if (!FileExists (JAR_DEST))
		DownloadFile (JAR_URL, JAR_DEST);
});


Task ("clean").IsDependentOn ("clean-base").Does (() => 
{	
	DeleteFiles ("./externals/*.aar");
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

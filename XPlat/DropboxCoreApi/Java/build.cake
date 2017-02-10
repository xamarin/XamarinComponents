#load "../../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var DB_VERSION = "1.8.2";
var DB_URL = string.Format ("https://www.dropbox.com/developers/downloads/sdks/core/java/dropbox-java-sdk-{0}.zip", DB_VERSION);
var JS_VERSION = "2.6.1";

var API_FILE = string.Format ("./externals/dropbox-java-sdk-{0}.zip", DB_VERSION);
var API_PATH = string.Format ("./externals/dropbox-java-sdk-{0}/", DB_VERSION);

var JAR_DB_FILE = string.Format ("dropbox-core-sdk-{0}.jar", DB_VERSION);
var JAR_JS_FILE = string.Format ("jackson-core-{0}.jar", JS_VERSION);

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new IOSSolutionBuilder {
			SolutionPath = "./source/Dropbox.CoreApi.Java.sln",
			Configuration = "Release",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/Dropbox.CoreApi.Java/bin/Release/Dropbox.CoreApi.Java.dll",
					ToDirectory = "./output/"
				}
			}
		}
	},

	Samples = new ISolutionBuilder [] {
		new IOSSolutionBuilder { SolutionPath = "./samples/DropboxCoreApiSample/DropboxCoreApiSample.sln" }
	},

	Components = new [] {
		new Component {ManifestDirectory = "./component"},
	},
};

Task ("externals")
	.WithCriteria (!FileExists (API_FILE))
	.Does (() => 
{
	if (!DirectoryExists ("./externals/"))
		CreateDirectory ("./externals");

	DownloadFile (DB_URL, API_FILE);

	Unzip (API_FILE, "./externals/");

	CopyFile (API_PATH + "lib/" + JAR_DB_FILE, "./externals/" + JAR_DB_FILE);
	CopyFile (API_PATH + "lib/" + JAR_JS_FILE, "./externals/" + JAR_JS_FILE);	
});

Task ("clean").IsDependentOn ("clean-base").Does (() => 
{
	if (DirectoryExists ("./externals/"))
		DeleteDirectory ("./externals", true);
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

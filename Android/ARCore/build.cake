#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var NUGET_VERSION = "1.9.0";

var AAR_VERSION = "1.9.0";
var AAR_URL = string.Format("https://dl.google.com/dl/android/maven2/com/google/ar/core/{0}/core-{0}.aar", AAR_VERSION);
var OBJ_VERSION = "0.3.0";
var OBJ_URL = string.Format("https://oss.sonatype.org/content/repositories/releases/de/javagl/obj/{0}/obj-{0}.jar", OBJ_VERSION);

var buildSpec = new BuildSpec () {
	Libs = new [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./ARCore.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/bin/Release/Xamarin.Google.ARCore.dll",
				}
			}
		}
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Google.ARCore.nuspec", Version = NUGET_VERSION, RequireLicenseAcceptance = true  },
	},
};

Task ("externals")
	.Does (() => 
{
	var AAR_FILE = "./externals/arcore.aar";
	var OBJ_JAR_FILE = "./externals/obj.jar";

	if (!DirectoryExists ("./externals/"))
		CreateDirectory ("./externals");

	if (!FileExists (AAR_FILE))
		DownloadFile (AAR_URL, AAR_FILE);

	if (!FileExists (OBJ_JAR_FILE))
		DownloadFile (OBJ_URL, OBJ_JAR_FILE);
});


Task ("clean").IsDependentOn ("clean-base").Does (() => 
{	
	if (DirectoryExists ("./externals"))
		DeleteDirectory ("./externals", true);
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);
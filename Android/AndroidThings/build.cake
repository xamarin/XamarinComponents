#addin nuget:?package=Cake.Xamarin.Build
#addin nuget:?package=Cake.Xamarin

var TARGET = Argument ("t", Argument ("target", "Default"));

var NUGET_VERSION = "0.3-devpreview";

var JAR_VERSION = "0.3-devpreview";
var JAR_URL = string.Format ("https://bintray.com/google/androidthings/download_file?file_path=com%2Fgoogle%2Fandroid%2Fthings%2Fandroidthings%2F{0}%2Fandroidthings-{0}.jar", JAR_VERSION);
var JAR_DEST = "./externals/androidthings.jar";

var DOCS_URL = "https://developer.android.com/things/downloads/com.google.android.things-docs-dp2.zip";


var buildSpec = new BuildSpec () {
	Libs = new [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./Android.Things.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/bin/Release/Xamarin.Android.Things.dll",
				}
			}
		}
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Android.Things.nuspec", Version = NUGET_VERSION },
	},
};

Task ("externals")
	.Does (() => 
{
	if (!DirectoryExists ("./externals/"))
		CreateDirectory ("./externals");

	if (!FileExists (JAR_DEST))
		DownloadFile (JAR_URL, JAR_DEST);

	if (!FileExists ("./externals/docs.zip")) {
		DownloadFile (DOCS_URL, "./externals/docs.zip");
		Unzip ("./externals/docs.zip", "./externals/docs");
	}
});


Task ("clean").IsDependentOn ("clean-base").Does (() => 
{	
	if (DirectoryExists ("./externals"))
		DeleteDirectory ("./externals", true);
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var GUAVA_VERSION = "23.2-android";
var NUGET_VERSION = "23.2.0";

var GUAVA_JAR_URL = string.Format ("http://search.maven.org/remotecontent?filepath=com/google/guava/guava/{0}/guava-{0}.jar", GUAVA_VERSION);
var GUAVA_DOCS_URL = string.Format ("http://search.maven.org/remotecontent?filepath=com/google/guava/guava/{0}/guava-{0}-javadoc.jar", GUAVA_VERSION);

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./Guava.sln",
			OutputFiles = new [] {
				new OutputFileCopy { FromFile = "./source/Guava/bin/Release/Xamarin.Google.Guava.dll" },
			}
		}
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Guava.nuspec", Version = NUGET_VERSION },
	},
};

Task ("externals")
	.WithCriteria (!FileExists ("./externals/guava.jar"))
	.Does (() =>
{
	if (!DirectoryExists ("./externals/"))
		CreateDirectory ("./externals");

	// Download Dependencies
	DownloadFile (GUAVA_JAR_URL, "./externals/guava.jar");
	DownloadFile (GUAVA_DOCS_URL, "./externals/guava-javadocs.jar");

	Unzip ("./externals/guava-javadocs.jar", "./externals/guava-javadocs/");
});


Task ("clean").IsDependentOn ("clean-base").Does (() =>
{
	if (DirectoryExists ("./externals/"))
		DeleteDirectory ("./externals", true);
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

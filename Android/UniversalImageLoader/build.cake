
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var JAR_VERSION = "1.9.5";
var JAR_URL = string.Format ("http://repo1.maven.org/maven2/com/nostra13/universalimageloader/universal-image-loader/{0}/universal-image-loader-{0}.jar", JAR_VERSION);
var JAR_DEST = "./externals/UniversalImageLoader.jar";

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/UniversalImageLoader.sln",
			OutputFiles = new [] { 
				new OutputFileCopy { FromFile = "./source/UniversalImageLoader/bin/Release/UniversalImageLoader.dll" }
			}
		}
	},

	Samples = new ISolutionBuilder [] {
		new DefaultSolutionBuilder { SolutionPath = "./samples/UniversalImageLoaderSample.sln" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Android.UniversalImageLoader.nuspec" },
	},

	Components = new [] {
		new Component { ManifestDirectory = "./component" },
	},
};

Task ("externals")
	.Does (() => 
{
	EnsureDirectoryExists ("./externals/");
	DownloadFile (JAR_URL, JAR_DEST);
});

Task ("clean").IsDependentOn ("clean-base").Does (() => 
{	
	DeleteFiles ("./externals/*.jar");
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

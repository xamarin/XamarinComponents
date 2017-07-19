
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var JAR_VERSION = "0.5.0";
var JAR_URL = string.Format ("http://repo1.maven.org/maven2/io/supercharge/shimmerlayout/{0}/shimmerlayout-{0}.aar", JAR_VERSION);
var JAR_DEST = "./externals/ShimmerLayout.aar";

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/Xamarin.Android.ShimmerLayout.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/Xamarin.Android.ShimmerLayout/bin/Release/Xamarin.Android.ShimmerLayout.dll",
				}
			}
		}
	},

	Samples = new ISolutionBuilder [] {
		new DefaultSolutionBuilder { SolutionPath = "./samples/ShimmerLayoutSample.sln" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Android.ShimmerLayout.nuspec" },
	},

	Components = new [] {
		new Component { ManifestDirectory = "./component" },
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

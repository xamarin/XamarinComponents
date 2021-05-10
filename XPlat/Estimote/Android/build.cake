
#load "../../../common.cake"

var NUGET_VERSION = "1.0.3";

var ESTIMOTES_VERSION = "1.0.3";
var ESTIMOTES_SDK_URL = "http://search.maven.org/remotecontent?filepath=com/estimote/sdk/" + ESTIMOTES_VERSION + "/sdk-" + ESTIMOTES_VERSION + ".aar";
var ESTIMOTES_DOC_URL = "http://search.maven.org/remotecontent?filepath=com/estimote/sdk/" + ESTIMOTES_VERSION + "/sdk-" + ESTIMOTES_VERSION + "-javadoc.jar";

var TARGET = Argument ("t", Argument ("target", "libs"));

var buildSpec = new BuildSpec {

	Libs = new [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./Xamarin.Estimote.Android.sln",
			OutputFiles = new [] { 
				new OutputFileCopy { FromFile = "./source/bin/Release/Xamarin.Estimote.Android.dll" }
			}
		},
	},

	Samples = new [] {
		new DefaultSolutionBuilder { SolutionPath = "./samples/EstimoteSample.sln" }
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Estimote.Android.nuspec", Version = NUGET_VERSION },
	},

	Components = new [] {
		new Component { ManifestDirectory = "./component" },
	}
};


Task ("externals")
	.IsDependentOn ("externals-base")
	.WithCriteria (!DirectoryExists ("./externals/javadocs/") && !FileExists ("./externals/estimotes.aar"))
	.Does (() => 
{
	CreateDirectory ("./externals/");
	DownloadFile (ESTIMOTES_SDK_URL, "./externals/estimotes.aar");
});

Task ("clean")
	.IsDependentOn ("clean-base")
	.Does (() => 
{
	if (DirectoryExists ("./externals"))
		DeleteDirectory ("./externals", true);
});

Task("ci")
	.IsDependentOn("nuget");

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

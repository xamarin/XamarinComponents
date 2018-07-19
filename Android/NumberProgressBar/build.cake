
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var AAR_VERSION = "1.2";
var AAR_URL = string.Format ("http://search.maven.org/remotecontent?filepath=com/daimajia/numberprogressbar/library/{0}/library-{0}.aar", AAR_VERSION);
var AAR_FILE = "./externals/NumberProgressBar.aar";

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/NumberProgressBar.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/NumberProgressBar/bin/Release/NumberProgressBar.dll",
				}
			}
		}
	},

	Samples = new ISolutionBuilder [] {
		new DefaultSolutionBuilder { SolutionPath = "./samples/NumberProgressBarSample.sln" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.NumberProgressBar.nuspec" },
	},

	Components = new [] {
		new Component {ManifestDirectory = "./component"},
	},
};

Task ("externals")
	.Does (() => 
{
	if (!DirectoryExists ("./externals/"))
		CreateDirectory ("./externals/");

	if (!FileExists (AAR_FILE))
		DownloadFile (AAR_URL, AAR_FILE);
});

Task ("clean").IsDependentOn ("clean-base").Does (() => 
{	
	DeleteFiles ("./externals/*.aar");
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

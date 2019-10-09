
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var AAR_VERSION = "1.0.7";
var AAR_URL = string.Format ("http://search.maven.org/remotecontent?filepath=com/flaviofaria/kenburnsview/{0}/kenburnsview-{0}.aar", AAR_VERSION);
var AAR_FILE = "KenBurnsView.aar";

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/KenBurnsView.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/KenBurnsView/bin/Release/KenBurnsView.dll",
				}
			}
		}
	},

	Samples = new ISolutionBuilder [] {
		new DefaultSolutionBuilder { SolutionPath = "./samples/KenBurnsViewSample.sln" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.SortableTableView.nuspec" },
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
		
	DownloadFile (AAR_URL, "./externals/" + AAR_FILE);
});


Task ("clean").IsDependentOn ("clean-base").Does (() => 
{	
	DeleteFiles ("./externals/*.aar");
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

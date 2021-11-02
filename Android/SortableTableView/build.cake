
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "ci"));

var AAR_VERSION = "2.8.0";
var AAR_URL = $"https://bintray.com/artifact/download/ischwarz/maven/de/codecrafters/tableview/tableview/{AAR_VERSION}/tableview-{AAR_VERSION}.aar";
var AAR_FILE = "SortableTableView.aar";

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/SortableTableView.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/SortableTableView/bin/Release/SortableTableView.dll",
				}
			}
		}
	},

	Samples = new ISolutionBuilder [] {
		new DefaultSolutionBuilder { SolutionPath = "./samples/SortableTableViewSample.sln" },
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
		
	Information($"Downloading :");
	Information($"		{AAR_URL}");
	Information($"to :");
	Information($"		{AAR_FILE}");

	DownloadFile (AAR_URL, "./externals/" + AAR_FILE);		
});

Task ("clean").IsDependentOn ("clean-base").Does (() => 
{	
	DeleteFiles ("./externals/*.aar");
});

Task("ci")
	//.IsDependentOn("nuget")
	.Does 
	(
		() => 
		{
			Warning($"Not available (moljac 2021-05-08) :");
			Information($"		{AAR_URL}");
		}
	);

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

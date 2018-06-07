
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var AAR_VERSION = "2.7.0";
var AAR_URL = string.Format ("http://repo1.maven.org/maven2/se/emilsjolander/stickylistheaders/{0}/stickylistheaders-{0}.aar", AAR_VERSION);
var AAR_FILE = "StickyListHeaders.aar";

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/StickyListHeaders.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/StickyListHeaders/bin/Release/StickyListHeaders.dll",
					ToDirectory = "./output/"
				}
			}
		}
	},

	Samples = new ISolutionBuilder [] {
		new DefaultSolutionBuilder { SolutionPath = "./samples/StickyListHeadersSample.sln" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.StickyListHeaders.nuspec" },
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


#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var AAR_VERSION = "1.2.4";
var AAR_URL = string.Format ("http://search.maven.org/remotecontent?filepath=com/github/chrisbanes/photoview/library/{0}/library-{0}.aar", AAR_VERSION);
var AAR_FILE = "PhotoView.aar";

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/PhotoView.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/PhotoView/bin/Release/PhotoView.dll",
				}
			}
		}
	},

	Samples = new ISolutionBuilder [] {
		new DefaultSolutionBuilder { SolutionPath = "./samples/PhotoViewSample.sln" },
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

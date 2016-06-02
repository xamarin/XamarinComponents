
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var AAR_VERSION = "1.0.4";
var AAR_URL = string.Format ("https://bintray.com/artifact/download/michelelacorte/maven/it/michelelacorte/elasticprogressbar/library/{0}/library-{0}.aar", AAR_VERSION);
var AAR_FILE = "./externals/ElasticProgressBar.aar";

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/ElasticProgressBar.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/ElasticProgressBar/bin/Release/ElasticProgressBar.dll",
				}
			}
		}
	},

	Samples = new ISolutionBuilder [] {
		new DefaultSolutionBuilder { SolutionPath = "./samples/ElasticProgressBarSample.sln" },
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

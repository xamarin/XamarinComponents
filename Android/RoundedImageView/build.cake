
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var AAR_VERSION = "2.3.0";
var AAR_URL = string.Format ("http://search.maven.org/remotecontent?filepath=com/makeramen/roundedimageview/{0}/roundedimageview-{0}.aar", AAR_VERSION);
var AAR_FILE = "RoundedImageView.aar";

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/RoundedImageView.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/RoundedImageView/bin/Release/RoundedImageView.dll",
				}
			}
		}
	},

	Samples = new ISolutionBuilder [] {
		new DefaultSolutionBuilder { SolutionPath = "./samples/RoundedImageViewSample.sln" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Android.RoundedImageView.nuspec" },
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

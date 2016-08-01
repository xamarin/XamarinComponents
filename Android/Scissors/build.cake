
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var AAR_VERSION = "1.1.1";
var AAR_URL = string.Format ("http://search.maven.org/remotecontent?filepath=com/lyft/scissors/{0}/scissors-{0}.aar", AAR_VERSION);
var AAR_DEST = "./externals/Scissors.aar";
var JAR_DIR = "./externals/Scissors/";
var JAR_DEST = "./externals/Scissors/classes.jar";

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/Scissors.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/Scissors/bin/Release/Lyft.Scissors.dll",
				},
				new OutputFileCopy {
					FromFile = "./source/Scissors.Picasso/bin/Release/Lyft.Scissors.Picasso.dll",
				}
			}
		}
	},

	Samples = new ISolutionBuilder [] {
		new DefaultSolutionBuilder { SolutionPath = "./samples/ScissorsSample.sln" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Scissors.nuspec" },
		new NuGetInfo { NuSpec = "./nuget/Scissors.Picasso.nuspec" },
	},

	Components = new [] {
		new Component {ManifestDirectory = "./component"},
	},
};

Task ("externals")
	.Does (() => 
{
	if (!DirectoryExists ("./externals/"))
		CreateDirectory ("./externals");

	if (!FileExists (AAR_DEST))
		DownloadFile (AAR_URL, AAR_DEST);
	if (!FileExists (JAR_DEST))
		Unzip (AAR_DEST, JAR_DIR);
});


Task ("clean").IsDependentOn ("clean-base").Does (() => 
{	
	DeleteFiles ("./externals/*.aar");
	DeleteFiles ("./externals/Scissors/*.*");
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

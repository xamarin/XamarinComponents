
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var JAR_VERSION = "2.4.0";
var JAR_URL = string.Format ("http://search.maven.org/remotecontent?filepath=com/nineoldandroids/library/{0}/library-{0}.jar", JAR_VERSION);
var JAR_FILE = "NineOldAndroids.jar";

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/NineOldAndroids.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/NineOldAndroids/bin/Release/NineOldAndroids.dll",
				}
			}
		}
	},

	Samples = new ISolutionBuilder [] {
		new DefaultSolutionBuilder { SolutionPath = "./samples/NineOldAndroidsSample.sln" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/NineOldAndroids.nuspec" },
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

	DownloadFile (JAR_URL ,"./externals/" + JAR_FILE);	
});


Task ("clean").IsDependentOn ("clean-base").Does (() => 
{
	DeleteFiles ("./externals/*.jar");
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

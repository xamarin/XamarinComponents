
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var JAR_VERSION = "1.4.5";
var JAR_URL = string.Format ("http://search.maven.org/remotecontent?filepath=com/bartoszlipinski/viewpropertyobjectanimator/{0}/viewpropertyobjectanimator-{0}.aar", JAR_VERSION);
var JAR_DEST = "./externals/ViewPropertyObjectAnimator.aar";

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/Xamarin.Android.ViewPropertyObjectAnimator.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/Xamarin.Android.ViewPropertyObjectAnimator/bin/Release/Xamarin.Android.ViewPropertyObjectAnimator.dll",
				}
			}
		}
	},

	Samples = new ISolutionBuilder [] {
		new DefaultSolutionBuilder { SolutionPath = "./samples/ViewPropertyObjectAnimatorSample.sln" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Android.ViewPropertyObjectAnimator.nuspec" },
	},

	// Components = new [] {
	// 	new Component { ManifestDirectory = "./component" },
	// },
};

Task ("externals")
	.Does (() => 
{
	if (!DirectoryExists ("./externals/"))
		CreateDirectory ("./externals");

	if (!FileExists (JAR_DEST))
		DownloadFile (JAR_URL, JAR_DEST);
});


Task ("clean").IsDependentOn ("clean-base").Does (() => 
{	
	DeleteFiles ("./externals/*.aar");
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

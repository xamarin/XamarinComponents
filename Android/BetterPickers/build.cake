
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var LIB_VERSION = "1.6.0";
var AAR_URL = string.Format ("http://search.maven.org/remotecontent?filepath=com/doomonafireball/betterpickers/library/{0}/library-{0}.aar", LIB_VERSION);

var ASB_LIB_VERSION = "1.4.0";
var ASB_AAR_URL = string.Format ("https://bintray.com/artifact/download/bod/JRAF/org/jraf/android-switch-backport/{0}/android-switch-backport-{0}.aar", ASB_LIB_VERSION);

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./BetterPickers.sln",
			OutputFiles = new [] { 
				new OutputFileCopy { FromFile = "./source/BetterPickers/bin/Release/BetterPickers.dll" },
				new OutputFileCopy { FromFile = "./source/AndroidSwitchBackport/bin/Release/AndroidSwitchBackport.dll" }
			}
		}
	},

	Samples = new ISolutionBuilder [] {
		new DefaultSolutionBuilder { SolutionPath = "./samples/BetterPickersSample.sln" },	
	},

	Components = new [] {
		new Component { ManifestDirectory = "./component" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.BetterPickers.nuspec" },
	},
};

Task ("externals").IsDependentOn ("externals-base")
	.WithCriteria (!FileExists ("./externals/BetterPickers.aar"))
	.WithCriteria (!FileExists ("./externals/AndroidSwitchBackport.aar"))
	.Does (() => 
{
	if (!DirectoryExists ("./externals/"))
		CreateDirectory ("./externals");

	DownloadFile (AAR_URL, "./externals/BetterPickers.aar");
	DownloadFile (ASB_AAR_URL, "./externals/AndroidSwitchBackport.aar");
});


Task ("clean").IsDependentOn ("clean-base").Does (() => 
{
	if (DirectoryExists ("./externals/"))
		DeleteDirectory ("./externals", true);
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

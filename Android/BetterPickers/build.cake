
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "ci"));

var LIB_VERSION = "1.6.0";
var AAR_URL = $"https://repo1.maven.org/maven2/com/doomonafireball/betterpickers/library/{LIB_VERSION}/library-{LIB_VERSION}.aar";

var  ASB_LIB_VERSION = "1.4.0";
var ASB_AAR_URL = $"https://dl.bintray.com/bod/JRAF/org/jraf/android-switch-backport/{ASB_LIB_VERSION}/android-switch-backport-{ASB_LIB_VERSION}.aar";

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

	Information($"Downloading :");
	Information($"		{AAR_URL}");

	Information($"Downloading :");
	Information($"		{ASB_AAR_URL}");

	DownloadFile (AAR_URL, "./externals/BetterPickers.aar");
	DownloadFile (ASB_AAR_URL, "./externals/AndroidSwitchBackport.aar");
});


Task ("clean").IsDependentOn ("clean-base").Does (() => 
{
	if (DirectoryExists ("./externals/"))
		DeleteDirectory ("./externals", true);
});

Task("ci")
	//.IsDependentOn("nuget")
	.Does 
	(
		() => 
		{
			Warning($"Not available (moljac 2021-05-08) :");
			Information($"		{ASB_AAR_URL}");
		}
	);
	
SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

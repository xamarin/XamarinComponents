
#load "../../../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var NUGET_VERSION = "11.4.0.0";
var VERSION = "v11.4.0";
var URL = string.Format ("https://github.com/stripe/stripe-ios/releases/download/{0}/StripeiOS-Static.zip", VERSION);

var buildSpec = new BuildSpec () {
	Libs = new [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/Stripe.iOS/Stripe.iOS.sln",
			OutputFiles = new [] { 
				new OutputFileCopy { FromFile = "./source/Stripe.iOS/bin/Release/Stripe.iOS.dll" },
			}
		},	
	},

	Samples = new [] {
		new IOSSolutionBuilder { SolutionPath = "samples/Stripe.UIExamples/Stripe.UIExamples.sln",  Configuration = "Release", Platform="iPhone" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Stripe.iOS.nuspec", Version = NUGET_VERSION },
	},
};

Task ("externals")
	.IsDependentOn ("externals-base")
	.WithCriteria (!FileExists ("./externals/Stripe.framework/Versions/A/Stripe"))
	.Does (() =>
{
	EnsureDirectoryExists ("./externals");
	
	DownloadFile (URL, "./externals/StripeiOS-Static.zip");
	Unzip ("./externals/StripeiOS-Static.zip", "./externals/");
});

Task ("clean").IsDependentOn ("clean-base").Does (() =>
{
	if (DirectoryExists ("./externals"))
		DeleteDirectory ("./externals", true);
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

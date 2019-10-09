
#load "../../../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var NUGET_ANDROID_VERSION = "5.1.1";
var ANDROID_VERSION = "5.1.1";

var NUGET_ANDROID_PAY_VERSION = "5.1.0";
var ANDROID_PAY_VERSION = "5.1.0";
var ANDROIDURL = string.Format ("https://search.maven.org/remotecontent?filepath=com/stripe/stripe-android/{0}/stripe-android-{0}.aar", ANDROID_VERSION);
var ANDROIDDOCSURL = string.Format ("https://search.maven.org/remotecontent?filepath=com/stripe/stripe-android/{0}/stripe-android-{0}-javadoc.jar", ANDROID_VERSION);
var ANDROIDPAYURL = string.Format ("https://search.maven.org/remotecontent?filepath=com/stripe/stripe-android-pay/{0}/stripe-android-pay-{0}.aar", ANDROID_PAY_VERSION);
var ANDROIDPAYDOCSURL = string.Format ("https://search.maven.org/remotecontent?filepath=com/stripe/stripe-android-pay/{0}/stripe-android-pay-{0}-javadoc.jar", ANDROID_PAY_VERSION);

var buildSpec = new BuildSpec () {
	Libs = new [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/Stripe.Android.sln",
			OutputFiles = new [] { 
				new OutputFileCopy { FromFile = "./source/Stripe.Android/bin/Release/Stripe.Android.dll" },
				new OutputFileCopy { FromFile = "./source/Stripe.Android.Pay/bin/Release/Stripe.Android.Pay.dll" },
			}
		},	
	},

	// Samples = new [] {
	// 	new DefaultSolutionBuilder { SolutionPath = "samples/Stripe.UIExamples/Stripe.UIExamples.sln",  Configuration = "Release" },
	// },

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Stripe.Android.nuspec", Version = NUGET_ANDROID_VERSION },
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Stripe.Android.Pay.nuspec", Version = NUGET_ANDROID_PAY_VERSION },
	},
};

Task ("externals")
	.IsDependentOn ("externals-base")
	.WithCriteria (!FileExists ("./externals/stripe-android.aar"))
	.Does (() =>
{
	EnsureDirectoryExists ("./externals");
	
	DownloadFile (ANDROIDURL, "./externals/stripe-android.aar");
	DownloadFile (ANDROIDPAYURL, "./externals/stripe-android-pay.aar");

	DownloadFile (ANDROIDDOCSURL, "./externals/stripe-android-javadoc.jar");
	Unzip ("./externals/stripe-android-javadoc.jar", "./externals/stripe-android-javadoc/");

	DownloadFile (ANDROIDPAYDOCSURL, "./externals/stripe-android-pay-javadoc.jar");
	Unzip ("./externals/stripe-android-pay-javadoc.jar", "./externals/stripe-android-pay-javadoc/");
});

Task ("clean").IsDependentOn ("clean-base").Does (() =>
{
	if (DirectoryExists ("./externals"))
		DeleteDirectory ("./externals", true);
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

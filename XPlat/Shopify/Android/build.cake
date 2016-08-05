#load "../../../common.cake"

var VERSION = "1.2.4";
var URL = string.Format ("https://bintray.com/shopify/shopify-android/download_file?file_path=com%2Fshopify%2Fmobilebuysdk%2Fbuy%2F{0}%2Fbuy-{0}.aar", VERSION);

var TARGET = Argument ("t", Argument ("target", "Default"));

var buildSpec = new BuildSpec {
	Libs = new [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/Shopify.sln",
			Configuration = "Release",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/Shopify.Android/bin/Release/Shopify.Android.dll",
				},			
			}
		},	
	},

	Samples = new [] {
		new DefaultSolutionBuilder { SolutionPath = "./samples/ShopifyAndroidSample.sln" },
		new DefaultSolutionBuilder { SolutionPath = "./samples/ShopifyAndroidAsyncSample.sln" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Shopify.Android.nuspec", Version = VERSION },
	},

	Components = new [] {
		new Component { ManifestDirectory = "./component" }
	},
};


Task ("externals")
	.IsDependentOn ("externals-base")
	.WithCriteria (!FileExists ("./externals/mobile-buy-sdk-android.aar"))
	.Does (() => 
{
	EnsureDirectoryExists ("./externals/");
	DownloadFile (URL, "./externals/mobile-buy-sdk-android.aar");
});

Task ("clean").IsDependentOn ("clean-base").Does (() =>
{
	if (DirectoryExists ("./externals/"))
		DeleteDirectory ("./externals/", true);
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

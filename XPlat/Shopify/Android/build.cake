#load "../../../common.cake"

var VERSION = "1.2.4";
var URL = $"https://github.com/Shopify/mobile-buy-sdk-android/archive/{VERSION}.zip";

var TARGET = Argument ("t", Argument ("target", "ci"));

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

	Information($"Downloading :");
	Information($"		{URL}");

	DownloadFile (URL, "./externals/mobile-buy-sdk-android.aar");
});

Task ("clean").IsDependentOn ("clean-base").Does (() =>
{
	if (DirectoryExists ("./externals/"))
		DeleteDirectory ("./externals/", true);
});

Task("ci")
	.IsDependentOn("nuget")
	.Does 
	(
		() => 
		{
			// Warning($"Not available (moljac 2021-05-08) :");
			// Information($"		{URL}");
		}
	);


SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

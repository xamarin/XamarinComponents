#load "../../../common.cake"

var VERSION = "1.2.6";
var URL = $"https://github.com/Shopify/mobile-buy-sdk-ios/archive/{VERSION}.zip";

var TARGET = Argument ("t", Argument ("target", "ci"));

var buildSpec = new BuildSpec {
	Libs = new [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/Shopify.sln",
			Configuration = "Release",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/Shopify.iOS/bin/Release/Shopify.iOS.dll",
					ToDirectory = "./output"
				},
			}
		},	
	},

	Samples = new [] {
		new IOSSolutionBuilder { SolutionPath = "./samples/ShopifyiOSSample.sln",  Configuration = "Release", Platform="iPhone" },
		new IOSSolutionBuilder { SolutionPath = "./samples/ShopifyiOSAsyncSample.sln",  Configuration = "Release", Platform="iPhone" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Shopify.iOS.nuspec", Version = VERSION },
	},

	Components = new [] {
		new Component { ManifestDirectory = "./component" }
	},
};


Task ("externals")
	.IsDependentOn ("externals-base")
	.WithCriteria (!FileExists ("./externals/libMobile-Buy-SDK.a"))
	.Does (() => 
{
	if (!DirectoryExists ("./externals"))
		CreateDirectory ("./externals");

    if (!FileExists ("./externals/mobile-buy-sdk-ios.zip"))
        DownloadFile (URL, "./externals/mobile-buy-sdk-ios.zip");
        
    var temp = "./externals/mobile-buy-sdk-ios-" + VERSION;
	if (!DirectoryExists (temp))
	   Unzip ("./externals/mobile-buy-sdk-ios.zip", "./externals/");

	XCodeBuild (new XCodeBuildSettings {
		Project = temp + "/Mobile Buy SDK/Mobile Buy SDK.xcodeproj",
		Target = "Buy Static",
		Sdk = "iphoneos",
		Configuration = "Release",
	});

	XCodeBuild (new XCodeBuildSettings {
		Project = temp + "/Mobile Buy SDK/Mobile Buy SDK.xcodeproj",
		Target = "Buy Static",
		Sdk = "iphonesimulator",
		Configuration = "Release",
	});

	RunLipoCreate ("./", "./externals/libMobile-Buy-SDK.a",
	    // inputs
		temp + "/Mobile Buy SDK/build/Release-iphoneos/Buy.framework/Buy",
		temp + "/Mobile Buy SDK/build/Release-iphonesimulator/Buy.framework/Buy");
});

Task ("clean").IsDependentOn ("clean-base").Does (() =>
{
	if (DirectoryExists ("./externals/"))
		DeleteDirectory ("./externals/", true);
});

Task("ci")
	//.IsDependentOn("nuget")
	.Does 
	(
		() => 
		{
			Warning($"iOS build need love - fails with exit code 65 (moljac 2021-05-08) :");
		}
	);



SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

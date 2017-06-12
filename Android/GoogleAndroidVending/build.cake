
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var RepositoryUrlRoot = "https://dl.google.com/android/repository/";
var RepositoryUrl = RepositoryUrlRoot + "addon.xml";
var RepositoryNS = "http://schemas.android.com/sdk/android/addon/7";
var LicensingKey = "market_licensing";
var LicensingVersion = "r02";
var ExpansionKey = "market_apk_expansion";
var ExpansionVersion = "r03";

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			BuildsOn = BuildPlatforms.Mac | BuildPlatforms.Windows,
			SolutionPath = "./source/Google.Android.Vending.sln",
			OutputFiles = new [] { 
				new OutputFileCopy { FromFile = "./source/Google.Android.Vending.Licensing/bin/Release/Google.Android.Vending.Licensing.dll" },
				new OutputFileCopy { FromFile = "./source/Google.Android.Vending.Expansion.ZipFile/bin/Release/Google.Android.Vending.Expansion.ZipFile.dll" },
				new OutputFileCopy { FromFile = "./source/Google.Android.Vending.Expansion.Downloader/bin/Release/Google.Android.Vending.Expansion.Downloader.dll" },
			}
		}
	},

	Samples = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			BuildsOn = BuildPlatforms.Mac | BuildPlatforms.Windows,
			SolutionPath = "./samples/LicensingSample.sln"
		},
		new DefaultSolutionBuilder {
			BuildsOn = BuildPlatforms.Mac | BuildPlatforms.Windows,
			SolutionPath = "./samples/SimpleDownloaderSample.sln"
		},
		new DefaultSolutionBuilder {
			BuildsOn = BuildPlatforms.Mac | BuildPlatforms.Windows,
			SolutionPath = "./samples/DownloaderSample.sln"
		},
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Google.Android.Vending.Licensing.nuspec" },
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Google.Android.Vending.Expansion.ZipFile.nuspec" },
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Google.Android.Vending.Expansion.Downloader.nuspec" },
	},

	// Components = new [] {
	// 	new Component { ManifestDirectory = "./component" },
	// },
};

Task ("externals")
	.Does (() => 
{
	EnsureDirectoryExists("./externals/");

	// download the Java code
	var download = new Action<string, string>((key, version) => {
		var dest = "./externals/" + key + ".zip";
		if (!FileExists(dest)) {
			DownloadFile(RepositoryUrlRoot + string.Format("{0}-{1}.zip", key, version), dest);
			Unzip(dest, "./externals/");
		}
	});
	download(ExpansionKey, ExpansionVersion);
	download(LicensingKey, LicensingVersion);

	// Build the Java projects
	var result = StartProcess(IsRunningOnWindows() ? "cmd" : "sh", new ProcessSettings {
		Arguments = (IsRunningOnWindows() ? "/c gradlew" : "gradlew") + " bundleRelease",
		WorkingDirectory = "native"
	});
	if (result != 0) {
		throw new Exception("gradlew returned " + result);
	}
});


Task ("clean").IsDependentOn ("clean-base").Does (() => 
{	
	DeleteDirectory ("./externals/", true);
	CleanDirectories ("./native/market_*/build");
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

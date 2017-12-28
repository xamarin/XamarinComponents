
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var LicensingVersion = "daaed06de2e298783aa645121ede4ea2cb880fa1";
var ExpansionVersion = "b99c38aa4ce05551a4d5d74c3a4cec0a1b80d275";

var LicensingUrl = "https://github.com/google/play-licensing/archive/" + LicensingVersion + ".zip";
var ExpansionUrl = "https://github.com/google/play-apk-expansion/archive/" + ExpansionVersion + ".zip";

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
	var download = new Action<string, string, string>((url, name, version) => {
		var dest = "./externals/" + name + ".zip";
		if (!FileExists(dest)) {
			DownloadFile(url, dest);
			Unzip(dest, "./externals/");
			MoveDirectory("./externals/" + name + "-" + version, "./externals/" + name);
		}
	});
	download(LicensingUrl, "play-licensing", LicensingVersion);
	download(ExpansionUrl, "play-apk-expansion", ExpansionVersion);

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

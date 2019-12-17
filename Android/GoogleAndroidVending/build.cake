
var TARGET = Argument ("t", Argument ("target", "ci"));

var LicensingVersion = "89ebdfcf52562018ff4c366055f0e35393919763";
var ExpansionVersion = "9ecf54e5ce7c5a74a2eeedcec4d940ea52b16f0e";

var LicensingUrl = "https://github.com/google/play-licensing/archive/" + LicensingVersion + ".zip";
var ExpansionUrl = "https://github.com/google/play-apk-expansion/archive/" + ExpansionVersion + ".zip";

Task("libs")
	.IsDependentOn("externals")
	.Does(() =>
{
	MSBuild("./source/Google.Android.Vending.sln", c => {
		c.Configuration = "Release";
		c.Restore = true;
		c.Properties.Add("DesignTimeBuild", new [] { "false" });
	});
});

Task("nuget")
	.IsDependentOn("libs")
	.Does(() =>
{
	MSBuild ("./source/Google.Android.Vending.sln", c => {
		c.Configuration = "Release";
		c.Targets.Clear();
		c.Targets.Add("Pack");
		c.Properties.Add("PackageOutputPath", new [] { MakeAbsolute(new FilePath("./output")).FullPath });
		c.Properties.Add("PackageRequireLicenseAcceptance", new [] { "true" });
		c.Properties.Add("DesignTimeBuild", new [] { "false" });
	});
});

Task("samples")
	.IsDependentOn("nuget")
	.Does(() =>
{
	var samples = new List<string> {
		"./samples/LicensingSample.sln",
		"./samples/SimpleDownloaderSample.sln",
		"./samples/DownloaderSample.sln"
	};

	foreach (var s in samples) {
		MSBuild (s, c => {
			c.Configuration = "Release";
			c.Targets.Clear();
			c.Targets.Add("Restore");
			c.Properties.Add("DesignTimeBuild", new [] { "false" });
		});

		MSBuild(s, c => {
			c.Configuration = "Release";
			c.Properties.Add("DesignTimeBuild", new [] { "false" });
		});
	}
});

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


Task ("clean")
	.Does (() => 
{	
	DeleteDirectory ("./externals/", true);
	CleanDirectories ("./native/market_*/build");
});


Task("ci")
	.IsDependentOn("nuget")
	.IsDependentOn("samples");

RunTarget (TARGET);

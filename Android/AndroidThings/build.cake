//#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "ci"));

var NUGET_VERSION = "1.0.0.1";

var JAR_VERSION = "1.0";
var JAR_URL = $"https://bintray.com/google/androidthings/download_file?file_path=com%2Fgoogle%2Fandroid%2Fthings%2Fandroidthings%2F{JAR_VERSION}%2Fandroidthings-{JAR_VERSION}.jar";
var DOCS_URL = $"https://bintray.com/google/androidthings/download_file?file_path=com%2Fgoogle%2Fandroid%2Fthings%2Fandroidthings%2F{JAR_VERSION}%2Fandroidthings-{JAR_VERSION}-javadoc.jar";

var CONTRIB_URL = "https://bintray.com/google/androidthings/download_file?file_path=com%2Fgoogle%2Fandroid%2Fthings%2Fcontrib%2Fdriver-{0}%2F{1}%2Fdriver-{0}-{1}.aar";
var CONTRIB_SRC_URL = "https://bintray.com/google/androidthings/download_file?file_path=com%2Fgoogle%2Fandroid%2Fthings%2Fcontrib%2Fdriver-{0}%2F{1}%2Fdriver-{0}-{1}-sources.jar";

var JAR_DEST = "./externals/androidthings.jar";

var CONTRIB_DRIVERS = new [] {
	"adcv2x",
	"apa102",
	"bmx280",
	"button",
	"cap12xx",
	"gps",
	"ht16k33",
	"lowpan",
	"matrixkeypad",
	"mma7660fc",
	"motorhat",
	"pwmservo",
	"pwmspeaker",
	"rainbowhat",
	"sensehat",
	"ssd1306",
	"tm1637",
	"vcnl4200",
	"voicehat",
	"zxgesturesensor"
};

Task("libs")
	.IsDependentOn("externals")
	.Does(() =>
{
	MSBuild("./Android.Things.sln", c => 
		c.SetConfiguration("Release")
			.WithTarget("Restore"));

	MSBuild("./Android.Things.sln", c => 
		c.SetConfiguration("Release")
			.WithTarget("Build"));
});

Task("nuget")
	.IsDependentOn("libs")
	.Does(() =>
{
	EnsureDirectoryExists("./output");

	MSBuild("./source/things/Xamarin.Android.Things.csproj", c => 
		c.SetConfiguration("Release")
			.WithTarget("Pack")
			.WithProperty("PackageVersion", NUGET_VERSION)
			.WithProperty("PackageOutputPath", "../../output"));

	MSBuild("./source/drivers/Xamarin.Android.Things.Contrib.Drivers.csproj", c => 
		c.SetConfiguration("Release")
			.WithTarget("Pack")
			.WithProperty("PackageVersion", NUGET_VERSION)
			.WithProperty("PackageOutputPath", "../../output"));
});

Task("samples")
	.IsDependentOn("nuget");
Task("component");

Task("ci")
	.IsDependentOn("samples");

Task ("externals")
	.Does (() => 
{
	if (!DirectoryExists ("./externals/"))
		CreateDirectory ("./externals");

	if (!FileExists (JAR_DEST))
		DownloadFile (JAR_URL, JAR_DEST);

	if (!FileExists ("./externals/docs.zip")) {
		DownloadFile (DOCS_URL, "./externals/docs.zip");
		Unzip ("./externals/docs.zip", "./externals/docs");
	}

	foreach (var c in CONTRIB_DRIVERS) {
		var aarFile = "./externals/driver-" + c + ".aar";
		var srcsFile = "./externals/driver-" + c + "-sources.jar";

		if (!FileExists(aarFile))
			DownloadFile(string.Format(CONTRIB_URL, c, JAR_VERSION), aarFile);

		if (!FileExists(srcsFile))
			DownloadFile(string.Format(CONTRIB_SRC_URL, c, JAR_VERSION), srcsFile);
	}
});

Task ("clean")
	.Does (() => 
{
	if (DirectoryExists ("./externals"))
		DeleteDirectory ("./externals", new DeleteDirectorySettings { Force=true });
});

RunTarget (TARGET);
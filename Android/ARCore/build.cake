//#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "ci"));

var NUGET_VERSION = "1.25.0";

var AAR_VERSION = "1.25.0";
var AAR_URL = string.Format("https://dl.google.com/dl/android/maven2/com/google/ar/core/{0}/core-{0}.aar", AAR_VERSION);
var OBJ_VERSION = "0.3.0";
var OBJ_URL = string.Format("https://oss.sonatype.org/content/repositories/releases/de/javagl/obj/{0}/obj-{0}.jar", OBJ_VERSION);

Task ("externals")
	.Does (() =>
{
	var AAR_FILE = "./externals/arcore.aar";
	var OBJ_JAR_FILE = "./externals/obj.jar";

	if (!DirectoryExists ("./externals/"))
		CreateDirectory ("./externals");

	if (!FileExists (AAR_FILE))
		DownloadFile (AAR_URL, AAR_FILE);

	if (!FileExists (OBJ_JAR_FILE))
		DownloadFile (OBJ_URL, OBJ_JAR_FILE);
});

Task("libs")
	.IsDependentOn("externals")
	.Does(() =>
{
	MSBuild("./ARCore.sln", c => {
		c.Configuration = "Release";
		c.Targets.Clear();
		c.Targets.Add("Restore");
		c.Targets.Add("Build");
		c.Properties.Add("DesignTimeBuild", new [] { "false" });
		c.MaxCpuCount = 0;
	});
});

Task("nuget")
	.IsDependentOn("libs")
	.Does(() =>
{
	var nuGetPackSettings = new NuGetPackSettings {
		BasePath                 = "./",
		OutputDirectory          = "./output",
		RequireLicenseAcceptance = true,
		Version                  = NUGET_VERSION,
	};

	NuGetPack("./nuget/Xamarin.Google.ARCore.nuspec", nuGetPackSettings);
});

Task("samples")
	.IsDependentOn("nuget")
	.Does (() =>
{
	MSBuild("./samples/HelloAR.sln", c => {
		c.Configuration = "Release";
		c.Targets.Clear();
		c.Targets.Add("Restore");
		c.Targets.Add("Build");
	});
});

Task ("clean")
	.Does (() =>
{
	if (DirectoryExists ("./externals/"))
		DeleteDirectory ("./externals", new DeleteDirectorySettings { Force=true });
});

Task("ci")
	.IsDependentOn("samples");

RunTarget (TARGET);

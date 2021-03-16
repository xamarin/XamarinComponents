
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "ci"));

var AAR_VERSION = "1.3.0";
var JAR_VERSION = "1.5.0";

var NUGET_VERSION = AAR_VERSION;
var AAR_URL = $"https://search.maven.org/remotecontent?filepath=com/jakewharton/threetenabp/threetenabp/{AAR_VERSION}/threetenabp-{AAR_VERSION}.aar";
var JAR_URL = $"https://search.maven.org/remotecontent?filepath=org/threeten/threetenbp/{JAR_VERSION}/threetenbp-{JAR_VERSION}-no-tzdb.jar";

Task ("externals")
	.WithCriteria (!FileExists ("./externals/threetenabp.aar"))
	.Does (() =>
{
	EnsureDirectoryExists ("./externals");
	
	// Download Dependencies
	DownloadFile (AAR_URL, "./externals/threetenabp.aar");
	DownloadFile (JAR_URL, "./externals/threetenbp-no-tzdb.jar");

	// Update .csproj nuget versions
	XmlPoke("./source/ThreeTenAbp/ThreeTenAbp.csproj", "/Project/PropertyGroup/PackageVersion", NUGET_VERSION);
});


Task("libs")
	.IsDependentOn("externals")
	.Does(() =>
{
	MSBuild("./ThreeTenAbp.sln", c => {
		c.Configuration = "Release";
		c.Restore = true;
		c.MaxCpuCount = 0;
		c.Properties.Add("DesignTimeBuild", new [] { "false" });
	});
});

Task("nuget")
	.IsDependentOn("libs")
	.Does(() =>
{
	MSBuild ("./ThreeTenAbp.sln", c => {
		c.Configuration = "Release";
		c.MaxCpuCount = 0;
		c.Targets.Clear();
		c.Targets.Add("Pack");
		c.Properties.Add("PackageOutputPath", new [] { MakeAbsolute(new FilePath("./output")).FullPath });
		c.Properties.Add("PackageRequireLicenseAcceptance", new [] { "true" });
		c.Properties.Add("DesignTimeBuild", new [] { "false" });
	});
});

Task("samples")
	.IsDependentOn("nuget");

Task("ci")
	.IsDependentOn("externals")
	.IsDependentOn("nuget")
	.IsDependentOn("samples");

Task ("clean")
	.Does (() =>
{
	if (DirectoryExists ("./externals/"))
		DeleteDirectory ("./externals", new DeleteDirectorySettings {
			Recursive = true,
			Force = true
		});
});

RunTarget (TARGET);

var TARGET = Argument ("t", Argument ("target", "ci"));

var NUGET_VERSION = "3.3.3";

var JAR_VERSION = "3.3.3";
var JAR_URL = string.Format ("http://search.maven.org/remotecontent?filepath=com/google/zxing/core/{0}/core-{0}.jar", JAR_VERSION);
var JAR_DEST = "./externals/zxing.core.jar";

Task ("externals")
	.WithCriteria (!FileExists (JAR_DEST))
	.Does (() =>
{
	EnsureDirectoryExists ("./externals");
	
	// Download Dependencies
	DownloadFile (JAR_URL, JAR_DEST);

	// Update .csproj nuget versions
	XmlPoke("./source/Google.ZXing.Core/Google.ZXing.Core.csproj", "/Project/PropertyGroup/PackageVersion", NUGET_VERSION);
});


Task("libs")
	.IsDependentOn("externals")
	.Does(() =>
{
	MSBuild("./source/GoogleZXing.sln", c => {
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
	MSBuild ("./source/GoogleZXing.sln", c => {
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
	.IsDependentOn("nuget")
	.Does (() =>
{
	MSBuild ("./samples/GoogleZXingSample.sln", c => {
		c.Configuration = "Release";
		c.MaxCpuCount = 0;
		c.Restore = true;
	});
});

Task("ci")
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
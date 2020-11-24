var TARGET = Argument ("t", Argument ("target", "ci"));

var DAGGERS_VERSION = "2.27";
var DAGGERS_NUGET_VERSION = DAGGERS_VERSION + ".0";
var DAGGERS_URL = $"https://repo1.maven.org/maven2/com/google/dagger/dagger/{DAGGERS_VERSION}/dagger-{DAGGERS_VERSION}.jar";

Task ("externals")
	.WithCriteria (!FileExists ("./externals/dagger.jar"))
	.Does (() =>
{
	EnsureDirectoryExists ("./externals");
	
	// Download Dependencies
	DownloadFile (DAGGERS_URL, "./externals/dagger.jar");

	// Update .csproj nuget versions
	XmlPoke("./source/dagger/dagger.csproj", "/Project/PropertyGroup/PackageVersion", DAGGERS_NUGET_VERSION);
});


Task("libs")
	.IsDependentOn("externals")
	.Does(() =>
{
	MSBuild("./GoogleDagger.sln", c => {
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
	MSBuild ("./GoogleDagger.sln", c => {
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

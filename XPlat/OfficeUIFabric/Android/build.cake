
var TARGET = Argument ("t", Argument ("target", "ci"));

var AAR_VERSION = "0.3.9";
var NUGET_VERSION = AAR_VERSION + "-preview01";
var AAR_URL = $"https://jcenter.bintray.com/com/microsoft/uifabric/OfficeUIFabric/{AAR_VERSION}/OfficeUIFabric-{AAR_VERSION}.aar";

Task ("externals")
	.WithCriteria (!FileExists ("./externals/OfficeUIFabric.aar"))
	.Does (() =>
{
	EnsureDirectoryExists ("./externals");
	
	// Download Dependencies
	DownloadFile (AAR_URL, "./externals/OfficeUIFabric.aar");

	// Update .csproj nuget versions
	XmlPoke("./source/OfficeUIFabric.Android.csproj", "/Project/PropertyGroup/PackageVersion", NUGET_VERSION);
});


Task("libs")
	.IsDependentOn("externals")
	.Does(() =>
{
	NuGetRestore("./OfficeUIFabric.sln", new NuGetRestoreSettings { });

	MSBuild("./OfficeUIFabric.sln", c => {
		c.Configuration = "Release";
		c.MaxCpuCount = 0;
		c.Properties.Add("DesignTimeBuild", new [] { "false" });
	});
});

Task("nuget")
	.IsDependentOn("libs")
	.Does(() =>
{
	MSBuild ("./OfficeUIFabric.sln", c => {
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

Task ("clean")
	.Does (() =>
{
	if (DirectoryExists ("./externals/"))
		DeleteDirectory ("./externals", new DeleteDirectorySettings {
			Recursive = true,
			Force = true
		});
});

Task("ci")
	.IsDependentOn("samples");

RunTarget (TARGET);

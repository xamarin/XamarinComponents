
var TARGET = Argument ("t", Argument ("target", "ci"));

var PODFILE_VERSION = "0.3.9";
var NUGET_VERSION = PODFILE_VERSION + "-preview01";

Task ("externals")
	.WithCriteria (!DirectoryExists ("./externals/OfficeUIFabric.framework"))
	.Does (() =>
{
	EnsureDirectoryExists ("./externals");
	
	// Download and build Dependencies
	StartProcess("make", new ProcessSettings
	{
		Arguments = "all",
		WorkingDirectory = "./externals/",
	});

	// Update .csproj nuget versions
	XmlPoke("./source/OfficeUIFabric.iOS.csproj", "/Project/PropertyGroup/PackageVersion", NUGET_VERSION);
});


Task("libs")
	.IsDependentOn("externals")
	.Does(() =>
{
	MSBuild("./OfficeUIFabric.sln", c => {
		c.Configuration = "Release";
		c.Restore = true;
		c.MaxCpuCount = 0;
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
	});
});

Task("samples")
	.IsDependentOn("nuget");

Task ("clean")
	.Does (() =>
{
	StartProcess("make", new ProcessSettings
	{
		Arguments = "clean",
		WorkingDirectory = "./externals/",
	});
});

Task("ci")
	.IsDependentOn("samples");


RunTarget (TARGET);

var TARGET = Argument ("t", Argument ("target", "ci"));

var NUGET_VERSION = "1.3.2";

var AAR_VERSION = "1.3.2";
var AAR_URL = $"https://repo1.maven.org/maven2/io/coil-kt/coil-base/{AAR_VERSION}/coil-base-{AAR_VERSION}.aar";

Task ("externals")
	.Does (() =>
{
	EnsureDirectoryExists ("./externals");
	
	DownloadFile(AAR_URL, $"./externals/coil-base-{AAR_VERSION}.aar");

	// Update .csproj nuget versions
	XmlPoke("./source/CoilBase/CoilBase.csproj", "/Project/PropertyGroup/PackageVersion", NUGET_VERSION);
});

Task("nuget")
	.IsDependentOn("externals")
	.Does(() =>
{
	MSBuild ("./source/CoilBase.sln", c => {
		c.Configuration = "Release";
		c.Restore = true;
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
	.Does(() =>
{
	
});

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


#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var ANNOTATIONS_VERSION = "1.6.6";
var ANNOTATIONS_NUGET_VERSION = ANNOTATIONS_VERSION;
var ANNOTATIONS_URL = $"https://repo1.maven.org/maven2/com/google/auto/value/auto-value-annotations/{ANNOTATIONS_VERSION}/auto-value-annotations-{ANNOTATIONS_VERSION}.jar";

Task ("externals")
	.WithCriteria (!FileExists ("./externals/auto-value-annotations.jar"))
	.Does (() =>
{
	EnsureDirectoryExists ("./externals");
	
	// Download Dependencies
	DownloadFile (ANNOTATIONS_URL, "./externals/auto-value-annotations.jar");

	// Update .csproj nuget versions
	XmlPoke("./source/Annotations/Annotations.csproj", "/Project/PropertyGroup/PackageVersion", ANNOTATIONS_NUGET_VERSION);
});


Task("libs")
	.IsDependentOn("externals")
	.Does(() =>
{
	MSBuild("./GoogleAutoValue.sln", c => {
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
	MSBuild ("./GoogleAutoValue.sln", c => {
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
	.IsDependentOn("libs")
	.IsDependentOn("nuget")
	.Does
	(
		() => {}
	);

RunTarget (TARGET);

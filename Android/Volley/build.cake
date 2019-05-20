
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var VOLLEY_VERSION = "1.1.1";
var XAMARIN_FIX_VERSION = "1";
var VOLLEY_NUGET_VERSION = $"{VOLLEY_VERSION}.{XAMARIN_FIX_VERSION}";
var VOLLEY_URL = $"http://repo.spring.io/libs-release/com/android/volley/volley/{VOLLEY_VERSION}/volley-{VOLLEY_VERSION}.aar";

Task ("externals")
	.WithCriteria (!FileExists ("./externals/volley.aar"))
	.Does (() =>
{
	EnsureDirectoryExists ("./externals");
	
	// Download Dependencies
	DownloadFile (VOLLEY_URL, "./externals/volley.aar");

	// Update .csproj nuget versions
	XmlPoke("./source/Volley/Volley.csproj", "/Project/PropertyGroup/PackageVersion", VOLLEY_NUGET_VERSION);
});


Task("libs")
	.IsDependentOn("externals")
	.Does(() =>
{
	MSBuild("./Volley.sln", c => {
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
	MSBuild ("./Volley.sln", c => {
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

RunTarget (TARGET);

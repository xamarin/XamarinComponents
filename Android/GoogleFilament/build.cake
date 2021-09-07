var TARGET = Argument ("t", Argument ("target", "ci"));

var FILAMENTS_VERSION = "1.10.7";
var FILAMENTS_NUGET_VERSION = FILAMENTS_VERSION;
var FILAMENT_URL = $"https://search.maven.org/remotecontent?filepath=com/google/android/filament/filament-android/{FILAMENTS_VERSION}/filament-android-{FILAMENTS_VERSION}.aar";
var GLTFIO_URL = $"https://search.maven.org/remotecontent?filepath=com/google/android/filament/gltfio-android/{FILAMENTS_VERSION}/gltfio-android-{FILAMENTS_VERSION}.aar";
var FILAMENT_UTILS_URL = $"https://search.maven.org/remotecontent?filepath=com/google/android/filament/filament-utils-android/{FILAMENTS_VERSION}/filament-utils-android-{FILAMENTS_VERSION}.aar";

Task ("externals")
	.WithCriteria (!FileExists ($"./externals/filament-android.aar"))
	.Does (() =>
{
	EnsureDirectoryExists ("./externals");
	
	// Download Dependencies
	DownloadFile (FILAMENT_URL, $"./externals/filament-android.aar");
	DownloadFile (GLTFIO_URL, $"./externals/gltfio-android.aar");
	DownloadFile (FILAMENT_UTILS_URL, $"./externals/filament-utils-android.aar");

	// Update .csproj nuget versions
	XmlPoke("./source/Filament/Filament.csproj", "/Project/PropertyGroup/PackageVersion", FILAMENTS_NUGET_VERSION);
	XmlPoke("./source/Gltfio/Gltfio.csproj", "/Project/PropertyGroup/PackageVersion", FILAMENTS_NUGET_VERSION);
	XmlPoke("./source/FilamentUtils/FilamentUtils.csproj", "/Project/PropertyGroup/PackageVersion", FILAMENTS_NUGET_VERSION);
});


Task("libs")
	.IsDependentOn("externals")
	.Does(() =>
{
	MSBuild("./GoogleFilament.sln", c => {
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
	MSBuild ("./GoogleFilament.sln", c => {
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

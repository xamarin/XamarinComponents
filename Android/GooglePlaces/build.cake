var TARGET = Argument ("t", Argument ("target", "Default"));

var PLACES_VERSION = "2.4.0";
var XAMARIN_FIX_VERSION = "0";
var PLACES_NUGET_VERSION = $"{PLACES_VERSION}.{XAMARIN_FIX_VERSION}";
var PLACES_URL = $"https://maven.google.com/com/google/android/libraries/places/places/{PLACES_VERSION}/places-{PLACES_VERSION}.aar";
var ANDROID_SDK_BUILD_TOOLS_VERSION = "30.0.2";

Task ("externals")
	.WithCriteria (!FileExists ("./externals/places.aar"))
	.Does (() =>
{
	EnsureDirectoryExists ("./externals");
	
	// Download Dependencies
	DownloadFile (PLACES_URL, "./externals/places.aar");

	// Update .csproj nuget versions
	XmlPoke("./source/GooglePlaces/GooglePlaces.csproj", "/Project/PropertyGroup/PackageVersion", PLACES_NUGET_VERSION);
});


Task("libs")
	.IsDependentOn("externals")
	.Does(() =>
{
	MSBuild("./GooglePlaces.sln", c => {
		c.Configuration = "Release";
		c.Restore = true;
		c.MaxCpuCount = 0;
		c.Targets.Clear();
		c.Targets.Add("GooglePlaces");
		c.Properties.Add("DesignTimeBuild", new [] { "false" });
		c.Properties.Add("AndroidSdkBuildToolsVersion", new [] { ANDROID_SDK_BUILD_TOOLS_VERSION });
	});
});

Task("nuget")
	.IsDependentOn("libs")
	.Does(() =>
{
	MSBuild ("./GooglePlaces.sln", c => {
		c.Configuration = "Release";
		c.MaxCpuCount = 0;
		c.Targets.Clear();
		c.Targets.Add("GooglePlaces:Pack");
		c.Properties.Add("PackageOutputPath", new [] { MakeAbsolute(new FilePath("./output")).FullPath });
		c.Properties.Add("PackageRequireLicenseAcceptance", new [] { "true" });
		c.Properties.Add("DesignTimeBuild", new [] { "false" });
		c.Properties.Add("AndroidSdkBuildToolsVersion", new [] { ANDROID_SDK_BUILD_TOOLS_VERSION });
	});
});

Task("samples")
	.IsDependentOn("nuget")
	.Does(() =>
{	
	MSBuild ("./GooglePlaces.sln", c => {
		c.Configuration = "Release";
		c.MaxCpuCount = 0;
		c.Targets.Clear();
		c.Targets.Add("PlacesSample");
		c.Properties.Add("DesignTimeBuild", new [] { "false" });
		c.Properties.Add("AndroidSdkBuildToolsVersion", new [] { ANDROID_SDK_BUILD_TOOLS_VERSION });
	});
});

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

Task ("Default")
	.IsDependentOn("ci");

RunTarget (TARGET);

#addin nuget:?package=SharpZipLib

var TARGET = Argument ("t", Argument ("target", "ci"));

var SF_VERSION = "1.17.1";

var NUGET_VERSION = "1.17.1";

var BASE_JAR_URL = $"https://dl.google.com/dl/android/maven2/com/google/ar/sceneform/sceneform-base/{SF_VERSION}/sceneform-base-{SF_VERSION}.aar";
var ANIMATION_JAR_URL = $"https://dl.google.com:443/dl/android/maven2/com/google/ar/sceneform/animation/{SF_VERSION}/animation-{SF_VERSION}.aar";
var ASSETS_JAR_URL = $"https://dl.google.com:443/dl/android/maven2/com/google/ar/sceneform/assets/{SF_VERSION}/assets-{SF_VERSION}.aar";
var CORE_JAR_URL = $"https://dl.google.com:443/dl/android/maven2/com/google/ar/sceneform/core/{SF_VERSION}/core-{SF_VERSION}.aar";
var FILAMENT_JAR_URL = $"https://dl.google.com:443/dl/android/maven2/com/google/ar/sceneform/filament-android/{SF_VERSION}/filament-android-{SF_VERSION}.aar";
var RENDERING_JAR_URL = $"https://dl.google.com:443/dl/android/maven2/com/google/ar/sceneform/rendering/{SF_VERSION}/rendering-{SF_VERSION}.aar";
var UX_JAR_URL = $"https://dl.google.com:443/dl/android/maven2/com/google/ar/sceneform/ux/sceneform-ux/{SF_VERSION}/sceneform-ux-{SF_VERSION}.aar";
var UX_JAR_URL_DOCS = $"https://dl.google.com:443/dl/android/maven2/com/google/ar/sceneform/ux/sceneform-ux/{SF_VERSION}/sceneform-ux-{SF_VERSION}-javadoc.jar";

Task ("externals")
	.WithCriteria (!FileExists ("./externals/sceneform-base.aar"))
	.Does (() =>
{
	EnsureDirectoryExists ("./externals/");

	// Download Dependencies
	Information ("Base Path: {0}", BASE_JAR_URL);
	DownloadFile (BASE_JAR_URL, "./externals/sceneform-base.aar");
	Information ("Animation Path: {0}", ANIMATION_JAR_URL);
	DownloadFile (ANIMATION_JAR_URL, "./externals/animation.aar");
	Information ("Assets Path: {0}", ASSETS_JAR_URL);
	DownloadFile (ASSETS_JAR_URL, "./externals/assets.aar");
	Information ("Core Path: {0}", CORE_JAR_URL);
	DownloadFile (CORE_JAR_URL, "./externals/core.aar");
	Information ("Filament Path: {0}", FILAMENT_JAR_URL);
	DownloadFile (FILAMENT_JAR_URL, "./externals/filament-android.aar");
	Information ("Rendering Path: {0}", RENDERING_JAR_URL);
	DownloadFile (RENDERING_JAR_URL, "./externals/rendering.aar");
	Information ("Rendering Path: {0}", UX_JAR_URL);
	DownloadFile (UX_JAR_URL, "./externals/ux.aar");

	// Update .csproj nuget versions
	XmlPoke("./source/Animation/Animation.csproj", "/Project/PropertyGroup/PackageVersion", NUGET_VERSION);
	XmlPoke("./source/Assets/Assets.csproj", "/Project/PropertyGroup/PackageVersion", NUGET_VERSION);
	XmlPoke("./source/Base/Base.csproj", "/Project/PropertyGroup/PackageVersion", NUGET_VERSION);
	XmlPoke("./source/Core/Core.csproj", "/Project/PropertyGroup/PackageVersion", NUGET_VERSION);
	XmlPoke("./source/Filament/Filament.csproj", "/Project/PropertyGroup/PackageVersion", NUGET_VERSION);
	XmlPoke("./source/Rendering/Rendering.csproj", "/Project/PropertyGroup/PackageVersion", NUGET_VERSION);
	XmlPoke("./source/UX/UX.csproj", "/Project/PropertyGroup/PackageVersion", NUGET_VERSION);
});


Task("libs")
	.IsDependentOn("externals")
	.Does(() =>
{
	MSBuild("./SceneForm.sln", c => {
		c.Configuration = "Release";
		c.Targets.Clear();
		c.Targets.Add("Restore");
		c.Targets.Add("Build");
		c.Properties.Add("DesignTimeBuild", new [] { "false" });
	});
});

Task("nuget")
	.IsDependentOn("libs")
	.Does(() =>
{
	MSBuild ("./SceneForm.sln", c => {
		c.Configuration = "Release";
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
	MSBuild("./samples/HelloSceneform.sln", c => {
		c.Configuration = "Release";
		c.Targets.Clear();
		c.Targets.Add("Restore");
		c.Targets.Add("Build");		
	});
});

Task ("clean")
	.Does (() =>
{
	if (DirectoryExists ("./externals/"))
		DeleteDirectory ("./externals", true);
});

Task ("ci")
	.IsDependentOn("libs")
	.IsDependentOn("nuget")
	.IsDependentOn("samples")
	.Does 
	(
		() =>
		{
		}
	);

RunTarget (TARGET);

#addin nuget:?package=SharpZipLib

var TARGET = Argument ("t", Argument ("target", "Default"));

var SF_VERSION = "1.7.0";

var NUGET_VERSION = SF_VERSION;

var BASE_JAR_URL = $"https://dl.google.com:443/dl/android/maven2/com/google/ar/sceneform/sceneform-base/{SF_VERSION}/sceneform-base-{SF_VERSION}.jar";
var ANIMATION_JAR_URL = $"https://dl.google.com:443/dl/android/maven2/com/google/ar/sceneform/animation/{SF_VERSION}/animation-{SF_VERSION}.jar";
var ASSETS_JAR_URL = $"https://dl.google.com:443/dl/android/maven2/com/google/ar/sceneform/assets/{SF_VERSION}/assets-{SF_VERSION}.jar";
var CORE_JAR_URL = $"https://dl.google.com:443/dl/android/maven2/com/google/ar/sceneform/core/{SF_VERSION}/core-{SF_VERSION}.jar";
var FILAMENT_JAR_URL = $"https://dl.google.com:443/dl/android/maven2/com/google/ar/sceneform/filament-android/{SF_VERSION}/filament-android-{SF_VERSION}.jar";
var PLUGIN_JAR_URL = $"https://dl.google.com:443/dl/android/maven2/com/google/ar/sceneform/plugin/{SF_VERSION}/plugin-{SF_VERSION}.jar";
var RENDERING_JAR_URL = $"https://dl.google.com:443/dl/android/maven2/com/google/ar/sceneform/rendering/{SF_VERSION}/rendering-{SF_VERSION}.jar";

Task ("externals")
	.WithCriteria (!FileExists ("./externals/sceneform-base.jar"))
	.Does (() =>
{
	EnsureDirectoryExists ("./externals/");

	// Download Dependencies
	DownloadFile (BASE_JAR_URL, "./externals/sceneform-base.jar");
	DownloadFile (ANIMATION_JAR_URL, "./externals/animation.jar");
	DownloadFile (ASSETS_JAR_URL, "./externals/assets.jar");
	DownloadFile (CORE_JAR_URL, "./externals/core.jar");
	DownloadFile (FILAMENT_JAR_URL, "./externals/filament-android.jar");
	DownloadFile (PLUGIN_JAR_URL, "./externals/plugin.jar");
	DownloadFile (RENDERING_JAR_URL, "./externals/rendering.jar");

	// Update .csproj nuget versions
	XmlPoke("./source/RxJava/RxJava.csproj", "/Project/PropertyGroup/PackageVersion", RXJAVA_NUGET_VERSION);
	XmlPoke("./source/RxAndroid/RxAndroid.csproj", "/Project/PropertyGroup/PackageVersion", RXANDROID_NUGET_VERSION);
});


Task("libs")
	.IsDependentOn("externals")
	.Does(() =>
{
	MSBuild("./ReactiveX.sln", c => {
		c.Configuration = "Release";
		c.Restore = true;
		c.Properties.Add("DesignTimeBuild", new [] { "false" });
	});
});

Task("nuget")
	.IsDependentOn("libs")
	.Does(() =>
{
	MSBuild ("./ReactiveX.sln", c => {
		c.Configuration = "Release";
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
		DeleteDirectory ("./externals", true);
});

RunTarget (TARGET);

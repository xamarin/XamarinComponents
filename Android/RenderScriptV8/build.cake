var NUGET_VERSION = "28.0.0.1";

var TARGET = Argument ("t", Argument ("target", "Default"));
var BUILD_TOOLS_URL = "https://dl-ssl.google.com/android/repository/build-tools_r28-macosx.zip";
var RENDERSCRIPT_FOLDER = "android-9";

Task ("externals")
	.WithCriteria (!FileExists ("./externals/build-tools/renderscript/lib/renderscript-v8.jar"))
	.Does (() =>
{
	if (!DirectoryExists ("./externals/"))
		CreateDirectory ("./externals");


	// Get Renderscript
	if (!FileExists ("./externals/buildtools.zip"))
		DownloadFile (BUILD_TOOLS_URL, "./externals/buildtools.zip");
	if (!FileExists ("./externals/build-tools/renderscript/lib/renderscript-v8.jar")) {
		Unzip ("./externals/buildtools.zip", "./externals/");
		CopyDirectory ("./externals/" + RENDERSCRIPT_FOLDER, "./externals/build-tools");
		DeleteDirectory ("./externals/" + RENDERSCRIPT_FOLDER, true);
	}

	XmlPoke("./source/RenderScriptV8.csproj", "/Project/PropertyGroup/PackageVersion", NUGET_VERSION);
	XmlPoke("./source/RenderScriptV8.csproj", "/Project/ItemGroup/PackageReference[@Include='Xamarin.Android.Support.Annotations']/@Version", NUGET_VERSION);
});


Task("libs")
	.IsDependentOn("externals")
	.Does(() =>
{
	MSBuild("./RenderScriptV8.sln", c => {
		c.Configuration = "Release";
		c.Restore = true;
		c.Properties.Add("DesignTimeBuild", new [] { "false" });
		c.Properties.Add("AndroidSdkBuildToolsVersion", new [] { "28.0.3" });
	});
});

Task("nuget")
	.IsDependentOn("libs")
	.Does(() =>
{
	MSBuild ("./RenderScriptV8.sln", c => {
		c.Configuration = "Release";
		c.Targets.Clear();
		c.Targets.Add("Pack");
		c.Properties.Add("PackageOutputPath", new [] { MakeAbsolute(new FilePath("./output")).FullPath });
		c.Properties.Add("PackageRequireLicenseAcceptance", new [] { "true" });
		c.Properties.Add("DesignTimeBuild", new [] { "false" });
		c.Properties.Add("AndroidSdkBuildToolsVersion", new [] { "28.0.3" });
	});
});

Task("samples")
	.IsDependentOn("nuget")
	.Does(() =>
{
	MSBuild("./samples/RenderScriptSample.sln", c => {
		c.Configuration = "Release";
		c.Restore = true;
		c.Properties.Add("DesignTimeBuild", new [] { "false" });
		c.Properties.Add("AndroidSdkBuildToolsVersion", new [] { "28.0.3" });
	});
});

Task ("clean")
	.Does (() =>
{
	if (DirectoryExists ("./externals/"))
		DeleteDirectory ("./externals", true);
});

RunTarget (TARGET);

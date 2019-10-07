
var TARGET = Argument("t", Argument("target", "ci"));

var NUGET_VERSION = "2.1.0";

var JAR_VERSION = "2.1";
var JAR_URL = string.Format("http://search.maven.org/remotecontent?filepath=com/daimajia/easing/library/{0}/library-{0}.aar", JAR_VERSION);
var JAR_DEST = "./externals/AndroidEasingFunctions.aar";

Task("externals")
	.Does(() => 
{
	EnsureDirectoryExists("./externals/");

	if (!FileExists(JAR_DEST))
		DownloadFile(JAR_URL, JAR_DEST);

	XmlPoke("./source/AndroidEasingFunctions/AndroidEasingFunctions.csproj", "/Project/PropertyGroup/PackageVersion", NUGET_VERSION);
});

Task("libs")
	.IsDependentOn("externals")
	.Does(() =>
{
	EnsureDirectoryExists("./output/");

	MSBuild("./source/AndroidEasingFunctions.sln", new MSBuildSettings()
		.EnableBinaryLogger("./output/libs.binlog")
		.SetConfiguration("Release")
		.SetMaxCpuCount(0)
		.SetVerbosity(Verbosity.Minimal)
		.WithProperty("DesignTimeBuild", "False")
		.WithRestore());
});

Task("nuget")
	.IsDependentOn("libs")
	.Does(() =>
{
	EnsureDirectoryExists("./output/");

	MSBuild("./source/AndroidEasingFunctions.sln", new MSBuildSettings()
		.EnableBinaryLogger("./output/nuget.binlog")
		.SetConfiguration("Release")
		.SetMaxCpuCount(0)
		.SetVerbosity(Verbosity.Minimal)
		.WithProperty("NoBuild", "True")
		.WithProperty("DesignTimeBuild", "False")
		.WithProperty("PackageOutputPath", MakeAbsolute(new FilePath("./output/")).FullPath)
		.WithTarget("Pack"));
});

Task("samples")
	.IsDependentOn("externals")
	.Does(() =>
{
	EnsureDirectoryExists("./output/");

	MSBuild("./samples/AndroidEasingFunctionsSample.sln", new MSBuildSettings()
		.EnableBinaryLogger("./output/samples.binlog")
		.SetConfiguration("Release")
		.SetMaxCpuCount(0)
		.SetVerbosity(Verbosity.Minimal)
		.WithProperty("DesignTimeBuild", "False")
		.WithRestore());
});

Task ("clean")
	.Does (() =>
{
	if (DirectoryExists("./externals/"))
		DeleteDirectory("./externals/", true);
});

Task("ci")
	.IsDependentOn("externals")
	.IsDependentOn("libs")
	.IsDependentOn("nuget")
	.IsDependentOn("samples");

RunTarget(TARGET);

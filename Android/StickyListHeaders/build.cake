
var TARGET = Argument("t", Argument("target", "ci"));

var NUGET_VERSION = "2.7.1";

var AAR_VERSION = "2.7.0";
var AAR_URL = string.Format ("http://repo1.maven.org/maven2/se/emilsjolander/stickylistheaders/{0}/stickylistheaders-{0}.aar", AAR_VERSION);
var AAR_DEST = "./externals/StickyListHeaders.aar";

Task("externals")
	.Does(() => 
{
	EnsureDirectoryExists("./externals/");

	if (!FileExists(AAR_DEST))
		DownloadFile(AAR_URL, AAR_DEST);

	XmlPoke("./source/StickyListHeaders/StickyListHeaders.csproj", "/Project/PropertyGroup/PackageVersion", NUGET_VERSION);
});

Task("libs")
	.IsDependentOn("externals")
	.Does(() =>
{
	EnsureDirectoryExists("./output/");

	MSBuild("./source/StickyListHeaders.sln", new MSBuildSettings()
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

	MSBuild("./source/StickyListHeaders.sln", new MSBuildSettings()
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

	MSBuild("./samples/StickyListHeadersSample.sln", new MSBuildSettings()
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

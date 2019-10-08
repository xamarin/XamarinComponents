
#load "../../common.cake"

var TARGET = Argument("t", Argument("target", "Default"));

var MONO_TAG = "mono-5.12.0.273";

var ASSEMBLY_VERSION = "5.0.0.0";
var ASSEMBLY_FILE_VERSION = "5.12.0.0";
var ASSEMBLY_INFO_VERSION = "5.12.0.273";
var NUGET_VERSION = "5.12.0.273";

var OUTPUT_PATH = (DirectoryPath)"./output/";

Task("externals")
	.Does(() =>
{
	DownloadMonoSources(MONO_TAG, "./externals/",
		"mcs/class/corlib/Mono/DataConverter.cs");
});

Task("libs")
	.IsDependentOn("externals")
	.Does(() =>
{
	EnsureDirectoryExists(OUTPUT_PATH);

	MSBuild("./source/Mono.DataConverter/Mono.DataConverter.csproj", c => c
		.SetConfiguration("Release")
		.WithRestore()
		.WithTarget("Build")
		.WithTarget("Pack")
		.WithProperty("PackageVersion", NUGET_VERSION)
		.WithProperty("AssemblyVersion", ASSEMBLY_VERSION)
		.WithProperty("FileVersion", ASSEMBLY_FILE_VERSION)
		.WithProperty("InformationalVersion", ASSEMBLY_INFO_VERSION)
		.WithProperty("PackageOutputPath", MakeAbsolute(OUTPUT_PATH).FullPath));
});

Task("nuget").IsDependentOn("libs");

Task("component");

Task("samples")
	.IsDependentOn("libs")
	.Does(() =>
{
	MSBuild("./samples/DataConverterSample.sln", c => c
		.SetConfiguration("Release")
		.WithRestore()
		.WithTarget("Build"));
});

Task("clean")
	.Does(() =>
{
	CleanDirectories("./externals/");
});

RunTarget(TARGET);

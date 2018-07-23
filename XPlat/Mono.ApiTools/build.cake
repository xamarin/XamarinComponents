
#load "../../common.cake"

var TARGET = Argument("t", Argument("target", "Default"));

var MONO_TAG = "ffe4e3f7878fb1980a620f377d64a6129ab6a4ce";

var ASSEMBLY_VERSION = "5.0.0.0";
var ASSEMBLY_FILE_VERSION = "5.14.0.0";
var ASSEMBLY_INFO_VERSION = "5.14.0.0";
var NUGET_VERSION = "5.14.0";

var OUTPUT_PATH = (DirectoryPath)"./output/";

Task("externals")
	.Does(() =>
{
	DownloadMonoSources(MONO_TAG, "./externals/mono-api-info/",
		"mcs/tools/corcompare/mono-api-info.exe.sources");

	DownloadMonoSources(MONO_TAG, "./externals/mono-api-diff/",
		"mcs/tools/mono-api-diff/mono-api-diff.exe.sources");

	DownloadMonoSources(MONO_TAG, "./externals/mono-api-html/",
		"mcs/tools/mono-api-html/mono-api-html.exe.sources");
});

Task("libs")
	.IsDependentOn("externals")
	.Does(() =>
{
	EnsureDirectoryExists(OUTPUT_PATH);
	CleanDirectories(OUTPUT_PATH.FullPath);

	MSBuild("./source/mono-api-tools.sln", c => c
		.SetConfiguration("Release")
		.WithRestore()
		.WithTarget("Build")
		.WithProperty("AssemblyVersion", ASSEMBLY_VERSION)
		.WithProperty("FileVersion", ASSEMBLY_FILE_VERSION)
		.WithProperty("InformationalVersion", ASSEMBLY_INFO_VERSION));

	var outDir = OUTPUT_PATH.Combine("tools");
	EnsureDirectoryExists(outDir);
	CopyFiles("source/*/bin/Release/*/*.dll", outDir);
	CopyFiles("source/*/bin/Release/*/*.exe", outDir);
});

Task("nuget")
	.IsDependentOn("libs")
	.Does(() => 
{
	EnsureDirectoryExists(OUTPUT_PATH);

	// build the "preview" nuget
	NuGetPack("nuget/Mono.ApiTools.nuspec", new NuGetPackSettings {
		Version = NUGET_VERSION + "-preview",
		BasePath = ".",
		OutputDirectory = OUTPUT_PATH,
	});

	// build the "stable" nuget
	NuGetPack("nuget/Mono.ApiTools.nuspec", new NuGetPackSettings {
		Version = NUGET_VERSION,
		BasePath = ".",
		OutputDirectory = OUTPUT_PATH,
	});
});

Task("component");

Task("samples");

Task("clean")
	.Does(() =>
{
	CleanDirectories("./externals/");
	CleanDirectories(OUTPUT_PATH.FullPath);
});

RunTarget(TARGET);

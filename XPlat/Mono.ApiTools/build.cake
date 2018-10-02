
#load "../../common.cake"

var TARGET = Argument("t", Argument("target", "Default"));

var MONO_TAG = "c32af8905b5d672f58acad6fc9e08bf61375b850";

var ASSEMBLY_VERSION = "5.0.0.0";
var ASSEMBLY_FILE_VERSION = "5.14.0.0";
var ASSEMBLY_INFO_VERSION = "5.14.0.0";
var NUGET_VERSION = "5.14.0.2";

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

	var toolsDir = OUTPUT_PATH.Combine("tools");
	EnsureDirectoryExists(toolsDir);
	CopyFiles("source/mono-api-*/bin/Release/*/*.dll", toolsDir);
	CopyFiles("source/mono-api-*/bin/Release/*/*.exe", toolsDir);

	var libDir = OUTPUT_PATH.Combine("lib/netstandard2.0");
	EnsureDirectoryExists(libDir);
	CopyFiles("source/Mono.ApiTools.*/bin/Release/netstandard2.0/Mono.ApiTools.*.dll", libDir);

	libDir = OUTPUT_PATH.Combine("lib/net45");
	EnsureDirectoryExists(libDir);
	CopyFiles("source/Mono.ApiTools.*/bin/Release/net45/Mono.ApiTools.*.dll", libDir);
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

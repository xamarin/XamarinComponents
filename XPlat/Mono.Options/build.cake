#load "../../common.cake"

var TARGET = Argument("t", Argument("target", "ci"));

var MONO_TAG = "mono-6.6.0.162";

var ASSEMBLY_VERSION = "6.0.0.0";
var ASSEMBLY_FILE_VERSION = "6.6.0.0";
var ASSEMBLY_INFO_VERSION = "6.6.0.162";
var NUGET_VERSION = "6.6.0.162";

var OUTPUT_PATH = (DirectoryPath)"./output/";

Task("externals")
	.Does(() =>
{
	DownloadMonoSources(MONO_TAG, "./externals/",
		"mcs/class/Mono.Options/Mono.Options/Options.cs");
});

Task("libs")
	.IsDependentOn("externals")
	.Does(() =>
{
	EnsureDirectoryExists(OUTPUT_PATH);

	MSBuild("./source/Mono.Options/Mono.Options.csproj", c => c
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

Task("nuget")
	.IsDependentOn("libs");

Task("samples")
	.IsDependentOn("libs")
	.Does(() =>
{
	MSBuild("./samples/OptionsSample.sln", c => c
		.SetConfiguration("Release")
		.WithRestore()
		.WithTarget("Build"));
});

Task("clean")
	.Does(() =>
{
	CleanDirectories("./externals/");
});


Task("ci")
	.IsDependentOn("libs")
	.IsDependentOn("nuget")
	.IsDependentOn("samples");

RunTarget(TARGET);

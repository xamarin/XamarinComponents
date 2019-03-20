
#load "../../common.cake"

var TARGET = Argument("t", Argument("target", "Default"));

var OUTPUT_PATH = (DirectoryPath)"./output/";

var ASSEMBLY_VERSION = "1.1.0.0";
var ASSEMBLY_FILE_VERSION = "1.1.1.0";
var ASSEMBLY_INFO_VERSION = "1.1.1.0";
var NUGET_VERSION = "1.1.1.0";

Task("libs")
	.Does(() =>
{
	EnsureDirectoryExists(OUTPUT_PATH);

	MSBuild("./Xamarin.Nuget.Validator/Xamarin.Nuget.Validator.csproj", c => c
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

RunTarget(TARGET);

var TARGET = Argument("t", Argument("target", "ci"));

var SRC_COMMIT = "a23acc77fd31da183e00eea0d636d4ec746d3f7f";
var SRC_URL = $"https://github.com/xamarin/xamarin.exposurenotification/archive/{SRC_COMMIT}.zip";

var OUTPUT_PATH = (DirectoryPath)"./output/";
var NUGET_VERSION = "0.7.0-preview";

Task("externals")
	.Does(() =>
{
	DownloadFile(SRC_URL, "./src.zip");

	Unzip("./src.zip", "./");

	MoveDirectory($"./xamarin.exposurenotification-{SRC_COMMIT}", "./src");
});

Task("libs")
	.IsDependentOn("externals")
	.Does(() =>
{
	EnsureDirectoryExists(OUTPUT_PATH);

	MSBuild($"./src/Xamarin.ExposureNotification/Xamarin.ExposureNotification.csproj", c => c
		.SetConfiguration("Release")
		.WithRestore()
		.WithTarget("Build")
		.WithTarget("Pack")
		.WithProperty("PackageVersion", NUGET_VERSION)
		.WithProperty("PackageOutputPath", MakeAbsolute(OUTPUT_PATH).FullPath));
});

Task("nuget")
	.IsDependentOn("libs");

Task("samples")
	.IsDependentOn("nuget");

Task("clean")
	.Does(() =>
{
	CleanDirectories("./externals/");
});


Task("ci")
	.IsDependentOn("nuget");

RunTarget(TARGET);

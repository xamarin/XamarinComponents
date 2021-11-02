var TARGET = Argument("t", Argument("target", "ci"));

var OUTPUT_PATH = (DirectoryPath)"./output/";

Task("nuget")
	.Does(() =>
{
	MSBuild($"./source/Xamarin.ExposureNotification/Xamarin.ExposureNotification.csproj", c => c
		.SetConfiguration("Release")
		.WithRestore()
		.WithTarget("Build")
		.WithTarget("Pack")
		.WithProperty("PackageOutputPath", MakeAbsolute(OUTPUT_PATH).FullPath));
});

Task("ci")
	.IsDependentOn("nuget");

RunTarget(TARGET);

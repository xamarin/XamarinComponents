var TARGET = Argument("t", Argument("target", "Default"));

var OUTPUT_PATH = (DirectoryPath)"./output/";

Task("libs")
	.Does(() =>
{
	EnsureDirectoryExists(OUTPUT_PATH);

	MSBuild("./Xamarin.AndroidBinderator.sln", c => c
		.SetConfiguration("Release")
		.WithTarget("Restore")
		.WithTarget("Rebuild")
		.WithTarget("Pack")
		.WithProperty("PackageOutputPath", MakeAbsolute(OUTPUT_PATH).FullPath));
});

Task("nuget").IsDependentOn("libs");
Task("samples").IsDependentOn("nuget");
Task("Default").IsDependentOn("nuget");

RunTarget(TARGET);

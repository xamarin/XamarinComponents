
var TARGET = Argument("t", Argument("target", "Default"));

Task("binderate")
	.Does(() =>
{
	var configFile = MakeAbsolute(new FilePath("./config.json")).FullPath;
	var basePath = MakeAbsolute(new DirectoryPath("./")).FullPath;

	var exit = StartProcess("xamarin-android-binderator",
		$"--config=\"{configFile}\" --basepath=\"{basePath}\"");
	if (exit != 0) throw new Exception($"xamarin-android-binderator exited with code {exit}.");
});

Task("native")
	.Does(() =>
{
	var gradlew = MakeAbsolute((FilePath)"./native/KotlinSample/gradlew");
	var exit = StartProcess(gradlew, new ProcessSettings {
		Arguments = "assemble",
		WorkingDirectory = "./native/KotlinSample/"
	});
	if (exit != 0) throw new Exception($"Gradle exited with exit code {exit}.");
});

Task("externals")
	.IsDependentOn("binderate")
	.IsDependentOn("native");

Task("libs")
	.IsDependentOn("externals")
	.Does(() =>
{
	var settings = new MSBuildSettings()
		.SetConfiguration("Release")
		.SetVerbosity(Verbosity.Minimal)
		.WithRestore()
		.WithProperty("DesignTimeBuild", "false")
		.WithProperty("PackageOutputPath", MakeAbsolute((DirectoryPath)"./output/").FullPath)
		.WithTarget("Pack");

	MSBuild("./generated/Xamarin.Kotlin.sln", settings);
});

Task("nuget")
	.IsDependentOn("libs");

Task("samples")
	.IsDependentOn("libs")
	.Does(() =>
{
	var settings = new MSBuildSettings()
		.SetConfiguration("Release")
		.SetVerbosity(Verbosity.Minimal)
		.WithRestore()
		.WithProperty("DesignTimeBuild", "false");

	MSBuild("./samples/KotlinSample.sln", settings);
});

Task("clean")
	.Does(() =>
{
	CleanDirectories("./generated/*/bin");
	CleanDirectories("./generated/*/obj");

	CleanDirectories("./externals/");
	CleanDirectories("./generated/");
	CleanDirectories("./native/.gradle");
	CleanDirectories("./native/**/build");
});

Task("Default")
	.IsDependentOn("externals")
	.IsDependentOn("libs")
	.IsDependentOn("nuget")
	.IsDependentOn("samples");

RunTarget(TARGET);

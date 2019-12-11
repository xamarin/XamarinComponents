
var TARGET = Argument("t", Argument("target", "ci"));

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
	var fn = IsRunningOnWindows() ? "gradlew.bat" : "gradlew";
	var gradlew = MakeAbsolute((FilePath)("./native/KotlinSample/" + fn));
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
		.EnableBinaryLogger("./output/libs.binlog")
		.WithRestore()
		.WithProperty("DesignTimeBuild", "false")
		.WithTarget("Build");

	MSBuild("./generated/Xamarin.Kotlin.sln", settings);
});

Task("nuget")
	.IsDependentOn("libs")
	.Does(() =>
{
	var settings = new MSBuildSettings()
		.SetConfiguration("Release")
		.EnableBinaryLogger("./output/nuget.binlog")
		.WithProperty("NoBuild", "true")
		.WithProperty("PackageOutputPath", MakeAbsolute((DirectoryPath)"./output/").FullPath)
		.WithTarget("Pack");

	MSBuild("./generated/Xamarin.Kotlin.sln", settings);
});

Task("samples")
	.IsDependentOn("libs")
	.Does(() =>
{
	var settings = new MSBuildSettings()
		.SetConfiguration("Release")
		.SetVerbosity(Verbosity.Minimal)
		.EnableBinaryLogger("./output/samples.binlog")
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

Task("ci")
	.IsDependentOn("externals")
	.IsDependentOn("libs")
	.IsDependentOn("nuget")
	.IsDependentOn("samples");

RunTarget(TARGET);

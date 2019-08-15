
var TARGET = Argument("t", Argument("target", "Default"));

Task("externals")
	.Does(() =>
{
	if (!FileExists("./externals/kotlin-stdlib.jar")) {
		EnsureDirectoryExists("./externals/");
		DownloadFile(
			"https://search.maven.org/remotecontent?filepath=org/jetbrains/kotlin/kotlin-stdlib/1.2.71/kotlin-stdlib-1.2.71.jar",
			"./externals/kotlin-stdlib.jar");
	}
});

Task("libs")
	.IsDependentOn("externals")
	.Does(() =>
{
	var gradlew = MakeAbsolute((FilePath)"./native/gradlew");
	var exitCode = StartProcess(gradlew, new ProcessSettings {
		Arguments = "jar",
		WorkingDirectory = "./native/"
	});
	if (exitCode != 0)
		throw new Exception($"Gradle exited with code {exitCode}.");
});

Task("nuget")
	.IsDependentOn("libs")
	.Does(() =>
{
	var settings = new MSBuildSettings()
		.SetConfiguration("Release")
		.SetVerbosity(Verbosity.Minimal)
		.WithRestore()
		.WithProperty("PackageOutputPath", MakeAbsolute((DirectoryPath)"./output/").FullPath)
		.WithTarget("Pack");

	MSBuild("./source/Xamarin.Kotlin.BindingSupport.csproj", settings);
});

Task("samples");

Task("tests")
	.IsDependentOn("nuget")
	.Does(() =>
{
	var settings = new MSBuildSettings()
		.SetConfiguration("Release")
		.SetVerbosity(Verbosity.Minimal)
		.WithRestore();

	MSBuild("./samples/KotlinBindingSupportSample.sln", settings);
});

Task("clean")
	.Does(() =>
{
	CleanDirectories("./source/bin");
	CleanDirectories("./source/obj");
	CleanDirectories("./output/");
	CleanDirectories("./native/.gradle");
	CleanDirectories("./native/**/build");
});

Task("Default")
	.IsDependentOn("externals")
	.IsDependentOn("libs")
	.IsDependentOn("nuget")
	.IsDependentOn("samples")
	.IsDependentOn("tests");

RunTarget(TARGET);

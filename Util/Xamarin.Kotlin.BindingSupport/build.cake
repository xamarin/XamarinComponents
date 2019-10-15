
var TARGET = Argument("t", Argument("target", "Default"));

var PACKAGE_VERSION = "0.5.2-preview";

Task("externals")
	.Does(() =>
{
	EnsureDirectoryExists("./externals/");
	if (!FileExists("./externals/kotlin-stdlib-1.2.jar"))
		DownloadFile(
			"https://search.maven.org/remotecontent?filepath=org/jetbrains/kotlin/kotlin-stdlib/1.2.71/kotlin-stdlib-1.2.71.jar",
			"./externals/kotlin-stdlib-1.2.jar");
	if (!FileExists("./externals/kotlin-stdlib-1.3.jar"))
		DownloadFile(
			"https://search.maven.org/remotecontent?filepath=org/jetbrains/kotlin/kotlin-stdlib/1.3.41/kotlin-stdlib-1.3.41.jar",
			"./externals/kotlin-stdlib-1.3.jar");
	if (!FileExists("./externals/Bubble-Picker.aar"))
		DownloadFile(
			"https://jitpack.io/com/github/igalata/Bubble-Picker/v0.2.5/Bubble-Picker-v0.2.5.aar",
			"./externals/Bubble-Picker.aar");
});

Task("libs")
	.IsDependentOn("externals")
	.Does(() =>
{
	var fn = IsRunningOnWindows() ? "gradlew.bat" : "gradlew";
	var gradlew = MakeAbsolute((FilePath)("./native/" + fn));
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
	NuGetPack("./nuget/Xamarin.Kotlin.BindingSupport.nuspec", new NuGetPackSettings {
		Version = PACKAGE_VERSION,
		OutputDirectory = "./output"
	});
});

Task("samples")
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
	.IsDependentOn("samples");

RunTarget(TARGET);

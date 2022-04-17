var TARGET = Argument ("t", Argument ("target", "ci"));

Task("libs")
	.Does(() =>
{
	var dotNetCoreBuildSettings = new DotNetCoreBuildSettings { 
		Configuration = "Release",
		Verbosity = DotNetCoreVerbosity.Diagnostic,
	};
	DotNetCoreBuild("./source/Xamarin.Build.Download.sln", dotNetCoreBuildSettings);
});

Task("nuget")
	.IsDependentOn("libs")
	.Does(() =>
{
	var dotNetCorePackSettings = new DotNetCorePackSettings {
		Configuration = "Release",
		NoRestore = true,
		NoBuild = true,
		OutputDirectory = "./output/",
		Verbosity = DotNetCoreVerbosity.Diagnostic,
	};
	DotNetCorePack($"./source/Xamarin.Build.Download.sln", dotNetCorePackSettings);
});

Task("tests")
	.IsDependentOn("nuget")
	.Does(() =>
{
	DotNetCoreTest("./source/Xamarin.Build.Download.Tests/", new DotNetCoreTestSettings {
		Configuration = "Release",
		VSTestReportPath = "./output/tests/TestResults.trx"
	});
});

Task ("clean")
	.Does (() =>
{
	CleanDirectories ("./source/**/bin");
	CleanDirectories ("./source/**/obj");
});

Task ("ci")
	.IsDependentOn("tests");

RunTarget (TARGET);

var TARGET = Argument ("t", Argument ("target", "ci"));

var SOURCE_COMMIT = EnvironmentVariable("BUILD_SOURCEVERSION") ?? "";
var SOURCE_BRANCH = EnvironmentVariable("BUIlD_SOURCEBRANCHNAME") ?? "";

Task("libs")
	.Does(() =>
{
	MSBuild("./source/Xamarin.Build.Download.sln", c => {
		c.Configuration = "Release";
		c.Restore = true;
	});
});

Task("nuget")
	.IsDependentOn("libs")
	.Does(() =>
{
	MSBuild ("./source/Xamarin.Build.Download.sln", c => {
		c.Configuration = "Release";
		c.Restore = true;
		c.Targets.Clear();
		c.Targets.Add("Pack");
		c.Properties.Add("PackageOutputPath", new [] { MakeAbsolute(new FilePath("./output")).FullPath });
		if (!string.IsNullOrEmpty(SOURCE_BRANCH))
			c.Properties.Add("RepositoryBranch", new [] { SOURCE_BRANCH });
		if (!string.IsNullOrEmpty(SOURCE_COMMIT))
			c.Properties.Add("RepositoryCommit", new [] { SOURCE_COMMIT });
	});
});

Task("tests")
	.IsDependentOn("nuget")
	.Does(() =>
{
	XUnit2("./source/Xamarin.Build.Download.Tests/**/bin/Release/*.Tests.dll",
		new XUnit2Settings { OutputDirectory = "./output" });
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

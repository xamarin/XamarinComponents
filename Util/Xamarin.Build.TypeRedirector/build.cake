var TARGET = Argument("t", Argument("target", "ci"));

Task("libs")
	.Does(() =>
{
	MSBuild("./source/Xamarin.Build.TypeRedirector.sln", c => {
		c.Configuration = "Release";
		c.Restore = true;
	});
});

Task("nuget")
	.IsDependentOn("libs")
	.Does(() =>
{
	MSBuild("./source/Xamarin.Build.TypeRedirector.sln", c => {
		c.Configuration = "Release";
		c.Restore = true;
		c.Targets.Clear();
		c.Targets.Add("Pack");
		c.Properties.Add("PackageOutputPath", new [] { MakeAbsolute(new FilePath("./output")).FullPath });
	});
});

Task("clean")
	.Does(() =>
{
	CleanDirectories("./source/**/bin");
	CleanDirectories("./source/**/obj");
});

Task("ci")
	.IsDependentOn("nuget");

RunTarget(TARGET);

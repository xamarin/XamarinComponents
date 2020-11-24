
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "ci"));

Task("nuget")
	.Does(() =>
{
	NuGetRestore ("./ExposureNotification.sln");
	MSBuild ("./ExposureNotification.sln", c => {
		c.Configuration = "Release";
		c.MaxCpuCount = 0;
		c.Targets.Clear();
		c.Targets.Add("Pack");
		c.Properties.Add("PackageOutputPath", new [] { MakeAbsolute(new FilePath("./output")).FullPath });
		c.Properties.Add("PackageRequireLicenseAcceptance", new [] { "true" });
	});
});

Task("ci")
	.IsDependentOn("nuget");

RunTarget (TARGET);

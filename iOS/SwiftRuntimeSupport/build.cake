
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "ci"));

var SWIFT_RUNTIME_SUPPORT_VERSION = "0.2.2";
var SWIFT_RUNTIME_SUPPORT_NUGET_VERSION = SWIFT_RUNTIME_SUPPORT_VERSION;

Task ("externals")
	.Does (() =>
{
	// Update .csproj nuget versions
	XmlPoke("./source/SwiftRuntimeSupport.csproj", "/Project/PropertyGroup/PackageVersion", SWIFT_RUNTIME_SUPPORT_NUGET_VERSION);
});

Task("libs")
	.IsDependentOn("externals")
	.Does(() =>
{
	MSBuild("./SwiftRuntimeSupport.sln", c => {
		c.Configuration = "Release";
		c.Restore = true;
		c.MaxCpuCount = 0;
	});
});

Task("nuget")
	.IsDependentOn("libs")
	.Does(() =>
{
	MSBuild ("./SwiftRuntimeSupport.sln", c => {
		c.Configuration = "Release";
		c.MaxCpuCount = 0;
		c.Targets.Clear();
		c.Targets.Add("Pack");
		c.Properties.Add("PackageOutputPath", new [] { MakeAbsolute(new FilePath("./output")).FullPath });
		c.Properties.Add("PackageRequireLicenseAcceptance", new [] { "true" });
		c.Properties.Add("IncludeBuildOutput", new [] { "false" });
	});
});

Task("samples")
	.IsDependentOn("nuget");

Task("ci")
	.IsDependentOn("externals")
	.IsDependentOn("libs")
	.IsDependentOn("nuget")
	.IsDependentOn("samples");

RunTarget (TARGET);

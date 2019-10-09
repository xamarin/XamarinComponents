
var TARGET = Argument("t", Argument("target", "ci"));

var NUGET_VERSION = "1.1.0";

Task("libs")
	.Does(() =>
{
	XmlPoke("./source/Explosions/Explosions.csproj", "/Project/PropertyGroup/PackageVersion", NUGET_VERSION);
	MSBuild("./source/Explosions.sln", new MSBuildSettings()
		.EnableBinaryLogger("./output/libs.binlog")
		.SetConfiguration("Release")
		.WithProperty("DesignTimeBuild", "False")
		.WithProperty("PackageOutputPath", MakeAbsolute(new FilePath("./output/")).FullPath)
		.WithRestore()
		.WithTarget("Pack"));
});

Task("nuget")
	.IsDependentOn("libs");

Task("samples")
	.IsDependentOn("nuget")
	.Does(() =>
{
	MSBuild("./samples/ExplosionsSample.sln", new MSBuildSettings()
		.EnableBinaryLogger("./output/samples.binlog")
		.SetConfiguration("Release")
		.SetMaxCpuCount(0)
		.SetVerbosity(Verbosity.Minimal)
		.WithProperty("DesignTimeBuild", "False")
		.WithRestore());
});

Task("ci")
	.IsDependentOn("libs")
	.IsDependentOn("nuget")
	.IsDependentOn("samples");

RunTarget(TARGET);

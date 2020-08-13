
var TARGET = Argument("t", Argument("target", "ci"));

var NUGET_VERSION = "1.1.0";

Task("libs")
	.Does(() =>
{
	XmlPoke("./source/ITSwitch/ITSwitch.csproj", "/Project/PropertyGroup/PackageVersion", NUGET_VERSION);
	MSBuild("./source/ITSwitch.sln", new MSBuildSettings()
		.EnableBinaryLogger("./output/libs.binlog")
		.SetConfiguration("Release")
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
	MSBuild("./samples/ITSwitchSample/ITSwitchSample.sln", new MSBuildSettings()
		.EnableBinaryLogger("./output/samples.binlog")
		.SetConfiguration("Release")
		.WithRestore());
});

Task("ci")
	.IsDependentOn("libs")
	.IsDependentOn("nuget")
	.IsDependentOn("samples");

RunTarget(TARGET);

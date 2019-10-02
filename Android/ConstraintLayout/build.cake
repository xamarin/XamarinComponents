var TARGET = Argument ("target", Argument ("t", "ci"));

var NUGET_VERSION = "2.0.0-beta2";
var AAR_VERSION = "2.0.0-beta2";

var CONSTRAINT_LAYOUT_URL = string.Format ("https://dl.google.com/dl/android/maven2/com/android/support/constraint/constraint-layout/{0}/constraint-layout-{0}.aar", AAR_VERSION);
var CONSTRAINT_LAYOUT_SOLVER_URL = string.Format ("https://dl.google.com/dl/android/maven2/com/android/support/constraint/constraint-layout-solver/{0}/constraint-layout-solver-{0}.jar", AAR_VERSION);

Task("libs")
	.IsDependentOn("externals")
	.Does (() =>
{
	MSBuild("./source/ConstraintLayout.sln", c => {
		c.Configuration = "Release";
		c.Targets.Clear();
		c.Targets.Add("Restore");
		c.Targets.Add("Rebuild");
		c.Properties.Add("DesignTimeBuild", new [] { "false" });
		c.BinaryLogger = new MSBuildBinaryLogSettings {
			Enabled = true,
			FileName = "./output/libs.binlog"
		};
	});
});

Task("samples")
	.IsDependentOn("nuget")
	.Does (() =>
{
	MSBuild("./samples/ConstraintLayoutSample.sln", c => {
		c.Configuration = "Release";
		c.Targets.Clear();
		c.Targets.Add("Restore");
		c.Targets.Add("Build");
		c.BinaryLogger = new MSBuildBinaryLogSettings {
			Enabled = true,
			FileName = "./output/samples.binlog"
		};
	});
});


Task("nuget")
	.IsDependentOn("libs")
	.Does (() =>
{
	MSBuild ("./source/ConstraintLayout.sln", c => {
		c.Configuration = "Release";
		c.Targets.Clear();
		c.Targets.Add("Pack");
		c.Properties.Add("PackageOutputPath", new [] { MakeAbsolute(new FilePath("./output")).FullPath });
		c.Properties.Add("PackageRequireLicenseAcceptance", new [] { "true" });
		c.Properties.Add("DesignTimeBuild", new [] { "false" });
		c.BinaryLogger = new MSBuildBinaryLogSettings {
			Enabled = true,
			FileName = "./output/nuget.binlog"
		};
	});
});

Task("ci")
	.IsDependentOn("samples");


Task ("externals")
	.WithCriteria (() => !FileExists ("./externals/constraint-layout.aar"))
	.Does (() =>
{
	var path = "./externals/";

	EnsureDirectoryExists (path);

	if (!FileExists (path + "constraint-layout.aar")) {
		DownloadFile (CONSTRAINT_LAYOUT_URL, path + "constraint-layout.aar");
		Unzip(path + "constraint-layout.aar", path + "constraint-layout/");
	}
	if (!FileExists (path + "constraint-layout-solver.jar"))
		DownloadFile (CONSTRAINT_LAYOUT_SOLVER_URL, path + "constraint-layout-solver.jar");

	XmlPoke("./source/ConstraintLayout/ConstraintLayout.csproj", "/Project/PropertyGroup/PackageVersion", NUGET_VERSION);
	XmlPoke("./source/ConstraintLayoutSolver/ConstraintLayoutSolver.csproj", "/Project/PropertyGroup/PackageVersion", NUGET_VERSION);
	
});

Task ("clean")
	.Does (() => 
{
	if (DirectoryExists ("./externals/"))
		DeleteDirectory("./externals/", true);

	if (DirectoryExists ("./output"))
		DeleteDirectory ("./output", true);
});

RunTarget (TARGET);

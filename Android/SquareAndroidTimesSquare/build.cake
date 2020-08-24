var TARGET = Argument ("t", Argument ("target", "ci"));

var NUGET_VERSION = "1.7.3.1";

var JAR_VERSION = "1.7.3";
var JAR_URL = $"https://repo1.maven.org/maven2/com/squareup/android-times-square/{JAR_VERSION}/android-times-square-{JAR_VERSION}.aar";

Task ("externals")
	.Does (() =>
{
	EnsureDirectoryExists ("./externals");
	
	DownloadFile(JAR_URL, "./externals/android-times-square.aar");

	// Update .csproj nuget versions
	XmlPoke("./source/Square.AndroidTimesSquare/Square.AndroidTimesSquare.csproj", "/Project/PropertyGroup/PackageVersion", NUGET_VERSION);
});

Task("nuget")
	.IsDependentOn("externals")
	.Does(() =>
{
	MSBuild ("./source/Square.AndroidTimesSquare.sln", c => {
		c.Configuration = "Release";
		c.Restore = true;
		c.MaxCpuCount = 0;
		c.Targets.Clear();
		c.Targets.Add("Pack");
		c.Properties.Add("PackageOutputPath", new [] { MakeAbsolute(new FilePath("./output")).FullPath });
		c.Properties.Add("PackageRequireLicenseAcceptance", new [] { "true" });
		c.Properties.Add("DesignTimeBuild", new [] { "false" });
	});
});

Task("samples")
	.IsDependentOn("nuget")
	.Does(() =>
{
	MSBuild ("./samples/AndroidTimesSquareSample.sln", c => {
		c.Configuration = "Release";
		c.Restore = true;
		c.MaxCpuCount = 0;
		c.Properties.Add("DesignTimeBuild", new [] { "false" });
	});
});

Task("ci")
	.IsDependentOn("externals")
	.IsDependentOn("nuget")
	.IsDependentOn("samples");

Task ("clean")
	.Does (() =>
{
	if (DirectoryExists ("./externals/"))
		DeleteDirectory ("./externals", new DeleteDirectorySettings {
			Recursive = true,
			Force = true
		});
});

RunTarget (TARGET);

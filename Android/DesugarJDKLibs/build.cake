var TARGET = Argument ("t", Argument ("target", "ci"));

var NUGET_VERSION = "1.1.5";

var JAR_VERSION = "1.1.5";
var JAR_URL = $"https://dl.google.com/android/maven2/com/android/tools/desugar_jdk_libs/{JAR_VERSION}/desugar_jdk_libs-{JAR_VERSION}.jar";

Task ("externals")
	.Does (() =>
{
	EnsureDirectoryExists ("./externals");
	
	DownloadFile(JAR_URL, $"./externals/desugar_jdk_libs-{JAR_VERSION}.jar");

	// Update .csproj nuget versions
	XmlPoke("./source/Xamarin.Android.Tools.DesugarJdkLibs/Xamarin.Android.Tools.DesugarJdkLibs.csproj", "/Project/PropertyGroup/PackageVersion", NUGET_VERSION);
});

Task("nuget")
	.IsDependentOn("externals")
	.Does(() =>
{
	MSBuild ("./source/DesugarJDKLibs.sln", c => {
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

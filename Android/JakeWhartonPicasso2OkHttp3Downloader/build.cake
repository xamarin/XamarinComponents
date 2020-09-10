var TARGET = Argument ("t", Argument ("target", "ci"));

var NUGET_VERSION = "1.1.0.1";

var JAR_VERSION = "1.1.0";
var JAR_URL = $"https://repo1.maven.org/maven2/com/jakewharton/picasso/picasso2-okhttp3-downloader/{JAR_VERSION}/picasso2-okhttp3-downloader-{JAR_VERSION}.jar";

Task ("externals")
	.Does (() =>
{
	EnsureDirectoryExists ("./externals");
	
	DownloadFile(JAR_URL, "./externals/picasso2-okhttp3-downloader.jar");

	// Update .csproj nuget versions
	XmlPoke("./source/JakeWharton.Picasso2OkHttp3Downloader/JakeWharton.Picasso2OkHttp3Downloader.csproj", "/Project/PropertyGroup/PackageVersion", NUGET_VERSION);
});

Task("nuget")
	.IsDependentOn("externals")
	.Does(() =>
{
	MSBuild ("./source/JakeWharton.Picasso2OkHttp3Downloader.sln", c => {
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


Task("ci")
	.IsDependentOn("externals")
	.IsDependentOn("nuget");

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

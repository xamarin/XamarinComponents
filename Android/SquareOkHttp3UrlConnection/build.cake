var TARGET = Argument ("t", Argument ("target", "ci"));

var NUGET_VERSION = "4.4.1";

var JAR_VERSION = "4.4.1";
var JAR_URL = $"https://repo1.maven.org/maven2/com/squareup/okhttp3/okhttp-urlconnection/{JAR_VERSION}/okhttp-urlconnection-{JAR_VERSION}.jar";

Task ("externals")
	.Does (() =>
{
	EnsureDirectoryExists ("./externals");
	
	DownloadFile(JAR_URL, "./externals/okhttp3-urlconnection.jar");

	// Update .csproj nuget versions
	XmlPoke("./source/Square.OkHttp3.UrlConnection/Square.OkHttp3.UrlConnection.csproj", "/Project/PropertyGroup/PackageVersion", NUGET_VERSION);
});

Task("nuget")
	.IsDependentOn("externals")
	.Does(() =>
{
	MSBuild ("./source/Square.OkHttp3.UrlConnection.sln", c => {
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

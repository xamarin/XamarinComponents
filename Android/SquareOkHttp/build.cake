var TARGET = Argument ("t", Argument ("target", "ci"));

var NUGET_VERSION = "2.7.5.2";

var JAR_VERSION = "2.7.5";
var JAR_OKHTTP_URL = $"https://repo1.maven.org/maven2/com/squareup/okhttp/okhttp/{JAR_VERSION}/okhttp-{JAR_VERSION}.jar";
var JAR_OKHTTP_URLCONNECTION_URL = $"https://repo1.maven.org/maven2/com/squareup/okhttp/okhttp-urlconnection/{JAR_VERSION}/okhttp-urlconnection-{JAR_VERSION}.jar";
var JAR_OKHTTP_WS_URL = $"https://repo1.maven.org/maven2/com/squareup/okhttp/okhttp-ws/{JAR_VERSION}/okhttp-ws-{JAR_VERSION}.jar";

Task ("externals")
	.Does (() =>
{
	EnsureDirectoryExists ("./externals");
	
	DownloadFile(JAR_OKHTTP_URL, "./externals/okhttp.jar");
	DownloadFile(JAR_OKHTTP_URLCONNECTION_URL, "./externals/okhttp-urlconnection.jar");
	DownloadFile(JAR_OKHTTP_WS_URL, "./externals/okhttp-ws.jar");

	// Update .csproj nuget versions
	XmlPoke("./source/Square.OkHttp/Square.OkHttp.csproj", "/Project/PropertyGroup/PackageVersion", NUGET_VERSION);
	XmlPoke("./source/Square.OkHttp.UrlConnection/Square.OkHttp.UrlConnection.csproj", "/Project/PropertyGroup/PackageVersion", NUGET_VERSION);
	XmlPoke("./source/Square.OkHttp.WS/Square.OkHttp.WS.csproj", "/Project/PropertyGroup/PackageVersion", NUGET_VERSION);
});

Task("nuget")
	.IsDependentOn("externals")
	.Does(() =>
{
	MSBuild ("./source/Square.OkHttp.sln", c => {
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
	MSBuild ("./samples/OkHttpSample.sln", c => {
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

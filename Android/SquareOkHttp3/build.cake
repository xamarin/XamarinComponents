var TARGET = Argument ("t", Argument ("target", "ci"));

var NUGET_VERSION = "4.9.1.1";

var JAR_VERSION = "4.9.1";
var JAR_OKHTTP_URL = $"https://repo1.maven.org/maven2/com/squareup/okhttp3/okhttp/{JAR_VERSION}/okhttp-{JAR_VERSION}.jar";
var JAR_OKHTTP_URLCONNECTION_URL = $"https://repo1.maven.org/maven2/com/squareup/okhttp3/okhttp-urlconnection/{JAR_VERSION}/okhttp-urlconnection-{JAR_VERSION}.jar";
var JAR_OKHTTP_LOGGING_URL = $"https://repo1.maven.org/maven2/com/squareup/okhttp3/logging-interceptor/{JAR_VERSION}/logging-interceptor-{JAR_VERSION}.jar";

Task ("externals")
	.Does (() =>
{
	EnsureDirectoryExists ("./externals");
	
	DownloadFile(JAR_OKHTTP_URL, "./externals/okhttp3.jar");
	DownloadFile(JAR_OKHTTP_URLCONNECTION_URL, "./externals/okhttp3-urlconnection.jar");
	DownloadFile(JAR_OKHTTP_LOGGING_URL, "./externals/okhttp3-logging-interceptor.jar");

	// Update .csproj nuget versions
	XmlPoke("./source/Square.OkHttp3/Square.OkHttp3.csproj", "/Project/PropertyGroup/PackageVersion", NUGET_VERSION);
	XmlPoke("./source/Square.OkHttp3.UrlConnection/Square.OkHttp3.UrlConnection.csproj", "/Project/PropertyGroup/PackageVersion", NUGET_VERSION);
	XmlPoke("./source/Square.OkHttp3.LoggingInterceptor/Square.OkHttp3.LoggingInterceptor.csproj", "/Project/PropertyGroup/PackageVersion", NUGET_VERSION);
});

Task("nuget")
	.IsDependentOn("externals")
	.Does(() =>
{
	MSBuild ("./source/Square.OkHttp3.sln", c => {
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
	MSBuild ("./samples/OkHttp3Sample.sln", c => {
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

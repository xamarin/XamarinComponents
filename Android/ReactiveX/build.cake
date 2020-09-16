#addin nuget:?package=SharpZipLib&version=1.2.0

var TARGET = Argument ("t", Argument ("target", "ci"));

var RXANDROID_VERSION = "2.1.1";
var RXJAVA_VERSION = "2.2.12";
var RXKOTLIN_VERSION = "2.4.0";

var RXANDROID_NUGET_VERSION = "2.1.1.1";
var RXJAVA_NUGET_VERSION = "2.2.12";
var RXKOTLIN_NUGET_VERSION = "2.4.0";

var RXANDROID_AAR_URL = $"https://search.maven.org/remotecontent?filepath=io/reactivex/rxjava2/rxandroid/{RXANDROID_VERSION}/rxandroid-{RXANDROID_VERSION}.aar";
var RXANDROID_DOCS_URL = $"https://search.maven.org/remotecontent?filepath=io/reactivex/rxjava2/rxandroid/{RXANDROID_VERSION}/rxandroid-{RXANDROID_VERSION}-javadoc.jar";

var RXJAVA_JAR_URL = $"https://search.maven.org/remotecontent?filepath=io/reactivex/rxjava2/rxjava/{RXJAVA_VERSION}/rxjava-{RXJAVA_VERSION}.jar";
var RXJAVA_DOCS_URL = $"https://search.maven.org/remotecontent?filepath=io/reactivex/rxjava2/rxjava/{RXJAVA_VERSION}/rxjava-{RXJAVA_VERSION}-javadoc.jar";

var RXKOTLIN_JAR_URL = $"https://search.maven.org/remotecontent?filepath=io/reactivex/rxjava2/rxkotlin/{RXKOTLIN_VERSION}/rxkotlin-{RXKOTLIN_VERSION}.jar";
var RXKOTLIN_DOCS_URL = $"https://search.maven.org/remotecontent?filepath=io/reactivex/rxjava2/rxkotlin/{RXKOTLIN_VERSION}/rxkotlin-{RXKOTLIN_VERSION}-javadoc.jar";

Task ("externals")
	.WithCriteria (!FileExists ("./externals/rxjava.jar"))
	.Does (() =>
{
	EnsureDirectoryExists ("./externals/");

	// Download Dependencies
	DownloadFile (RXJAVA_JAR_URL, "./externals/rxjava.jar");
	DownloadFile (RXJAVA_DOCS_URL, "./externals/rxjava-javadocs.jar");

	DownloadFile (RXANDROID_AAR_URL, "./externals/rxandroid.aar");
	DownloadFile (RXANDROID_DOCS_URL, "./externals/rxandroid-javadocs.jar");

	DownloadFile (RXKOTLIN_JAR_URL, "./externals/rxkotlin.jar");
	DownloadFile (RXKOTLIN_DOCS_URL, "./externals/rxkotlin-javadocs.jar");

	Unzip ("./externals/rxjava-javadocs.jar", "./externals/rxjava-javadocs/");
	Unzip ("./externals/rxandroid-javadocs.jar", "./externals/rxandroid-javadocs/");
	Unzip ("./externals/rxkotlin-javadocs.jar", "./externals/rxkotlin-javadocs/");
	Unzip ("./externals/rxandroid.aar", "./externals/rxandroid/");

	// Update .csproj nuget versions
	XmlPoke("./source/RxJava/RxJava.csproj", "/Project/PropertyGroup/PackageVersion", RXJAVA_NUGET_VERSION);
	XmlPoke("./source/RxAndroid/RxAndroid.csproj", "/Project/PropertyGroup/PackageVersion", RXANDROID_NUGET_VERSION);
	XmlPoke("./source/RxKotlin/RxKotlin.csproj", "/Project/PropertyGroup/PackageVersion", RXKOTLIN_NUGET_VERSION);
});


Task("libs")
	.IsDependentOn("externals")
	.Does(() =>
{
	MSBuild("./ReactiveX.sln", c => {
		c.Configuration = "Release";
		c.Restore = true;
		c.Properties.Add("DesignTimeBuild", new [] { "false" });
	});
});

Task("nuget")
	.IsDependentOn("libs")
	.Does(() =>
{
	MSBuild ("./ReactiveX.sln", c => {
		c.Configuration = "Release";
		c.Targets.Clear();
		c.Targets.Add("Pack");
		c.Properties.Add("PackageOutputPath", new [] { MakeAbsolute(new FilePath("./output")).FullPath });
		c.Properties.Add("PackageRequireLicenseAcceptance", new [] { "true" });
		c.Properties.Add("DesignTimeBuild", new [] { "false" });
	});
});

Task("samples")
	.IsDependentOn("nuget");

Task("ci")
	.IsDependentOn("externals")
	.IsDependentOn("nuget")
	.IsDependentOn("samples");

Task ("clean")
	.Does (() =>
{
	if (DirectoryExists ("./externals/"))
		DeleteDirectory ("./externals", true);
});

RunTarget (TARGET);

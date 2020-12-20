#addin nuget:?package=SharpZipLib&version=1.3.1

var TARGET = Argument ("t", Argument ("target", "ci"));

var KOTLINX_VERSION = "1.4.1";
var KOTLINX_NUGET_VERSION = "1.4.1";

var KOTLINX_CORE_JAR_URL = $"https://repo1.maven.org/maven2/org/jetbrains/kotlinx/kotlinx-coroutines-core/{KOTLINX_VERSION}/kotlinx-coroutines-core-{KOTLINX_VERSION}.jar";
var KOTLINX_CORE_DOCS_URL = $"https://repo1.maven.org/maven2/org/jetbrains/kotlinx/kotlinx-coroutines-core/{KOTLINX_VERSION}/kotlinx-coroutines-core-{KOTLINX_VERSION}-javadoc.jar";

var KOTLINX_ANDROID_JAR_URL = $"https://repo1.maven.org/maven2/org/jetbrains/kotlinx/kotlinx-coroutines-android/{KOTLINX_VERSION}/kotlinx-coroutines-android-{KOTLINX_VERSION}.jar";
var KOTLINX_ANDROID_DOCS_URL = $"https://repo1.maven.org/maven2/org/jetbrains/kotlinx/kotlinx-coroutines-android/{KOTLINX_VERSION}/kotlinx-coroutines-android-{KOTLINX_VERSION}-javadoc.jar";

var KOTLINX_JDK8_JAR_URL = $"https://repo1.maven.org/maven2/org/jetbrains/kotlinx/kotlinx-coroutines-jdk8/{KOTLINX_VERSION}/kotlinx-coroutines-jdk8-{KOTLINX_VERSION}.jar";
var KOTLINX_JDK8_DOCS_URL = $"https://repo1.maven.org/maven2/org/jetbrains/kotlinx/kotlinx-coroutines-jdk8/{KOTLINX_VERSION}/kotlinx-coroutines-jdk8-{KOTLINX_VERSION}-javadoc.jar";

var KOTLINX_JVM_JAR_URL = $"https://repo1.maven.org/maven2/org/jetbrains/kotlinx/kotlinx-coroutines-core-jvm/{KOTLINX_VERSION}/kotlinx-coroutines-core-jvm-{KOTLINX_VERSION}.jar";
var KOTLINX_JVM_DOCS_URL = $"https://repo1.maven.org/maven2/org/jetbrains/kotlinx/kotlinx-coroutines-core-jvm/{KOTLINX_VERSION}/kotlinx-coroutines-core-jvm-{KOTLINX_VERSION}-javadoc.jar";

var KOTLINX_REACTIVE_JAR_URL = $"https://repo1.maven.org/maven2/org/jetbrains/kotlinx/kotlinx-coroutines-reactive/{KOTLINX_VERSION}/kotlinx-coroutines-reactive-{KOTLINX_VERSION}.jar";
var KOTLINX_REACTIVE_DOCS_URL = $"https://repo1.maven.org/maven2/org/jetbrains/kotlinx/kotlinx-coroutines-reactive/{KOTLINX_VERSION}/kotlinx-coroutines-reactive-{KOTLINX_VERSION}-javadoc.jar";

var KOTLINX_RX2_JAR_URL = $"https://repo1.maven.org/maven2/org/jetbrains/kotlinx/kotlinx-coroutines-rx2/{KOTLINX_VERSION}/kotlinx-coroutines-rx2-{KOTLINX_VERSION}.jar";
var KOTLINX_RX2_DOCS_URL = $"https://repo1.maven.org/maven2/org/jetbrains/kotlinx/kotlinx-coroutines-rx2/{KOTLINX_VERSION}/kotlinx-coroutines-rx2-{KOTLINX_VERSION}-javadoc.jar";


Task ("externals")
	.WithCriteria (!FileExists ("./externals/kotlinx-coroutines-core.jar"))
	.Does (() =>
{
	EnsureDirectoryExists ("./externals/");

	// Download Dependencies
	DownloadFile (KOTLINX_CORE_JAR_URL, "./externals/kotlinx-coroutines-core.jar");
	DownloadFile (KOTLINX_CORE_DOCS_URL, "./externals/kotlinx-coroutines-core-javadocs.jar");

	DownloadFile (KOTLINX_ANDROID_JAR_URL, "./externals/kotlinx-coroutines-android.jar");
	DownloadFile (KOTLINX_ANDROID_DOCS_URL, "./externals/kotlinx-coroutines-android-javadocs.jar");

	DownloadFile (KOTLINX_JDK8_JAR_URL, "./externals/kotlinx-coroutines-jdk8.jar");
	DownloadFile (KOTLINX_JDK8_DOCS_URL, "./externals/kotlinx-coroutines-jdk8-javadocs.jar");

	DownloadFile (KOTLINX_JVM_JAR_URL, "./externals/kotlinx-coroutines-core-jvm.jar");
	DownloadFile (KOTLINX_JVM_DOCS_URL, "./externals/kotlinx-coroutines-core-jvm-javadocs.jar");
	
	DownloadFile (KOTLINX_REACTIVE_JAR_URL, "./externals/kotlinx-coroutines-reactive.jar");
	DownloadFile (KOTLINX_REACTIVE_DOCS_URL, "./externals/kotlinx-coroutines-reactive-javadocs.jar");

	DownloadFile (KOTLINX_RX2_JAR_URL, "./externals/kotlinx-coroutines-rx2.jar");
	DownloadFile (KOTLINX_RX2_DOCS_URL, "./externals/kotlinx-coroutines-rx2-javadocs.jar");

	Unzip ("./externals/kotlinx-coroutines-core-javadocs.jar", "./externals/kotlinx-coroutines-core-javadocs/");
	Unzip ("./externals/kotlinx-coroutines-android-javadocs.jar", "./externals/kotlinx-coroutines-android-javadocs/");
	Unzip ("./externals/kotlinx-coroutines-jdk8-javadocs.jar", "./externals/kotlinx-coroutines-jdk8-javadocs/");
	Unzip ("./externals/kotlinx-coroutines-core-jvm-javadocs.jar", "./externals/kotlinx-coroutines-core-jvm-javadocs/");
	Unzip ("./externals/kotlinx-coroutines-reactive-javadocs.jar", "./externals/kotlinx-coroutines-reactive-javadocs/");
	Unzip ("./externals/kotlinx-coroutines-rx2-javadocs.jar", "./externals/kotlinx-coroutines-rx2-javadocs/");


	// Update .csproj nuget versions
	XmlPoke("./source/Kotlinx.Coroutines.Core/Kotlinx.Coroutines.Core.csproj", "/Project/PropertyGroup/PackageVersion", KOTLINX_NUGET_VERSION);
	XmlPoke("./source/Kotlinx.Coroutines.Android/Kotlinx.Coroutines.Android.csproj", "/Project/PropertyGroup/PackageVersion", KOTLINX_NUGET_VERSION);
	XmlPoke("./source/Kotlinx.Coroutines.Jdk8/Kotlinx.Coroutines.Jdk8.csproj", "/Project/PropertyGroup/PackageVersion", KOTLINX_NUGET_VERSION);
	XmlPoke("./source/Kotlinx.Coroutines.Core.Jvm/Kotlinx.Coroutines.Core.Jvm.csproj", "/Project/PropertyGroup/PackageVersion", KOTLINX_NUGET_VERSION);
	XmlPoke("./source/Kotlinx.Coroutines.Reactive/Kotlinx.Coroutines.Reactive.csproj", "/Project/PropertyGroup/PackageVersion", KOTLINX_NUGET_VERSION);
	XmlPoke("./source/Kotlinx.Coroutines.Rx2/Kotlinx.Coroutines.Rx2.csproj", "/Project/PropertyGroup/PackageVersion", KOTLINX_NUGET_VERSION);
});

Task("native")
	.Does(() =>
{
	var fn = IsRunningOnWindows() ? "gradlew.bat" : "gradlew";
	var gradlew = MakeAbsolute((FilePath)("./native/KotlinxCoroutinesSample/" + fn));
	var exit = StartProcess(gradlew, new ProcessSettings {
		Arguments = "assemble",
		WorkingDirectory = "./native/KotlinxCoroutinesSample/"
	});
	if (exit != 0) throw new Exception($"Gradle exited with exit code {exit}.");
});

Task("libs")
	.IsDependentOn("externals")
	.Does(() =>
{
	MSBuild("./KotlinxCoroutines.sln", c => {
		c.Configuration = "Release";
		c.Restore = true;
		c.Properties.Add("DesignTimeBuild", new [] { "false" });
	});
});

Task("nuget")
	.IsDependentOn("libs")
	.Does(() =>
{
	MSBuild ("./KotlinxCoroutines.sln", c => {
		c.Configuration = "Release";
		c.Targets.Clear();
		c.Targets.Add("Pack");
		c.Properties.Add("PackageOutputPath", new [] { MakeAbsolute(new FilePath("./output")).FullPath });
		c.Properties.Add("PackageRequireLicenseAcceptance", new [] { "true" });
		c.Properties.Add("DesignTimeBuild", new [] { "false" });
	});
});

Task("samples")
	.IsDependentOn("native")
	.IsDependentOn("nuget")
	.Does(() =>
{
	var settings = new MSBuildSettings()
		.SetConfiguration("Release")
		.SetVerbosity(Verbosity.Minimal)
		.EnableBinaryLogger("./output/samples.binlog")
		.WithRestore()
		.WithProperty("DesignTimeBuild", "false");

	MSBuild("./samples/KotlinxCoroutinesSample.sln", settings);
});

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

#addin nuget:?package=SharpZipLib&version=1.3.1

var TARGET = Argument ("t", Argument ("target", "ci"));

var KOTLINX_VERSION = "1.5.1";
var KOTLINX_NUGET_VERSION = "1.5.1";

/*
	since version 1.4.3 there are no javadocs, but javasources
*/
var KOTLINX_CORE_JAR_URL = $"https://repo1.maven.org/maven2/org/jetbrains/kotlinx/kotlinx-coroutines-core/{KOTLINX_VERSION}/kotlinx-coroutines-core-{KOTLINX_VERSION}.jar";
var KOTLINX_CORE_SOURCES_URL = $"https://repo1.maven.org/maven2/org/jetbrains/kotlinx/kotlinx-coroutines-core/{KOTLINX_VERSION}/kotlinx-coroutines-core-{KOTLINX_VERSION}-sources.jar";

var KOTLINX_ANDROID_JAR_URL = $"https://repo1.maven.org/maven2/org/jetbrains/kotlinx/kotlinx-coroutines-android/{KOTLINX_VERSION}/kotlinx-coroutines-android-{KOTLINX_VERSION}.jar";
var KOTLINX_ANDROID_SOURCES_URL = $"https://repo1.maven.org/maven2/org/jetbrains/kotlinx/kotlinx-coroutines-android/{KOTLINX_VERSION}/kotlinx-coroutines-android-{KOTLINX_VERSION}-sources.jar";

var KOTLINX_JDK8_JAR_URL = $"https://repo1.maven.org/maven2/org/jetbrains/kotlinx/kotlinx-coroutines-jdk8/{KOTLINX_VERSION}/kotlinx-coroutines-jdk8-{KOTLINX_VERSION}.jar";
var KOTLINX_JDK8_SOURCES_URL = $"https://repo1.maven.org/maven2/org/jetbrains/kotlinx/kotlinx-coroutines-jdk8/{KOTLINX_VERSION}/kotlinx-coroutines-jdk8-{KOTLINX_VERSION}-sources.jar";

var KOTLINX_JVM_JAR_URL = $"https://repo1.maven.org/maven2/org/jetbrains/kotlinx/kotlinx-coroutines-core-jvm/{KOTLINX_VERSION}/kotlinx-coroutines-core-jvm-{KOTLINX_VERSION}.jar";
var KOTLINX_JVM_SOURCES_URL = $"https://repo1.maven.org/maven2/org/jetbrains/kotlinx/kotlinx-coroutines-core-jvm/{KOTLINX_VERSION}/kotlinx-coroutines-core-jvm-{KOTLINX_VERSION}-sources.jar";

var KOTLINX_REACTIVE_JAR_URL = $"https://repo1.maven.org/maven2/org/jetbrains/kotlinx/kotlinx-coroutines-reactive/{KOTLINX_VERSION}/kotlinx-coroutines-reactive-{KOTLINX_VERSION}.jar";
var KOTLINX_REACTIVE_SOURCES_URL = $"https://repo1.maven.org/maven2/org/jetbrains/kotlinx/kotlinx-coroutines-reactive/{KOTLINX_VERSION}/kotlinx-coroutines-reactive-{KOTLINX_VERSION}-sources.jar";

var KOTLINX_RX2_JAR_URL = $"https://repo1.maven.org/maven2/org/jetbrains/kotlinx/kotlinx-coroutines-rx2/{KOTLINX_VERSION}/kotlinx-coroutines-rx2-{KOTLINX_VERSION}.jar";
var KOTLINX_RX2_SOURCES_URL = $"https://repo1.maven.org/maven2/org/jetbrains/kotlinx/kotlinx-coroutines-rx2/{KOTLINX_VERSION}/kotlinx-coroutines-rx2-{KOTLINX_VERSION}-sources.jar";


Task ("externals")
	.WithCriteria (!FileExists ($"./externals/kotlinx-coroutines-core-{KOTLINX_VERSION}.jar"))
	.Does (() =>
{
	EnsureDirectoryExists ("./externals/");

	// Download Dependencies
	DownloadFile (KOTLINX_CORE_JAR_URL, $"./externals/kotlinx-coroutines-core-{KOTLINX_VERSION}.jar");
	DownloadFile (KOTLINX_CORE_SOURCES_URL, $"./externals/kotlinx-coroutines-core-sources-{KOTLINX_VERSION}.jar");

	DownloadFile (KOTLINX_ANDROID_JAR_URL, $"./externals/kotlinx-coroutines-android-{KOTLINX_VERSION}.jar");
	DownloadFile (KOTLINX_ANDROID_SOURCES_URL, $"./externals/kotlinx-coroutines-android-sources-{KOTLINX_VERSION}.jar");

	DownloadFile (KOTLINX_JDK8_JAR_URL, $"./externals/kotlinx-coroutines-jdk8-{KOTLINX_VERSION}.jar");
	DownloadFile (KOTLINX_JDK8_SOURCES_URL, $"./externals/kotlinx-coroutines-jdk8-sources-{KOTLINX_VERSION}.jar");

	DownloadFile (KOTLINX_JVM_JAR_URL, $"./externals/kotlinx-coroutines-core-jvm-{KOTLINX_VERSION}.jar");
	DownloadFile (KOTLINX_JVM_SOURCES_URL, $"./externals/kotlinx-coroutines-core-jvm-sources-{KOTLINX_VERSION}.jar");
	
	DownloadFile (KOTLINX_REACTIVE_JAR_URL, $"./externals/kotlinx-coroutines-reactive-{KOTLINX_VERSION}.jar");
	DownloadFile (KOTLINX_REACTIVE_SOURCES_URL, $"./externals/kotlinx-coroutines-reactive-sources-{KOTLINX_VERSION}.jar");

	DownloadFile (KOTLINX_RX2_JAR_URL, $"./externals/kotlinx-coroutines-rx2-{KOTLINX_VERSION}.jar");
	DownloadFile (KOTLINX_RX2_SOURCES_URL, $"./externals/kotlinx-coroutines-rx2-sources-{KOTLINX_VERSION}.jar");

	Unzip ($"./externals/kotlinx-coroutines-core-sources-{KOTLINX_VERSION}.jar", "./externals/kotlinx-coroutines-core-javasources/");
	Unzip ($"./externals/kotlinx-coroutines-android-sources-{KOTLINX_VERSION}.jar", "./externals/kotlinx-coroutines-android-javasources/");
	Unzip ($"./externals/kotlinx-coroutines-jdk8-sources-{KOTLINX_VERSION}.jar", "./externals/kotlinx-coroutines-jdk8-javasources/");
	Unzip ($"./externals/kotlinx-coroutines-core-jvm-sources-{KOTLINX_VERSION}.jar", "./externals/kotlinx-coroutines-core-jvm-javasources/");
	Unzip ($"./externals/kotlinx-coroutines-reactive-sources-{KOTLINX_VERSION}.jar", "./externals/kotlinx-coroutines-reactive-javasources/");
	Unzip ($"./externals/kotlinx-coroutines-rx2-sources-{KOTLINX_VERSION}.jar", "./externals/kotlinx-coroutines-rx2-javasources/");


	// Update .csproj and .targets with nuget and artifact versions
	XmlPoke("./source/KotlinX.Coroutines.Core/KotlinX.Coroutines.Core.csproj", "/Project/PropertyGroup/PackageVersion", KOTLINX_NUGET_VERSION);
	XmlPoke("./source/KotlinX.Coroutines.Android/KotlinX.Coroutines.Android.csproj", "/Project/PropertyGroup/PackageVersion", KOTLINX_NUGET_VERSION);
	XmlPoke("./source/KotlinX.Coroutines.Jdk8/KotlinX.Coroutines.Jdk8.csproj", "/Project/PropertyGroup/PackageVersion", KOTLINX_NUGET_VERSION);
	XmlPoke("./source/KotlinX.Coroutines.Core.Jvm/KotlinX.Coroutines.Core.Jvm.csproj", "/Project/PropertyGroup/PackageVersion", KOTLINX_NUGET_VERSION);
	XmlPoke("./source/KotlinX.Coroutines.Reactive/KotlinX.Coroutines.Reactive.csproj", "/Project/PropertyGroup/PackageVersion", KOTLINX_NUGET_VERSION);
	XmlPoke("./source/KotlinX.Coroutines.Rx2/KotlinX.Coroutines.Rx2.csproj", "/Project/PropertyGroup/PackageVersion", KOTLINX_NUGET_VERSION);

	XmlPoke("./source/KotlinX.Coroutines.Core/KotlinX.Coroutines.Core.csproj", "/Project/PropertyGroup/ArtifactVersion", KOTLINX_VERSION);
	XmlPoke("./source/KotlinX.Coroutines.Android/KotlinX.Coroutines.Android.csproj", "/Project/PropertyGroup/ArtifactVersion", KOTLINX_VERSION);
	XmlPoke("./source/KotlinX.Coroutines.Jdk8/KotlinX.Coroutines.Jdk8.csproj", "/Project/PropertyGroup/ArtifactVersion", KOTLINX_VERSION);
	XmlPoke("./source/KotlinX.Coroutines.Core.Jvm/KotlinX.Coroutines.Core.Jvm.csproj", "/Project/PropertyGroup/ArtifactVersion", KOTLINX_VERSION);
	XmlPoke("./source/KotlinX.Coroutines.Reactive/KotlinX.Coroutines.Reactive.csproj", "/Project/PropertyGroup/ArtifactVersion", KOTLINX_VERSION);
	XmlPoke("./source/KotlinX.Coroutines.Rx2/KotlinX.Coroutines.Rx2.csproj", "/Project/PropertyGroup/ArtifactVersion", KOTLINX_VERSION);

	// XmlPoke("./source/KotlinX.Coroutines.Core/Xamarin.KotlinX.Coroutines.Core.targets", "/Project/PropertyGroup/ArtifactVersion", KOTLINX_VERSION);
	// XmlPoke("./source/KotlinX.Coroutines.Android/Xamarin.KotlinX.Coroutines.Android.targets", "/Project/PropertyGroup/ArtifactVersion", KOTLINX_VERSION);
	// XmlPoke("./source/KotlinX.Coroutines.Jdk8/Xamarin.KotlinX.Coroutines.Jdk8.targets", "/Project/PropertyGroup/ArtifactVersion", KOTLINX_VERSION);
	// XmlPoke("./source/KotlinX.Coroutines.Core.Jvm/Xamarin.KotlinX.Coroutines.Core.Jvm.targets", "/Project/PropertyGroup/ArtifactVersion", KOTLINX_VERSION);
	// XmlPoke("./source/KotlinX.Coroutines.Reactive/Xamarin.KotlinX.Coroutines.Reactive.targets", "/Project/PropertyGroup/ArtifactVersion", KOTLINX_VERSION);
	// XmlPoke("./source/KotlinX.Coroutines.Rx2/Xamarin.KotlinX.Coroutines.Rx2.targets", "/Project/PropertyGroup/ArtifactVersion", KOTLINX_VERSION);

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
	MSBuild("./KotlinXCoroutines.sln", c => {
		c.Configuration = "Release";
		c.Restore = true;
		c.Properties.Add("DesignTimeBuild", new [] { "false" });
	});
});

Task("nuget")
	.IsDependentOn("libs")
	.Does(() =>
{
	MSBuild ("./KotlinXCoroutines.sln", c => {
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
		DeleteDirectory ("./externals", new DeleteDirectorySettings {
												Recursive = true,
												Force = true
											});
});

RunTarget (TARGET);

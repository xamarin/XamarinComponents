
var target = Argument("target", "Default");

var PACKAGE_VERSION = "1.0.0-preview02";

var TOOL_VERSION = "26.5.0";

var KXML_VERSION = "2.3.0";
var KOTLIN_VERSION = "1.3.41";
var GUAVA_VERSION = "27.0.1-jre";

Task("externals")
    .Does(() =>
{
    Download($"https://dl.google.com/dl/android/maven2/com/android/tools/build/manifest-merger/{TOOL_VERSION}/manifest-merger-{TOOL_VERSION}.jar", "manifest-merger.jar");
    Download($"https://dl.google.com/dl/android/maven2/com/android/tools/common/{TOOL_VERSION}/common-{TOOL_VERSION}.jar", "common.jar");
    Download($"https://dl.google.com/dl/android/maven2/com/android/tools/sdklib/{TOOL_VERSION}/sdklib-{TOOL_VERSION}.jar", "sdklib.jar");
    Download($"https://dl.google.com/dl/android/maven2/com/android/tools/sdk-common/{TOOL_VERSION}/sdk-common-{TOOL_VERSION}.jar", "sdk-common.jar");
    Download($"https://repo1.maven.org/maven2/net/sf/kxml/kxml2/{KXML_VERSION}/kxml2-{KXML_VERSION}.jar", "kxml2.jar");
    Download($"https://repo1.maven.org/maven2/com/google/guava/guava/{GUAVA_VERSION}/guava-{GUAVA_VERSION}.jar", "guava.jar");
    Download($"https://repo1.maven.org/maven2/org/jetbrains/kotlin/kotlin-stdlib-jdk8/{KOTLIN_VERSION}/kotlin-stdlib-jdk8-{KOTLIN_VERSION}.jar", "kotlin-stdlib-jdk8.jar");
    Download($"https://repo1.maven.org/maven2/org/jetbrains/kotlin/kotlin-stdlib/{KOTLIN_VERSION}/kotlin-stdlib-{KOTLIN_VERSION}.jar", "kotlin-stdlib.jar");

    void Download(string url, string filename)
    {
        var root = (DirectoryPath)"./externals/merger/";
        EnsureDirectoryExists(root);

        var dest = root.CombineWithFilePath(filename);

        if (!FileExists(dest))
            DownloadFile(url, dest);
    }
});

Task("nuget")
    .IsDependentOn("externals")
    .Does(() =>
{
    NuGetPack("./nuget/Xamarin.Android.ManifestMerger.nuspec", new NuGetPackSettings {
        Version = PACKAGE_VERSION,
        OutputDirectory = "./output"
    });
});

Task("samples")
    .IsDependentOn("nuget")
    .Does(() =>
{
    if (DirectoryExists("./externals/packages/xamarin.android.manifestmerger/"))
        CleanDirectories("./externals/packages/xamarin.android.manifestmerger/");

    var settings = new MSBuildSettings()
        .SetConfiguration("Release")
        .SetVerbosity(Verbosity.Minimal)
        .WithRestore();

    MSBuild("./samples/FancyMergingApp.sln", settings);
});

Task("Default")
    .IsDependentOn("externals")
    .IsDependentOn("nuget")
    .IsDependentOn("samples");

RunTarget(target);

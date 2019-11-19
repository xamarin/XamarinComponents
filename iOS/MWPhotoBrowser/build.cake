#addin nuget:?package=Cake.XCode&version=4.2.0
#addin nuget:?package=Cake.Xamarin.Build&version=4.1.2
#addin nuget:?package=Cake.FileHelpers&version=3.2.1

var TARGET = Argument("t", Argument("target", "ci"));

var NUGET_VERSION = "2.1.2";
var POD_VERSION = "2.1.2";

var PODFILE =
$@"platform :ios, '8.0'
install! 'cocoapods', :integrate_targets => false
target 'Xamarin' do
	pod 'MWPhotoBrowser', '{POD_VERSION}'
end";

Task("externals")
	.WithCriteria(!FileExists("./externals/libMWPhotoBrowser.a"))
	.Does(() => 
{
	EnsureDirectoryExists("./externals/");

	// download pod
	FileWriteText("./externals/Podfile", PODFILE);
	CocoaPodRepoUpdate();
	CocoaPodInstall("./externals");

	// build pod
	XCodeBuild(new XCodeBuildSettings {
		Project = "./externals/Pods/Pods.xcodeproj",
		Target = "MWPhotoBrowser",
		Sdk = "iphoneos",
		Configuration = "Release",
	});
	XCodeBuild(new XCodeBuildSettings {
		Project = "./externals/Pods/Pods.xcodeproj",
		Target = "MWPhotoBrowser",
		Sdk = "iphonesimulator",
		Configuration = "Release",
	});

	// build fat archive
	RunLipoCreate("./", 
		"./externals/libMWPhotoBrowser.a",
		"./externals/build/Release-iphoneos/MWPhotoBrowser/libMWPhotoBrowser.a",
		"./externals/build/Release-iphonesimulator/MWPhotoBrowser/libMWPhotoBrowser.a");

	// update csproj
	XmlPoke("./source/MWPhotoBrowser/MWPhotoBrowser.csproj", "/Project/PropertyGroup/PackageVersion", NUGET_VERSION);
});

Task("libs")
	.IsDependentOn("externals")
	.Does(() =>
{
	MSBuild("./source/MWPhotoBrowser.sln", new MSBuildSettings()
		.EnableBinaryLogger("./output/libs.binlog")
		.SetConfiguration("Release")
		.SetMaxCpuCount(0)
		.SetVerbosity(Verbosity.Minimal)
		.WithRestore());
});

Task("nuget")
	.IsDependentOn("libs")
	.Does(() =>
{
	MSBuild("./source/MWPhotoBrowser/MWPhotoBrowser.csproj", new MSBuildSettings()
		.EnableBinaryLogger("./output/nuget.binlog")
		.SetConfiguration("Release")
		.SetMaxCpuCount(0)
		.SetVerbosity(Verbosity.Minimal)
		.WithProperty("NoBuild", "True")
		.WithProperty("PackageOutputPath", MakeAbsolute(new FilePath("./output/")).FullPath)
		.WithTarget("Pack"));
});

Task("samples")
	.IsDependentOn("externals")
	.Does(() =>
{
	MSBuild("./samples/MWPhotoBrowserSample.sln", new MSBuildSettings()
		.EnableBinaryLogger("./output/samples.binlog")
		.SetConfiguration("Release")
		.SetMaxCpuCount(0)
		.SetVerbosity(Verbosity.Minimal)
		.WithProperty("Platform", "iPhone")
		.WithRestore());
});

Task("clean")
	.Does(() =>
{
	if (DirectoryExists("./externals/"))
		DeleteDirectory("./externals/", true);
});

Task("ci")
	.IsDependentOn("externals")
	.IsDependentOn("libs")
	.IsDependentOn("nuget")
	.IsDependentOn("samples");

// this component only builds on macOS
if (IsRunningOnWindows())
	Warning("Building MWPhotoBrowser is only supported on macOS.");
else
	RunTarget(TARGET);

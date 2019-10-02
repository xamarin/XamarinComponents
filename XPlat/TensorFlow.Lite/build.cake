
var TARGET = Argument("t", Argument("target", "Default"));

var NUGET_VERSION = "1.14.0";
var AAR_VERSION = "1.14.0";
var NUGET_PACKAGE_ID = "Xamarin.TensorFlow.Lite";
var AAR_URL = $"https://bintray.com/google/tensorflow/download_file?file_path=org%2Ftensorflow%2Ftensorflow-lite%2F{AAR_VERSION}%2Ftensorflow-lite-{AAR_VERSION}.aar";

Task("externals")
	.WithCriteria(!FileExists("./externals/tensorflow-lite.aar"))
	.Does(() => 
{
	EnsureDirectoryExists("./externals/");
	DownloadFile(AAR_URL, "./externals/tensorflow-lite.aar");

	var csproj = "./source/Xamarin.TensorFlow.Lite.Bindings.XamarinAndroid/Xamarin.TensorFlow.Lite.Bindings.XamarinAndroid.csproj";
	XmlPoke(csproj, "/Project/PropertyGroup/PackageVersion", NUGET_VERSION);
});

Task("libs")
	.IsDependentOn("externals")
	.Does(() =>
{
	MSBuild("./source/Xamarin.TensorFlow.Lite.sln", c => {
		c.Configuration = "Release";
		c.Restore = true;
		c.Properties.Add("PackageOutputPath", new [] { MakeAbsolute(new FilePath("./output")).FullPath });
		c.Properties.Add("DesignTimeBuild", new [] { "false" });
	});
});

// stub because we pack on build
Task("nuget")
	.IsDependentOn("libs");

Task("clean")
	.Does(() =>
{
	if (DirectoryExists("./externals/"))
		DeleteDirectory("./externals", true);
});

RunTarget(TARGET);

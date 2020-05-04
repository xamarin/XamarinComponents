
var TARGET = Argument("t", Argument("target", "ci"));

var NUGET_VERSION = "1.15.0";
var AAR_VERSION = "1.15.0";
var NUGET_PACKAGE_ID = "Xamarin.TensorFlow.Lite";
var AAR_URL_01 = $"https://bintray.com/google/tensorflow/download_file?file_path=org%2Ftensorflow%2Ftensorflow-lite%2F{AAR_VERSION}%2Ftensorflow-lite-{AAR_VERSION}.aar";
var AAR_URL_02 = $"https://bintray.com/google/tensorflow/download_file?file_path=org%2Ftensorflow%2Ftensorflow-lite-gpu%2F{AAR_VERSION}%2Ftensorflow-lite-gpu-{AAR_VERSION}.aar";

Task("externals")
	.WithCriteria(!FileExists("./externals/tensorflow-lite.aar"))
	.WithCriteria(!FileExists("./externals/tensorflow-lite-gpu.aar"))
	.Does(() => 
{
	EnsureDirectoryExists("./externals/");
	DownloadFile(AAR_URL_01, "./externals/tensorflow-lite.aar");
	DownloadFile(AAR_URL_02, "./externals/tensorflow-lite-gpu.aar");

	var csproj_01 = "./source/Xamarin.TensorFlow.Lite.Bindings.XamarinAndroid/Xamarin.TensorFlow.Lite.Bindings.XamarinAndroid.csproj";
	var csproj_02 = "./source/Xamarin.TensorFlow.Lite.Gpu.Bindings.XamarinAndroid/Xamarin.TensorFlow.Lite.Gpu.Bindings.XamarinAndroid.csproj";
	XmlPoke(csproj_01, "/Project/PropertyGroup/PackageVersion", NUGET_VERSION);
	XmlPoke(csproj_02, "/Project/PropertyGroup/PackageVersion", NUGET_VERSION);
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

Task("ci")
	.IsDependentOn("libs")
	.IsDependentOn("nuget");

RunTarget(TARGET);

var TARGET = Argument("t", Argument("target", "ci"));

var NUGET_VERSION = "2.4.0";
var AAR_VERSION = "2.4.0";

string NUGET_PACKAGE_ID_TFLITE = "Xamarin.TensorFlow.Lite";

string AAR_URL_TFLITE = $"https://repo1.maven.org/maven2/org/tensorflow/tensorflow-lite/{AAR_VERSION}/tensorflow-lite-{AAR_VERSION}.aar";
string AAR_URL_TFLITE_POM = $"https://repo1.maven.org/maven2/org/tensorflow/tensorflow-lite/{AAR_VERSION}/tensorflow-lite-{AAR_VERSION}.pom";
string AAR_URL_TFLITE_JAVADOC = $"https://repo1.maven.org/maven2/org/tensorflow/tensorflow-lite/{AAR_VERSION}/tensorflow-lite-{AAR_VERSION}-javadoc.aar";
string AAR_URL_TFLITE_SOURCES = $"https://repo1.maven.org/maven2/org/tensorflow/tensorflow-lite/{AAR_VERSION}/tensorflow-lite-{AAR_VERSION}-sources.aar";

string AAR_URL_TFLITE_GPU = $"https://dl.bintray.com/google/tensorflow/org/tensorflow/tensorflow-lite-gpu/{AAR_VERSION}/tensorflow-lite-gpu-{AAR_VERSION}.aar";
string AAR_URL_TFLITE_GPU_POM = $"https://dl.bintray.com/google/tensorflow/org/tensorflow/tensorflow-lite-gpu/{AAR_VERSION}/tensorflow-lite-gpu-{AAR_VERSION}.pom";

Task("externals")
	.WithCriteria(!FileExists($"./externals/tensorflow-lite-{AAR_VERSION}.aar"))
	.WithCriteria(!FileExists($"./externals/tensorflow-lite-{AAR_VERSION}.pom"))
	.WithCriteria(!FileExists($"./externals/tensorflow-lite-{AAR_VERSION}-javadoc.aar"))
	.WithCriteria(!FileExists($"./externals/tensorflow-lite-{AAR_VERSION}-sources.aar"))

	.WithCriteria(!FileExists($"./externals/tensorflow-lite-gpu-{AAR_VERSION}.aar"))
	.WithCriteria(!FileExists($"./externals/tensorflow-lite-gpu-{AAR_VERSION}.pom"))
	.Does(() => 
{
	EnsureDirectoryExists("./externals/");

	DownloadFile(AAR_URL_TFLITE, $"./externals/tensorflow-lite-{AAR_VERSION}.aar");
	DownloadFile(AAR_URL_TFLITE_POM, $"./externals/tensorflow-lite-{AAR_VERSION}.pom");
	DownloadFile(AAR_URL_TFLITE_JAVADOC, $"./externals/tensorflow-lite-{AAR_VERSION}-javadoc.aar");
	DownloadFile(AAR_URL_TFLITE_SOURCES, $"./externals/tensorflow-lite-{AAR_VERSION}-sources.aar");

	DownloadFile(AAR_URL_TFLITE_GPU, $"./externals/tensorflow-lite-gpu-{AAR_VERSION}.aar");
	DownloadFile(AAR_URL_TFLITE_GPU_POM, $"./externals/tensorflow-lite-gpu-{AAR_VERSION}.pom");

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
		DeleteDirectory("./externals", new DeleteDirectorySettings 
												{
													Recursive = true,
													Force = true 
												}
						);
});

Task("ci")
	.IsDependentOn("libs")
	.IsDependentOn("nuget");

RunTarget(TARGET);

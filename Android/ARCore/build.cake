#addin nuget:?package=Cake.Xamarin.Build
#addin nuget:?package=Cake.Xamarin

var TARGET = Argument ("t", Argument ("target", "Default"));

var NUGET_VERSION = "0.0.1-preview";
var JAVAGL_VERSION = "0.2.1";
var SDK_URL = "https://github.com/google-ar/arcore-android-sdk/releases/download/sdk-preview/arcore-android-sdk-preview.zip";

var buildSpec = new BuildSpec () {
	Libs = new [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./ARCore.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/bin/Release/Xamarin.Google.ARCore.dll",
				}
			}
		}
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Google.ARCore.nuspec", Version = NUGET_VERSION },
	},
};

Task ("externals")
	.Does (() => 
{
	var SDK_ZIP = "./externals/arcoresdk.zip";

	if (!DirectoryExists ("./externals/"))
		CreateDirectory ("./externals");

	if (!FileExists (SDK_ZIP)) {
		DownloadFile (SDK_URL, SDK_ZIP);
		Unzip (SDK_ZIP, "./externals/");
		CopyFile ("./externals/arcore-android-sdk-master/libraries/arcore_client.aar", "./externals/arcore_client.aar");
		CopyFile ("./externals/arcore-android-sdk-master/samples/java_arcore_hello_ar/app/libs/obj-" + JAVAGL_VERSION + ".jar", "./externals/obj.jar");
	}
});


Task ("clean").IsDependentOn ("clean-base").Does (() => 
{	
	if (DirectoryExists ("./externals"))
		DeleteDirectory ("./externals", true);
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);
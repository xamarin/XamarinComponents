
#load "../../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var ANDROID_VERSION = "0954a01934199b8536755f06688d98e3d2a958e4";
var ANDROID_NUGET_VERSION = "0.8.0-beta1";
var ANDROID_URL = string.Format ("https://github.com/googlevr/gvr-android-sdk/archive/{0}.zip", ANDROID_VERSION);

var buildSpec = new BuildSpec {

	Libs = new [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/Google.VR.Android.sln",
			OutputFiles = new [] { 
				new OutputFileCopy { FromFile = "./source/Google.VR.Android/Audio/bin/Release/Xamarin.Google.VR.Android.Audio.dll" },
				new OutputFileCopy { FromFile = "./source/Google.VR.Android/Base/bin/Release/Xamarin.Google.VR.Android.Base.dll" },
				new OutputFileCopy { FromFile = "./source/Google.VR.Android/Common/bin/Release/Xamarin.Google.VR.Android.Common.dll" },
				new OutputFileCopy { FromFile = "./source/Google.VR.Android/CommonWidget/bin/Release/Xamarin.Google.VR.Android.CommonWidget.dll" },
				new OutputFileCopy { FromFile = "./source/Google.VR.Android/Controller/bin/Release/Xamarin.Google.VR.Android.Controller.dll" },
				new OutputFileCopy { FromFile = "./source/Google.VR.Android/PanoWidget/bin/Release/Xamarin.Google.VR.Android.PanoWidget.dll" },
				new OutputFileCopy { FromFile = "./source/Google.VR.Android/VideoWidget/bin/Release/Xamarin.Google.VR.Android.VideoWidget.dll" },
			}
		},
	},

	Samples = new [] {
		new DefaultSolutionBuilder { SolutionPath = "./samples/TreasureHunt.sln" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Google.VR.Android.nuspec", Version = ANDROID_NUGET_VERSION, RequireLicenseAcceptance = true },
	}
};

Task ("externals")
	.WithCriteria (!FileExists ("./externals/android/libraries/base/base.aar"))
	.Does (() => 
{
	EnsureDirectoryExists ("./externals/android");

	DownloadFile (ANDROID_URL, "./externals/android.zip");

	Unzip ("./externals/android.zip", "./externals");
	CopyDirectory ("./externals/gvr-android-sdk-" + ANDROID_VERSION, "./externals/android");
	DeleteDirectory ("./externals/gvr-android-sdk-" + ANDROID_VERSION, true);
	Unzip ("./externals/android/libraries/audio/audio.aar", "./externals/android/libraries/audio");
	Unzip ("./externals/android/libraries/base/base.aar", "./externals/android/libraries/base");
	Unzip ("./externals/android/libraries/common/common.aar", "./externals/android/libraries/common");
	Unzip ("./externals/android/libraries/commonwidget/commonwidget.aar", "./externals/android/libraries/commonwidget");
	Unzip ("./externals/android/libraries/controller/controller.aar", "./externals/android/libraries/controller");
	Unzip ("./externals/android/libraries/panowidget/panowidget.aar", "./externals/android/libraries/panowidget");
	Unzip ("./externals/android/libraries/videowidget/videowidget.aar", "./externals/android/libraries/videowidget");

});

Task ("clean").IsDependentOn ("clean-base").Does (() => 
{
	if (DirectoryExists ("./externals"))
		DeleteDirectory ("./externals", true);
});


SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);
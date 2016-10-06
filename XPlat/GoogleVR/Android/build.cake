
#load "../../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var ANDROID_VERSION = "25e7e14413229d4644a66be77e8f8ddeb3f91fe7";
var ANDROID_NUGET_VERSION = "1.0.0";
var ANDROID_URL = string.Format ("https://github.com/googlevr/gvr-android-sdk/archive/{0}.zip", ANDROID_VERSION);

var PROTOBUF_NANO_VERSION = "3.1.0";
var PROTOBUF_NANO_URL = string.Format ("https://bintray.com/bintray/jcenter/download_file?file_path=com%2Fgoogle%2Fprotobuf%2Fnano%2Fprotobuf-javanano%2F{0}%2Fprotobuf-javanano-{0}.jar", PROTOBUF_NANO_VERSION);

var EXOPLAYER_VERSION = "r1.5.2";
var EXOPLAYER_URL = string.Format ("https://bintray.com/google/exoplayer/download_file?file_path=com%2Fgoogle%2Fandroid%2Fexoplayer%2Fexoplayer%2F{0}%2Fexoplayer-{0}.aar", EXOPLAYER_VERSION);

var buildSpec = new BuildSpec {

	Libs = new [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/Google.VR.Android.sln",
			OutputFiles = new [] { 
				new OutputFileCopy { FromFile = "./source/Google.VR.Android/Protobuf.Nano/bin/Release/Xamarin.Google.Protobuf.Nano.dll" },
				new OutputFileCopy { FromFile = "./source/Google.VR.Android/ExoPlayer/bin/Release/Xamarin.Google.Android.ExoPlayer.dll" },
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

	EnsureDirectoryExists ("./externals/android/libraries/protobuf-javanano");
	DownloadFile (PROTOBUF_NANO_URL, "./externals/android/libraries/protobuf-javanano/classes.jar");

	EnsureDirectoryExists ("./externals/android/libraries/exoplayer");
	DownloadFile (EXOPLAYER_URL, "./externals/android/libraries/exoplayer/exoplayer.aar");
	Unzip ("./externals/android/libraries/exoplayer/exoplayer.aar", "./externals/android/libraries/exoplayer");
});

Task ("clean").IsDependentOn ("clean-base").Does (() => 
{
	if (DirectoryExists ("./externals"))
		DeleteDirectory ("./externals", true);
});


SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

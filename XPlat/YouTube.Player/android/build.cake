
#load "../../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var ANDROID_VERSION = "1.2.2";
var ANDROID_NUGET_VERSION = "1.2.2";
var ANDROID_URL = string.Format ("https://developers.google.com/youtube/android/player/downloads/YouTubeAndroidPlayerApi-{0}.zip", ANDROID_VERSION);

var buildSpec = new BuildSpec {

	Libs = new [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/YouTube.Player.Android.sln",
			OutputFiles = new [] { 
				new OutputFileCopy { FromFile = "./source/YouTube.Player.Android/bin/Release/YouTube.Player.Android.dll" },
			}
		},
	},

	Samples = new ISolutionBuilder [] {
		new DefaultSolutionBuilder { SolutionPath = "./samples/YouTubePlayerSample.sln" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.YouTube.Player.Android.nuspec" },
	},

	// Components = new [] {
	// 	new Component {ManifestDirectory = "./component"},
	// },
};

Task ("externals")
	.WithCriteria (!FileExists ("./externals/ytandroid/libs/YouTubeAndroidPlayerApi.jar"))
	.Does (() => 
{
	EnsureDirectoryExists ("./externals");

	DownloadFile (ANDROID_URL, "./externals/ytandroid.zip");

	Unzip ("./externals/ytandroid.zip", "./externals/ytandroid");

});

Task ("clean").IsDependentOn ("clean-base").Does (() => 
{
	if (DirectoryExists ("./externals"))
		DeleteDirectory ("./externals", true);
});


SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);
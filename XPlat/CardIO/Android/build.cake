#load "../../../common.cake"

var VERSION = "5.4.2";
var URL = string.Format ("https://github.com/card-io/card.io-Android-SDK/archive/{0}.zip", VERSION);

var TARGET = Argument ("t", Argument ("target", "Default"));

var buildSpec = new BuildSpec {
	Libs = new [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/CardIO.Android.sln",
			Configuration = "Release",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/CardIO.Android/bin/Release/Card.IO.Android.dll",
				},			
			}
		},	
	},

	Samples = new [] {
		new DefaultSolutionBuilder { SolutionPath = "./samples/CardIOSampleAndroid.sln" },
	},

	Components = new [] {
		new Component { ManifestDirectory = "./component/" },
	},
};


Task ("externals")
	.IsDependentOn ("externals-base")
	.WithCriteria (!FileExists ("./externals/cardio.android.zip"))
	.Does (() => 
{
	EnsureDirectoryExists ("./externals/");

	DownloadFile (URL, "./externals/cardio.android.zip");

	Unzip ("./externals/cardio.android.zip", "./externals/");

	CopyDirectory (string.Format ("./externals/card.io-Android-SDK-{0}", VERSION), "./externals/card.io-Android-SDK");

	CopyFiles ("./externals/**/*.aar", "./externals/");

	// Remove MIPS architectures that we don't support
	StartProcess ("zip", string.Format ("-d ./externals/card.io-{0}.aar jni/mips64/libcardioDecider.so", VERSION));
	StartProcess ("zip", string.Format ("-d ./externals/card.io-{0}.aar jni/mips/libcardioDecider.so", VERSION));

	MoveFile (string.Format ("./externals/card.io-{0}.aar", VERSION), "./externals/card.io.aar");
	
});

Task ("clean").IsDependentOn ("clean-base").Does (() =>
{
	if (DirectoryExists ("./externals/"))
		DeleteDirectory ("./externals/", true);
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

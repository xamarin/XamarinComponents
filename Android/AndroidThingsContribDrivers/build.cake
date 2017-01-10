#addin nuget:?package=Cake.Xamarin.Build
#addin nuget:?package=Cake.Xamarin
#addin nuget:?package=Cake.FileHelpers

var TARGET = Argument ("t", Argument ("target", "Default"));
var NUGET_VERSION = "0.1";
var AAR_VERSION = "0.1";
FilePath solution = "./Android.Things.Contrib.Drivers.sln";
var NugetToolPath = File ("./tools/nuget.exe");

var Build = new Action<FilePath> ((solution) =>
{
    if (IsRunningOnWindows ()) {
        MSBuild (solution, s => s.SetConfiguration ("Release").SetMSBuildPlatform (MSBuildPlatform.x86));
    } else {
        XBuild (solution, s => s.SetConfiguration ("Release"));
    }
});

var RunNuGetRestore = new Action<FilePath> ((solution) =>
{
    NuGetRestore (solution, new NuGetRestoreSettings { 
        ToolPath = NugetToolPath
    });
});

var AAR_INFOS = new [] {
	new AarInfo ("driver-apa102", string.Format ("https://bintray.com/google/androidthings/download_file?file_path=com%2Fgoogle%2Fandroid%2Fthings%2Fcontrib%2Fdriver-apa102%2F{0}%2Fdriver-apa102-{0}.aar", AAR_VERSION), "./externals/driver-apa102.aar", AAR_VERSION, NUGET_VERSION),
	new AarInfo ("driver-bmx280", string.Format ("https://bintray.com/google/androidthings/download_file?file_path=com%2Fgoogle%2Fandroid%2Fthings%2Fcontrib%2Fdriver-bmx280%2F{0}%2Fdriver-bmx280-{0}.aar", AAR_VERSION), "./externals/driver-bmx280.aar", AAR_VERSION, NUGET_VERSION),
	new AarInfo ("driver-button", string.Format ("https://bintray.com/google/androidthings/download_file?file_path=com%2Fgoogle%2Fandroid%2Fthings%2Fcontrib%2Fdriver-button%2F{0}%2Fdriver-button-{0}.aar", AAR_VERSION), "./externals/driver-button.aar", AAR_VERSION, NUGET_VERSION),
	new AarInfo ("driver-cap12xx", string.Format ("https://bintray.com/google/androidthings/download_file?file_path=com%2Fgoogle%2Fandroid%2Fthings%2Fcontrib%2Fdriver-cap12xx%2F{0}%2Fdriver-cap12xx-{0}.aar", AAR_VERSION), "./externals/driver-cap12xx.aar", AAR_VERSION, NUGET_VERSION),
	new AarInfo ("driver-gps", string.Format ("https://bintray.com/google/androidthings/download_file?file_path=com%2Fgoogle%2Fandroid%2Fthings%2Fcontrib%2Fdriver-gps%2F{0}%2Fdriver-gps-{0}.aar", AAR_VERSION), "./externals/driver-gps.aar", AAR_VERSION, NUGET_VERSION),
	new AarInfo ("driver-ht16k33", string.Format ("https://bintray.com/google/androidthings/download_file?file_path=com%2Fgoogle%2Fandroid%2Fthings%2Fcontrib%2Fdriver-ht16k33%2F{0}%2Fdriver-ht16k33-{0}.aar", AAR_VERSION), "./externals/driver-ht16k33.aar", AAR_VERSION, NUGET_VERSION),
	new AarInfo ("driver-mma7660fc", string.Format ("https://bintray.com/google/androidthings/download_file?file_path=com%2Fgoogle%2Fandroid%2Fthings%2Fcontrib%2Fdriver-mma7660fc%2F{0}%2Fdriver-mma7660fc-{0}.aar", AAR_VERSION), "./externals/driver-mma7660fc.aar", AAR_VERSION, NUGET_VERSION),
	new AarInfo ("driver-pwmservo", string.Format ("https://bintray.com/google/androidthings/download_file?file_path=com%2Fgoogle%2Fandroid%2Fthings%2Fcontrib%2Fdriver-pwmservo%2F{0}%2Fdriver-pwmservo-{0}.aar", AAR_VERSION), "./externals/driver-pwmservo.aar", AAR_VERSION, NUGET_VERSION),
	new AarInfo ("driver-pwmspeaker", string.Format ("https://bintray.com/google/androidthings/download_file?file_path=com%2Fgoogle%2Fandroid%2Fthings%2Fcontrib%2Fdriver-pwmspeaker%2F{0}%2Fdriver-pwmspeaker-{0}.aar", AAR_VERSION), "./externals/driver-pwmspeaker.aar", AAR_VERSION, NUGET_VERSION),
	new AarInfo ("driver-rainbowhat", string.Format ("https://bintray.com/google/androidthings/download_file?file_path=com%2Fgoogle%2Fandroid%2Fthings%2Fcontrib%2Fdriver-rainbowhat%2F{0}%2Fdriver-rainbowhat-{0}.aar", AAR_VERSION), "./externals/driver-rainbowhat.aar", AAR_VERSION, NUGET_VERSION),
	new AarInfo ("driver-ssd1306", string.Format ("https://bintray.com/google/androidthings/download_file?file_path=com%2Fgoogle%2Fandroid%2Fthings%2Fcontrib%2Fdriver-ssd1306%2F{0}%2Fdriver-ssd1306-{0}.aar", AAR_VERSION), "./externals/driver-ssd1306.aar", AAR_VERSION, NUGET_VERSION),
	new AarInfo ("driver-tm1637", string.Format ("https://bintray.com/google/androidthings/download_file?file_path=com%2Fgoogle%2Fandroid%2Fthings%2Fcontrib%2Fdriver-tm1637%2F{0}%2Fdriver-tm1637-{0}.aar", AAR_VERSION), "./externals/driver-tm1637.aar", AAR_VERSION, NUGET_VERSION)
};

var buildSpec = new BuildSpec () {
	Libs = new [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./Android.Things.Contrib.Drivers.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/bin/Release/Xamarin.Android.Things.Contrib.Drivers.dll",
				}
			}
		}
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Android.Things.Contrib.Drivers.nuspec", Version = NUGET_VERSION },
	},
};

class AarInfo
{
	public AarInfo (string name, string aarUrl, string aarDest, string aarVersion, string nugetVersion)
	{
		Name = name;
		AarUrl = aarUrl;
		AarDest = aarDest;
		AarVersion = aarVersion;
		NuGetVersion = nugetVersion;
	}

	public string Name { get; set; }
	public string AarUrl { get; set; }
	public string AarDest {get; set; }
	public string AarVersion { get; set; }
	public string NuGetVersion { get; set; }
}

Task ("externals")
	.Does (() => 
{
	if (!DirectoryExists ("./externals/"))
		CreateDirectory ("./externals");

	foreach (var aar in AAR_INFOS)
	{
		if (!FileExists (aar.AarDest))
		DownloadFile (aar.AarUrl, aar.AarDest);
	}

});

Task ("libs")
	.Does (() => 
{
	RunNuGetRestore (solution);
	 Build (solution);

});


Task ("clean").IsDependentOn ("clean-base").Does (() => 
{	
	if (DirectoryExists ("./externals"))
		DeleteDirectory ("./externals", true);
});

Task("Default")
    .IsDependentOn("externals")
	.IsDependentOn("libs");

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);
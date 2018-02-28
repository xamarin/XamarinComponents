#addin nuget:?package=Cake.FileHelpers&version=1.0.4
#addin nuget:?package=Cake.Xamarin&version=1.3.0.15
#addin nuget:?package=Cake.Android.Adb
#tool nuget:?package=NUnit.Runners&version=2.6.4
#tool nuget:?package=Xamarin.UITest&version=2.0.5

/************************************************************************************************************
*  .XAM Xamarin Component Validation Script
* 
* The purpose of this script is to take a built .xam component file and extract its contents to validate that
* the samples build and if there are any UITests for those samples, optionally run them either locally
* or on Xamarin's Test Cloud
*
* Limitations:
*  - Currently this script will only compile Android samples and execute UITests for them
*
* Assumptions:
*  - Your UITests should be setup in such a way that the APK file is loaded as `app.apk` in the same
*    directory as your UITest .dll assembly file.
*  - Your UITests should be setup to run on a device specified by the serial.  This value is passed
*    to the UITest via an environment variable named `XTC_DEVICE_ID`
*
************************************************************************************************************/

var pattern = Argument("pattern", Argument ("p", (string)null)) ?? "./**/*.xam";
var runUITests = Argument("uitests", "false").ToLower () == "true";
var useTestCloud = Argument("testcloud", "false").ToLower () == "true";

var rxCsProj = new System.Text.RegularExpressions.Regex (@"Project\s?\(\s?""(?<type>.*?)""\)\s?=\s?""(?<name>.*?)""\s?,\s?""(?<path>.*?)""\s?,\s?""(?<guid>.*?)""\s?", System.Text.RegularExpressions.RegexOptions.Singleline);

// Test Cloud parameters should come from Environment vars (setup in CI)
var XTC_API_KEY = EnvironmentVariable ("XTC_API_KEY");
var XTC_DEVICE_HASH = EnvironmentVariable ("XTC_DEVICE_HASH");
var XTC_EMAIL = EnvironmentVariable ("XTC_EMAIL");
var XTC_SERIES = EnvironmentVariable ("XTC_SERIES") ?? "Master";
var XTC_LOCALE = EnvironmentVariable ("XTC_LOCALE") ?? "en_US";
var LOCAL_DEVICES = (EnvironmentVariable ("LOCAL_DEVICES") ?? "").Split (',',';');

var NUGET_SOURCES = Argument ("nuget_sources", EnvironmentVariable ("NUGET_SOURCES") ?? "").Split (',',';');

Task ("Default").Does (() => {
    // Clean up and recreate the temp dir
    var temp = "./temp";
	if (DirectoryExists (temp))
		DeleteDirectory (temp, true);
	EnsureDirectoryExists (temp);

    // Find the pattern of .xam component files to validate
	var xamFiles = GetFiles (pattern);

    // We want to keep track of all the apps we need to validate
	var appInfos = new List<AppInfo> ();

    var tempProjNum = 0;

    // Inspect each .xam file we find
	foreach (var xamFile in xamFiles) {
        tempProjNum++;

		Information ("{0}", xamFile);

        // .xam files are just zip files, unzip to work with it
		Unzip (xamFile, temp);

        // The unzipped temp dir will probably have a long name, so let's shorten it to avoid MAX_PATH issues on windows
        var componentPath = new DirectoryPath (temp + "/" + tempProjNum);
        MoveDirectory (temp + "/" + xamFile.GetFilenameWithoutExtension (), componentPath);

        // Ensure a manifest exists for the component
		if (!FileExists (componentPath + "/component/Manifest.xml"))
			Error ("Component contains no manifest: {0}", componentPath);

        Information ("Looking for samples: {0}", componentPath + "/samples/**/*.sln");
        // Find all the .sln's within the samples folder
		var sampleSlns = GetFiles (componentPath + "/samples/**/*.sln");

        // Look through all the sample sln files
		foreach (var sampleSln in sampleSlns) {

            Information ("Sample: {0}", sampleSln);
			
            var nugetRestoreSettings = new NuGetRestoreSettings ();
            if (NUGET_SOURCES != null && NUGET_SOURCES.Length > 0)
                nugetRestoreSettings.Source = NUGET_SOURCES;

			NuGetRestore (sampleSln, nugetRestoreSettings);

			MSBuild (sampleSln, c => c.Configuration = "Release");

			var androidCsProj = "";
			var uitestCsProj = "";

            // Look for any csproj references in the solution
			var slnProjMatches = rxCsProj.Matches (FileReadText (sampleSln));

			Information ("{0}", slnProjMatches);

            // Check each project in the solution
			foreach (System.Text.RegularExpressions.Match projMatch in slnProjMatches) {

                // Get the relative path to the csproj from our regex match
				var relativeCsProj = projMatch?.Groups?["path"]?.Value;

				if (string.IsNullOrEmpty (relativeCsProj))
					continue;

                // Build the absolute path to the csproj
				var absCsProj = sampleSln.GetDirectory ().FullPath.TrimEnd ('/') + "/" + relativeCsProj.Replace ('\\', '/').TrimStart ('/');
                
                // Read in the csproj text
				var projText = FileReadText (absCsProj);

				// Check to see what type of project it is based on certain text in the csproj file
                // An existence of this GUID (which is a project type guid) indicates
				if (projText.Contains ("{EFBA0AD7-5A72-4C68-AF49-83D382785DCF}"))
					androidCsProj = absCsProj;
				else if (projText.Contains ("<Reference Include=\"Xamarin.UITest\">")) // UITests reference Xamarin.UITest
					uitestCsProj = absCsProj;
			}

            // Make sure we have an android csproj and corresponding uitest proj
			if (string.IsNullOrEmpty (androidCsProj)) {
				Warning ("Couldn't find Android App Project: {0}", androidCsProj);
				continue;
			}
            Information ("Found Android Project: {0}", androidCsProj);
			if (string.IsNullOrEmpty (uitestCsProj)) {
				Warning ("Couldn't find UITest Project: {0}", uitestCsProj);
				continue;
			}
            Information ("Found UITest  Project: {0}", uitestCsProj);

            // Next we need to find the output assembly name for the uitest project
			var asmNameMatch = System.Text.RegularExpressions.Regex.Match (FileReadText (uitestCsProj), "<AssemblyName>(?<name>.*?)</AssemblyName>", System.Text.RegularExpressions.RegexOptions.Singleline);
			var uitestAssemblyName = (asmNameMatch?.Groups?["name"]?.Value ?? "") + ".dll";
			Information ("UITest Assembly Name: {0}", uitestAssemblyName);

            // Find the actual assembly file on disk for UITests
			var uitestAssemblyFile = GetFiles (new FilePath (uitestCsProj).GetDirectory().FullPath.TrimEnd ('/') + "/**/" + uitestAssemblyName).FirstOrDefault ();
			Information ("UITest Assembly File: {0}", uitestAssemblyFile);

            // Package up the android APK
			var apk = AndroidPackage (androidCsProj, false, c => c.Configuration = "Release");
			Information ("Apk: {0}", apk);

            // Move the apk file to the location (and name) that UITest will expect to load it from (it will expect it relative the UITest assembly itself, named app.apk)
			var movedApk = uitestAssemblyFile.GetDirectory ().CombineWithFilePath ("app.apk");
			CopyFile (apk, movedApk);

            // If everything checks out, add this to our list of apk's to process.
			if (FileExists (movedApk) && FileExists (uitestAssemblyFile))
				appInfos.Add (new AppInfo { Name = uitestAssemblyName.Replace (".", "-"), ApkFile = movedApk, UITestAssembly = uitestAssemblyFile });
		}
	}

    // First we built all the sample apk's for samples, now we can run uitests on them either locally or on test cloud
    if (runUITests) {
        foreach (var appInfo in appInfos) {
            try {
                if (useTestCloud) {
                    TestCloud (appInfo.ApkFile, XTC_API_KEY, XTC_DEVICE_HASH, XTC_EMAIL, appInfo.UITestAssembly.GetDirectory (), 
                        new TestCloudSettings { 
                            Series = XTC_SERIES, 
                            Locale = XTC_LOCALE,
                            NUnitXmlFile = "./xtc-uitests-" + appInfo.Name + ".xml",
                            TestChunk = true,
                    });

                } else {
                    // If devices weren't specified explicitly, just get everything available to ADB
                    if (LOCAL_DEVICES == null || LOCAL_DEVICES.Length <= 0)
                        LOCAL_DEVICES = AdbDevices (null).Select (d => d.Serial).ToArray ();

                    // Run tests on all devices specified
                    foreach (var deviceSerial in LOCAL_DEVICES) {
                        // The UITest should expect to get the device id from the XTC_DEVICE_ID Environment variable
                        System.Environment.SetEnvironmentVariable ("XTC_DEVICE_ID", deviceSerial);

                        // Run the UITest
                        UITest (appInfo.UITestAssembly, new NUnitSettings { ResultsFile = "./uitests-" + appInfo.Name + ".xml" });
                    }
                }
                
            } catch (Exception ex) {
                Warning ("UITests Test(s) Failed: {0}", ex);
            }
        }
    }
});

public class AppInfo
{
	public string Name { get;set; }
	public FilePath ApkFile { get;set; }
	public FilePath UITestAssembly { get;set; }
}

RunTarget (Argument ("target", Argument ("t", "Default")));

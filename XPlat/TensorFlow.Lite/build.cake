/*
#########################################################################################
Installing

	Windows - powershell
		
        Invoke-WebRequest http://cakebuild.net/download/bootstrapper/windows -OutFile build.ps1
        .\build.ps1

	Windows - cmd.exe prompt	
	
        powershell ^
			Invoke-WebRequest http://cakebuild.net/download/bootstrapper/windows -OutFile build.ps1
        powershell ^
			.\build.ps1
	
	Mac OSX 

        rm -fr tools/; mkdir ./tools/ ; \
        cp cake.packages.config ./tools/packages.config ; \
        curl -Lsfo build.sh http://cakebuild.net/download/bootstrapper/osx ; \
        chmod +x ./build.sh ;
        ./build.sh

	Linux

        curl -Lsfo build.sh http://cakebuild.net/download/bootstrapper/linux
        chmod +x ./build.sh && ./build.sh

Running Cake to Build targets

	Windows

		tools\Cake\Cake.exe --verbosity=diagnostic --target=libs
		tools\Cake\Cake.exe --verbosity=diagnostic --target=nuget
		tools\Cake\Cake.exe --verbosity=diagnostic --target=samples

		tools\Cake\Cake.exe -experimental --verbosity=diagnostic --target=libs
		tools\Cake\Cake.exe -experimental --verbosity=diagnostic --target=nuget
		tools\Cake\Cake.exe -experimental --verbosity=diagnostic --target=samples
		
	Mac OSX 
	
		mono tools/Cake/Cake.exe --verbosity=diagnostic --target=libs
		mono tools/Cake/Cake.exe --verbosity=diagnostic --target=nuget
#########################################################################################
*/
#load "../../common.cake"
//#load "apidiffutils.cake"

#tool nuget:?package=XamarinComponent

#addin nuget:?package=Cake.XCode
#addin nuget:?package=Cake.Xamarin.Build
#addin nuget:?package=Cake.Xamarin
#addin nuget:?package=Cake.FileHelpers

var NUGET_VERSION = "1.12.0";
var AAR_VERSION = "1.12.0";
var NUGET_PACKAGE_ID = "Xamarin.TensorFlow.Lite";

var TARGET = Argument ("t", Argument ("target", "Default"));

string AAR_URL=$"https://bintray.com/google/tensorflow/download_file?file_path=org%2Ftensorflow%2Ftensorflow-lite%2F{AAR_VERSION}%2Ftensorflow-lite-{AAR_VERSION}.aar";

var TRACKED_NUGETS = new Dictionary<string, Version> {
    { "Xamarin.TensorFlow.Lite",                          new Version (1, 9, 0) },
};




BuildSpec buildSpec = new BuildSpec () 
{
	Libs = new ISolutionBuilder [] 
	{
		new DefaultSolutionBuilder 
		{
			SolutionPath = "./source/Xamarin.TensorFlow.Lite.sln",
			OutputFiles = new [] 
			{ 
				new OutputFileCopy 
				{ 
					FromFile = "./source/Xamarin.TensorFlow.Lite.Bindings.XamarinAndroid/bin/Release/monoandroid81/Xamarin.TensorFlow.Lite.dll" 
				},
				new OutputFileCopy 
				{ 
					FromFile = $"source/Xamarin.TensorFlow.Lite.Bindings.XamarinAndroid/bin/Release/{NUGET_PACKAGE_ID}.{NUGET_VERSION}.nupkg" 
				},
				// new OutputFileCopy 
				// { 
				// 	FromFile = "./source/Xamarin.TensorFlow.Lite.Bindings.XamarinIOS/bin/Release/Xamarin.TensorFlow.Lite.dll" 
				// },
			}
		}
	},

	Samples = new ISolutionBuilder [] 
	{
		new DefaultSolutionBuilder 
		{ 
			SolutionPath = "./samples/Xamarin.TensorFlow.Lite.Samples.sln" 
		},	
	},

	Components = new []
	{
		new Component 
		{ 
			ManifestDirectory = "./component" 
		},
	},

};

Task ("externals")
	.IsDependentOn ("externals-base")
	// .WithCriteria (!FileExists ("./externals/Xamarin.TensorFlow.Lite.aar"))
	.Does 
	(
		() => 
		{
			Information("externals ...");
			EnsureDirectoryExists("./externals/android");
			Information("    downloading ...");

			string file = $"./externals/android/tensorflow-lite-{AAR_VERSION}.aar";
			if ( ! string.IsNullOrEmpty(AAR_URL) && ! FileExists(file))
			{
				DownloadFile (AAR_URL, file);
			}
		}
	);


Task ("clean")
	.IsDependentOn ("clean-base")
	.Does
	(
		() => 
		{
			if (DirectoryExists ("./externals/"))
			{
				DeleteDirectory ("./externals", true);
			}
		}
	);

Task ("docs-api-diff")
	.IsDependentOn ("nuget")
    .Does (async () =>
{
    var baseDir = "./output/api-diff";
    CleanDirectories (baseDir);

	var comparer = CreateNuGetDiff();
	comparer.IgnoreResolutionErrors = true;

	var version = NUGET_VERSION;
	var latestVersion = (await NuGetVersions.GetLatestAsync (NUGET_PACKAGE_ID))?.ToNormalizedString ();

	// pre-cache so we can have better logs
	if (!string.IsNullOrEmpty (latestVersion)) {
		Debug ($"Caching version '{latestVersion}' of '{NUGET_PACKAGE_ID}'...");
		await comparer.ExtractCachedPackageAsync (NUGET_PACKAGE_ID, latestVersion);
	}

	Debug ($"Running a diff on '{latestVersion}' vs '{version}' of '{NUGET_PACKAGE_ID}'...");
	var diffRoot = $"{baseDir}/{NUGET_PACKAGE_ID}";
	using (var reader = new PackageArchiveReader ($"./output/{NUGET_PACKAGE_ID.ToLower ()}.{version}.nupkg")) 
	{
		// run the diff with just the breaking changes
		comparer.MarkdownDiffFileExtension = ".breaking.md";
		comparer.IgnoreNonBreakingChanges = true;
		await comparer.SaveCompleteDiffToDirectoryAsync (NUGET_PACKAGE_ID, latestVersion, reader, diffRoot);
		// run the diff on everything
		comparer.MarkdownDiffFileExtension = null;
		comparer.IgnoreNonBreakingChanges = false;
		await comparer.SaveCompleteDiffToDirectoryAsync (NUGET_PACKAGE_ID, latestVersion, reader, diffRoot);
	}

	CopyChangelogs (diffRoot, NUGET_PACKAGE_ID, version, "./output/changelogs");

    Information ($"Diff complete of '{NUGET_PACKAGE_ID}'.");

    // clean up after working
    CleanDirectories (baseDir);
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

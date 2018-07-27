
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var Crashlytics_VER = "2.9.4";
var CrashlyticsCore_VER = "2.6.3";
var CrashlyticsBeta_VER = "1.2.9";
var CrashlyticsAnswers_VER = "1.4.2";
var Fabric_VER = "1.4.3";
var Crashlytics_URL = string.Format ("https://maven.google.com/com/crashlytics/sdk/android/crashlytics/{0}/crashlytics-{0}.aar", Crashlytics_VER);
var CrashlyticsCore_URL = string.Format ("https://maven.google.com/com/crashlytics/sdk/android/crashlytics-core/{0}/crashlytics-core-{0}.aar", CrashlyticsCore_VER);
var CrashlyticsBeta_URL = string.Format ("https://maven.google.com/com/crashlytics/sdk/android/beta/{0}/beta-{0}.aar", CrashlyticsBeta_VER);
var CrashlyticsAnswers_URL = string.Format ("https://maven.google.com/com/crashlytics/sdk/android/answers/{0}/answers-{0}.aar", CrashlyticsAnswers_VER);
var Fabric_URL = string.Format ("https://maven.google.com/io/fabric/sdk/android/fabric/{0}/fabric-{0}.aar", Fabric_VER);
var Crashlytics_DEST = "./externals/Crashlytics.aar";
var CrashlyticsCore_DEST = "./externals/CrashlyticsCore.aar";
var CrashlyticsBeta_DEST = "./externals/CrashlyticsBeta.aar";
var CrashlyticsAnswers_DEST = "./externals/CrashlyticsAnswers.aar";
var Fabric_DEST = "./externals/Fabric.aar";

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/Crashlytics.sln",
			OutputFiles = new [] { 
				new OutputFileCopy { FromFile = "./source/Crashlytics/bin/Release/Crashlytics.dll" },
				new OutputFileCopy { FromFile = "./source/CrashlyticsAnswers/bin/Release/CrashlyticsAnswers.dll" },
				new OutputFileCopy { FromFile = "./source/CrashlyticsBeta/bin/Release/CrashlyticsBeta.dll" },
				new OutputFileCopy { FromFile = "./source/CrashlyticsCore/bin/Release/CrashlyticsCore.dll" },
				new OutputFileCopy { FromFile = "./source/Fabric/bin/Release/Fabric.dll" },				
			}
		}
	},
};

Task ("externals")
	.Does (() => 
{
	if (!DirectoryExists ("./externals/"))
		CreateDirectory ("./externals");

	if (!FileExists (Crashlytics_DEST))
		DownloadFile (Crashlytics_URL, Crashlytics_DEST);

	if (!FileExists (CrashlyticsCore_DEST))
		DownloadFile (CrashlyticsCore_URL, CrashlyticsCore_DEST);

	if (!FileExists (CrashlyticsBeta_DEST))
		DownloadFile (CrashlyticsBeta_URL, CrashlyticsBeta_DEST);

	if (!FileExists (CrashlyticsAnswers_DEST))
		DownloadFile (CrashlyticsAnswers_URL, CrashlyticsAnswers_DEST);

	if (!FileExists (Fabric_DEST))
		DownloadFile (Fabric_URL, Fabric_DEST);
});


Task ("clean").IsDependentOn ("clean-base").Does (() => 
{	
	DeleteFiles ("./externals/*.aar");
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

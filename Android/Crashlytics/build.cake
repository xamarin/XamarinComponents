#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var Crashlytics_VER = "2.9.4";
var Crashlytics_NuGet_VER = Crashlytics_VER + ".2";
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

Task ("externals")
	.Does (() => 
{
	if (!DirectoryExists ("./externals/"))
		CreateDirectory ("./externals");

	if (!FileExists (Crashlytics_DEST)) {
		DownloadFile (Crashlytics_URL, Crashlytics_DEST);
		Unzip(Crashlytics_DEST, "./externals/Crashlytics/");
	}

	if (!FileExists (CrashlyticsCore_DEST)) {
		DownloadFile (CrashlyticsCore_URL, CrashlyticsCore_DEST);
		Unzip(CrashlyticsCore_DEST, "./externals/CrashlyticsCore/");
	}

	if (!FileExists (CrashlyticsBeta_DEST)) {
		DownloadFile (CrashlyticsBeta_URL, CrashlyticsBeta_DEST);
		Unzip(CrashlyticsBeta_DEST, "./externals/CrashlyticsBeta/");
	}

	if (!FileExists (CrashlyticsAnswers_DEST)) {
		DownloadFile (CrashlyticsAnswers_URL, CrashlyticsAnswers_DEST);
		Unzip(CrashlyticsAnswers_DEST, "./externals/CrashlyticsAnswers/");
	}

	if (!FileExists (Fabric_DEST)) {
		DownloadFile (Fabric_URL, Fabric_DEST);
		Unzip(Fabric_DEST, "./externals/Fabric/");
	}

	var msprojPokeSettings = new XmlPokeSettings {
		Namespaces = new Dictionary<string, string> { { "ns", "http://schemas.microsoft.com/developer/msbuild/2003" } }
	};

	// Update versions in .targets file for xamarin.build.download .aar downloads
	XmlPoke("./source/Crashlytics/Xamarin.Android.Crashlytics.targets", "/ns:Project/ns:Target/ns:PropertyGroup/ns:_XbdAarVersion_crashlytics", Crashlytics_VER, msprojPokeSettings);
	XmlPoke("./source/CrashlyticsCore/Xamarin.Android.Crashlytics.Core.targets", "/ns:Project/ns:Target/ns:PropertyGroup/ns:_XbdAarVersion_crashlyticscore", CrashlyticsCore_VER, msprojPokeSettings);
	XmlPoke("./source/CrashlyticsBeta/Xamarin.Android.Crashlytics.Beta.targets", "/ns:Project/ns:Target/ns:PropertyGroup/ns:_XbdAarVersion_crashlyticsbeta", CrashlyticsBeta_VER, msprojPokeSettings);
	XmlPoke("./source/CrashlyticsAnswers/Xamarin.Android.Crashlytics.Answers.targets", "/ns:Project/ns:Target/ns:PropertyGroup/ns:_XbdAarVersion_crashlyticsanswers", CrashlyticsAnswers_VER, msprojPokeSettings);
	XmlPoke("./source/Fabric/Xamarin.Android.Fabric.targets", "/ns:Project/ns:Target/ns:PropertyGroup/ns:_XbdAarVersion_fabric", Fabric_VER, msprojPokeSettings);

	//Update PackageVersion in .csproj files
	XmlPoke("./source/Crashlytics/Crashlytics.csproj", "/Project/PropertyGroup/PackageVersion", Crashlytics_NuGet_VER);
	XmlPoke("./source/CrashlyticsCore/CrashlyticsCore.csproj", "/Project/PropertyGroup/PackageVersion", CrashlyticsCore_VER);
	XmlPoke("./source/CrashlyticsBeta/CrashlyticsBeta.csproj", "/Project/PropertyGroup/PackageVersion", CrashlyticsBeta_VER);
	XmlPoke("./source/CrashlyticsAnswers/CrashlyticsAnswers.csproj", "/Project/PropertyGroup/PackageVersion", CrashlyticsAnswers_VER);
	XmlPoke("./source/Fabric/Fabric.csproj", "/Project/PropertyGroup/PackageVersion", Fabric_VER);
});


Task ("clean")
	.Does (() => 
{	
	if (DirectoryExists("./externals/"))
		DeleteDirectory("./externals/", true);
});

Task("libs")
	.IsDependentOn("externals")
	.Does(() =>
{
	MSBuild("./source/Crashlytics.sln", c => 
		c.SetConfiguration("Release")
			.WithTarget("Restore"));

	MSBuild("./source/Crashlytics.sln", c => 
		c.SetConfiguration("Release")
			.WithTarget("Build")
			.WithProperty("DesignTimeBuild", "false"));
});

Task("nuget")
	.IsDependentOn("libs")
	.Does(() =>
{
	EnsureDirectoryExists("./output");

	MSBuild("./source/Crashlytics.sln", c => 
		c.SetConfiguration("Release")
			.WithTarget("Pack")
			.WithProperty("PackageOutputPath", "../../output")
			.WithProperty("DesignTimeBuild", "false"));
});

Task("samples")
	.IsDependentOn("nuget")
	.Does(() => 
{
	MSBuild("./samples/CrashlyticsSample/CrashlyticsSample.sln", c =>
		c.SetConfiguration("Release")
			.WithTarget("Restore"));

	MSBuild("./samples/CrashlyticsSample/CrashlyticsSample.sln", c =>
		c.SetConfiguration("Release")
		.WithProperty("DesignTimeBuild", "false"));
});

Task("component");

RunTarget (TARGET);

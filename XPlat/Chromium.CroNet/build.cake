#tool nuget:?package=XamarinComponent

#addin nuget:?package=Cake.XCode
#addin nuget:?package=Cake.Xamarin.Build
#addin nuget:?package=Cake.Xamarin
#addin nuget:?package=Cake.FileHelpers

var TARGET = Argument ("t", Argument ("target", "ci"));

string v="72.3626.96";
string CRONET_SDK_VERSION = v;

List<string> CocoaPods = new List<string>
{
	"platform :ios, '12.0'",
	"install! 'cocoapods', :integrate_targets => false",
	"target 'Xamarin' do",
	"pod 'Cronet' ",   //, '" + CRONET_SDK_VERSION + "'",
	"  use_frameworks!",
	"end",
};

Dictionary<string, string> URLS_ARTIFACT_FILES= new Dictionary<string, string>()
{
	{
		$"https://maven.google.com/org/chromium/net/cronet-api/{v}/cronet-api-{v}.aar",
		$"./externals/android/cronet-api-{v}.aar"
	},
	{
		$"https://maven.google.com/org/chromium/net/cronet-common/{v}/cronet-common-{v}.aar",
		$"./externals/android/cronet-common-{v}.aar"
	},
	{
		$"https://maven.google.com/org/chromium/net/cronet-embedded/{v}/cronet-embedded-{v}.aar",
		$"./externals/android/cronet-embedded-{v}.aar"
	},
	{
		$"https://maven.google.com/org/chromium/net/cronet-fallback/{v}/cronet-fallback-{v}.aar",
		$"./externals/android/cronet-fallback-{v}.aar"
	},
};


Task ("externals")
	.Does
	(
		() =>
		{
			Information("externals ...");
			EnsureDirectoryExists("./externals/android");

			foreach(KeyValuePair<string, string> URL_ARTIFACT_FILE in URLS_ARTIFACT_FILES)
			{
				string URL = URL_ARTIFACT_FILE.Key;
				string ARTIFACT_FILE = URL_ARTIFACT_FILE.Value;

				Information($"    downloading ...");
				Information($"                {URL}");
				Information($"    to ");
				Information($"                {ARTIFACT_FILE}");
				if ( ! string.IsNullOrEmpty(URL) && ! FileExists(ARTIFACT_FILE))
				{
					DownloadFile (URL, ARTIFACT_FILE);
				}
			}

			EnsureDirectoryExists("./externals/ios");
			if ( ! IsRunningOnWindows() )
			{
				if (CocoaPodVersion () < new System.Version (1, 0))
				{
					CocoaPods.RemoveAt (1);
				}

				FileWriteLines ("./externals/ios/Podfile", CocoaPods.ToArray ());

				CocoaPodRepoUpdate ();

				CocoaPodInstall
					(
						"./externals/ios/",
						new CocoaPodInstallSettings { NoIntegrate = true }
					);
			}

			return;
		}
	);

Task("libs")
	.IsDependentOn("externals")
	.Does
	(
		() =>
		{
			MSBuild
			(
				"./source/Xamarin.Chromium.CroNet.sln",
				c =>
				{
					c.Configuration = "Release";
					c.Restore = true;
					c.MaxCpuCount = 0;
					c.Properties.Add("DesignTimeBuild", new [] { "false" });
				}
			);

			return;
		}
	);

Task("samples")
	.IsDependentOn("nuget")
	.Does
	(
		() =>
		{
			MSBuild
			(
				"./samples/Samples.sln",
				c =>
				{
					c.Configuration = "Release";
					c.Restore = true;
					c.MaxCpuCount = 0;
					c.Properties.Add("DesignTimeBuild", new [] { "false" });
				}
			);

			return;
		}
	);

Task ("clean")
	.Does
	(
		() =>
		{
			if (DirectoryExists ("./externals/"))
			{
				DeleteDirectory ("./externals/", true);
			}
			if (DirectoryExists ("./output/"))
			{
				DeleteDirectory ("./output/", true);
			}
		}
	);

Task("nuget")
	.IsDependentOn("libs")
	.Does
	(
		() =>
		{
			EnsureDirectoryExists("./output");

			MSBuild
			(
				"./source/Xamarin.Chromium.CroNet.sln",
				configuration =>
					configuration
						.SetConfiguration("Release")
						.WithTarget("Pack")
						//.WithProperty("PackageVersion", NUGET_VERSION)
						.WithProperty("PackageOutputPath", "../../output")
			);
		}
);

Task("ci")
	.IsDependentOn("libs")
	.IsDependentOn("nuget")
	.IsDependentOn("samples")
	.Does
	(
		() => {}
	);

RunTarget (TARGET);


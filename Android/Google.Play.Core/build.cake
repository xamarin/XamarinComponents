#tool nuget:?package=XamarinComponent

#addin nuget:?package=Cake.XCode
#addin nuget:?package=Cake.Xamarin.Build
#addin nuget:?package=Cake.Xamarin
#addin nuget:?package=Cake.FileHelpers

var TARGET = Argument ("t", Argument ("target", "ci"));

string v="1.6.5";

Dictionary<string, string> URLS_ARTIFACT_FILES= new Dictionary<string, string>()
{
	{
		$"https://dl.google.com/android/maven2/com/google/android/play/core/{v}/core-{v}.aar",
		$"./externals/android/core.aar"
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
				"./source/Xamarin.Google.Play.Core.sln", 
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
				"./source/Xamarin.Google.Play.Core.sln", 
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
	.Does
	(
		() => {}
	);

RunTarget (TARGET);


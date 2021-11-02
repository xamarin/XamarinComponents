#tool nuget:?package=XamarinComponent

#addin nuget:?package=Cake.XCode
#addin nuget:?package=Cake.Xamarin.Build
#addin nuget:?package=Cake.Xamarin
#addin nuget:?package=Cake.FileHelpers

var TARGET = Argument ("t", Argument ("target", "Default"));

string ARTIFACT_VERSION="0.23.0";
string JAR_URL = "";
Dictionary<string, string> JAR_URLS_ARTIFACT_FILES= new Dictionary<string, string>()
{
	{
		$"https://repo1.maven.org/maven2/io/perfmark/perfmark-api/{ARTIFACT_VERSION}/perfmark-api-{ARTIFACT_VERSION}.jar",
		//$"https://search.maven.org/remotecontent?filepath=io/opencensus/opencensus-api/{ARTIFACT_VERSION}/opencensus-api-{ARTIFACT_VERSION}.jar",
		$"./externals/android/opencensus-api-{ARTIFACT_VERSION}.jar"
	},
};
string ARTIFACT_FILE = "";
string NUGET_VERSION=$"{ARTIFACT_VERSION}";


Task ("externals")
	.Does
	(
		() =>
		{
			Information("externals ...");
			EnsureDirectoryExists("./externals/android");

			foreach(KeyValuePair<string, string> JAR_URL_ARTIFACT_FILE in JAR_URLS_ARTIFACT_FILES)
			{
				string JAR_URL = JAR_URL_ARTIFACT_FILE.Key;
				string ARTIFACT_FILE = JAR_URL_ARTIFACT_FILE.Value;

				Information($"    downloading ...");
				Information($"                {JAR_URL}");
				Information($"    to ");
				Information($"                {ARTIFACT_FILE}");
				if ( ! string.IsNullOrEmpty(JAR_URL) && ! FileExists(ARTIFACT_FILE))
				{
					DownloadFile (JAR_URL, ARTIFACT_FILE);
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
				"./source/Xamarin.Io.PerfMark.sln", 
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
				"./source/Xamarin.Io.PerfMark.sln",
				configuration =>
					configuration
						.SetConfiguration("Release")
						.WithTarget("Pack")
						.WithProperty("PackageVersion", NUGET_VERSION)
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


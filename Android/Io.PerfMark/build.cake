#tool nuget:?package=XamarinComponent

#addin nuget:?package=Cake.XCode
#addin nuget:?package=Cake.Xamarin.Build
#addin nuget:?package=Cake.Xamarin
#addin nuget:?package=Cake.FileHelpers

var TARGET = Argument ("t", Argument ("target", "Default"));

string ARTIFACT_VERSION="0.17.0";
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


BuildSpec buildSpec = new BuildSpec ()
{
	Libs = new ISolutionBuilder []
	{
		new DefaultSolutionBuilder
		{
			SolutionPath = "./source/Xamarin.Io.PerfMark.sln",
			OutputFiles = new []
			{
				new OutputFileCopy
				{
					FromFile = "./source/Xamarin.Io.PerfMark.PerfMarkApi/bin/Release/monoandroid81/Xamarin.Io.PerfMark.PerfMarkApi.dll"
				},
			}
		}
	},

	Samples = new ISolutionBuilder []
	{
		new DefaultSolutionBuilder
		{
			SolutionPath = "./samples/Xamarin.Io.PerfMark.sln"
		},
	},
};

Task ("externals")
	.IsDependentOn ("externals-base")
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

Task("nuget")
	.IsDependentOn("libs")
	.Does
	(
		() =>
		{
			EnsureDirectoryExists("./output");

			MSBuild
			(
				"./source/Xamarin.Io.PerfMark.PerfMarkApi/Xamarin.Io.PerfMark.PerfMarkApi.csproj",
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
	);

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);


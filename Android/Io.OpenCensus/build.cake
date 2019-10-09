#tool nuget:?package=XamarinComponent

#addin nuget:?package=Cake.XCode
#addin nuget:?package=Cake.Xamarin.Build
#addin nuget:?package=Cake.Xamarin
#addin nuget:?package=Cake.FileHelpers

var TARGET = Argument ("t", Argument ("target", "Default"));

string ARTIFACT_VERSION="0.21.0";
string JAR_URL = "";
Dictionary<string, string> JAR_URLS_ARTIFACT_FILES= new Dictionary<string, string>()
{
	{
		$"https://repo1.maven.org/maven2/io/opencensus/opencensus-api/{ARTIFACT_VERSION}/opencensus-api-{ARTIFACT_VERSION}.jar",
		//$"https://search.maven.org/remotecontent?filepath=io/opencensus/opencensus-api/{ARTIFACT_VERSION}/opencensus-api-{ARTIFACT_VERSION}.jar",
		$"./externals/android/opencensus-api-{ARTIFACT_VERSION}.jar"
	},
	{
		$"https://repo1.maven.org/maven2/io/opencensus/opencensus-contrib-grpc-metrics/{ARTIFACT_VERSION}/opencensus-contrib-grpc-metrics-{ARTIFACT_VERSION}.jar",
		//$"https://search.maven.org/remotecontent?filepath=io/opencensus/opencensus-contrib-grpc-metrics/{ARTIFACT_VERSION}/opencensus-contrib-grpc-metrics-{ARTIFACT_VERSION}.jar",
		$"./externals/android/opencensus-contrib-grpc-metrics-{ARTIFACT_VERSION}.jar"
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
			SolutionPath = "./source/Xamarin.Io.OpenCensus.sln",
			OutputFiles = new []
			{
				new OutputFileCopy
				{
					FromFile = "./source/Xamarin.Io.OpenCensus.OpenCensusApi/bin/Release/monoandroid81/Xamarin.Io.OpenCensus.OpenCensusApi.dll"
				},
				new OutputFileCopy
				{
					FromFile = $"source/Xamarin.Io.OpenCensus.OpenCensusApi/bin/Release/Xamarin.Io.OpenCensus.OpenCensusApi.{ARTIFACT_VERSION}.nupkg"
				},

				new OutputFileCopy
				{
					FromFile = "./source/Xamarin.Io.OpenCensus.OpenCensusContribGrpcMetrics/bin/Release/monoandroid81/Xamarin.Io.OpenCensus.OpenCensusContribGrpcMetrics.dll"
				},
				new OutputFileCopy
				{
					FromFile = $"source/Xamarin.Io.OpenCensus.OpenCensusContribGrpcMetrics/bin/Release/Xamarin.Io.OpenCensus.OpenCensusContribGrpcMetrics.{ARTIFACT_VERSION}.nupkg"
				},
			}
		}
	},

	Samples = new ISolutionBuilder []
	{
		new DefaultSolutionBuilder
		{
			SolutionPath = "./samples/Xamarin.Io.OpenCensus.sln"
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
				"./source/Xamarin.Io.OpenCensus.OpenCensusApi/Xamarin.Io.OpenCensus.OpenCensusApi.csproj",
				configuration =>
					configuration
						.SetConfiguration("Release")
						.WithTarget("Pack")
						.WithProperty("PackageVersion", NUGET_VERSION)
						.WithProperty("PackageOutputPath", "../../output")
			);
			MSBuild
			(
				"./source/Xamarin.Io.OpenCensus.OpenCensusContribGrpcMetrics/Xamarin.Io.OpenCensus.OpenCensusContribGrpcMetrics.csproj",
				configuration =>
					configuration
						.SetConfiguration("Release")
						.WithTarget("Pack")
						.WithProperty("PackageVersion", NUGET_VERSION)
						.WithProperty("PackageOutputPath", "../../output")
			);

			return;
		}
);

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);


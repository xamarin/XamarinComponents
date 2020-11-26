#tool nuget:?package=XamarinComponent

#addin nuget:?package=Cake.XCode
#addin nuget:?package=Cake.Xamarin
#addin nuget:?package=Cake.FileHelpers

var TARGET = Argument ("t", Argument ("target", "Default"));

string ARTIFACT_VERSION="1.28.0";
string JAR_URL = "";
Dictionary<string, string> JAR_URLS_ARTIFACT_FILES= new Dictionary<string, string>()
{
	{
		$"https://repo1.maven.org/maven2/io/grpc/grpc-android/{ARTIFACT_VERSION}/grpc-android-{ARTIFACT_VERSION}.aar",
		$"./externals/android/grpc-android-{ARTIFACT_VERSION}.aar"
	},
	{
		$"https://repo1.maven.org/maven2/io/grpc/grpc-api/{ARTIFACT_VERSION}/grpc-api-{ARTIFACT_VERSION}.jar",
		$"./externals/android/grpc-api-{ARTIFACT_VERSION}.jar"
	},
	{
		$"https://repo1.maven.org/maven2/io/grpc/grpc-stub/{ARTIFACT_VERSION}/grpc-stub-{ARTIFACT_VERSION}.jar",
		$"./externals/android/grpc-stub-{ARTIFACT_VERSION}.jar"
	},
	{
		$"https://repo1.maven.org/maven2/io/grpc/grpc-core/{ARTIFACT_VERSION}/grpc-core-{ARTIFACT_VERSION}.jar",
		$"./externals/android/grpc-core-{ARTIFACT_VERSION}.jar"
	},
	{
		$"https://repo1.maven.org/maven2/io/grpc/grpc-okhttp/{ARTIFACT_VERSION}/grpc-okhttp-{ARTIFACT_VERSION}.jar",
		$"./externals/android/grpc-okhttp-{ARTIFACT_VERSION}.jar"
	},
	{
		$"https://repo1.maven.org/maven2/io/grpc/grpc-protobuf-lite/{ARTIFACT_VERSION}/grpc-protobuf-lite-{ARTIFACT_VERSION}.jar",
		$"./externals/android/grpc-protobuf-lite-{ARTIFACT_VERSION}.jar"
	},
	{
		$"https://repo1.maven.org/maven2/io/grpc/grpc-context/{ARTIFACT_VERSION}/grpc-context-{ARTIFACT_VERSION}.jar",
		$"./externals/android/grpc-context-{ARTIFACT_VERSION}.jar"
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

Task ("libs")
	.IsDependentOn ("externals")
	.Does
	(
		() =>
		{
			MSBuild
			(
				"./source/Xamarin.Grpc.sln", 
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
			if (DirectoryExists ("./output/"))
			{
				DeleteDirectory ("./output/", true);
			}
			if (DirectoryExists ("./externals/"))
			{
				DeleteDirectory ("./externals/", true);
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
				"./source/Xamarin.Grpc.sln",
				c => 
				{
					c.Configuration = "Release";
					c.MaxCpuCount = 0;
					c.Targets.Clear();
					c.Targets.Add("Pack");
					c.Properties.Add("PackageOutputPath", new [] { MakeAbsolute(new FilePath("./output")).FullPath });
					c.Properties.Add("PackageRequireLicenseAcceptance", new [] { "true" });
					c.Properties.Add("DesignTimeBuild", new [] { "false" });
					c.Properties.Add("NoBuild", new [] { "true" });
				}			
			);

			return;
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


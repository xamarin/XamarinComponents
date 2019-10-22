#tool nuget:?package=XamarinComponent

#addin nuget:?package=Cake.XCode
#addin nuget:?package=Cake.Xamarin
#addin nuget:?package=Cake.FileHelpers

var TARGET = Argument ("t", Argument ("target", "Default"));

string ARTIFACT_VERSION="1.24.0";
string JAR_URL = "";
Dictionary<string, string> JAR_URLS_ARTIFACT_FILES= new Dictionary<string, string>()
{
	{
		$"http://central.maven.org/maven2/io/grpc/grpc-android/{ARTIFACT_VERSION}/grpc-android-{ARTIFACT_VERSION}.aar",
		$"./externals/android/grpc-android-{ARTIFACT_VERSION}.aar"
	},
	{
		$"http://central.maven.org/maven2/io/grpc/grpc-api/{ARTIFACT_VERSION}/grpc-api-{ARTIFACT_VERSION}.jar",
		$"./externals/android/grpc-api-{ARTIFACT_VERSION}.jar"
	},
	{
		$"http://central.maven.org/maven2/io/grpc/grpc-stub/{ARTIFACT_VERSION}/grpc-stub-{ARTIFACT_VERSION}.jar",
		$"./externals/android/grpc-stub-{ARTIFACT_VERSION}.jar"
	},
	{
		$"http://central.maven.org/maven2/io/grpc/grpc-core/{ARTIFACT_VERSION}/grpc-core-{ARTIFACT_VERSION}.jar",
		$"./externals/android/grpc-core-{ARTIFACT_VERSION}.jar"
	},
	{
		$"http://central.maven.org/maven2/io/grpc/grpc-okhttp/{ARTIFACT_VERSION}/grpc-okhttp-{ARTIFACT_VERSION}.jar",
		$"./externals/android/grpc-okhttp-{ARTIFACT_VERSION}.jar"
	},
	{
		$"http://central.maven.org/maven2/io/grpc/grpc-protobuf-lite/{ARTIFACT_VERSION}/grpc-protobuf-lite-{ARTIFACT_VERSION}.jar",
		$"./externals/android/grpc-protobuf-lite-{ARTIFACT_VERSION}.jar"
	},
	{
		$"http://central.maven.org/maven2/io/grpc/grpc-context/{ARTIFACT_VERSION}/grpc-context-{ARTIFACT_VERSION}.jar",
		$"./externals/android/grpc-context-{ARTIFACT_VERSION}.jar"
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
			SolutionPath = "./source/Xamarin.Grpc.sln",
			OutputFiles = new []
			{
				new OutputFileCopy
				{
					FromFile = "./source/Xamarin.Grpc.Core.Bindings.XamarinAndroid/bin/Release/monoandroid81/Xamarin.Grpc.Core.dll"
				},
				new OutputFileCopy
				{
					FromFile = $"source/Xamarin.Grpc.Core.Bindings.XamarinAndroid/bin/Release/Xamarin.Grpc.Core.{NUGET_VERSION}.nupkg"
				},

				new OutputFileCopy
				{
					FromFile = "./source/Xamarin.Grpc.Api.Bindings.XamarinAndroid/bin/Release/monoandroid81/Xamarin.Grpc.Api.dll"
				},
				new OutputFileCopy
				{
					FromFile = $"source/Xamarin.Grpc.Api.Bindings.XamarinAndroid/bin/Release/Xamarin.Grpc.Api.{NUGET_VERSION}.nupkg"
				},

				new OutputFileCopy
				{
					FromFile = "./source/Xamarin.Grpc.Android.Bindings.XamarinAndroid/bin/Release/monoandroid81/Xamarin.Grpc.Android.dll"
				},
				new OutputFileCopy
				{
					FromFile = $"source/Xamarin.Grpc.Android.Bindings.XamarinAndroid/bin/Release/Xamarin.Grpc.Android.{NUGET_VERSION}.nupkg"
				},

				new OutputFileCopy
				{
					FromFile = "./source/Xamarin.Grpc.Stub.Bindings.XamarinAndroid/bin/Release/monoandroid81/Xamarin.Grpc.Stub.dll"
				},
				new OutputFileCopy
				{
					FromFile = $"source/Xamarin.Grpc.Stub.Bindings.XamarinAndroid/bin/Release/Xamarin.Grpc.Stub.{NUGET_VERSION}.nupkg"
				},

				new OutputFileCopy
				{
					FromFile = "./source/Xamarin.Grpc.Protobuf.Lite.Bindings.XamarinAndroid/bin/Release/monoandroid81/Xamarin.Grpc.Protobuf.Lite.dll"
				},
				new OutputFileCopy
				{
					FromFile = $"source/Xamarin.Grpc.Protobuf.Lite.Bindings.XamarinAndroid/bin/Release/Xamarin.Grpc.Protobuf.Lite.{NUGET_VERSION}.nupkg"
				},

				new OutputFileCopy
				{
					FromFile = "./source/Xamarin.Grpc.OkHttp.Bindings.XamarinAndroid/bin/Release/monoandroid81/Xamarin.Grpc.OkHttp.dll"
				},
				new OutputFileCopy
				{
					FromFile = $"source/Xamarin.Grpc.OkHttp.Bindings.XamarinAndroid/bin/Release/Xamarin.Grpc.OkHttp.{NUGET_VERSION}.nupkg"
				},

				new OutputFileCopy
				{
					FromFile = "./source/Xamarin.Grpc.Context.Bindings.XamarinAndroid/bin/Release/monoandroid81/Xamarin.Grpc.Context.dll"
				},
				new OutputFileCopy
				{
					FromFile = $"source/Xamarin.Grpc.Context.Bindings.XamarinAndroid/bin/Release/Xamarin.Grpc.Context.{NUGET_VERSION}.nupkg"
				},
			}
		}
	},

	Samples = new ISolutionBuilder []
	{
		new DefaultSolutionBuilder
		{
			SolutionPath = "./samples/Xamarin.Grpc.sln"
		},
	},
};

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


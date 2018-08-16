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
#tool nuget:?package=XamarinComponent

#addin nuget:?package=Cake.XCode
#addin nuget:?package=Cake.Xamarin.Build
#addin nuget:?package=Cake.Xamarin
#addin nuget:?package=Cake.FileHelpers

var TARGET = Argument ("t", Argument ("target", "Default"));

string ARTIFACT_VERSION="1.14.0";
string JAR_URL = "";
Dictionary<string, string> JAR_URLS_ARTIFACT_FILES= new Dictionary<string, string>()
{	
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
					FromFile = $"source/Xamarin.Grpc.Core.Bindings.XamarinAndroid/bin/Release/Xamarin.Grpc.Core.{ARTIFACT_VERSION}.nupkg" 
				},
				new OutputFileCopy 
				{ 
					FromFile = "./source/Xamarin.Grpc.Stub.Bindings.XamarinAndroid/bin/Release/monoandroid81/Xamarin.Grpc.Stub.dll" 
				},
				new OutputFileCopy 
				{ 
					FromFile = $"source/Xamarin.Grpc.Stub.Bindings.XamarinAndroid/bin/Release/Xamarin.Grpc.Stub.{ARTIFACT_VERSION}.nupkg" 
				},

				new OutputFileCopy 
				{ 
					FromFile = "./source/Xamarin.Grpc.Protobuf.Lite.Bindings.XamarinAndroid/bin/Release/monoandroid81/Xamarin.Grpc.Protobuf.Lite.dll" 
				},
				new OutputFileCopy 
				{ 
					FromFile = $"source/Xamarin.Grpc.Protobuf.Lite.Bindings.XamarinAndroid/bin/Release/Xamarin.Grpc.Protobuf.Lite.{ARTIFACT_VERSION}.nupkg" 
				},
				new OutputFileCopy 
				{ 
					FromFile = "./source/Xamarin.Grpc.OkHttp.Bindings.XamarinAndroid/bin/Release/monoandroid81/Xamarin.Grpc.OkHttp.dll" 
				},
				new OutputFileCopy 
				{ 
					FromFile = $"source/Xamarin.Grpc.OkHttp.Bindings.XamarinAndroid/bin/Release/Xamarin.Grpc.OkHttp.{ARTIFACT_VERSION}.nupkg" 
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
				"./source/Xamarin.Grpc.Core.Bindings.XamarinAndroid/Xamarin.Grpc.Core.Bindings.XamarinAndroid.csproj", 
				configuration => 
					configuration
						.SetConfiguration("Release")
						.WithTarget("Pack")
						.WithProperty("PackageVersion", NUGET_VERSION)
						.WithProperty("PackageOutputPath", "../../output")
			);
			MSBuild
			(
				"./source/Xamarin.Grpc.OkHttp.Bindings.XamarinAndroid/Xamarin.Grpc.OkHttp.Bindings.XamarinAndroid.csproj", 
				configuration => 
					configuration
						.SetConfiguration("Release")
						.WithTarget("Pack")
						.WithProperty("PackageVersion", NUGET_VERSION)
						.WithProperty("PackageOutputPath", "../../output")
			);
			MSBuild
			(
				"./source/Xamarin.Grpc.Stub.Bindings.XamarinAndroid/Xamarin.Grpc.Stub.Bindings.XamarinAndroid.csproj", 
				configuration => 
					configuration
						.SetConfiguration("Release")
						.WithTarget("Pack")
						.WithProperty("PackageVersion", NUGET_VERSION)
						.WithProperty("PackageOutputPath", "../../output")
			);
			MSBuild
			(
				"./source/Xamarin.Grpc.Protobuf.Lite.Bindings.XamarinAndroid/Xamarin.Grpc.Protobuf.Lite.Bindings.XamarinAndroid.csproj", 
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


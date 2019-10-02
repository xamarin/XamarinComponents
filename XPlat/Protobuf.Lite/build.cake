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

string ARTIFACT_VERSION="3.0.1";
string JAR_URL=$"http://central.maven.org/maven2/com/google/protobuf/protobuf-lite/{ARTIFACT_VERSION}/protobuf-lite-{ARTIFACT_VERSION}.jar";
string ARTIFACT_FILE = $"./externals/android/protobuf-lite-{ARTIFACT_VERSION}.jar";
string NUGET_VERSION=$"{ARTIFACT_VERSION}";


BuildSpec buildSpec = new BuildSpec () 
{
	Libs = new ISolutionBuilder [] 
	{
		new DefaultSolutionBuilder 
		{
			SolutionPath = "./source/Xamarin.Protobuf.Lite.sln",
			OutputFiles = new [] 
			{ 
				new OutputFileCopy 
				{ 
					FromFile = "./source/Xamarin.Protobuf.Lite.Bindings.XamarinAndroid/bin/Release/monoandroid81/Xamarin.Protobuf.Lite.dll" 
				},
				new OutputFileCopy 
				{ 
					FromFile = $"source/Xamarin.Protobuf.Lite.Bindings.XamarinAndroid/bin/Release/Xamarin.Protobuf.Lite.{NUGET_VERSION}.nupkg" 
				},
			}
		}
	},

	Samples = new ISolutionBuilder [] 
	{
		new DefaultSolutionBuilder 
		{ 
			SolutionPath = "./samples/Xamarin.Protobuf.Lite.Samples.sln" 
		},	
	},
};

Task ("externals")
	.IsDependentOn ("externals-base")
	.WithCriteria (!FileExists (ARTIFACT_FILE))
	.Does 
	(
		() => 
		{
			Information("externals ...");
			EnsureDirectoryExists("./externals/android");
			Information("    downloading ...");

			if ( ! string.IsNullOrEmpty(JAR_URL) && ! FileExists(ARTIFACT_FILE))
			{
				DownloadFile (JAR_URL, ARTIFACT_FILE);
			}
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
				"./source/Xamarin.Protobuf.Lite.Bindings.XamarinAndroid/Xamarin.Protobuf.Lite.Bindings.XamarinAndroid.csproj", 
				configuration => 
					configuration
						.SetConfiguration("Release")
						.WithTarget("Pack")
						.WithProperty("PackageVersion", NUGET_VERSION)
						.WithProperty("PackageOutputPath", "../../output")
			);

		}
);

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

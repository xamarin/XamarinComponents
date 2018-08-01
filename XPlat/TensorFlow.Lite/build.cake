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

string AAR_URL="https://bintray.com/google/tensorflow/download_file?file_path=org%2Ftensorflow%2Ftensorflow-lite%2F1.9.0%2Ftensorflow-lite-1.9.0.aar";

BuildSpec buildSpec = new BuildSpec () 
{
	Libs = new ISolutionBuilder [] 
	{
		new DefaultSolutionBuilder 
		{
			SolutionPath = "./source/Xamarin.TensorFlow.Lite.sln",
			OutputFiles = new [] 
			{ 
				new OutputFileCopy 
				{ 
					FromFile = "./source/Xamarin.TensorFlow.Lite.Bindings.XamarinAndroid/bin/Release/monoandroid81/Xamarin.TensorFlow.Lite.dll" 
				},
				new OutputFileCopy 
				{ 
					FromFile = "source/Xamarin.TensorFlow.Lite.Bindings.XamarinAndroid/bin/Release/Xamarin.TensorFlow.Lite.1.9.0.nupkg" 
				},
				// new OutputFileCopy 
				// { 
				// 	FromFile = "./source/Xamarin.TensorFlow.Lite.Bindings.XamarinIOS/bin/Release/Xamarin.TensorFlow.Lite.dll" 
				// },
			}
		}
	},

	Samples = new ISolutionBuilder [] 
	{
		new DefaultSolutionBuilder 
		{ 
			SolutionPath = "./samples/Xamarin.TensorFlow.Lite.Samples.sln" 
		},	
	},

	Components = new []
	{
		new Component 
		{ 
			ManifestDirectory = "./component" 
		},
	},

	NuGets = new [] 
	{
		new NuGetInfo 
		{ 
			NuSpec = "./nuget/HolisticWare.Xamarin.TensorFlow.Lite.nuspec" 
		},
	},
};

Task ("externals")
	.IsDependentOn ("externals-base")
	// .WithCriteria (!FileExists ("./externals/Xamarin.TensorFlow.Lite.aar"))
	.Does 
	(
		() => 
		{
			Information("externals ...");
			EnsureDirectoryExists("./externals/android");
			Information("    downloading ...");

			string file = "./externals/android/tensorflow-lite-1.9.0.aar";
			if ( ! string.IsNullOrEmpty(AAR_URL) && ! FileExists(file))
			{
				DownloadFile (AAR_URL, file);
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

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

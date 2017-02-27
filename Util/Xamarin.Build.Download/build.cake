#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var buildSpec = new BuildSpec {
	Libs = new [] { 
		new DefaultSolutionBuilder {
			SolutionPath = "source/Xamarin.Build.Download.sln",
			BuildsOn = BuildPlatforms.Windows | BuildPlatforms.Mac,
			OutputFiles = new [] { 
				new OutputFileCopy { FromFile = "./source/Xamarin.Build.Download/bin/Release/Xamarin.Build.Download.dll" },
				new OutputFileCopy { FromFile = "./source/Xamarin.Build.Download/bin/Release/Xamarin.Build.Download.targets" },
				new OutputFileCopy { FromFile = "./source/Xamarin.Build.Download/bin/Release/Xamarin.Build.Download.props" },
				new OutputFileCopy { FromFile = "./source/Xamarin.Build.Download/bin/Release/System.Net.Http.Formatting.dll" },
				new OutputFileCopy { FromFile = "./source/Xamarin.Build.Download/bin/Release/Newtonsoft.Json.dll" },
				new OutputFileCopy { FromFile = "./source/Xamarin.Build.Download/bin/Release/Mono.Cecil.dll" },
			},

		}
	},
	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Build.Download.nuspec" },
	}
};

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

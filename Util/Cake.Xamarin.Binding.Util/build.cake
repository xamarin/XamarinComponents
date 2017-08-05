#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var buildSpec = new BuildSpec {
	Libs = new [] { 
		new DefaultSolutionBuilder {
			SolutionPath = "source/Cake.Xamarin.Binding.Util.sln",
			BuildsOn = BuildPlatforms.Windows | BuildPlatforms.Mac,
			OutputFiles = new [] { 
				new OutputFileCopy { FromFile = "./source/Cake.Xamarin.Binding.Util/bin/Release/Cake.Xamarin.Binding.Util.dll" },
				new OutputFileCopy { FromFile = "./source/Cake.Xamarin.Binding.Util/bin/Release/Cake.Xamarin.Binding.Util.xml" },
				new OutputFileCopy { FromFile = "./source/Cake.Xamarin.Binding.Util/bin/Release/Mono.Cecil.dll" },
				new OutputFileCopy { FromFile = "./source/Cake.Xamarin.Binding.Util/bin/Release/Mono.Cecil.Mdb.dll" },
				new OutputFileCopy { FromFile = "./source/Cake.Xamarin.Binding.Util/bin/Release/Mono.Cecil.Pdb.dll" },
				new OutputFileCopy { FromFile = "./source/Cake.Xamarin.Binding.Util/bin/Release/Mono.Cecil.Rocks.dll" },
			},
		}
	},
	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Cake.Xamarin.Binding.Util.nuspec" },
	}
};

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

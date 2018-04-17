#load "../../common.cake"

#tool "nuget:?package=ILRepack"

var TARGET = Argument ("t", Argument ("target", "Default"));

var buildSpec = new BuildSpec {
	Libs = new [] { 
		new DefaultSolutionBuilder {
			SolutionPath = "source/Cake.Xamarin.Binding.Util.sln",
			BuildsOn = BuildPlatforms.Windows | BuildPlatforms.Mac,
			OutputFiles = new [] { 
				new OutputFileCopy { FromFile = "./source/Cake.Xamarin.Binding.Util/bin/Release/netstandard2.0/Cake.Xamarin.Binding.Util.dll" },
				new OutputFileCopy { FromFile = "./source/Cake.Xamarin.Binding.Util/bin/Release/netstandard2.0/Cake.Xamarin.Binding.Util.xml" }
			},
		}
	},
	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Cake.Xamarin.Binding.Util.nuspec" },
	}
};

Task ("merge").IsDependentOn("libs").Does (() => {
	// CopyFile ("./output/Cake.Xamarin.Binding.Util.dll", "./output/Cake.Xamarin.Binding.Util.Temp.dll");

	// ILRepack ("./output/Cake.Xamarin.Binding.Util.dll", 
	// 	"./output/Cake.Xamarin.Binding.Util.Temp.dll",
	// 	new FilePath[] { "./output/Mono.Cecil.dll", "./output/Mono.Cecil.Mdb.dll", "./output/Mono.Cecil.Pdb.dll", "./output/Mono.Cecil.Rocks.dll" },
	// 	new ILRepackSettings {
	// 		Libs = new List<FilePath> {
	// 			"./output/",
	// 			"./source/Cake.Xamarin.Binding.Util/bin/Release/",
	// 		}
	// 	});

	// CopyFile ("./source/Cake.Xamarin.Binding.Util/bin/Release/Cake.Xamarin.Binding.Util.xml", "./output/Cake.Xamarin.Binding.Util.xml");
});

Task("nuget").IsDependentOn ("merge").IsDependentOn("nuget-base");

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

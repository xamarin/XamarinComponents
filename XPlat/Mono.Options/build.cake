
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var FileUrl = "https://raw.githubusercontent.com/mono/mono/{0}/mcs/class/Mono.Options/Mono.Options/Options.cs";
var FileRevision = "7e2571ed334e9cee3f0d3bafeef02852310f4d3b";
var OptionsUrl = string.Format (FileUrl, FileRevision);

var buildSpec = new BuildSpec {

	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			PreBuildAction = () => {
				// restore netstandard
				StartProcess("dotnet", new ProcessSettings {
					Arguments = "restore ./source/Mono.Options.NetStandard.sln"
				});
			},
			PostBuildAction = () => {
				// restore netstandard
				StartProcess("dotnet", new ProcessSettings {
					Arguments = "build -c Release ./source/Mono.Options.NetStandard.sln"
				});
			},
			SolutionPath = "./source/Mono.Options.sln",
			Configuration = "Release",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/Mono.Options/bin/Release/Mono.Options.dll",
					ToDirectory = "./output/net4"
				},
				new OutputFileCopy {
					FromFile = "./source/Mono.Options.Portable/bin/Release/Mono.Options.dll",
					ToDirectory = "./output/pcl"
				},
				new OutputFileCopy {
					FromFile = "./source/Mono.Options.NetStandard/bin/Release/Mono.Options.dll",
					ToDirectory = "./output/netstandard"
				},
			}
		},
	},

	Samples = new ISolutionBuilder [] {
		new DefaultSolutionBuilder { SolutionPath = "./samples/OptionsSample.sln" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Mono.Options.nuspec"},
	},

	Components = new [] {
		new Component { ManifestDirectory = "./component/" },
	},
};

Task ("externals").IsDependentOn ("externals-base").Does (() => 
{
	if (!DirectoryExists ("./externals/")) {
		CreateDirectory ("./externals/");
	}
	
	if (!FileExists ("./externals/Options.cs")) {
		DownloadFile (OptionsUrl, "./externals/Options.cs");
	}
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);


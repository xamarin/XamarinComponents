
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var FileUrl = "https://raw.githubusercontent.com/mono/mono/{0}/mcs/class/corlib/Mono/DataConverter.cs";
var FileRevision = "7e2571ed334e9cee3f0d3bafeef02852310f4d3b";
var DataConverterUrl = string.Format (FileUrl, FileRevision);

var buildSpec = new BuildSpec {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			PreBuildAction = () => {
				// restore netstandard
				StartProcess("dotnet", new ProcessSettings {
					Arguments = "restore ./source/Mono.DataConverter.NetStandard.sln"
				});
			},
			PostBuildAction = () => {
				// restore netstandard
				StartProcess("dotnet", new ProcessSettings {
					Arguments = "build -c Release ./source/Mono.DataConverter.NetStandard.sln"
				});
			},
			SolutionPath = "./source/Mono.DataConverter.sln",
			Configuration = "Release",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/Mono.DataConverter/bin/Release/Mono.DataConverter.dll",
					ToDirectory = "./output/pcl"
				},
				new OutputFileCopy {
					FromFile = "./source/Mono.DataConverter/bin/Release/Mono.DataConverter.xml",
					ToDirectory = "./output/pcl"
				},
				new OutputFileCopy {
					FromFile = "./source/Mono.DataConverter.NetStandard/bin/Release/Mono.DataConverter.dll",
					ToDirectory = "./output/netstandard"
				},
				new OutputFileCopy {
					FromFile = "./source/Mono.DataConverter.NetStandard/bin/Release/Mono.DataConverter.xml",
					ToDirectory = "./output/netstandard"
				},
			}
		},
	},

	Samples = new ISolutionBuilder [] {
		new DefaultSolutionBuilder { SolutionPath = "./samples/DataConverterSample.sln" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Mono.DataConverter.nuspec"},
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
	
	if (!FileExists ("./externals/DataConverter.cs")) {
		DownloadFile (DataConverterUrl, "./externals/DataConverter.cs");
	}
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);



#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var FileUrl = "https://raw.githubusercontent.com/mono/mono/{0}/mcs/tools/csharp/getline.cs";
var FileRevision = "7e2571ed334e9cee3f0d3bafeef02852310f4d3b";
var TerminalUrl = string.Format (FileUrl, FileRevision);

var buildSpec = new BuildSpec {

	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/Mono.Terminal.sln",
			Configuration = "Release",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/Mono.Terminal/bin/Release/Mono.Terminal.dll",
					ToDirectory = "./output/net4"
				},
				new OutputFileCopy {
					FromFile = "./source/Mono.Terminal/bin/Release/Mono.Terminal.xml",
					ToDirectory = "./output/net4"
				},
			}
		},
	},

	Samples = new ISolutionBuilder [] {
		new DefaultSolutionBuilder { SolutionPath = "./samples/TerminalSample.sln" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Mono.Terminal.nuspec"},
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
	
	if (!FileExists ("./externals/getline.cs")) {
		DownloadFile (TerminalUrl, "./externals/getline.cs");
	}
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);



#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./BlurBehind.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/bin/Release/BlurBehind.dll",
				}
			}
		}
	},

	Samples = new ISolutionBuilder [] {
		new DefaultSolutionBuilder { SolutionPath = "./samples/BlurBehindSample.sln" },
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/BlurBehind.nuspec" },
	},

	Components = new [] {
		new Component {ManifestDirectory = "./component"},
	},
};

Task ("externals").IsDependentOn ("externals-base").Does (() =>
{
	if (!DirectoryExists ("./externals"))
		CreateDirectory ("./externals");

	DownloadFile ("https://github.com/faradaj/BlurBehind/releases/download/v1.1/blur-behind-1.1.aar", "./externals/blur-behind.aar");
});

Task ("clean").IsDependentOn ("clean-base").Does (() => {
	if (DirectoryExists ("./externals"))
		DeleteDirectory ("./externals", true);
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

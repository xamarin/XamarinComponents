
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var buildSpec = new BuildSpec {
	Libs = new [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/Xamarin.Dropbox.Api.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/Xamarin.Dropbox.Api.Core/bin/Release/netstandard2.0/netstandard2.0/Xamarin.Dropbox.Api.Core.dll",
				},
				new OutputFileCopy {
					FromFile = "./source/Xamarin.Dropbox.Api.Android/bin/Release/Xamarin.Dropbox.Api.Android.dll",
				},
				new OutputFileCopy {
					FromFile = "./source/Xamarin.Dropbox.Api.iOS/bin/Release/Xamarin.Dropbox.Api.iOS.dll",
				}
			}
		}
	},

	Samples = new [] {
		new IOSSolutionBuilder { SolutionPath = "./samples/DropboxV2ApiSampleiOS/DropboxV2ApiSampleiOS.sln" },	
		new DefaultSolutionBuilder { SolutionPath = "./samples/DropboxV2ApiSampleDroid/DropboxV2ApiSampleDroid.sln" }
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Dropbox.Api.Core.nuspec" },
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Dropbox.Api.iOS.nuspec" },
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Dropbox.Api.Android.nuspec" },
	},

	Components = new [] {
		new Component { ManifestDirectory = "./component" }
	}
};

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);
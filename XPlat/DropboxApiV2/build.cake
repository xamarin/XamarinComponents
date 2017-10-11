
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var buildSpec = new BuildSpec {
	Libs = new [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/Xamarin.Dropbox.Api.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/Xamarin.Dropbox.Api.Core/bin/Release/netstandard2.0/Xamarin.Dropbox.Api.Core.dll",
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
		new IOSSolutionBuilder { SolutionPath = "./samples/LoginScreen.iOS.Sample/LoginScreen.iOS.Sample.sln" },	
		new DefaultSolutionBuilder { SolutionPath = "./samples/LoginScreen.Android.Sample/LoginScreen.Android.Sample.sln" }
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.LoginScreen.nuspec" },
	},

	Components = new [] {
		new Component { ManifestDirectory = "./component" }
	}
};

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);
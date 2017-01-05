
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var buildSpec = new BuildSpec {
	Libs = new [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/LoginScreen.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/LoginScreen.Android/bin/Release/Xamarin.Controls.LoginScreen.Android.dll",
				},
				new OutputFileCopy {
					FromFile = "./source/LoginScreen.iOS/bin/unified/Release/Xamarin.Controls.LoginScreen.iOS.dll",
				}
			}
		}
	},

	Samples = new [] {
		new IOSSolutionBuilder { SolutionPath = "./samples/LoginScreen.iOS.Sample/LoginScreen.iOS.Sample.sln" },	
		new DefaultSolutionBuilder { SolutionPath = "./samples/LoginScreen.Android.Sample/LoginScreen.Android.Sample.sln" }
	},

	Components = new [] {
		new Component { ManifestDirectory = "./component" }
	}
};

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./Xamarin.InAppBilling.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/Xamarin.InAppBilling/bin/Release/Xamarin.InAppBilling.dll",
				},
				new OutputFileCopy {
					FromFile = "./source/Xamarin.InAppBilling/bin/Release/Xamarin.InAppBilling.xml",
					ToDirectory = "output/mcsdocs"
				}
			}
		}
	},

	Samples = new ISolutionBuilder [] {
		new DefaultSolutionBuilder { SolutionPath = "./samples/InAppBillingTest/InAppBillingTest.sln" },
	},

	Components = new [] {
		new Component {ManifestDirectory = "./component"},
	},
};

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

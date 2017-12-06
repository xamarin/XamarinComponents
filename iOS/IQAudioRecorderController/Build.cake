#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));


BuildSpec buildSpec = new BuildSpec () 
{
	Libs = new ISolutionBuilder [] 
	{
		new DefaultSolutionBuilder 
		{
			SolutionPath = "./source/IQAudioRecorderController.sln",
			Configuration = "Release",
			BuildsOn = BuildPlatforms.Mac,
			OutputFiles = new [] { 
				new OutputFileCopy { 
					FromFile = "./source/IQAudioRecorderController/bin/Release/IQAudioRecorderController.dll" 
					ToDirectory = "./output/" 
				},	
			}
		}
	},

	Samples = new ISolutionBuilder [] 
	{
		new IOSSolutionBuilder { SolutionPath = "./samples/IQAudioRecorderControllerSample.sln", Configuration = "Release", Platform="iPhone", BuildsOn = BuildPlatforms.Mac },
	},

	Components = new []
	{
		new Component 
		{ 
			ManifestDirectory = "./component/" 
		},
	},
	

	NuGets = new [] 
	{
		new NuGetInfo 
		{ 
			NuSpec = "./nuget/IQAudioRecorderController.nuspec" 
		},
	},
};


SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);


#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var JAR_VERSION = "1.1.0";

var JAR_STDLIB_URL = string.Format ("https://repo1.maven.org/maven2/org/jetbrains/kotlin/kotlin-stdlib/{0}/kotlin-stdlib-{0}.jar", JAR_VERSION);

var JAR_STDLIB_DEST = "./externals/kotlin-stdlib.jar";

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/Xamarin.Kotlin.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/Xamarin.Kotlin.StdLib/bin/Release/Xamarin.Kotlin.StdLib.dll",
				}
			}
		}
	},

	Samples = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			PreBuildAction = () => {
				var gradlew = MakeAbsolute((FilePath)"./native/KotlinSample/gradlew");
				StartProcess (gradlew, new ProcessSettings {
					Arguments = "build",
					WorkingDirectory = "./native/KotlinSample/"
				});
			},
			SolutionPath = "./samples/KotlinSample.sln"
		},
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Kotlin.StdLib.nuspec" },
	},
};

Task ("externals")
	.Does (() => 
{
	EnsureDirectoryExists ("./externals/");

	if (!FileExists (JAR_STDLIB_DEST)) DownloadFile (JAR_STDLIB_URL, JAR_STDLIB_DEST);
});

Task ("clean").IsDependentOn ("clean-base").Does (() => 
{	
	DeleteFiles ("./externals/*.jar");
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

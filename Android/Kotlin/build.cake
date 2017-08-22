
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var JAR_VERSION = "1.1.2-5";

var JAR_STDLIB_URL = string.Format ("https://repo1.maven.org/maven2/org/jetbrains/kotlin/kotlin-stdlib/{0}/kotlin-stdlib-{0}.jar", JAR_VERSION);
var JAR_STDLIB_JRE7_URL = string.Format ("https://repo1.maven.org/maven2/org/jetbrains/kotlin/kotlin-stdlib-jre7/{0}/kotlin-stdlib-jre7-{0}.jar", JAR_VERSION);
var JAR_STDLIB_JRE8_URL = string.Format ("https://repo1.maven.org/maven2/org/jetbrains/kotlin/kotlin-stdlib-jre8/{0}/kotlin-stdlib-jre8-{0}.jar", JAR_VERSION);

var JAR_STDLIB_DEST = "./externals/kotlin-stdlib.jar";
var JAR_STDLIB_JRE7_DEST = "./externals/kotlin-stdlib-jre7.jar";
var JAR_STDLIB_JRE8_DEST = "./externals/kotlin-stdlib-jre8.jar";

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/Xamarin.Kotlin.sln",
			OutputFiles = new [] { 
				new OutputFileCopy { FromFile = "./source/Xamarin.Kotlin.StdLib/bin/Release/Xamarin.Kotlin.StdLib.dll", },
				new OutputFileCopy { FromFile = "./source/Xamarin.Kotlin.StdLib.Jre7/bin/Release/Xamarin.Kotlin.StdLib.Jre7.dll", },
				new OutputFileCopy { FromFile = "./source/Xamarin.Kotlin.StdLib.Jre8/bin/Release/Xamarin.Kotlin.StdLib.Jre8.dll", },
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
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Kotlin.StdLib.Jre7.nuspec" },
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Kotlin.StdLib.Jre8.nuspec" },
	},
};

Task ("externals")
	.Does (() => 
{
	EnsureDirectoryExists ("./externals/");

	if (!FileExists (JAR_STDLIB_DEST)) DownloadFile (JAR_STDLIB_URL, JAR_STDLIB_DEST);
	if (!FileExists (JAR_STDLIB_JRE7_DEST)) DownloadFile (JAR_STDLIB_JRE7_URL, JAR_STDLIB_JRE7_DEST);
	if (!FileExists (JAR_STDLIB_JRE8_DEST)) DownloadFile (JAR_STDLIB_JRE8_URL, JAR_STDLIB_JRE8_DEST);
});

Task ("clean").IsDependentOn ("clean-base").Does (() => 
{	
	DeleteFiles ("./externals/*.jar");
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

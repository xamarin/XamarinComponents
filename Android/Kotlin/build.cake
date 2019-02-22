
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

var JAR_VERSION = "1.3.11";

var JAR_STDLIB_URL = string.Format ("https://repo1.maven.org/maven2/org/jetbrains/kotlin/kotlin-stdlib/{0}/kotlin-stdlib-{0}.jar", JAR_VERSION);
var JAR_STDLIB_JDK7_URL = string.Format ("https://repo1.maven.org/maven2/org/jetbrains/kotlin/kotlin-stdlib-jdk7/{0}/kotlin-stdlib-jdk7-{0}.jar", JAR_VERSION);
var JAR_STDLIB_JDK8_URL = string.Format ("https://repo1.maven.org/maven2/org/jetbrains/kotlin/kotlin-stdlib-jdk8/{0}/kotlin-stdlib-jdk8-{0}.jar", JAR_VERSION);

var JAR_STDLIB_DEST = "./externals/kotlin-stdlib.jar";
var JAR_STDLIB_JDK7_DEST = "./externals/kotlin-stdlib-jdk7.jar";
var JAR_STDLIB_JDK8_DEST = "./externals/kotlin-stdlib-jdk8.jar";

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/Xamarin.Kotlin.sln",
			OutputFiles = new [] { 
				new OutputFileCopy { FromFile = "./source/Xamarin.Kotlin.StdLib/bin/Release/Xamarin.Kotlin.StdLib.dll", },
				new OutputFileCopy { FromFile = "./source/Xamarin.Kotlin.StdLib.Jdk7/bin/Release/Xamarin.Kotlin.StdLib.Jdk7.dll", },
				new OutputFileCopy { FromFile = "./source/Xamarin.Kotlin.StdLib.Jdk8/bin/Release/Xamarin.Kotlin.StdLib.Jdk8.dll", },
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
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Kotlin.StdLib.nuspec", RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Kotlin.StdLib.Jdk7.nuspec", RequireLicenseAcceptance = true },
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Kotlin.StdLib.Jdk8.nuspec", RequireLicenseAcceptance = true },
	},
};

Task ("externals")
	.Does (() => 
{
	EnsureDirectoryExists ("./externals/");

	if (!FileExists (JAR_STDLIB_DEST)) DownloadFile (JAR_STDLIB_URL, JAR_STDLIB_DEST);
	if (!FileExists (JAR_STDLIB_JDK7_DEST)) DownloadFile (JAR_STDLIB_JDK7_URL, JAR_STDLIB_JDK7_DEST);
	if (!FileExists (JAR_STDLIB_JDK8_DEST)) DownloadFile (JAR_STDLIB_JDK8_URL, JAR_STDLIB_JDK8_DEST);
});

Task ("clean").IsDependentOn ("clean-base").Does (() => 
{	
	DeleteFiles ("./externals/*.jar");
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

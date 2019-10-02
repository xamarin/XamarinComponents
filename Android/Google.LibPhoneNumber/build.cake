#addin nuget:?package=Cake.Xamarin.Build
#addin nuget:?package=Cake.Xamarin

var TARGET = Argument ("t", Argument ("target", "Default"));

var NUGET_VERSION = "8.9.13";

var JAR_VERSION = "8.9.13";
var JAR_URL = string.Format ("http://search.maven.org/remotecontent?filepath=com/googlecode/libphonenumber/libphonenumber/{0}/libphonenumber-{0}.jar", JAR_VERSION);
var DOCS_URL = string.Format ("http://search.maven.org/remotecontent?filepath=com/googlecode/libphonenumber/libphonenumber/{0}/libphonenumber-{0}-javadoc.jar", JAR_VERSION);
var JAR_DEST = "./externals/libphonenumber.jar";

var buildSpec = new BuildSpec () {
	Libs = new [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./Xamarin.Google.LibPhoneNumber.sln",
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/bin/Release/monoandroid81/Xamarin.Google.LibPhoneNumber.dll",
				}
			}
		}
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Google.LibPhoneNumber.nuspec", Version = NUGET_VERSION },
	},
};

Task ("externals")
	.Does (() => 
{
	if (!DirectoryExists ("./externals/"))
		CreateDirectory ("./externals");

	if (!FileExists (JAR_DEST))
		DownloadFile (JAR_URL, JAR_DEST);

	if (!FileExists ("./externals/libphonenumber-javadoc.jar"))
		DownloadFile (DOCS_URL, "./externals/libphonenumber-javadoc.jar");
});


Task ("clean").IsDependentOn ("clean-base").Does (() => 
{	
	if (DirectoryExists ("./externals"))
		DeleteDirectory ("./externals", true);
});

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);

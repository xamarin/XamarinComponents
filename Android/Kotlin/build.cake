/*
Error: Could not resolve type with token 01000016 from typeref (expected class
'Cake.Core.CakeTaskBuilder`1' in assembly
'Cake.Core, Version=0.26.1.0, Culture=neutral, PublicKeyToken=null')
*/
//#load "../../common.cake" //

var TARGET = Argument ("t", Argument ("target", "Default"));

var JAR_VERSION = "1.3.31";

var JAR_STDLIB_URL = string.Format ("https://repo1.maven.org/maven2/org/jetbrains/kotlin/kotlin-stdlib/{0}/kotlin-stdlib-{0}.jar", JAR_VERSION);
var JAR_STDLIB_JDK7_URL = string.Format ("https://repo1.maven.org/maven2/org/jetbrains/kotlin/kotlin-stdlib-jdk7/{0}/kotlin-stdlib-jdk7-{0}.jar", JAR_VERSION);
var JAR_STDLIB_JDK8_URL = string.Format ("https://repo1.maven.org/maven2/org/jetbrains/kotlin/kotlin-stdlib-jdk8/{0}/kotlin-stdlib-jdk8-{0}.jar", JAR_VERSION);

var JAR_STDLIB_DEST = "./externals/kotlin-stdlib.jar";
var JAR_STDLIB_JDK7_DEST = "./externals/kotlin-stdlib-jdk7.jar";
var JAR_STDLIB_JDK8_DEST = "./externals/kotlin-stdlib-jdk8.jar";

string[] configs = new string[]
{
	"Debug",
	"Release"
};

Task ("externals")
	.Does
	(
		() =>
		{
			EnsureDirectoryExists ("./externals/");

			if (!FileExists (JAR_STDLIB_DEST))
			{
				DownloadFile (JAR_STDLIB_URL, JAR_STDLIB_DEST);
			}
			if (!FileExists (JAR_STDLIB_JDK7_DEST))
			{
				DownloadFile (JAR_STDLIB_JDK7_URL, JAR_STDLIB_JDK7_DEST);
			}
			if (!FileExists (JAR_STDLIB_JDK8_DEST))
			{
				DownloadFile (JAR_STDLIB_JDK8_URL, JAR_STDLIB_JDK8_DEST);
			}


			string dir = "./native/KotlinSample/";
			var gradlew = MakeAbsolute((FilePath)"./native/KotlinSample/gradlew");

			int exitCodeWithArgument = StartProcess (gradlew, new ProcessSettings {
				Arguments = "build",
				WorkingDirectory = dir
			});

		}
);


Task("libs")
	.IsDependentOn("externals")
	.Does
    (
        () =>
        {
			RestorePackages("./source/**/*.sln");
			Build("./source/**/*.sln");
			Build("./source/**/*.csproj");

			string[] assemblies = new string[]
			{
				"./source/Xamarin.Kotlin.StdLib/bin/Release/Xamarin.Kotlin.StdLib.dll",
				"./source/Xamarin.Kotlin.StdLib.Jdk7/bin/Release/Xamarin.Kotlin.StdLib.Jdk7.dll",
				"./source/Xamarin.Kotlin.StdLib.Jdk8/bin/Release/Xamarin.Kotlin.StdLib.Jdk8.dll",
			};

			EnsureDirectoryExists("./output/");
			foreach(string asm in assemblies)
			{
				CopyFileToDirectory(asm, "./output/");
			}

        }
    );

Task("samples")
.Does
    (
        () =>
        {
			RestorePackages("./samples/**/*.sln");
			Build("./samples/**/*.sln");
			Build("./samples/**/*.csproj");

        }
    );

public void RestorePackages(string pattern)
{
	FilePathCollection files = GetFiles(pattern);

	foreach(FilePath file in files)
	{
		NuGetRestore(file, new NuGetRestoreSettings { } );
	}

	return;
}
public void Build(string pattern)
{
	FilePathCollection files = GetFiles(pattern);

	foreach(FilePath file in files)
	{
		foreach (string config in configs)
		{
			MSBuild
			(
				file.ToString(),
				new MSBuildSettings
				{
					Configuration = config,
				}
				//.WithProperty("DefineConstants", "TRACE;DEBUG;NETCOREAPP2_0;NUNIT")
				.WithProperty("AndroidClassParser", "jar2xml")

			);
		}
	}

	return;
}

public void Package(string pattern)
{
	NuGetPackSettings settings = new NuGetPackSettings
	{
		BasePath = "./",
		OutputDirectory         = "./output/"
		/*
		Id                      = "TestNuGet",
		Version                 = "0.0.0.1",
		Title                   = "The tile of the package",
		Authors                 = new string[] {"John Doe"},
		Owners                  = new string[] {"Contoso"},
		Description             = "The description of the package",
		Summary                 = "Excellent summary of what the package does",
		ProjectUrl              = new Uri("https://github.com/SomeUser/TestNuGet/"),
		IconUrl                 = new Uri("http://cdn.rawgit.com/SomeUser/TestNuGet/master/icons/testNuGet.png"),
		LicenseUrl              = new Uri("https://github.com/SomeUser/TestNuGet/blob/master/LICENSE.md"),
		Copyright               = "Some company 2015",
		ReleaseNotes            = new string[] {"Bug fixes", "Issue fixes", "Typos"},
		Tags                    = new string[] {"Cake", "Script", "Build"},
		RequireLicenseAcceptance= false,
		Symbols                 = false,
		NoPackageAnalysis       = true,
		Files                   = new string[]
		{
			new NuSpecContent {Source = "bin/TestNuGet.dll", Target = "bin"},
		},
		BasePath                = "./src/TestNuGet/bin/release",
		*/
	};

	FilePathCollection files = GetFiles(pattern);

	foreach(FilePath file in files)
	{
		foreach (string config in configs)
		{
			NuGetPack(file.ToString(), settings);
		}
	}

	return;
}
Task ("nuget")
	.IsDependentOn("libs")
	.Does
	(
		() =>
		{
			Package("./nuget/*.nuspec");

			return;
		}
	);

Task ("clean")
	.Does
	(
		() =>
		{
			DeleteFiles ("./externals/*.jar");
		}
	);

Task("Default")
.Does
    (
        () =>
        {
            RunTarget("nuget");
        }
    );

RunTarget (TARGET);

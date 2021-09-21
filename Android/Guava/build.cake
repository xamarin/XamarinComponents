#addin nuget:?package=SharpZipLib&version=1.2.0

var TARGET = Argument ("t", Argument ("target", "ci"));

var GUAVA_VERSION_BASE = "30.0";
var GUAVA_VERSION = GUAVA_VERSION_BASE + "-android";
var GUAVA_FAILUREACCESS_VERSION = "1.0.1";
var GUAVA_LISTENABLEFUTURE_VERSION = "1.0";

var JSR305_VERSION = "3.0.2";
var CHECKER_COMPAT_QUAL_VERSION = "2.5.5";
var ERROR_PRONE_ANNOTATIONS_VERSION = "2.3.3";
var J2OBJC_ANNOTATIONS_VERSION = "1.3";
var ANIMAL_SNIFFER_ANNOTATIONS_VERSION = "1.17";

var GUAVA_JAR_URL = string.Format ("http://search.maven.org/remotecontent?filepath=com/google/guava/guava/{0}/guava-{0}.jar", GUAVA_VERSION);
var GUAVA_DOCS_URL = string.Format ("http://search.maven.org/remotecontent?filepath=com/google/guava/guava/{0}/guava-{0}-javadoc.jar", GUAVA_VERSION);

var GUAVA_FAILUREACCESS_JAR_URL = string.Format ("https://search.maven.org/remotecontent?filepath=com/google/guava/failureaccess/{0}/failureaccess-{0}.jar", GUAVA_FAILUREACCESS_VERSION);
var GUAVA_FAILUREACCESS_DOCS_URL = string.Format("https://search.maven.org/remotecontent?filepath=com/google/guava/failureaccess/{0}/failureaccess-{0}-javadoc.jar", GUAVA_FAILUREACCESS_VERSION);

var GUAVA_LISTENABLEFUTURE_JAR_URL = string.Format ("https://search.maven.org/remotecontent?filepath=com/google/guava/listenablefuture/{0}/listenablefuture-{0}.jar", GUAVA_LISTENABLEFUTURE_VERSION);
var GUAVA_LISTENABLEFUTURE_DOCS_URL = string.Format("https://search.maven.org/remotecontent?filepath=com/google/guava/listenablefuture/{0}/listenablefuture-{0}-javadoc.jar", GUAVA_LISTENABLEFUTURE_VERSION);

var JSR305_JAR_URL = string.Format("https://search.maven.org/remotecontent?filepath=com/google/code/findbugs/jsr305/{0}/jsr305-{0}.jar", JSR305_VERSION);
var CHECKER_COMPAT_QUAL_JAR_URL = string.Format("https://search.maven.org/remotecontent?filepath=org/checkerframework/checker-compat-qual/{0}/checker-compat-qual-{0}.jar", CHECKER_COMPAT_QUAL_VERSION);
var ERROR_PRONE_ANNOTATIONS_JAR_URL = string.Format("https://search.maven.org/remotecontent?filepath=com/google/errorprone/error_prone_annotations/{0}/error_prone_annotations-{0}.jar", ERROR_PRONE_ANNOTATIONS_VERSION);
var J2OBJC_ANNOTATIONS_URL = string.Format("https://search.maven.org/remotecontent?filepath=com/google/j2objc/j2objc-annotations/{0}/j2objc-annotations-{0}.jar", J2OBJC_ANNOTATIONS_VERSION);
var ANIMAL_SNIFFER_ANNOTATIONS_URL = string.Format("https://search.maven.org/remotecontent?filepath=org/codehaus/mojo/animal-sniffer-annotations/{0}/animal-sniffer-annotations-{0}.jar", ANIMAL_SNIFFER_ANNOTATIONS_VERSION);

Task ("externals")
	.WithCriteria (!FileExists ("./externals/guava.jar"))
	.Does (() =>
{
	if (!DirectoryExists ("./externals/"))
		CreateDirectory ("./externals");

	// Download Dependencies
	Information($"Downloading {GUAVA_JAR_URL}");
	DownloadFile (GUAVA_JAR_URL, "./externals/guava.jar");
	Information($"Downloading {GUAVA_DOCS_URL}");
	DownloadFile (GUAVA_DOCS_URL, "./externals/guava-javadocs.jar");

	Information($"Downloading {GUAVA_FAILUREACCESS_JAR_URL}");
	DownloadFile(GUAVA_FAILUREACCESS_JAR_URL, "./externals/guava-failureaccess.jar");
	Information($"Downloading {GUAVA_FAILUREACCESS_DOCS_URL}");
	DownloadFile(GUAVA_FAILUREACCESS_DOCS_URL, "./externals/guava-failureaccess-javadocs.jar");

	Information($"Downloading {GUAVA_LISTENABLEFUTURE_JAR_URL}");
	DownloadFile(GUAVA_LISTENABLEFUTURE_JAR_URL, "./externals/guava-listenablefuture.jar");
	Information($"Downloading {GUAVA_LISTENABLEFUTURE_DOCS_URL}");
	DownloadFile(GUAVA_LISTENABLEFUTURE_DOCS_URL, "./externals/guava-listenablefuture-javadocs.jar");

	Information($"Downloading {JSR305_JAR_URL}");
	DownloadFile(JSR305_JAR_URL, "./externals/jsr305-annotations.jar");
	Information($"Downloading {CHECKER_COMPAT_QUAL_JAR_URL}");
	DownloadFile(CHECKER_COMPAT_QUAL_JAR_URL, "./externals/checker-compat-qual-annotations.jar");
	Information($"Downloading {ERROR_PRONE_ANNOTATIONS_JAR_URL}");
	DownloadFile(ERROR_PRONE_ANNOTATIONS_JAR_URL, "./externals/error-prone-annotations.jar");
	Information($"Downloading {J2OBJC_ANNOTATIONS_URL}");
	DownloadFile(J2OBJC_ANNOTATIONS_URL, "./externals/j2objc-annotations.jar");
	Information($"Downloading {ANIMAL_SNIFFER_ANNOTATIONS_URL}");
	DownloadFile(ANIMAL_SNIFFER_ANNOTATIONS_URL, "./externals/animal-sniffer-annotations.jar");

	Unzip ("./externals/guava-javadocs.jar", "./externals/guava-javadocs/");
	Unzip ("./externals/guava-failureaccess-javadocs.jar", "./externals/guava-failureaccess-javadocs/");
	Unzip ("./externals/guava-listenablefuture-javadocs.jar", "./externals/guava-listenablefuture-javadocs/");

	// We strip out ListenableFuture.class interface from the guava.jar file because the listenablefuture.jar has it already
	// Google did something weird where they make guava depend on listenablefuture version 9999.0-something which is just
	// a blank/empty artifact so that they avoid the conflict/duplicate interface in both places.
	// They should have moved the code out of guava.jar and taken a normal dependency on listenablefuture.jar.

	// guava.jar is ~2mb and listenablefuture.jar is like 3kb, it only has one single interface in it
	// So in order to not make users depend on a 2mb file when they really only need the 3kb one, yet not have duplicate
	// types if they have another dependency which DOES depend on the full guava.jar, and rather than replicate
	// the weird high version number of the empty version of the one, we are going to fix the guava.jar to exclude
	// this type and just always make it depend on listenablefuture which does have the actual implementation

	// All we really need to do is remove the .class file from the .jar (which is a zip):
	ICSharpCode.SharpZipLib.Zip.ZipFile zipFile = null;
	try
	{
		zipFile = new ICSharpCode.SharpZipLib.Zip.ZipFile("./externals/guava.jar");
		zipFile.BeginUpdate();
		 var entry = zipFile.GetEntry("com/google/common/util/concurrent/ListenableFuture.class");
		 if (entry != null) {
			 Information("Deleting File from guava.jar: {0}", "ListenableFuture.class");
			zipFile.Delete(entry);
		}
		zipFile.CommitUpdate();
	}
	finally
	{
		if (zipFile != null)
			zipFile.Close();
	}
});


Task("libs")
	.IsDependentOn("externals")
	.Does(() =>
{
	DotNetCoreRestore ("./Guava.sln");

	DotNetCoreMSBuild ("./Guava.sln",
		new DotNetCoreMSBuildSettings()
			.SetConfiguration("Release")
	);
});

Task("nuget")
	.IsDependentOn("libs")
	.Does(() =>
{
	DotNetCoreMSBuild ("./Guava.sln",
		new DotNetCoreMSBuildSettings()
			.WithTarget("Pack")
			.SetConfiguration("Release")
			.WithProperty ("PackageOutputPath", MakeAbsolute(new FilePath("./output")).FullPath)
			.WithProperty ("PackageRequireLicenseAcceptance", "true")
			.WithProperty ("NoBuild", "true")
	);
});

Task("samples")
	.IsDependentOn("nuget")
	.Does (() =>
{
	MSBuild("./samples/GuavaSample/GuavaSample.csproj", c => c.Restore = true);
});

Task ("clean")
	.Does (() =>
{
	if (DirectoryExists ("./externals/"))
		DeleteDirectory ("./externals", new DeleteDirectorySettings { Recursive = true });
});

Task ("ci")
	.IsDependentOn("libs")
	.IsDependentOn("nuget")
	.IsDependentOn("samples")
	.Does 
	(
		() => {}
	);


RunTarget (TARGET);

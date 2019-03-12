var TARGET = Argument ("t", Argument ("target", "Default"));

var GUAVA_NUGET_VERSION = "27.1";
var GUAVA_FAILUREACCESS_NUGET_VERSION = "1.0.1";

var GUAVA_VERSION = GUAVA_NUGET_VERSION + "-android";
var GUAVA_FAILUREACCESS_VERSION = GUAVA_FAILUREACCESS_NUGET_VERSION;
var JSR305_VERSION = "3.0.2";
var CHECKER_COMPAT_QUAL_VERSION = "2.5.5";
var ERROR_PRONE_ANNOTATIONS_VERSION = "2.3.3";
var J2OBJC_ANNOTATIONS_VERSION = "1.3";
var ANIMAL_SNIFFER_ANNOTATIONS_VERSION = "1.17";

var GUAVA_JAR_URL = string.Format ("http://search.maven.org/remotecontent?filepath=com/google/guava/guava/{0}/guava-{0}.jar", GUAVA_VERSION);
var GUAVA_DOCS_URL = string.Format ("http://search.maven.org/remotecontent?filepath=com/google/guava/guava/{0}/guava-{0}-javadoc.jar", GUAVA_VERSION);

var GUAVA_FAILUREACCESS_JAR_URL = string.Format ("https://search.maven.org/remotecontent?filepath=com/google/guava/failureaccess/{0}/failureaccess-{0}.jar", GUAVA_FAILUREACCESS_VERSION);
var GUAVA_FAILUREACCESS_DOCS_URL = string.Format("https://search.maven.org/remotecontent?filepath=com/google/guava/failureaccess/{0}/failureaccess-{0}-javadoc.jar", GUAVA_FAILUREACCESS_VERSION);

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
	DownloadFile (GUAVA_JAR_URL, "./externals/guava.jar");
	DownloadFile (GUAVA_DOCS_URL, "./externals/guava-javadocs.jar");

	DownloadFile(GUAVA_FAILUREACCESS_JAR_URL, "./externals/guava-failureaccess.jar");
	DownloadFile(GUAVA_FAILUREACCESS_DOCS_URL, "./externals/guava-failureaccess-javadocs.jar");

	DownloadFile(JSR305_JAR_URL, "./externals/jsr305-annotations.jar");
	DownloadFile(CHECKER_COMPAT_QUAL_JAR_URL, "./externals/checker-compat-qual-annotations.jar");
	DownloadFile(ERROR_PRONE_ANNOTATIONS_JAR_URL, "./externals/error-prone-annotations.jar");
	DownloadFile(J2OBJC_ANNOTATIONS_URL, "./externals/j2objc-annotations.jar");
	DownloadFile(ANIMAL_SNIFFER_ANNOTATIONS_URL, "./externals/animal-sniffer-annotations.jar");

	Unzip ("./externals/guava-javadocs.jar", "./externals/guava-javadocs/");
	Unzip ("./externals/guava-failureaccess-javadocs.jar", "./externals/guava-failureaccess-javadocs/");

	// Update .csproj nuget versions
	XmlPoke("./source/Guava/Guava.csproj", "/Project/PropertyGroup/PackageVersion", GUAVA_NUGET_VERSION);
	XmlPoke("./source/Guava.FailureAccess/Guava.FailureAccess.csproj", "/Project/PropertyGroup/PackageVersion", GUAVA_FAILUREACCESS_NUGET_VERSION);
});


Task("libs")
	.IsDependentOn("externals")
	.Does(() =>
{
	MSBuild("./Guava.sln", c => {
		c.Configuration = "Release";
		c.Restore = true;
		c.MaxCpuCount = 0;
		c.Properties.Add("DesignTimeBuild", new [] { "false" });
	});
});

Task("nuget")
	.IsDependentOn("libs")
	.Does(() =>
{
	MSBuild ("./Guava.sln", c => {
		c.Configuration = "Release";
		c.MaxCpuCount = 0;
		c.Targets.Clear();
		c.Targets.Add("Pack");
		c.Properties.Add("PackageOutputPath", new [] { MakeAbsolute(new FilePath("./output")).FullPath });
		c.Properties.Add("PackageRequireLicenseAcceptance", new [] { "true" });
		c.Properties.Add("DesignTimeBuild", new [] { "false" });
	});
});

Task("samples")
	.IsDependentOn("nuget");

Task ("clean")
	.Does (() =>
{
	if (DirectoryExists ("./externals/"))
		DeleteDirectory ("./externals", true);
});

RunTarget (TARGET);

var TARGET = Argument ("t", Argument ("target", "ci"));

var JAR_VERSION = "8.10.23";
var NUGET_VERSION = JAR_VERSION;

var JAR_URL = string.Format ("http://search.maven.org/remotecontent?filepath=com/googlecode/libphonenumber/libphonenumber/{0}/libphonenumber-{0}.jar", JAR_VERSION);
var DOCS_URL = string.Format ("http://search.maven.org/remotecontent?filepath=com/googlecode/libphonenumber/libphonenumber/{0}/libphonenumber-{0}-javadoc.jar", JAR_VERSION);
var JAR_DEST = "./externals/libphonenumber.jar";
//rebuild
Task ("externals")
	.WithCriteria (!FileExists (JAR_DEST))
	.Does (() =>
{
	EnsureDirectoryExists ("./externals");
	
	// Download Dependencies
	DownloadFile (JAR_URL, JAR_DEST);

	// Update .csproj nuget versions
	XmlPoke("./source/Xamarin.Google.LibPhoneNumber.csproj", "/Project/PropertyGroup/PackageVersion", NUGET_VERSION);
});


Task("libs")
	.IsDependentOn("externals")
	.Does(() =>
{
	MSBuild("./Xamarin.Google.LibPhoneNumber.sln", c => {
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
	MSBuild ("./Xamarin.Google.LibPhoneNumber.sln", c => {
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

Task("ci")
	.IsDependentOn("samples");

Task ("clean")
	.Does (() =>
{
	if (DirectoryExists ("./externals/"))
		DeleteDirectory ("./externals", new DeleteDirectorySettings {
			Recursive = true,
			Force = true
		});
});

RunTarget (TARGET);

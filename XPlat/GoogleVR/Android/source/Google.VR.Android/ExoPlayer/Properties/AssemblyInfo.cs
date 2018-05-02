using System.Reflection;
using System.Runtime.CompilerServices;
using Android.App;

// Information about this assembly is defined by the following attributes. 
// Change them to the values specific to your project.

[assembly: AssemblyTitle("ExoPlayer")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("")]
[assembly: AssemblyCopyright("Guannan Wang")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// The assembly version has the format "{Major}.{Minor}.{Build}.{Revision}".
// The form "{Major}.{Minor}.*" will automatically update the build and revision,
// and "{Major}.{Minor}.{Build}.*" will update just the revision.

[assembly: AssemblyVersion("1.0.0")]

// The following attributes are used to specify the signing key for the assembly, 
// if desired. See the Mono documentation for more information about signing.

//[assembly: AssemblyDelaySign(false)]
//[assembly: AssemblyKeyFile("")]

[assembly: Java.Interop.JavaLibraryReference("classes.jar",
	PackageName = __Consts.PackageName,
	SourceUrl = __Consts.Url,
	EmbeddedArchive = __Consts.AarPath,
	Version = __Consts.Version)]

[assembly: Android.IncludeAndroidResourcesFromAttribute("./",
	PackageName = __Consts.PackageName,
	SourceUrl = __Consts.Url,
	EmbeddedArchive = __Consts.AarPath,
	Version = __Consts.Version)]

static class __Consts
{
	public const string PackageName = "exoplayer";
	public const string AarPath = "";
	public const string Version = "r1.5.2";
	public const string Url = "https://bintray.com/google/exoplayer/download_file?file_path=com%2Fgoogle%2Fandroid%2Fexoplayer%2Fexoplayer%2F" + Version + "%2Fexoplayer-" + Version + ".aar";
}


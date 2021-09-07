var TARGET = Argument ("t", Argument ("target", "ci"));

var NUGET_PATCH = ".2";

var GLIDE_VERSION = "4.11.0";
var GLIDE_NUGET_VERSION = GLIDE_VERSION + NUGET_PATCH;
var GLIDE_URL = $"https://repo1.maven.org/maven2/com/github/bumptech/glide/glide/{GLIDE_VERSION}/glide-{GLIDE_VERSION}.aar";

var GIFDECODER_VERSION = GLIDE_VERSION;
var GIFDECODER_NUGET_VERSION = GIFDECODER_VERSION + NUGET_PATCH;
var GIFDECODER_URL = $"https://repo1.maven.org/maven2/com/github/bumptech/glide/gifdecoder/{GIFDECODER_VERSION}/gifdecoder-{GIFDECODER_VERSION}.aar";

var DISKLRUCACHE_VERSION = GLIDE_VERSION;
var DISKLRUCACHE_NUGET_VERSION = DISKLRUCACHE_VERSION + NUGET_PATCH;
var DISKLRUCACHE_URL = $"https://repo1.maven.org/maven2/com/github/bumptech/glide/disklrucache/{DISKLRUCACHE_VERSION}/disklrucache-{DISKLRUCACHE_VERSION}.jar";

var RECYCLERVIEW_VERSION = GLIDE_VERSION;
var RECYCLERVIEW_NUGET_VERSION = RECYCLERVIEW_VERSION + NUGET_PATCH;
var RECYCLERVIEW_URL = $"https://repo1.maven.org/maven2/com/github/bumptech/glide/recyclerview-integration/{RECYCLERVIEW_VERSION}/recyclerview-integration-{RECYCLERVIEW_VERSION}.aar";

Task ("externals")
	.WithCriteria (!FileExists ("./externals/glide.aar"))
	.Does (() =>
{
	if (!DirectoryExists ("./externals/"))
		CreateDirectory ("./externals");

	// Download Dependencies
	DownloadFile (GLIDE_URL, "./externals/glide.aar");
	
	DownloadFile(GIFDECODER_URL, "./externals/gifdecoder.aar");

	DownloadFile(DISKLRUCACHE_URL, "./externals/disklrucache.jar");

	DownloadFile(RECYCLERVIEW_URL, "./externals/recyclerview-integration.aar");

	// Update .csproj nuget versions
	XmlPoke("./source/Xamarin.Android.Glide/Xamarin.Android.Glide.csproj", "/Project/PropertyGroup/PackageVersion", GLIDE_NUGET_VERSION);
	XmlPoke("./source/Xamarin.Android.Glide.DiskLruCache/Xamarin.Android.Glide.DiskLruCache.csproj", "/Project/PropertyGroup/PackageVersion", DISKLRUCACHE_NUGET_VERSION);
	XmlPoke("./source/Xamarin.Android.Glide.GifDecoder/Xamarin.Android.Glide.GifDecoder.csproj", "/Project/PropertyGroup/PackageVersion", GIFDECODER_NUGET_VERSION);
	XmlPoke("./source/Xamarin.Android.Glide.RecyclerViewIntegration/Xamarin.Android.Glide.RecyclerViewIntegration.csproj", "/Project/PropertyGroup/PackageVersion", RECYCLERVIEW_NUGET_VERSION);
});

Task("libs")
	.IsDependentOn("externals")
	.Does(() =>
{
	MSBuild("./source/Xamarin.Android.Glide.sln", c => {
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
	MSBuild ("./source/Xamarin.Android.Glide.sln", c => {
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
	.IsDependentOn("nuget")
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

var TARGET = Argument ("t", Argument ("target", "ci"));

var NUGET_VERSION = "18.0.2-eap.6";
var AAR_URL = "https://github.com/google/exposure-notifications-android/raw/174cf0dab62d0c7c5d2c5d1abe5bef595d0d4942/app/libs/play-services-nearby-18.0.2-eap.aar";

Task ("externals")
	.Does (() =>
{
	EnsureDirectoryExists ("./externals");
	
	DownloadFile(AAR_URL, "./externals/play-services-nearby-18.0.2-eap.aar");

	// Update .csproj nuget versions
	XmlPoke("./source/PlayServicesNearby/PlayServicesNearby.csproj", "/Project/PropertyGroup/PackageVersion", NUGET_VERSION);
});


Task("libs")
	.IsDependentOn("externals")
	.Does(() =>
{
	MSBuild("./PlayServicesNearby.sln", c => {
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
	MSBuild ("./PlayServicesNearby.sln", c => {
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

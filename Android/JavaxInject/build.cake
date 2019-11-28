var TARGET = Argument ("t", Argument ("target", "ci"));

var JAVAX_INJECT_VERSION = "1";
var JAVAX_INJECT_NUGET_VERSION = JAVAX_INJECT_VERSION;
var JAVAX_INJECT_URL = $"https://repo1.maven.org/maven2/javax/inject/javax.inject/{JAVAX_INJECT_VERSION}/javax.inject-{JAVAX_INJECT_VERSION}.jar";

Task ("externals")
	.WithCriteria (!FileExists ("./externals/javax-inject.jar"))
	.Does (() =>
{
	EnsureDirectoryExists ("./externals");
	
	// Download Dependencies
	DownloadFile (JAVAX_INJECT_URL, "./externals/javax-inject.jar");

	// Update .csproj nuget versions
	XmlPoke("./source/JavaxInject/JavaxInject.csproj", "/Project/PropertyGroup/PackageVersion", JAVAX_INJECT_NUGET_VERSION);
});


Task("libs")
	.IsDependentOn("externals")
	.Does(() =>
{
	MSBuild("./JavaxInject.sln", c => {
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
	MSBuild ("./JavaxInject.sln", c => {
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

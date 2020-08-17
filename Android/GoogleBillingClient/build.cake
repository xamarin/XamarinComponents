
var TARGET = Argument ("t", Argument ("target", "Default"));

var AAR_VERSION = "3.0.0";
var NUGET_VERSION = AAR_VERSION;

var AAR_URL = $"https://dl.google.com/dl/android/maven2/com/android/billingclient/billing/{AAR_VERSION}/billing-{AAR_VERSION}.aar";


Task ("externals")
	.WithCriteria(!FileExists("./externals/billing.aar"))
	.Does (() => 
{
	EnsureDirectoryExists ("./externals/");
	DownloadFile (AAR_URL, "./externals/billing.aar");
	Unzip("./externals/billing.aar", "./externals/billing/");

	XmlPoke("./source/GoogleBillingClient/GoogleBillingClient.csproj", "/Project/PropertyGroup/PackageVersion", NUGET_VERSION);
});

Task("libs")
	.IsDependentOn("externals")
	.Does(() =>
{
	MSBuild("./source/GoogleBillingClient.sln", c => {
		c.Configuration = "Release";
		c.Restore = true;
		c.Properties.Add("DesignTimeBuild", new [] { "false" });
	});
});

Task("nuget")
	.IsDependentOn("libs")
	.Does(() =>
{
	MSBuild ("./source/GoogleBillingClient.sln", c => {
		c.Configuration = "Release";
		c.Targets.Clear();
		c.Targets.Add("Pack");
		c.Properties.Add("PackageOutputPath", new [] { MakeAbsolute(new FilePath("./output")).FullPath });
		c.Properties.Add("PackageRequireLicenseAcceptance", new [] { "true" });
		c.Properties.Add("DesignTimeBuild", new [] { "false" });
	});
});

Task ("clean")
	.Does (() =>
{
	if (DirectoryExists ("./externals/"))
		DeleteDirectory ("./externals", true);
});

RunTarget (TARGET);


var TARGET = Argument ("t", Argument ("target", "ci"));

var AAR_VERSION = "5.0.0";
var AAR_KTX_VERSION = "5.0.0";
var NUGET_VERSION = AAR_VERSION;

var AAR_URL = $"https://dl.google.com/dl/android/maven2/com/android/billingclient/billing/{AAR_VERSION}/billing-{AAR_VERSION}.aar";
var AAR_KTX_URL = $"https://dl.google.com/dl/android/maven2/com/android/billingclient/billing-ktx/{AAR_KTX_VERSION}/billing-ktx-{AAR_KTX_VERSION}.aar";


Task ("externals")
	.WithCriteria(!FileExists($"./externals/billing-{AAR_VERSION}.aar"))
	.WithCriteria(!FileExists($"./externals/billing-{AAR_KTX_VERSION}.aar"))
	.Does (() =>
{
	EnsureDirectoryExists ("./externals/");
	DownloadFile (AAR_URL, $"./externals/billing-{AAR_VERSION}.aar");
	DownloadFile (AAR_KTX_URL, $"./externals/billing-ktx-{AAR_KTX_VERSION}.aar");
	Unzip($"./externals/billing-{AAR_VERSION}.aar", $"./externals/billing-{AAR_VERSION}/");
	Unzip($"./externals/billing-ktx-{AAR_KTX_VERSION}.aar", $"./externals/billing-ktx-{AAR_KTX_VERSION}/");

	XmlPoke("./source/GoogleBillingClient/GoogleBillingClient.csproj", "/Project/PropertyGroup/PackageVersion", NUGET_VERSION);
	XmlPoke("./source/GoogleBillingClient.Ktx/GoogleBillingClient.Ktx.csproj", "/Project/PropertyGroup/PackageVersion", NUGET_VERSION);
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

Task ("ci")
	.IsDependentOn("nuget")
	;

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

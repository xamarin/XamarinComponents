var TARGET = Argument ("t", Argument ("target", "ci"));

var IOS_VERSION = "3.0.0-preview2";
var IOS_NUGET_VERSION = "3.0.0-preview2";
var IOS_URL = $"https://github.com/Azure/azure-notificationhubs-ios/releases/download/{IOS_VERSION}/WindowsAzureMessaging.framework.zip";

var ANDROID_VERSION = "1.0.0-preview2";
var ANDROID_NUGET_VERSION = "1.0.0-preview2.1";
var ANDROID_URL = string.Format ("https://dl.bintray.com/microsoftazuremobile/SDK/com/microsoft/azure/notification-hubs-android-sdk/{0}/notification-hubs-android-sdk-{0}.aar", ANDROID_VERSION);

Task("libs-ios")
	.WithCriteria(IsRunningOnUnix())
	.IsDependentOn("externals-ios")
	.Does (() =>
{
	MSBuild("./iOS/source/Xamarin.Azure.NotificationHubs.iOS.sln", c => {
		c.Configuration = "Release";
		c.Restore = true;
		c.Targets.Clear();
		c.Targets.Add("Rebuild");
		c.Properties.Add("DesignTimeBuild", new [] { "false" });
		c.BinaryLogger = new MSBuildBinaryLogSettings {
			Enabled = true,
			FileName = "./output/libs-ios.binlog"
		};
	});
});

Task("libs-android")
	.IsDependentOn("externals-android")
	.Does (() =>
{
	MSBuild("./Android/source/Xamarin.Azure.NotificationHubs.Android.sln", c => {
		c.Configuration = "Release";
		c.Restore = true;
		c.Targets.Clear();
		c.Targets.Add("Rebuild");
		c.Properties.Add("DesignTimeBuild", new [] { "false" });
		c.BinaryLogger = new MSBuildBinaryLogSettings {
			Enabled = true,
			FileName = "./output/libs-android.binlog"
		};
	});
});

Task("nuget-ios")
	.WithCriteria(IsRunningOnUnix())
	.IsDependentOn("libs-ios")
	.Does (() =>
{
	MSBuild ("./iOS/source/Xamarin.Azure.NotificationHubs.iOS.sln", c => {
		c.Configuration = "Release";
		c.Targets.Clear();
		c.Targets.Add("Pack");
		c.Properties.Add("PackageOutputPath", new [] { MakeAbsolute(new FilePath("./output")).FullPath });
		c.Properties.Add("PackageRequireLicenseAcceptance", new [] { "true" });
		c.Properties.Add("DesignTimeBuild", new [] { "false" });
		c.BinaryLogger = new MSBuildBinaryLogSettings {
			Enabled = true,
			FileName = "./output/nuget-ios.binlog"
		};
	});
});


Task("nuget-android")
	.IsDependentOn("libs-android")
	.Does (() =>
{
	MSBuild ("./Android/source/Xamarin.Azure.NotificationHubs.Android.sln", c => {
		c.Configuration = "Release";
		c.Targets.Clear();
		c.Targets.Add("Pack");
		c.Properties.Add("PackageOutputPath", new [] { MakeAbsolute(new FilePath("./output")).FullPath });
		c.Properties.Add("PackageRequireLicenseAcceptance", new [] { "true" });
		c.Properties.Add("DesignTimeBuild", new [] { "false" });
		c.BinaryLogger = new MSBuildBinaryLogSettings {
			Enabled = true,
			FileName = "./output/nuget-android.binlog"
		};
	});
});


Task("samples-ios")
	.WithCriteria(IsRunningOnUnix())
	.IsDependentOn("nuget-ios")
	.Does (() =>
{
	MSBuild("./iOS/samples/NotificationHubsSampleiOS.sln", c => {
		c.Configuration = "Release";
		c.Restore = true;
		c.Targets.Clear();
		c.Targets.Add("Build");
		c.BinaryLogger = new MSBuildBinaryLogSettings {
			Enabled = true,
			FileName = "./output/samples-ios.binlog"
		};
	});
});


Task("samples-android")
	.IsDependentOn("nuget-android")
	.Does (() =>
{
	MSBuild("./Android/samples/NotificationHubsSampleAndroid.sln", c => {
		c.Configuration = "Release";
		c.Restore = true;
		c.Targets.Clear();
		c.Targets.Add("Build");
		c.BinaryLogger = new MSBuildBinaryLogSettings {
			Enabled = true,
			FileName = "./output/samples-android.binlog"
		};
	});
});

Task("ci")
	.IsDependentOn("nuget");


Task ("externals-ios")
	.WithCriteria(IsRunningOnUnix())
	.WithCriteria (!FileExists ("./iOS/externals/sdk.zip"))
	.Does (() => 
{
	EnsureDirectoryExists ("./iOS/externals");

	DownloadFile (IOS_URL, "./iOS/externals/sdk.zip");

	Unzip ("./iOS/externals/sdk.zip", "./iOS/externals");
	
	XmlPoke("./iOS/source/Xamarin.Azure.NotificationHubs.iOS.csproj", "/Project/PropertyGroup/PackageVersion", IOS_NUGET_VERSION);
});

Task ("externals-android")
	.WithCriteria (!FileExists ("./externals/Android/notificationhubs.aar"))
	.Does (() => 
{
	EnsureDirectoryExists ("./Android/externals");

	DownloadFile (ANDROID_URL, "./Android/externals/notificationhubs.aar");
	
	XmlPoke("./Android/source/Xamarin.Azure.NotificationHubs.Android.csproj", "/Project/PropertyGroup/PackageVersion", ANDROID_NUGET_VERSION);
});

Task ("externals")
	.IsDependentOn ("externals-ios")
	.IsDependentOn ("externals-android");

Task ("samples")
	.IsDependentOn ("samples-ios")
	.IsDependentOn ("samples-android");

Task ("nuget")
	.IsDependentOn ("nuget-ios")
	.IsDependentOn ("nuget-android");

Task ("libs")
	.IsDependentOn ("libs-ios")
	.IsDependentOn ("libs-android");

Task ("clean")
	.Does (() => 
{
	if (DirectoryExists ("./Android/externals"))
		DeleteDirectory ("./Android/externals", true);

	if (DirectoryExists ("./iOS/externals"))
		DeleteDirectory ("./iOS/externals", true);
});

RunTarget (TARGET);

#tool "XamarinComponent"
#addin "nuget:?package=Cake.Json"
#addin "nuget:?package=Cake.Xamarin"
#addin "nuget:?package=Cake.ExtendedNuGet&version=1.0.0.24"
#addin "nuget:?package=NuGet.Core&version=2.14.0"

// NOTE: COOKIE_JAR_PATH Environment variable should contain the .xamarin cookie file

var TARGET = Argument ("target", Argument ("t", "build"));

var PREVIEW_ONLY = Argument ("preview", EnvironmentVariable ("PREVIEW_ONLY") ?? "false").Equals ("true");

var NUGET_FORCE_PUSH = Argument ("nuget-force-push", EnvironmentVariable ("NUGET_FORCE_PUSH") ?? "false").Equals ("true");
var NUGET_MAX_ATTEMPTS = 5;

var MYGET_FORCE_PUSH = Argument ("myget-force-push", EnvironmentVariable ("MYGET_FORCE_PUSH") ?? "false").Equals ("true");
var MYGET_MAX_ATTEMPTS = 5;

var COMP_MAX_ATTEMPTS = 3;

var NUGET_API_KEY = Argument ("nuget-api-key", EnvironmentVariable ("NUGET_API_KEY") ?? "");
var NUGET_SOURCE = Argument ("nuget-source", EnvironmentVariable ("NUGET_SOURCE") ?? "");
var NUGET_PUSH_SOURCE = Argument ("nuget-push-source", EnvironmentVariable ("NUGET_PUSH_SOURCE") ?? "");

var MYGET_API_KEY = Argument ("myget-api-key", EnvironmentVariable ("MYGET_API_KEY") ?? "");
var MYGET_SOURCE = Argument ("myget-source", EnvironmentVariable ("MYGET_SOURCE") ?? "");
var MYGET_PUSH_SOURCE = Argument ("myget-push-source", EnvironmentVariable ("MYGET_PUSH_SOURCE") ?? "");

var XAM_ACCT_EMAIL = Argument ("xamarin-account-email", EnvironmentVariable ("XAM_ACCT_EMAIL") ?? "");
var XAM_ACCT_PWD = Argument ("xamarin-account-password", EnvironmentVariable ("XAM_ACCT_PWD") ?? "");

var GLOB_PATTERNS = Argument ("glob-patterns", EnvironmentVariable ("GLOBBER_FILE_PATTERNS"));

Action<string[]> DumpGlobPatterns = (string[] globPatterns) => {

	foreach (var gp in globPatterns) {

		Information ("Matching: {0}", gp ?? "");

		var files = GetFiles (gp);
		
		if (files == null || !files.Any ())
			continue;

		foreach (var f in files)
			Information ("Matched: {0}", f);

		Information ("GLOBBER_FILE_PATTERNS={0}", string.Join (",", files));
	}
};

Task ("MyGet").Does (() =>
{
	var globPatterns = (GLOB_PATTERNS ?? "./output/*.nupkg").Split (new [] { ',', ';', ' ' });

	DumpGlobPatterns (globPatterns);
	if (PREVIEW_ONLY)
		return;

	var settings = new PublishNuGetsSettings {
		MaxAttempts = MYGET_MAX_ATTEMPTS,
		ForcePush = MYGET_FORCE_PUSH
	};

	PublishNuGets (MYGET_SOURCE, MYGET_PUSH_SOURCE, MYGET_API_KEY, settings, globPatterns);
});

Task ("NuGet").Does (() =>
{
	var globPatterns = (GLOB_PATTERNS ?? "./output/*.nupkg").Split (new [] { ',', ';', ' ' });

	DumpGlobPatterns (globPatterns);
	if (PREVIEW_ONLY)
		return;

	var settings = new PublishNuGetsSettings {
		MaxAttempts = NUGET_MAX_ATTEMPTS,
		ForcePush = NUGET_FORCE_PUSH
	};

	PublishNuGets (NUGET_SOURCE, NUGET_PUSH_SOURCE, NUGET_API_KEY, settings, globPatterns);
});

Task ("Component").Does (() => 
{
	var globPatterns = (GLOB_PATTERNS ?? "./output/*.xam").Split (new [] { ',', ';', ' ' });

	DumpGlobPatterns (globPatterns);
	if (PREVIEW_ONLY)
		return;

	var settings = new XamarinComponentUploadSettings { 
		Email = XAM_ACCT_EMAIL,
		Password = XAM_ACCT_PWD,
		MaxAttempts = COMP_MAX_ATTEMPTS,
	};

	UploadComponents (settings, globPatterns);
});

RunTarget (TARGET);
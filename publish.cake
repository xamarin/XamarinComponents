#addin "nuget:?package=Cake.Http&version=0.5.0"
#addin "nuget:?package=Cake.Json&version=3.0.1"
#addin "nuget:?package=Newtonsoft.Json&version=9.0.1"
#addin "nuget:?package=Cake.Xamarin&version=3.0.0"
#addin "nuget:?package=Cake.ExtendedNuGet&version=1.0.0.27"
#addin "nuget:?package=NuGet.Core&version=2.14.0"
#addin "nuget:?package=Xamarin.Nuget.Validator&version=1.1.1"

var DEFAULT_SIGNTOOL_PATH = IsRunningOnWindows ()
	? "C:\\Program Files (x86)\\Windows Kits\\10\\bin\\x64\\signtool.exe"
	: "/Library/Frameworks/Mono.framework/Versions/Current/Commands/chktrust";
var DEFAULT_NUGET_PATH = Context.Tools.Resolve ("nuget.exe").FullPath;
// this is the SHA256 hash of the Microsoft Corporation certificate
var DEFAULT_MS_FINGERPRINT = "3F9001EA83C560D712C24CF213C3D312CB3BFF51EE89435D3430BD06B5D0EECE";

var TARGET = Argument ("target", Argument ("t", "build"));

var PREVIEW_ONLY = GetArgBool ("preview", "PREVIEW_ONLY");
var NUGET_FINGERPRINTS = GetArg ("nuget-fingerprints", "NUGET_FINGERPRINTS", DEFAULT_MS_FINGERPRINT);
var MANIFEST_URL = GetArg ("build-manifest-url", "BUILD_MANIFEST_URL");

var SIGNTOOL_PATH = GetArg ("signtool-path", "SIGNTOOL_PATH", DEFAULT_SIGNTOOL_PATH);
var NUGET_PATH = GetArg ("nuget-path", "NUGET_PATH", DEFAULT_NUGET_PATH);

var NUGET_API_KEY = GetArg ("nuget-api-key", "NUGET_API_KEY");
var NUGET_SOURCE = GetArg ("nuget-source", "NUGET_SOURCE");
var NUGET_PUSH_SOURCE = GetArg ("nuget-push-source", "NUGET_PUSH_SOURCE");
var NUGET_FORCE_PUSH = GetArgBool ("nuget-force-push", "NUGET_FORCE_PUSH");
var NUGET_MAX_ATTEMPTS = 5;

var MYGET_API_KEY = GetArg ("myget-api-key", "MYGET_API_KEY");
var MYGET_SOURCE = GetArg ("myget-source", "MYGET_SOURCE");
var MYGET_PUSH_SOURCE = GetArg ("myget-push-source", "MYGET_PUSH_SOURCE");
var MYGET_FORCE_PUSH = GetArgBool ("myget-force-push", "MYGET_FORCE_PUSH");
var MYGET_MAX_ATTEMPTS = 5;

var CUSTOM_API_KEY = GetArg ("custom-api-key", "CUSTOM_API_KEY");
var CUSTOM_PUSH_SOURCE = GetArg ("custom-push-source", "CUSTOM_PUSH_SOURCE");
var CUSTOM_SOURCE = GetArg ("custom-source", "CUSTOM_SOURCE", CUSTOM_PUSH_SOURCE);
var CUSTOM_FORCE_PUSH = GetArgBool ("custom-force-push", "CUSTOM_FORCE_PUSH");
var CUSTOM_MAX_ATTEMPTS = 3;

var GLOB_PATTERNS = GetArgList ("glob-patterns", "GLOBBER_FILE_PATTERNS", "./output/*.nupkg");

var SKIP_FILE_PATTERNS = GetArgList ("skip-files", "SKIP_FILE_PATTERNS", "");

DumpGlobPatterns (GLOB_PATTERNS);

public partial class BuildManifest
{
	public string Url { get; set; }
	public string Sha256 { get; set; }
	public long Size { get; set; }
}

string GetArg (string cmd, string env, string def = "")
{
	return Argument (cmd, EnvironmentVariable (env) ?? def);
}
string[] GetArgList (string cmd, string env, string def = "")
{
	return GetArg (cmd, env, def).Split (new [] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
}
bool GetArgBool (string cmd, string env, bool def = false)
{
	return GetArg (cmd, env, def.ToString ()).ToLower ().Equals ("true");
}

string Sha256File (FilePath file)
{
	var crypt = new System.Security.Cryptography.SHA256Managed();
	string hash = String.Empty;
	using (var stream = System.IO.File.OpenRead(MakeAbsolute(file).FullPath)) {
		var hashBytes = crypt.ComputeHash(stream);
		foreach (byte theByte in hashBytes)
			hash += theByte.ToString("x2");	
	}
	return hash;
}

void DumpGlobPatterns (string[] globPatterns)
{
	foreach (var gp in globPatterns) {
		Information ("Matching: {0}", gp ?? "");

		var files = GetFiles (gp);
		if (files == null || !files.Any ())
			continue;

		foreach (var f in files)
			Information ("Matched: {0}", f);

		Information ("GLOBBER_FILE_PATTERNS={0}", string.Join (",", files));
	}
}

Task ("DownloadArtifacts")
	.WithCriteria (!string.IsNullOrEmpty (MANIFEST_URL))
	.Does (() => 
{
	var manifestJson = HttpGet (MANIFEST_URL);
	var buildManifests = DeserializeJson<BuildManifest[]> (manifestJson);

	var downloadDir = new DirectoryPath ("./output/");
	EnsureDirectoryExists (downloadDir);

	foreach (var buildManifest in buildManifests) {
		var uri = new Uri (buildManifest.Url);
		var filename = System.IO.Path.GetFileName(uri.LocalPath);
		var downloadedFile = downloadDir.CombineWithFilePath (filename);
		
		var skip = false;
		
		if (SKIP_FILE_PATTERNS != null && SKIP_FILE_PATTERNS.Any()) {
			foreach (var rxSkip in SKIP_FILE_PATTERNS) {
				if (System.Text.RegularExpressions.Regex.IsMatch(filename, rxSkip))
					skip = true;
			}
		}
		
		if (skip || !downloadedFile.GetExtension().Equals(".nupkg", StringComparison.InvariantCultureIgnoreCase)) {
			Information ("Skipping: {0}", filename);
			continue;
		}

		Information ("Downloading: {0}", filename);

		DownloadFile (buildManifest.Url, downloadedFile);
		var downloadedFileHash = Sha256File (downloadedFile);

		if (!downloadedFileHash.Equals (buildManifest.Sha256, StringComparison.InvariantCultureIgnoreCase))
			throw new Exception ("Download Corrupt");
	}
});

Task ("VerifyNuGets")
	.IsDependentOn ("VerifyAuthenticode")
	.IsDependentOn ("VerifyNuGetSigning")
	.IsDependentOn ("VerifyNugetMetaData");

Task ("VerifyNuGetSigning")
	.IsDependentOn ("DownloadArtifacts")
	.Does (() => 
{
	foreach (var globPattern in GLOB_PATTERNS) {
		var nupkgFiles = GetFiles (globPattern);

		foreach (var nupkgFile in nupkgFiles) {
			Information ("Verifiying Signature of {0}", nupkgFile.GetFilename ());

			IEnumerable<string> stdout;
			var result = StartProcess (NUGET_PATH, new ProcessSettings {
				Arguments = $"verify -All -CertificateFingerprint \"{NUGET_FINGERPRINTS}\" -Verbosity Normal \"{nupkgFile}\"",
				RedirectStandardOutput = true,
			}, out stdout);

			var stdoutput = string.Join ("\n    ", stdout);
			Information (" -> {0}", nupkgFile.GetFilename ());
			Information ("    " + stdoutput);
			Information ("");

			if (result != 0)
				throw new Exception ($"Invalid Signature {nupkgFile.GetFilename ()}");
		}
	}
});

Task ("VerifyNugetMetaData")
	.IsDependentOn ("DownloadArtifacts")
	.Does (() => 
{
	var options = new Xamarin.Nuget.Validator.NugetValidatorOptions()
	{
		Copyright = "© Microsoft Corporation. All rights reserved.",
		Author = "Microsoft",
		Owner = "Microsoft",
		NeedsProjectUrl = true,
		NeedsLicenseUrl = true,
		ValidateRequireLicenseAcceptance = true,
		ValidPackageNamespace = new [] { "Xamarin", "Mono", "SkiaSharp", "HarfBuzzSharp", "mdoc", "UrhoSharp", "Masonry" },
	};

	foreach (var globPattern in GLOB_PATTERNS) {
		var nupkgFiles = GetFiles (globPattern);

		foreach (var nupkgFile in nupkgFiles) {
			Information ("Verifiying Metadata of {0}", nupkgFile.GetFilename ());

			var result = Xamarin.Nuget.Validator.NugetValidator.Validate(MakeAbsolute(nupkgFile).FullPath, options);

			if (!result.Success)
			{
				Information ("Metadata validation failed for: {0} \n\n", nupkgFile.GetFilename ());
				Information (string.Join("\n    ", result.ErrorMessages));
				throw new Exception ($"Invalid Metadata for: {nupkgFile.GetFilename ()}");

			}
			else
			{
				Information ("Metadata validation passed for: {0}", nupkgFile.GetFilename ());
			}
			
		}
	}
});

Task ("VerifyAuthenticode")
	.IsDependentOn ("DownloadArtifacts")
	.Does (() => 
{
	foreach (var globPattern in GLOB_PATTERNS) {
		var nupkgFiles = GetFiles (globPattern);

		foreach (var nupkgFile in nupkgFiles) {
			Information ("Verifiying Signatures of Assemblies inside of {0}", nupkgFile.GetFilename());

			if (DirectoryExists ("./tmpnupkg"))
				DeleteDirectory ("./tmpnupkg", true);
			EnsureDirectoryExists("./tmpnupkg");

			Unzip (nupkgFile, "./tmpnupkg");

			var assemblies = GetFiles ("./tmpnupkg/**/*.dll") + GetFiles ("./tmpnupkg/**/*.exe");
			foreach (var assembly in assemblies) {

				IEnumerable<string> stdout;
				var stdoutput = string.Empty;
				bool verified = false;

				if (IsRunningOnWindows ()) {
					StartProcess (SIGNTOOL_PATH, new ProcessSettings {
						Arguments = $"verify /pa \"{MakeAbsolute(assembly)}\"",
						RedirectStandardOutput = true,
					}, out stdout);
					stdoutput = string.Join("\n    ", stdout);
					verified = stdoutput.Contains ("Successfully verified");
				} else {
					StartProcess (SIGNTOOL_PATH, new ProcessSettings {
						Arguments = $"\"{MakeAbsolute(assembly)}\"",
						RedirectStandardOutput = true,
					}, out stdout);
					stdoutput = string.Join("\n    ", stdout);
					verified = !stdoutput.Contains ("doesn't contain a digital signature");
				}
				Information (" -> {0}", assembly.GetFilename());
				Information ("    " + stdoutput);

				if (!verified)
					throw new Exception (string.Format("Missing Authenticode Signature found in {0} for {1}", assembly.GetFilename(), nupkgFile.GetFilename()));
			}
		}
	}
});

Task ("MyGet")
	.IsDependentOn("VerifyNuGets")
	.Does (() =>
{
	if (PREVIEW_ONLY) {
		Warning ("Skipping publish due to \"preview only\" flag.");
		return;
	}

	var settings = new PublishNuGetsSettings {
		MaxAttempts = MYGET_MAX_ATTEMPTS,
		ForcePush = MYGET_FORCE_PUSH
	};

	PublishNuGets (MYGET_SOURCE, MYGET_PUSH_SOURCE, MYGET_API_KEY, settings, GLOB_PATTERNS);
});

Task ("NuGet")
	.IsDependentOn("VerifyNuGets")
	.Does (() =>
{
	if (PREVIEW_ONLY) {
		Warning ("Skipping publish due to \"preview only\" flag.");
		return;
	}

	var settings = new PublishNuGetsSettings {
		MaxAttempts = NUGET_MAX_ATTEMPTS,
		ForcePush = NUGET_FORCE_PUSH
	};

	PublishNuGets (NUGET_SOURCE, NUGET_PUSH_SOURCE, NUGET_API_KEY, settings, GLOB_PATTERNS);
});

Task ("Custom")
	.IsDependentOn("VerifyNuGets")
	.Does (() =>
{
	if (PREVIEW_ONLY) {
		Warning ("Skipping publish due to \"preview only\" flag.");
		return;
	}

	var settings = new PublishNuGetsSettings {
		MaxAttempts = CUSTOM_MAX_ATTEMPTS,
		ForcePush = CUSTOM_FORCE_PUSH
	};

	PublishNuGets (CUSTOM_SOURCE, CUSTOM_PUSH_SOURCE, CUSTOM_API_KEY, settings, GLOB_PATTERNS);
});

RunTarget (TARGET);

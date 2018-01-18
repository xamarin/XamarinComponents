#addin "nuget:?package=Cake.Http&version=0.4.0"
#addin "nuget:?package=Cake.Json&version=1.0.2.13"
#addin "nuget:?package=Cake.Xamarin&version=1.3.0.15"
#addin "nuget:?package=Cake.ExtendedNuGet&version=1.0.0.24"
#addin "nuget:?package=NuGet.Core&version=2.14.0"

var TARGET = Argument ("target", Argument ("t", "build"));

var PREVIEW_ONLY = Argument ("preview", EnvironmentVariable ("PREVIEW_ONLY") ?? "false").Equals ("true");
var SIGNTOOL_PATH = Argument ("signtool-path", EnvironmentVariable ("SIGNTOOL_PATH") ?? "C:\\Program Files (x86)\\Windows Kits\\10\\bin\\x64\\signtool.exe");

var NUGET_FORCE_PUSH = Argument ("nuget-force-push", EnvironmentVariable ("NUGET_FORCE_PUSH") ?? "false").Equals ("true");
var NUGET_MAX_ATTEMPTS = 5;

var MYGET_FORCE_PUSH = Argument ("myget-force-push", EnvironmentVariable ("MYGET_FORCE_PUSH") ?? "false").Equals ("true");
var MYGET_MAX_ATTEMPTS = 5;

var MANIFEST_URL = Argument ("build-manifest-url", EnvironmentVariable ("BUILD_MANIFEST_URL") ?? "");

var NUGET_API_KEY = Argument ("nuget-api-key", EnvironmentVariable ("NUGET_API_KEY") ?? "");
var NUGET_SOURCE = Argument ("nuget-source", EnvironmentVariable ("NUGET_SOURCE") ?? "");
var NUGET_PUSH_SOURCE = Argument ("nuget-push-source", EnvironmentVariable ("NUGET_PUSH_SOURCE") ?? "");

var MYGET_API_KEY = Argument ("myget-api-key", EnvironmentVariable ("MYGET_API_KEY") ?? "");
var MYGET_SOURCE = Argument ("myget-source", EnvironmentVariable ("MYGET_SOURCE") ?? "");
var MYGET_PUSH_SOURCE = Argument ("myget-push-source", EnvironmentVariable ("MYGET_PUSH_SOURCE") ?? "");

var GLOB_PATTERNS = Argument ("glob-patterns", EnvironmentVariable ("GLOBBER_FILE_PATTERNS"));

public partial class BuildManifest
{
	public string Url { get; set; }
	public string Sha256 { get; set; }
	public long Size { get; set; }
}

Func<FilePath, string> Sha256File = (FilePath file) =>
{
    var crypt = new System.Security.Cryptography.SHA256Managed();
    string hash = String.Empty;
	using (var stream = System.IO.File.OpenRead(MakeAbsolute(file).FullPath)) {
		var hashBytes = crypt.ComputeHash(stream);
		foreach (byte theByte in hashBytes)
        	hash += theByte.ToString("x2");	
	}
    return hash;
};

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
        
        if (!downloadedFile.GetExtension().Equals(".nupkg", StringComparison.InvariantCultureIgnoreCase)) {
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

Task ("VerifyAuthenticode")
	.IsDependentOn ("DownloadArtifacts")
	.Does (() => 
{
	var globPatterns = (GLOB_PATTERNS ?? "./output/*.nupkg").Split (new [] { ',', ';', ' ' });

	DumpGlobPatterns (globPatterns);

	foreach (var globPattern in globPatterns) {
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
						Arguments = "verify /pa \"" + MakeAbsolute(assembly).FullPath + "\"",
						RedirectStandardOutput = true,
					}, out stdout);
					stdoutput = string.Join(" ", stdout);
					verified = stdoutput.Contains ("Successfully verified");
				} else {
					StartProcess ("/Library/Frameworks/Mono.framework/Versions/Current/Commands/chktrust", new ProcessSettings {
						Arguments = "\"" + MakeAbsolute(assembly).FullPath + "\"",
						RedirectStandardOutput = true,
					}, out stdout);
					stdoutput = string.Join(" ", stdout);
					verified = !stdoutput.Contains ("doesn't contain a digital signature");
				}
				Information (" -> {0}", assembly.GetFilename());
				Information (" -> {0}", stdoutput);

				if (!verified)
					throw new Exception (string.Format("Missing Authenticode Signature found in {0} for {1}", assembly.GetFilename(), nupkgFile.GetFilename()));
			}
		}
	}
});

Task ("MyGet")
	.IsDependentOn("VerifyAuthenticode")
	.Does (() =>
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

Task ("NuGet")
	.IsDependentOn("VerifyAuthenticode")
	.Does (() =>
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

RunTarget (TARGET);

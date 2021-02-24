#addin nuget:?package=Cake.XCode&version=4.2.0
#addin nuget:?package=Cake.Yaml&version=3.1.0&loadDependencies=true
#addin nuget:?package=Cake.Json&version=4.0.0&loadDependencies=true
#addin nuget:?package=Xamarin.Nuget.Validator&version=1.1.1
#addin nuget:?package=redth.xunit.resultwriter&version=1.0.0


using Xamarin.Nuget.Validator;
using System.Runtime.InteropServices;


// SECTION: Arguments and Settings

var VERBOSITY = Argument ("v", Argument ("verbosity", Verbosity.Normal));
var CONFIGURATION = Argument ("c", Argument ("configuration", "Release"));

var GIT_PREVIOUS_COMMIT = Argument ("gitpreviouscommit", "");
var GIT_COMMIT = Argument ("gitcommit", "");
var GIT_BRANCH = Argument ("gitbranch", "origin/master");
var GIT_EXE = Argument ("gitexe", "git");

var ROOT_DIR = (DirectoryPath)Argument ("root", ".");

var ROOT_OUTPUT_DIR = ROOT_DIR.Combine ("output");
var COPY_OUTPUT_TO_ROOT = Argument ("copyoutputtoroot", false);

var MANIFEST_YAML = (FilePath)Argument ("manifest", ROOT_DIR.CombineWithFilePath ("manifest.yaml").FullPath);
var BUILD_GROUPS = DeserializeYamlFromFile<List<BuildGroup>> (MANIFEST_YAML);

var BUILD_NAMES = Argument ("names", Argument ("name", Argument ("n", "")))
	.Split (new [] { ",", ";" }, StringSplitOptions.RemoveEmptyEntries);
var BUILD_TARGETS = Argument ("targets", Argument ("build-targets", Argument ("build-targets", Argument ("build", ""))))
	.Split (new [] { ",", ";" }, StringSplitOptions.RemoveEmptyEntries);

var FORCE_BUILD = Argument ("force", Argument ("forcebuild", Argument ("force-build", false)));
var POD_REPO_UPDATE = Argument ("update", Argument ("repo-update", Argument ("pod-repo-update", false)));


// SECTION: Main Script

Information ("##vso[task.setprogress value=0;]Starting script...");

Information ("");
Information ("Script Arguments:");
Information ("  Previous commit: {0}", GIT_PREVIOUS_COMMIT);
Information ("  Current commit: {0}", GIT_COMMIT);
Information ("  Current branch: {0}", GIT_BRANCH);
Information ("  Root directory: {0}", MakeAbsolute (ROOT_DIR));
Information ("  Path to manifest.yaml: {0}", MakeAbsolute (MANIFEST_YAML));
Information ("  Build names: {0}", string.Join (", ", BUILD_NAMES));
Information ("  Build targets: {0}", string.Join (", ", BUILD_TARGETS));
Information ("  Force build of all items: {0}", FORCE_BUILD);
Information ("  Copy build output to root: {0}", COPY_OUTPUT_TO_ROOT);
Information ("  Root output directory: {0}", ROOT_OUTPUT_DIR);
Information ("  Update Cocoapods repository: {0}", POD_REPO_UPDATE);
Information ("");

// Tagged build might contain the group name to build specifically if no setting was specified
if (GIT_BRANCH.StartsWith ("refs/tags/") && !BUILD_NAMES.Any ()) {
	var tagName = GIT_BRANCH.Substring (10);
	Information ("Trying to build tag: {0}", tagName);

	var buildName = tagName;
	if (tagName.Contains ('-'))
		buildName = tagName.Substring (0, tagName.IndexOf ('-'));

	// If we found a build name from the tag name, let's force a build
	if (!string.IsNullOrWhiteSpace (buildName)) {
		Information ("Going to be building tagged item: {0}", buildName);

		BUILD_NAMES = new string[] { buildName };
		FORCE_BUILD = true;
	}
}

// Determine which groups to build
var groupsToBuild = new List<BuildGroup> ();
var podRepoUpdate = POD_REPO_UPDATE ? PodRepoUpdate.Forced : PodRepoUpdate.NotRequired;
if (FORCE_BUILD) {
	Information ("Forcing a build of all the items...");
	Information ("");

	podRepoUpdate = PodRepoUpdate.Forced;
	groupsToBuild.AddRange (BUILD_GROUPS);
	Debug ("Found {0} items to build:" + Environment.NewLine +
		" - " + string.Join (Environment.NewLine + " - ", BUILD_GROUPS),
		BUILD_GROUPS.Count);
	Debug ("");
} else {
	Information ("Determining which items to build based on the changes...");

	// If we weren't supplied a git hash, then get the current one
	if (string.IsNullOrWhiteSpace (GIT_COMMIT)) {
		Information ("Git commit was not specified, trying to determine one...");
		try {
			GIT_COMMIT = GetGitOutput ("rev-parse --verify HEAD")[0];
		} catch (Exception ex) {
			throw new Exception ("Unable to determine current git hash.", ex);
		}
		Information ("Found git commit: {0}", GIT_COMMIT);
	}

	string gitArgs;
	if (!string.IsNullOrWhiteSpace (GIT_PREVIOUS_COMMIT)) {
		Information ("Previous git specified...");
		gitArgs = $"--no-pager diff --name-only {GIT_PREVIOUS_COMMIT} {GIT_COMMIT}";
	} else {
		Information ("No previous git specified, using the changes in the current commit...");
		gitArgs = $"--no-pager show --pretty=\"format:\" --name-only {GIT_COMMIT}";
	}

	// Get all the changed files between these commits
	var changedFilesRaw = GetGitOutput (gitArgs);
	var changedFiles = new FilePathCollection (changedFilesRaw.Select (f => (FilePath)f));

	Debug ("Found {0} changed file(s):" + Environment.NewLine +
		" - " + string.Join (Environment.NewLine + " - ", changedFilesRaw),
		changedFilesRaw.Length);

	// Determine which group each file belongs to
	var matchedFiles = new Dictionary<BuildGroup, FilePathCollection> ();
	foreach (var buildGroup in BUILD_GROUPS) {
		// If ignore triggers for the platform this is running on, do not add the group even if the trigger is matched
		if ((buildGroup.IgnoreTriggersOnLinux && IsRunningOnLinux ()) ||
			(buildGroup.IgnoreTriggersOnMac && IsRunningOnMacOs ()) ||
			(buildGroup.IgnoreTriggersOnWindows && IsRunningOnWindows ()))
			continue;

		foreach (var file in changedFiles) {
			if (podRepoUpdate == PodRepoUpdate.NotRequired && file.GetFilename ().ToString () == "Podfile")
				podRepoUpdate = PodRepoUpdate.Required;

			foreach (var triggerPath in buildGroup.TriggerPaths) {
				var relative = MakeAbsolute (triggerPath).GetRelativePath (MakeAbsolute (file));
				if (relative.FullPath.StartsWith (".."))
					continue;

				if (!matchedFiles.TryGetValue (buildGroup, out var match))
					matchedFiles[buildGroup] = match = new FilePathCollection ();
				if (!match.Contains (file))
					match.Add (file);
				if (!groupsToBuild.Contains (buildGroup))
					groupsToBuild.Add (buildGroup);

				break;
			}
			
			foreach (var triggerFile in buildGroup.TriggerFiles) {
				if (file.FullPath != triggerFile.FullPath)
					continue;

				if (!matchedFiles.TryGetValue (buildGroup, out var match))
					matchedFiles[buildGroup] = match = new FilePathCollection ();
				if (!match.Contains (file))
					match.Add (file);
				if (!groupsToBuild.Contains (buildGroup))
					groupsToBuild.Add (buildGroup);

				break;
			}
		}
	}

	Information ("");
	Information ("Found {0} changed file(s) that will trigger {1} build(s):",
		matchedFiles.Values.Sum (m => m.Count),
		matchedFiles.Keys.Count);
	foreach (var match in matchedFiles) {
		var platforms = new List<string> ();
		if (match.Key.BuildOnWindows)
			platforms.Add ("windows");
		if (match.Key.BuildOnMac)
			platforms.Add ("mac");
		if (match.Key.BuildOnLinux)
			platforms.Add ("linux");
		Information ($" - {{0}} ({string.Join (" and ", platforms)})", match.Key);
		Information ("    - " + string.Join (Environment.NewLine + "    - ", match.Value));
	}

	var extraFiles = changedFiles - new FilePathCollection (matchedFiles.SelectMany (m => m.Value));
	if (extraFiles.Count > 0) {
		Information ("Found {0} changed file(s) that did not match any item in the manifest:" + Environment.NewLine +
			" - " + string.Join (Environment.NewLine + " - ", extraFiles),
			extraFiles.Count);
	}
	Information ("");
}

// If a filter was specified, then use it
if (BUILD_NAMES.Length > 0) {
	groupsToBuild = groupsToBuild
		.Where (bg => BUILD_NAMES.Any (n => n.Equals (bg.Name, StringComparison.OrdinalIgnoreCase)))
		.ToList ();
	if (groupsToBuild.Count > 0) {
		Information ("Received a filter, so reducing the items to build, leaving:" + Environment.NewLine +
			" - " + string.Join (Environment.NewLine + " - ", groupsToBuild));
	} else {
		Information ("Received a filter, so reducing the items to build...");
	}
	Information ("");
}

// Remove the builds that cannot run on this platform
groupsToBuild = groupsToBuild
	.Where (bg => (bg.BuildOnWindows && IsRunningOnWindows ()) || (bg.BuildOnMac && IsRunningOnMacOs ()) || (bg.BuildOnLinux && IsRunningOnLinux ()))
	.ToList ();
if (groupsToBuild.Count > 0) {
	Information ("Removed the items that cannot build on this platform, leaving:" + Environment.NewLine +
		" - " + string.Join (Environment.NewLine + " - ", groupsToBuild));
} else {
	Information ("Removing the items that cannot build on this platform...");
}
Information ("");


// SECTION: Build

Information ("##vso[task.setprogress value=5;]Beginning main build...");

var buildExceptions = new List<Exception> ();

if (groupsToBuild.Count == 0) {
	// Make a note if nothing changed...
	Warning ("No changed files affected any of the paths from the manifest.yaml.");
} else {
	if (IsRunningOnMacOs ()) {
		// Make sure cocoapods are up to date if needed
		if (podRepoUpdate != PodRepoUpdate.NotRequired) {
			if (podRepoUpdate == PodRepoUpdate.Forced)
				Information ("Forcing Cocoapods repo update...");
			else
				Information ("A modified Podfile was found, updating the Cocoapods repo...");

			CocoaPodRepoUpdate ();

			Information ("Cocoapods repo update complete.");
			Information ("");
		}
	}

	Information ("################################################################################");
	Information ("#                       STARTING INDIVIDUAL ITEMS BUILD                        #");
	Information ("################################################################################");
	Information ("");

	// Prepare the test output
	var assembly = new Xunit.ResultWriter.Assembly {
		Name = "Components",
		TestFramework = "xUnit",
		Environment = "Components",
		RunDate = DateTime.UtcNow.ToString("yyyy-MM-dd"),
		RunTime = DateTime.UtcNow.ToString("hh:mm:ss")
	};
	var col = new Xunit.ResultWriter.Collection {
		Name = "Components"
	};
	assembly.CollectionItems.Add(col);

	// Build is between 5 and 95 (non-inclusive)
	var percentStep = 85.0 / groupsToBuild.Count;
	var percent = 5.0;

	// Build each group
	foreach (var buildGroup in groupsToBuild) {
		// Determine the targets to build
		List<string> targets;
		if (BUILD_TARGETS.Length > 0)
			targets = BUILD_TARGETS.ToList ();
		else if (IsRunningOnWindows ())
			targets = buildGroup.WindowsBuildTargets.ToList ();
		else if (IsRunningOnMacOs ())
			targets = buildGroup.MacBuildTargets.ToList ();
		else if (IsRunningOnLinux ())
			targets = buildGroup.LinuxBuildTargets.ToList ();
		else
			throw new Exception ("Unable to determine the target to build.");

		var smallStep = percentStep / ((targets.Count * 2) + 1);

		Information ("================================================================================");
		Information (buildGroup.Name);
		Information ("================================================================================");
		Information ("");

		Information (@"Building ""{0}"" ({1}) with Targets: {2}...",
			buildGroup.Name,
			buildGroup.BuildScript,
			string.Join (", ", targets));
		foreach (var target in targets) {
			// Update DevOps
			percent += smallStep;
			Information ($"##vso[task.setprogress value={percent};]Building {buildGroup.Name} ({target})...");

			// Create a test run for this build
			var test = new Xunit.ResultWriter.Test {
				Name = buildGroup.Name,
				Type = "Components",
				Method = $"{buildGroup.Name} ({target})",
			};
			var start = DateTime.UtcNow;

			try {
				// Run the actual build
				var cakeSettings = new CakeSettings {
					Arguments = new Dictionary<string, string> {
						{ "target", target },
						{ "configuration", CONFIGURATION },
					},
					Verbosity = VERBOSITY
				};
				CakeExecuteScript (ROOT_DIR.CombineWithFilePath (buildGroup.BuildScript), cakeSettings);

				// The build was a success
				test.Result = Xunit.ResultWriter.ResultType.Pass;
			} catch (Exception ex) {
				// The test was a failure
				test.Result = Xunit.ResultWriter.ResultType.Fail;
				test.Failure = new Xunit.ResultWriter.Failure {
					Message = ex.Message,
					StackTrace = ex.ToString()
				};

				// Record that failure so we can throw later
				buildExceptions.Add (ex);

				// Update DevOps
				Warning ($"##vso[task.logissue type=warning]Failed to build {buildGroup.Name} ({target}).");
			}

			// Add the test run to the collection
			test.Time = (decimal)(DateTime.UtcNow - start).TotalSeconds;
			col.TestItems.Add(test);

			// Update DevOps
			percent += smallStep;
			Information ($"##vso[task.setprogress value={percent};]Build of target {target} for {buildGroup.Name} completed.");
		}
		Information ("");

		// Update DevOps
		percent += smallStep;
		Information ($"##vso[task.setprogress value={percent};]Build of {buildGroup.Name} completed.");
	}

	// Write the test output
	var testsDir = ROOT_OUTPUT_DIR.Combine ("tests");
	EnsureDirectoryExists (testsDir);
	var resultWriter = new Xunit.ResultWriter.XunitV2Writer();
	resultWriter.Write(
		new List<Xunit.ResultWriter.Assembly> { assembly },
		testsDir.CombineWithFilePath ("ManifestBuildTestResults.xml").FullPath);

	Information ("################################################################################");
	Information ("#                             ALL BUILDS COMPLETE                              #");
	Information ("################################################################################");
	Information ("");
}


// SECTION: Copy Output

Information ("##vso[task.setprogress value=95;]Finishing build...");

// Log all the things that were found after a build
var artifacts = GetFiles ($"{ROOT_DIR}/*/**/output/**/*");
Information ("Found {0} Artifacts:" + Environment.NewLine +
	" - " + string.Join (Environment.NewLine + " - ", artifacts),
	artifacts.Count);
Information ("");

// Copy all child "output" directories to a root level artifacts dir
if (COPY_OUTPUT_TO_ROOT) {
	Information ("Copying all {0} artifacts to the root output directory...", artifacts.Count);
	EnsureDirectoryExists (ROOT_OUTPUT_DIR);
	var dirs = GetDirectories ($"{ROOT_DIR}/*/**/output");
	foreach (var dir in dirs) {
		Information ("Copying {0}...", dir);
		CopyDirectory (dir, ROOT_OUTPUT_DIR);
	}
	Information ("Copy complete.");
}
Information ("");


// SECTION: Clean up

// There were exceptions, so throw them now
if (buildExceptions.Count > 0) {
	throw new AggregateException (buildExceptions);
}

Information ("##vso[task.setprogress value=100;]Build complete.");

// SECTION: Helper Methods and Types

public enum PodRepoUpdate {
	NotRequired,
	Required,
	Forced
}

public class BuildGroup {
	public string Name { get; set; }
	public FilePath BuildScript { get; set; }
	public List<DirectoryPath> TriggerPaths { get; set; } = new List<DirectoryPath> ();
	public List<FilePath> TriggerFiles { get; set; } = new List<FilePath> ();
	public bool IgnoreTriggersOnMac { get; set; } = false;
	public bool IgnoreTriggersOnLinux { get; set; } = false;
	public bool IgnoreTriggersOnWindows { get; set; } = false;
	public List<string> WindowsBuildTargets { get; set; } = new List<string> ();
	public List<string> MacBuildTargets { get; set; } = new List<string> ();
	public List<string> LinuxBuildTargets { get; set; } = new List<string> ();

	public bool BuildOnWindows => WindowsBuildTargets?.Any () == true;
	public bool BuildOnMac => MacBuildTargets?.Any () == true;
	public bool BuildOnLinux => LinuxBuildTargets?.Any () == true;

	public override string ToString () => Name;
}

bool IsRunningOnMacOs () {
	return System.Environment.OSVersion.Platform == PlatformID.MacOSX || MacPlatformDetector.IsMac.Value;
}

bool IsRunningOnLinux () {
	return IsRunningOnUnix () && !IsRunningOnMacOs ();
}

internal static class MacPlatformDetector {
	internal static readonly Lazy<bool> IsMac = new Lazy<bool> (IsRunningOnMac);

	[DllImport ("libc")]
	static extern int uname (IntPtr buf);

	static bool IsRunningOnMac () {
		IntPtr buf = IntPtr.Zero;
		try {
			buf = Marshal.AllocHGlobal (8192);
			// This is a hacktastic way of getting sysname from uname()
			if (uname (buf) == 0) {
				string os = Marshal.PtrToStringAnsi (buf);
				if (os == "Darwin")
					return true;
			}
		} catch {
		} finally {
			if (buf != IntPtr.Zero)
				Marshal.FreeHGlobal (buf);
		}
		return false;
	}
}

string[] GetGitOutput (string args) {
	var settings = new ProcessSettings {
		Arguments = args,
		RedirectStandardOutput = true
	};

	var exitCode = StartProcess (GIT_EXE, settings, out var changedFiles);
	if (exitCode != 0)
		throw new Exception ($"git exited with error code {exitCode}.");
	
	return changedFiles.ToArray ();
}

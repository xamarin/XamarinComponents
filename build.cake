#load "common.cake"

#addin nuget:?package=redth.xunit.resultwriter&version=1.0.0
#addin nuget:?package=Xamarin.Nuget.Validator&version=1.1.1

var TARGET = Argument ("target", Argument ("t", Argument ("Target", "build")));

var GIT_PREVIOUS_COMMIT = EnvironmentVariable ("GIT_PREVIOUS_SUCCESSFUL_COMMIT") ?? Argument ("gitpreviouscommit", "");
var GIT_COMMIT = EnvironmentVariable ("GIT_COMMIT") ?? EnvironmentVariable("BUILD_SOURCEVERSION") ?? Argument("gitcommit", "");
var GIT_BRANCH = EnvironmentVariable ("GIT_BRANCH") ?? EnvironmentVariable("BUILD_SOURCEBRANCH") ?? Argument("gitbranch", "origin/master");
var GIT_PATH = EnvironmentVariable ("GIT_EXE") ?? Argument("gitexe", "git");

var BUILD_GROUPS = DeserializeYamlFromFile<List<BuildGroup>> ("./manifest.yaml");
var BUILD_NAMES = Argument ("names", Argument ("name", Argument ("n", "")))
	.Split (new [] { ",", ";" }, StringSplitOptions.RemoveEmptyEntries)
	.Where (x => !string.IsNullOrWhiteSpace (x))
	.ToArray ();
var BUILD_TARGETS = Argument ("targets", Argument ("build-targets", Argument ("build-targets", Argument ("build", ""))))
	.Split (new [] { ",", ";" }, StringSplitOptions.RemoveEmptyEntries)
	.Where (x => !string.IsNullOrWhiteSpace (x))
	.ToArray ();

var COPY_OUTPUT_TO_ROOT = Argument("copyoutputtoroot", "false").ToLower().Equals("true");

var FORCE_BUILD = Argument ("force", Argument ("forcebuild", Argument ("force-build", "false"))).ToLower ().Equals ("true");

var POD_REPO_UPDATE = Argument ("update", Argument ("repo-update", Argument ("pod-repo-update", false)));

var CAKE_EXE = Context.Tools.Resolve ("Cake.exe");

var runningOnMac = IsRunningOnUnix ();
var runningOnWin = IsRunningOnWindows ();

// Print out environment variables to console
var ENV_VARS = EnvironmentVariables ();
Information ("Environment Variables: {0}", "");
foreach (var ev in ENV_VARS)
	Information ("\t{0} = {1}", ev.Key, ev.Value);

CakeStealer.CakeContext = Context;

// Print out git commit info
Information ("Git Path: {0}", GIT_PATH);
Information ("Git Previous Commit: {0}", GIT_PREVIOUS_COMMIT);
Information ("Git Commit: {0}", GIT_COMMIT);
Information ("Git Branch: {0}", GIT_BRANCH);
Information ("Force Build: {0}", FORCE_BUILD);
Information ("Running on: Mac? {0} Windows? {1}", runningOnMac, runningOnWin);

public enum PodRepoUpdate {
	NotRequired,
	Required,
	Forced
}

public class CakeStealer
{
	static public ICakeContext CakeContext { get; set; }
}

public class BuildGroup
{
	public BuildGroup ()
	{
		Name = string.Empty;
		TriggerPaths = new List<string> ();
		TriggerFiles = new List<string> ();
		WindowsBuildTargets = new List<string> ();
		MacBuildTargets = new List<string> ();
		IgnoreTriggersOnMac = false;
		IgnoreTriggersOnWindows = false;
	}

	public string Name { get; set; }
	public string BuildScript { get; set; }
	public List<string> TriggerPaths { get; set; }
	public List<string> TriggerFiles { get; set; }
	public bool IgnoreTriggersOnMac { get; set; }
	public bool IgnoreTriggersOnWindows { get; set; }
	public List<string> WindowsBuildTargets { get; set; }
	public List<string> MacBuildTargets { get; set; }

	public bool BuildOnWindows { get { return WindowsBuildTargets != null && WindowsBuildTargets.Any (); } }
	public bool BuildOnMac { get { return MacBuildTargets != null && MacBuildTargets.Any (); } }

	public List<string> BuildTargets {
		get {
			return CakeStealer.CakeContext.IsRunningOnWindows () ? WindowsBuildTargets : MacBuildTargets;
		}
	}

	public override string ToString ()
	{
		return Name;
	}
}

Task ("build")
	.Does (() =>
{
	// Tagged build might contain the group name to build specifically if no setting was specified
	if (GIT_BRANCH.StartsWith("refs/tags/") && !BUILD_NAMES.Any()) {
		var tagName = GIT_BRANCH.Substring(10);
		Information ("Trying to build tag: {0}", tagName);

		var buildName = tagName;
		if (tagName.Contains('-'))
			buildName = tagName.Substring(0, tagName.IndexOf('-'));

		// If we found a build name from the tag name, let's force a build
		if (!string.IsNullOrWhiteSpace(buildName)) {
			Information ("Going to be building tagged item: {0}", buildName);

			BUILD_NAMES = new string[] { buildName };
			FORCE_BUILD = true;
		}
	}

	// Determine groups to build
	var groupsToBuild = new List<BuildGroup> ();
	var podRepoUpdate = POD_REPO_UPDATE ? PodRepoUpdate.Forced : PodRepoUpdate.NotRequired;
	if (FORCE_BUILD) {
		podRepoUpdate = PodRepoUpdate.Forced;
		groupsToBuild.AddRange (BUILD_GROUPS);
		Information ("Groups To Build: {0}", string.Join (", ", BUILD_GROUPS));
	} else {
		// If neither commit hash was found, we can't detect changes
		if (string.IsNullOrWhiteSpace (GIT_PREVIOUS_COMMIT) && string.IsNullOrWhiteSpace (GIT_COMMIT))
			throw new CakeException ("GIT Commit/Previous Commit hashes were invalid, can't find affected files.");

		string gitArgs;
		if (!string.IsNullOrWhiteSpace (GIT_PREVIOUS_COMMIT))
			// We have both commit hashes (previous and current) so do a diff on them
			gitArgs = $"--no-pager diff --name-only {GIT_PREVIOUS_COMMIT} {GIT_COMMIT}";
		else
			// We only have the current commit hash, so list files for this commit only
			gitArgs = $"--no-pager show --pretty=\"format:\" --name-only {GIT_COMMIT}";

		// Get all the changed files in this commit
		IEnumerable<string> changedFiles = new List<string> ();
		var exitCode = StartProcess (GIT_PATH, new ProcessSettings {
			Arguments = gitArgs,
			RedirectStandardOutput = true },
			out changedFiles);
		if (exitCode != 0)
			throw new Exception($"git exited with error code {exitCode}.");

		// Determine which group each file belongs to
		Information ("Changed Files:");
		foreach (var file in changedFiles) {
			Information ("\t{0}", file);
			if (podRepoUpdate == PodRepoUpdate.NotRequired && file.EndsWith ("Podfile"))
				podRepoUpdate = PodRepoUpdate.Required;

			foreach (var buildGroup in BUILD_GROUPS) {
				// If ignore triggers for the platform this is running on, do not add the group even if the trigger is matched
				if ((buildGroup.IgnoreTriggersOnMac && runningOnMac) || (buildGroup.IgnoreTriggersOnWindows && runningOnWin))
					continue;

				foreach (var triggerPath in buildGroup.TriggerPaths) {
					if (file.StartsWith (triggerPath.ToString ())) {
						Information ("\t\tMatched: {0}", triggerPath);
						if (!groupsToBuild.Contains (buildGroup))
							groupsToBuild.Add (buildGroup);
						break;
					}
				}
				foreach (var triggerFile in buildGroup.TriggerFiles) {
					if (file.Equals (triggerFile.ToString ())) {
						Information ("\t\tMatched: {0}", triggerFile);
						if (!groupsToBuild.Contains (buildGroup))
							groupsToBuild.Add (buildGroup);
						break;
					}
				}
			}
		}
	}

	// If a filter was specified, then use it
	if (BUILD_NAMES.Any ()) {
		Information ("Only building groups: {0}", string.Join (",", BUILD_NAMES));
		groupsToBuild = groupsToBuild.Where (bg => BUILD_NAMES.Any (n => n.ToLower ().Trim () == bg.Name.ToLower ().Trim ())).ToList ();
	}

	if (!groupsToBuild.Any ()) {
		// Make a note if nothing changed...
		Warning ("No changed files affected any of the paths from the manifest.yaml! {0}", "Skipping Builds");
	} else {
		// Logging about the jobs and what platforms they can run on
		foreach (var gtb in groupsToBuild)
			Information ("{0} Build on: Mac? {1}  Win? {2}", gtb.Name, gtb.BuildOnMac, gtb.BuildOnWindows);

		// Filter out any groups that can't/shouldn't be built on this platform
		groupsToBuild = groupsToBuild.Where (bg => (bg.BuildOnMac && runningOnMac) || (bg.BuildOnWindows && runningOnWin)).ToList ();

		// Replace build targets with custom specified build targets if any were specified
		if (BUILD_TARGETS.Any ()) {
			foreach (var buildGroup in groupsToBuild) {
				buildGroup.BuildTargets.Clear ();
				buildGroup.BuildTargets.AddRange (BUILD_TARGETS);
			}
		}

		// Make sure cocoapods are up to date if needed
		if (podRepoUpdate != PodRepoUpdate.NotRequired && runningOnMac) {
			string message;
			if (podRepoUpdate == PodRepoUpdate.Forced)
				message = "Forcing Cocoapods repo update...";
			else
				message = "A modified Podfile was found...";

			Information ($"{message}\nUpdating Cocoapods repo...");
			CocoaPodRepoUpdate ();
			Information ("Cocoapods repo update complete.");
		}

		// Build each group
		foreach (var buildGroup in groupsToBuild) {
			Information ("Building {0} with Targets {1}...", buildGroup.Name, string.Join (",", buildGroup.BuildTargets));
			foreach (var target in buildGroup.BuildTargets) {
				var cakeSettings = new CakeSettings {
					ToolPath = CAKE_EXE,
					Arguments = new Dictionary<string, string> { { "target", target } },
					Verbosity = Verbosity.Diagnostic
				};
				CakeExecuteScript (buildGroup.BuildScript, cakeSettings);
			}
		}
	}

	// Log all the things that were found after a build
	var artifacts = GetFiles ("./**/output/**/*");
	Information ("Found {0} Artifacts:", artifacts.Count);
	foreach (var a in artifacts)
		Information ("\t{0}", a);
	var dlls = GetFiles ("./**/*.dll") - GetFiles ("./tools/**/*.dll");
	Information ("Found {0} DLL's:", dlls.Count);
	foreach (var d in dlls)
		Information ("\t{0}", d);

	// Copy all child "output" directories to a root level artifacts dir
	if (COPY_OUTPUT_TO_ROOT) {
		EnsureDirectoryExists("./artifacts");
		CopyFiles("./**/output/*", "./artifacts", false);
	}
});

Task ("buildall")
	.Does (() =>
{
	// If BUILD_NAMES were specified, only take BUILD_GROUPS that match one of the specified names, otherwise, all
	var groupsToBuild = BUILD_NAMES.Any ()
		? BUILD_GROUPS.Where (i => BUILD_NAMES.Contains (i.Name))
		: BUILD_GROUPS;

	var assembly = new Xunit.ResultWriter.Assembly {
		Name = "ComponentsBuilder",
		TestFramework = "xUnit",
		Environment = "CI",
		RunDate = DateTime.UtcNow.ToString("yyyy-MM-dd"),
		RunTime = DateTime.UtcNow.ToString("hh:mm:ss")
	};

	var col = new Xunit.ResultWriter.Collection {
		Name = "Components"
	};

	foreach (var bg in groupsToBuild) {
		foreach (var t in bg.BuildTargets) {
			var test = new Xunit.ResultWriter.Test {
				Name = bg.BuildScript,
				Type = "ComponentsBuilder",
				Method = "Build (" + t + ")",
			};

			var start = DateTime.UtcNow;

			try {
				CakeExecuteScript(bg.BuildScript, new CakeSettings {
					Arguments = new Dictionary<string, string> { { "--target", t } }
				});
				test.Result = Xunit.ResultWriter.ResultType.Pass;
			} catch (Exception ex) {
				test.Result = Xunit.ResultWriter.ResultType.Fail;
				test.Failure = new Xunit.ResultWriter.Failure {
					Message = ex.Message,
					StackTrace = ex.ToString()
				};
			}

			test.Time = (decimal)(DateTime.UtcNow - start).TotalSeconds;
			col.TestItems.Add(test);
		}
	}

	assembly.CollectionItems.Add(col);

	var resultWriter = new Xunit.ResultWriter.XunitV2Writer();
	resultWriter.Write(
		new List<Xunit.ResultWriter.Assembly> { assembly },
		MakeAbsolute(new FilePath("./xunit.xml")).FullPath);
});

Task("nuget-validation")
	.Does(()=>
{
	//setup validation options
	var options = new Xamarin.Nuget.Validator.NugetValidatorOptions {
		Copyright = "© Microsoft Corporation. All rights reserved.",
		Author = "Microsoft",
		Owner = "Microsoft",
		NeedsProjectUrl = true,
		NeedsLicenseUrl = true,
		ValidateRequireLicenseAcceptance = true,
		ValidPackageNamespace = new [] { "Xamarin", "Mono", "SkiaSharp", "HarfBuzzSharp", "mdoc", "Masonry" },
	};

	var nupkgFiles = GetFiles ("./**/output/*.nupkg");
	Information ("Found ({0}) Nuget's to validate", nupkgFiles.Count ());
	foreach (var nupkgFile in nupkgFiles)
	{
		Information ("Verifiying Metadata of {0}", nupkgFile.GetFilename ());
		var result = Xamarin.Nuget.Validator.NugetValidator.Validate(MakeAbsolute(nupkgFile).FullPath, options);
		if (!result.Success) {
			Information ("Metadata validation failed for: {0} \n\n", nupkgFile.GetFilename ());
			Information (string.Join("\n    ", result.ErrorMessages));
			throw new Exception ($"Invalid Metadata for: {nupkgFile.GetFilename ()}");
		} else {
			Information ("Metadata validation passed for: {0}", nupkgFile.GetFilename ());
		}
	}
});

RunTarget (TARGET);

// DUMMY

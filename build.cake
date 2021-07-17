var VERBOSITY = Argument ("v", Argument ("verbosity", Verbosity.Normal));
var CONFIGURATION = Argument ("c", Argument ("configuration", "Release"));

var BUILD_NAMES = Argument ("names", Argument ("name", ""));
var BUILD_TARGETS = Argument ("build-targets", Argument ("targets", Argument ("target", "ci")));

var FORCE_BUILD = Argument ("force", Argument ("forcebuild", Argument ("force-build", false)));
var POD_REPO_UPDATE = Argument ("repo-update", Argument ("pod-repo-update", false));

var ROOT_DIR = MakeAbsolute((DirectoryPath)".");

var COPY_OUTPUT_TO_ROOT = Argument ("copyoutputtoroot", true);
var ROOT_OUTPUT_DIR = ROOT_DIR.Combine ("output");

if (string.IsNullOrEmpty (BUILD_NAMES))
	Warning ("No items were specified, building everything. Use the --names=<comma-separated-names> argument to build a specific item.");

var cakeSettings = new CakeSettings {
	Arguments = new Dictionary<string, string> {
		{ "configuration", CONFIGURATION },
		{ "copyoutputtoroot", COPY_OUTPUT_TO_ROOT.ToString () },
		{ "root", ROOT_DIR.FullPath },
		{ "output", ROOT_DIR.Combine ("output").FullPath },
		{ "names", BUILD_NAMES },
		{ "targets", BUILD_TARGETS },
		{ "forcebuild", true.ToString () },
		{ "repo-update", POD_REPO_UPDATE.ToString () },
	},
	Verbosity = VERBOSITY
};

CakeExecuteScript ("./.ci/build-manifest.cake", cakeSettings);


// SECTION: Arguments and Settings

var ROOT_DIR = MakeAbsolute((DirectoryPath)Argument("root", "."));
var ARTIFACTS_DIR = MakeAbsolute((DirectoryPath)Argument("artifacts", ROOT_DIR.Combine("output").FullPath));
var CACHE_DIR = MakeAbsolute((DirectoryPath)Argument("cache", ROOT_DIR.Combine("externals/api-diff").FullPath));
var OUTPUT_DIR = MakeAbsolute((DirectoryPath)Argument("output", ROOT_DIR.Combine("output/api-diff").FullPath));


// SECTION: Main Script

Information("");
Information("Script Arguments:");
Information("  Root directory: {0}", ROOT_DIR);
Information("  Artifacts directory: {0}", ARTIFACTS_DIR);
Information("  Cache directory: {0}", CACHE_DIR);
Information("  Output directory: {0}", OUTPUT_DIR);
Information("");


// SECTION: Diff NuGets

if (!GetFiles($"{ARTIFACTS_DIR}/**/*.nupkg").Any()) {
	Warning($"##vso[task.logissue type=warning]No NuGet packages were found.");
} else {
	var exitCode = StartProcess("api-tools", new ProcessSettings {
		Arguments = new ProcessArgumentBuilder()
			.Append("nuget-diff")
			.AppendQuoted(ARTIFACTS_DIR.FullPath)
			.Append("--latest")
			.Append("--prerelease")
			.Append("--group-ids")
			.Append("--ignore-unchanged")
			.AppendSwitchQuoted("--output", OUTPUT_DIR.FullPath)
			.AppendSwitchQuoted("--cache", CACHE_DIR.Combine("package-cache").FullPath)
	});
	if (exitCode != 0)
		throw new Exception ($"api-tools exited with error code {exitCode}.");
}


// SECTION: Upload Diffs

var diffs = GetFiles($"{OUTPUT_DIR}/**/*.md");
if (!diffs.Any()) {
	Warning($"##vso[task.logissue type=warning]No NuGet diffs were found.");
} else {
	var temp = CACHE_DIR.Combine("md-files");
	EnsureDirectoryExists(temp);

	foreach (var diff in diffs) {
		var segments = diff.Segments.Reverse().ToArray();
		var nugetId = segments[2];
		var platform = segments[1];
		var assembly = ((FilePath)segments[0]).GetFilenameWithoutExtension().GetFilenameWithoutExtension();
		var breaking = segments[0].EndsWith(".breaking.md");

		// using non-breaking spaces
		var newName = breaking ? "[BREAKING]   " : "";
		newName += $"{nugetId}    {assembly} ({platform}).md";
		var newPath = temp.CombineWithFilePath(newName);

		CopyFile(diff, newPath);
	}

	var temps = GetFiles($"{temp}/**/*.md");
	foreach (var t in temps.OrderBy(x => x.FullPath)) {
		Information($"##vso[task.uploadsummary]{t}");
	}
}

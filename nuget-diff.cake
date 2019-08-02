
#addin nuget:?package=Cake.FileHelpers&version=3.2.0
#addin nuget:?package=Mono.ApiTools.NuGetDiff&version=1.0.1&loaddependencies=true

using NuGet.Packaging;
using NuGet.Versioning;
using Mono.ApiTools;

Task ("Default")
	.Does (async () =>
{
	var nupkgFiles = GetFiles ("./artifacts/*.nupkg");
	Information ("Found ({0}) NuGet's to Diff", nupkgFiles.Count);

	foreach (var nupkg in nupkgFiles) {
		using (var reader = new PackageArchiveReader (nupkg.FullPath)) {
			// get the id from the package and the version number
			var packageId = reader.GetIdentity ().Id;
			var currentVersionNo = reader.GetIdentity ().Version.ToNormalizedString();

			// calculate the diff storage path from the location of the nuget
			var diffRoot = $"{nupkg.GetDirectory()}/api-diff/{packageId}";
			CleanDirectories (diffRoot);

			// get the latest version of this package - if any
			var latestVersion = (await NuGetVersions.GetLatestAsync (packageId))?.ToNormalizedString ();

			// log what is going to happen
			if (string.IsNullOrEmpty (latestVersion))
				Information ($"Running a diff on a new package '{packageId}'...");
			else
				Information ($"Running a diff on '{latestVersion}' vs '{currentVersionNo}' of '{packageId}'...");

			// create comparer
			var comparer = new NuGetDiff ();
			comparer.PackageCache = "./externals/package_cache"; // TODO: should this be a variable
			comparer.SaveAssemblyApiInfo = true;       // we don't keep this, but it lets us know if there were no changes
			comparer.SaveAssemblyMarkdownDiff = true;  // we want markdown
			comparer.IgnoreResolutionErrors = true;    // we don't care if frameowrk/platform types can't be found

			await comparer.SaveCompleteDiffToDirectoryAsync (packageId, latestVersion, reader, diffRoot);

			// run the diff with just the breaking changes
			comparer.MarkdownDiffFileExtension = ".breaking.md";
			comparer.IgnoreNonBreakingChanges = true;
			await comparer.SaveCompleteDiffToDirectoryAsync (packageId, latestVersion, reader, diffRoot);

			// TODO: there are two bugs in this version of mono-api-html
			var mdFiles = $"{diffRoot}/*.*.md";
			// 1. the <h4> doesn't look pretty in the markdown
			ReplaceTextInFiles (mdFiles, "<h4>", "> ");
			ReplaceTextInFiles (mdFiles, "</h4>", Environment.NewLine); 
			// 2. newlines are inccorrect on Windows: https://github.com/mono/mono/pull/9918
			ReplaceTextInFiles (mdFiles, "\r\r", "\r");

			// we are done
			Information ($"Diff complete of '{packageId}'.");
		}
	}
});

RunTarget ("Default");

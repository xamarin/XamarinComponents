#tool nuget:?package=XamarinComponent&version=1.1.0.65

#addin nuget:?package=Cake.XCode&version=4.0.0
#addin nuget:?package=Cake.Xamarin.Build&version=4.0.0
#addin nuget:?package=Cake.Xamarin&version=3.0.0
#addin nuget:?package=Cake.FileHelpers&version=3.0.0
#addin nuget:?package=YamlDotNet&version=4.2.1
#addin nuget:?package=Cake.Yaml&version=2.1.0
#addin nuget:?package=Newtonsoft.Json&version=9.0.1
#addin nuget:?package=Cake.Json&version=3.0.1
#addin nuget:?package=Mono.ApiTools.NuGetDiff&version=1.0.1&loaddependencies=true

using Mono.ApiTools;
using NuGet.Packaging;
using NuGet.Versioning;

public enum TargetOS {
	Windows,
	Mac,
	Android,
	iOS,
	tvOS,
}

void BuildXCodeFatLibrary(FilePath xcodeProject, string target, string libraryTitle = null, FilePath fatLibrary = null, DirectoryPath workingDirectory = null, string targetFolderName = null)
{
	BuildXCodeFatLibrary_iOS(xcodeProject, target, libraryTitle, fatLibrary, workingDirectory, targetFolderName);
}

void BuildXCodeFatLibrary_iOS(FilePath xcodeProject, string target, string libraryTitle = null, FilePath fatLibrary = null, DirectoryPath workingDirectory = null, string targetFolderName = null)
{
	if (!IsRunningOnUnix())
	{
		Warning("{0} is not available on the current platform.", "xcodebuild");
		return;
	}

	libraryTitle = libraryTitle ?? target;
	fatLibrary = fatLibrary ?? string.Format("lib{0}.a", libraryTitle);
	workingDirectory = workingDirectory ?? Directory("./externals/");

	var output = string.Format("lib{0}.a", libraryTitle);
	var i386 = string.Format("lib{0}-i386.a", libraryTitle);
	var x86_64 = string.Format("lib{0}-x86_64.a", libraryTitle);
	var armv7 = string.Format("lib{0}-armv7.a", libraryTitle);
	var armv7s = string.Format("lib{0}-armv7s.a", libraryTitle);
	var arm64 = string.Format("lib{0}-arm64.a", libraryTitle);

	var buildArch = new Action<string, string, FilePath>((sdk, arch, dest) => {
		if (!FileExists(dest))
		{
			XCodeBuild(new XCodeBuildSettings
			{
				Project = workingDirectory.CombineWithFilePath(xcodeProject).ToString(),
				Target = target,
				Sdk = sdk,
				Arch = arch,
				Configuration = "Release",
			});
			var tmpOutputPath = workingDirectory.Combine("build").Combine("Release-" + sdk);
			if (!string.IsNullOrEmpty (targetFolderName))
				tmpOutputPath = tmpOutputPath.Combine (targetFolderName);
			var outputPath = tmpOutputPath.CombineWithFilePath(output);

			CopyFile(outputPath, dest);
		}
	});

	buildArch("iphonesimulator", "i386", workingDirectory.CombineWithFilePath(i386));
	buildArch("iphonesimulator", "x86_64", workingDirectory.CombineWithFilePath(x86_64));

	buildArch("iphoneos", "armv7", workingDirectory.CombineWithFilePath(armv7));
	buildArch("iphoneos", "armv7s", workingDirectory.CombineWithFilePath(armv7s));
	buildArch("iphoneos", "arm64", workingDirectory.CombineWithFilePath(arm64));

	RunLipoCreate(workingDirectory, fatLibrary, i386, x86_64, armv7, armv7s, arm64);
}

void BuildXCodeFatLibrary_tvOS(FilePath xcodeProject, string target, string libraryTitle = null, FilePath fatLibrary = null, DirectoryPath workingDirectory = null, string targetFolderName = null)
{
	if (!IsRunningOnUnix())
	{
		Warning("{0} is not available on the current platform.", "xcodebuild");
		return;
	}

	libraryTitle = libraryTitle ?? target;
	fatLibrary = fatLibrary ?? string.Format("lib{0}.a", libraryTitle);
	workingDirectory = workingDirectory ?? Directory("./externals/");

	var output = string.Format("lib{0}.a", libraryTitle);
	var x86_64 = string.Format("lib{0}-x86_64.a", libraryTitle);
	var arm64 = string.Format("lib{0}-arm64.a", libraryTitle);

	var buildArch = new Action<string, string, FilePath>((sdk, arch, dest) => {
		if (!FileExists(dest))
		{
			XCodeBuild(new XCodeBuildSettings
			{
				Project = workingDirectory.CombineWithFilePath(xcodeProject).ToString(),
				Target = target,
				Sdk = sdk,
				Arch = arch,
				Configuration = "Release",
			});
			var tmpOutputPath = workingDirectory.Combine("build").Combine("Release-" + sdk);
			if (!string.IsNullOrEmpty (targetFolderName))
				tmpOutputPath = tmpOutputPath.Combine (targetFolderName);
			var outputPath = tmpOutputPath.CombineWithFilePath(output);

			CopyFile(outputPath, dest);
		}
	});

	buildArch("appletvsimulator", "x86_64", workingDirectory.CombineWithFilePath(x86_64));
	buildArch("appletvos", "arm64", workingDirectory.CombineWithFilePath(arm64));

	RunLipoCreate(workingDirectory, fatLibrary, x86_64, arm64);
}

void BuildXCodeFatLibrary_macOS(FilePath xcodeProject, string target, string libraryTitle = null, FilePath fatLibrary = null, DirectoryPath workingDirectory = null, string targetFolderName = null)
{
	if (!IsRunningOnUnix())
	{
		Warning("{0} is not available on the current platform.", "xcodebuild");
		return;
	}

	// NOTE: 'i386' is no longer supported

	libraryTitle = libraryTitle ?? target;
	fatLibrary = fatLibrary ?? string.Format("lib{0}.a", libraryTitle);
	workingDirectory = workingDirectory ?? Directory("./externals/");

	var output = string.Format("lib{0}.a", libraryTitle);
	// var i386 = string.Format("lib{0}-i386.a", libraryTitle);
	var x86_64 = string.Format("lib{0}-x86_64.a", libraryTitle);

	var buildArch = new Action<string, string, FilePath>((sdk, arch, dest) => {
		if (!FileExists(dest))
		{
			XCodeBuild(new XCodeBuildSettings
			{
				Project = workingDirectory.CombineWithFilePath(xcodeProject).ToString(),
				Target = target,
				Sdk = sdk,
				Arch = arch,
				Configuration = "Release",
			});
			var tmpOutputPath = workingDirectory.Combine("build").Combine("Release");
			if (!string.IsNullOrEmpty (targetFolderName))
				tmpOutputPath = tmpOutputPath.Combine (targetFolderName);
			var outputPath = tmpOutputPath.CombineWithFilePath(output);

			CopyFile(outputPath, dest);
		}
	});

	buildArch("macosx", "x86_64", workingDirectory.CombineWithFilePath(x86_64));
	// buildArch("macosx", "i386", workingDirectory.CombineWithFilePath(i386));

	RunLipoCreate(workingDirectory, fatLibrary, x86_64);
	// RunLipoCreate(workingDirectory, fatLibrary, x86_64, i386);
}

void BuildXCode (FilePath project, string target, string libraryTitle, DirectoryPath workingDirectory, TargetOS os)
{
	if (!IsRunningOnUnix ()) {
		Warning("{0} is not available on the current platform.", "xcodebuild");
		return;
	}
	
	var fatLibrary = string.Format("lib{0}.a", libraryTitle);

	var output = string.Format ("lib{0}.a", libraryTitle);
	var i386 = string.Format ("lib{0}-i386.a", libraryTitle);
	var x86_64 = string.Format ("lib{0}-x86_64.a", libraryTitle);
	var armv7 = string.Format ("lib{0}-armv7.a", libraryTitle);
	var armv7s = string.Format ("lib{0}-armv7s.a", libraryTitle);
	var arm64 = string.Format ("lib{0}-arm64.a", libraryTitle);
	
	var buildArch = new Action<string, string, FilePath> ((sdk, arch, dest) => {
		if (!FileExists (dest)) {
			XCodeBuild (new XCodeBuildSettings {
				Project = workingDirectory.CombineWithFilePath (project).ToString (),
				Target = target,
				Sdk = sdk,
				Arch = arch,
				Configuration = "Release",
			});
			var outputPath = workingDirectory.Combine ("build").Combine (os == TargetOS.Mac ? "Release" : ("Release-" + sdk)).Combine (target).CombineWithFilePath (output);
			CopyFile (outputPath, dest);
		}
	});
	
	if (os == TargetOS.Mac) {
		// not supported anymore
		// buildArch ("macosx", "i386", workingDirectory.CombineWithFilePath (i386));
		buildArch ("macosx", "x86_64", workingDirectory.CombineWithFilePath (x86_64));
		
		if (!FileExists (workingDirectory.CombineWithFilePath (fatLibrary))) {
			RunLipoCreate (workingDirectory, fatLibrary, x86_64);
		}
	} else if (os == TargetOS.iOS) {
		buildArch ("iphonesimulator", "i386", workingDirectory.CombineWithFilePath (i386));
		buildArch ("iphonesimulator", "x86_64", workingDirectory.CombineWithFilePath (x86_64));
		
		buildArch ("iphoneos", "armv7", workingDirectory.CombineWithFilePath (armv7));
		buildArch ("iphoneos", "armv7s", workingDirectory.CombineWithFilePath (armv7s));
		buildArch ("iphoneos", "arm64", workingDirectory.CombineWithFilePath (arm64));
		
		if (!FileExists (workingDirectory.CombineWithFilePath (fatLibrary))) {
			RunLipoCreate (workingDirectory, fatLibrary, i386, x86_64, armv7, armv7s, arm64);
		}
	} else if (os == TargetOS.tvOS) {
		buildArch ("appletvsimulator", "x86_64", workingDirectory.CombineWithFilePath (x86_64));
		
		buildArch ("appletvos", "arm64", workingDirectory.CombineWithFilePath (arm64));
		
		if (!FileExists (workingDirectory.CombineWithFilePath (fatLibrary))) {
			RunLipoCreate (workingDirectory, fatLibrary, x86_64, arm64);
		}
	}
}

void BuildDynamicXCode (FilePath project, string target, string libraryTitle, DirectoryPath workingDirectory, TargetOS os)
{
	if (!IsRunningOnUnix ()) {
		Warning("{0} is not available on the current platform.", "xcodebuild");
		return;
	}
	
	var fatLibrary = (DirectoryPath)string.Format("{0}.framework", libraryTitle);
	var fatLibraryPath = workingDirectory.Combine (fatLibrary);

	var output = (DirectoryPath)string.Format ("{0}.framework", libraryTitle);
	var i386 = (DirectoryPath)string.Format ("{0}-i386.framework", libraryTitle);
	var x86_64 = (DirectoryPath)string.Format ("{0}-x86_64.framework", libraryTitle);
	var armv7 = (DirectoryPath)string.Format ("{0}-armv7.framework", libraryTitle);
	var armv7s = (DirectoryPath)string.Format ("{0}-armv7s.framework", libraryTitle);
	var arm64 = (DirectoryPath)string.Format ("{0}-arm64.framework", libraryTitle);
	
	var buildArch = new Action<string, string, DirectoryPath> ((sdk, arch, dest) => {
		if (!DirectoryExists (dest)) {
			XCodeBuild (new XCodeBuildSettings {
				Project = workingDirectory.CombineWithFilePath (project).ToString (),
				Target = target,
				Sdk = sdk,
				Arch = arch,
				Configuration = "Release",
			});
			var outputPath = workingDirectory.Combine ("build").Combine (os == TargetOS.Mac ? "Release" : ("Release-" + sdk)).Combine (target).Combine (output);
			CopyDirectory (outputPath, dest);
		}
	});
	
	if (os == TargetOS.Mac) {
		buildArch ("macosx", "x86_64", workingDirectory.Combine (x86_64));
		
		if (!DirectoryExists (fatLibraryPath)) {
			CopyDirectory (workingDirectory.Combine (x86_64), fatLibraryPath);
			RunLipoCreate (workingDirectory, fatLibrary.CombineWithFilePath (libraryTitle), 
				x86_64.CombineWithFilePath (libraryTitle));
		}
	} else if (os == TargetOS.iOS) {
		buildArch ("iphonesimulator", "i386", workingDirectory.Combine (i386));
		buildArch ("iphonesimulator", "x86_64", workingDirectory.Combine (x86_64));
		
		buildArch ("iphoneos", "armv7", workingDirectory.Combine (armv7));
		buildArch ("iphoneos", "armv7s", workingDirectory.Combine (armv7s));
		buildArch ("iphoneos", "arm64", workingDirectory.Combine (arm64));
		
		if (!DirectoryExists (fatLibraryPath)) {
			CopyDirectory (workingDirectory.Combine (arm64), fatLibraryPath);
			RunLipoCreate (workingDirectory, fatLibrary.CombineWithFilePath (libraryTitle), 
				i386.CombineWithFilePath (libraryTitle),
				x86_64.CombineWithFilePath (libraryTitle),
				armv7.CombineWithFilePath (libraryTitle),
				armv7s.CombineWithFilePath (libraryTitle),
				arm64.CombineWithFilePath (libraryTitle));
		}
	} else if (os == TargetOS.tvOS) {
		buildArch ("appletvsimulator", "x86_64", workingDirectory.Combine (x86_64));
		
		buildArch ("appletvos", "arm64", workingDirectory.Combine (arm64));
		
		if (!DirectoryExists (fatLibraryPath)) {
			CopyDirectory (workingDirectory.Combine (arm64), fatLibraryPath);
			RunLipoCreate (workingDirectory, fatLibrary.CombineWithFilePath (libraryTitle), 
				x86_64.CombineWithFilePath (libraryTitle),
				arm64.CombineWithFilePath (libraryTitle));
		}
	}
}

void DownloadMonoSources (string tag, DirectoryPath dest, params string[] urls)
{
	var rootUrl = $"https://github.com/mono/mono/raw/{tag}";

	EnsureDirectoryExists (dest);
	foreach (var originalUrl in urls) {
		// make sure the urls are rooted
		var url = originalUrl;
		if (!url.StartsWith ("http:") && !url.StartsWith ("https:")) {
			url = $"{rootUrl}/{url}";
		}
		// get the path parts
		var file = url.Substring (url.LastIndexOf ("/") + 1);
		var dir = url.Substring (0, url.LastIndexOf ("/"));
		var destFile = dest.CombineWithFilePath (file);
		// download the file
		if (!FileExists (destFile)) {
			Information ($"Downloading '{url}' to '{destFile}'...");
			DownloadFile (url, destFile);
		}
		// if this is a .sources file, download all the listed files too
		if (file.EndsWith (".sources")) {
			var listedFiles = FileReadLines (destFile)
				.Where (f => !f.StartsWith (".."))
				.Select (f => $"{dir}/{f}")
				.ToArray ();
			DownloadMonoSources (tag, dest, listedFiles);
		}
	}
}

/// Api Diff Stuff

DirectoryPath PACKAGE_CACHE_PATH = "externals/package_cache";

IEnumerable<(DirectoryPath path, string platform)> GetPlatformDirectories (DirectoryPath rootDir)
{
    var platformDirs = GetDirectories ($"{rootDir}/*");

    // try find any cross-platform frameworks
    foreach (var dir in platformDirs) {
        var d = dir.GetDirectoryName ().ToLower ();
        if (d.StartsWith ("netstandard") || d.StartsWith ("portable")) {
            // we just want this single platform
            yield return (dir, null);
            yield break;
        }
    }

    // there were no cross-platform libraries, so process each platform
    foreach (var dir in platformDirs) {
        var d = dir.GetDirectoryName ().ToLower ();
        if (d.StartsWith ("monoandroid"))
            yield return (dir, "android");
        else if (d.StartsWith ("net4"))
            yield return (dir, "net");
        else if (d.StartsWith ("uap"))
            yield return (dir, "uwp");
        else if (d.StartsWith ("xamarinios") || d.StartsWith ("xamarin.ios"))
            yield return (dir, "ios");
        else if (d.StartsWith ("xamarinmac") || d.StartsWith ("xamarin.mac"))
            yield return (dir, "macos");
        else if (d.StartsWith ("xamarintvos") || d.StartsWith ("xamarin.tvos"))
            yield return (dir, "tvos");
        else if (d.StartsWith ("xamarinwatchos") || d.StartsWith ("xamarin.watchos"))
            yield return (dir, "watchos");
        else if (d.StartsWith ("tizen"))
            yield return (dir, "tizen");
        else
            throw new Exception ($"Unknown platform '{d}' found at '{dir}'.");
    }
}

void CopyChangelogs (DirectoryPath diffRoot, string id, string version, string outputPath)
{
    foreach (var (path, platform) in GetPlatformDirectories (diffRoot)) {
        // first, make sure to create markdown files for unchanged assemblies
        var xmlFiles = $"{path}/*.new.info.xml";
        foreach (var file in GetFiles (xmlFiles)) {
            var dll = file.GetFilenameWithoutExtension ().GetFilenameWithoutExtension ().GetFilenameWithoutExtension ();
            var md = $"{path}/{dll}.diff.md";
            if (!FileExists (md)) {
                var n = Environment.NewLine;
                var noChangesText = $"# API diff: {dll}{n}{n}## {dll}{n}{n}> No changes.{n}";
                FileWriteText (md, noChangesText);
            }
        }

        // now copy the markdown files to the changelogs
        var mdFiles = $"{path}/*.*.md";
        ReplaceTextInFiles (mdFiles, "<h4>", "> ");
        ReplaceTextInFiles (mdFiles, "</h4>", Environment.NewLine);
        ReplaceTextInFiles (mdFiles, "\r\r", "\r");
        foreach (var file in GetFiles (mdFiles)) {
            var dllName = file.GetFilenameWithoutExtension ().GetFilenameWithoutExtension ().GetFilenameWithoutExtension ();
            if (file.GetFilenameWithoutExtension ().GetExtension () == ".breaking") {
                // skip over breaking changes without any breaking changes
                if (!FindTextInFiles (file.FullPath, "###").Any ())
                    continue;

                dllName += ".breaking";
            }
            var changelogPath = (FilePath)$"./{outputPath}/{id}/{version}/{dllName}.md";
            EnsureDirectoryExists (changelogPath.GetDirectory ());
            CopyFile (file, changelogPath);
        }
    }
}

string[] GetReferenceSearchPaths ()
{
    var refs = new List<string> ();

    if (IsRunningOnWindows ()) {
        var vs = VSWhereLatest (new VSWhereLatestSettings { Requires = "Component.Xamarin" });
        var referenceAssemblies = $"{vs}/Common7/IDE/ReferenceAssemblies/Microsoft/Framework";
        var pf = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);

        // HACK: https://github.com/mono/api-doc-tools/pull/401
        if (!FileExists ("./externals/winmd/Windows.winmd")) {
            EnsureDirectoryExists ("./externals/winmd/");
            CopyFile ($"{pf}/Windows Kits/10/UnionMetadata/Facade/Windows.WinMD", "./externals/winmd/Windows.winmd");
        }
        refs.Add (MakeAbsolute ((FilePath)"./externals/winmd/").FullPath);

        refs.AddRange (GetDirectories ("./output/docs/temp/*").Select (d => d.FullPath));
        refs.Add ($"{referenceAssemblies}/MonoTouch/v1.0");
        refs.Add ($"{referenceAssemblies}/MonoAndroid/v1.0");
        refs.Add ($"{referenceAssemblies}/MonoAndroid/v4.0.3");
        refs.Add ($"{referenceAssemblies}/Xamarin.iOS/v1.0");
        refs.Add ($"{referenceAssemblies}/Xamarin.TVOS/v1.0");
        refs.Add ($"{referenceAssemblies}/Xamarin.WatchOS/v1.0");
        refs.Add ($"{referenceAssemblies}/Xamarin.Mac/v2.0");
        refs.Add ($"{pf}/Windows Kits/10/UnionMetadata/Facade");
        refs.Add ($"{pf}/Windows Kits/10/References/Windows.Foundation.UniversalApiContract/1.0.0.0");
        refs.Add ($"{pf}/Windows Kits/10/References/Windows.Foundation.FoundationContract/1.0.0.0");
        refs.Add ($"{pf}/GtkSharp/2.12/lib");
        refs.Add ($"{vs}/Common7/IDE/PublicAssemblies");
    } else {
        // TODO
    }

    return refs.ToArray ();
}



NuGetDiff CreateNuGetDiff()
{
    var comparer = new NuGetDiff ();
    comparer.SearchPaths.AddRange (GetReferenceSearchPaths ());
    comparer.PackageCache = PACKAGE_CACHE_PATH.FullPath;
    comparer.SaveAssemblyApiInfo = true;
    comparer.SaveAssemblyMarkdownDiff = true;

    return comparer;
}

async Task BuildApiDiff(string packageId, string currentVersionNo)
{
	var baseDir = "./output/api-diff";
    CleanDirectories (baseDir);

	var comparer = CreateNuGetDiff();
	comparer.IgnoreResolutionErrors = true;

	var version = currentVersionNo;
	var latestVersion = (await NuGetVersions.GetLatestAsync (packageId))?.ToNormalizedString ();

	// pre-cache so we can have better logs
	if (!string.IsNullOrEmpty (latestVersion)) {
		Debug ($"Caching version '{latestVersion}' of '{packageId}'...");
		await comparer.ExtractCachedPackageAsync (packageId, latestVersion);
	}

	Debug ($"Running a diff on '{latestVersion}' vs '{version}' of '{packageId}'...");
	var diffRoot = $"{baseDir}/{packageId}";
	using (var reader = new PackageArchiveReader ($"./output/{packageId.ToLower ()}.{version}.nupkg")) 
	{
		// run the diff with just the breaking changes
		comparer.MarkdownDiffFileExtension = ".breaking.md";
		comparer.IgnoreNonBreakingChanges = true;
		await comparer.SaveCompleteDiffToDirectoryAsync (packageId, latestVersion, reader, diffRoot);
		// run the diff on everything
		comparer.MarkdownDiffFileExtension = null;
		comparer.IgnoreNonBreakingChanges = false;
		await comparer.SaveCompleteDiffToDirectoryAsync (packageId, latestVersion, reader, diffRoot);
	}

	CopyChangelogs (diffRoot, packageId, version, "./output/changelogs");

    Information ($"Diff complete of '{packageId}'.");

    // clean up after working
    CleanDirectories (baseDir);
}

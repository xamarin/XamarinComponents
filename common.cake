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

async Task BuildApiDiff (FilePath nupkg)
{
	var baseDir = nupkg.GetDirectory(); //get the parent directory of the packge file

	using (var reader = new PackageArchiveReader (nupkg.FullPath)) 
	{
		//get the id from the package and the version number
		 var packageId = reader.GetIdentity ().Id;
		var currentVersionNo = reader.GetIdentity ().Version.ToNormalizedString();

		//calculate the diff storage path from the location of the nuget
		var diffRoot = $"{baseDir}/api-diff/{packageId}";
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
		// 2. newlines are inccorect on Windows: https://github.com/mono/mono/pull/9918
		ReplaceTextInFiles (mdFiles, "\r\r", "\r");

		// we are done
		Information ($"Diff complete of '{packageId}'.");
	}
}
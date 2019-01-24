#addin nuget:?package=Mono.ApiTools.NuGetDiff&version=1.0.1&loaddependencies=true

using Mono.ApiTools;
using NuGet.Packaging;
using NuGet.Versioning;

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

void CopyChangelogs (DirectoryPath diffRoot, string id, string version)
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
            var changelogPath = (FilePath)$"./changelogs/{id}/{version}/{dllName}.md";
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

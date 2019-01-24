#addin nuget:?package=Mono.ApiTools.NuGetDiff&version=1.0.1&loaddependencies=true

using Mono.ApiTools;
using NuGet.Packaging;
using NuGet.Versioning;

DirectoryPath PACKAGE_CACHE_PATH = "externals/package_cache";

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

#tool nuget:?package=Paket
#addin nuget:?package=Cake.FileHelpers

var NUGET_SOURCE_LIST = EnvironmentVariable ("NUGET_SOURCES") ?? "https://nuget.org/api/v2";
var PAKET_SOURCE_LIST = EnvironmentVariable ("PAKET_SOURCES") ?? "./output,https://nuget.org/api/v2";

var FRAMEWORK = "MonoAndroid70";
var SUPPORT_VERSION = EnvironmentVariable ("ANDROID_SUPPORT_VERSION") ?? "25.3.1";
var GPS_VERSION = EnvironmentVariable ("PLAY_SERVICES_VERSION") ?? "29.0.0.1";

var BUILD_TARGETS = new [] {
    "Xamarin_Forms_Platform_Android",
    "Xamarin_Forms_Platform_Android_AppLinks",
    "Xamarin_Forms_Maps_Android",
    // App projects cause msbuild/nuget restore issues currently, leave out for now
    //"Xamarin_Forms_ControlGallery_Android",
    //"PagesGallery_Droid",
};

var PROJECT_REFS = new Dictionary<string, PkgChanges> {
    { 
        "./Xamarin.Forms.Platform.Android", new PkgChanges { 
            Add = new [] { 
                new Pkg ("Xamarin.Android.Support.v4", SUPPORT_VERSION),
                new Pkg ("Xamarin.Android.Support.Design", SUPPORT_VERSION), 
                new Pkg ("Xamarin.Android.Support.v7.CardView", SUPPORT_VERSION),
            },
            RemovePatterns = new [] { "Xamarin.Android.Support.", "Xamarin.Build.Download" }
        }
    },
    { 
        "./Xamarin.Forms.Platform.Android.AppLinks", new PkgChanges {
            Add = new [] {
                new Pkg ("Xamarin.Android.Support.v4", SUPPORT_VERSION),
                new Pkg("Xamarin.GooglePlayServices.AppIndexing", GPS_VERSION),
                new Pkg("Xamarin.Firebase.AppIndexing", GPS_VERSION),
            },
            RemovePatterns = new [] { "Xamarin.Android.Support.", "Xamarin.GooglePlayServices.", "Xamarin.Firebase.", "Xamarin.Build.Download" }
        }
    },
    {
        "./Xamarin.Forms.Maps.Android", new PkgChanges {
            Add = new [] {
                new Pkg ("Xamarin.Android.Support.v4", SUPPORT_VERSION),
                new Pkg ("Xamarin.Android.Support.v7.AppCompat", SUPPORT_VERSION),
                new Pkg("Xamarin.GooglePlayServices.Maps", GPS_VERSION),
            },
            RemovePatterns = new [] { "Xamarin.Android.Support.", "Xamarin.GooglePlayServices.", "Xamarin.Build.Download" }
        }
    },
    {
        "./Xamarin.Forms.ControlGallery.Android", new PkgChanges {
            Add = new [] {
                new Pkg ("Xamarin.Android.Support.v4", SUPPORT_VERSION),
                new Pkg ("Xamarin.Android.Support.Design", SUPPORT_VERSION),
                new Pkg ("Xamarin.Android.Support.v7.CardView", SUPPORT_VERSION),
                new Pkg("Xamarin.GooglePlayServices.Maps", GPS_VERSION),
            },
            RemovePatterns = new [] { "Xamarin.Android.Support.", "Xamarin.GooglePlayServices.", "Xamarin.Build.Download" }
        }
    },
    {
        "./Stubs/Xamarin.Forms.Platform.Android", new PkgChanges { 
            Add = new [] {
                new Pkg ("Xamarin.Android.Support.v4", SUPPORT_VERSION),
                new Pkg ("Xamarin.Android.Support.v7.RecyclerView", SUPPORT_VERSION),
            },
            RemovePatterns = new [] { "Xamarin.Android.Support.", "Xamarin.Build.Download" }
        } 
    },
    {
        "./PagesGallery/PagesGallery.Droid", new PkgChanges { 
            Add = new [] {
                new Pkg ("Xamarin.Android.Support.v4", SUPPORT_VERSION),
                new Pkg ("Xamarin.Android.Support.Design", SUPPORT_VERSION),
                new Pkg ("Xamarin.Android.Support.v7.CardView", SUPPORT_VERSION),
            },
            RemovePatterns = new [] { "Xamarin.Android.Support.", "Xamarin.Build.Download" }
        }
    },
};

var NUGET_SOURCES = NUGET_SOURCE_LIST.Split (new [] { ',', ';' });
var PAKET_SOURCES = PAKET_SOURCE_LIST.Split (new [] { ',', ';' });

var TARGET = Argument ("target", Argument ("t", "Init"));
var NL = "\r\n";

public class Pkg
{
    public Pkg (string id, string version)
    {
        Id = id;
        Version = version;
    }

    public string Id { get;set; }
    public string Version { get;set; }
}

public class PkgChanges
{
    public Pkg[] Add { get;set; }
    public string[] RemovePatterns { get;set; }
}

Task ("Init").Does (() => {

    var paketFramework = "framework: " + FRAMEWORK + NL;

    Information ("Init Framework");

    var paketSources = PAKET_SOURCES.Select (s => "source " + s);

    // Get the list of dependencies
    var paketNugets = PROJECT_REFS
        .SelectMany (kvp => kvp.Value.Add)
        .GroupBy(v => v.Id)
        .Select (g => g.First())
        .Select (p => "nuget " + p.Id + " == " + p.Version);

    // Write out the paket.dependencies
    FileWriteText ("./paket.dependencies", 
        paketFramework + NL +
        string.Join (NL, paketSources) + NL + 
        string.Join (NL, paketNugets));

    foreach (var kvp in PROJECT_REFS) {
        var paketPackages = kvp.Value.Add.Select (p => p.Id);

        var refFilePath = (new DirectoryPath (kvp.Key)).CombineWithFilePath ("./paket.references");

        FileWriteText (refFilePath, string.Join (NL, paketPackages));
    }
    
    EnsureDirectoryExists ("./output");

});

Task ("Update").IsDependentOn ("Init").Does (() => {

    // Remove old nuget references
    foreach (var kvp in PROJECT_REFS) {
        
        foreach (var removePackagePattern in kvp.Value.RemovePatterns) {

            Information ("Removing Package ID Pattern: {0}", removePackagePattern);

            // Build a list of xpath patterns to remove
            var csprojXPathRemoves = new [] {
                "//x:Reference[starts-with(@Include, '" + removePackagePattern + "')]",
                "//x:Import[contains(@Project, '" + removePackagePattern + "')]",
                "//x:Target[@Name='EnsureNuGetPackageBuildImports']",
                "//x:TreatWarningsAsErrors[text() = 'true']",
            };

            // Get the .csproj files for the project directory
            var csprojFiles = GetFiles (kvp.Key + "/*.csproj");

            foreach (var csproj in csprojFiles) {
                Information ("  Removing items from .csproj: {0}", csproj);
                
                // Remove all the xpath patterns from this csproj
                foreach (var xpathRemove in csprojXPathRemoves) {
                    try {
                        Information ("    XPath: {0}", xpathRemove);
                        // We XmlPoke the pattern with null to remove it
                        XmlPoke (csproj, xpathRemove, null, new XmlPokeSettings {
                            Namespaces = new Dictionary<string, string> { { "x", "http://schemas.microsoft.com/developer/msbuild/2003" } }
                        });
                    } catch {
                        Information ("      None found.");
                    }
                }
            }

            // Find the packages.config file
            var packagesConfigFile = (new DirectoryPath (kvp.Key)).CombineWithFilePath ("./packages.config");

            Information("  Removing items from packages.config: {0}", packagesConfigFile);

            // Remove from packages.config
            try {
                var xpath = "//package[starts-with(@id, '" + removePackagePattern + "')]";
                Information ("    XPath: {0}", xpath);
                XmlPoke (packagesConfigFile, xpath, null);
            } catch {
                Information ("    None found.");
            }
        }
    }

    var paketExe = GetFiles ("./tools/**/paket.exe").FirstOrDefault ();

    StartProcess (paketExe, "update --force");

    Information ("Restoring nuget packages...");

    // Perform a nuget restore for each .csproj
    foreach (var kvp in PROJECT_REFS)
    {
        var csprojFiles = GetFiles (kvp.Key + "/*.csproj");

        foreach (var csproj in csprojFiles) {
            Information ("  Restoring NuGets for .csproj: {0}", csproj);

            NuGetRestore (csproj, new NuGetRestoreSettings {
                Source = NUGET_SOURCES,
                PackagesDirectory = "./packages"
            });
        }             
    }

    // Build each project separately
    foreach (var bt in BUILD_TARGETS)
        XBuild ("./Xamarin.Forms.sln", c => c.Targets.Add (bt));
});

RunTarget (TARGET);

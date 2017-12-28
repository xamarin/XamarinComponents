
#addin nuget:?package=Cake.FileHelpers
#addin nuget:?package=Cake.Git

#tool nuget:?package=Paket

var PAKET_SOURCE_LIST = EnvironmentVariable ("PAKET_SOURCES") ?? "./output,https://api.nuget.org/v3/index.json";
var TREAT_WARNINGS_AS_ERRORS = (EnvironmentVariable ("TREAT_WARNINGS_AS_ERRORS") ?? "false").ToLower ().Equals ("true");

var ANDROID_SUPPORT_VERSION = EnvironmentVariable ("ANDROID_SUPPORT_VERSION") ?? "25.4.0.2";
var GOOGLE_PLAY_SERVICES_FIREBASE_VERSION = EnvironmentVariable ("PLAY_SERVICES_VERSION") ?? "42.1021.1";
var ANDROID_VERSION = EnvironmentVariable ("ANDROID_VERSION") ?? "8.0";
var MONOANDROID_VERSION = "monoandroid" + ANDROID_VERSION;

var XAMARIN_FORMS_GIT_URL = "https://github.com/xamarin/Xamarin.Forms.git";

var BUILD_TARGETS = new [] {
    "Platforms\\Xamarin_Forms_Platform_Android",
    "Platforms\\Xamarin_Forms_Platform_Android_AppLinks",
    "Xamarin_Forms_Maps\\Xamarin_Forms_Maps_Android",
    // App projects cause msbuild/nuget restore issues currently, leave out for now
    //"Control Gallery\\Xamarin_Forms_ControlGallery_Android",
    //"Pages Gallery\\PagesGallery_Droid",
};

var DELETE_DIRS = new DirectoryPath [] {
    "./PagesGallery/PagesGallery.WinPhone",
    "./PagesGallery/PagesGallery.Windows",
};

var PAKET_SOURCES = PAKET_SOURCE_LIST.Split (new [] { ',', ';' });
var PAKET_EXE = GetFiles ("./tools/**/paket.exe").FirstOrDefault ();

var TARGET = Argument ("target", Argument ("t", "Default"));

Task ("clone").Does(() => {
    GitClone ("https://github.com/xamarin/Xamarin.Forms.git", "./xf");
});

Task ("init").Does (() => {
    // Delete these directories as they aren't referenced by Xamarin.Forms.sln anymore
    // and they reference nuget package versions of Xamarin.Forms in them (and an old version)
    // which messes with paket.exe and causes version conflicts, so easiest just to delete the dirs
    foreach (var dd in DELETE_DIRS) {
        if (DirectoryExists (dd))
            DeleteDirectory (dd, true);
    }
    
    // Update the target framework version in all the android .csproj files
    ReplaceRegexInFiles ("./**/*droid*.csproj",
                         "<TargetFrameworkVersion>v[0-9\\.]+</TargetFrameworkVersion>",
                         "<TargetFrameworkVersion>v" + ANDROID_VERSION + "</TargetFrameworkVersion>");

    // Convert the sln and projects to use paket
    StartProcess (PAKET_EXE, "convert-from-nuget --force --verbose");

    // Load the dependencies file, skipping any 'source' line
    var lines = FileReadLines ("./paket.dependencies")
                    .Where (l => !l.StartsWith ("source"));

    // Keep track of our changes, start by adding the requested nuget sources
    var newLines = new List<string> ();
    newLines.AddRange (PAKET_SOURCES.Select (n => "source " + n));

    foreach (var line in lines) {

        // Break apart the line to see if it's a nuget specification
        var match = System.Text.RegularExpressions.Regex.Match (line, @"nuget\s+(?<id>[^ ]+)\s+(?<version>[^ ]+)");

        // If not, just write out the line as is
        if (!match.Success) {
            newLines.Add (line);
            continue;
        }

        // Get the package id and version
        var pkgId = string.Empty;
        if (match != null && match.Groups != null && match.Groups["id"] != null)
            pkgId = match.Groups["id"].Value;
        var pkgVer = string.Empty;
        if (match != null && match.Groups != null && match.Groups["version"] != null)
            pkgVer = match.Groups["version"].Value;
        var restriction = line.Contains ("restriction:");

        // Check to see if it's a Google Play Services or Firebase package and build the line appropriately
        if (pkgId.StartsWith ("Xamarin.GooglePlayServices.") || pkgId.StartsWith ("Xamarin.Firebase."))
            newLines.Add (string.Format ("nuget {0} {1}{2}", pkgId, GOOGLE_PLAY_SERVICES_FIREBASE_VERSION, restriction ? " restriction: >= " + MONOANDROID_VERSION : string.Empty));
        else if (pkgId.StartsWith ("Xamarin.Android.Support."))
            newLines.Add (string.Format ("nuget {0} {1}{2}", pkgId, ANDROID_SUPPORT_VERSION, restriction ? " restriction: >= " + MONOANDROID_VERSION : string.Empty));
        else // Other package id's just get written out as is
            newLines.Add (line);
    }

    // Overwrite our dependencies file with the modified info
    FileWriteLines ("./paket.dependencies", newLines.ToArray ());
    
    // Convert the sln and projects to use paket
    StartProcess (PAKET_EXE, "install --force --verbose");
});

Task ("build").Does (() => {
    // Build each project separately
    foreach (var bt in BUILD_TARGETS) {
        MSBuild ("./Xamarin.Forms.sln", c => {
            c.Targets.Add ("\"" + bt + "\"");
            c.Properties.Add ("TreatWarningsAsErrors", new List<string> { TREAT_WARNINGS_AS_ERRORS ? "true" : "false" });
        });
    }
});

Task ("Default").IsDependentOn ("init").IsDependentOn ("build");

RunTarget (TARGET);

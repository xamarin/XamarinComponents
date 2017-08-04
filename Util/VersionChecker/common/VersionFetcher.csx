#load "UpdateInfo.csx"

using System.Text.RegularExpressions;
using Semver;

public abstract class VersionFetcher
{
    public VersionFetcher (string componentName, string currentVersion, string owner)
    {
        ComponentName = componentName;
        CurrentVersion = currentVersion;
        Owner = owner;
    }

    public virtual string ComponentName { get; private set; }

    public virtual string CurrentVersion { get; private set; }

    public virtual string Owner { get; protected set; }

    public abstract string FetchNewestAvailableVersion ();
    
    public string FindVersion (string input)
    {
        var rx = new Regex ("(?<major>\\d+)(\\.(?<minor>\\d+))?(\\.(?<patch>\\d+))?(\\-(?<pre>[0-9A-Za-z\\-\\.]+))?(\\+(?<build>[0-9A-Za-z\\-\\.]+))?");
        var match = rx.Match (input);

        if (match != null)
            return match.Value;

        return null;
    }    
    
    public UpdateInfo Run ()
    {
        var newestVersion = FetchNewestAvailableVersion ();
     
        var updateAvailable = SemVersion.Parse (newestVersion) > SemVersion.Parse (CurrentVersion);

        if (updateAvailable) {
            if (!string.IsNullOrEmpty (Owner)) {
                Console.WriteLine ("{0}: {1} => {2} - @{3}", ComponentName, CurrentVersion, newestVersion, Owner);
            } else {
                Console.WriteLine ("{0}: {1} => {2}", ComponentName, CurrentVersion, newestVersion);   
            }

            return new UpdateInfo { Title = ComponentName, CurrentVersion = CurrentVersion, AvailableVersion = newestVersion, Owner = Owner };
        }
    
        return null;
    }
}

Version Fetchers
================

This is a collection of `scriptcs` scripts to assist in keeping track of versions of 3rd party libraries available and the current versions of those libraries which we currently have released bindings for.

Version information can come from a variety of places.  By default the following sources have base implementations to help you write a version checker:

 - GitHub Releases (supports Tags as version #'s)
 - GitHub Tags
 - Maven Repository
 - CocoaPods
 - Xml (Using XPath to query)
 - Html (Using HtmlAgilityPack to parse)
 
You can of course create your own fetch script by implementing the `VersionFetcher` class which can be found in `common\VersionFetcher.csx`.  Using `scriptcs` and implementing this base class, you can write any sort of C# necessary to fetch version information.  You can even include NuGet packages to help you (such as HtmlAgilityPack for instance), by editing the `scriptcs_packages.config` file.

## Installing scriptcs

Use [Homebrew](http://brew.sh) to install scriptcs.

After homebrew is installed, simply run `brew install scriptcs`.


## Checking all the versions

The easiest way to check everything is to run `scriptcs versions.csx`.  This will install the required NuGet dependencies and then run every single `.csx` script in this directory.  


## Creating Fetchers


### GitHub Release Version Fetchers
Some libraries tag releases using the GitHub *Releases* feature.  If you choose to subclass `GitHubReleaseVersionFetcher`, your fetcher must override the standard `ComponentName` and `CurrentVersion` properties.  It must also override:
	
 - `string GitHubRepoName { get; }`
 - `string GitHubOwnerName { get; }`
 
The other requirement for this fetcher to work is that the Release `tag` value must be the version number.  It is acceptable for this tag to have a *v* prefix to the version (eg: v1.0.0) as well as some punctuation (, or .) which will be trimmed out automatically.

Example:
```csharp
// import the core script files
#load "./common/BindingVersionFetcher.csx"

new CardIOAndroidVersionFetcher ().Run ();

public class CardIOAndroidVersionFetcher : GitHubReleasesVersionFetcher
{
    public override string ComponentName {
        get { return "Card.IO Android"; }
    }

    public override string CurrentVersion {
        get { return "4.0.0"; }
    }

    public override string GitHubRepoName {
        get { return "card.io-Android-SDK"; }
    }

    public override string GitHubOwnerName {
        get { return "card-io"; }
    }
}
```




### Maven Version Fetchers
If you choose to subclass `MavenVersionFetcher`, your fetcher must override the standard `ComponentName` and `CurrentVersion` properties, as well as the string property `MavenMetadataXmlUrl`.  This URL should be the url to the `maven-metadata.xml` file for the library you are fetching the version of.

You can search http://search.maven.org/ to find popular libraries.  You can then navigate the results to find the `maven-metadata.xml` file for a given library.

Example:
```csharp
// import the core script files
#load "./common/BindingVersionFetcher.csx"

new BetterPickersVersionFetcher ().Run ();

public class BetterPickersVersionFetcher : MavenVersionFetcher
{
    public override string ComponentName {
        get { return "BetterPickers"; }
    }

    public override string CurrentVersion {
        get { return "1.5.3"; }
    }

    public override string MavenMetadataXmlUrl {
        get { return "https://repo1.maven.org/maven2/com/doomonafireball/betterpickers/library/maven-metadata.xml"; }
    }
}
```


### CocoaPods Version Fetchers

If you choose to subclass `CocoapodsVersionFetcher`, your fetcher must override the standard `ComponentName`, `CurrentVersion`, and `PodId` properties.  The `PodId` property is the name of the cocoapod that goes in your Podfile (the name returned by searching cocoapods.org).

Example:
```csharp
// import the core script files
#load "./common/BindingVersionFetcher.csx"

new FacebookPopVersionFetcher ().Run ();

public class FacebookPopVersionFetcher : PodspecVersionFetcher
{
    public override string ComponentName {
        get { return "FacebookPop"; }
    }

    public override string CurrentVersion {
        get { return "1.0.7"; }
    }

    public override string PodId {
        get { return "pop"; }
    }
}
```

### HTML Version Fetchers

Often the version information for something may only be available through an HTML page.  To streamline scraping of HTML data, a special abstract class called `HtmlVersionFetcher` was created.  

HtmlAgilityPack is a library which helps you query HTML DOM with XPath.  The entire HTML Document is available to you to query from.  To find out more about HtmlAgilityPack, [click here](http://htmlagilitypack.codeplex.com/).

The `HtmlVersionFetcher` has the following methods and properties to override:

```csharp
public abstract class HtmlVersionFetcher : VersionFetcher
{
    public abstract string ComponentName { get; }
    public abstract string CurrentVersion { get; }

	// The url of the HTML you want to scrape
	// This page will automatically be fetched for you
	// and loaded into an HtmlDocument instance from HtmlAgilityPack
    public abstract string Url { get; }

	// This method must return the version that you've parsed from
	// the HTML page.  HtmlDocument gives you access to the entire page DOM
    public abstract string FetchNewestAvailableVersion (HtmlDocument html);
}
```


### Custom Version Fetchers

To write your own version fetcher, you must subclass `VersionFetcher` which has the following members for you to override:

```csharp
public abstract class VersionFetcher
{
	// Component's name (so we can output which one it is)
    public abstract string ComponentName { get; }

	// What version is currently in the binding (hard coded)
    public abstract string CurrentVersion { get; }

	// Do whatever you want here to return the available version
    public abstract string FetchNewestAvailableVersion ();
}
```

Since the fetchers are all written as `scriptcs` scripts, which is just C#, you are free to do whatever it takes in `FetchNewestAvailableVersion ()` to return the version string.




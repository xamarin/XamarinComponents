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


## Fetchers

The `versions.csx` file contains definitions to fetch versions for many different libraries.  You should be able to infer usage of each of the available fetchers by looking for one of the libraries using it.

All fetcher constructors take the following parameters:

 - `string component` The name of the library, just for display purposes.
 - `string version` The current version we have released bindings for.  This is the version that is checked against to see if a new version is available to be bound and released.
 - `string owner` The owner's nickname of the library.  This is useful for Slack notifications to mention the user.

There are various existing fetcher implementations for common library/package services:

 - `Maven (string component, string version, string metadataXmlUrl, string owner)`
 - `BinTray (string component, string version, string bintrayUser, string artifactPath, string owner)`
 - `MavenCentral (string component, string version, string artifactPath, string owner)`
 - `CocoaPods (string component, string version, string podId, string owner)`
 - `GitHubReleases (string component, string version, string githubUser, string githubRepo, string owner)`
 - `XPath (string component, string version, string xmlUrl, string xpathQuery, string ownerz)`
    

### HTML Version Fetchers

Often the version information for something may only be available through an HTML page.  To streamline scraping of HTML data, a special abstract class called `HtmlVersionFetcher` was created.  

HtmlAgilityPack is a library which helps you query HTML DOM with XPath.  The entire HTML Document is available to you to query from.  To find out more about HtmlAgilityPack, [click here](http://htmlagilitypack.codeplex.com/).

To create your own fetcher, subclass `Html` like this:

```csharp
public abstract class SimpleHtml : Html
{
	public SimpleHtml (string component, string version, string owner)
		: base (component, version, "http://somesite.com", owner)
	{}
	
	// This method must return the version that you've parsed from
	// the HTML page.  HtmlDocument gives you access to the entire page DOM
    public override string FetchNewestAvailableVersion (HtmlDocument html)
    {
		// Parse the newest available version from the html and return it...
	}
}
```


### Custom Version Fetchers

To write your own version fetcher, you must subclass `VersionFetcher` like this:

```csharp
public abstract class MyCustom : VersionFetcher
{
	public MyCustom (string component, string version, string owner)
		: base (component, version, owner)
	{
	}
	
	// Do whatever you want here to return the available version
    public override string FetchNewestAvailableVersion ()
    {
    	// Do whatever you need to find/parse the newest available version here
    	// and return it!
	}
}
```






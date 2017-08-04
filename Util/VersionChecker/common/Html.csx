#load "VersionFetcher.csx"

using System.Xml;
using Semver;
using HtmlAgilityPack;

public abstract class Html : VersionFetcher
{
    public Html (string component, string version, string url, string owner)
        : base (component, version, owner)
    {
        Url = url;
    }
    
    public virtual string Url { get; private set; }

    public override string FetchNewestAvailableVersion ()
    {
        var http = new HttpClient ();
        var data = http.GetStringAsync (Url).Result;
        
        var html = new HtmlDocument ();
        html.LoadHtml (data);

        return FetchNewestAvailableVersion (html);
    }

    public abstract string FetchNewestAvailableVersion (HtmlDocument htmlDoc);
}

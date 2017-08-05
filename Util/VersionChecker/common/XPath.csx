#load "VersionFetcher.csx"

using System.Xml;
using Semver;

public class XPath : VersionFetcher
{
    public XPath (string component, string version, string url, string versionXPath, string owner)
        : base (component, version, owner)
    {
        Url = url;
        VersionXPath = versionXPath;
    }
    
    public string Url { get; private set; }
    public string VersionXPath { get; private set; }

    public override string FetchNewestAvailableVersion ()
    {
        var http = new HttpClient ();
        http.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml");
        http.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
        http.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
        http.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Charset", "ISO-8859-1");

        var data = http.GetStringAsync (Url).Result;

        var xdoc = new System.Xml.XmlDocument ();
        xdoc.LoadXml (data);

        return xdoc.SelectSingleNode (VersionXPath).InnerText;
    }
}

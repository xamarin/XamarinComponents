#load "VersionFetcher.csx"

using System.Xml;

public class Maven : VersionFetcher
{
    public Maven (string component, string version, string url, string owner) 
        : base (component, version, owner)
    {
        MavenMetadataXmlUrl = url;
    }

    public string MavenMetadataXmlUrl { get; private set; }

    public override string FetchNewestAvailableVersion ()
    {
        var http = new HttpClient ();
        http.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml");
        http.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
        http.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
        http.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Charset", "ISO-8859-1");

        var data = http.GetStringAsync (MavenMetadataXmlUrl).Result;

        var xml = new XmlDocument ();
        xml.LoadXml (data);

        var releaseNode = xml.SelectSingleNode ("//versioning/release");

        return SemVersion.Parse (releaseNode.InnerText).ToString ();
    }
}

public class BinTray : Maven
{
    public BinTray (string component, string version, string bintrayUser, string path, string owner)
        : base (component, version, "https://bintray.com/artifact/download/" + bintrayUser + "/" + path + "/maven-metadata.xml", owner)
    {
    }
}

public class MavenCentral : Maven
{
    public MavenCentral (string component, string version, string path, string owner)
        : base (component, version, "http://repo1.maven.org/maven2/" + path + "/maven-metadata.xml", owner)
    {
    }
}


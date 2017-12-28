#load "VersionFetcher.csx"

using Newtonsoft.Json.Linq;
using Semver;

public class CocoaPods : VersionFetcher
{
    public CocoaPods (string component, string version, string podId, string owner)
        : base (component, version, owner)
    {
        PodId = podId;
    }
    public string PodId { get; private set; }

    public override string FetchNewestAvailableVersion ()
    {
        var githubClientId = System.Environment.GetEnvironmentVariable ("GITHUB_CLIENT_ID");
        var githubClientSecret = System.Environment.GetEnvironmentVariable ("GITHUB_CLIENT_SECRET");

        // CocoaPods moved to using a folder prefix in their Specs repo in order to avoid
        // having all the specs in a single directory
        // The formula is the first 3 chars of the md5 value of the Pod ID
        // where each of the 3 letters is a folder/subfolder/subsubfolder
        // So we'll hash the PodID, and construct the path using this formula
        var podIdBytes = System.Text.Encoding.Default.GetBytes (PodId);
        var podIdMd5 = string.Empty;
        using (var md5 = System.Security.Cryptography.MD5.Create ()) {
            podIdMd5 = System.BitConverter.ToString (md5.ComputeHash (podIdBytes)).Replace ("-", "").ToLowerInvariant ();
        }

        var podIdPath = podIdMd5[0] + "/" + podIdMd5[1] + "/" + podIdMd5[2] + "/" + PodId;

        var http = new HttpClient ();
        http.DefaultRequestHeaders.UserAgent.ParseAdd ("Xamarin-Internal/1.0");

        var tree = string.Format ("master:Specs/{0}", podIdPath);
        
        var url = string.Format ("https://api.github.com/repos/{0}/{1}/git/trees/{2}", "CocoaPods", "Specs", System.Net.WebUtility.UrlEncode (tree));

        if (!string.IsNullOrEmpty (githubClientId) && !string.IsNullOrEmpty (githubClientSecret)) 
            url += "?client_id=" + githubClientId + "&client_secret=" + githubClientSecret;

        var data = http.GetStringAsync (url).Result;

        var json = JObject.Parse (data);

        var items = json["tree"] as JArray;

        var highestVersion = SemVersion.Parse ("0.0.0");

        foreach (var item in items) {

            var path = item["path"].ToString ();

            try {
                var v = SemVersion.Parse (path);

                if (v > highestVersion)
                    highestVersion = v;
            } catch { }
        }

        return highestVersion.ToString ();
    }
}

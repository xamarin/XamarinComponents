#load "VersionFetcher.csx"

using Semver;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

public class GitHubTags : VersionFetcher
{
    public GitHubTags (string component, string version, string user, string repo, string owner)
        : base (component, version, owner)
    {
        GitHubRepoName = repo;
        GitHubOwnerName = user;
    }

    public string GitHubRepoName { get; }
    public string GitHubOwnerName { get; }

    public override string FetchNewestAvailableVersion ()
    {
        var githubClientId = System.Environment.GetEnvironmentVariable ("GITHUB_CLIENT_ID");
        var githubClientSecret = System.Environment.GetEnvironmentVariable ("GITHUB_CLIENT_SECRET");

        var http = new HttpClient ();
        http.DefaultRequestHeaders.UserAgent.ParseAdd("Xamarin-Internal/1.0");

        var url = string.Format ("https://api.github.com/repos/{0}/{1}/tags",
                                    GitHubOwnerName,
                                    GitHubRepoName);

        if (!string.IsNullOrEmpty (githubClientId) && !string.IsNullOrEmpty (githubClientSecret)) 
            url += "?client_id=" + githubClientId + "&client_secret=" + githubClientSecret;

        var data = http.GetStringAsync (url).Result;

        var items = JArray.Parse (data);

        var highestVersion = SemVersion.Parse ("0.0.0");

        foreach (var item in items) {

            if (item["name"] == null)
                continue;

            var tag = item.Value<string> ("name");

            var v = SemVersion.Parse ("0.0.0");

            var sv = FindVersion (tag);
            if (!string.IsNullOrEmpty (sv))
                v = SemVersion.Parse (sv);

            if (v > highestVersion)
                highestVersion = v;
        }

        return highestVersion.ToString ();
    }
}

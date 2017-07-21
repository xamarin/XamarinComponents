#load "UpdateInfo.csx"

public class SlackNotifier
{
    public static void Notify (string owner, IEnumerable<UpdateInfo> updates)
    {
        string slackUrl = System.Environment.GetEnvironmentVariable ("SLACK_URL");
        var slackUser = System.Environment.GetEnvironmentVariable ("SLACK_USER");

        var http = new HttpClient ();

        var attachments = updates.Select (a => "{\"title\":\"" + a.Title + "\", \"text\":\"" + a.CurrentVersion + " -> " + a.AvailableVersion + "\"}");
        var payload = "{ \"text\": \"<@" + owner + "> please update :allthethings:\", \"attachments\":[" + string.Join(",", attachments) + "], \"username\":\"" + slackUser + "\" }";

        Console.WriteLine (payload);
        
        var content = new StringContent(payload, Encoding.UTF8, "application/json");

        var result = http.PostAsync(slackUrl, content).Result;

        Console.WriteLine ("POST");
        var data = result.Content.ReadAsStringAsync().Result;

        Console.WriteLine (data);
    }

    public static void Notify (List<string> failures)
    {
        string slackUrl = System.Environment.GetEnvironmentVariable ("SLACK_URL");
        var slackUser = System.Environment.GetEnvironmentVariable ("SLACK_USER");

        var http = new HttpClient ();

        var attachments = failures.Select (f => "{\"title\":\"" + f + "\"}");
        var payload = "{ \"text\": \"The following version fetchers failed:\", \"attachments\":[" + string.Join(",", attachments) + "], \"username\":\"" + slackUser + "\" }";

        Console.WriteLine (payload);
        
        var content = new StringContent(payload, Encoding.UTF8, "application/json");

        var result = http.PostAsync(slackUrl, content).Result;

        Console.WriteLine ("POST");
        var data = result.Content.ReadAsStringAsync().Result;

        Console.WriteLine (data);
    }
}
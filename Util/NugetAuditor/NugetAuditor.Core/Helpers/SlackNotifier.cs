using NugetAuditor.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NugetAuditor.Core.Helpers
{
    public class SlackNotifier
    {
        public static void Notify(List<string> failures)
        {
            string slackUrl = SettingsHelper.GetSettingString("slackUrl");
            var slackUser = SettingsHelper.GetSettingString("slackUser");

            var http = new HttpClient();

            var attachments = failures.Select(f => "{\"title\":\"" + f + "\"}");
            var payload = "{ \"text\": \"The following packages have failed signature validation:\", \"attachments\":[" + string.Join(",", attachments) + "], \"username\":\"" + slackUser + "\" }";

            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            var result = http.PostAsync(slackUrl, content).Result;
            var data = result.Content.ReadAsStringAsync().Result;
        }
    }
}

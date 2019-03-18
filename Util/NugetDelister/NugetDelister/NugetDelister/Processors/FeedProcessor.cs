using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NugetDelister.Helpers;
using NugetDelister.Models;

namespace NugetDelister.Processors
{
    public class FeedProcessor
    {
        private string _apiKey;

        public FeedProcessor(string apiKey)
        {
            _apiKey = apiKey;

        }
        public async Task<List<PackageData>> ProcessQueryAsync(string packageId)
        {
            var results = new List<PackageData>();

            var baseUrl = NugetServiceIndex.QueryServiceApiUrl;

            using (var wc = new WebClient())
            {

                var result = await wc.DownloadStringTaskAsync($"{baseUrl}?q=id:\"{packageId}\"&prerelease=true&take=100");
                var queryResult = JsonConvert.DeserializeObject<PackagesData>(result, JsonDeserializeSettings.Default);

                while (queryResult.Data.Count > 0)
                {
                    results.AddRange(queryResult.Data);
                    result = await wc.DownloadStringTaskAsync($"{baseUrl}?q=id:\"{packageId}\"&prerelease=true&skip={results.Count}&take=100");
                    queryResult = JsonConvert.DeserializeObject<PackagesData>(result, JsonDeserializeSettings.Default);
                }
            }

            return results;
        }

        internal async Task DelistAsync(string packageId, string version)
        {

            using (var wc = new WebClient())
            {
                var aUrl = $"https://www.nuget.org/api/v2/package/{packageId}/{version}";

                wc.Headers.Add("X-NuGet-ApiKey", _apiKey);

                await wc.UploadStringTaskAsync(new Uri(aUrl), "DELETE", "");

            }
        }
    }
}

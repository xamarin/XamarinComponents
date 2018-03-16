using NugetAuditor.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NugetAuditor.Processors
{
    public class FeedProcessor
    {
        

        public async Task<List<PackageData>> ProcessQueryAsync()
        {
            var results = new List<PackageData>();

            var baseUrl = NugetServiceIndex.QueryServiceApiUrl;

            using (var wc = new WebClient())
            {

                var result = await wc.DownloadStringTaskAsync($"{baseUrl}?q=owner:xamarin&prerelease=true&take=100");
                var queryResult = JsonConvert.DeserializeObject<PackagesData>(result, JsonDeserializeSettings.Default);

                while (queryResult.Data.Count > 0)
                {
                    results.AddRange(queryResult.Data);
                    result = await wc.DownloadStringTaskAsync($"{baseUrl}?q=owner:xamarin&prerelease=true&skip={results.Count}&take=100");
                    queryResult = JsonConvert.DeserializeObject<PackagesData>(result, JsonDeserializeSettings.Default);
                }
            }

            return results;
        }

        public async Task<List<PackageSearchData>> ProcessSearchAsync()
        {
            var results = new List<PackageSearchData>();

            var baseUrl = NugetServiceIndex.SearchQueryServiceApiUrl;

            using (var wc = new WebClient())
            {

                var result = await wc.DownloadStringTaskAsync($"{baseUrl}?q=owner:xamarin&prerelease=true&take=100");
                var queryResult = JsonConvert.DeserializeObject<PackageSearchResults>(result, JsonDeserializeSettings.Default);

                while (queryResult.Data.Count > 0)
                {
                    results.AddRange(queryResult.Data);
                    result = await wc.DownloadStringTaskAsync($"{baseUrl}?q=owner:xamarin&prerelease=true&skip={results.Count}&take=100");
                    queryResult = JsonConvert.DeserializeObject<PackageSearchResults>(result, JsonDeserializeSettings.Default);
                }
            }

            return results;
        }
    }
}

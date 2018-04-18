using Newtonsoft.Json;
using NugetAuditor.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NugetAuditor.Core.Helpers
{
    public class NugetServiceIndex
    {
        private static string baseQueryServiceApiUrl;
        private const string serviceIndexApiUrl = "https://api.nuget.org/v3/index.json";

        public static string QueryServiceApiUrl
        {
            get
            {

                return $"{BaseQuertServiceApiUrl}" + "/query";

            }
            
        }

        public static string SearchQueryServiceApiUrl
        {
            get
            {

                return $"{BaseQuertServiceApiUrl}" + "/search/query";

            }

        }

        private static string BaseQuertServiceApiUrl
        {
            get
            {
                if (string.IsNullOrWhiteSpace(baseQueryServiceApiUrl))
                    throw new NullReferenceException("You must setup the SearchApi first by calling NugetServiceIndexProcessor.SetupSearchApiAsync");

                return baseQueryServiceApiUrl;
            }
            set
            {
                baseQueryServiceApiUrl = value;
            }
        }

        public static async Task SetupSearchApiAsync()
        {
            using (var wc = new WebClient())
            {

                //var things = "https://api-v2v3search-0.nuget.org/search/query?q=owner:xamarin";

                var result = await wc.DownloadStringTaskAsync(serviceIndexApiUrl);
                var apiDef = JsonConvert.DeserializeObject<NugetApiDefinition>(result, JsonDeserializeSettings.Default);

                var searchQueryService = apiDef.Resources.First(x => x.Type.Equals("SearchQueryService"));

                BaseQuertServiceApiUrl = searchQueryService.Id.Replace("/query","");


                
            }
        }
    }

    public partial class NugetApiDefinition
    {
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("resources")]
        public Resource[] Resources { get; set; }

        [JsonProperty("@context")]
        public Context Context { get; set; }
    }

    public partial class Context
    {
        [JsonProperty("@vocab")]
        public string Vocab { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }
    }

    public partial class Resource
    {
        [JsonProperty("@id")]
        public string Id { get; set; }

        [JsonProperty("@type")]
        public string Type { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("clientVersion")]
        public string ClientVersion { get; set; }
    }

}

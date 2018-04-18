using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetAuditor.Core
{

    public class PackageSearchResults
    {
        [JsonProperty("totalHits")]
        public long TotalHits { get; set; }

        [JsonProperty("index")]
        public string Index { get; set; }

        [JsonProperty("indexTimestamp")]
        public string IndexTimestamp { get; set; }

        [JsonProperty("data")]
        public List<PackageSearchData> Data { get; set; }
    }
}

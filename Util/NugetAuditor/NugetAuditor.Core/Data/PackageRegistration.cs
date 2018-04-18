using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetAuditor.Core
{
    public partial class PackageRegistration
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("DownloadCount")]
        public long DownloadCount { get; set; }

        [JsonProperty("Verified")]
        public bool Verified { get; set; }

        [JsonProperty("Owners")]
        public string[] Owners { get; set; }
    }
}

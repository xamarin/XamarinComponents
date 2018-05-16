using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace NugetAuditor.Core
{
    public partial class PackagesData
    {
        [J("totalHits")] public long TotalHits { get; set; }
        [J("data")] public List<PackageData> Data { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace NugetAuditor.Core
{
    public partial class RegistrationLeafResponse
    {
        [J("@id")] public string Id { get; set; }
        [J("catalogEntry")] public string CatalogEntryUrl { get; set; }
        [J("packageContent")] public string PackageContentUrl { get; set; }
        [J("published")] public DateTime Published { get; set; }
    }
}

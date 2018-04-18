using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace NugetAuditor.Core
{
    public partial class CatalogEntryResponse
    {
        [J("@id")] public string Id { get; set; }
        [J("isPrerelease")] public bool IsPrerelease { get; set; }
        [J("projectUrl")] public string ProjectUrl { get; set; }
        [J("licenseUrl")] public string LicenceUrl { get; set; }
        [J("iconUrl")] public string IconUrl { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace NugetAuditor.Core
{
    public partial class PackageData
    {
        [J("id")] public string PackageId { get; set; }
        [J("title")] public string Title { get; set; }
        [J("version")] public string CurrentVersion { get; set; }
        [J("versions")] public List<PackageVersion> Versions { get; set; }
        [J("projectUrl")] public string ProjectUrl { get; set; }
        [J("licenseUrl")] public string LicenceUrl { get; set; }
        [J("iconUrl")] public string IconUrl { get; set; }
        [J("totalDownloads")] public long TotalDownloads { get; set; }
        [J("authors")] public string[] Authors { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace CheckNuGets.Data
{
    public partial class PackageVersion
    {
        [J("version")] public string VersionString { get; set; }
        [J("downloads")] public long Downloads { get; set; }
        [J("@id")] public string RegistrationLeafUrl { get; set; }
    }
}

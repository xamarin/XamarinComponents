using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetAuditor.Core
{
    public partial class PackageSearchData
    {
        [JsonProperty("PackageRegistration")]
        public PackageRegistration PackageRegistration { get; set; }

        [JsonProperty("Version")]
        public string Version { get; set; }

        [JsonProperty("NormalizedVersion")]
        public string NormalizedVersion { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Summary")]
        public string Summary { get; set; }

        [JsonProperty("Authors")]
        public string Authors { get; set; }

        [JsonProperty("Copyright")]
        public string Copyright { get; set; }

        [JsonProperty("Tags")]
        public string Tags { get; set; }

        [JsonProperty("ProjectUrl")]
        public string ProjectUrl { get; set; }

        [JsonProperty("IconUrl")]
        public string IconUrl { get; set; }

        [JsonProperty("IsLatestStable")]
        public bool IsLatestStable { get; set; }

        [JsonProperty("IsLatest")]
        public bool IsLatest { get; set; }

        [JsonProperty("Listed")]
        public bool Listed { get; set; }

        [JsonProperty("Created")]
        public System.DateTimeOffset Created { get; set; }

        [JsonProperty("Published")]
        public System.DateTimeOffset Published { get; set; }

        [JsonProperty("LastUpdated")]
        public System.DateTimeOffset LastUpdated { get; set; }

        [JsonProperty("DownloadCount")]
        public long DownloadCount { get; set; }

        [JsonProperty("FlattenedDependencies")]
        public string FlattenedDependencies { get; set; }

        //[JsonProperty("Dependencies")]
        //public Dependency[] Dependencies { get; set; }

        [JsonProperty("SupportedFrameworks")]
        public string[] SupportedFrameworks { get; set; }

        [JsonProperty("Hash")]
        public string Hash { get; set; }

        [JsonProperty("HashAlgorithm")]
        public string HashAlgorithm { get; set; }

        [JsonProperty("PackageFileSize")]
        public long PackageFileSize { get; set; }

        [JsonProperty("LicenseUrl")]
        public string LicenseUrl { get; set; }

        [JsonProperty("RequiresLicenseAcceptance")]
        public bool RequiresLicenseAcceptance { get; set; }

        [JsonProperty("LastEdited")]
        public System.DateTimeOffset? LastEdited { get; set; }

        [JsonProperty("ReleaseNotes")]
        public string ReleaseNotes { get; set; }

        [JsonProperty("MinClientVersion")]
        public string MinClientVersion { get; set; }

        [JsonProperty("Language")]
        public string Language { get; set; }
    }
}

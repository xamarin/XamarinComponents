using CheckNuGets.Data.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckNuGets.Data
{
    public class ProcessResult
    {
        public string PackageId { get; set; }

        public string PackageTitle { get; set; }

        public string CurrentVersion { get; set; }

        public bool IsSigned { get; set; }

        public long TotalVersions { get; set; }

        public long TotalDownloads { get; set; }

        public UrlResults UrlResult { get; set; }
    }
}

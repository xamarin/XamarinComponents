using NugetAuditor.Core.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetAuditor.Core
{
    public class ProcessResult
    {

        public string PackageId { get; set; }

        public string PackageTitle { get; set; }

        public string CurrentVersion { get; set; }

        public DateTime DatePublished { get; set; }

        public bool IsSigned { get; set; }

        public long TotalVersions { get; set; }

        public long TotalDownloads { get; set; }

        public bool ProjectUrlIsValid { get; set; }

        public bool LicenceUrlIsValid { get; set; }

        public bool IconUrlIsValid { get; set; }

        public bool ProjectUrlIsFWLink { get; set; }

        public bool LicenceUrlIsFWLink { get; set; }

        public bool HasMicrosoftOwner { get; set; }

        public string Owners { get; set; }

        public bool ProjectUrlPointsToComponentsStore { get; set; }

        public bool LicenceUrlPointsToComponentsStore { get; set; }

        public string Copyright { get; set; }

        public bool IsValidCopyright { get; set; }
    }
}

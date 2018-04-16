using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetAuditor.Core.Results
{
    public class UrlResults
    {
        public bool ProjectUrlIsValid { get; set; }
        public bool LicenceUrlIsValid { get; set; }
        public bool IconUrlIsValid { get; set; }

        public bool ProjectUrlIsFWLink { get; set; }
        public bool LicenceUrlIsFWLink { get; set; }
    }
}

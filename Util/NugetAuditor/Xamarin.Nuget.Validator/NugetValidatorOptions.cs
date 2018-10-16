using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Nuget.Validator
{
    public class NugetValidatorOptions
    {
        public string Copyright { get; set; }

        public string Author { get; set; }

        public string Owner { get; set; }

        public string CertificatePublicKey { get; set; }

        public bool NeedsProjectUrl { get; set; }

        public bool NeedsLicenseUrl { get; set; }

        public bool ValidateRequireLicenseAcceptance { get; set; }

        public string[] ValidPackageNamespace { get; set; }

    }
}

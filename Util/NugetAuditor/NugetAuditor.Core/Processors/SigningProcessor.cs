//using Mono.Security.Authenticode;
//using Mono.Security.X509;
using NugetAuditor.Core.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NugetAuditor.Core.Processors
{
    public static class SigningProcessor
    {
        public static bool IsTrusted(string filePath)
        {
            try
            {

                var cert = X509Certificate.CreateFromSignedFile(filePath);

                var pubKey = cert.GetPublicKeyString();
                var isTrusted = PubkeyHelpers.PubKeys.Contains(pubKey);

                return isTrusted;
            }
            catch (Exception)
            {

                throw;
            }
  
        }


    }
}

using Mono.Security.Authenticode;
using Mono.Security.X509;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

               // AuthenticodeTools.IsTrusted(filePath)

                AuthenticodeDeformatter a = new AuthenticodeDeformatter(filePath);

                var isTrusted = a.IsTrusted();

                return isTrusted;
            }
            catch (Exception)
            {

                throw;
            }
  
        }


    }
}

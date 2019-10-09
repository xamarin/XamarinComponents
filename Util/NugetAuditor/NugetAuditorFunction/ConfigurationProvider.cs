using NugetAuditor.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetAuditorFunction
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        public string GetAppSettingString(string key)
        {
            return Environment.GetEnvironmentVariable(key);
        }

        public string GetConnectionString(string connectionName)
        {
            return Environment.GetEnvironmentVariable(connectionName);
        }
    }
}

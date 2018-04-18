using NugetAuditor.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetAuditor
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        public string GetAppSettingString(string key)
        {
            if (!ConfigurationManager.AppSettings.AllKeys.Contains(key))
                throw new Exception($"No app setting named {key} could not be found");

            return ConfigurationManager.AppSettings[key];

        }

        public string GetConnectionString(string connectionName)
        {
            var conString = ConfigurationManager.ConnectionStrings[connectionName];

            if (conString == null)
                throw new Exception($"No connection named {connectionName} could not be found");

            return conString.ConnectionString;

        }
    }
}

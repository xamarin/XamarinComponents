using Microsoft.Extensions.Configuration;
using NugetAuditor.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace NugetAuditorNetcore
{
    public class ConfigurationProvider : NugetAuditor.Core.Interfaces.IConfigurationProvider
    {
        private static IConfiguration configuration;

        private IConfiguration Configuration
        {
            get
            {
                if (configuration == null)
                {
                    var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json");

                    configuration = builder.Build();
                }

                return configuration;

            }
        }

        public string GetAppSettingString(string key)
        {
            var str = Configuration[key] as String;

            if (string.IsNullOrWhiteSpace(str))
                throw new Exception($"Unable to find the connection string called: {key}");

            return str;
        }

        public string GetConnectionString(string connectionName)
        {
            
            var str = Configuration[connectionName] as String;

            if (string.IsNullOrWhiteSpace(str))
                throw new Exception($"Unable to find the connection string called: {connectionName}");

            return str;
        }
    }
}

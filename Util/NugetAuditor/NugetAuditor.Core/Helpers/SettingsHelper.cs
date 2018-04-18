using NugetAuditor.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetAuditor.Core.Helpers
{
    public class SettingsHelper
    {
        public static IConfigurationProvider ConfigurationProvider { get; set; }

        public static readonly DateTime CutOffDateTime = new DateTime(2018, 01, 15);


        public static string GetSettingString(string key)
        {
            var value = ConfigurationProvider.GetAppSettingString(key);

            if (value == null)
                throw new Exception($"no setting provided for key: {key}");

            return value;
        }
    }

}

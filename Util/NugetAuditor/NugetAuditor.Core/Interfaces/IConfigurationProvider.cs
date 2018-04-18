using System;
using System.Collections.Generic;
using System.Text;

namespace NugetAuditor.Core.Interfaces
{
    public interface IConfigurationProvider
    {
        string GetConnectionString(string connectionName);

        string GetAppSettingString(string key);

    }
}


using NugetAuditor.Core;
using NugetAuditor.Core.Helpers;
using System;
using System.Threading.Tasks;

namespace NugetAuditorNetcore
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            //setup
            SettingsHelper.ConfigurationProvider = new ConfigurationProvider();

            LogHelper.WriteLine("Initialising Database...");
            await AuditorDbContext.InitializeAsync();

            LogHelper.WriteLine("Setting up Nuget Search Service Api...");
            await NugetServiceIndex.SetupSearchApiAsync();

            LogHelper.WriteLine("Processing feed...");
            await NugetAuditRobot.ProcessAsync();
        }
    }
}

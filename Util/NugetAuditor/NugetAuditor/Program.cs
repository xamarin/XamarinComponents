using System.Threading.Tasks;
using NugetAuditor.Core;
using NugetAuditor.Core.Helpers;

namespace NugetAuditor
{
    class MainClass
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

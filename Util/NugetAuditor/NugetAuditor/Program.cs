using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;
using NugetAuditor.Data;
using NugetAuditor.Processors;
using System.Configuration;
using NugetAuditor.Helpers;

namespace NugetAuditor
{
    class MainClass
	{

		public static async Task Main(string[] args)
		{
            LogHelper.WriteLine("Initialising Database...");
            await AuditorDbContext.InitializeAsync();

            LogHelper.WriteLine("Setting up Nuget Search Service Api...");
            await NugetServiceIndex.SetupSearchApiAsync();

            LogHelper.WriteLine("Processing feed...");
            await NugetAuditRobot.ProcessAsync();
		}
	}


}

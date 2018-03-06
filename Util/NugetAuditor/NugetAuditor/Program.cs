using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;
using NugetAuditor.Data;
using NugetAuditor.Processors;

namespace NugetAuditor
{
    class MainClass
	{

		public static async Task Main(string[] args)
		{
            Console.WriteLine("Initialising Database...");
            await AuditorDbContext.InitializeAsync();

            Console.WriteLine("Setting up Nuget Search Service Api...");
            await NugetServiceIndex.SetupSearchApiAsync();

            Console.WriteLine("Processing feed...");
            await NugetAuditProcessor.ProcessAsync();
		}
	}


}

using NugetAuditor.Data;
using NugetAuditor.Processors;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetAuditor
{
    public class NugetAuditProcessor
    {
      
        public static async Task ProcessAsync()
        {
            Console.WriteLine("Loading nuget package meta data...");

            var fProcessor = new FeedProcessor();
            var packageData = await fProcessor.ProcessAsync();

            Console.WriteLine("Processing packages...");

            var results = new List<ProcessResult>();

            Parallel.ForEach<PackageData>(packageData, package =>
            {
                Console.WriteLine($"Processing: {package.Title}");

                using (var aProc = new PackageProcessor(package))
                {
                    var result = aProc.Process();

                    results.Add(result);
                }
            });

            //
            Console.WriteLine("Saving results to database...");

            using (var aDb = new AuditorDbContext())
            {
                //empty the old results
                aDb.Results.RemoveRange(aDb.Results);

                aDb.Results.AddRange(results);

                await aDb.SaveChangesAsync();
            }

            Console.WriteLine($"Processed: {results.Count} Packages");
            Console.ReadLine();

            
        }
            
    }
}

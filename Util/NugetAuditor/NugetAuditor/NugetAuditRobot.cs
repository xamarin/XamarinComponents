using NugetAuditor.Data;
using NugetAuditor.Helpers;
using NugetAuditor.Processors;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetAuditor
{
    public class NugetAuditRobot
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

            //check for failed packages, that haven't been signed but only after the cutoff date when signing was required
            var failedSignCheck = results.Where(x => x.IsSigned.Equals(false) 
                                            && x.DatePublished >= SettingsHelper.CutOffDateTime);

            if (failedSignCheck.Any())
            {
                var packageIds = failedSignCheck.Select(x => x.PackageId);

                SlackNotifier.Notify(packageIds.ToList());
            }


            Console.WriteLine($"Processed: {results.Count} Packages");
            Console.ReadLine();

            
        }
            
    }
}

using NugetAuditor.Core;
using NugetAuditor.Core.Helpers;
using NugetAuditor.Core.Processors;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetAuditor.Core
{
    public class NugetAuditRobot
    {
      
        public static async Task ProcessAsync()
        {
            try
            {
                LogHelper.WriteLine("Loading nuget package meta data...");

                var fProcessor = new FeedProcessor();
                var packageData = await fProcessor.ProcessQueryAsync();

                var searchData = await fProcessor.ProcessSearchAsync();


                LogHelper.WriteLine("Processing packages...");

                var results = new List<ProcessResult>();

                var options = new ParallelOptions()
                {
                    MaxDegreeOfParallelism = 2
                    
                };

                Parallel.ForEach<PackageData>(packageData, options, package =>
                {
                    LogHelper.WriteLine($"Processing: {package.Title}");

                    var sPackage = searchData.FirstOrDefault(x => x.PackageRegistration.Id.Equals(package.PackageId));

                    using (var aProc = new PackageProcessor(package, sPackage))
                    {
                        var result = aProc.Process();

                        results.Add(result);
                    }

                });

                //
                LogHelper.WriteLine("Saving results to database...");

                using (var aDb = new AuditorDbContext())
                {
                    //empty the old results
                    aDb.Results.RemoveRange(aDb.Results);
                    await aDb.SaveChangesAsync();

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


                LogHelper.WriteLine($"Processed: {results.Count} Packages");

                //Console.ReadLine();
            }
            catch (Exception ex)
            {
                var msg = ex.Message;

                if (ex.InnerException != null)
                    msg += Environment.NewLine + Environment.NewLine + ex.InnerException.Message;

                LogHelper.WriteLine(msg);


            }

        }
            
    }
}

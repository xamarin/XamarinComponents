using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NugetDelister.Helpers;

namespace NugetDelister.Processors
{
    public class NugetFeedProcessor
    {
        public async static Task ProcessAsync(Dictionary<string, string> dict, string nugetApiKey)
        {
            await NugetServiceIndex.SetupSearchApiAsync();

            var newItems = new Dictionary<string, List<string>>();

            var fProcessor = new FeedProcessor(nugetApiKey);

            foreach (var aDictKey in dict.Keys)
            {
                var packageId = aDictKey;
                var latestVersion = dict[aDictKey];

                var packageData = await fProcessor.ProcessQueryAsync(packageId);

                foreach (var aPackage in packageData)
                {
                    foreach (var aVer in aPackage.Versions)
                    {
                        if (!aVer.VersionString.Equals(latestVersion, StringComparison.OrdinalIgnoreCase))
                        {
                            List<string> items = null;

                            if (newItems.ContainsKey(aPackage.PackageId))
                            {
                                items = newItems[aPackage.PackageId];
                            }
                            else
                            {
                                items = new List<string>();

                                newItems.Add(aPackage.PackageId, items);
                            }

                            items.Add(aVer.VersionString);
                        }
                    }

                }
            }


            foreach (var aPkgId in newItems.Keys)
            {
                var vals = newItems[aPkgId];

                foreach (var aVersion in vals)
                {
                    await fProcessor.DelistAsync(aPkgId, aVersion);
                }


            }


        }

    }
}

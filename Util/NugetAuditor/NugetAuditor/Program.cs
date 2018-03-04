using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;
using CheckNuGets.Data;
using CheckNuGets.Processors;

namespace CheckNuGets
{
    class MainClass
	{
		List<PackageData> queryResponseData = new List<PackageData>();
		
		const int NumThreads = 5;

		public static void Main(string[] args)
		{
			new MainClass().DoWork().Wait();
		}

		async Task DoWork()
		{
            Console.WriteLine("Loading nuget package meta data...");


            var fProcessor = new FeedProcessor();
            queryResponseData = await fProcessor.ProcessAsync();


            Console.WriteLine("Processing packages...");

            var cq = new ConcurrentQueue<PackageData>(queryResponseData);

            var results = new List<ProcessResult>();

            var threads = new Task[NumThreads];
            for (int i = 0; i < NumThreads; i++)
            {
                threads[i] = Task.Run(async () =>
                {
                    while (true)
                    {
                        PackageData package;

                        if (!cq.TryDequeue(out package))
                            return true;

                        Console.WriteLine($"Processing: {package.Title}");

                        using (var aProc = new PackageProcessor(package))
                        {
                            
                            var result = await aProc.ProcessAsync();

                            
                            results.Add(result);
                        }

                    }
                });
            }

            Task.WaitAll(threads);

            Console.WriteLine($"Processed: {results.Count} Packages");
            Console.ReadLine();

        }

	}


}

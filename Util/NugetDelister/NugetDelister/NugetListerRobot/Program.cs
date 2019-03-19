using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NugetDelister.Helpers;
using NugetDelister.Processors;

namespace NugetListerRobot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Processing....");

            var dict = new Dictionary<string, List<string>>()
            {
                { "Xamarin.Firebase.Abt.",new List<string>() {"71.1501.0"} },
                 { "Xamarin.Firebase.Ads", new List<string>(){"71.1501.0" } },
                  { "Xamarin.Firebase.Ads.Lite.", new List<string>() {"71.1501.0"} },
            };

            var nugetApiKey = string.Empty;

            await NugetFeedProcessor.ProcessAsync(dict, nugetApiKey);
        }
    }
}


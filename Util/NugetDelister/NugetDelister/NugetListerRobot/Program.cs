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

            var dict = new Dictionary<string, string>()
            {
                { "Xamarin.Firebase.Abt.","71.1501.0" },
                 { "Xamarin.Firebase.Ads","71.1501.0" },
                  { "Xamarin.Firebase.Ads.Lite.","71.1501.0" },
            };

            await NugetFeedProcessor.ProcessAsync(dict, "");


        }
    }
}

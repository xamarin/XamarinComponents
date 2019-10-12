using AndroidBinderator;
using Mono.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Xamarin.AndroidBinderator.Tool
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var configs = new List<string>();
            var basePath = string.Empty;
            var shouldShowHelp = false;

            // thses are the available options, not that they set the variables
            var options = new OptionSet {
                { "c|config=", "JSON Config File.", n => configs.Add (n) },
                { "b|basepath=", "Default Base Path.", (string b) => basePath= b },
                { "h|help", "show this message and exit", h => shouldShowHelp = h != null },
            };

            List<string> extra;
            try
            {
                // parse the command line
                extra = options.Parse(args);
            }
            catch (OptionException e)
            {
                // output some error message
                System.Console.Write("android-binderator: ");
                System.Console.WriteLine(e.Message);
                System.Console.WriteLine("Try `android-binderator --help' for more information.");
                return;
            }

            if (shouldShowHelp)
            {
                options.WriteOptionDescriptions(System.Console.Out);
                return;
            }

            foreach (var config in configs)
            {
                var cfgs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BindingConfig>>(File.ReadAllText(config));

                foreach (var c in cfgs)
                {

                    if (string.IsNullOrEmpty(c.BasePath))
                    {
                        if (!string.IsNullOrEmpty(basePath))
                            c.BasePath = basePath;
                        else
                            c.BasePath = AppDomain.CurrentDomain.BaseDirectory;
                    }

                    await Engine.BinderateAsync(c);
                }
            }
        }
    }
}

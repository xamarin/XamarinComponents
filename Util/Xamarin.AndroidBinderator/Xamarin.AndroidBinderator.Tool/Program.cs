using AndroidBinderator;
using AndroidBinderator.Common;
using Mono.Options;
using Newtonsoft.Json;
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
            var logging = false;
            var shouldShowHelp = false;

            // thses are the available options, not that they set the variables
            var options = new OptionSet {
                { "c|config=", "JSON Config File.", n => configs.Add (n) },
                { "b|basepath=", "Default Base Path.", (string b) => basePath= b },
                { "l|logging", "Turn Logging on", l => logging  = l != null },
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

            if (logging)
            {
                Engine.Logger += Logger;
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

                    try
                    {
                        await Engine.BinderateAsync(c);
                    }
                    catch (AndroidBinderatorException ex)
                    {
                        Logger(LogLevel.Critical, ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Logger(LogLevel.Critical, ex.Message);
                        Logger(LogLevel.Debug, ex.ToString());
                    }
                }
            }
        }

        public static void Logger(LogLevel logLevel, string message)
        {
            ConsoleColor foregroundColor = default;
            ConsoleColor backgroundColor = default;
            string level = string.Empty;

            switch (logLevel)
            {
                case LogLevel.Debug:
                    foregroundColor = ConsoleColor.Blue;
                    level = "DEBG";
                    break;
                case LogLevel.Information:
                    foregroundColor = ConsoleColor.Green;
                    level = "INFO";
                    break;
                case LogLevel.Warning:
                    foregroundColor = ConsoleColor.Yellow;
                    level = "WARN";
                    break;
                case LogLevel.Error:
                    foregroundColor = ConsoleColor.Red;
                    level = "ERRO";
                    break;
                case LogLevel.Critical:
                    backgroundColor = ConsoleColor.Red;
                    foregroundColor = ConsoleColor.Yellow;
                    level = "CRIT";
                    break;
            }

            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;

            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] [{level}] {message}");
            Console.ResetColor();
        }
    }
}

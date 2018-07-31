using NugetAuditor.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetAuditor.Core.Helpers
{
    public class LogHelper
    {
        private static ILogger _logger;

        public static ILogger Logger
        {
            get { return _logger; }
            set { _logger = value; }
        }

        public static void WriteLine(string message)
        {
            if (Logger == null)
            {
                Console.WriteLine(message);
            }
            else
            {
                Logger.WriteLine(message);
            }
            
        }
    }
}

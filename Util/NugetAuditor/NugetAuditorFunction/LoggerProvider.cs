using Microsoft.Azure.WebJobs.Host;
using NugetAuditor.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetAuditorFunction
{
    public class LoggerProvider : ILogger
    {
        TraceWriter _log;

        public LoggerProvider(TraceWriter log)
        {
            _log = log;
        }
        public void WriteLine(string message)
        {
            _log.Info(message);
        }
    }
}

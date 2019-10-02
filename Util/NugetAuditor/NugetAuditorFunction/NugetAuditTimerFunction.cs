using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace NugetAuditorFunction
{
    public static class Function1
    {
        //[FunctionName("NugetAuditTimerFunction")]
        public static void Run([TimerTrigger("0 */60 * * * *")]TimerInfo myTimer, TraceWriter log)
        {
            log.Info($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}

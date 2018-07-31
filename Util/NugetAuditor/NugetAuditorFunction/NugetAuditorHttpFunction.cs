using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Configuration;
using NugetAuditor.Core;
using NugetAuditor.Core.Helpers;

namespace NugetAuditorFunction
{
    public static class NugetAuditorHttpFunction
    {
        [FunctionName("NugetAuditorHttpFunction")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log, ExecutionContext context)
        {
            SettingsHelper.ConfigurationProvider = new ConfigurationProvider();
            LogHelper.Logger = new LoggerProvider(log);

            var config = SettingsHelper.ConfigurationProvider.GetConnectionString("dbConn");

            LogHelper.WriteLine("Initialising Database...");
            await AuditorDbContext.InitializeAsync();

            LogHelper.WriteLine("Setting up Nuget Search Service Api...");
            await NugetServiceIndex.SetupSearchApiAsync();

            LogHelper.WriteLine("Processing feed...");
            await NugetAuditRobot.ProcessAsync();

            return req.CreateResponse(HttpStatusCode.OK, "Boom!");
        }
    }
}

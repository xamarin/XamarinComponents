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

            var config = SettingsHelper.ConfigurationProvider.GetConnectionString("dbConn");

            //log.Info($"db con string: {config}");

            //log.Info("C# HTTP trigger function processed a request.");

            log.Info("Initialising Database...");
            await AuditorDbContext.InitializeAsync();

            log.Info("Setting up Nuget Search Service Api...");
            await NugetServiceIndex.SetupSearchApiAsync();

            log.Info("Processing feed...");
            await NugetAuditRobot.ProcessAsync();

            return req.CreateResponse(HttpStatusCode.OK, "Boom!");

            //// parse query parameter
            //string name = req.GetQueryNameValuePairs()
            //    .FirstOrDefault(q => string.Compare(q.Key, "name", true) == 0)
            //    .Value;

            //if (name == null)
            //{
            //    // Get request body
            //    dynamic data = await req.Content.ReadAsAsync<object>();
            //    name = data?.name;
            //}

            //return name == null
            //    ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body")
            //    : req.CreateResponse(HttpStatusCode.OK, "Hello " + name);
        }
    }
}

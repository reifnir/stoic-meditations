/*
IF Missing value for AzureWebJobsStorage in local.settings.json:
Base configuration needed for local emulation:
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true"
  }
}  
 */


using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace StoicMeditations.AzureFunctions.Fn
{
    public static class ExampleDurableFunction
    {
        [FunctionName("ExampleDurableFunction")]
        public static async Task<List<string>> RunOrchestrator(
            [OrchestrationTrigger] DurableOrchestrationContext context)
        {
            var tasks = new List<Task<string>>();

            Enumerable.Range(1, 10).ToList().ForEach(index =>
            {
                //we're going to find the hash code because this operation is deterministic
                var task = context.CallActivityAsync<string>("ExampleDurableFunction_FindHashCodeActivity", index.ToString());
                tasks.Add(task);
            });

            await Task.WhenAll(tasks);

            var results = tasks.Select(x => x.Result).ToList();
            return results;
        }

        //stop showing up in the list of URIs
        //[FunctionName("ExampleDurableFunction_FindHashCodeActivity")]
        public static string SayHello([ActivityTrigger] string value, ILogger log)
        {
            var result = value.GetHashCode();
            log.LogInformation($"ExampleDurableFunction_FindHashCodeActivity: HashCode of {value} = {result}.");
            return $"HashCode of {value} = {result}";
        }

        //[FunctionName("ExampleDurableFunction_HttpStart")]
        public static async Task<HttpResponseMessage> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")]HttpRequestMessage req,
            [OrchestrationClient]DurableOrchestrationClient starter,
            ILogger log)
        {
            // Function input comes from the request content.
            string instanceId = await starter.StartNewAsync("ExampleDurableFunction", null);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using StoicMeditations.AzureFunctions.Models;

namespace StoicMeditations.AzureFunctions
{
    public static class ShowAllTransformedAnchorPodcasts
    {
        [FunctionName(nameof(ShowAllTransformedAnchorPodcasts))]
        public static async Task<Results<Podcast>> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get")]
            HttpRequest req,
            ILogger log)
        {
            string url = "x";
            var http = new HttpClient();
            log.LogInformation($"Getting accessor for raw json data from anchor podcast. Calling {nameof(ShowAllRawAnchorPodcasts)}");
            var podcastResponse = await http.GetAsync(url);
            log.LogInformation("Reading contents of podcast data");
            var rawBody = await podcastResponse.Content.ReadAsStringAsync();
            0/0
            return new Results<Podcast>(new Podcast());



            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            return name != null
                ? (ActionResult)new OkObjectResult($"Hello, {name}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}

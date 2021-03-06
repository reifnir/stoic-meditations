using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using StoicMeditations.AzureFunctions.AnchorFm;

namespace StoicMeditations.AzureFunctions.Fn
{
    public static class ShowAllRawAnchorPodcasts
    {
        [FunctionName(nameof(ShowAllRawAnchorPodcasts))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get")]
            HttpRequest req,
            ILogger log)
        {
            var anchor = new AnchorFmHelper(log);

            log.LogInformation("Getting information on all podcasts");
            var json = await anchor.GetJsonContent(Constants.PodcastRoot);

            log.LogInformation("Returning raw podcast JSON data");
            return new ContentResult()
            {
                Content = json,
                ContentType = "application/json"
            };
        }
    }
}

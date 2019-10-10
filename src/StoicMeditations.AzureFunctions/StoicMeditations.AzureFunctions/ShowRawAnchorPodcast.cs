using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StoicMeditations.AzureFunctions.AnchorFm;

namespace StoicMeditations.AzureFunctions.Fn
{
    public static class ShowRawAnchorPodcast
    {
        [FunctionName(nameof(ShowRawAnchorPodcast))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = nameof(ShowRawAnchorPodcast) + "/{anchorPodcastId}")]
            HttpRequest req,
            string anchorPodcastId,
            ILogger log)
        {
            var anchor = new AnchorFmHelper(log);

            log.LogInformation($"Getting information on podcast: {anchorPodcastId}");
            var json = await anchor.GetJsonContent($"{Constants.PodcastRoot}episodes/{anchorPodcastId}");

            log.LogInformation("Returning raw podcast JSON data");
            return new ContentResult()
            {
                Content = json,
                ContentType = "application/json"
            };
        }
    }
}

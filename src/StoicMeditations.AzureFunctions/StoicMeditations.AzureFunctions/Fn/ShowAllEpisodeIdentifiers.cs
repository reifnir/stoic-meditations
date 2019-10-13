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
using StoicMeditations.AzureFunctions.AnchorFm;
using System.Collections.Generic;

namespace StoicMeditations.AzureFunctions.Fn
{
    public static class ShowAllEpisodeIdentifiers
    {
        [FunctionName(nameof(ShowAllEpisodeIdentifiers))]
        public static async Task<List<string>> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get")]
            HttpRequest req,
            ILogger log)
        {
            var anchor = new AnchorFmHelper(log);
            var results = await anchor.GetAllPodcastEpisodeIds();
            return results;
        }
    }
}

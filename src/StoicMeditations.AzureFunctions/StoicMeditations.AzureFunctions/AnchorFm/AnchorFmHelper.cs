using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StoicMeditations.AzureFunctions.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StoicMeditations.AzureFunctions.AnchorFm
{
    internal class AnchorFmHelper
    {
        private readonly ILogger log;
        public AnchorFmHelper(ILogger logger)
        {
            log = logger;
        }
        public async Task<List<string>> GetAllPodcastEpisodeIds()
        {
            var rawPodcasts = await GetPodcastList();
            var foundResults = rawPodcasts
                ?.EpisodePreview
                ?.Episodes
                ?.Select(x => x?.ShareLinkPath?.Replace(Constants.PodcastEpisodePrefix, ""))
                ?.ToList();

            if (foundResults != null)
                return foundResults;

            if (rawPodcasts == null)
                log.LogWarning("Result from GetPodcastList was null.");
            else if (rawPodcasts.EpisodePreview == null)
                log.LogWarning("Result from GetPodcastList.EpisodePreview was null.");
            else if (rawPodcasts.EpisodePreview.Episodes == null)
                log.LogWarning("Result from GetPodcastList.EpisodePreview.Episodes was null.");

            return new List<string>();
        }

        #region Internal methods (for testing purposes)
        internal async Task<RawPodcastList> GetPodcastList()
        {
            log.LogInformation("Getting information on all podcasts");
            var json = await GetJsonContent(Constants.PodcastRoot);
            var rawPodcasts = JsonConvert.DeserializeObject<RawPodcastList>(json);
            return rawPodcasts;
        }
        internal async Task<string> GetJsonContent(string url)
        {
            var content = await GetContent(url);

            log.LogInformation("Extracting all but the JSON data");
            var justData = ExtractJsonData(content);
            return justData;
        }
        #endregion
        
        #region Private methods
        private async Task<string> GetContent(string url)
        {
            var http = new HttpClient();

            log.LogInformation($"Getting info from url: {url}");
            var podcastResponse = await http.GetAsync(url);

            log.LogInformation("Reading content from response");
            var rawBody = await podcastResponse.Content.ReadAsStringAsync();
            return rawBody;
        }
        private static string ExtractJsonData(string rawBody)
        {
            string exp = " window.__STATE__ = (?<justData>.*);";
            var regex = new Regex(exp);
            var match = regex.Match(rawBody);
            if (!match.Success)
                return "No regex match";

            var group = match.Groups["justData"];
            if (!group.Success)
                return "No regex match for group 'justData'";
            return group.Value;
        }
        #endregion
    }
}

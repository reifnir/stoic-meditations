using StoicMeditations.AzureFunctions.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace StoicMeditations.AzureFunctions.Transform
{
    internal class Converter
    {
        public List<Podcast> To(RawPodcastEpisode raw)
        {
            return null;
            /*
            var converted = raw?.EpisodePreview?.Episodes?
                .Select(x => new Podcast()
                {
                    Created = x.Created,
                    IsDeleted = x.IsDeleted,
                    Modified = x.Modified,
                    PublishOn = x.PublishOn,
                    EpisodeUuid = x.PodcastEpisodeUuid,
                    SeasonNumber = null,
                    Description = x.Description,
                    DescriptionPreview = x.DescriptionPreview,
                    Duration = x.Duration,
                    EpisodeId = x.EpisodeId,
                    DetailId = null,
                    Title = x.Title

                }).ToList();
            return converted;*/
        }
    }
}

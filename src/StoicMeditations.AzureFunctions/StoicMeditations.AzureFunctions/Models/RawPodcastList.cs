using System;
using System.Collections.Generic;
using System.Text;

namespace StoicMeditations.AzureFunctions.Models
{
    internal class RawPodcastList
    {
        public RawEpisodePreviewSection EpisodePreview { get; set; }



        internal class RawEpisodePreviewSection
        {
            public RawEpisode[] Episodes { get; set; }
        }
        internal class RawEpisode
        {
            public DateTime? Created { get; set; }
            public int? CreatedUnitTimestamp { get; set; }
            public bool? IsDeleted { get; set; }
            public DateTime? Modified { get; set; }
            public DateTime? PublishOn { get; set; }
            public int? PublishOnUnitTimestamp { get; set; }
            public int? HourOffset { get; set; }
            public bool? PodcastEpisodeIsExplicit { get; set; }
            public string PodcastEpisodeType { get; set; }
            public Guid PodcastEpisodeUuid { get; set; }
            public string Description { get; set; }
            public string DescriptionPreview { get; set; }
            public int? Duration { get; set; }
            public string EpisodeId { get; set; }
            public string ShareLinkPath { get; set; }
            public string ShareLinkEmbedPath { get; set; }
            public string StationId { get; set; }
            public string Title { get; set; }
        }
    }
}

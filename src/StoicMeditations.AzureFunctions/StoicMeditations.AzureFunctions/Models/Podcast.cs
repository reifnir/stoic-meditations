using System;
using System.Collections.Generic;
using System.Text;

namespace StoicMeditations.AzureFunctions.Models
{
    public class Podcast
    {
        public DateTime Created { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Modified { get; set; }
        public DateTime PublishOn { get; set; }
        public Guid EpisodeUuid { get; set; }
        public int SeasonNumber { get; set; }
        //Note, this can contain HTML encoded into it (paragraphs to force carriage returns)
        public string Description { get; set; }
        public string DescriptionPreview { get; set; }
        public int Duration { get; set; }
        public string EpisodeId { get; set; }
        public string DetailId { get; set; }
        public string Title { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DrakeLambert.Peerra.Entities
{
    public class Issue : BaseEntity
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public bool IsSolved { get; set; } = false;

        public Guid TopicId { get; set; }

        public Topic Topic { get; set; }

        public Guid OwnerId { get; set; }

        public ApplicationUser Owner { get; set; }

        public List<HelpRequest> HelpRequests { get; set; }

        public int UnseenHelpRequestsCount => HelpRequests?.Count(helpRequest => !helpRequest.HasBeenViewed) ?? 0;
    }
}

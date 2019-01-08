using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DrakeLambert.Peerra.Entities
{
    public class Issue : BaseEntity
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public Guid TopicId { get; set; }

        public Topic Topic { get; set; }

        public Guid OwnerId { get; set; }

        public ApplicationUser Owner { get; set; }

        public List<HelpRequest> HelpRequests { get; set; }
    }

    public class HelpRequest : BaseEntity
    {
        public Guid IssueId { get; set; }

        public Issue Issue { get; set; }

        public Guid HelperId { get; set; }

        public ApplicationUser Helper { get; set; }

        public bool IsAccepted { get; set; }
    }
}

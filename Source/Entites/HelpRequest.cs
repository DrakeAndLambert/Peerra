using System;

namespace DrakeLambert.Peerra.Entities
{
    public class HelpRequest : BaseEntity
    {
        public HelpRequestStatus Status { get; set; } = HelpRequestStatus.Pending;

        public bool HasBeenViewed { get; set; } = false;

        public string Message { get; set; }

        public Guid IssueId { get; set; }

        public Issue Issue { get; set; }

        public Guid HelperId { get; set; }

        public ApplicationUser Helper { get; set; }
    }
}

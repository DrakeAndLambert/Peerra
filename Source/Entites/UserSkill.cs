using System;

namespace DrakeLambert.Peerra.Entities
{
    public class UserSkill
    {
        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; }

        public Guid IssueId { get; set; }

        public Issue Issue { get; set; }
    }
}

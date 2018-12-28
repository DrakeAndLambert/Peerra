using System;

namespace DrakeLambert.Peerra.Entities
{
    public class TopicRequest : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsLeaf { get; set; }

        public Guid ParentId { get; set; }

        public Topic Parent { get; set; }

        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}

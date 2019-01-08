using System;
using System.Collections.Generic;

namespace DrakeLambert.Peerra.Entities
{
    public class Topic : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsLeaf { get; set; } = true;

        public Guid ParentId { get; set; }

        public Topic Parent { get; set; }

        public bool Approved { get; set; } = false;

        public Guid OwnerId { get; set; }

        public ApplicationUser Owner { get; set; }

        public List<Topic> Children { get; set; }

        public List<UserTopic> UserTopics { get; set; }
    }
}

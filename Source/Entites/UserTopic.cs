using System;

namespace DrakeLambert.Peerra.Entities
{
    public class UserTopic
    {
        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; }

        public Guid TopicId { get; set; }

        public Topic Topic { get; set; }
    }
}

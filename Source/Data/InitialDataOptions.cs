using System;
using System.Linq;
using DrakeLambert.Peerra.Entities;

namespace DrakeLambert.Peerra.Data
{
    public class InitialDataOptions
    {
        public TopicOption[] Topics { get; set; }

        public UserOption[] Users { get; set; }
    }

    public class UserOption
    {
        public string UserName { get; set; }
    }

    public class TopicOption
    {
        public Guid? Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsLeaf { get; set; }

        public TopicOption[] Children { get; set; }

        public static implicit operator Topic(TopicOption topicOption)
        {
            return new Topic
            {
                Id = topicOption.Id.HasValue ? topicOption.Id : Guid.Empty,
                Title = topicOption.Title,
                Description = topicOption.Description,
                Children = topicOption.Children?.Select(io => (Topic)io).ToList(),
                IsLeaf = topicOption.IsLeaf,
                Approved = true
            };
        }
    }
}

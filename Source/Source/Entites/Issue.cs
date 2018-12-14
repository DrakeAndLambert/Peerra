using System;
using System.Collections.Generic;
using System.Linq;

namespace DrakeLambert.Peerra.Entities
{
    public class Issue : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public Guid ParentId { get; set; }

        public Issue Parent { get; set; }

        public List<Issue> Children { get; set; }

        public Issue(string title)
        {
            Title = title;
        }

        public Issue(string title, string description) : this(title)
        {
            Description = description;
        }

        public Issue(string title, IEnumerable<Issue> children) : this(title)
        {
            Children = children.ToList();
        }

        public Issue(string title, string description, IEnumerable<Issue> children) : this(title, children)
        {
            Description = description;
        }
    }
}

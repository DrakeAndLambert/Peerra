using System;
using System.Collections.Generic;

namespace DrakeLambert.Peerra.Entities
{
    public class Issue : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public Guid ParentId { get; set; }

        public Issue Parent { get; set; }

        public List<Issue> Children { get; set; }
    }
}

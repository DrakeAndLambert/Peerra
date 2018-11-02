using System;

namespace DrakeLambert.Peerra.WebApi.Core.Entities
{
    /// <summary>
    /// A skill that users can have. Advertised skills are used to make connections.
    /// </summary>
    public class Skill
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
    }
}
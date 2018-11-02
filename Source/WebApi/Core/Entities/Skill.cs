using System;
using Ardalis.GuardClauses;

namespace DrakeLambert.Peerra.WebApi.Core.Entities
{
    /// <summary>
    /// A skill that users can have. Advertised skills are used to make connections.
    /// </summary>
    public class Skill : IEntity<Guid>
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public Skill(Guid id, string name)
        {
            Guard.Against.NullOrWhiteSpace(name, nameof(name));

            Id = id;
            Name = name;
        }
    }
}
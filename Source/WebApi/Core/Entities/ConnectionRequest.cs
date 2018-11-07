using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Ardalis.GuardClauses;

namespace DrakeLambert.Peerra.WebApi.Core.Entities
{
    /// <summary>
    /// A peer's request to connect.
    /// </summary>
    public class ConnectionRequest : IEntity<Guid>
    {
        /// <summary>
        /// The unique id of the connection.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// The peer mentoree of the connection.
        /// </summary>
        public ApplicationUser Requestor { get; private set; }

        /// <summary>
        /// The skills requested by the mentoree.
        /// </summary>
        public ReadOnlyCollection<Skill> RequestedSkills => _requestedSkills.AsReadOnly();

        private List<Skill> _requestedSkills;

        public ConnectionRequest(Guid id, ApplicationUser requestor, IEnumerable<Skill> requestedSkills)
        {
            Guard.Against.Null(requestor, nameof(requestor));
            Guard.Against.Null(requestedSkills, nameof(requestedSkills));

            Id = id;
            Requestor = requestor;
            if (requestedSkills is List<Skill> requestedSkillsList)
            {
                _requestedSkills = requestedSkillsList;
            }
            _requestedSkills = requestedSkills.ToList();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Ardalis.GuardClauses;

namespace DrakeLambert.Peerra.WebApi.Core.Entities
{
    /// <summary>
    /// An established connection between two users.
    /// </summary>
    public class Connection : IEntity<(Guid, Guid)>
    {
        /// <summary>
        /// The unique id of this conneciton.
        /// </summary>
        public (Guid, Guid) Id => (Mentor.Id, Mentoree.Id);

        /// <summary>
        /// The peer mentor of the connection.
        /// </summary>
        public Peer Mentor { get; private set; }

        /// <summary>
        /// The peer mentoree of the connection.
        /// </summary>
        public Peer Mentoree { get; private set; }

        /// <summary>
        /// The intersection of skills requested by the mentoree and advertised by the mentor.
        /// </summary>
        public ReadOnlyCollection<Skill> CommonSkills => _commonSkills.AsReadOnly();

        private List<Skill> _commonSkills;

        public Connection(Peer mentor, Peer mentoree, IEnumerable<Skill> commonSkills)
        {
            Guard.Against.Null(mentor, nameof(mentor));
            Guard.Against.Null(mentoree, nameof(mentoree));
            Guard.Against.Null(commonSkills, nameof(commonSkills));

            Mentor = mentor;
            Mentoree = mentoree;
            if (commonSkills is List<Skill> commonSkillsList)
            {
                _commonSkills = commonSkillsList;
            }
            _commonSkills = commonSkills.ToList();
        }
    }
}
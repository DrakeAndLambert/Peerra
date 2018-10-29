using System.Collections.Generic;

namespace DrakeLambert.Peerra.Core.Entities
{
    /// <summary>
    /// An established connection between two users.
    /// </summary>
    public class Connection
    {
        /// <summary>
        /// The peer mentor of the connection.
        /// </summary>
        public Peer Mentor { get; set; }

        /// <summary>
        /// The peer mentoree of the connection.
        /// </summary>
        public Peer Mentoree { get; set; }

        /// <summary>
        /// The intersection of skills requested by the mentoree and advertised by the mentor.
        /// </summary>
        public List<Skill> CommonSkills { get; set; }
    }
}
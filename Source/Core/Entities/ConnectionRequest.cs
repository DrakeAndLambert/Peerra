using System.Collections.Generic;

namespace DrakeLambert.Peerra.Core.Entities
{
    /// <summary>
    /// A peer's request to connect.
    /// </summary>
    public class ConnectionRequest
    {
        /// <summary>
        /// The peer mentoree of the connection.
        /// </summary>
        public Peer Requestor { get; set; }

        /// <summary>
        /// The skills requested by the mentoree.
        /// </summary>
        public List<Skill> RequestedSkills { get; set; }
    }
}
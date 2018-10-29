using System;
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
        public Peer Mentor { get; }

        /// <summary>
        /// The peer mentoree of the connection.
        /// </summary>
        public Peer Mentoree { get; }

        /// <summary>
        /// The intersection of skills requested by the mentoree and advertised by the mentor.
        /// </summary>
        /// <value>A read only collection of skills.</value>
        public IReadOnlyCollection<Skill> CommonSkills { get => _commonSkills.AsReadOnly(); }

        /// <summary>
        /// The intersection of skills requested by the mentoree and advertised by the mentor.
        /// </summary>
        private List<Skill> _commonSkills;

        public Connection(ConnectionRequest connectionRequest, Peer Mentor)
        {
            // TODO: implement this
        }
    }

    /// <summary>
    /// A skill that users can have. Advertised skills are used to make connections.
    /// </summary>
    public class Skill
    {
        public Guid Id { get; }

        public string Title { get; }

        public 
    }

    /// <summary>
    /// A peer's request to connect.
    /// </summary>
    public class ConnectionRequest
    {
        /// <summary>
        /// The peer mentoree of the connection.
        /// </summary>
        public Peer Requestor { get; }

        /// <summary>
        /// The skills requested by the mentoree.
        /// </summary>
        /// <value>A read only collection of skills.</value>
        public IReadOnlyCollection<Skill> RequestedSkills { get => _requestedSkills.AsReadOnly(); }

        /// <summary>
        /// The skills requested by the mentoree.
        /// </summary>
        private List<Skill> _requestedSkills;

        public ConnectionRequest(Peer requestor, IEnumerable<Skill> requestedSkills)
    }
}
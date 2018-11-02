using System;

namespace DrakeLambert.Peerra.WebApi.Core.Entities
{
    /// <summary>
    /// A user of the Peerra system, referred to as a Peer.
    /// </summary>
    public class Peer : IEntity<Guid>
    {
        /// <summary>
        /// The unique identifier of the peer.
        /// </summary>
        public Guid Id { get; private set; }

        public Peer(Guid id)
        {
            Id = id;
        }
    }
}
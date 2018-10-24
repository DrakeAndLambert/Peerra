using System;
using Ardalis.GuardClauses;

namespace DrakeLambert.Peerra.Core.Entities
{
    /// <summary>
    /// A user of the Peerra system, referred to as a Peer.
    /// </summary>
    public class Peer
    {
        /// <summary>
        /// The unique identifier of the peer.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Constructs a new peer.
        /// </summary>
        /// <param name="id">The id of the peer. Must not be null.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when id is null.
        /// </exception>
        public Peer(string id)
        {
            Guard.Against.Null(id, nameof(id));
            Id = id;
        }
    }
}
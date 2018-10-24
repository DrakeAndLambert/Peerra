using System;
using Ardalis.GuardClauses;

namespace DrakeLambert.Peerra.Core.Entities
{
    /// <summary>
    /// A user of the Peerra system.
    /// </summary>
    public class User
    {
        /// <summary>
        /// The unique identifier of the user.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Constructs a new user.
        /// </summary>
        /// <param name="id">The id of the user. Must not be null.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when id is null.
        /// </exception>
        public User(string id)
        {
            Guard.Against.Null(id, nameof(id));
            Id = id;
        }
    }
}
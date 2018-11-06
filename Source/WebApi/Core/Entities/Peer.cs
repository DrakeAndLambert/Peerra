using System;
using Microsoft.AspNetCore.Identity;

namespace DrakeLambert.Peerra.WebApi.Core.Entities
{
    /// <summary>
    /// A user of the Peerra system, referred to as a Peer.
    /// </summary>
    public class Peer : IdentityUser<Guid>, IEntity<Guid>
    { }
}
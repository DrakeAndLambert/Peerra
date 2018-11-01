using DrakeLambert.Peerra.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DrakeLambert.Peerra.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Peer> Peers { get; set; }
        
        public DbSet<Connection> Connections { get; set; }
        
        public DbSet<ConnectionRequest> ConnectionRequests { get; set; }
        
        public DbSet<Skill> Skills { get; set; }

        
    }
}
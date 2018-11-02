using DrakeLambert.Peerra.WebApi.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DrakeLambert.Peerra.WebApi.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Connection> Connections { get; set; }

        public DbSet<ConnectionRequest> ConnectionRequests { get; set; }

        public DbSet<Peer> Peers { get; set; }

        public DbSet<Skill> Skills { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
    }
}
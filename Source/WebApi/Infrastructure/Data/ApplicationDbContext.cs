using System;
using DrakeLambert.Peerra.WebApi.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DrakeLambert.Peerra.WebApi.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, Guid>
    {
        // public DbSet<Connection> Connections { get; set; }

        // public DbSet<ConnectionRequest> ConnectionRequests { get; set; }

        public DbSet<ApplicationUser> Peers { get; set; }

        // public DbSet<Skill> Skills { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<ApplicationUser>().HasKey(peer => peer.)
        }
    }
}
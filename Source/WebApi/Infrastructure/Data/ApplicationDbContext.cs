using System;
using DrakeLambert.Peerra.WebApi.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DrakeLambert.Peerra.WebApi.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityUserContext<ApplicationUser, Guid>
    {
        // public DbSet<Connection> Connections { get; set; }

        // public DbSet<ConnectionRequest> ConnectionRequests { get; set; }

        // public DbSet<Skill> Skills { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder model)
        { }
    }
}
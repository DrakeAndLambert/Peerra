using System;
using DrakeLambert.Peerra.WebApi.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DrakeLambert.Peerra.WebApi.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityUserContext<ApplicationUser, Guid>
    {
        public DbSet<Connection> Connections { get; set; }

        public DbSet<Connection> ConnectionRequests { get; set; }

        public DbSet<Skill> Skills { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<Connection>(connection =>
            {
                connection.HasKey(e => new { Mentor = e.Mentor.Id, Mentoree = e.Mentoree.Id });

                connection.HasOne(e => e.Mentor).WithMany();

                connection.HasOne(e => e.Mentoree).WithMany();

                connection.HasMany(e => e.CommonSkills);
            });
        }
    }
}

using System;
using DrakeLambert.Peerra.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DrakeLambert.Peerra.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public DbSet<Issue> Issues { get; set; }

        public DbSet<UserSkill> UserSkills { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserSkill>(userSkill =>
            {
                userSkill.HasKey(us => new { us.UserId, us.IssueId });
                userSkill.HasIndex(us => us.UserId);
                userSkill.HasIndex(us => us.IssueId);
            });

            base.OnModelCreating(builder);
        }
    }
}

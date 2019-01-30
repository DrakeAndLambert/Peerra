using System;
using DrakeLambert.Peerra.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DrakeLambert.Peerra.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public DbSet<Topic> Topics { get; set; }

        public DbSet<UserTopic> UserTopics { get; set; }

        public DbSet<Issue> Issues { get; set; }

        public DbSet<HelpRequest> HelpRequests { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Topic>().HasMany(t => t.Children).WithOne(t => t.Parent).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Issue>().HasOne(i => i.Topic).WithMany().OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserTopic>().HasOne(u => u.Topic).WithMany(t => t.UserTopics).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<UserTopic>().HasOne(u => u.User).WithMany().OnDelete(DeleteBehavior.Restrict);

            builder.Entity<HelpRequest>().HasOne(h => h.Helper).WithMany().OnDelete(DeleteBehavior.Restrict);
            builder.Entity<HelpRequest>().HasOne(h => h.Issue).WithMany(i => i.HelpRequests).OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}

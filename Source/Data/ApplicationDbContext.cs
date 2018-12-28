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

        public DbSet<TopicRequest> TopicRequests { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserTopic>(userSkill =>
            {
                userSkill.HasKey(us => new { us.UserId, us.TopicId });
                userSkill.HasIndex(us => us.UserId);
                userSkill.HasIndex(us => us.TopicId);
            });

            base.OnModelCreating(builder);
        }
    }
}

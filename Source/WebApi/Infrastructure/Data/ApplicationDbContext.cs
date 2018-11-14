using DrakeLambert.Peerra.WebApi.Core.Entities;
using DrakeLambert.Peerra.WebApi.WebCore.Authentication.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DrakeLambert.Peerra.WebApi.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityUserContext<WebUser>
    {
        public DbSet<User> AppUsers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<RefreshToken>().HasKey(token => new { token.Token, token.Username });

            model.Entity<User>().HasKey(user => user.Username);

            base.OnModelCreating(model);
        }
    }
}

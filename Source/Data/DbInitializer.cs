using System.Linq;
using DrakeLambert.Peerra.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DrakeLambert.Peerra.Data
{
    public class DbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DbInitializer> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly InitialDataOptions _initialData;

        public DbInitializer(ApplicationDbContext context, IOptions<InitialDataOptions> initialData, ILogger<DbInitializer> logger, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _initialData = initialData.Value;
            _logger = logger;
            _userManager = userManager;
        }

        public void Seed()
        {
            _context.Issues.RemoveRange(_context.Issues);

            _logger.LogInformation("Adding {count} top level issues.", _initialData.Issues.Length);

            _context.Issues.AddRange(_initialData.Issues.Select(io => (Issue)io));
            _context.SaveChanges();

            foreach (var user in _initialData.Users)
            {
                var applicationUser = new ApplicationUser
                {
                    Email = $"{user.UserName}@email.com",
                    EmailConfirmed = true
                };
                applicationUser.UserName = applicationUser.Email;
                _userManager.CreateAsync(applicationUser, "Password1!").GetAwaiter().GetResult();
            }
        }
    }
}
/*

Knowing when and how to schedule advising appointments
finding interesting easy a electives
good and bad professors


            "A Professor",
            "Choosing Courses",
            "Choosing Majors",
            "Residential Life",
 */

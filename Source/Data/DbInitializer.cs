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
        private readonly InitialDataOptions _initialIssueTree;

        public DbInitializer(ApplicationDbContext context, IOptions<InitialDataOptions> initialData, ILogger<DbInitializer> logger, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _initialIssueTree = initialData.Value;
            _logger = logger;
            _userManager = userManager;
        }

        public void Seed()
        {
            _context.Issues.RemoveRange(_context.Issues);

            _logger.LogInformation("Adding {count} top level issues.", _initialIssueTree.Issues.Length);

            _context.Issues.AddRange(_initialIssueTree.Issues.Select(io => (Issue)io));
            _context.SaveChanges();

            
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

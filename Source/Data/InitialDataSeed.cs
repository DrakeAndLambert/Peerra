using System.Linq;
using DrakeLambert.Peerra.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DrakeLambert.Peerra.Data
{
    public class InitialDataSeed
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<InitialDataSeed> _logger;
        private readonly InitialIssueTreeOptions _initialIssueTree;

        public InitialDataSeed(ApplicationDbContext context, IOptions<InitialIssueTreeOptions> initialIssueTree, ILogger<InitialDataSeed> logger)
        {
            _context = context;
            _logger = logger;
            _initialIssueTree = initialIssueTree.Value;
        }

        public void Seed()
        {
            _context.Issues.RemoveRange(_context.Issues);

            _logger.LogInformation("Adding {count} top level issues.", _initialIssueTree.InitialIssueTree.Length);

            _context.Issues.AddRange(_initialIssueTree.InitialIssueTree.Select(io => (Issue)io));
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

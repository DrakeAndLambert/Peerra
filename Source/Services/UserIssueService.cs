using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrakeLambert.Peerra.Data;
using DrakeLambert.Peerra.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DrakeLambert.Peerra.Services
{
    public class UserIssueService : IUserIssueService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserIssueService> _logger;

        public UserIssueService(ApplicationDbContext context, ILogger<UserIssueService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Issue>> GetImportantIssuesAsync(ApplicationUser user)
        {
            var userIssues = _context.Issues
                // Get issues belonging to user.
                .Where(issue => issue.Owner == user)
                // Filter unsolved issues
                .Where(issue => !issue.IsSolved)
                // Get help requests for each issue
                .GroupJoin(
                    // Join to help requests
                    _context.HelpRequests
                        // Only get help requests that have not been seen
                        .Where(helpRequest => !helpRequest.HasBeenViewed),
                    // Match issue Id on issue...
                    issue => issue.Id,
                    // ...with issue Id on help request
                    helpRequest => helpRequest.IssueId,
                    // select both issue and associated help requests
                    (issue, helpRequests) => new { Issue = issue, HelpRequests = helpRequests }
                )
                // Order by number of help requests descending
                .OrderByDescending(issue => issue.HelpRequests.Count())
                // Reduce selection to just the issues
                .Select(issue => issue.Issue)
                // Include topics and help requests
                .Include(issue => issue.HelpRequests).Include(issue => issue.Topic);

            return await userIssues.ToListAsync();
        }

        public async Task<List<Issue>> GetOthersIssuesAsync(ApplicationUser user)
        {
            return await _context.Issues
                .Include(i => i.HelpRequests)
                .Include(i => i.Topic)
                .Where(i => i.OwnerId != user.Id)
                .Where(i => !i.IsSolved)
                .Where(i => !i.HelpRequests.Any(hr => hr.HelperId == user.Id))
                .OrderBy(i => i.HelpRequests.Count(hr => hr.Status == HelpRequestStatus.Responded))
                .ToListAsync();
        }

        public async Task<List<Issue>> GetOthersTargetedIssuesAsync(ApplicationUser user)
        {
            return await _context.Issues
                .Include(i => i.HelpRequests)
                .Include(i => i.Topic)
                .Where(i => i.OwnerId != user.Id)
                .Where(i => !i.IsSolved)
                .Where(i => i.HelpRequests.Any(hr => hr.HelperId == user.Id && hr.Status == HelpRequestStatus.Pending))
                .ToListAsync();
        }
    }
}

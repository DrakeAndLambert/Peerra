using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrakeLambert.Peerra.Data;
using DrakeLambert.Peerra.Entities;
using Microsoft.EntityFrameworkCore;

namespace DrakeLambert.Peerra.Services
{
    public class UserIssueService : IUserIssueService
    {
        private readonly ApplicationDbContext _context;

        public UserIssueService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Issue>> GetImportantIssuesAsync(ApplicationUser user)
        {
            return await _context.Issues
                    .Where(i => i.OwnerId == user.Id)
                    .Where(i => !i.IsSolved)
                    .Include(i => i.HelpRequests)
                    .OrderByDescending(i => i.UnseenHelpRequestsCount)
                    .ToListAsync();
        }

        public async Task<List<Issue>> GetOthersIssuesAsync(ApplicationUser user)
        {
            return await _context.Issues
                .Where(i => i.OwnerId != user.Id)
                .Where(i => !i.IsSolved)
                .Include(i => i.HelpRequests)
                .Where(i => !i.HelpRequests.Any(hr => hr.HelperId == user.Id))
                .OrderBy(i => i.HelpRequests.Count(hr => hr.Status == HelpRequestStatus.Responded))
                .ToListAsync();
        }

        public async Task<List<Issue>> GetOthersTargetedIssuesAsync(ApplicationUser user)
        {
            return await _context.Issues
                .Where(i => i.OwnerId != user.Id)
                .Where(i => !i.IsSolved)
                .Include(i => i.HelpRequests)
                .Where(i => i.HelpRequests.Any(hr => hr.HelperId == user.Id && hr.Status == HelpRequestStatus.Pending))
                .ToListAsync();
        }
    }
}

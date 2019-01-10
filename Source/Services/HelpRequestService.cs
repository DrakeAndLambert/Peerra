using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrakeLambert.Peerra.Data;
using DrakeLambert.Peerra.Entities;
using Microsoft.Extensions.Logging;

namespace DrakeLambert.Peerra.Services
{
    public class HelpRequestService : IHelpRequestService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HelpRequestService> _logger;

        public HelpRequestService(ApplicationDbContext context, ILogger<HelpRequestService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task RequestHelpAsync(Issue issue)
        {
            await _context.Entry(issue).Collection(i => i.HelpRequests).LoadAsync();
            var currentHelpers = issue.HelpRequests.Select(hr => hr.HelperId).ToList();

            await _context.Entry(issue).Reference(i => i.Topic).LoadAsync();

            var newHelpers = await GetClosestUserIdsAsync(issue.Topic, 10, currentHelpers);

            var newHelpRequests = newHelpers.Select(helperId => new HelpRequest
            {
                HelperId = helperId,
                IssueId = issue.Id
            });

            _context.HelpRequests.AddRange(newHelpRequests);

            await _context.SaveChangesAsync();
        }

        private async Task<List<Guid>> GetClosestUserIdsAsync(Topic topic, int minimumUserCount, List<Guid> excludedUserIds)
        {
            var closestUserIds = await GetDownstreamUserIdsAsync(topic, minimumUserCount, excludedUserIds);

            if (closestUserIds.Count < minimumUserCount)
            {
                await _context.Entry(topic).Reference(t => t.Parent).LoadAsync();
                if (topic.Parent != null)
                {
                    closestUserIds.AddRange(await GetClosestUserIdsAsync(topic.Parent, minimumUserCount - closestUserIds.Count, closestUserIds));
                }
            }

            return closestUserIds;
        }

        private async Task<List<Guid>> GetDownstreamUserIdsAsync(Topic topic, int minimumUserCount, IEnumerable<Guid> excludedUserIds)
        {
            await _context.Entry(topic).Collection(t => t.UserTopics).LoadAsync();
            var downstreamUserIds = topic.UserTopics.Select(ut => ut.UserId).Except(excludedUserIds).ToList();

            if (downstreamUserIds.Count < minimumUserCount)
            {
                await _context.Entry(topic).Collection(t => t.Children).LoadAsync();
                foreach (var child in topic.Children)
                {
                    if (downstreamUserIds.Count >= minimumUserCount)
                    {
                        break;
                    }
                    downstreamUserIds.AddRange(await GetDownstreamUserIdsAsync(child, minimumUserCount - downstreamUserIds.Count, downstreamUserIds.Concat(excludedUserIds)));
                }
            }

            return downstreamUserIds;
        }
    }
}

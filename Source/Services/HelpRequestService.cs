using System;
using System.Collections.Generic;
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
            _logger.LogInformation("Requesting help for issue {id}.", issue.Id);

            await _context.Entry(issue).Reference(i => i.Topic).LoadAsync();
            if (issue.Topic == null)
            {
                _logger.LogError("Issue does not have a valid topic.");
                throw new InvalidOperationException("Issue must have a valid topic.");
            }

            var relatedUserTopics = await GetRelatedUserTopicsAsync(issue.Topic, 10);

            foreach (var userTopic in relatedUserTopics)
            {
                var helpRequest = new HelpRequest
                {
                    IsAccepted = false,
                    HelperId = userTopic.UserId,
                    IssueId = issue.Id
                };

                _context.HelpRequests.Add(helpRequest);
            }

            await _context.SaveChangesAsync();
        }

        private async Task<HashSet<UserTopic>> GetRelatedUserTopicsAsync(Topic topic, int minimumHelpers)
        {
            var userTopics = await GetDownstreamUserTopicsAsync(topic, minimumHelpers);

            if (userTopics.Count < minimumHelpers)
            {
                await _context.Entry(topic).Reference(t => t.Parent).LoadAsync();

                if (topic.Parent != null)
                {
                    userTopics = await GetRelatedUserTopicsAsync(topic.Parent, minimumHelpers - userTopics.Count);
                }
            }

            return userTopics;
        }

        private async Task<HashSet<UserTopic>> GetDownstreamUserTopicsAsync(Topic topic, int minimumHelpers, HashSet<UserTopic> exclusions = null)
        {
            if (minimumHelpers <= 0)
            {
                return new HashSet<UserTopic>(0);
            }

            await _context.Entry(topic).Collection(t => t.UserTopics).LoadAsync();

            var downstreamUserTopics = new HashSet<UserTopic>(topic.UserTopics);

            if (exclusions == null)
            {
                exclusions = new HashSet<UserTopic>();
            }

            downstreamUserTopics.ExceptWith(exclusions);

            if (downstreamUserTopics.Count < minimumHelpers)
            {
                await _context.Entry(topic).Collection(t => t.Children).LoadAsync();

                foreach (var child in topic.Children)
                {
                    downstreamUserTopics.UnionWith(await GetDownstreamUserTopicsAsync(child, minimumHelpers - downstreamUserTopics.Count));
                }
            }

            return downstreamUserTopics;
        }
    }
}

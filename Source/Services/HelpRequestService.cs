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
                    HelperId = userTopic.UserId,
                    IssueId = issue.Id,
                    RelatedUserTopicId = userTopic.Id,
                    Status = HelpRequestStatus.Pending
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
                return UserTopicHashSetFactory.NewZeroCapacity();
            }

            await _context.Entry(topic).Collection(t => t.UserTopics).LoadAsync();

            var downstreamUserTopics = UserTopicHashSetFactory.New(topic.UserTopics);

            if (exclusions == null)
            {
                exclusions = UserTopicHashSetFactory.New();
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

        private class UserTopicUserIdComparer : IEqualityComparer<UserTopic>
        {
            public bool Equals(UserTopic x, UserTopic y)
            {
                return x.UserId.Equals(y.UserId);
            }

            public int GetHashCode(UserTopic obj)
            {
                return obj.UserId.GetHashCode();
            }
        }

        private static class UserTopicHashSetFactory
        {
            public static HashSet<UserTopic> New()
            {
                return new HashSet<UserTopic>(new UserTopicUserIdComparer());
            }

            public static HashSet<UserTopic> New(IEnumerable<UserTopic> userTopics)
            {
                return new HashSet<UserTopic>(userTopics, new UserTopicUserIdComparer());
            }

            public static HashSet<UserTopic> NewZeroCapacity()
            {
                return new HashSet<UserTopic>(0, new UserTopicUserIdComparer());
            }
        }
    }
}

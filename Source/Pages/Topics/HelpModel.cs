using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrakeLambert.Peerra.Data;
using DrakeLambert.Peerra.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DrakeLambert.Peerra.Pages.Topics
{
    public class HelpModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public Topic Topic { get; set; }

        public IEnumerable<(ApplicationUser User, int RelatedTopicCount)> PrimaryUsers { get; set; }

        public IEnumerable<(ApplicationUser User, int RelatedTopicCount)> SecondaryUsers { get; set; }

        public HelpModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet([FromRoute] Guid id)
        {
            Topic = await _context.Topics.FindAsync(id);
            if (Topic == null)
            {
                return RedirectToPage("/NotFound", new { message = "Your topic may have moved. You can try searching for it again, or contact support." });
            }

            var relatedTopics = _context.Topics.Where(
                relatedTopic => relatedTopic.ParentId == Topic.ParentId
            );

            var usersOfRelatedTopics = (await relatedTopics.Join(
                inner: _context.UserTopics,
                outerKeySelector: relatedTopic => relatedTopic.Id,
                innerKeySelector: userTopic => userTopic.TopicId,
                resultSelector: (relatedTopic, userTopic) => userTopic
            ).GroupBy(
                keySelector: userTopic => userTopic.UserId,
                elementSelector: userTopic => userTopic.TopicId
            ).OrderByDescending(
                userGroup => userGroup.Count()
            ).OrderByDescending(
                userGroup => userGroup.Any(
                    topicId => topicId == id
                )
            ).Join(
                inner: _context.Users,
                outerKeySelector: userGroup => userGroup.Key,
                innerKeySelector: user => user.Id,
                resultSelector: (userGroup, user) => new
                {
                    User = user,
                    Topics = userGroup.Join(
                        _context.Topics,
                        topicId => topicId,
                        topic => topic.Id,
                        (topicId, topic) => topic
                    )
                }
            ).Select(
                userGroup => new
                {
                    User = userGroup.User,
                    TopicCount = userGroup.Topics.Count(),
                    HasPrimaryTopic = userGroup.Topics.Any(
                        topic => topic.Id == id
                    )
                }
            ).ToListAsync()).Select(
                userGroup => new
                {
                    UserGroup = new Tuple<ApplicationUser, int>(userGroup.User, userGroup.TopicCount).ToValueTuple(),
                    HasPrimaryTopic = userGroup.HasPrimaryTopic
                }
            );

            PrimaryUsers = usersOfRelatedTopics.Where(
                userGroup => userGroup.HasPrimaryTopic
            ).Take(10).Select(
                userGroup => userGroup.UserGroup
            );

            SecondaryUsers = usersOfRelatedTopics.Where(
                userGroup => !userGroup.HasPrimaryTopic
            ).Take(10).Select(
                userGroup => userGroup.UserGroup
            );

            return Page();
        }
    }
}

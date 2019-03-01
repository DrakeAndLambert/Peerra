using System;
using System.Linq;
using System.Threading.Tasks;
using DrakeLambert.Peerra.Data;
using DrakeLambert.Peerra.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DrakeLambert.Peerra.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class TopicKnowledgeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TopicKnowledgeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("rootTopics")]
        public async Task<IActionResult> GetRootTopicsAsync()
        {
            var rootTopics = await _context.Topics.Where(topic => topic.ParentId == null)
                .Select(topic => new { Title = topic.Title, Description = topic.Description, Id = topic.Id })
                .ToListAsync();

            return Ok(rootTopics);
        }

        [HttpGet("topics/{id}")]
        public async Task<IActionResult> GetChildTopicsAsync([FromRoute] Guid id)
        {
            var childTopics = await _context.Topics.Where(topic => topic.ParentId == id)
                .Select(topic => new { Title = topic.Title, Description = topic.Description, Id = topic.Id })
                .ToListAsync();

            return Ok(childTopics);
        }

        [HttpPost("register/{id}")]
        public async Task<IActionResult> RegisterKnowledge([FromRoute] Guid id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var alreadyHasKnowledge = await _context.UserTopics.AnyAsync(userTopic => userTopic.TopicId == id && userTopic.UserId == user.Id);

            if (!alreadyHasKnowledge)
            {
                var newUserTopic = new UserTopic { TopicId = id, UserId = user.Id };
                _context.UserTopics.Add(newUserTopic);
                await _context.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpPost("unregister/{id}")]
        public async Task<IActionResult> UnregisterKnowledge([FromRoute] Guid id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var existingKnowledge = await _context.UserTopics.Where(userTopic => userTopic.TopicId == id && userTopic.UserId == user.Id).ToListAsync();

            if (existingKnowledge.Count > 0)
            {
                _context.UserTopics.RemoveRange(existingKnowledge);
                await _context.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpGet("has/{id}")]
        public async Task<IActionResult> HasKnowledge([FromRoute] Guid id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var alreadyHasKnowledge = await _context.UserTopics.AnyAsync(userTopic => userTopic.TopicId == id && userTopic.UserId == user.Id);

            return Ok(alreadyHasKnowledge);
        }
    }
}
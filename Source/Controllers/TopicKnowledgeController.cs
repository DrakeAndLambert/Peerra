using System;
using System.Linq;
using System.Threading.Tasks;
using DrakeLambert.Peerra.Data;
using Microsoft.AspNetCore.Authorization;
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

        public TopicKnowledgeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("rootTopics")]
        public async Task<IActionResult> GetRootTopicsAsync()
        {
            var rootTopics = await _context.Topics.Where(topic => topic.ParentId == null)
                .Select(topic => new { Title = topic.Title, Description = topic.Description, Id = topic.Id })
                .ToListAsync();

            return Ok(rootTopics);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetChildTopicsAsync(Guid id)
        {
            var childTopics = await _context.Topics.Where(topic => topic.ParentId == id)
                .Select(topic => new { Title = topic.Title, Description = topic.Description, Id = topic.Id })
                .ToListAsync();

            return Ok(childTopics);
        }
    }
}
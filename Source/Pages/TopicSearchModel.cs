using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrakeLambert.Peerra.Data;
using DrakeLambert.Peerra.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DrakeLambert.Peerra.Pages
{
    public class TopicSearchModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<TopicSearchModel> _logger;

        public Topic ParentTopic { get; set; }

        public IEnumerable<Topic> ChildTopics { get; set; }

        public TopicSearchModel(ApplicationDbContext context, ILogger<TopicSearchModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync([FromRoute] Guid id)
        {
            _logger.LogInformation("Retrieving topics for parent {id}.", id);
            ParentTopic = await _context.Topics.FindAsync(id);
            if (ParentTopic == null)
            {
                return RedirectToPage("NotFound", new { message = "Your topic may have moved. You can try searching for it again, or contact support." });
            }
            ChildTopics = await _context.Topics.Where(topic => topic.ParentId == id).ToListAsync();
            return Page();
        }
    }
}

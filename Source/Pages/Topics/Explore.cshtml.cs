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

namespace DrakeLambert.Peerra.Pages.Topics
{
    public class ExploreModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<ExploreModel> _logger;

        public Topic ParentTopic { get; set; }

        public IEnumerable<Topic> ChildTopics { get; set; }

        public ExploreModel(ApplicationDbContext context, ILogger<ExploreModel> logger)
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
                return RedirectToPage("/NotFound", new { message = "Your topic may have moved. You can try searching for it again on the search page." });
            }
            ChildTopics = await _context.Topics.Where(topic => topic.ParentId == id && topic.Approved).ToListAsync();
            return Page();
        }
    }
}

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
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<IndexModel> _logger;

        public IEnumerable<Topic> RootTopics { get; set; }

        public IndexModel(ApplicationDbContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            RootTopics = await _context.Topics.Where(topic => topic.ParentId == Guid.Empty).ToListAsync();
            return Page();
        }
    }
}

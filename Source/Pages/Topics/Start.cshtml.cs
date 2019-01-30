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
    public class StartModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<StartModel> _logger;

        public IEnumerable<Topic> RootTopics { get; set; }

        public StartModel(ApplicationDbContext context, ILogger<StartModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            RootTopics = await _context.Topics.Where(topic => topic.ParentId == null).ToListAsync();
            return Page();
        }
    }
}

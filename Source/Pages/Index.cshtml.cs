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

        public Issue ParentIssue { get; set; }

        public IEnumerable<Issue> ChildIssues { get; set; }

        public IndexModel(ApplicationDbContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync([FromRoute] Guid parentId)
        {
            _logger.LogInformation("Retrieving issues for parent {id}.", parentId);
            if (parentId != Guid.Empty)
            {
                ParentIssue = await _context.Issues.FindAsync(parentId);
            }
            ChildIssues = await _context.Issues.Where(issue => issue.ParentId == parentId).ToListAsync();
            return Page();
        }
    }
}

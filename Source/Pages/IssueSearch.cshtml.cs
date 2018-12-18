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
    public class IssueSearchModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<IssueSearchModel> _logger;

        public Issue ParentIssue { get; set; }

        public IEnumerable<Issue> ChildIssues { get; set; }

        public IssueSearchModel(ApplicationDbContext context, ILogger<IssueSearchModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync([FromRoute] Guid id)
        {
            _logger.LogInformation("Retrieving issues for parent {id}.", id);
            ParentIssue = await _context.Issues.FindAsync(id);
            if (ParentIssue == null)
            {
                return NotFound("Sorry, that issue could not be found.");
            }
            ChildIssues = await _context.Issues.Where(issue => issue.ParentId == id).ToListAsync();
            return Page();
        }
    }
}

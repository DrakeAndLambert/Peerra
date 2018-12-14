using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrakeLambert.Peerra.Data;
using DrakeLambert.Peerra.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DrakeLambert.Peerra.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public Issue ParentIssue { get; set; }

        public IEnumerable<Issue> ChildIssues { get; set; }

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            ChildIssues = await _context.Issues.Where(issue => issue.ParentId == Guid.Empty).ToListAsync();
            return Page();
        }
    }
}

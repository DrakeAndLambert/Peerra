using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrakeLambert.Peerra.Data;
using DrakeLambert.Peerra.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DrakeLambert.Peerra.Pages.Issues
{
    [Authorize]
    public class MyModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MyModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public Issue Issue { get; set; }

        public MyModel(ApplicationDbContext context, ILogger<MyModel> logger, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync([FromRoute] Guid id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToPage("/Error");
            }

            var issue = await _context.Issues.FindAsync(id);

            if (issue == null || user.Id != issue.OwnerId)
            {
                return RedirectToPage("/NotFound", new { message = "Your issue may have been removed or moved. Return home to view all your issues." });
            }

            await _context.Entry(issue).Reference(i => i.Topic).LoadAsync();
            await _context.Entry(issue).Collection(i => i.HelpRequests).LoadAsync();

            Issue = issue;

            return Page();
        }
    }
}

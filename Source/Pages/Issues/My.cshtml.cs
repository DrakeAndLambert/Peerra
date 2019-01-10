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
        public IEnumerable<HelpRequest> Responses => Issue.HelpRequests.Where(h => h.Status == HelpRequestStatus.Responded);

        public int RespondedCount => Responses.Count();
        public int PendingCount => Issue.HelpRequests.Where(h => h.Status == HelpRequestStatus.Responded).Count();
        public int DeclinedCount => Issue.HelpRequests.Count(h => h.Status == HelpRequestStatus.Responded);

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

            Issue = await _context.Issues
                .Include(i => i.Topic)
                .Include(i => i.HelpRequests)
                    .ThenInclude(h => h.Helper)
                .Include(i => i.HelpRequests)
                    .ThenInclude(h => h.RelatedUserTopic)
                        .ThenInclude(u => u.Topic)
                .SingleOrDefaultAsync(i => i.Id == id);

            if (Issue == null || user.Id != Issue.OwnerId)
            {
                return RedirectToPage("/NotFound", new { message = "Your issue may have been removed or moved. Return home to view all your issues." });
            }

            var random = new Random();
            foreach (var request in Issue.HelpRequests)
            {
                if (random.NextDouble() > 0.5)
                {
                    request.Message = "Hi, I'd like to help you.";
                    request.Status = HelpRequestStatus.Responded;
                }
                else if (random.NextDouble() > 0.5)
                {
                    request.Status = HelpRequestStatus.Declined;
                }
            }

            return Page();
        }
    }
}

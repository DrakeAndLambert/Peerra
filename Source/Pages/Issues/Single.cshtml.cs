using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrakeLambert.Peerra.Data;
using DrakeLambert.Peerra.Entities;
using DrakeLambert.Peerra.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DrakeLambert.Peerra.Pages.Issues
{
    [Authorize]
    public class SingleModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SingleModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public Issue Issue { get; set; }
        public IEnumerable<HelpRequest> Responses => Issue.HelpRequests.Where(h => h.Status == HelpRequestStatus.Responded);

        public int RespondedCount => Responses.Count();
        public int PendingCount => Issue.HelpRequests.Where(h => h.Status == HelpRequestStatus.Pending).Count();
        public int DeclinedCount => Issue.HelpRequests.Count(h => h.Status == HelpRequestStatus.Declined);

        public SingleModel(ApplicationDbContext context, ILogger<SingleModel> logger, UserManager<ApplicationUser> userManager)
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
                .SingleOrDefaultAsync(i => i.Id == id);

            if (Issue == null || user.Id != Issue.OwnerId)
            {
                return RedirectToPage("/NotFound", new { message = "Your issue may have been removed or moved. Return home to view all your issues." });
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync([FromRoute] Guid id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToPage("/Error");
            }

            Issue = await _context.Issues.FindAsync(id);

            if (Issue == null || user.Id != Issue.OwnerId)
            {
                return RedirectToPage("/NotFound", new { message = "Your issue may have been removed or moved. Return home to view all your issues." });
            }

            await _context.Entry(Issue).Collection(i => i.HelpRequests).LoadAsync();

            _context.RemoveRange(Issue.HelpRequests);
            _context.Remove(Issue);

            await _context.SaveChangesAsync();

            return RedirectToPage("/Issues/All");
        }

        public async Task<IActionResult> OnPostSolvedAsync([FromRoute] Guid id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToPage("/Error");
            }

            Issue = await _context.Issues.FindAsync(id);

            if (Issue == null || user.Id != Issue.OwnerId)
            {
                return RedirectToPage("/NotFound", new { message = "Your issue may have been removed or moved. Return home to view all your issues." });
            }

            await _context.Entry(Issue).Collection(i => i.HelpRequests).LoadAsync();

            Issue.IsSolved = true;

            _context.RemoveRange(Issue.HelpRequests.Where(h => h.Status == HelpRequestStatus.Pending));

            await _context.SaveChangesAsync();

            return RedirectToPage("Single", new { id = id });
        }

        public async Task<IActionResult> OnPostReOpenAsync([FromRoute] Guid id, [FromServices] IHelpRequestService helpRequestService)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToPage("/Error");
            }

            Issue = await _context.Issues.FindAsync(id);

            if (Issue == null || user.Id != Issue.OwnerId)
            {
                return RedirectToPage("/NotFound", new { message = "Your issue may have been removed or moved. Return home to view all your issues." });
            }

            Issue.IsSolved = false;

            await _context.Entry(Issue).Collection(i => i.HelpRequests).LoadAsync();

            if (Issue.HelpRequests.Where(hr => hr.Status != HelpRequestStatus.Declined).Count() == 0)
            {
                await helpRequestService.RequestHelpAsync(Issue);
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("Single", new { id = id });
        }

        public async Task<IActionResult> OnPostMoreHelpAsync([FromRoute] Guid id, [FromServices] IHelpRequestService helpRequestService)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToPage("/Error");
            }

            Issue = await _context.Issues.FindAsync(id);

            if (Issue == null || user.Id != Issue.OwnerId)
            {
                return RedirectToPage("/NotFound", new { message = "Your issue may have been removed or moved. Return home to view all your issues." });
            }

            await helpRequestService.RequestHelpAsync(Issue);

            return RedirectToPage("Single", new { id = id });
        }
    }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using DrakeLambert.Peerra.Data;
using DrakeLambert.Peerra.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DrakeLambert.Peerra.Pages.Issues
{
    [Authorize]
    public class RespondModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public Issue Issue { get; set; }

        [BindProperty]
        public HelpRequest HelpRequest { get; set; }

        public RespondModel(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGetAsync([FromRoute] Guid id)
        {
            Issue = await _context.Issues
                .Include(i => i.HelpRequests)
                .Include(i => i.Owner)
                .Include(i => i.Topic)
                .SingleOrDefaultAsync(i => i.Id == id);

            if (Issue == null)
            {
                return RedirectToPage("/NotFound", new { message = "This issue may have been removed or moved." });
            }

            var user = await _signInManager.UserManager.GetUserAsync(User);

            if (user.Id == Issue.OwnerId)
            {
                return RedirectToPage("/Issues/Single", new { id = id });
            }

            HelpRequest = Issue.HelpRequests.SingleOrDefault(hr => hr.HelperId == user.Id);

            if (HelpRequest == null)
            {
                HelpRequest = new HelpRequest
                {
                    Helper = user,
                    Issue = Issue
                };

                Issue.HelpRequests.Add(HelpRequest);
            }

            await _context.SaveChangesAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync([FromRoute] Guid id)
        {
            Issue = await _context.Issues
                .Include(i => i.HelpRequests)
                .Include(i => i.Owner)
                .Include(i => i.Topic)
                .SingleOrDefaultAsync(i => i.Id == id);

            if (Issue == null)
            {
                return RedirectToPage("/NotFound", new { message = "This issue may have been removed or moved." });
            }

            var user = await _signInManager.UserManager.GetUserAsync(User);

            if (user.Id == Issue.OwnerId)
            {
                return RedirectToPage("/Issues/Single", new { id = id });
            }

            var existingHelpRequest = Issue.HelpRequests.SingleOrDefault(hr => hr.HelperId == user.Id);

            if (existingHelpRequest == null)
            {
                existingHelpRequest = new HelpRequest
                {
                    Helper = user,
                    Issue = Issue
                };

                Issue.HelpRequests.Add(existingHelpRequest);
            }

            existingHelpRequest.Message = HelpRequest.Message;
            existingHelpRequest.Status = HelpRequestStatus.Responded;

            await _context.SaveChangesAsync();

            return RedirectToPage("Peer", new { id = id });
        }
    }
}

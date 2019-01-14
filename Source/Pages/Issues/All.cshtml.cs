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

namespace DrakeLambert.Peerra.Pages.Issues
{
    [Authorize]
    public class AllModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public List<Issue> MyIssues { get; set; }

        public List<Issue> OthersIssues { get; set; }

        public AllModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToPage("/Error");
            }

            

            OthersIssues = await _context.HelpRequests.Include(hr => hr.Issue).Where(hr => hr.HelperId == user.Id).Where(hr => hr.Status != HelpRequestStatus.Declined).Select(hr => hr.Issue).ToListAsync();

            MyIssues = await _context.Issues.Include(i => i.HelpRequests).Where(i => i.OwnerId == user.Id).OrderBy(i => i.IsSolved).ToListAsync();

            return Page();
        }
    }
}

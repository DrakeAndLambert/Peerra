using System.Collections.Generic;
using System.Threading.Tasks;
using DrakeLambert.Peerra.Data;
using DrakeLambert.Peerra.Entities;
using DrakeLambert.Peerra.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DrakeLambert.Peerra.Pages.Issues
{
    [Authorize]
    public class PeersModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserIssueService _userIssueService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public List<Issue> TargetedIssues { get; set; }
        public List<Issue> AllIssues { get; set; }

        public PeersModel(ApplicationDbContext context, IUserIssueService userIssueService, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userIssueService = userIssueService;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _signInManager.UserManager.GetUserAsync(User);
            AllIssues = await _userIssueService.GetOthersIssuesAsync(user);
            TargetedIssues = await _userIssueService.GetOthersTargetedIssuesAsync(user);

            return Page();
        }
    }
}

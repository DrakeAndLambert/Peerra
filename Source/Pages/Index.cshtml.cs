using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrakeLambert.Peerra.Entities;
using DrakeLambert.Peerra.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DrakeLambert.Peerra.Pages
{
    public class IndexModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserIssueService _userIssueService;

        public List<Issue> MyIssues { get; set; }
        public List<Issue> OthersIssues { get; set; }
        public bool IsSignedIn { get; set; } = false;

        public IndexModel(SignInManager<ApplicationUser> signInManager, IUserIssueService userIssueService)
        {
            _signInManager = signInManager;
            _userIssueService = userIssueService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _signInManager.UserManager.GetUserAsync(User);
            if (_signInManager.IsSignedIn(User) && user != null)
            {
                IsSignedIn = true;
                MyIssues = (await _userIssueService.GetImportantIssuesAsync(user)).Take(10).ToList();
                OthersIssues = (await _userIssueService.GetOthersTargetedIssuesAsync(user))
                    .Concat(await _userIssueService.GetOthersIssuesAsync(user))
                    .Take(10).ToList();
            }
            return Page();
        }
    }
}

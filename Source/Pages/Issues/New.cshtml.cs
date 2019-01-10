using System;
using System.Threading.Tasks;
using DrakeLambert.Peerra.Data;
using DrakeLambert.Peerra.Entities;
using DrakeLambert.Peerra.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace DrakeLambert.Peerra.Pages.Issues
{
    [Authorize]
    public class NewModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<NewModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHelpRequestService _helpRequestService;

        public Topic ParentTopic { get; set; }

        [BindProperty]
        public Issue NewIssue { get; set; }

        public NewModel(ApplicationDbContext context, ILogger<NewModel> logger, UserManager<ApplicationUser> userManager, IHelpRequestService helpRequestService)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _helpRequestService = helpRequestService;
        }

        public async Task<IActionResult> OnGetAsync([FromRoute] Guid id)
        {
            if (id != Guid.Empty)
            {
                ParentTopic = await _context.Topics.FindAsync(id);
                if (ParentTopic == null)
                {
                    return RedirectToPage("/NotFound", new { message = "We couldn't find the parent topic from your link. This may be because the topic has been deleted. You can use the topic search on the home page to try to find it again." });
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync([FromRoute] Guid id)
        {
            if (id != Guid.Empty)
            {
                ParentTopic = await _context.Topics.FindAsync(id);
                if (ParentTopic == null)
                {
                    return RedirectToPage("/NotFound", new { message = "We couldn't find the parent topic from your link. This may be because the topic has been deleted. You can use the topic search on the home page to try to find it again." });
                }
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToPage("/Error");
            }

            NewIssue.OwnerId = user.Id;

            NewIssue.TopicId = ParentTopic.Id;

            _context.Issues.Add(NewIssue);

            await _context.SaveChangesAsync();

            await _helpRequestService.RequestHelpAsync(NewIssue);

            return RedirectToPage("Single", new { Id = NewIssue.Id });
        }
    }
}

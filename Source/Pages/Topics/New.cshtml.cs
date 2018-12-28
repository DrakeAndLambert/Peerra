using System;
using System.Threading.Tasks;
using DrakeLambert.Peerra.Data;
using DrakeLambert.Peerra.Entities;
using DrakeLambert.Peerra.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace DrakeLambert.Peerra.Pages.Topics
{
    [Authorize]
    public class NewModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<NewModel> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public Topic ParentTopic { get; private set; }

        [BindProperty]
        public NewTopic NewTopic { get; set; }

        public NewModel(ApplicationDbContext context, ILogger<NewModel> logger, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _logger = logger;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGetAsync([FromRoute] Guid id)
        {
            _logger.LogInformation("Retrieving topics for parent {id}.", id);
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
            _logger.LogInformation("Adding new topic for parent {id}.", id);
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

            var user = await _signInManager.UserManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                await _signInManager.SignOutAsync();
                return RedirectToRoute("/Identity/Account/Logout");
            }

            var topicRequest = new Topic
            {
                Title = NewTopic.Title,
                Description = NewTopic.Description,
                IsLeaf = !NewTopic.IsNotLeaf,
                ParentId = id,
                OwnerId = user.Id,
                Approved = false
            };

            _context.Topics.Add(topicRequest);

            await _context.SaveChangesAsync();

            return RedirectToPage("/Topics/Explore", new { id = topicRequest.Id });
        }
    }
}

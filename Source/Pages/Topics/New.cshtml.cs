using System;
using System.Threading.Tasks;
using DrakeLambert.Peerra.Data;
using DrakeLambert.Peerra.Entities;
using DrakeLambert.Peerra.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace DrakeLambert.Peerra.Pages.Topics
{
    public class NewModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<NewModel> _logger;

        public Topic ParentTopic { get; private set; }

        [BindProperty]
        public NewTopic NewTopic { get; set; }

        public NewModel(ApplicationDbContext context, ILogger<NewModel> logger)
        {
            _context = context;
            _logger = logger;
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
    }
}

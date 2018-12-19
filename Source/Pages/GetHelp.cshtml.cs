using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrakeLambert.Peerra.Data;
using DrakeLambert.Peerra.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DrakeLambert.Peerra.Pages
{
    public class GetHelpModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public Topic Topic { get; set; }

        public IEnumerable<ApplicationUser> HelpUsers { get; set; }

        public GetHelpModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet([FromRoute] Guid id)
        {
            Topic = await _context.Topics.FindAsync(id);
            if (Topic == null)
            {
                return NotFound("We could not find that topic.");
            }

            HelpUsers = await _context.Users
                .Join(
                    _context.UserTopics.Where(skill => skill.TopicId == id),
                    user => user.Id, userTopic => userTopic.UserId,
                    (user, skills) => user
                ).ToListAsync();

            return Page();
        }
    }
}

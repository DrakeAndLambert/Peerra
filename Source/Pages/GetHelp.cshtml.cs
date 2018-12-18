using System;
using System.Threading.Tasks;
using DrakeLambert.Peerra.Data;
using DrakeLambert.Peerra.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DrakeLambert.Peerra.Pages
{
    public class GetHelpModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public Issue Issue { get; set; }

        public GetHelpModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet([FromRoute] Guid id)
        {
            Issue = await _context.Issues.FindAsync(id);
            if (Issue == null)
            {
                return NotFound("We could not find that issue.");
            }
            return Page();
        }
    }
}
